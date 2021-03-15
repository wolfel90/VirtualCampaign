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
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class CharacterGenerationMenu : UserControl, SQLListener {
        private CharacterData data;
        private Random rand;
        private SQLParser parser;
        private List<Trait> traitBank;
        private List<Talent> talentBank;
        private List<Ability> abilityBank;
        private List<School> schoolBank;
        private List<ItemData> accessoryBank;
        private List<ItemData> armorBank;
        private List<ItemData> componentBank;
        private List<ItemData> weaponBank;
        private List<string> characterTags;
        private List<string> modules;
        private ItemBuilder builder;
        private NameGeneration nameGen;

        public CharacterGenerationMenu() {
            InitializeComponent();
            data = null;
            rand = new Random();
            parser = new SQLParser();
            traitBank = new List<Trait>();
            talentBank = new List<Talent>();
            abilityBank = new List<Ability>();
            schoolBank = new List<School>();
            accessoryBank = new List<ItemData>();
            armorBank = new List<ItemData>();
            componentBank = new List<ItemData>();
            weaponBank = new List<ItemData>();
            characterTags = new List<string>();
            modules = new List<string>();
            builder = new ItemBuilder();
            nameGen = new NameGeneration();

            DownloadBankData();
        }

        private void DownloadBankData() {
            SQLManager.runQuery("vc_articles",
                new string[] { "id", "trait", "talent", "ability", "school", "title", "description", "properties", "system_tags", "whitelist_protected", "whitelist" },
                " `trait` = '1' OR `talent` = '1' OR `ability` = '1' OR `school` = '1'",
                this, 
                SQLManager.LOAD_ARTICLE
                );
            SQLManager.runQuery("vc_item_bank", ItemData.fieldsList, null, this, SQLManager.LOAD_BANK_ITEM);
        }

        public void HandleData(int action, Dictionary<string, object> d) {
            if (d != null) {
                if(action == SQLManager.LOAD_ARTICLE) {
                    if ((bool)d["trait"]) {
                        traitBank.Add(parser.parseTrait(d));
                    } else if ((bool)d["talent"]) {
                        talentBank.Add(parser.parseTalent(d));
                    } else if ((bool)d["ability"]) {
                        abilityBank.Add(parser.parseAbility(d));
                    } else if ((bool)d["school"]) {
                        schoolBank.Add(parser.parseSchool(d));
                    }
                } else if(action == SQLManager.LOAD_BANK_ITEM) {
                    ItemData item = parser.parseBankItem(d);
                    switch(item.type) {
                        case ItemData.ARMOR_TYPE:
                            armorBank.Add(item);
                            break;
                        case ItemData.ACCESSORY_TYPE:
                            accessoryBank.Add(item);
                            break;
                        case ItemData.COMPONENT_TYPE:
                            componentBank.Add(item);
                            break;
                        case ItemData.WEAPON_TYPE:
                            weaponBank.Add(item);
                            break;
                    }
                }
            }
        }

        private void generateNameCheck_CheckedChanged(object sender, EventArgs e) {
            //nameField.Enabled = !generateNameCheck.Checked;
        }

        private void lvlMinSlider_Scroll(object sender, EventArgs e) {
            if(lvlMinSlider.Value > lvlMaxSlider.Value) {
                lvlMaxSlider.Value = lvlMinSlider.Value;
            }
            lvlRangeLabel.Text = lvlMinSlider.Value.ToString() + " - " + lvlMaxSlider.Value.ToString();
        }

        private void lvlMaxSlider_Scroll(object sender, EventArgs e) {
            if(lvlMaxSlider.Value < lvlMinSlider.Value) {
                lvlMinSlider.Value = lvlMaxSlider.Value;
            }
            lvlRangeLabel.Text = lvlMinSlider.Value.ToString() + " - " + lvlMaxSlider.Value.ToString();
        }

        private void strMaxSlider_Scroll(object sender, EventArgs e) {
            strRangeLabel.Text = "0 - " + strMaxSlider.Value.ToString();
        }

        private void agiMaxSlider_Scroll(object sender, EventArgs e) {
            agiRangeLabel.Text = "0 - " + agiMaxSlider.Value.ToString();
        }

        private void intMaxSlider_Scroll(object sender, EventArgs e) {
            intRangeLabel.Text = "0 - " + intMaxSlider.Value.ToString();
        }

        private void forMaxSlider_Scroll(object sender, EventArgs e) {
            forRangeLabel.Text = "0 - " + forMaxSlider.Value.ToString();
        }

        private void chaMaxSlider_Scroll(object sender, EventArgs e) {
            chaRangeLabel.Text = "0 - " + chaMaxSlider.Value.ToString();
        }

        private void alignMinSlider_Scroll(object sender, EventArgs e) {
            if (moralMinSlider.Value > moralMaxSlider.Value) {
                moralMaxSlider.Value = moralMinSlider.Value;
            }
            moralRangeLabel.Text = moralMinSlider.Value.ToString() + " - " + moralMaxSlider.Value.ToString();
        }

        private void alignMaxSlider_Scroll(object sender, EventArgs e) {
            if (moralMaxSlider.Value < moralMinSlider.Value) {
                moralMinSlider.Value = moralMaxSlider.Value;
            }
            moralRangeLabel.Text = moralMinSlider.Value.ToString() + " - " + moralMaxSlider.Value.ToString();
        }

        private void conductMinSlider_Scroll(object sender, EventArgs e) {
            if (conductMinSlider.Value > conductMaxSlider.Value) {
                conductMaxSlider.Value = conductMinSlider.Value;
            }
            conductRangeLabel.Text = conductMinSlider.Value.ToString() + " - " + conductMaxSlider.Value.ToString();
        }

        private void conductMaxSlider_Scroll(object sender, EventArgs e) {
            if (conductMaxSlider.Value < conductMinSlider.Value) {
                conductMinSlider.Value = conductMaxSlider.Value;
            }
            conductRangeLabel.Text = conductMinSlider.Value.ToString() + " - " + conductMaxSlider.Value.ToString();
        }

        private void fameMinSlider_Scroll(object sender, EventArgs e) {
            if (fameMinSlider.Value > fameMaxSlider.Value) {
                fameMaxSlider.Value = fameMinSlider.Value;
            }
            fameRangeLabel.Text = fameMinSlider.Value.ToString() + " - " + fameMaxSlider.Value.ToString();
        }

        private void fameMaxSlider_Scroll(object sender, EventArgs e) {
            if (fameMaxSlider.Value < fameMinSlider.Value) {
                fameMinSlider.Value = fameMaxSlider.Value;
            }
            fameRangeLabel.Text = fameMinSlider.Value.ToString() + " - " + fameMaxSlider.Value.ToString();
        }

        private void infamyMinSlider_Scroll(object sender, EventArgs e) {
            if (infamyMinSlider.Value > infamyMaxSlider.Value) {
                infamyMaxSlider.Value = infamyMinSlider.Value;
            }
            infamyRangeLabel.Text = infamyMinSlider.Value.ToString() + " - " + infamyMaxSlider.Value.ToString();
        }

        private void infamyMaxSlider_Scroll(object sender, EventArgs e) {
            if (infamyMaxSlider.Value < infamyMinSlider.Value) {
                infamyMinSlider.Value = infamyMaxSlider.Value;
            }
            infamyRangeLabel.Text = infamyMinSlider.Value.ToString() + " - " + infamyMaxSlider.Value.ToString();
        }

        private void armorSlider_Scroll(object sender, EventArgs e) {
            switch(armorSlider.Value) {
                case 0:
                    armorValueLabel.Text = "No Armor";
                    break;
                case 1:
                    armorValueLabel.Text = "Clothing";
                    break;
                case 2:
                    armorValueLabel.Text = "Low-Quality Armor";
                    break;
                case 3:
                    armorValueLabel.Text = "Best Wearable";
                    break;
                case 4:
                    armorValueLabel.Text = "Any Armor";
                    break;
            }
        }

        private void weaponSlider_Scroll(object sender, EventArgs e) {
            switch (weaponSlider.Value) {
                case 0:
                    weaponValueLabel.Text = "No Weapon";
                    break;
                case 1:
                    weaponValueLabel.Text = "Rudimentary Weapon";
                    break;
                case 2:
                    weaponValueLabel.Text = "Best Usable";
                    break;
                case 3:
                    weaponValueLabel.Text = "Any Weapon";
                    break;
            }
        }

        private void modulesCheckList_ItemCheck(object sender, ItemCheckEventArgs e) {
            List<string> selectedModules = new List<string>();
            List<string> selectedRaces = new List<string>();
            string[] ayaseyeRaces = new string[] { "Darkan", "Dehnamyn", "Dwarf", "Elf", "Human", "Illinsern" };
            string[] eigolynRaces = new string[] { "Daea", "Dwarf", "Elf", "Human", "Koahdu", "Lyca", "Nautaia", "Nyhma", "Viyr" };

            for (int i = 0; i < modulesCheckList.Items.Count; ++i) {
                if (i == e.Index) {
                    if (e.NewValue.Equals(CheckState.Checked)) {
                        selectedModules.Add(modulesCheckList.Items[i].ToString());
                    }
                } else {
                    if (modulesCheckList.CheckedIndices.Contains(i)) {
                        selectedModules.Add(modulesCheckList.Items[i].ToString());
                    }
                }
            }
            foreach (object o in raceCheckList.CheckedItems) {
                selectedRaces.Add(o.ToString());
            }
            raceCheckList.Items.Clear();
            raceCheckList.Items.Add("Human", selectedRaces.Contains("Human"));
            if (selectedModules.Contains("Ayaseye")) {
                foreach (string s in ayaseyeRaces) {
                    if (!raceCheckList.Items.Contains(s)) raceCheckList.Items.Add(s, selectedRaces.Contains(s));
                }
            }
            if (selectedModules.Contains("Eigolyn")) {
                foreach (string s in eigolynRaces) {
                    if (!raceCheckList.Items.Contains(s)) raceCheckList.Items.Add(s, selectedRaces.Contains(s));
                }
            }
        }
        
        private void generateButton_Click(object sender, EventArgs e) {
            data = new CharacterData();
            
            characterTags.Clear();
            modules.Clear();
            foreach(object o in modulesCheckList.CheckedItems) {
                modules.Add(o.ToString());
                data.AddModule(o.ToString());
            }
            data.level = rand.Next(lvlMinSlider.Value, lvlMaxSlider.Value + 1);
            data.creator = UserManager.currentUser.userID;

            if(maleRadioButton.Checked) {
                data.gender = "Male";
            } else if(femaleRadioButton.Checked) {
                data.gender = "Female";
            } else if(eitherRadioButton.Checked) {
                switch(rand.Next(0, 2)) {
                    case 0:
                        data.gender = "Male";
                        break;
                    case 1:
                        data.gender = "Female";
                        break;
                }
            }
            if(!String.IsNullOrWhiteSpace(data.gender)) characterTags.Add(data.gender.ToUpper());
            
            GenerateRace();
            GenerateBasics();
            GenerateProficiencies();
            GenerateAlignmentReputation();
            GenerateTraits();
            GenerateTalents();
            GenerateSchools();
            GenerateAbilities();
            GenerateArmor();
            GenerateWeapon();
            CharacterSheet cs = PageHandler.RequestCharacterSheet(data);
            cs.editable = true;
            cs.suspendSave = true;
        }

        private void GenerateRace() {
            if(raceCheckList.CheckedIndices.Count == 0) {
                data.race = "Human";
            } else {
                int selection = rand.Next(0, raceCheckList.CheckedIndices.Count);
                data.race = raceCheckList.CheckedItems[selection].ToString();
            }
            characterTags.Add(data.race.ToUpper());
        }

        private void GenerateBasics() {
            // Generate Name
            data.name = nameField.Text;
        }

        private string JumbleName(string input) {
            string output = input;
            string vowels = "aeiouy";
            string consonants = "bcdfghjklmnpqrstvwxz";
            char replacement = ' ';
            int selection;
            bool capital = true;
            for (int i = 0; i < output.Length; ++i) {
                char c = output[i];
                if (vowels.Contains(c)) {
                    selection = rand.Next(0, vowels.Length);
                    replacement = vowels[selection];
                    output = output.Substring(0, i) + (capital ? Char.ToUpper(replacement) : replacement) + output.Substring(i + 1);
                    capital = false;
                } else if (consonants.Contains(c)) {
                    selection = rand.Next(0, consonants.Length);
                    replacement = consonants[selection];
                    output = output.Substring(0, i) + (capital ? Char.ToUpper(replacement) : replacement) + output.Substring(i + 1);
                    capital = false;
                } else {
                    replacement = ' ';
                    capital = true;
                }
            }
            nameTest.Text = output;
            return output;
        }

        private void GenerateProficiencies() {
            if (data == null) data = new CharacterData();
            int PP = 50;
            if(data.level <= 20) {
                PP += data.level / 2;
            } else {
                PP += 10 + data.level - 20;
            }
            
            List<string> options = new List<string>() { "STR", "AGI", "INT", "FOR", "CHA" };
            Dictionary<string, int> values = new Dictionary<string, int>() { { "STR", 0 }, { "AGI", 0 }, { "INT", 0 }, { "FOR", 0 }, { "CHA", 0 } };
            int choice;
            for(int i = 0; i < PP; ++i) {
                if (values["STR"] >= strMaxSlider.Value) options.Remove("STR");
                if (values["AGI"] >= agiMaxSlider.Value) options.Remove("AGI");
                if (values["INT"] >= intMaxSlider.Value) options.Remove("INT");
                if (values["FOR"] >= forMaxSlider.Value) options.Remove("FOR");
                if (values["CHA"] >= chaMaxSlider.Value) options.Remove("CHA");
                if (options.Count == 0) break;
                choice = rand.Next(0, options.Count);
                values[options[choice]]++;
            }

            data.strength = values["STR"];
            data.agility = values["AGI"];
            data.intelligence = values["INT"];
            data.fortitude = values["FOR"];
            data.charisma = values["CHA"];

            if (data.strength > PP / 2) characterTags.Add("STR_HIGH");
            if (data.agility > PP / 2) characterTags.Add("AGI_HIGH");
            if (data.intelligence > PP / 2) characterTags.Add("INT_HIGH");
            if (data.fortitude > PP / 2) characterTags.Add("FOR_HIGH");
            if (data.charisma > PP / 2) characterTags.Add("CHA_HIGH");

            if (data.strength < PP / 10) characterTags.Add("STR_LOW");
            if (data.agility < PP / 10) characterTags.Add("AGI_LOW");
            if (data.intelligence < PP / 10) characterTags.Add("INT_LOW");
            if (data.fortitude < PP / 10) characterTags.Add("FOR_LOW");
            if (data.charisma < PP / 10) characterTags.Add("CHA_LOW");
        }
        
        private void GenerateAlignmentReputation() {
            data.morality = rand.Next(moralMinSlider.Value, moralMaxSlider.Value + 1);
            data.conduct = rand.Next(conductMinSlider.Value, conductMaxSlider.Value + 1);
            data.fame = rand.Next(fameMinSlider.Value, fameMaxSlider.Value + 1);
            data.infamy = rand.Next(infamyMinSlider.Value, infamyMaxSlider.Value + 1);
        }

        private void GenerateTraits() {
            int tp = 5 + (data.level / 10);
            Dictionary<Trait, int> traitPool = new Dictionary<Trait, int>();
            List<Trait> negativeTraits = new List<Trait>();
            int value;
            bool valid = false;
            foreach(Trait t in traitBank) {
                valid = false;
                if(t.IsModularized()) {
                    foreach(string s in modules) {
                        if(t.IsInModule(s)) {
                            valid = true;
                            break;
                        }
                    }
                } else {
                    valid = true;
                }
                if(valid) {
                    if (t.type == "Learned") {
                        if (t.cost < 0) {
                            negativeTraits.Add(t);
                        } else {
                            value = 1;
                            foreach (string s in t.GetTags()) {
                                if (characterTags.Contains(s)) {
                                    value += 1;
                                }
                            }
                            traitPool.Add(t, value);
                        }
                    } else if(t.type == "Biological") {
                        if(t.HasTag("BIO_" + data.race.ToUpper())) {
                            t.active = true;
                            data.traitsList.Add(t);
                        }
                    } else if(t.type == "Heritage") {
                        if(t.HasTag("HER_" + data.race.ToUpper())) {
                            t.active = true;
                            data.traitsList.Add(t);
                        }
                    }
                }
            }
            
            // Negative traits
            int chance = 10;
            while(rand.Next(0, 100) < chance && negativeTraits.Count > 0) {
                int choice = rand.Next(0, negativeTraits.Count);
                tp -= negativeTraits[choice].cost;
                negativeTraits[choice].active = true;
                data.traitsList.Add(negativeTraits[choice]);
                negativeTraits.RemoveAt(choice);
                chance /= 2;
            }

            // Non-negative traits
            int index, total;
            while (tp > 0 && traitPool.Count > 0) {
                total = 0;
                for(int i = 0; i < traitPool.Keys.Count; ++i) {
                    if(traitPool.Keys.ElementAt(i).cost > tp) {
                        traitPool.Remove(traitPool.Keys.ElementAt(i));
                        i--;
                    } else {
                        total += traitPool[traitPool.Keys.ElementAt(i)];
                    }
                }
                if (total == 0) break; // No more viable options available, exit loop

                index = rand.Next(1, total + 1);
                for(int i = 0; i < traitPool.Keys.Count; ++i) {
                    index -= traitPool[traitPool.Keys.ElementAt(i)];
                    if(index <= 0) {
                        tp -= traitPool.Keys.ElementAt(i).cost;
                        traitPool.Keys.ElementAt(i).active = true;
                        data.traitsList.Add(traitPool.Keys.ElementAt(i));
                        traitPool.Remove(traitPool.Keys.ElementAt(i));
                        break;
                    }
                }
            }
        }

        private void GenerateTalents() {
            int tp = 50 + (data.level * 10);
            Dictionary<Talent, int> talentPool = new Dictionary<Talent, int>();
            Dictionary<Talent, int> selectedTalents = new Dictionary<Talent, int>();
            int value = 0, cost = 0, total, index;
            bool valid = false;
            foreach(Talent t in talentBank) {
                valid = false;
                if(t.IsModularized()) {
                     foreach(string s in modules) {
                        if(t.IsInModule(s)) {
                            valid = true;
                        }
                    }
                } else {
                    valid = true;
                }

                if(valid) {
                    value = 1;
                    foreach(string tag in t.GetTags()) {
                        if(characterTags.Contains(tag)) {
                            value += 1;
                        }
                    }
                    talentPool.Add(t, value);
                }
            }

            while(tp > 0 && talentPool.Keys.Count > 0) {
                total = 0;
                for(int i = 0; i < talentPool.Keys.Count; ++i) {
                    if(selectedTalents.Keys.Contains(talentPool.Keys.ElementAt(i))) {
                        cost = (selectedTalents[talentPool.Keys.ElementAt(i)] < 10) ? selectedTalents[talentPool.Keys.ElementAt(i)] + 1 : 10;
                        if (cost > tp) {
                            talentPool.Remove(talentPool.Keys.ElementAt(i));
                            i--;
                        } else {
                            total += talentPool[talentPool.Keys.ElementAt(i)];
                        }
                    } else {
                        cost = 1;
                        if (cost > tp) {
                            talentPool.Remove(talentPool.Keys.ElementAt(i));
                            i--;
                        } else {
                            total += talentPool[talentPool.Keys.ElementAt(i)];
                        }
                    }
                }
                if (total == 0) break; // No more viable options available, exit loop

                index = rand.Next(1, total + 1);
                for(int i = 0; i < talentPool.Keys.Count; ++i) {
                    index -= talentPool[talentPool.Keys.ElementAt(i)];
                    if(index <= 0) {
                        if(!selectedTalents.Keys.Contains(talentPool.Keys.ElementAt(i))) {
                            selectedTalents.Add(talentPool.Keys.ElementAt(i), 0);
                        }
                        tp -= selectedTalents[talentPool.Keys.ElementAt(i)] < 10 ? selectedTalents[talentPool.Keys.ElementAt(i)] + 1 : 10;
                        selectedTalents[talentPool.Keys.ElementAt(i)]++;
                        if(selectedTalents[talentPool.Keys.ElementAt(i)] >= 10) {
                            talentPool.Remove(talentPool.Keys.ElementAt(i));
                        } else {
                            talentPool[talentPool.Keys.ElementAt(i)] *= 3; // Increase chance of selecting a talent that has already been selected
                        }
                        break;
                    }
                }

                String talentString = "";
                foreach(Talent t in selectedTalents.Keys) {
                    t.rank = selectedTalents[t];
                    talentString += t.generateString();
                }
                data.talents = talentString;
            }
        }

        private void GenerateSchools() {
            int sp = 15;
            if(data.level <= 50) {
                if (data.level > 0) {
                    sp += data.level;
                }
                if (data.level > 10) {
                    sp += data.level - 10;
                }
                if (data.level > 20) {
                    sp += data.level - 20;
                }
                if (data.level > 30) {
                    sp += data.level - 30;
                }
                if (data.level > 40) {
                    sp += data.level - 40;
                }
            } else if(data.level > 50) {
                sp = 165 + (data.level - 50) * 5;
            }
            
            Dictionary<School, int> schoolPool = new Dictionary<School, int>();
            Dictionary<School, int> selectedSchools = new Dictionary<School, int>();
            int value = 0, cost = 0, total, index;
            bool valid = false;
            foreach (School s in schoolBank) {
                valid = false;
                if (s.IsModularized()) {
                    foreach (string m in modules) {
                        if (s.IsInModule(m)) {
                            valid = true;
                        }
                    }
                } else {
                    valid = true;
                }

                if (valid) {
                    value = 1;
                    foreach (string tag in s.GetTags()) {
                        if (characterTags.Contains(tag)) {
                            value += 1;
                        }
                    }
                    schoolPool.Add(s, value);
                }
            }

            while (sp > 0 && schoolPool.Keys.Count > 0) {
                total = 0;
                for (int i = 0; i < schoolPool.Keys.Count; ++i) {
                    if (selectedSchools.Keys.Contains(schoolPool.Keys.ElementAt(i))) {
                        cost = (selectedSchools[schoolPool.Keys.ElementAt(i)] < 10) ? selectedSchools[schoolPool.Keys.ElementAt(i)] + 1 : 10;
                        if (cost > sp) {
                            schoolPool.Remove(schoolPool.Keys.ElementAt(i));
                            i--;
                        } else {
                            total += schoolPool[schoolPool.Keys.ElementAt(i)];
                        }
                    } else {
                        cost = 1;
                        if (cost > sp) {
                            schoolPool.Remove(schoolPool.Keys.ElementAt(i));
                            i--;
                        } else {
                            total += schoolPool[schoolPool.Keys.ElementAt(i)];
                        }
                    }
                }
                if (total == 0) break; // No more viable options available, exit loop

                index = rand.Next(1, total + 1);
                for (int i = 0; i < schoolPool.Keys.Count; ++i) {
                    index -= schoolPool[schoolPool.Keys.ElementAt(i)];
                    if (index <= 0) {
                        if (!selectedSchools.Keys.Contains(schoolPool.Keys.ElementAt(i))) {
                            selectedSchools.Add(schoolPool.Keys.ElementAt(i), 0);
                        }
                        sp -= selectedSchools[schoolPool.Keys.ElementAt(i)] < 10 ? selectedSchools[schoolPool.Keys.ElementAt(i)] + 1 : 10;
                        selectedSchools[schoolPool.Keys.ElementAt(i)]++;
                        if (selectedSchools[schoolPool.Keys.ElementAt(i)] >= 10) {
                            schoolPool.Remove(schoolPool.Keys.ElementAt(i));
                        } else {
                            schoolPool[schoolPool.Keys.ElementAt(i)] *= 3; // Increase chance of selecting a school that has already been selected
                        }
                        break;
                    }
                }

                String schoolString = "";
                foreach (School s in selectedSchools.Keys) {
                    s.level = selectedSchools[s];
                    schoolString += s.generateString();
                }
                data.schools = schoolString;
            }
        }

        private void GenerateAbilities() {
            List<Ability> abilityPool = new List<Ability>();
            List<Ability> selectedAbilities = new List<Ability>();

            foreach(Ability a in abilityBank) {
                if(a.vocation.ToLower() == "core" || a.vocation == "") {
                    if(a.level <= data.GetSchoolLevel(a.school)) {
                        abilityPool.Add(a);
                    }
                } else {
                    continue;
                }
            }

            int total = 0, index;
            while(abilityPool.Count > 0 && selectedAbilities.Count < 30) {
                total = 0;
                foreach(Ability a in abilityPool) {
                    total += a.level;
                }
                if (total == 0) break;

                index = rand.Next(1, total + 1);
                for(int i = 0; i < abilityPool.Count; ++i) {
                    index -= abilityPool.ElementAt(i).level;
                    if(index <= 0) {
                        selectedAbilities.Add(abilityPool.ElementAt(i));
                        abilityPool.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            if(selectedAbilities.Count > 0) {
                string abilityString = "";
                string auxString = "";

                for(int i = 0; i < selectedAbilities.Count; ++i) {
                    if(i < 10) {
                        abilityString += selectedAbilities.ElementAt(i).generateString();
                    } else {
                        auxString += selectedAbilities.ElementAt(i).generateString();
                    }
                }
                data.setAbilities(abilityString);
                data.setAuxAbilities(auxString);
            }
        }

        private void GenerateArmor() {
            if(armorSlider.Value > 0) {
                Dictionary<ItemData, int> armorPool = new Dictionary<ItemData, int>();
                Dictionary<ItemData, int> componentPool = new Dictionary<ItemData, int>();
                ItemData armorSelection = null;
                ItemData componentSelection = null;

                int value, total = 0, selection;
                foreach (ItemData it in armorBank) {
                    value = 0;
                    switch (armorSlider.Value) {
                        case 1:
                            if(it.title == "Shirt") {
                                value += 1;
                            }
                            break;
                        default:
                            switch(it.longType) {
                                case "Light":
                                    if(characterTags.Contains("INT_HIGH")) {
                                        value += 10;
                                    } else {
                                        value += 1;
                                    }
                                    break;
                                case "Medium":
                                    if (characterTags.Contains("AGI_HIGH")) {
                                        value += 10;
                                    } else {
                                        value += 1;
                                    }
                                    break;
                                case "Heavy":
                                    if (characterTags.Contains("STR_HIGH")) {
                                        value += 10;
                                    } else {
                                        value += 1;
                                    }
                                    break;
                                default:
                                    value += 1;
                                    break;
                            }
                            break;
                    }
                    if(value > 0) {
                        armorPool.Add(it, value);
                        total += value;
                    }
                }

                if (total <= 0) return;
                
                selection = rand.Next(1, total + 1);
                for (int i = 0; i < armorPool.Count; ++i) {
                    selection -= armorPool[armorPool.Keys.ElementAt(i)];
                    if(selection <= 0) {
                        armorSelection = armorPool.Keys.ElementAt(i);
                        break;
                    }
                }

                if (armorSelection == null) return;
                
                int tierMin = 0, tierMax = 1;
                total = 0;
                switch(armorSlider.Value) {
                    case 3:
                        tierMin = (data.level / 15) + 1;
                        if (tierMin < 1) tierMin = 1;
                        if (tierMin > 4) tierMin = 4;
                        tierMax = tierMin;
                        break;
                    case 4:
                        tierMin = 0;
                        tierMax = 4;
                        break;
                }
                foreach (ItemData it in componentBank) {
                    if(armorSelection.materials.ToLower().Contains(it.longType.ToLower())) {
                        if(it.tier >= tierMin && it.tier <= tierMax) {
                            componentPool.Add(it, it.tier * it.tier);
                            total += it.tier * it.tier;
                        }
                    }
                }

                if (componentPool.Count == 0) return;
                
                selection = rand.Next(1, total + 1);
                for(int i = 0; i < componentPool.Count; ++i) {
                    selection -= componentPool[componentPool.Keys.ElementAt(i)];
                    if(selection <= 0) {
                        componentSelection = componentPool.Keys.ElementAt(i);
                        break;
                    }
                }
                
                if (armorSelection != null && componentSelection != null) {
                    builder.clear();
                    builder.setBaseItem(armorSelection);
                    builder.addComponent(componentSelection);
                    if(builder.resultItem != null) {
                        data.torso = builder.resultItem.generateString();
                    }
                }
            }
        }

        private void GenerateWeapon() {

        }

        private void generateNameButton_Click(object sender, EventArgs e) {
            string name = "";
            if(phoneticNameCheck.Checked) {
                name = nameGen.GenerateName() + " " + nameGen.GenerateName();
            } else {
                List<string> firstNames = new List<string>();
                List<string> lastNames = new List<string>();
                string line;
                if (femaleRadioButton.Checked || eitherRadioButton.Checked) {
                    using (System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.First_Names_M)) {
                        while ((line = reader.ReadLine()) != null) {
                            firstNames.Add(line);
                        }
                    }
                }
                if (maleRadioButton.Checked || eitherRadioButton.Checked) {
                    using (System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.First_Names_F)) {
                        while ((line = reader.ReadLine()) != null) {
                            firstNames.Add(line);
                        }
                    }
                }
                using (System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.Last_Names)) {
                    while ((line = reader.ReadLine()) != null) {
                        lastNames.Add(line);
                    }
                }

                if (firstNames.Count > 0) {
                    name += firstNames[rand.Next(0, firstNames.Count)];
                }
                name += " ";
                if (lastNames.Count > 0) {
                    name += lastNames[rand.Next(0, lastNames.Count)];
                }
            }
            
            nameField.Text = name;
        }
    }
}
