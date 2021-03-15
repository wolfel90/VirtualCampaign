using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;
using VirtualCampaign.sys;

namespace VirtualCampaign.controls {
    public partial class ItemGrid : GameItemCollectionControl {
        public static int DEFAULT_BLOCK_SIZE = 64;
        public event EventHandler ItemsChanged;
        private List<KeyValuePair<ItemData, ItemImage>> itemList;
        public bool generateTooltips;
        private int _blockSize = DEFAULT_BLOCK_SIZE;
        private int blockSize { get { return _blockSize; } set { SetBlockSize(value); } }
        public int blockWidth { get { return (this.Width / blockSize >= 1) ? this.Width / blockSize : 1; } }
        public int blockHeight { get { return ((int)Math.Ceiling((double)itemList.Count / (double)blockWidth) >= 1) ? (int)Math.Ceiling((double)itemList.Count / (double)blockWidth) : 1; } }

        public ItemGrid() {
            InitializeComponent();
            generateTooltips = true;
            GrabMode = NONE;
            DropMode = NONE;
            itemList = new List<KeyValuePair<ItemData, ItemImage>>();
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void SetBlockSize(int size) {
            _blockSize = size;
            ArrangeItems();
        }

        public int CalculateNewHeight(int newWidth) {
            int newBlockWidth = (newWidth / blockSize >= 1) ? newWidth / blockSize : 1;
            return ((int)Math.Ceiling((double)itemList.Count / (double)newBlockWidth) >= 1) ? (int)Math.Ceiling((double)itemList.Count / (double)newBlockWidth) * blockSize: blockSize;
        }

        public void AddItem(ItemData item) {
            AddItemAt(item, itemList.Count);
        }

        public void AddItemAt(ItemData item, int index) {
            if (index < 0) index = 0;
            if (index > itemList.Count) index = itemList.Count;

            ItemImage image = new ItemImage();
            image.bgSrc = item.bgSrc;
            image.iconSrc = item.iconSrc;
            image.setBGColor(item.bgColor);
            image.setIconColor(item.iconColor);
            if(item.stackable && item.count > 1) {
                image.footStr = item.count.ToString();
            } else {
                image.footStr = "";
            }
            image.MouseDown += item_MouseDown;
            image.MouseEnter += item_MouseEnter;
            image.MouseLeave += item_MouseLeave;
            itemList.Insert(index, new KeyValuePair<ItemData, ItemImage>(item, image));
            for (int n = index; n < itemList.Count; ++n) {
                itemList[n].Value.Bounds = new Rectangle((n % blockWidth) * blockSize, (n / blockWidth * blockSize), blockSize, blockSize);
            }
            //image.Bounds = new Rectangle(((itemList.Count - 1) % blockWidth) * blockSize, ((itemList.Count - 1) / blockWidth) * blockSize, blockSize, blockSize);
            this.Size = new Size(this.Width, blockHeight * blockSize);
            Controls.Add(image);
            onItemsChanged(this, EventArgs.Empty);
        }

        public void RemoveItemAt(int index) {
            if (index >= 0 && index < itemList.Count) {
                RemoveItem(itemList[index].Key);
            }
        }

        public void RemoveItem(ItemData item) {
            for (int i = 0; i < itemList.Count; ++i) {
                if (itemList[i].Key == item) {
                    Controls.Remove(itemList[i].Value);
                    itemList[i].Value.MouseDown -= item_MouseDown;
                    itemList[i].Value.MouseEnter -= item_MouseEnter;
                    itemList[i].Value.MouseLeave -= item_MouseLeave;
                    itemList.RemoveAt(i);
                    for (int n = i; n < itemList.Count; ++n) {
                        itemList[n].Value.Bounds = new Rectangle((n % blockWidth) * blockSize, (n / blockWidth * blockSize), blockSize, blockSize);
                    }
                    onItemsChanged(this, EventArgs.Empty);
                    break;
                }
            }
        }

        public ItemData GetItemAt(int index) {
            if (index >= 0 && index < itemList.Count) {
                return itemList[index].Key;
            }
            return null;
        }

        public ItemImage GetItemImageAt(int index) {
            if (index >= 0 && index < itemList.Count) {
                return itemList[index].Value;
            }
            return null;
        }

        private void ArrangeItems() {
            for (int i = 0; i < itemList.Count; ++i) {
                itemList[i].Value.Bounds = new Rectangle((i % blockWidth) * blockSize, (i / blockWidth) * blockSize, blockSize, blockSize);
            }
            this.Size = new Size(this.Width, blockHeight * blockSize);
            Invalidate();
        }

        private void itemPanel_MouseDown(object sender, MouseEventArgs e) {

        }

        private void item_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if(sender is ItemImage && e.Button == MouseButtons.Left) {
                for (int i = 0; i < itemList.Count; ++i) {
                    KeyValuePair<ItemData, ItemImage> entry = itemList[i];
                    if (entry.Value == (ItemImage)sender) {
                        if (DragControlHandler.getCarriedData() != null) {
                            if (DragControlHandler.getCarriedData() is ItemData) {
                                ItemData carried = (ItemData)DragControlHandler.getCarriedData();
                                switch (DropMode) {
                                    case NONE:
                                        break;
                                    case CLONE:
                                        AddItemAt(new ItemData(carried), i);
                                        break;
                                    case MOVE:
                                        AddItemAt(carried, i);
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
                                        RemoveItemAt(i);
                                    }
                                    break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void item_MouseEnter(object sender, EventArgs e) {
            if (generateTooltips) {
                if (sender is ItemImage) {
                    ItemImage ii = (ItemImage)sender;
                    AdaptableTooltipHandler.CurrentControl = ii;
                    foreach (KeyValuePair<ItemData, ItemImage> item in itemList) {
                        if (item.Value == ii) {
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
                    AdaptableTooltipHandler.CurrentControl = null;
                    AdaptableTooltipHandler.Tooltip.Hide();
                }
            }
        }

        private void ItemGrid_SizeChanged(object sender, EventArgs e) {
            ArrangeItems();
        }

        public void onItemsChanged(object source, EventArgs e) {
            ItemsChanged?.Invoke(source, e);
        }
        
    }
}
