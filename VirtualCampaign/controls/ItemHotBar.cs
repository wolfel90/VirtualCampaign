using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using VirtualCampaign.data;
using VirtualCampaign.sys;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class ItemHotBar : GameItemCollectionControl {
        public event EventHandler ItemsChanged;
        private List<KeyValuePair<ItemData, ItemImage>> items;
        private ItemDesigner designer;
        private ItemData designerTarget;
        private int offset;
        private int blocksize { get { return Bounds.Height - 2; } }
        private int maxblocks { get { return itemPanel.Width / (blocksize + 10); } }
        public bool adaptableTooltip { get; set; }

        public ItemHotBar() {
            InitializeComponent();
            items = new List<KeyValuePair<ItemData, ItemImage>>();
            GrabMode = NONE;
            DropMode = NONE;
            adaptableTooltip = true;
            designer = null;
            designerTarget = null;
        }

        public void addItem(ItemData newItem) {
            addItemAt(newItem, items.Count);
        }

        public void addItemAt(ItemData newItem, int index) {
            ItemImage image = new ItemImage();
            image.bgSrc = newItem.bgSrc;
            image.iconSrc = newItem.iconSrc;
            image.setBGColor(newItem.bgColor);
            image.setIconColor(newItem.iconColor);
            if(newItem.stackable && newItem.count > 1) {
                image.footStr = newItem.count.ToString();
            } else {
                image.footStr = "";
            }
            image.MouseDown += item_MouseDown;
            image.MouseEnter += item_MouseEnter;
            image.MouseLeave += item_MouseLeave;

            items.Insert(index, new KeyValuePair<ItemData, ItemImage>(newItem, image));
            itemPanel.Controls.Add(image);
            arrangeItems();
            onItemsChanged(this, EventArgs.Empty);
        }

        public void removeItemAt(int index) {
            if(index < items.Count && index >= 0) {
                KeyValuePair<ItemData, ItemImage> entry = items[index];
                entry.Value.MouseDown -= item_MouseDown;
                entry.Value.MouseEnter -= item_MouseEnter;
                entry.Value.MouseLeave -= item_MouseLeave;
                itemPanel.Controls.Remove(entry.Value);
                items.RemoveAt(index);
                arrangeItems();
                onItemsChanged(this, EventArgs.Empty);
            }
        }

        public int getItemCount() {
            return items.Count;
        }

        public List<ItemData> getItemList() {
            List<ItemData> list = new List<ItemData>();
            foreach(KeyValuePair<ItemData, ItemImage> item in items) {
                list.Add(item.Key);
            }
            return list;
        }

        private void arrangeItems() {
            for(int i = 0; i < items.Count; ++i) {
                KeyValuePair<ItemData, ItemImage> entry = items[i];
                entry.Value.Bounds = new Rectangle((i - offset) * (blocksize + 10) + 10, 1, blocksize, blocksize);
                if (i < offset || entry.Value.Bounds.X > itemPanel.Width) {
                    entry.Value.Visible = false;
                } else {
                    entry.Value.Visible = true;
                }
            }
        }

        private void rightButton_Click(object sender, EventArgs e) {
            if(offset + maxblocks < items.Count) {
                offset++;
                arrangeItems();
            }
        }

        private void leftButton_Click(object sender, EventArgs e) {
            if (offset > 0) {
                offset--;
                arrangeItems();
            }
        }
        
        private void ItemHotBar_SizeChanged(object sender, EventArgs e) {
            arrangeItems();
        }

        private void itemPanel_MouseDown(object sender, MouseEventArgs e) {
            if (DragControlHandler.getCarriedData() != null) {
                if (DragControlHandler.getCarriedData() is ItemData) {
                    int targetIndex = 0;
                    for (int i = 0; i < items.Count; ++i) {
                        if(items[i].Value.Location.X > e.X) {
                            targetIndex = i;
                            break;
                        }
                        if (i == items.Count - 1) targetIndex = items.Count;
                    }
                    switch(DropMode) {
                        case NONE:
                            break;
                        case CLONE:
                            addItemAt((ItemData)DragControlHandler.getCarriedData(), targetIndex);
                            break;
                        case MOVE:
                            addItemAt((ItemData)DragControlHandler.getCarriedData(), targetIndex);
                            if((ModifierKeys & Keys.Control) != Keys.Control) {
                                DragControlHandler.clearCarriedData();
                            }
                            break;
                    }
                }
            }
        }

        private void item_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if(sender is ItemImage) {
                if(e.Button == MouseButtons.Left) {
                    for (int i = 0; i < items.Count; ++i) {
                        KeyValuePair<ItemData, ItemImage> entry = items[i];
                        if (entry.Value == (ItemImage)sender) {
                            if (DragControlHandler.getCarriedData() != null) {
                                if (DragControlHandler.getCarriedData() is ItemData) {
                                    ItemData carried = (ItemData)DragControlHandler.getCarriedData();
                                    switch (DropMode) {
                                        case NONE:
                                            break;
                                        case CLONE:
                                            addItemAt(new ItemData(carried), i);
                                            break;
                                        case MOVE:
                                            addItemAt(carried, i);
                                            if ((ModifierKeys & Keys.Control) != Keys.Control) {
                                                DragControlHandler.clearCarriedData();
                                            }
                                            break;
                                    }
                                }
                            } else {
                                switch (GrabMode) {
                                    case NONE:
                                        break;
                                    case CLONE:
                                        DragControlHandler.setCarriedData(new ItemData(entry.Key));
                                        break;
                                    case MOVE:
                                        DragControlHandler.setCarriedData(entry.Key);
                                        if ((ModifierKeys & Keys.Control) != Keys.Control) {
                                            removeItemAt(i);
                                        }
                                        break;
                                }
                            }
                            break;
                        }
                    }
                } else if(e.Button == MouseButtons.Right) {
                    foreach(KeyValuePair<ItemData, ItemImage> entry in items) {
                        if(entry.Value == (ItemImage)sender) {
                            designerTarget = entry.Key;
                            designer = new ItemDesigner(designerTarget);
                            DialogResult result = designer.ShowDialog();
                            if (result == DialogResult.OK) {
                                if (designerTarget != null) {
                                    if (entry.Key == designerTarget) {
                                        designerTarget = designer.resultItem;
                                        entry.Value.setIconSrc(designer.resultItem.iconSrc);
                                        entry.Value.setBGSrc(designer.resultItem.bgSrc);
                                        entry.Value.setIconColor(designer.resultItem.iconColor);
                                        entry.Value.setBGColor(designer.resultItem.bgColor);
                                        if(designer.resultItem.stackable && designer.resultItem.count > 1) {
                                            entry.Value.footStr = designer.resultItem.count.ToString();
                                        } else {
                                            entry.Value.footStr = "";
                                        }
                                        entry.Value.Invalidate();
                                    }
                                }
                            }
                            designerTarget = null;
                            break;
                        }
                    }
                }
                
            }
        }

        private void item_MouseEnter(object sender, EventArgs e) {
            if (adaptableTooltip) {
                if(sender is ItemImage) {
                    ItemImage ii = (ItemImage)sender;
                    AdaptableTooltipHandler.CurrentControl = ii;
                    foreach (KeyValuePair<ItemData, ItemImage> item in items) {
                        if(item.Value == ii) {
                            AdaptableTooltipHandler.Tooltip.generateTooltip(item.Key);
                            AdaptableTooltipHandler.Tooltip.Show();
                            AdaptableTooltipHandler.Tooltip.Location = ii.PointToScreen(new Point(0 + ii.Width + 5, 0));
                        }
                    }
                }
            }
        }

        private void item_MouseLeave(object sender, EventArgs e) {
            if (sender is ItemImage) {
                ItemImage ii = (ItemImage)sender;
                if (AdaptableTooltipHandler.CurrentControl == ii) {
                    AdaptableTooltipHandler.ClearTooltip();
                }
            }
        }

        public void onItemsChanged(object source, EventArgs e) {
            ItemsChanged?.Invoke(source, e);
        }
    }
}
