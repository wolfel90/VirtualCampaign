using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;
using VirtualCampaign.net;
using System.IO;
using VirtualCampaign.window;

namespace VirtualCampaign.window {
    public partial class ItemDesigner : Form, SQLListener {
        private static List<MagicEffect> magicEffectsPool = new List<MagicEffect>();
        private static SQLParser Parser = new SQLParser();
        private static bool effectsLoaded = false;
        public ItemData resultItem { get; }
        public MagicEffect onUseEffect, onEquipEffect;
        public int permissionLevel;

        public ItemDesigner() : this(null) {}

        public ItemDesigner(ItemData inData) {
            InitializeComponent();
            resultItem = inData == null ? new ItemData() : inData;

            if(inData != null) {
                basicTitleField.Text = resultItem.briefTitle;
                hiddenTitleField.Text = resultItem.title;
                hiddenCheckBox.Checked = resultItem.hidden;
                weightSpinner.Value = resultItem.weight;
                valueSpinner.Value = resultItem.value;
                materialsField.Text = resultItem.materials;
                stackCheck.Checked = resultItem.stackable;
                genericRadioButton.Checked = resultItem.type == ItemData.GENERIC_TYPE;
                weaponRadioButton.Checked = resultItem.type == ItemData.WEAPON_TYPE;
                armorRadioButton.Checked = resultItem.type == ItemData.ARMOR_TYPE;
                accessoryRadioButton.Checked = resultItem.type == ItemData.ACCESSORY_TYPE;
                consumableRadioButton.Checked = resultItem.type == ItemData.CONSUMABLE_TYPE;
                ammoRadioButton.Checked = resultItem.type == ItemData.AMMUNITION_TYPE;
                componentRadioButton.Checked = resultItem.type == ItemData.COMPONENT_TYPE;
                typeCombo.Text = resultItem.longType;
                dmgField.Text = resultItem.dmg;
                mdmgField.Text = resultItem.mdmg;
                powSpinner.Value = resultItem.pow;
                mpowSpinner.Value = resultItem.mpow;
                rangeSpinner.Value = resultItem.rng;
                mrangeSpinner.Value = resultItem.mrng;
                defSpinner.Value = resultItem.def;
                mdefSpinner.Value = resultItem.mdef;
                blockSpinner.Value = resultItem.block;
                countSpinner.Value = resultItem.count;
                unitSingularField.Text = resultItem.unitSingle;
                unitPluralField.Text = resultItem.unitPlural;
                onUseEffect = resultItem.onUseEffect == null ? null : resultItem.onUseEffect;
                onUseCombo.Text = resultItem.onUseEffect == null ? "" : resultItem.onUseEffect.title;
                onUseValueLabel.Text = resultItem.onUseEffect == null ? "<None>" : resultItem.onUseEffect.title;
                onEquipEffect = resultItem.equipEffect == null ? null : resultItem.equipEffect;
                onEquipCombo.Text = resultItem.equipEffect == null ? "" : resultItem.equipEffect.title;
                onEquipValueLabel.Text = resultItem.equipEffect == null ? "<None>" : resultItem.equipEffect.title;
                basicDescriptionField.Text = resultItem.briefDescription;
                hiddenDescriptionField.Text = resultItem.description;
                modsField.Text = resultItem.mods;
                itemImage.bgSrc = resultItem.bgSrc;
                itemImage.setBGColor(resultItem.bgColor);
                itemImage.iconSrc = resultItem.iconSrc;
                itemImage.setIconColor(resultItem.iconColor);
                itemImage.Invalidate();
            } else {
                resultItem.whitelistString = UserManager.currentUser.userID.ToString();
            }

            int permission;
            permission = VirtualCampaign.sys.WhitelistHandler.getCurrentUserPermissionLevel(resultItem.whitelistString);
            if(permission >= 1) { // Hidden status and whitelist permission should be determined by whitelist only
                hiddenCheckBox.Visible = true;
                whitelistButton.Visible = true;
            }

            if (!resultItem.hidden) permission = 1;
            
            setPermissiveFieldVisibility(permission >= 1);

            if(!effectsLoaded) {
                SQLManager.runQuery("vc_magic_effects",
                    new string[] { "id", "type", "title", "description", "quality", "rarity", "properties", "creator" },
                    null,
                    this,
                    SQLManager.LOAD_MAGIC_EFFECT
                );
            }
        }

        public void HandleData(int action, Dictionary<string, object> d) {
            if (action == SQLManager.LOAD_MAGIC_EFFECT) {
                if (d != null) {
                    MagicEffect m = Parser.parseMagicEffect(d);
                    for(int i = 0; i < magicEffectsPool.Count; ++i) { 
                        if(magicEffectsPool[i].netID == m.netID) {
                            magicEffectsPool.RemoveAt(i);
                            break;
                        }
                    }
                    magicEffectsPool.Add(m);
                }
            } else if(action == SQLManager.DONE) {
                refreshMagicEffectOptions();
                effectsLoaded = true;
            }
        }

        private void setPermissiveFieldVisibility(bool val) {
            hiddenTitleLabel.Visible = val;
            hiddenTitleField.Visible = val;
            //hiddenCheckBox.Visible = val;
            //whitelistButton.Visible = val;
            valueLabel.Visible = val;
            valueSpinner.Visible = val;
            onUseLabel.Visible = val;
            onUseCombo.Visible = val;
            onEquipLabel.Visible = val;
            onEquipCombo.Visible = val;
            hiddenDescriptionLabel.Visible = val;
            hiddenDescriptionField.Visible = val;
        }

        private void resolveItem() {
            if(string.IsNullOrWhiteSpace(basicTitleField.Text)) {
                resultItem.briefTitle = hiddenTitleField.Text;
                resultItem.title = hiddenTitleField.Text;
            } else if(string.IsNullOrWhiteSpace(hiddenTitleField.Text)) {
                resultItem.briefTitle = basicTitleField.Text;
                resultItem.title = basicTitleField.Text;
            } else {
                resultItem.briefTitle = basicTitleField.Text;
                resultItem.title = hiddenTitleField.Text;
            }
            resultItem.hidden = hiddenCheckBox.Checked;
            resultItem.weight = (int)weightSpinner.Value;
            resultItem.value = (int)valueSpinner.Value;
            resultItem.materials = materialsField.Text;
            resultItem.stackable = stackCheck.Checked;
            if (genericRadioButton.Checked) {
                resultItem.type = ItemData.GENERIC_TYPE;
            } else if (weaponRadioButton.Checked) {
                resultItem.type = ItemData.WEAPON_TYPE;
            } else if (armorRadioButton.Checked) {
                resultItem.type = ItemData.ARMOR_TYPE;
            } else if (accessoryRadioButton.Checked) {
                resultItem.type = ItemData.ACCESSORY_TYPE;
            } else if (consumableRadioButton.Checked) {
                resultItem.type = ItemData.CONSUMABLE_TYPE;
            } else if (ammoRadioButton.Checked) {
                resultItem.type = ItemData.AMMUNITION_TYPE;
            } else if(componentRadioButton.Checked) {
                resultItem.type = ItemData.COMPONENT_TYPE;
            } else {
                resultItem.type = ItemData.GENERIC_TYPE;
            }
            if(typeCombo.SelectedItem != null) {
                resultItem.longType = typeCombo.SelectedItem.ToString();
            }
            resultItem.dmg = dmgField.Text;
            resultItem.mdmg = mdmgField.Text;
            resultItem.pow = (int)powSpinner.Value;
            resultItem.mpow = (int)mpowSpinner.Value;
            resultItem.rng = (int)rangeSpinner.Value;
            resultItem.mrng = (int)mrangeSpinner.Value;
            resultItem.def = (int)defSpinner.Value;
            resultItem.mdef = (int)mdefSpinner.Value;
            resultItem.block = (int)blockSpinner.Value;
            resultItem.count = (int)countSpinner.Value;
            resultItem.unitSingle = unitSingularField.Text;
            resultItem.unitPlural = unitPluralField.Text;
            resultItem.onUseEffect = onUseEffect;
            resultItem.equipEffect = onEquipEffect;
            if(string.IsNullOrWhiteSpace(basicDescriptionField.Text)) {
                resultItem.briefDescription = hiddenDescriptionField.Text;
                resultItem.description = hiddenDescriptionField.Text;
            } else if(string.IsNullOrWhiteSpace(hiddenDescriptionField.Text)) {
                resultItem.briefDescription = basicDescriptionField.Text;
                resultItem.description = basicDescriptionField.Text;
            } else {
                resultItem.briefDescription = basicDescriptionField.Text;
                resultItem.description = hiddenDescriptionField.Text;
            }
            resultItem.mods = modsField.Text;
            resultItem.bgSrc = itemImage.bgSrc;
            resultItem.bgColor = itemImage.bgColor.ToArgb();
            resultItem.iconSrc = itemImage.iconSrc;
            resultItem.iconColor = itemImage.iconColor.ToArgb();
        }

        private void okButton_Click(object sender, EventArgs e) {
            resolveItem();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void whitelistButton_Click(object sender, EventArgs e) {
            using (var whitelistMenu = new WhitelistMenu(WhitelistMenu.BINARY_MODE, false)) {
                whitelistMenu.setData(resultItem.whitelistString);
                var result = whitelistMenu.ShowDialog();
                if (result == DialogResult.OK) {
                    resultItem.whitelistString = whitelistMenu.resultString;

                    int permission = VirtualCampaign.sys.WhitelistHandler.getCurrentUserPermissionLevel(resultItem.whitelistString);

                    setPermissiveFieldVisibility(permission >= 1);
                }
            }
        }

        private void refreshMagicEffectOptions() {
            onUseCombo.BeginUpdate();
            onEquipCombo.BeginUpdate();
            onUseCombo.Items.Clear();
            onEquipCombo.Items.Clear();
            onUseCombo.Items.Add("<None>");
            onEquipCombo.Items.Add("<None>");
            foreach (MagicEffect m in magicEffectsPool) {
                switch (m.Type) {
                    case MagicEffect.EffectType.Continuous_Effect:
                        onEquipCombo.Items.Add(m);
                        break;
                    case MagicEffect.EffectType.Continuous_Armor_Effect:
                        if(armorRadioButton.Checked || accessoryRadioButton.Checked) {
                            onEquipCombo.Items.Add(m);
                        }
                        break;
                    case MagicEffect.EffectType.Continuous_Weapon_Effect:
                        if(weaponRadioButton.Checked || ammoRadioButton.Checked || accessoryRadioButton.Checked) {
                            onEquipCombo.Items.Add(m);
                        }
                        break;
                    case MagicEffect.EffectType.Triggered_Effect:
                        onUseCombo.Items.Add(m);
                        break;
                    case MagicEffect.EffectType.Triggered_Armor_Effect:
                        if(armorRadioButton.Checked || accessoryRadioButton.Checked) {
                            onUseCombo.Items.Add(m);
                        }
                        break;
                    case MagicEffect.EffectType.Triggered_Weapon_Effect:
                        if(weaponRadioButton.Checked || ammoRadioButton.Checked || accessoryRadioButton.Checked) {
                            onUseCombo.Items.Add(m);
                        }
                        break;
                }
            }
            onEquipCombo.EndUpdate();
            onUseCombo.EndUpdate();
        }

        private void genericRadioButton_CheckedChanged(object sender, EventArgs e) {
            if(genericRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using (StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.generic_types)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = false;
                armorPanel.Visible = false;
                modsPanel.Visible = false;
                refreshMagicEffectOptions();
            }
        }

        private void weaponRadioButton_CheckedChanged(object sender, EventArgs e) {
            if(weaponRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using(StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.weapon_types)) {
                    string line;
                    while((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = true;
                armorPanel.Visible = false;
                modsPanel.Visible = true;
                refreshMagicEffectOptions();
            }
        }

        private void armorRadioButton_CheckedChanged(object sender, EventArgs e) {
            if(armorRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using (StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.armor_types)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = false;
                armorPanel.Visible = true;
                modsPanel.Visible = true;
                refreshMagicEffectOptions();
            }
        }

        private void accessoryRadioButton_CheckedChanged(object sender, EventArgs e) {
            if(accessoryRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using (StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.accessory_types)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = false;
                armorPanel.Visible = false;
                modsPanel.Visible = true;
                refreshMagicEffectOptions();
            }
        }

        private void consumableRadioButton_CheckedChanged(object sender, EventArgs e) {
            if(consumableRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using (StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.consumable_types)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = false;
                armorPanel.Visible = false;
                modsPanel.Visible = false;
                refreshMagicEffectOptions();
            }
        }

        private void ammoRadioButton_CheckedChanged(object sender, EventArgs e) {
            if(ammoRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using (StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.ammo_types)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = true;
                armorPanel.Visible = false;
                modsPanel.Visible = true;
                refreshMagicEffectOptions();
            }
        }

        private void componentRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (componentRadioButton.Checked) {
                typeCombo.BeginUpdate();
                typeCombo.Items.Clear();
                using (StringReader reader = new StringReader(VirtualCampaign.Properties.Resources.component_types)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        typeCombo.Items.Add(line);
                    }
                }
                if (typeCombo.Items.Contains(resultItem.type)) {
                    typeCombo.SelectedItem = resultItem.type;
                } else {
                    typeCombo.SelectedIndex = 0;
                }
                typeCombo.EndUpdate();
                weaponPanel.Visible = true;
                armorPanel.Visible = true;
                modsPanel.Visible = true;
                refreshMagicEffectOptions();
            }
        }

        private void stackCheck_CheckedChanged(object sender, EventArgs e) {
            unitPanel.Visible = stackCheck.Checked;
        }

        private void hiddenCheckBox_CheckedChanged(object sender, EventArgs e) {
        }

        private void itemImage_Click(object sender, EventArgs e) {
            using(ItemImageEditor editor = new ItemImageEditor(itemImage)) {
                DialogResult result = editor.ShowDialog();
                if(result == DialogResult.OK) {
                    itemImage.bgSrc = editor.resultImage.bgSrc;
                    itemImage.bgColor = editor.resultImage.bgColor;
                    itemImage.iconSrc = editor.resultImage.iconSrc;
                    itemImage.iconColor = editor.resultImage.iconColor;
                }
            }
        }

        private void onUseButton_Click(object sender, EventArgs e) {
            MagicEffectEditDialog meed = onUseEffect == null ? new MagicEffectEditDialog() : new MagicEffectEditDialog(onUseEffect);
            DialogResult result = meed.ShowDialog();
            if(result == DialogResult.OK) {
                onUseEffect = meed.resultEffect;
                onUseValueLabel.Text = meed.resultEffect.title;
            }
        }

        private void onEquipButton_Click(object sender, EventArgs e) {
            MagicEffectEditDialog meed = onEquipEffect == null ? new MagicEffectEditDialog() : new MagicEffectEditDialog(onEquipEffect);
            DialogResult result = meed.ShowDialog();
            if (result == DialogResult.OK) {
                onEquipEffect = meed.resultEffect;
                onEquipValueLabel.Text = meed.resultEffect.title;
            }
        }

        private void removeOnUseButton_Click(object sender, EventArgs e) {
            onUseEffect = null;
            onUseValueLabel.Text = "<None>";
        }
        
        private void removeOnEquipButton_Click(object sender, EventArgs e) {
            onEquipEffect = null;
            onEquipValueLabel.Text = "<None>";
        }

        private void onUseCombo_SelectedIndexChanged(object sender, EventArgs e) {
            if(onUseCombo.SelectedItem != null) {
                if(onUseCombo.SelectedItem is MagicEffect) {
                    onUseEffect = (MagicEffect)onUseCombo.SelectedItem;
                    onUseValueLabel.Text = onUseCombo.SelectedItem.ToString();
                } else {
                    onUseEffect = null;
                    onUseValueLabel.Text = "<None>";
                }
            }
        }

        private void onEquipCombo_SelectedIndexChanged(object sender, EventArgs e) {
            if(onEquipCombo.SelectedItem != null) {
                if(onEquipCombo.SelectedItem is MagicEffect) {
                    onEquipEffect = (MagicEffect)onEquipCombo.SelectedItem;
                    onEquipValueLabel.Text = onEquipCombo.SelectedItem.ToString();
                } else {
                    onEquipEffect = null;
                    onEquipValueLabel.Text = "<None>";
                }
            }
        }
    }
}
