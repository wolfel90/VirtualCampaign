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
using VirtualCampaign.events;
using VirtualCampaign.sys;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class EquipSlot : GameItemCollectionControl {
        private static ItemDesigner designer;
        public event ItemEventHandler ItemChanged;
        public event MouseEventHandler SlotClicked;
        private ItemData _itemData;
        public ItemData itemData { get { return _itemData;  } set { setItemData(value); } }
        private List<string> allowedLongTypes;
        private List<int> allowedTypes;
        private string _emptyIconSrc;
        public string emptyIconSrc { get { return _emptyIconSrc; } set { setEmptyIconSrc(value); } }
        public bool editable { get; set; }
        public bool adaptableTooltip;

        static EquipSlot() {
            designer = null;
        }

        public EquipSlot() {
            InitializeComponent();
            itemData = null;
            GrabMode = MOVE;
            DropMode = MOVE;
            allowedLongTypes = new List<string>();
            allowedLongTypes.Add("any");
            allowedTypes = new List<int>();
            allowedTypes.Add(-1);
            editable = true;
            adaptableTooltip = true;
        }

        private void setItemData(ItemData newData) {
            ItemData oldData = _itemData;
            _itemData = newData;
            if(newData == null) {
                itemImage.bgSrc = "";
                itemImage.setBGColor(Color.Black);
                itemImage.iconSrc = _emptyIconSrc;
                itemImage.setIconColor(Color.Black);
                itemImage.footStr = "";
                itemImage.Invalidate();
                OnItemChanged(this, new ItemEventArgs(oldData, _itemData));
            } else {
                itemImage.bgSrc = newData.bgSrc;
                itemImage.setBGColor(newData.bgColor);
                itemImage.iconSrc = newData.iconSrc;
                itemImage.setIconColor(newData.iconColor);
                if(newData.stackable && newData.count > 1) {
                    itemImage.footStr = newData.count.ToString();
                } else {
                    itemImage.footStr = "";
                }
                itemImage.Invalidate();
                OnItemChanged(this, new ItemEventArgs(oldData, _itemData));
            }
        }

        public void setEmptyIconSrc(string src) {
            _emptyIconSrc = src;
            itemImage.bgSrc = _emptyIconSrc;
            itemImage.bgColor = Color.Black;
        }

        private void itemImage_MouseDown(object sender, MouseEventArgs e) {
            this.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, e.X + itemImage.Location.X, e.Y + itemImage.Location.Y, e.Delta));
        }

        private void itemImage_MouseClick(object sender, EventArgs e) {
            if(e is MouseEventArgs) {
                MouseEventArgs newE = new MouseEventArgs(((MouseEventArgs)e).Button, ((MouseEventArgs)e).Clicks, ((MouseEventArgs)e).X + itemImage.Location.X,
                    ((MouseEventArgs)e).Y + itemImage.Location.Y, ((MouseEventArgs)e).Delta);
                EquipSlot_MouseClick(this, newE);
            }
        }

        private void itemImage_DoubleClick(object sender, EventArgs e) {
            if (e is MouseEventArgs) {
                MouseEventArgs newE = new MouseEventArgs(((MouseEventArgs)e).Button, 2, ((MouseEventArgs)e).X + itemImage.Location.X,
                    ((MouseEventArgs)e).Y + itemImage.Location.Y, ((MouseEventArgs)e).Delta);
                EquipSlot_MouseClick(this, newE);
            }
        }

        private void OnItemChanged(object source, ItemEventArgs e) {
            ItemChanged?.Invoke(source, e);
        }

        private void OnSlotClicked(object source, MouseEventArgs e) {
            SlotClicked?.Invoke(source, e);
        }

        public bool isAllowed(ItemData item) {
            return isAllowed(item.type, item.longType);
        }

        public bool isAllowed(int type, string longType) {
            bool check = false;
            if(allowedTypes.Contains(-1)) {
                check = true;
            } else if(allowedTypes.Contains(type)) {
                check = true;
            }
            if(check) {
                if (allowedLongTypes.Contains("any")) {
                    return true;
                } else if (allowedLongTypes.Contains(longType.ToLower())) {
                    return true;
                }
            }
            return false;
        }

        public void setAllowedTypes(params int[] types) {
            allowedTypes.Clear();
            addAllowedTypes(types);
        }

        public void addAllowedTypes(params int[] types) {
            if (allowedTypes.Contains(-1)) allowedTypes.Remove(-1);
            foreach(int i in types) {
                allowedTypes.Add(i);
            }
        }

        public void removeAllowedType(int type) {
            allowedTypes.Remove(type);
        }

        public void clearAllowedTypes() {
            allowedTypes.Clear();
        }

        public void setAllowedLongTypes(params string[] types) {
            allowedLongTypes.Clear();
            addAllowedLongTypes(types);
        }

        public void addAllowedLongTypes(params string[] types) {
            if (allowedLongTypes.Contains("any")) allowedLongTypes.Remove("any");
            foreach(string s in types) {
                allowedLongTypes.Add(s.ToLower());
            }
        }

        public void removeAllowedLongType(string type) {
            allowedLongTypes.Remove(type);
        }

        public void clearAllowedLongTypes() {
            allowedLongTypes.Clear();
        }

        private void EquipSlot_MouseClick(object sender, EventArgs e) {
            if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                if (DragControlHandler.isCarrying(typeof(ItemData))) {
                    if(editable) {
                        ItemData oldData = itemData;
                        switch (DropMode) {
                            case NONE:
                                break;
                            case MOVE:
                                if (isAllowed((ItemData)DragControlHandler.getCarriedData())) {
                                    itemData = (ItemData)DragControlHandler.getCarriedData();
                                    itemData.equipped = true;
                                    if (oldData == null) {
                                        DragControlHandler.clearCarriedData();
                                    } else {
                                        DragControlHandler.setCarriedData(oldData);
                                    }
                                    OnItemChanged(this, new ItemEventArgs(oldData, itemData));
                                }
                                break;
                            case CLONE:
                                if (isAllowed((ItemData)DragControlHandler.getCarriedData())) {
                                    itemData = (ItemData)DragControlHandler.getCarriedData();
                                    itemData.equipped = true;
                                    OnItemChanged(this, new ItemEventArgs(oldData, itemData));
                                }
                                break;
                        }
                    }
                } else {
                    if (itemData != null) {
                        ItemData oldData = itemData;
                        switch (GrabMode) {
                            case NONE:
                                break;
                            case MOVE:
                                DragControlHandler.setCarriedData(itemData);
                                ((ItemData)DragControlHandler.getCarriedData()).equipped = false;
                                if (editable) {
                                    if ((ModifierKeys & Keys.Control) != Keys.Control) {
                                        itemData = null;
                                        OnItemChanged(this, new ItemEventArgs(oldData, itemData));
                                    }
                                }
                                break;
                            case CLONE:
                                DragControlHandler.setCarriedData(itemData);
                                ((ItemData)DragControlHandler.getCarriedData()).equipped = false;
                                break;
                        }
                    }
                }
            } else if(((MouseEventArgs)e).Button == MouseButtons.Right) {
                if(editable) {
                    if(designer != null) {
                        designer.Close();
                        designer.Dispose();
                        designer = null;
                    }
                    if (itemData == null) {
                        designer = new ItemDesigner();
                    } else {
                        designer = new ItemDesigner(itemData);
                    }
                    DialogResult result = designer.ShowDialog();
                    if(result == DialogResult.OK) {
                        ItemData oldData = itemData;
                        itemData = designer.resultItem;
                        this.OnItemChanged(this, new ItemEventArgs(oldData, itemData));
                    }
                    designer.Dispose();
                    designer = null;
                }
            }
            OnSlotClicked(this, (MouseEventArgs)e);
        }

        private void itemImage_MouseEnter(object sender, EventArgs e) {
            if (adaptableTooltip) {
                if (itemData != null) {
                    AdaptableTooltipHandler.CurrentControl = this;
                    AdaptableTooltipHandler.Tooltip.generateTooltip(itemData);
                    AdaptableTooltipHandler.Tooltip.Show();
                    AdaptableTooltipHandler.Tooltip.Location = this.PointToScreen(new Point(0 + this.Width + 5, 0));
                }
            }
        }

        private void itemImage_MouseLeave(object sender, EventArgs e) {
            if (AdaptableTooltipHandler.CurrentControl == this) {
                AdaptableTooltipHandler.CurrentControl = null;
                AdaptableTooltipHandler.Tooltip.Hide();
            }
        }
    }
}
