using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.net;
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class ItemBank : UserControl, SQLListener {
        private SQLParser parser;
        private ItemBuilder builder;

        public ItemBank() {
            InitializeComponent();

            itemStoreBar.DropMode = ItemHotBar.MOVE;
            itemStoreBar.GrabMode = ItemHotBar.MOVE;
            componentsHotBar.DropMode = ItemHotBar.MOVE;
            componentsHotBar.GrabMode = ItemHotBar.MOVE;
            parser = new SQLParser();
            builder = new ItemBuilder();
            baseItemSlot.GrabMode = EquipSlot.MOVE;
            baseItemSlot.DropMode = EquipSlot.MOVE;
            resultItemSlot.GrabMode = EquipSlot.CLONE;
            resultItemSlot.DropMode = EquipSlot.NONE;

            refreshItemList();
            refreshEffectList();

            componentsHotBar.ItemsChanged += (s, e) => {
                builder.clearComponents();
                List<ItemData> itemList = componentsHotBar.getItemList();
                foreach (ItemData item in itemList) {
                    builder.addComponent(item);
                }
                resultItemSlot.itemData = builder.resultItem;
                if (resultItemSlot.itemData == null) {
                    detailsTextField.Text = "";
                    baseItemPanel.BackColor = SystemColors.Control;
                    baseItemPanel.BorderStyle = BorderStyle.None;
                    resultItemPanel.BackColor = SystemColors.Control;
                    resultItemPanel.BorderStyle = BorderStyle.None;
                } else {
                    displayItem(resultItemSlot.itemData);
                    baseItemPanel.BackColor = SystemColors.Control;
                    baseItemPanel.BorderStyle = BorderStyle.None;
                    resultItemPanel.BackColor = SystemColors.ControlDark;
                    resultItemPanel.BorderStyle = BorderStyle.FixedSingle;
                }
            };

            baseItemSlot.ItemChanged += (s, e) => {
                builder.setBaseItem(baseItemSlot.itemData);

                resultItemSlot.itemData = builder.resultItem;
            };

            baseItemSlot.SlotClicked += baseItemSlot_MouseClick;
            resultItemSlot.SlotClicked += resultItemSlot_MouseClick;

            if (UserManager.currentUser.userRank >= 5) {
                ContextMenu AdminContextMenu = new ContextMenu();
                MenuItem newItemMenuItem = new MenuItem("New Item");
                newItemMenuItem.Tag = "new";
                newItemMenuItem.Click += newItemMenuItem_Clicked;
                AdminContextMenu.MenuItems.Add(newItemMenuItem);
                itemList.ContextMenu = AdminContextMenu;
            }
        }

        private void baseItemSlot_MouseClick(object sender, EventArgs e) {
            if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                if (((MouseEventArgs)e).Clicks == 1) {
                    if (baseItemSlot.itemData == null) {
                        detailsTextField.Text = "";
                        baseItemPanel.BackColor = SystemColors.Control;
                        baseItemPanel.BorderStyle = BorderStyle.None;
                        resultItemPanel.BackColor = SystemColors.Control;
                        resultItemPanel.BorderStyle = BorderStyle.None;
                    } else {
                        displayItem(baseItemSlot.itemData);
                        baseItemPanel.BackColor = SystemColors.ControlDark;
                        baseItemPanel.BorderStyle = BorderStyle.FixedSingle;
                        resultItemPanel.BackColor = SystemColors.Control;
                        resultItemPanel.BorderStyle = BorderStyle.None;
                    }
                }
            }
        }

        private void resultItemSlot_MouseClick(object sender, EventArgs e) {
            if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                if (((MouseEventArgs)e).Clicks == 1) {
                    if (resultItemSlot.itemData == null) {
                        detailsTextField.Text = "";
                        baseItemPanel.BackColor = SystemColors.Control;
                        baseItemPanel.BorderStyle = BorderStyle.None;
                        resultItemPanel.BackColor = SystemColors.Control;
                        resultItemPanel.BorderStyle = BorderStyle.None;
                    } else {
                        displayItem(resultItemSlot.itemData);
                        baseItemPanel.BackColor = SystemColors.Control;
                        baseItemPanel.BorderStyle = BorderStyle.None;
                        resultItemPanel.BackColor = SystemColors.ControlDark;
                        resultItemPanel.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
        }

        public void refreshItemList() {
            SQLManager.runQuery("vc_item_bank",
                new string[] { "id", "title", "brief_title", "type", "long_type", "mod_prof", "weight", "components", "value", "dmg", "mdmg", "pow", "mpow", "rng", "mrng",
                    "unit_single", "unit_plural", "def", "mdef", "block", "count", "description", "brief_description", "tier", "use_effect", "equip_effect", "mods", "stackable",
                    "bg_src", "bg_color", "icon_src", "icon_color", "whitelist" },
                "", this, SQLManager.LOAD_BANK_ITEM);
        }

        public void refreshEffectList() {
            SQLManager.runQuery("vc_magic_effects",
                new string[] { "id", "type", "title", "description", "quality", "rarity", "properties", "creator" },
                "", this, SQLManager.LOAD_MAGIC_EFFECT);
        }

        private void displayItem(long netID) {
            List<Dictionary<string, object>> queryResult =
                SQLManager.runImmediateQuery("vc_item_bank", ItemData.fieldsList, "id = " + netID + " LIMIT 1;");
            if (queryResult.Count > 0) {
                ItemData item = parser.parseBankItem(queryResult[0]);
                displayItem(item);
            }
        }

        private void resultItemSlot_MouseClick(object sender, MouseEventArgs e) {
            displayItem(resultItemSlot.itemData);
            baseItemPanel.BackColor = SystemColors.Control;
            baseItemPanel.BorderStyle = BorderStyle.None;
            resultItemPanel.BackColor = SystemColors.ControlDark;
            resultItemPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void baseItemSlot_MouseClick(object sender, MouseEventArgs e) {
            displayItem(baseItemSlot.itemData);
            baseItemPanel.BackColor = SystemColors.ControlDark;
            baseItemPanel.BorderStyle = BorderStyle.FixedSingle;
            resultItemPanel.BackColor = SystemColors.Control;
            resultItemPanel.BorderStyle = BorderStyle.None;
        }

        private void displayItem(ItemData item) {
            if (item != null) {
                detailsTextField.Text = item.getDescriptiveString();
            }
        }

        public void HandleData(int action, Dictionary<string, object> d) {
            if (action == SQLManager.LOAD_BANK_ITEM) {
                ItemData item = parser.parseBankItem(d);
                itemList.AddItem(item);
            } else if (action == SQLManager.LOAD_MAGIC_EFFECT) {
                MagicEffect effect = parser.parseMagicEffect(d);
                switch (effect.Type) {
                    case MagicEffect.EffectType.Triggered_Effect:
                    case MagicEffect.EffectType.Triggered_Armor_Effect:
                    case MagicEffect.EffectType.Triggered_Weapon_Effect:
                        onUseComboBox.Items.Add(effect);
                        break;
                    case MagicEffect.EffectType.Continuous_Effect:
                    case MagicEffect.EffectType.Continuous_Armor_Effect:
                    case MagicEffect.EffectType.Continuous_Weapon_Effect:
                        onEquipComboBox.Items.Add(effect);
                        break;
                }
            }
        }

        private void newItemMenuItem_Clicked(object source, EventArgs e) {
            using (ItemDesigner designer = new ItemDesigner()) {
                var result = designer.ShowDialog();
                if (result == DialogResult.OK) {
                    ItemData data = designer.resultItem;
                    long newID = uploadItemDataToBank(data);
                    if (newID >= 0) {
                        data.netID = newID;
                        itemList.AddItem(data);
                    }
                }
            }
        }

        private void itemList_SizeChanged(object sender, EventArgs e) {

        }

        private long uploadItemDataToBank(ItemData data) {
            long result = -1;
            string[][] a = data.buildMySQLArray();
            SQLManager.runInsert("vc_item_bank", a[0], a[1], out result);
            return result;
        }

        private void onUseComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (onUseComboBox.SelectedItem is MagicEffect) {
                builder.onUseEffect = (MagicEffect)onUseComboBox.SelectedItem;
            } else {
                builder.onUseEffect = null;
            }
            resultItemSlot.itemData = builder.resultItem;
            if (resultItemSlot.itemData == null) {
                detailsTextField.Text = "";
                baseItemPanel.BackColor = SystemColors.Control;
                baseItemPanel.BorderStyle = BorderStyle.None;
                resultItemPanel.BackColor = SystemColors.Control;
                resultItemPanel.BorderStyle = BorderStyle.None;
            } else {
                displayItem(resultItemSlot.itemData);
                baseItemPanel.BackColor = SystemColors.Control;
                baseItemPanel.BorderStyle = BorderStyle.None;
                resultItemPanel.BackColor = SystemColors.ControlDark;
                resultItemPanel.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void onEquipComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (onEquipComboBox.SelectedItem is MagicEffect) {
                builder.equipEffect = (MagicEffect)onEquipComboBox.SelectedItem;
            } else {
                builder.equipEffect = null;
            }
            resultItemSlot.itemData = builder.resultItem;
            if (resultItemSlot.itemData == null) {
                detailsTextField.Text = "";
                baseItemPanel.BackColor = SystemColors.Control;
                baseItemPanel.BorderStyle = BorderStyle.None;
                resultItemPanel.BackColor = SystemColors.Control;
                resultItemPanel.BorderStyle = BorderStyle.None;
            } else {
                displayItem(resultItemSlot.itemData);
                baseItemPanel.BackColor = SystemColors.Control;
                baseItemPanel.BorderStyle = BorderStyle.None;
                resultItemPanel.BackColor = SystemColors.ControlDark;
                resultItemPanel.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }

    }
}
