using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.events;
using VirtualCampaign.net;
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class CharacterSheet : UserControl, IDisposable {
        public CharacterData _charaDat;
        public CharacterData charaDat { get { return _charaDat; } set { setCharacterData(value); } }
        private Dictionary<TextBox, string> defaults;
        private CompoundedStat csDefense, csMagicDefense, csAccuracy, csPowerL, csPowerR, csMagicPowerL, csMagicPowerR, csDamageL, csDamageR, csMagicDamageL, csMagicDamageR, csSpeed, csDodge;
        private CompoundedStat csMP, csEP;
        private Dictionary<string, AttributePanel> attributePanels;
        private System.Timers.Timer saveTimer;
        private bool magicAttackMode = false;
        private bool saveQueued = false;
        private bool saving = false;
        private bool refreshing = false;
        private bool _editable = true;
        public bool suspendSave = false;
        public bool editable { get { return _editable; } set { setEditable(value); } }

        public CharacterSheet() {
            _charaDat = new CharacterData();
            InitializeComponent();
            defaults = new Dictionary<TextBox, string>();
            defaults.Add(nameField, nameField.Text);
            defaults.Add(genderField, genderField.Text);
            defaults.Add(raceField, raceField.Text);
            defaults.Add(ageField, ageField.Text);
            defaults.Add(birthField, birthField.Text);
            defaults.Add(heightField, heightField.Text);
            defaults.Add(weightField, weightField.Text);
            defaults.Add(hairField, hairField.Text);
            defaults.Add(eyesField, eyesField.Text);
            defaults.Add(homeField, homeField.Text);

            attributePanels = new Dictionary<string, AttributePanel>();

            saveTimer = new System.Timers.Timer(30000);
            saveTimer.AutoReset = true;
            saveTimer.Elapsed += OnSaveTimerLapse;
            saveTimer.Start();

            strProfPanel.ValueChanged += proficiencyPanel_ValueChanged;
            agiProfPanel.ValueChanged += proficiencyPanel_ValueChanged;
            intProfPanel.ValueChanged += proficiencyPanel_ValueChanged;
            forProfPanel.ValueChanged += proficiencyPanel_ValueChanged;
            chaProfPanel.ValueChanged += proficiencyPanel_ValueChanged;

            epMeter.curValEditable = false;
            epMeter.maxValEditable = false;
            mpMeter.curValEditable = false;
            mpMeter.maxValEditable = false;

            csDefense = new CompoundedStat("DEF");
            csMagicDefense = new CompoundedStat("MDEF");
            csAccuracy = new CompoundedStat("ACC");
            csPowerL = new CompoundedStat("POW");
            csPowerR = new CompoundedStat("POW");
            csMagicPowerL = new CompoundedStat("MPOW");
            csMagicPowerR = new CompoundedStat("MPOW");
            csDamageL = new CompoundedStat("DMG");
            csDamageR = new CompoundedStat("DMG");
            csMagicDamageL = new CompoundedStat("MDMG");
            csMagicDamageR = new CompoundedStat("MDMG");
            csMP = new CompoundedStat("MP");
            csEP = new CompoundedStat("EP");
            csDodge = new CompoundedStat("DDG");
            csSpeed = new CompoundedStat("SPD");

            bluntResistPanel.Editable = false;
            coldResistPanel.Editable = false;
            darkResistPanel.Editable = false;
            forceResistPanel.Editable = false;
            heatResistPanel.Editable = false;
            lightResistPanel.Editable = false;
            pierceResistPanel.Editable = false;
            psionicResistPanel.Editable = false;
            slashResistPanel.Editable = false;
            shockResistPanel.Editable = false;

            inventoryList.GrabMode = InventoryList.MOVE;
            inventoryList.DropMode = InventoryList.MOVE;

            talentList.ParentSheet = this;

            ammoSlot.setEmptyIconSrc("arrow_blank");
            headSlot.setEmptyIconSrc("helmet_blank");
            torsoSlot.setEmptyIconSrc("shirt_blank");
            legsSlot.setEmptyIconSrc("pants_blank");
            feetSlot.setEmptyIconSrc("boots_blank");
            handsSlot.setEmptyIconSrc("gloves_blank");
            rhSlot.setEmptyIconSrc("rh_blank");
            lhSlot.setEmptyIconSrc("lh_blank");
            waistSlot.setEmptyIconSrc("belt_blank");
            neckSlot.setEmptyIconSrc("amulet_blank");
            ring1Slot.setEmptyIconSrc("ring_blank");
            ring2Slot.setEmptyIconSrc("ring_blank");
            ring3Slot.setEmptyIconSrc("ring_blank");
            ring4Slot.setEmptyIconSrc("ring_blank");
            ring5Slot.setEmptyIconSrc("ring_blank");
            ring6Slot.setEmptyIconSrc("ring_blank");

            ammoSlot.setAllowedTypes(ItemData.AMMUNITION_TYPE);
            ammoSlot.setAllowedLongTypes("any");
            headSlot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            headSlot.setAllowedLongTypes("helmet");
            torsoSlot.setAllowedTypes(ItemData.ARMOR_TYPE);
            torsoSlot.setAllowedLongTypes("any");
            legsSlot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            legsSlot.setAllowedLongTypes("leggings");
            feetSlot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            feetSlot.setAllowedLongTypes("shoes");
            handsSlot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            handsSlot.setAllowedLongTypes("gloves", "armband");
            rhSlot.setAllowedTypes(ItemData.WEAPON_TYPE);
            rhSlot.setAllowedLongTypes("any");
            lhSlot.setAllowedTypes(ItemData.WEAPON_TYPE);
            lhSlot.setAllowedLongTypes("any");
            waistSlot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            waistSlot.setAllowedLongTypes("belt");
            neckSlot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            neckSlot.setAllowedLongTypes("necklace", "cloak", "cape");
            ring1Slot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            ring1Slot.setAllowedLongTypes("ring");
            ring2Slot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            ring2Slot.setAllowedLongTypes("ring");
            ring3Slot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            ring3Slot.setAllowedLongTypes("ring");
            ring4Slot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            ring4Slot.setAllowedLongTypes("ring");
            ring5Slot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            ring5Slot.setAllowedLongTypes("ring");
            ring6Slot.setAllowedTypes(ItemData.ACCESSORY_TYPE);
            ring6Slot.setAllowedLongTypes("ring");
            
            specializedAbilityGrid.maxAbilityCount = 10;
            auxiliaryAbilityGrid.maxAbilityCount = 50;
            
            confirmBasicsButton.Click += savableChangePerformed;
            appearanceText.Leave += savableChangePerformed;
            bioText.Leave += savableChangePerformed;
            languagesField.Leave += savableChangePerformed;
            ayaseyeModuleCheckbox.CheckedChanged += savableChangePerformed;
            eigolynModuleCheckbox.CheckedChanged += savableChangePerformed;
            notesField.Leave += savableChangePerformed;
            notesField1.Leave += savableChangePerformed;
            notesField2.Leave += savableChangePerformed;
            notesField3.Leave += savableChangePerformed;
            strProfPanel.ValueChanged += savableChangePerformed;
            agiProfPanel.ValueChanged += savableChangePerformed;
            intProfPanel.ValueChanged += savableChangePerformed;
            forProfPanel.ValueChanged += savableChangePerformed;
            chaProfPanel.ValueChanged += savableChangePerformed;
            hpMeter.ValueChanged += savableChangePerformed;
            xpCurrentSpinner.ValueChanged += savableChangePerformed;
            xpNextSpinner.ValueChanged += savableChangePerformed;
            conductSpinner.ValueChanged += savableChangePerformed;
            moralitySpinner.ValueChanged += savableChangePerformed;
            fameSpinner.ValueChanged += savableChangePerformed;
            infamySpinner.ValueChanged += savableChangePerformed;
            epAdjustSpinner.ValueChanged += savableChangePerformed;
            mpAdjustSpinner.ValueChanged += savableChangePerformed;
            exhaustSpinner.ValueChanged += savableChangePerformed;
            inventoryList.ValueChanged += savableChangePerformed;
            ammoSlot.ItemChanged += savableChangePerformed;
            headSlot.ItemChanged += savableChangePerformed;
            torsoSlot.ItemChanged += savableChangePerformed;
            legsSlot.ItemChanged += savableChangePerformed;
            feetSlot.ItemChanged += savableChangePerformed;
            handsSlot.ItemChanged += savableChangePerformed;
            lhSlot.ItemChanged += savableChangePerformed;
            rhSlot.ItemChanged += savableChangePerformed;
            neckSlot.ItemChanged += savableChangePerformed;
            waistSlot.ItemChanged += savableChangePerformed;
            ring1Slot.ItemChanged += savableChangePerformed;
            ring2Slot.ItemChanged += savableChangePerformed;
            ring3Slot.ItemChanged += savableChangePerformed;
            ring4Slot.ItemChanged += savableChangePerformed;
            ring5Slot.ItemChanged += savableChangePerformed;
            ring6Slot.ItemChanged += savableChangePerformed;
            ammoSlot.ItemChanged += equipSlot_ItemChanged;
            headSlot.ItemChanged += equipSlot_ItemChanged;
            torsoSlot.ItemChanged += equipSlot_ItemChanged;
            legsSlot.ItemChanged += equipSlot_ItemChanged;
            feetSlot.ItemChanged += equipSlot_ItemChanged;
            handsSlot.ItemChanged += equipSlot_ItemChanged;
            lhSlot.ItemChanged += equipSlot_ItemChanged;
            rhSlot.ItemChanged += equipSlot_ItemChanged;
            neckSlot.ItemChanged += equipSlot_ItemChanged;
            waistSlot.ItemChanged += equipSlot_ItemChanged;
            ring1Slot.ItemChanged += equipSlot_ItemChanged;
            ring2Slot.ItemChanged += equipSlot_ItemChanged;
            ring3Slot.ItemChanged += equipSlot_ItemChanged;
            ring4Slot.ItemChanged += equipSlot_ItemChanged;
            ring5Slot.ItemChanged += equipSlot_ItemChanged;
            ring6Slot.ItemChanged += equipSlot_ItemChanged;
            ammoSlot.SlotClicked += equipSlot_SlotClicked;
            headSlot.SlotClicked += equipSlot_SlotClicked;
            torsoSlot.SlotClicked += equipSlot_SlotClicked;
            legsSlot.SlotClicked += equipSlot_SlotClicked;
            feetSlot.SlotClicked += equipSlot_SlotClicked;
            handsSlot.SlotClicked += equipSlot_SlotClicked;
            lhSlot.SlotClicked += equipSlot_SlotClicked;
            rhSlot.SlotClicked += equipSlot_SlotClicked;
            neckSlot.SlotClicked += equipSlot_SlotClicked;
            waistSlot.SlotClicked += equipSlot_SlotClicked;
            ring1Slot.SlotClicked += equipSlot_SlotClicked;
            ring2Slot.SlotClicked += equipSlot_SlotClicked;
            ring3Slot.SlotClicked += equipSlot_SlotClicked;
            ring4Slot.SlotClicked += equipSlot_SlotClicked;
            ring5Slot.SlotClicked += equipSlot_SlotClicked;
            ring6Slot.SlotClicked += equipSlot_SlotClicked;
            allervianSpinner.ValueChanged += savableChangePerformed;
            florinSpinner.ValueChanged += savableChangePerformed;
            drakeSpinner.ValueChanged += savableChangePerformed;
            denariusSpinner.ValueChanged += savableChangePerformed;
            markSpinner.ValueChanged += savableChangePerformed;
            inventoryList.ItemSlotClicked += itemSlot_Clicked;
            traitList.ValueChanged += savableChangePerformed;
            traitList.ValueChanged += traitList_ValueChanged;
            traitList.TraitAdded += traitList_TraitAdded;
            traitList.TraitRemoved += traitList_TraitRemoved;
            traitList.TraitChanged += traitList_TraitChanged;
            traitList.TraitClicked += traitList_TraitClicked;
            talentList.ValueChanged += savableChangePerformed;
            talentList.ValueChanged += talentList_ValueChanged;
            talentList.TalentClicked += talentList_TalentClicked;
            schoolList.ValueChanged += savableChangePerformed;
            schoolList.ValueChanged += schoolList_ValueChanged;
            schoolList.SchoolClicked += schoolList_SchoolClicked;
            vocationList.ValueChanged += savableChangePerformed;
            vocationList.ValueChanged += vocationList_ValueChanged;
            specializedAbilityGrid.ValueChanged += savableChangePerformed;
            specializedAbilityGrid.ValueChanged += specializedAbilitiyList_ValueChanged;
            specializedAbilityGrid.AbilityClicked += specializedAbilityList_AbilityClicked;
            auxiliaryAbilityGrid.ValueChanged += savableChangePerformed;
            auxiliaryAbilityGrid.ValueChanged += auxAbilityList_ValueChanged;
            auxiliaryAbilityGrid.AbilityClicked += auxAbilityList_AbilityClicked;
            csPowerL.ValueChanged += compoundedStat_ValueChanged;
            csPowerR.ValueChanged += compoundedStat_ValueChanged;
            csMagicPowerL.ValueChanged += compoundedStat_ValueChanged;
            csMagicPowerR.ValueChanged += compoundedStat_ValueChanged;
            csDamageL.ValueChanged += compoundedStat_ValueChanged;
            csDamageR.ValueChanged += compoundedStat_ValueChanged;
            csMagicDamageL.ValueChanged += compoundedStat_ValueChanged;
            csMagicDamageR.ValueChanged += compoundedStat_ValueChanged;
            csDefense.ValueChanged += compoundedStat_ValueChanged;
            csMagicDefense.ValueChanged += compoundedStat_ValueChanged;
            csEP.ValueChanged += compoundedStat_ValueChanged;
            csMP.ValueChanged += compoundedStat_ValueChanged;
            csSpeed.ValueChanged += compoundedStat_ValueChanged;
            csAccuracy.ValueChanged += compoundedStat_ValueChanged;
            csDodge.ValueChanged += compoundedStat_ValueChanged;
            xpCurrentSpinner.MouseWheel += scrollWheel_Bypass;
            xpNextSpinner.MouseWheel += scrollWheel_Bypass;
            moralitySpinner.MouseWheel += scrollWheel_Bypass;
            conductSpinner.MouseWheel += scrollWheel_Bypass;
            fameSpinner.MouseWheel += scrollWheel_Bypass;
            infamySpinner.MouseWheel += scrollWheel_Bypass;
            epAdjustSpinner.MouseWheel += scrollWheel_Bypass;
            mpAdjustSpinner.MouseWheel += scrollWheel_Bypass;
            exhaustSpinner.MouseWheel += scrollWheel_Bypass;

            mainControlPanel.RowStyles[2].Height = 0;
        }

        public CharacterSheet(CharacterData data) : this() {
            void setDat(object sender, EventArgs e) {
                charaDat = data;
                this.Load -= setDat;
            }
            this.Load += setDat;
        }

        private void dmgTypeSwitchButton_Click(object sender, EventArgs e) {
            magicAttackMode = !magicAttackMode;
            if(magicAttackMode) {
                powLabel.Text = "Magic Power";
                dmgLabel.Text = "Magic Damage";
                compoundedStat_ValueChanged(csMagicPowerL, EventArgs.Empty);
                compoundedStat_ValueChanged(csMagicDamageL, EventArgs.Empty);
                compoundedStat_ValueChanged(csMagicPowerR, EventArgs.Empty);
                compoundedStat_ValueChanged(csMagicDamageR, EventArgs.Empty);
            } else {;
                powLabel.Text = "Power";
                dmgLabel.Text = "Damage";
                compoundedStat_ValueChanged(csPowerL, EventArgs.Empty);
                compoundedStat_ValueChanged(csDamageL, EventArgs.Empty);
                compoundedStat_ValueChanged(csPowerR, EventArgs.Empty);
                compoundedStat_ValueChanged(csDamageR, EventArgs.Empty);
            }
        }

        private void savableChangePerformed(object sender, EventArgs e) {
            if(!refreshing) saveQueued = true;
        }

        private void CharacterSheet_Load(object sender, EventArgs e) {
            //refreshFields();
        }

        public void setCharacterData(CharacterData data) {
            string[] keys;
            int h = 0;
            int pX = 0, pY = 0;
            _charaDat = data;
            
            if (_charaDat.classic) {
                staminaPanelLabel.Text = "EP";
                channelingPanelLabel.Text = "MP";
                keys = new string[] { "STRHPB", "EPC", "BDEF", "BDMG", "WGT", "POW", "AGIEPB", "ACC", "DB", "STLTH", "CC", "INTMPB", "MPC", "CONC", "DTM", "SPLR", "MPOW",
                    "FORHPB", "HPREG", "IMM", "SUR", "BAL", "CHAEPB", "CHAMPB", "SPCH", "LEAD", "INTEG"};
            } else {
                staminaPanelLabel.Text = "Energy";
                channelingPanelLabel.Text = "Mana";
                keys = new string[] { "AWA", "WGT", "REG", "SUR", "WIL", "STLTH" };
            }
            foreach (string k in keys) {
                if (!attributePanels.ContainsKey(k)) {
                    AttributePanel newPanel = new AttributePanel(k);
                    if(!_charaDat.classic && (k == "AWA" || k == "WIL" || k == "STLTH")) {
                        newPanel.rollable = true;
                    }
                    newPanel.Tag = k;
                    newPanel.Title = StatKeywords.Translate(k);
                    newPanel.ValueChanged += attributePanel_ValueChanged;
                    attributesPanel.Controls.Add(newPanel);
                    attributePanels.Add(k, newPanel);
                }
            }
            
            foreach (string k in attributePanels.Keys) {
                AttributePanel ap = attributePanels[k];
                ap.Location = new Point(ap.Width * pX + 5, ap.Height * pY + 15);
                pX++;
                if (pX > 1) {
                    pX = 0;
                    pY++;
                }
                h = ap.Bottom + 5;
            }
            attributesPanel.Height = h;
            topPanel.RowStyles[2].Height = (attributesPanel.Bottom > alignmentPanel.Bottom) ? attributesPanel.Bottom + 5 : alignmentPanel.Bottom + 5;
            topPanel.Height = (int)(topPanel.RowStyles[0].Height + topPanel.RowStyles[1].Height + topPanel.RowStyles[2].Height);
            SetWieldMode(_charaDat.wieldMode);
            refreshFields();
        }

        private void traitList_SizeChanged(object sender, EventArgs e) {
            traitPanel.Height = traitList.Location.Y + traitList.Height + 10;
        }

        private void traitPanel_SizeChanged(object sender, EventArgs e) {
            if (traitPanel.Height > talentsPanel.Height) {
                ttPanel.Height = traitPanel.Location.Y + traitPanel.Height + 10;
            } else {
                ttPanel.Height = talentsPanel.Location.Y + talentsPanel.Height + 10;
            }
        }

        private void talentList_SizeChanged(object sender, EventArgs e) {
            talentsPanel.Height = talentList.Location.Y + talentList.Height + 10;
        }

        private void schoolList_SizeChanged(object sender, EventArgs e) {
            schoolPanel.Height = schoolList.Location.Y + schoolList.Height + 30;
        }

        private void vocationList_SizeChanged(object sender, EventArgs e) {
            vocationPanel.Height = vocationList.Location.Y + vocationList.Height + 10;
        }

        private void specializedAbilityGrid_SizeChange(object sender, EventArgs e) {
            specializedAbilityGrid.Location = new Point(specializedAbilitiesPanel.Width / 2 - specializedAbilityGrid.Width / 2, specializedAbilityGrid.Location.Y);
            abilitiesTablePanel.RowStyles[1].Height = specializedAbilitiesPanel.Height + 10;
            specializedAbilitiesPanel.Height = specializedAbilityGrid.Bottom + 10;
        }

        private void auxiliaryAbilityGrid_SizeChange(object sender, EventArgs e) {
            auxiliaryAbilityGrid.Location = new Point(auxAbilitiesPanel.Width / 2 - auxiliaryAbilityGrid.Width / 2, auxiliaryAbilityGrid.Location.Y);
            abilitiesTablePanel.RowStyles[2].Height = auxAbilitiesPanel.Height + 70;
            auxAbilitiesPanel.Height = auxiliaryAbilityGrid.Bottom + 10;
        }
        
        private void talentsPanel_SizeChanged(object sender, EventArgs e) {
            if (traitPanel.Height > talentsPanel.Height) {
                ttPanel.Height = traitPanel.Location.Y + traitPanel.Height + 10;
            } else {
                ttPanel.Height = talentsPanel.Location.Y + talentsPanel.Height + 10;
            }
        }

        private void schoolsPanel_SizeChanged(object sender, EventArgs e) {
            int newHeight = (schoolPanel.Height > vocationPanel.Height ? schoolPanel.Location.Y + schoolPanel.Height : vocationPanel.Location.Y + vocationPanel.Height) + 10;
            schoolsVocationsPanel.Height = newHeight;
            abilitiesTablePanel.RowStyles[0].Height = newHeight;
            abilitiesTablePanel.Height = newHeight + (int)abilitiesTablePanel.RowStyles[1].Height + (int)abilitiesTablePanel.RowStyles[2].Height + 30;
        }

        private void vocationPanel_SizeChanged(object sender, EventArgs e) {
            int newHeight = (schoolPanel.Height > vocationPanel.Height ? schoolPanel.Location.Y + schoolPanel.Height : vocationPanel.Location.Y + vocationPanel.Height) + 10;
            schoolsVocationsPanel.Height = newHeight;
            abilitiesTablePanel.RowStyles[0].Height = newHeight;
            abilitiesTablePanel.Height = newHeight + (int)abilitiesTablePanel.RowStyles[1].Height + (int)abilitiesTablePanel.RowStyles[2].Height + 30;
        }

        private void specializedAbilitiesPanel_SizeChanged(object sender, EventArgs e) {
            abilitiesTablePanel.Height = (int)abilitiesTablePanel.RowStyles[0].Height + specializedAbilitiesPanel.Height + (int)abilitiesTablePanel.RowStyles[2].Height + 30;
        }

        private void auxAbilitiesPanel_SizeChanged(object sender, EventArgs e) {
            abilitiesTablePanel.Height = (int)abilitiesTablePanel.RowStyles[0].Height + (int)abilitiesTablePanel.RowStyles[1].Height + auxAbilitiesPanel.Height + 30;
        }
        
        private void OnSaveTimerLapse(object source, System.Timers.ElapsedEventArgs e) {
            if (!saving && saveQueued && !suspendSave) {
                SaveCharacterData();
            }
        }

        // Publically-accessible save function
        public void SaveCharacterData() {
            if(editable) {
                saving = true;
                UploadCharacterData();
                saveQueued = false;
                saving = false;
            }
        }

        // Save Character Function - Use SaveCharacterData for external access
        private void UploadCharacterData() {
            if(_charaDat.classic) {
                Console.Out.WriteLine("Saving disabled on classic characters");
                return;
            }
            Console.Out.WriteLine("Saving character " + _charaDat.netID);
            updateBasicData();
            updateTraitsTalents();
            updateSchools();
            updateAbilities();
            updateInventory();
            updateWallet();
            string[][] a = _charaDat.buildMySQLArray();
            long nID = _charaDat.netID;
            if (nID < 0) {
                SQLManager.runInsert("vc_character_sheets_2", a[0], a[1], out nID);
                _charaDat.netID = nID;
            } else {
                SQLManager.runUpdate("vc_character_sheets_2", a[0], a[1], "id = " + nID);
            }
        }

        private void hpAddButton_Click(object sender, EventArgs e) {
            int newValue = (int)(hpMeter.current + hpAddSpinner.Value);
            if (newValue > hpMeter.max) newValue = (int)hpMeter.max;
            hpMeter.current = newValue;
        }

        private void hpSubtractButton_Click(object sender, EventArgs e) {
            hpMeter.current = hpMeter.current - (int)hpAddSpinner.Value;
        }

        private void whitelistButton_Click(object sender, EventArgs e) {
            using (var whitelistMenu = new WhitelistMenu()) {
                whitelistMenu.setActive(_charaDat.useWhitelist);
                whitelistMenu.setData(_charaDat.whitelistString);
                var result = whitelistMenu.ShowDialog();
                if (result == DialogResult.OK) {
                    _charaDat.useWhitelist = whitelistMenu.resultUse;
                    _charaDat.whitelistString = whitelistMenu.resultString;
                }
            }
        }

        private void setEditable(bool val) {
            editBasicsButton.Enabled = val;
            editBasicsButton.Visible = val;
            languagesField.Enabled = val;
            appearanceText.Enabled = val;
            bioText.Enabled = val;
            ayaseyeModuleCheckbox.Enabled = val;
            eigolynModuleCheckbox.Enabled = val;
            notesField.Enabled = val;
            notesField1.Enabled = val;
            notesField2.Enabled = val;
            notesField3.Enabled = val;
            strProfPanel.editable = val;
            agiProfPanel.editable = val;
            intProfPanel.editable = val;
            forProfPanel.editable = val;
            chaProfPanel.editable = val;
            epAdjustSpinner.Enabled = val;
            mpAdjustSpinner.Enabled = val;
            exhaustSpinner.Enabled = val;
            conductSpinner.Enabled = val;
            moralitySpinner.Enabled = val;
            fameSpinner.Enabled = val;
            infamySpinner.Enabled = val;
            ammoSlot.editable = val;
            headSlot.editable = val;
            torsoSlot.editable = val;
            legsSlot.editable = val;
            feetSlot.editable = val;
            handsSlot.editable = val;
            lhSlot.editable = val;
            rhSlot.editable = val;
            neckSlot.editable = val;
            waistSlot.editable = val;
            ring1Slot.editable = val;
            ring2Slot.editable = val;
            ring3Slot.editable = val;
            ring4Slot.editable = val;
            ring5Slot.editable = val;
            ring6Slot.editable = val;
            traitList.setEditable(val);
            talentList.setEditable(val);

            _editable = val;
        }

        public void registerModifier(string tag, string stat, int amount) {
            registerModifier(tag, stat, amount, tag, "");
        }

        public void registerModifier(string tag, string stat, int amount, string hint) {
            registerModifier(tag, stat, amount, hint, "");
        }

        public void registerModifier(string tag, string stat, int amount, string hint, string parameter) {
            if (stat != null && tag != null && amount != 0) {
                switch (stat.ToUpper()) {
                    case "STR":
                        strProfPanel.setAddend(tag, amount, hint);
                        break;
                    case "AGI":
                        agiProfPanel.setAddend(tag, amount, hint);
                        break;
                    case "INT":
                        intProfPanel.setAddend(tag, amount, hint);
                        break;
                    case "FOR":
                        forProfPanel.setAddend(tag, amount, hint);
                        break;
                    case "CHA":
                        chaProfPanel.setAddend(tag, amount, hint);
                        break;
                    case "STRMOD":
                        strProfPanel.Modifier.setAddend(tag, amount, hint);
                        break;
                    case "AGIMOD":
                        agiProfPanel.Modifier.setAddend(tag, amount, hint);
                        break;
                    case "INTMOD":
                        intProfPanel.Modifier.setAddend(tag, amount, hint);
                        break;
                    case "FORMOD":
                        forProfPanel.Modifier.setAddend(tag, amount, hint);
                        break;
                    case "CHAMOD":
                        chaProfPanel.Modifier.setAddend(tag, amount, hint);
                        break;
                    case "ACC":
                        csAccuracy.setAddend(tag, amount, hint);
                        break;
                    case "DEF":
                        csDefense.setAddend(tag, amount, hint);
                        break;
                    case "MDEF":
                        csMagicDefense.setAddend(tag, amount, hint);
                        break;
                    case "POW":
                        if(parameter == "left") {
                            csPowerL.setAddend(tag, amount, hint);
                        } else if(parameter == "right") {
                            csPowerR.setAddend(tag, amount, hint);
                        } else {
                            csPowerL.setAddend(tag, amount, hint);
                            csPowerR.setAddend(tag, amount, hint);
                        }
                        break;
                    case "MPOW":
                        if (parameter == "left") {
                            csMagicPowerL.setAddend(tag, amount, hint);
                        } else if (parameter == "right") {
                            csMagicPowerR.setAddend(tag, amount, hint);
                        } else {
                            csMagicPowerL.setAddend(tag, amount, hint);
                            csMagicPowerR.setAddend(tag, amount, hint);
                        }
                        break;
                    case "DMG":
                        if(parameter == "left") {
                            csDamageL.setAddend(tag, amount, hint);
                        } else if(parameter == "right") {
                            csDamageR.setAddend(tag, amount, hint);
                        } else {
                            csDamageL.setAddend(tag, amount, hint);
                            csDamageR.setAddend(tag, amount, hint);
                        }
                        break;
                    case "MDMG":
                        if(parameter == "left") {
                            csMagicDamageL.setAddend(tag, amount, hint);
                        } else if(parameter == "right") {
                            csMagicDamageR.setAddend(tag, amount, hint);
                        } else {
                            csMagicDamageL.setAddend(tag, amount, hint);
                            csMagicDamageR.setAddend(tag, amount, hint);
                        }
                        break;
                    case "MP":
                        csMP.setAddend(tag, amount, hint);
                        break;
                    case "EP":
                        csEP.setAddend(tag, amount, hint);
                        break;
                    case "SPD":
                        csSpeed.setAddend(tag, amount, hint);
                        break;
                    case "DDG":
                        csDodge.setAddend(tag, amount, hint);
                        break;
                    case "EXHA":
                        
                        break;
                    case "AWA":
                    case "WGT":
                    case "SUR":
                    case "REG":
                    case "WIL":
                    case "SPI":
                    case "STLTH":
                        attribute(stat)?.setAddend(tag, amount, hint);
                        break;
                    case "BLURES":
                        bluntResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "COLRES":
                        coldResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "DARRES":
                        darkResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "FORRES":
                        forceResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "HEARES":
                        heatResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "LIGRES":
                        lightResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "PIERES":
                        pierceResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "PSIRES":
                        psionicResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "SHORES":
                        shockResistPanel.setAddend(tag, amount, hint);
                        break;
                    case "SLARES":
                        slashResistPanel.setAddend(tag, amount, hint);
                        break;
                }
            }
        }

        public void removeModifierFromAll(string tag) {
            strProfPanel.removeAddend(tag);
            agiProfPanel.removeAddend(tag);
            intProfPanel.removeAddend(tag);
            forProfPanel.removeAddend(tag);
            chaProfPanel.removeAddend(tag);
            strProfPanel.Modifier.removeAddend(tag);
            agiProfPanel.Modifier.removeAddend(tag);
            intProfPanel.Modifier.removeAddend(tag);
            forProfPanel.Modifier.removeAddend(tag);
            chaProfPanel.Modifier.removeAddend(tag);
            csAccuracy.removeAddend(tag);
            csDefense.removeAddend(tag);
            csMagicDefense.removeAddend(tag);
            csPowerL.removeAddend(tag);
            csPowerR.removeAddend(tag);
            csMagicPowerL.removeAddend(tag);
            csMagicPowerR.removeAddend(tag);
            csDamageL.removeAddend(tag);
            csDamageR.removeAddend(tag);
            csMagicDamageL.removeAddend(tag);
            csMagicDamageR.removeAddend(tag);
            csMP.removeAddend(tag);
            csEP.removeAddend(tag);
            csSpeed.removeAddend(tag);
            csDodge.removeAddend(tag);
            attribute("AWA")?.removeAddend(tag);
            attribute("WGT")?.removeAddend(tag);
            attribute("SUR")?.removeAddend(tag);
            attribute("REG")?.removeAddend(tag);
            attribute("WIL")?.removeAddend(tag);
            attribute("SPI")?.removeAddend(tag);
            attribute("STLTH")?.removeAddend(tag);
            bluntResistPanel.removeAddend(tag);
            coldResistPanel.removeAddend(tag);
            darkResistPanel.removeAddend(tag);
            forceResistPanel.removeAddend(tag);
            heatResistPanel.removeAddend(tag);
            lightResistPanel.removeAddend(tag);
            pierceResistPanel.removeAddend(tag);
            psionicResistPanel.removeAddend(tag);
            slashResistPanel.removeAddend(tag);
            shockResistPanel.removeAddend(tag);
        }

        public void removeModifier(string tag, string stat) {
            if(tag != null && stat != null) {
                switch(stat.ToUpper()) {
                    case "ACC":

                        break;
                    case "AGIMOD":

                        break;
                    case "AWA":
                    case "WGT":
                    case "SUR":
                    case "REG":
                    case "WIL":
                    case "SPI":
                    case "STLTH":
                        csAccuracy.removeAddend(tag);
                        break;
                }
            }
        }

        private void controlViewButton_Click(object sender, EventArgs e) {
            if(mainControlPanel.RowStyles[2].Height == 0) {
                mainControlPanel.RowStyles[2].Height = 150;
                controlViewButton.Text = "˅";
            } else {
                mainControlPanel.RowStyles[2].Height = 0;
                controlViewButton.Text = "˄";
            }
        }

        // Reset Field Data from CharacterData object
        private void refreshFields() {
            refreshing = true;
            
            refreshInfoFields();
            levelXPLabel.Text = charaDat.level.ToString();
            appearanceText.Text = charaDat.appearance;
            bioText.Text = charaDat.biography;
            languagesField.Text = charaDat.languages;
            notesField.Text = charaDat.notes0;
            ayaseyeModuleCheckbox.Checked = charaDat.modules.Contains("Ayaseye");
            eigolynModuleCheckbox.Checked = charaDat.modules.Contains("Eigolyn");

            string mFilter = "Core";
            if (ayaseyeModuleCheckbox.Checked) mFilter += "|Ayaseye";
            if (eigolynModuleCheckbox.Checked) mFilter += "|Eigolyn";
            talentList.ModuleFilters = mFilter;
            traitList.ModuleFilters = mFilter;
            schoolList.ModuleFilters = mFilter;
            specializedAbilityGrid.ModuleFilters = mFilter;
            auxiliaryAbilityGrid.ModuleFilters = mFilter;

            strProfPanel.setBaseline(charaDat.strength);
            agiProfPanel.setBaseline(charaDat.agility);
            intProfPanel.setBaseline(charaDat.intelligence);
            forProfPanel.setBaseline(charaDat.fortitude);
            chaProfPanel.setBaseline(charaDat.charisma);

            hpMeter.current = charaDat.hpCurrent;
            hpMeter.max = charaDat.hpMax;
            xpCurrentSpinner.Value = (charaDat.xpCurrent <= xpCurrentSpinner.Maximum && charaDat.xpCurrent >= xpCurrentSpinner.Minimum) ? charaDat.xpCurrent : 0;
            xpCurrentSpinner.Value = (charaDat.xpNext <= xpNextSpinner.Maximum && charaDat.xpNext >= xpNextSpinner.Minimum) ? charaDat.xpNext : 0;
            levelXPLabel.Text = charaDat.level.ToString();
            conductSpinner.Value = charaDat.conduct;
            moralitySpinner.Value = charaDat.morality;
            fameSpinner.Value = charaDat.fame;
            infamySpinner.Value = charaDat.infamy;
            epAdjustSpinner.Value = charaDat.staminaAdjust;
            mpAdjustSpinner.Value = charaDat.channelingAdjust;
            exhaustSpinner.Value = charaDat.exhaustion;

            notesField1.Text = charaDat.notes1;
            notesField2.Text = charaDat.notes2;
            notesField3.Text = charaDat.notes3;
            
            refreshCompoundedStats();
            refreshTraitsTalents();
            refreshSchools();
            refreshAbilities();
            refreshInventory();
            refreshWallet();
            refreshing = false;
        }

        private void refreshInfoFields() {
            nameLabel.Text = charaDat.name;
            descriptionLabel.Text = "Level " + charaDat.level + " " + charaDat.race + "  -  " + charaDat.gender;
            ageLabel.Text = "Born: " + charaDat.birth + "  -  Age: " + charaDat.age;
            homeLabel.Text = charaDat.home;
            heightLabel.Text = "Height: " + charaDat.height + "  -  Weight: " + charaDat.weight;
            hairlabel.Text = "Hair: " + charaDat.hair + "  -  Eyes: " + charaDat.eyes;
            levelSpinner.Value = charaDat.level;
            levelXPLabel.Text = charaDat.level.ToString();

            removeModifierFromAll(CompoundedStat.RACE);
            switch (charaDat.functionalRace) {
                case (int)CharacterData.Races.Daea:
                    registerModifier(CompoundedStat.RACE, "CHA", 2, "Race: Daea");
                    registerModifier(CompoundedStat.RACE, "REG", 3, "Race: Daea");
                    break;
                case (int)CharacterData.Races.Darkan:
                    break;
                case (int)CharacterData.Races.Dehnamyn:
                    registerModifier(CompoundedStat.RACE, "AGI", 2, "Race: Dehnamyn");
                    break;
                case (int)CharacterData.Races.Dwarf:
                    registerModifier(CompoundedStat.RACE, "STR", 1, "Race: Dwarf");
                    registerModifier(CompoundedStat.RACE, "FOR", 2, "Race: Dwarf");
                    break;
                case (int)CharacterData.Races.Elf:
                    registerModifier(CompoundedStat.RACE, "AGI", 2, "Race: Elf");
                    registerModifier(CompoundedStat.RACE, "INT", 1, "Race: Elf");
                    break;
                case (int)CharacterData.Races.Gnome:
                    break;
                case (int)CharacterData.Races.Human:
                    break;
                case (int)CharacterData.Races.Illinsern:
                    registerModifier(CompoundedStat.RACE, "INT", 2, "Race: Illinsern");
                    break;
                case (int)CharacterData.Races.Koahdu:
                    registerModifier(CompoundedStat.RACE, "STR", 3, "Race: Koahdu");
                    registerModifier(CompoundedStat.RACE, "FOR", 1, "Race: Koahdu");
                    break;
                case (int)CharacterData.Races.Lyca:
                    registerModifier(CompoundedStat.RACE, "AGI", 3, "Race: Lyca");
                    registerModifier(CompoundedStat.RACE, "SPD", 5, "Race: Lyca");
                    break;
                case (int)CharacterData.Races.Nautaia:
                    registerModifier(CompoundedStat.RACE, "AGI", 2, "Race: Daea");
                    break;
                case (int)CharacterData.Races.Nyhma:
                    registerModifier(CompoundedStat.RACE, "INT", 2, "Race: Nyhma");
                    registerModifier(CompoundedStat.RACE, "CHA", 2, "Race: Nyhma");
                    break;
                case (int)CharacterData.Races.Viyr:
                    break;
                default:
                    break;
            }
        }

        private void refreshCompoundedStats() {
            int staminaBonus, channelingBonus;
            switch(charaDat.functionalRace) {
                case (int)CharacterData.Races.Daea:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Darkan:
                    staminaBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Dehnamyn:
                    staminaBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Dwarf:
                    staminaBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.LowestEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Elf:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Gnome:
                    staminaBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Human:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Illinsern:
                    staminaBonus = (int)ProficiencyLookups.LowEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.HighestEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Koahdu:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.LowestEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Lyca:
                    staminaBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Nautaia:
                    staminaBonus = (int)ProficiencyLookups.LowEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.HighestEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Nyhma:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.HighEPMP(charaDat.level);
                    break;
                case (int)CharacterData.Races.Viyr:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    break;
                default:
                    staminaBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    channelingBonus = (int)ProficiencyLookups.StandardEPMP(charaDat.level);
                    break;
            }
            
            if(charaDat.classic) {
                csDamageL.setAddend(      CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("BDMG", strProfPanel.value));
                csDamageR.setAddend(      CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("BDMG", strProfPanel.value));
                csMagicDamageL.setAddend( CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                csMagicDamageR.setAddend( CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                csPowerL.setAddend(       CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("POW", strProfPanel.value));
                csPowerR.setAddend(       CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("POW", strProfPanel.value));
                csMagicPowerL.setAddend(  CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("MPOW", intProfPanel.value));
                csMagicPowerR.setAddend(  CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("MPOW", intProfPanel.value));
                csDefense.setAddend(      CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("BDEF", strProfPanel.value));
                csMagicDefense.setAddend( CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("SPLR", intProfPanel.value));
                csMP.setAddend(           CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("INTMPB", intProfPanel.value) + 
                    (int)ProficiencyLookups.ClassicAttribute("CHAMPB", chaProfPanel.value));
                csEP.setAddend(           CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("AGIEPB", agiProfPanel.value) + 
                    (int)ProficiencyLookups.ClassicAttribute("CHAEPB", chaProfPanel.value));
                csDodge.setAddend(        CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("DB", agiProfPanel.value));
                csSpeed.setAddend(        CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("SPD", intProfPanel.value));
                csAccuracy.setAddend(     CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("ACC", agiProfPanel.value));

                attribute("STRHPB")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("STRHPB", strProfPanel.value));
                attribute("EPC")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("EPC", strProfPanel.value));
                attribute("WGT")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("WGT", strProfPanel.value));
                attribute("AGIEPB")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("AGIEPB", agiProfPanel.value));
                attribute("STLTH")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("STLTH", agiProfPanel.value));
                attribute("CC")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("CC", agiProfPanel.value));
                attribute("INTMPB")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("INTMPB", intProfPanel.value));
                attribute("MPC")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("MPC", intProfPanel.value));
                attribute("CONC")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("CONC", intProfPanel.value));
                attribute("DTM")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("DTM", intProfPanel.value));
                attribute("FORHPB")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("FORHPB", forProfPanel.value));
                attribute("HPREG")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("HPREG", forProfPanel.value));
                attribute("IMM")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("IMM", forProfPanel.value));
                attribute("SUR")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("SUR", forProfPanel.value));
                attribute("BAL")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("BAL", forProfPanel.value));
                attribute("CHAEPB")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("CHAEPB", chaProfPanel.value));
                attribute("CHAMPB")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("CHAMPB", chaProfPanel.value));
                attribute("SPCH")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("SPCH", chaProfPanel.value));
                attribute("LEAD")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("LEAD", chaProfPanel.value));
                attribute("INTEG")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.ClassicAttribute("INTEG", chaProfPanel.value));
            } else {
                switch(_charaDat.wieldMode) {
                    case CharacterData.LH_WIELD:
                        if (lhSlot.itemData == null) {
                            csDamageL.setAddend(CompoundedStat.BASELINE, (strProfPanel.value > agiProfPanel.value) ?
                                (int)ProficiencyLookups.BaseDamage(strProfPanel.value) : (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            csMagicDamageL.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                        } else {
                            if (lhSlot.itemData.modProf.Contains("STR") && lhSlot.itemData.modProf.Contains("AGI")) {
                                csDamageL.setAddend(CompoundedStat.BASELINE, (strProfPanel.value > agiProfPanel.value) ?
                                    (int)ProficiencyLookups.BaseDamage(strProfPanel.value) : (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            } else if (lhSlot.itemData.modProf.Contains("STR")) {
                                csDamageL.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseDamage(strProfPanel.value));
                            } else if (lhSlot.itemData.modProf.Contains("AGI")) {
                                csDamageL.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            } else {
                                csDamageL.setAddend(CompoundedStat.BASELINE, 0);
                            }
                            csMagicDamageL.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                        }
                        break;
                    case CharacterData.RH_WIELD:
                        if (rhSlot.itemData == null) {
                            csDamageR.setAddend(CompoundedStat.BASELINE, (strProfPanel.value > agiProfPanel.value) ?
                                (int)ProficiencyLookups.BaseDamage(strProfPanel.value) : (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            csMagicDamageR.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                        } else {
                            if (rhSlot.itemData.modProf.Contains("STR") && rhSlot.itemData.modProf.Contains("AGI")) {
                                csDamageR.setAddend(CompoundedStat.BASELINE, (strProfPanel.value > agiProfPanel.value) ?
                                    (int)ProficiencyLookups.BaseDamage(strProfPanel.value) : (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            } else if (rhSlot.itemData.modProf.Contains("STR")) {
                                csDamageR.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseDamage(strProfPanel.value));
                            } else if (rhSlot.itemData.modProf.Contains("AGI")) {
                                csDamageR.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            } else {
                                csDamageR.setAddend(CompoundedStat.BASELINE, 0);
                            }
                            csMagicDamageR.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                        }
                        break;
                    case CharacterData.DUAL_WIELD:
                        if(lhSlot.itemData == null) {
                            csDamageL.setAddend(CompoundedStat.BASELINE, (strProfPanel.value > agiProfPanel.value) ?
                                (int)ProficiencyLookups.BaseDamage(strProfPanel.value) : (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            csMagicDamageL.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                        } else {
                            csDamageL.setAddend(CompoundedStat.BASELINE, 0);
                            csMagicDamageL.setAddend(CompoundedStat.BASELINE, 0);
                        }
                        if (rhSlot.itemData == null) {
                            csDamageR.setAddend(CompoundedStat.BASELINE, (strProfPanel.value > agiProfPanel.value) ?
                                (int)ProficiencyLookups.BaseDamage(strProfPanel.value) : (int)ProficiencyLookups.BaseDamage(agiProfPanel.value));
                            csMagicDamageR.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.BaseMagicDamage(intProfPanel.value));
                        } else {
                            csDamageR.setAddend(CompoundedStat.BASELINE, 0);
                            csMagicDamageR.setAddend(CompoundedStat.BASELINE, 0);
                        }
                        
                        break;
                }
                

                csPowerL.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Power(strProfPanel.value) + ProficiencyLookups.Power(agiProfPanel.value)) / 2));
                csPowerR.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Power(strProfPanel.value) + ProficiencyLookups.Power(agiProfPanel.value)) / 2));
                csMagicPowerL.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.MagicPower(intProfPanel.value) + ProficiencyLookups.MagicPower(agiProfPanel.value)) / 2));
                csMagicPowerR.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.MagicPower(intProfPanel.value) + ProficiencyLookups.MagicPower(agiProfPanel.value)) / 2));
                csDefense.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.BaseDefense(strProfPanel.value) + ProficiencyLookups.BaseDefense(forProfPanel.value)) / 2));
                csMagicDefense.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.BaseMagicDefense(chaProfPanel.value) + ProficiencyLookups.BaseMagicDefense(intProfPanel.value)) / 2));
                csMP.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Channeling(chaProfPanel.value) + ProficiencyLookups.Channeling(intProfPanel.value)) / 2));
                csMP.setAddend("race", channelingBonus);
                csEP.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Stamina(strProfPanel.value) + ProficiencyLookups.Stamina(forProfPanel.value)) / 2));
                csEP.setAddend("race", staminaBonus);
                csDodge.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.Dodge(agiProfPanel.value));
                csSpeed.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.Speed(agiProfPanel.value));
                csAccuracy.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.Accuracy(agiProfPanel.value));

                attribute("WGT")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.WeightAllowance(strProfPanel.value));
                attribute("AWA")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.Awareness(intProfPanel.value));
                attribute("SUR")?.setAddend(CompoundedStat.BASELINE, (int)ProficiencyLookups.Survival(forProfPanel.value));
                attribute("REG")?.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Regen(forProfPanel.value) + ProficiencyLookups.Regen(chaProfPanel.value)) / 2));
                attribute("WIL")?.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Willpower(chaProfPanel.value) + ProficiencyLookups.Willpower(intProfPanel.value)) / 2));
                attribute("SPI")?.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Spirit(chaProfPanel.value) + ProficiencyLookups.Spirit(intProfPanel.value)) / 2));
                attribute("STLTH")?.setAddend(CompoundedStat.BASELINE, (int)((ProficiencyLookups.Stealth(agiProfPanel.value) + ProficiencyLookups.Stealth(intProfPanel.value)) / 2));

                recalculateDamageFields();
            }
        }

        // Convenience method for accessing attribute panels that may or may not exist
        private AttributePanel attribute(string key) {
            if(attributePanels.ContainsKey(key)) {
                return attributePanels[key];
            }
            return null;
        }

        private void updateBasicData() {
            /* These fields are updated systematically
            _charaDat.name = nameField.Text;
            _charaDat.gender = genderField.Text;
            _charaDat.race = raceField.Text;
            _charaDat.birth = birthField.Text;
            _charaDat.age = ageField.Text;
            _charaDat.home = homeField.Text;
            _charaDat.height = heightField.Text;
            _charaDat.weight = weightField.Text;
            _charaDat.hair = hairField.Text;
            _charaDat.eyes = eyesField.Text;
            _charaDat.level = (int)levelSpinner.Value;
            */
            _charaDat.appearance = appearanceText.Text;
            _charaDat.biography = bioText.Text;
            _charaDat.languages = languagesField.Text;
            _charaDat.notes0 = notesField.Text;
            _charaDat.notes1 = notesField1.Text;
            _charaDat.notes2 = notesField2.Text;
            _charaDat.notes3 = notesField3.Text;
            _charaDat.strength = strProfPanel.getBaseline();
            _charaDat.agility = agiProfPanel.getBaseline();
            _charaDat.intelligence = intProfPanel.getBaseline();
            _charaDat.fortitude = forProfPanel.getBaseline();
            _charaDat.charisma = chaProfPanel.getBaseline();
            _charaDat.hpCurrent = hpMeter.current;
            _charaDat.hpMax = hpMeter.max;
            _charaDat.xpCurrent = (int)xpCurrentSpinner.Value;
            _charaDat.xpNext = (int)xpNextSpinner.Value;
            _charaDat.conduct = (int)conductSpinner.Value;
            _charaDat.morality = (int)moralitySpinner.Value;
            _charaDat.fame = (int)fameSpinner.Value;
            _charaDat.infamy = (int)infamySpinner.Value;
            _charaDat.staminaAdjust = (int)epAdjustSpinner.Value;
            _charaDat.channelingAdjust = (int)mpAdjustSpinner.Value;
            _charaDat.exhaustion = (int)exhaustSpinner.Value;
        }

        private void updateSchools() {
            _charaDat.schools = schoolList.generateString();
            _charaDat.vocations = vocationList.generateString();
        }

        private void refreshSchools() {
            schoolList.refreshing = true;
            while(schoolList.GetSchoolCount() > charaDat.schoolsList.Count) {
                schoolList.RemoveSchoolAt(schoolList.GetSchoolCount() - 1);
            }
            for(int index = 0; index < charaDat.schoolsList.Count; ++index) {
                if(index >= schoolList.GetSchoolCount()) {
                    schoolList.AddSchool(charaDat.schoolsList[index]);
                } else {
                    schoolList.SetSchoolAt(index, charaDat.schoolsList[index]);
                }
            }
            schoolList.refreshing = false;
            vocationList.refreshing = true;
            while(vocationList.GetVocationCount() > charaDat.vocationsList.Count) {
                vocationList.RemoveVocationAt(vocationList.GetVocationCount() - 1);
            }
            for(int index = 0; index < charaDat.vocationsList.Count; ++index) {
                if(index >= vocationList.GetVocationCount()) {
                    vocationList.addVocation(charaDat.vocationsList[index]);
                } else {
                    vocationList.SetVocationAt(index, charaDat.vocationsList[index]);
                }
            }
            vocationList.refreshing = false;
        }

        private void updateAbilities() {
            _charaDat.abilities = specializedAbilityGrid.generateString();
            _charaDat.auxAbilities = auxiliaryAbilityGrid.generateString();
        }

        private void refreshAbilities() {
            specializedAbilityGrid.refreshing = true;
            while(specializedAbilityGrid.getAbilityCount() > charaDat.abilitiesList.Count) {
                specializedAbilityGrid.removeAbilityAt(specializedAbilityGrid.getAbilityCount() - 1);
            }
            for(int index = 0; index < charaDat.abilitiesList.Count; ++index) {
                if(index >= specializedAbilityGrid.getAbilityCount()) {
                    specializedAbilityGrid.addAbility(charaDat.abilitiesList[index]);
                } else {
                    specializedAbilityGrid.setAbilityAt(index, charaDat.abilitiesList[index]);
                }
            }
            specializedAbilityGrid.refreshing = false;
            auxiliaryAbilityGrid.refreshing = true;
            while (auxiliaryAbilityGrid.getAbilityCount() > charaDat.auxAbilitiesList.Count) {
                auxiliaryAbilityGrid.removeAbilityAt(auxiliaryAbilityGrid.getAbilityCount() - 1);
            }
            for (int index = 0; index < charaDat.auxAbilitiesList.Count; ++index) {
                if (index >= auxiliaryAbilityGrid.getAbilityCount()) {
                    auxiliaryAbilityGrid.addAbility(charaDat.auxAbilitiesList[index]);
                } else {
                    auxiliaryAbilityGrid.setAbilityAt(index, charaDat.auxAbilitiesList[index]);
                }
            }
            auxiliaryAbilityGrid.refreshing = false;
        }

        private void updateTraitsTalents() {
            _charaDat.traits = traitList.generateString();
            _charaDat.talents = talentList.generateString();
        }

        private void refreshTraitsTalents() {
            traitList.refreshing = true;
            while(traitList.GetTraitCount() > charaDat.traitsList.Count) {
                traitList.RemoveTraitAt(traitList.GetTraitCount() - 1);
            }
            for(int index = 0; index < charaDat.traitsList.Count; ++index) {
                if (index >= traitList.GetTraitCount()) {
                    traitList.AddTrait(charaDat.traitsList[index]);
                } else {
                    traitList.SetTraitAt(index, charaDat.traitsList[index]);
                }
            }
            traitList.refreshing = false;

            talentList.refreshing = true;
            while(talentList.GetTalentCount() > charaDat.talentsList.Count) {
                talentList.RemoveTalentAt(talentList.GetTalentCount() - 1);
            }
            for(int index = 0; index < charaDat.talentsList.Count; ++index) {
                if (index >= talentList.GetTalentCount()) {
                    talentList.AddTalent(charaDat.talentsList[index]);
                } else {
                    talentList.SetTalentAt(index, charaDat.talentsList[index]);
                }
            }
            talentList.refreshing = false;
        }

        private void updateInventory() {
            _charaDat.items = inventoryList.generateString();
            _charaDat.ammo = ammoSlot.itemData == null ? "" : ammoSlot.itemData.generateString();
            _charaDat.head = headSlot.itemData == null ? "" : headSlot.itemData.generateString();
            _charaDat.torso = torsoSlot.itemData == null ? "" : torsoSlot.itemData.generateString();
            _charaDat.legs = legsSlot.itemData == null ? "" : legsSlot.itemData.generateString();
            _charaDat.feet = feetSlot.itemData == null ? "" : feetSlot.itemData.generateString();
            _charaDat.hands = handsSlot.itemData == null ? "" : handsSlot.itemData.generateString();
            _charaDat.lh = lhSlot.itemData == null ? "" : lhSlot.itemData.generateString();
            _charaDat.rh = rhSlot.itemData == null ? "" : rhSlot.itemData.generateString();
            _charaDat.neck = neckSlot.itemData == null ? "" : neckSlot.itemData.generateString();
            _charaDat.waist = waistSlot.itemData == null ? "" : waistSlot.itemData.generateString();
            _charaDat.ring1 = ring1Slot.itemData == null ? "" : ring1Slot.itemData.generateString();
            _charaDat.ring2 = ring2Slot.itemData == null ? "" : ring2Slot.itemData.generateString();
            _charaDat.ring3 = ring3Slot.itemData == null ? "" : ring3Slot.itemData.generateString();
            _charaDat.ring4 = ring4Slot.itemData == null ? "" : ring4Slot.itemData.generateString();
            _charaDat.ring5 = ring5Slot.itemData == null ? "" : ring5Slot.itemData.generateString();
            _charaDat.ring6 = ring6Slot.itemData == null ? "" : ring6Slot.itemData.generateString();
        }

        private void refreshInventory() {
            inventoryList.refreshing = true;

            inventoryList.setInventoryByString(_charaDat.items);
            ammoSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.ammo) ? null : new ItemData(_charaDat.ammo);
            headSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.head) ? null : new ItemData(_charaDat.head);
            torsoSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.torso) ? null : new ItemData(_charaDat.torso);
            legsSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.legs) ? null : new ItemData(_charaDat.legs);
            feetSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.feet) ? null : new ItemData(_charaDat.feet);
            handsSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.hands) ? null : new ItemData(_charaDat.hands);
            lhSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.lh) ? null : new ItemData(_charaDat.lh);
            rhSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.rh) ? null : new ItemData(_charaDat.rh);
            neckSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.neck) ? null : new ItemData(_charaDat.neck);
            waistSlot.itemData = String.IsNullOrWhiteSpace(_charaDat.waist) ? null : new ItemData(_charaDat.waist);
            ring1Slot.itemData = String.IsNullOrWhiteSpace(_charaDat.ring1) ? null : new ItemData(_charaDat.ring1);
            ring2Slot.itemData = String.IsNullOrWhiteSpace(_charaDat.ring2) ? null : new ItemData(_charaDat.ring2);
            ring3Slot.itemData = String.IsNullOrWhiteSpace(_charaDat.ring3) ? null : new ItemData(_charaDat.ring3);
            ring4Slot.itemData = String.IsNullOrWhiteSpace(_charaDat.ring4) ? null : new ItemData(_charaDat.ring4);
            ring5Slot.itemData = String.IsNullOrWhiteSpace(_charaDat.ring5) ? null : new ItemData(_charaDat.ring5);
            ring6Slot.itemData = String.IsNullOrWhiteSpace(_charaDat.ring6) ? null : new ItemData(_charaDat.ring6);

            inventoryList.refreshing = false;
        }

        private void updateWallet() {
            string walletString = "";
            walletString += "[mark]" + (int)markSpinner.Value + "[/mark]";
            walletString += "[denarius]" + (int)denariusSpinner.Value + "[/denarius]";
            walletString += "[drake]" + (int)drakeSpinner.Value + "[/drake]";
            walletString += "[florin]" + (int)florinSpinner.Value + "[/florin]";
            walletString += "[allervian]" + (int)allervianSpinner.Value + "[/allervian]";
            _charaDat.wallet = walletString;
        }

        private void refreshWallet() {
            bool old = refreshing;
            refreshing = true;

            int i = 0;
            markSpinner.Value = int.TryParse(StringFunctions.ReadTag(_charaDat.wallet, "mark"), out i) ? i : 0;
            denariusSpinner.Value = int.TryParse(StringFunctions.ReadTag(_charaDat.wallet, "denarius"), out i) ? i : 0;
            drakeSpinner.Value = int.TryParse(StringFunctions.ReadTag(_charaDat.wallet, "drake"), out i) ? i : 0;
            florinSpinner.Value = int.TryParse(StringFunctions.ReadTag(_charaDat.wallet, "florin"), out i) ? i : 0;
            allervianSpinner.Value = int.TryParse(StringFunctions.ReadTag(_charaDat.wallet, "allervian"), out i) ? i : 0;

            int total = 0;
            total += (int)markSpinner.Value;
            total += (int)denariusSpinner.Value * 100;
            total += (int)drakeSpinner.Value * 1000;
            total += (int)florinSpinner.Value * 10000;
            total += (int)allervianSpinner.Value * 100000;
            totalCoinValueLabel.Text = total.ToString();

            refreshing = old;
        }

        private void recalculateDamageFields() {
            switch (_charaDat.wieldMode) {
                case CharacterData.LH_WIELD:
                    if (lhSlot.itemData == null) {
                        damageValueLabel.Text = StringFunctions.simplifyDiceString((magicAttackMode ? csMagicDamageL.Value.ToString() : csDamageL.Value.ToString()));
                    } else {
                        damageValueLabel.Text = StringFunctions.simplifyDiceString((magicAttackMode ?
                            lhSlot.itemData.mdmg + (csMagicDamageL.Value == 0 ? "" : "+" + csMagicDamageL.Value) :
                            lhSlot.itemData.dmg + (csDamageL.Value == 0 ? "" : "+" + csDamageL.Value)));
                    }
                    break;
                case CharacterData.RH_WIELD:
                    if (rhSlot.itemData == null) {
                        damageValueLabel2.Text = StringFunctions.simplifyDiceString((magicAttackMode ? csMagicDamageR.Value.ToString() : csDamageR.Value.ToString()));
                    } else {
                        damageValueLabel2.Text = StringFunctions.simplifyDiceString((magicAttackMode ?
                            rhSlot.itemData.mdmg + (csMagicDamageR.Value == 0 ? "" : "+" + csMagicDamageR.Value) :
                            rhSlot.itemData.dmg + (csDamageR.Value == 0 ? "" : "+" + csDamageR.Value)));
                    }
                    break;
                case CharacterData.DUAL_WIELD:
                    if(lhSlot.itemData == null) {
                        damageValueLabel.Text = StringFunctions.simplifyDiceString((magicAttackMode ? csMagicDamageL.Value.ToString() : csDamageL.Value.ToString()));
                    } else {
                        damageValueLabel.Text = StringFunctions.simplifyDiceString((magicAttackMode ?
                            lhSlot.itemData.mdmg + (csMagicDamageL.Value == 0 ? "" : "+" + csMagicDamageL.Value) :
                            lhSlot.itemData.dmg + (csDamageL.Value == 0 ? "" : "+" + csDamageL.Value)));
                    }

                    if(rhSlot.itemData == null) {
                        damageValueLabel2.Text = StringFunctions.simplifyDiceString((magicAttackMode ? csMagicDamageR.Value.ToString() : csDamageR.Value.ToString()));
                    } else {
                        damageValueLabel2.Text = StringFunctions.simplifyDiceString((magicAttackMode ?
                            rhSlot.itemData.mdmg + (csMagicDamageR.Value == 0 ? "" : "+" + csMagicDamageR.Value) :
                            rhSlot.itemData.dmg + (csDamageR.Value == 0 ? "" : "+" + csDamageR.Value)));
                    }
                    break;
                default:
                    damageValueLabel.Text = StringFunctions.simplifyDiceString((magicAttackMode ? csMagicDamageL.Value.ToString() : csDamageL.Value.ToString()));
                    damageValueLabel2.Text = StringFunctions.simplifyDiceString((magicAttackMode ? csMagicDamageR.Value.ToString() : csDamageR.Value.ToString()));
                    break;
            }
        }

        private void textField_Leave(object sender, EventArgs e) {
            if (sender is TextBox) {
                if(defaults.ContainsKey((TextBox)sender)) {
                    if (String.IsNullOrEmpty(((TextBox)sender).Text)) {
                        ((TextBox)sender).Text = defaults[(TextBox)sender];
                        ((TextBox)sender).ForeColor = SystemColors.GrayText;
                    }
                }
            }
        }

        private void textField_Enter(object sender, EventArgs e) {
            if (sender is TextBox) {
                if(defaults.ContainsKey((TextBox)sender)) {
                    if (((TextBox)sender).Text.Equals(defaults[(TextBox)sender])) {
                        ((TextBox)sender).Text = "";
                        ((TextBox)sender).ForeColor = SystemColors.WindowText;
                    }
                }
            }
        }

        private void editBasicsButton_Click(object sender, EventArgs e) {
            nameField.Text = (String.IsNullOrEmpty(charaDat.name) ? defaults[nameField] : charaDat.name);
            raceField.Text = (String.IsNullOrEmpty(charaDat.race) ? defaults[raceField] : charaDat.race);
            genderField.Text = (String.IsNullOrEmpty(charaDat.gender) ? defaults[genderField] : charaDat.gender);
            ageField.Text = (String.IsNullOrEmpty(charaDat.age) ? defaults[ageField] : charaDat.age);
            birthField.Text = (String.IsNullOrEmpty(charaDat.birth) ? defaults[birthField] : charaDat.birth);
            homeField.Text = (String.IsNullOrEmpty(charaDat.home) ? defaults[homeField] : charaDat.home);
            heightField.Text = (String.IsNullOrEmpty(charaDat.height) ? defaults[heightField] : charaDat.height);
            weightField.Text = (String.IsNullOrEmpty(charaDat.weight) ? defaults[weightField] : charaDat.weight);
            hairField.Text = (String.IsNullOrEmpty(charaDat.hair) ? defaults[hairField] : charaDat.hair);
            eyesField.Text = (String.IsNullOrEmpty(charaDat.eyes) ? defaults[eyesField] : charaDat.eyes);
            levelSpinner.Value = charaDat.level;

            basicsSplitter.Panel1.Controls.Remove(basicsViewPanel);
            basicsSplitter.Panel1.Controls.Add(basicsEditPanel);
            basicsViewPanel.Visible = false;
            basicsEditPanel.Visible = true;
        }

        private void confirmBasicsButton_Click(object sender, EventArgs e) {
            basicsSplitter.Panel1.Controls.Remove(basicsEditPanel);
            basicsSplitter.Panel1.Controls.Add(basicsViewPanel);
            basicsViewPanel.Visible = true;
            basicsEditPanel.Visible = false;

            charaDat.name = nameField.Text;
            charaDat.race = raceField.Text;
            charaDat.gender = genderField.Text;
            charaDat.height = heightField.Text;
            charaDat.weight = weightField.Text;
            charaDat.eyes = eyesField.Text;
            charaDat.hair = hairField.Text;
            charaDat.age = ageField.Text;
            charaDat.birth = birthField.Text;
            charaDat.home = homeField.Text;
            charaDat.level = (int)levelSpinner.Value;
            
            refreshInfoFields();
            refreshCompoundedStats();
        }

        private void basicsFields_TextChanged(object sender, EventArgs e) {
            if(sender is TextBox) {
                if(String.IsNullOrEmpty(((TextBox)sender).Text)) {
                    ((TextBox)sender).ForeColor = SystemColors.GrayText;
                } else {
                    ((TextBox)sender).ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void alignmentSpinner_ValueChanged(object sender, EventArgs e) {
            conductValueLabel.Text = CharacterData.getConductString((int)conductSpinner.Value);
            moralityValueLabel.Text = CharacterData.getMoralityString((int)moralitySpinner.Value);
            alignmentLabel.Text = CharacterData.getAlignmentString((int)conductSpinner.Value, (int)moralitySpinner.Value);
        }

        private void reputationSpinner_ValueChanged(object sender, EventArgs e) {
            fameValueLabel.Text = CharacterData.getFameString((int)fameSpinner.Value);
            infamyValueLabel.Text = CharacterData.getInfamyString((int)infamySpinner.Value);
            reputationLabel.Text = CharacterData.getReputationString((int)fameSpinner.Value, (int)infamySpinner.Value);
        }

        private void proficiencyPanel_ValueChanged(object sender, EventArgs e) {
            ppLabel.Text = "PP\n" + (strProfPanel.getBaseline() + agiProfPanel.getBaseline() + intProfPanel.getBaseline() + forProfPanel.getBaseline() + chaProfPanel.getBaseline()).ToString();
            refreshCompoundedStats();
        }

        private void itemSlot_Clicked(object sender, EventArgs e) {
            if(e is MouseEventArgs && sender is ItemSlot) {
                if(((MouseEventArgs)e).Button == MouseButtons.Right) {
                    if(((MouseEventArgs)e).Clicks == 1) {
                        using (var itemDesigner = new ItemDesigner(((ItemSlot)sender).itemData)) {
                            var result = itemDesigner.ShowDialog();
                            if (result == DialogResult.OK) {
                                ((ItemSlot)sender).setItemData(itemDesigner.resultItem);
                            }
                        }
                    }
                }
            }
        }

        private void itemSlot_EquipChanged(object sender, EventArgs e) {
            /*
            if(sender is ItemSlot) {
                if(((ItemSlot)sender).itemData != null) {
                    ItemData dat = ((ItemSlot)sender).itemData;
                    if (dat.type == ItemData.ARMOR_TYPE) {
                        if(dat.equipped) {
                            foreach(ItemSlot item in inventoryList.items.Values) {
                                if(item != (ItemSlot)sender) {
                                    if(item.itemData.type == ItemData.ARMOR_TYPE) {
                                        if(item.itemData.equipped) {
                                            item.itemData.equipped = false;
                                            item.refreshIcons();
                                        }
                                    }
                                }
                            }

                            torsoSlot.itemData = dat;
                            csDefense.setAddend("torso", dat.def);
                            csMagicDefense.setAddend("torso", dat.mdef);
                        } else {
                            torsoSlot.itemData = null;
                            csDefense.removeAddend("torso");
                            csMagicDefense.removeAddend("torso");
                        }
                    } else if(dat.type == ItemData.WEAPON_TYPE) {
                        if(dat.equipped) {
                            foreach (ItemSlot item in inventoryList.items.Values) {
                                if (item != (ItemSlot)sender) {
                                    if (item.itemData.type == ItemData.WEAPON_TYPE) {
                                        if (item.itemData.equipped) {
                                            item.itemData.equipped = false;
                                            item.refreshIcons();
                                        }
                                    }
                                }
                            }

                            rhSlot.itemData = dat;
                            csPower.setAddend("weapon1", dat.pow);
                            csMagicPower.setAddend("weapon1", dat.mpow);
                            compoundedStat_ValueChanged(csDamage, EventArgs.Empty);
                            compoundedStat_ValueChanged(csMagicDamage, EventArgs.Empty);
                        } else {
                            rhSlot.itemData = null;
                            csPower.removeAddend("weapon1");
                            csMagicPower.removeAddend("weapon1");
                            compoundedStat_ValueChanged(csDamage, EventArgs.Empty);
                            compoundedStat_ValueChanged(csMagicDamage, EventArgs.Empty);
                        }
                    }
                }
            }
            */
        }

        private void equipSlot_SlotClicked(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                if(e.Clicks == 1) {
                    if(sender is EquipSlot) {
                        if(((EquipSlot)sender).itemData == null) {
                            displayGenericInfo("");
                        } else {
                            displayItemInfo(((EquipSlot)sender).itemData);
                        }
                    }
                }
            }
        }

        private void equipSlot_ItemChanged(object sender, ItemEventArgs e) {
            if(sender is EquipSlot) {
                if(((EquipSlot)sender).Tag != null) {
                    string tag = ((EquipSlot)sender).Tag.ToString();
                    removeModifierFromAll(tag);
                    if(((EquipSlot)sender).itemData != null) {
                        ItemData item = ((EquipSlot)sender).itemData;
                        foreach(string s in item.getModifierKeySet()) {
                            registerModifier(tag, s, item.getModifierValue(s, false, true), item.wlTitle);
                        }
                        if(item.def != 0) {
                            registerModifier(tag, "DEF", item.def, item.wlTitle);
                        }
                        if (item.mdef != 0) {
                            registerModifier(tag, "MDEF", item.mdef, item.wlTitle);
                        }
                        if (item.pow != 0) {
                            if(sender == lhSlot) {
                                registerModifier(tag, "POW", item.pow, item.wlTitle, "left");
                            } else if(sender == rhSlot) {
                                registerModifier(tag, "POW", item.pow, item.wlTitle, "right");
                            } else {
                                registerModifier(tag, "POW", item.pow, item.wlTitle);
                            }
                        }
                        if (item.mpow != 0) {
                            if (sender == lhSlot) {
                                registerModifier(tag, "MPOW", item.mpow, item.wlTitle, "left");
                            } else if (sender == rhSlot) {
                                registerModifier(tag, "MPOW", item.mpow, item.wlTitle, "right");
                            } else {
                                registerModifier(tag, "MPOW", item.mpow, item.wlTitle);
                            }
                        }
                        if(item.block != 0) {
                            registerModifier(tag, "DDG", item.block, item.wlTitle);
                        }

                    }
                }
                //if(sender == lhSlot || sender == rhSlot) {
                    refreshCompoundedStats();
                //}
            }
        }

        private void traitList_ValueChanged(object sender, EventArgs e) {
            if(!traitList.refreshing) charaDat.traits = traitList.generateString();
        }

        private void traitList_TraitClicked(object sender, EventArgs e) {
            if(sender is TraitPanel) {
                if(((MouseEventArgs)e).Button == MouseButtons.Left) {
                    displayTraitInfo(((TraitPanel)sender).trait);
                }
            }
        }

        private void traitList_TraitAdded(object sender, TraitListEventArgs e) {
            if(e.Target != null) {
                if(e.Target.trait.active) {
                    string[] keys = e.Target.trait.getModifierKeys();
                    if (keys.Count() > 0) {
                        foreach (string s in keys) {
                            registerModifier("trait" + e.Target.trait.netID, s, e.Target.trait.getModifierValue(s), "Trait: " + e.Target.trait.title);
                        }
                    }
                }
            }
        }

        private void traitList_TraitRemoved(object sender, TraitListEventArgs e) {
            if (e.Target != null) {
                removeModifierFromAll("trait" + e.Target.trait.netID);
            }
        }

        private void traitList_TraitChanged(object sender, TraitListEventArgs e) {
            if(e.PreviousTrait != null) {
                removeModifierFromAll("trait" + e.PreviousTrait.netID);
            }
            if (e.Target != null) {
                if (e.Target.trait.active) {
                    string[] keys = e.Target.trait.getModifierKeys();
                    if (keys.Count() > 0) {
                        foreach (string s in keys) {
                            registerModifier("trait" + e.Target.trait.netID, s, e.Target.trait.getModifierValue(s), "Trait: " + e.Target.trait.title);
                        }
                    }
                } else {
                    removeModifierFromAll("trait" + e.Target.trait.netID);
                }
            }
        }

        private void talentList_ValueChanged(object sender, EventArgs e) {
            charaDat.talents = talentList.generateString();
        }

        private void walletSpinner_ValueChanged(object sender, EventArgs e) {
            int total = 0;
            total += (int)markSpinner.Value;
            total += (int)denariusSpinner.Value * 100;
            total += (int)drakeSpinner.Value * 1000;
            total += (int)florinSpinner.Value * 10000;
            total += (int)allervianSpinner.Value * 100000;
            totalCoinValueLabel.Text = total.ToString();
        }

        private void epAdjustSpinner_ValueChanged(object sender, EventArgs e) {
            int v = csEP.Value - (int)exhaustSpinner.Value + (int)epAdjustSpinner.Value;
            epMeter.current = v >= epMeter.min ? v : epMeter.min;
        }

        private void mpAdjustSpinner_ValueChanged(object sender, EventArgs e) {
            int v = csMP.Value - (int)exhaustSpinner.Value + (int)mpAdjustSpinner.Value;
            mpMeter.current = v >= mpMeter.min ? v : mpMeter.min;
        }

        private void exhaustSpinner_ValueChanged(object sender, EventArgs e) {
            int v = csEP.Value - (int)exhaustSpinner.Value + (int)epAdjustSpinner.Value;
            epMeter.current = v >= epMeter.min ? v : epMeter.min;
            v = csMP.Value - (int)exhaustSpinner.Value + (int)mpAdjustSpinner.Value;
            mpMeter.current = v >= mpMeter.min ? v : mpMeter.min;
        }

        private void talentList_TalentClicked(object sender, EventArgs e) {
            if(sender is TalentPanel) {
                if(((MouseEventArgs)e).Button == MouseButtons.Left) {
                    displayTalentInfo(((TalentPanel)sender).talent);
                }
            }
        }

        private void schoolList_ValueChanged(object sender, EventArgs e) {
            charaDat.schools = schoolList.generateString();
            Dictionary<string, int> levels = new Dictionary<string, int>();
            foreach (AbilitySlot a in specializedAbilityGrid.abilitySlots) {
                if (levels.ContainsKey(a.ability.school)) {
                    levels[a.ability.school]++;
                } else {
                    levels.Add(a.ability.school, 1);
                }
            }
            schoolList.refreshing = true;
            foreach (SchoolPanel p in schoolList.panels) {
                if (levels.ContainsKey(p.school.title)) {
                    p.school.special = levels[p.school.title];
                    p.updateFields();
                }
            }
            schoolList.refreshing = false;
        }

        private void schoolList_SchoolClicked(object sender, EventArgs e) {
            if(sender is SchoolPanel) {
                if(((MouseEventArgs)e).Button == MouseButtons.Left) {
                    displaySchoolInfo(((SchoolPanel)sender).school);
                }
            }
        }
        
        private void vocationList_ValueChanged(object sender, EventArgs e) {
            charaDat.vocations = vocationList.generateString();
        }

        private void specializedAbilitiyList_ValueChanged(object sender, EventArgs e) {
            charaDat.abilities = specializedAbilityGrid.generateString();
            Dictionary<string, int> levels = new Dictionary<string, int>();
            foreach (AbilitySlot a in specializedAbilityGrid.abilitySlots) {
                if(levels.ContainsKey(a.ability.school)) {
                    levels[a.ability.school]++;
                } else {
                    levels.Add(a.ability.school, 1);
                }
            }
            schoolList.refreshing = true;
            foreach(SchoolPanel p in schoolList.panels) {
                if(levels.ContainsKey(p.school.title)) {
                    p.school.special = levels[p.school.title];
                    p.updateFields();
                }
            }
            schoolList.refreshing = false;
        }

        private void specializedAbilityList_AbilityClicked(object sender, EventArgs e) {
            if(sender is AbilitySlot) {
                if(((MouseEventArgs)e).Button == MouseButtons.Left) {
                    displayAbilityInfo(((AbilitySlot)sender).ability);
                }
            }
        }

        private void auxAbilityList_ValueChanged(object sender, EventArgs e) {
            charaDat.auxAbilities = auxiliaryAbilityGrid.generateString();
        }

        private void ayaseyeModuleCheckbox_CheckedChanged(object sender, EventArgs e) {
            if(ayaseyeModuleCheckbox.Checked) {
                _charaDat.AddModule("Ayaseye");
            } else {
                _charaDat.RemoveModule("Ayaseye");
            }
            string mFilter = "Core";
            if (ayaseyeModuleCheckbox.Checked) mFilter += "|Ayaseye";
            if (eigolynModuleCheckbox.Checked) mFilter += "|Eigolyn";
            talentList.ModuleFilters = mFilter;
            traitList.ModuleFilters = mFilter;
            schoolList.ModuleFilters = mFilter;
            specializedAbilityGrid.ModuleFilters = mFilter;
            auxiliaryAbilityGrid.ModuleFilters = mFilter;
        }

        private void xpCurrentSpinner_ValueChanged(object sender, EventArgs e) {

        }
        
        private void eigolynModuleCheckbox_CheckedChanged(object sender, EventArgs e) {
            if (ayaseyeModuleCheckbox.Checked) {
                _charaDat.AddModule("Eigolyn");
            } else {
                _charaDat.RemoveModule("Eigolyn");
            }
            string mFilter = "Core";
            if (ayaseyeModuleCheckbox.Checked) mFilter += "|Ayaseye";
            if (eigolynModuleCheckbox.Checked) mFilter += "|Eigolyn";
            talentList.ModuleFilters = mFilter;
            traitList.ModuleFilters = mFilter;
            schoolList.ModuleFilters = mFilter;
            specializedAbilityGrid.ModuleFilters = mFilter;
            auxiliaryAbilityGrid.ModuleFilters = mFilter;
        }

        private void lhButton_Click(object sender, EventArgs e) {
            switch(_charaDat.wieldMode) {
                case CharacterData.RH_WIELD:
                    SetWieldMode(CharacterData.DUAL_WIELD);
                    break;
                case CharacterData.LH_WIELD:
                    //SetWieldMode(CharacterData.RH_WIELD);
                    break;
                case CharacterData.DUAL_WIELD:
                    SetWieldMode(CharacterData.RH_WIELD);
                    break;
            }
        }

        private void rhButton_Click(object sender, EventArgs e) {
            switch (_charaDat.wieldMode) {
                case CharacterData.RH_WIELD:
                    //SetWieldMode(CharacterData.LH_WIELD);
                    break;
                case CharacterData.LH_WIELD:
                    SetWieldMode(CharacterData.DUAL_WIELD);
                    break;
                case CharacterData.DUAL_WIELD:
                    SetWieldMode(CharacterData.LH_WIELD);
                    break;
            }
        }

        private void auxAbilityList_AbilityClicked(object sender, EventArgs e) {
            if (sender is AbilitySlot) {
                if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                    displayAbilityInfo(((AbilitySlot)sender).ability);
                }
            }
        }

        private void scrollWheel_Bypass(object sender, MouseEventArgs e) {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void compoundedStat_ValueChanged(object sender, EventArgs e) {
            if (sender == csPowerL || sender == csMagicPowerL) {
                powerValueLabel.Text = (magicAttackMode ? csMagicPowerL.Value.ToString() : csPowerL.Value.ToString());
            } else if(sender == csPowerR || sender == csMagicPowerR) {
                powerValueLabel2.Text = (magicAttackMode ? csMagicPowerR.Value.ToString() : csPowerR.Value.ToString());
            } else if (sender == csDefense) {
                defValueLabel.Text = csDefense.Value.ToString();
            } else if (sender == csMagicDefense) {
                mdefValueLabel.Text = csMagicDefense.Value.ToString();
            } else if (sender == csDamageL || sender == csMagicDamageL) {
                recalculateDamageFields();
            } else if (sender == csMP) {
                mpMeter.current = csMP.Value - (int)exhaustSpinner.Value + (int)mpAdjustSpinner.Value;
                mpMeter.max = csMP.Value;
            } else if(sender == csEP) {
                epMeter.current = csEP.Value - (int)exhaustSpinner.Value + (int)epAdjustSpinner.Value;
                epMeter.max = csEP.Value;
            } else if(sender == csSpeed) {
                speedValueLabel.Text = csSpeed.Value.ToString();
            } else if(sender == csAccuracy) {
                int v = csAccuracy.Value;
                accuracyValueLabel.Text = (v >= 0 ? "+" + v : v.ToString());
            } else if(sender == csDodge) {
                dodgeValueLabel.Text = csDodge.Value.ToString();
            }
        }

        private void attributePanel_ValueChanged(object sender, AttributePanelEventArgs e) {
            if(sender is AttributePanel) {
                if (e.AttributeTag.Equals("SUR")) {
                    hpMeter.min = ((AttributePanel)sender).Value.Value >= 0 ? -((AttributePanel)sender).Value.Value : 0;
                }
            }
        }

        public void SetWieldMode(int mode) {
            _charaDat.SetWieldMode(mode);
            switch(mode) {
                case CharacterData.RH_WIELD:
                    _charaDat.SetWieldMode(mode);
                    lhButton.BackColor = SystemColors.Control;
                    rhButton.BackColor = SystemColors.ControlDark;
                    powerValueLabel2.Location = new Point(powerValueLabel2.Location.X, 32);
                    powerValueLabel.Visible = false;
                    powerValueLabel2.Visible = true;
                    damageValueLabel2.Location = new Point(damageValueLabel2.Location.X, 32);
                    damageValueLabel.Visible = false;
                    damageValueLabel2.Visible = true;
                    refreshCompoundedStats();
                    break;
                case CharacterData.LH_WIELD:
                    _charaDat.SetWieldMode(mode);
                    lhButton.BackColor = SystemColors.ControlDark;
                    rhButton.BackColor = SystemColors.Control;
                    powerValueLabel.Location = new Point(powerValueLabel.Location.X, 32);
                    powerValueLabel.Visible = true;
                    powerValueLabel2.Visible = false;
                    damageValueLabel.Location = new Point(damageValueLabel.Location.X, 32);
                    damageValueLabel.Visible = true;
                    damageValueLabel2.Visible = false;
                    refreshCompoundedStats();
                    break;
                case CharacterData.DUAL_WIELD:
                    _charaDat.SetWieldMode(mode);
                    lhButton.BackColor = SystemColors.ControlDark;
                    rhButton.BackColor = SystemColors.ControlDark;
                    powerValueLabel.Location = new Point(powerValueLabel.Location.X, 22);
                    powerValueLabel2.Location = new Point(powerValueLabel2.Location.X, 46);
                    powerValueLabel.Visible = true;
                    powerValueLabel2.Visible = true;
                    damageValueLabel.Location = new Point(damageValueLabel.Location.X, 22);
                    damageValueLabel2.Location = new Point(damageValueLabel2.Location.X, 46);
                    damageValueLabel.Visible = true;
                    damageValueLabel2.Visible = true;
                    refreshCompoundedStats();
                    break;
            }
        }

        private void displayItemInfo(ItemData item) {
            infoTextBox.Text = item.getDescriptiveString();
        }

        private void displayTraitInfo(Trait t) {
            infoTextBox.Text = t.title + Environment.NewLine + t.type + " Trait" + Environment.NewLine +
                (t.cost > 0 ? "Cost: " + t.cost + Environment.NewLine : "") +
                Environment.NewLine + t.description;
        }

        private void displayTalentInfo(Talent t) {
            infoTextBox.Text = t.title + Environment.NewLine + t.type + " Talent" + (t.proficiency == "" ? "" : Environment.NewLine + "(" + t.proficiency + ")") + Environment.NewLine 
                + "Rank: " + t.rank + Environment.NewLine + Environment.NewLine + t.description;
        }

        private void displaySchoolInfo(School s) {
            infoTextBox.Text = s.title + Environment.NewLine + "Level: " + s.level + Environment.NewLine + Environment.NewLine + s.description;
        }

        private void displayAbilityInfo(Ability a) {
            int mpCost = StringFunctions.resolveCost(a.cost, "mp");
            int castTime;
            if(csMP.Value != 0) {
                castTime = (int)Math.Ceiling((double)mpCost / (double)csMP.Value);
                if(castTime == 0) castTime = 1;
            } else {
                if(mpCost == 0) {
                    castTime = 1;
                } else {
                    castTime = 1000000;
                }
            }
            infoTextBox.Text = a.title + Environment.NewLine +
                "Level " + a.level + " " + a.school + " " + a.type + " - " + a.vocation + Environment.NewLine +
                (mpCost > 0 ? "Cost: " + mpCost + " MP (" +(castTime >= 1000000 ? "Infinite Cast Time)" : castTime + " Action Cast Time)") + Environment.NewLine : "") +
                "Exhaustion: " + a.exhaustion + Environment.NewLine +
                "Range: " + a.range + Environment.NewLine +
                "Duration: " + a.duration + Environment.NewLine +
                "Acc. Check: " + (a.accCheck ? "Yes" : "No") + Environment.NewLine +
                Environment.NewLine + a.description;
        }

        private void displayGenericInfo(string s) {
            infoTextBox.Text = s;
        }
    }
}
