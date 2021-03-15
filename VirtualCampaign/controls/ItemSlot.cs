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

namespace VirtualCampaign.controls {
    public partial class ItemSlot : UserControl {
        public event EventHandler ValueChanged;
        public event EventHandler ItemClicked;
        public event EventHandler ItemEquipChanged;
        public InventoryList parentList = null;
        private ItemData _itemData;
        public ItemData itemData { get { return _itemData; } set { setItemData(value); } }
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value);  } }
        public bool refreshing { get; set; }

        public ItemSlot() : this(new ItemData()) {
        }

        public ItemSlot(ItemData item) {
            InitializeComponent();
            itemData = item;
            _editable = true;
            refreshing = false;
        }

        public void setItemData(ItemData newData) {
            _itemData = newData;
            int permission = (newData.hidden ? 0 : 1);
            /*
            if(newData.hidden) {
                permission = VirtualCampaign.sys.WhitelistHandler.getCurrentUserPermissionLevel(newData.whitelistString);
            } else {
                permission = 1;
            }
            */

            infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle);
            if(newData.equipped) {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_active;
            } else {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_inactive;
            }
            if(newData.starred) {
                starButton.Image = VirtualCampaign.Properties.Resources.item_star_active;
            } else {
                starButton.Image = VirtualCampaign.Properties.Resources.item_star_inactive;
            }

            itemImage.bgSrc = newData.bgSrc;
            itemImage.setBGColor(newData.bgColor);
            itemImage.iconSrc = newData.iconSrc;
            itemImage.setIconColor(newData.iconColor);
            if(newData.stackable && newData.count > 1) {
                itemImage.footStr = newData.count.ToString();
            } else {
                itemImage.footStr = "";
            }

            string countString = "";
            if(newData.stackable) {
                if(string.IsNullOrWhiteSpace(newData.unitSingle) && string.IsNullOrWhiteSpace(newData.unitPlural)) {
                    countString = newData.count.ToString();
                } else {
                    countString = newData.count + " " + (newData.count == 1 ? newData.unitSingle : newData.unitPlural);
                }
            }
            switch(newData.type) {
                case ItemData.GENERIC_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + "   -   " + newData.longType;
                    infoLabel2.Text = (permission >= 1 ? newData.description : newData.briefDescription);
                    infoLabel3.Text = "";
                    break;
                case ItemData.WEAPON_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + "   -   " + newData.longType;
                    infoLabel2.Text = "DMG: " + newData.dmg + "   POW: " + newData.pow + "   Range: " + newData.rng;
                    infoLabel3.Text = "MDMG: " + newData.mdmg + "   MPOW: " + newData.mpow + "   M. Range: " + newData.mrng;
                    break;
                case ItemData.ARMOR_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + "   -   " + newData.longType;
                    infoLabel2.Text = "DEF: " + newData.def + "   MDEF: " + newData.mdef + "   Block: " + newData.block;
                    infoLabel3.Text = (permission >= 1 ? newData.description : newData.briefDescription);
                    break;
                case ItemData.ACCESSORY_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + "   -   " + newData.longType;
                    infoLabel2.Text = (permission >= 1 ? newData.description : newData.briefDescription);
                    infoLabel3.Text = "";
                    break;
                case ItemData.CONSUMABLE_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + (newData.stackable ? " (" + countString + ")" : "") + "   -   " + newData.longType;
                    infoLabel2.Text = (permission >= 1 ? newData.description : newData.briefDescription);
                    infoLabel3.Text = "";
                    break;
                case ItemData.AMMUNITION_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + (newData.stackable ? " (" + countString + ")" : "") + "   -   " + newData.longType;
                    infoLabel2.Text = "DMG: " + newData.dmg + "   POW: " + newData.pow;
                    infoLabel3.Text = "MDMG: " + newData.mdmg + "   MPOW: " + newData.mpow;
                    break;
                case ItemData.COMPONENT_TYPE:
                    infoLabel1.Text = (permission >= 1 ? newData.title : newData.briefTitle) + "   -   " + newData.longType;
                    infoLabel2.Text = newData.mods;
                    infoLabel3.Text = "";
                    break;
            }

            if (!refreshing) OnChanged(EventArgs.Empty);
        }

        public void refreshIcons() {
            if(itemData.equipped) {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_active;
            } else {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_inactive;
            }
            equipButton.Invalidate();
        }

        private void equipButton_Click(object sender, EventArgs e) {
            if(((MouseEventArgs)e).Button == MouseButtons.Left) {
                if(itemData.equipped) {
                    itemData.equipped = false;
                    equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_inactive;
                    equipButton.Invalidate();
                    equipButton.Update();
                    
                } else {
                    itemData.equipped = true;
                    equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_active;
                    equipButton.Invalidate();
                    equipButton.Update();
                }
                OnItemEquipChanged(EventArgs.Empty);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        private void starButton_Click(object sender, EventArgs e) {
            if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                if (itemData.starred) {
                    itemData.starred = false;
                    starButton.Image = VirtualCampaign.Properties.Resources.item_star_inactive;
                    starButton.Invalidate();
                    starButton.Update();
                } else {
                    itemData.starred = true;
                    starButton.Image = VirtualCampaign.Properties.Resources.item_star_active;
                    starButton.Invalidate();
                    starButton.Update();
                }
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        private void closeButton_Click(object sender, EventArgs e) {
            if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                if(parentList != null) {
                    parentList.removeItemSlot(this);
                }
            }
        }

        public void setEditable(bool value) {
            _editable = value;
            closeButton.Enabled = value;
            if(value) {
                closeButton.Image = VirtualCampaign.Properties.Resources.item_delete_active;
            } else {
                closeButton.Image = VirtualCampaign.Properties.Resources.item_delete_inactive;
            }
            
        }

        private void closeButton_MouseEnter(object sender, EventArgs e) {
            if(closeButton.Enabled) {
                closeButton.Image = VirtualCampaign.Properties.Resources.item_delete_active_highlight;
            }
        }

        private void closeButton_MouseLeave(object sender, EventArgs e) {
            if (closeButton.Enabled) {
                closeButton.Image = VirtualCampaign.Properties.Resources.item_delete_active;
            }
        }

        private void equipButton_MouseEnter(object sender, EventArgs e) {
            if(itemData.equipped) {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_active_highlight;
            } else {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_inactive_highlight;
            }
        }

        private void equipButton_MouseLeave(object sender, EventArgs e) {
            if (itemData.equipped) {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_active;
            } else {
                equipButton.Image = VirtualCampaign.Properties.Resources.item_equip_inactive;
            }
        }

        private void starButton_MouseEnter(object sender, EventArgs e) {
            if (itemData.starred) {
                starButton.Image = VirtualCampaign.Properties.Resources.item_star_active_highlight;
            } else {
                starButton.Image = VirtualCampaign.Properties.Resources.item_star_inactive_highlight;
            }
        }

        private void starButton_MouseLeave(object sender, EventArgs e) {
            if (itemData.starred) {
                starButton.Image = VirtualCampaign.Properties.Resources.item_star_active;
            } else {
                starButton.Image = VirtualCampaign.Properties.Resources.item_star_inactive;
            }
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        private void ItemSlot_Click(object sender, EventArgs e) {
            OnItemClicked(this, e);
        }

        protected virtual void OnItemClicked(object source, EventArgs e) {
            ItemClicked?.Invoke(source, e);
        }

        protected virtual void OnItemEquipChanged(EventArgs e) {
            ItemEquipChanged?.Invoke(this, e);
        }

        private void expandBar_Click(object sender, EventArgs e) {
            this.DoDragDrop(_itemData, DragDropEffects.Copy);
        }
    }
}
