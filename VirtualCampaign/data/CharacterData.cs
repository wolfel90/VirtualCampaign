using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class CharacterData : CharacterTag {
        public const int RH_WIELD = 0, LH_WIELD = 1, DUAL_WIELD = 2;
        public enum Races { Darkan, Dehnamyn, Dwarf, Elf, Human, Illinsern, Daea, Lyca, Koahdu, Nyhma, Gnome, Viyr, Nautaia };
        public static string[] fieldsList;
        public static string[] classicFieldsList;
        public int functionalRace = (int)Races.Human;

        static CharacterData() {
            fieldsList = new string[] {
                "id", "name", "race", "gender", "height", "weight", "age", "birth", "hair", "eyes",
                "appearance", "biography", "languages", "modules", "notes_0", "notes_1", "notes_2", "notes_3", "home", "level", "strength",
                "agility", "intelligence", "fortitude", "charisma", "hp_current", "hp_max", "xp_current", "xp_next", "conduct", "morality",
                "fame", "infamy", "channel_adj", "stamina_adj", "exhaust", "traits", "talents", "schools", "vocations", "abilities", "aux_abilities", "inventory", "wallet",
                "ammo", "head", "torso", "legs", "feet", "hands", "lh", "rh", "wield_mode", "neck", "waist", "ring1", "ring2", "ring3", "ring4", "ring5", "ring6",
                "creator", "whitelist_protected", "whitelist"
            };
            classicFieldsList = new string[] {
                "char_id", "last_edit", "edit_time", "last_editor", "owner", "shared", "gm", "whitelist", "name", "family", "race", "age", "birth", "home", "gender", "height", "weight",
                "hair", "eye", "appear", "bio", "lvl", "modifiers", "pp", "ap", "str", "agi", "intel", "fort", "cha", "xp", "next", "bhp", "bep", "bmp", "chp", "cep", "cmp", "mhp", "mep", "mmp",
                "str0", "str1", "str2", "str3", "str4", "str5", "str6", "agi0", "agi1", "agi2", "agi3", "agi4", "agi5", "agi6", "intel0", "intel1","intel2","intel3","intel4","intel5","intel6",
                "fort0", "fort1","fort2","fort3","fort4","fort5", "cha0", "cha1","cha2","cha3","cha4","cha5",
                "conductval", "moralval", "fame", "infamy", "aff", "allervian", "florin", "drake", "denarius", "mark", "totalcoin", "lang", "head", "torso", "arms", "legs", "feet", "neck",
                "back", "waist", "ringl1", "ringl2", "ringl3", "ringr1", "ringr2", "ringr3", "ammo1", "ammo2", "ammo3", "portrait", "air", "darkness", "earth", "electricity", "fire", "water",
                "psych", "venom", "weakness", "righthand", "lefthand", "inv", "schools0", "schools1", "traits", "talents", "skills", "spells", "notes", "notes2", "notes3", "notes4", "notes5"
            };
        }

        private string _race = "";
        public string race { get { return _race; } set { setRace(value); } }
        public string gender = "";
        public string height = "";
        public string weight = "";
        public string age = "";
        public string birth = "";
        public string hair = "";
        public string eyes = "";
        public string appearance = "";
        public string biography = "";
        public string languages = "";
        public string notes0 = "";
        public string notes1 = "";
        public string notes2 = "";
        public string notes3 = "";
        public string home = "";
        public int level = 0;
        public int strength = 0;
        public int agility = 0;
        public int intelligence = 0;
        public int fortitude = 0;
        public int charisma = 0;
        public int hpCurrent = 0;
        public int hpMax = 0;
        public int xpCurrent = 0;
        public int xpNext = 0;
        public int conduct = 0;
        public int morality = 0;
        public int fame = 0;
        public int infamy = 0;
        public int channelingAdjust = 0;
        public int staminaAdjust = 0;
        public int exhaustion = 0;
        public List<Trait> traitsList { get; set; } = new List<Trait>();
        private string _traits = "";
        public string traits { get { return _traits; } set { setTraits(value); } }
        public List<Talent> talentsList { get; set; } = new List<Talent>();
        private string _talents = "";
        public string talents { get { return _talents; } set { setTalents(value); } }
        public List<School> schoolsList { get; set; } = new List<School>();
        private string _schools = "";
        public string schools { get { return _schools; } set { setSchools(value); } }
        public List<String> vocationsList { get; set; } = new List<String>();
        private string _vocations = "";
        public string vocations { get { return _vocations; } set { setVocations(value); } }
        public List<Ability> abilitiesList { get; set; } = new List<Ability>();
        private string _abilities = "";
        public string abilities { get { return _abilities; } set { setAbilities(value); } }
        public List<Ability> auxAbilitiesList { get; set; } = new List<Ability>();
        private string _auxAbilities = "";
        public string auxAbilities { get { return _auxAbilities; } set { setAuxAbilities(value); } }
        public List<String> modulesList { get; set; } = new List<String>();
        private string _modules = "";
        public string modules { get { return _modules; } set { SetModules(value); } }
        private string _items = "";
        public string items { get { return _items; } set { setItems(value); } }
        public string wallet { get; set; }
        public string ammo = "";
        public string head = "";
        public string torso = "";
        public string legs = "";
        public string feet = "";
        public string hands = "";
        public string lh = "";
        public string rh = "";
        private int _wield = RH_WIELD;
        public int wieldMode { get { return _wield; } set { SetWieldMode(value); } }
        public string neck = "";
        public string waist = "";
        public string ring1 = "";
        public string ring2 = "";
        public string ring3 = "";
        public string ring4 = "";
        public string ring5 = "";
        public string ring6 = "";

        public string[][] buildMySQLArray() {
            string[][] result = new string[2][] { new string[fieldsList.Length - 1], new string[fieldsList.Length - 1]}; // Ignore ID field

            int fieldIndex = 0;
            for(int i = 0; i < result[0].Length; ++i) {
                if(fieldIndex < fieldsList.Length) {
                    switch (fieldsList[fieldIndex]) {
                        case "id":
                            ++fieldIndex;
                            --i;
                            continue;
                        case "name":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = name;
                            break;
                        case "race":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = race;
                            break;
                        case "gender":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = gender;
                            break;
                        case "height":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = height;
                            break;
                        case "weight":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = weight;
                            break;
                        case "age":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = age;
                            break;
                        case "birth":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = birth;
                            break;
                        case "hair":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = hair;
                            break;
                        case "eyes":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = eyes;
                            break;
                        case "appearance":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = appearance;
                            break;
                        case "biography":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = biography;
                            break;
                        case "languages":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = languages;
                            break;
                        case "modules":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = modules;
                            break;
                        case "notes_0":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = notes0;
                            break;
                        case "notes_1":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = notes1;
                            break;
                        case "notes_2":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = notes2;
                            break;
                        case "notes_3":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = notes3;
                            break;
                        case "home":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = home;
                            break;
                        case "level":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = level.ToString();
                            break;
                        case "strength":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = strength.ToString();
                            break;
                        case "agility":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = agility.ToString();
                            break;
                        case "intelligence":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = intelligence.ToString();
                            break;
                        case "fortitude":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = fortitude.ToString();
                            break;
                        case "charisma":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = charisma.ToString();
                            break;
                        case "hp_current":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = hpCurrent.ToString();
                            break;
                        case "hp_max":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = hpMax.ToString();
                            break;
                        case "xp_current":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = xpCurrent.ToString();
                            break;
                        case "xp_next":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = xpNext.ToString();
                            break;
                        case "conduct":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = conduct.ToString();
                            break;
                        case "morality":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = morality.ToString();
                            break;
                        case "fame":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = fame.ToString();
                            break;
                        case "infamy":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = infamy.ToString();
                            break;
                        case "channel_adj":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = channelingAdjust.ToString();
                            break;
                        case "stamina_adj":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = staminaAdjust.ToString();
                            break;
                        case "exhaust":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = exhaustion.ToString();
                            break;
                        case "traits":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _traits;
                            break;
                        case "talents":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _talents;
                            break;
                        case "schools":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _schools;
                            break;
                        case "vocations":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _vocations;
                            break;
                        case "abilities":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _abilities;
                            break;
                        case "aux_abilities":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _auxAbilities;
                            break;
                        case "inventory":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = _items;
                            break;
                        case "wallet":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = wallet;
                            break;
                        case "ammo":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ammo;
                            break;
                        case "head":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = head;
                            break;
                        case "torso":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = torso;
                            break;
                        case "legs":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = legs;
                            break;
                        case "feet":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = feet;
                            break;
                        case "hands":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = hands;
                            break;
                        case "lh":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = lh;
                            break;
                        case "rh":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = rh;
                            break;
                        case "wield_mode":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = wieldMode.ToString();
                            break;
                        case "neck":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = neck;
                            break;
                        case "waist":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = waist;
                            break;
                        case "ring1":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ring1;
                            break;
                        case "ring2":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ring2;
                            break;
                        case "ring3":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ring3;
                            break;
                        case "ring4":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ring4;
                            break;
                        case "ring5":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ring5;
                            break;
                        case "ring6":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = ring6;
                            break;
                        case "creator":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = creator.ToString();
                            break;
                        case "whitelist_protected":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = useWhitelist ? "1" : "0";
                            break;
                        case "whitelist":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = whitelistString;
                            break;
                        default:
                            ++fieldIndex;
                            --i;
                            break;
                    }
                } else {
                    break;
                }
            }
            return result;
        }

        private void setRace(string r) {
            _race = r;
            string check = r.ToLower().Trim();
            switch (check) {
                case "daea":
                    functionalRace = (int)Races.Daea;
                    break;
                case "darkan":
                    functionalRace = (int)Races.Darkan;
                    break;
                case "dehnamyn":
                    functionalRace = (int)Races.Dehnamyn;
                    break;
                case "dwarf":
                    functionalRace = (int)Races.Dwarf;
                    break;
                case "elf":
                    functionalRace = (int)Races.Elf;
                    break;
                case "human":
                    functionalRace = (int)Races.Human;
                    break;
                case "illinsern":
                    functionalRace = (int)Races.Illinsern;
                    break;
                case "koahdu":
                    functionalRace = (int)Races.Koahdu;
                    break;
                case "lyca":
                    functionalRace = (int)Races.Lyca;
                    break;
                case "nautaia":
                    functionalRace = (int)Races.Nautaia;
                    break;
                case "viyr":
                    functionalRace = (int)Races.Viyr;
                    break;
                default:
                    functionalRace = (int)Races.Human;
                    break;
            }
        }

        public void setTraits(string str) {
            _traits = str;
            traitsList.Clear();
            string[] t = StringFunctions.ReadTagMulti(str, "trait");
            foreach(string s in t) {
                Trait newTrait = new Trait(s);
                traitsList.Add(newTrait);
            }
        }

        public void setTalents(string str) {
            _talents = str;
            talentsList.Clear();
            string[] t = StringFunctions.ReadTagMulti(str, "talent");
            foreach (string s in t) {
                Talent newTalent = new Talent(s, classic);
                talentsList.Add(newTalent);
            }
        }

        public void setSchools(string str) {
            _schools = str;
            schoolsList.Clear();
            string[] s = StringFunctions.ReadTagMulti(str, "school");
            foreach(string sch in s) {
                School newSchool = new School(sch);
                schoolsList.Add(newSchool);
            }
        }

        public int GetSchoolLevel(string sch) {
            foreach(School s in schoolsList) {
                if(s.title.ToLower() == sch.ToLower()) {
                    return s.level;
                }
            }
            return 0;
        }

        public void setVocations(string str) {
            _vocations = str;
            vocationsList.Clear();
            string[] s = StringFunctions.ReadTagMulti(str, "vocation");
            foreach(string voc in s) {
                vocationsList.Add(voc);
            }
        }

        public void setAbilities(string str) {
            _abilities = str;
            abilitiesList.Clear();
            string[] s = StringFunctions.ReadTagMulti(str, "ability");
            foreach(string abi in s) {
                Ability newAbility = new Ability(abi);
                abilitiesList.Add(newAbility);
            }
        }

        public void setAuxAbilities(string str) {
            _auxAbilities = str;
            auxAbilitiesList.Clear();
            string[] s = StringFunctions.ReadTagMulti(str, "ability");
            foreach (string abi in s) {
                Ability newAbility = new Ability(abi);
                auxAbilitiesList.Add(newAbility);
            }
        }

        public void SetModules(string str) {
            _modules = str;
            modulesList.Clear();
            string[] s = str.Split('|');
            foreach(string mod in s) {
                modulesList.Add(mod);
            }
        }

        public void AddModule(string str) {
            if(!modulesList.Contains(str)) {
                modulesList.Add(str);
                _modules = "";
                foreach(string s in modulesList) {
                    if (!_modules.Equals("")) _modules += "|";
                    _modules += s;
                }
            }
        }

        public void RemoveModule(string str) {
            if(modulesList.Contains(str)) {
                modulesList.Remove(str);
                foreach (string s in modulesList) {
                    if (!_modules.Equals("")) _modules += "|";
                    _modules += s;
                }
            }
        }

        public void SetWieldMode(int mode) {
            _wield = mode;
        }

        public void setItems(string str) {
            _items = str;
        }

        public static string getConductString(int val) {
            if (val < -40) {
                return "Chaotic";
            } else if (val <= 40) {
                return "Neutral";
            } else {
                return "Lawful";
            }
        }

        public static string getMoralityString(int val) {
            if (val < -40) {
                return "Evil";
            } else if (val <= 40) {
                return "Neutral";
            } else {
                return "Good";
            }
        }

        public static string getAlignmentString(int con, int mor) {
            if (con < -40) {
                if (mor < -40) {
                    return "Calamitous";
                } else if (mor <= 40) {
                    return "Capricious";
                } else {
                    return "Vigilant";
                }
            } else if (con <= 40) {
                if (mor < -40) {
                    return "Corrupt";
                } else if (mor <= 40) {
                    return "Neutral";
                } else {
                    return "Honorable";
                }
            } else {
                if (mor < -40) {
                    return "Nefarious";
                } else if (mor <= 40) {
                    return "Eminent";
                } else {
                    return "Heroic";
                }
            }
        }

        public static string getFameString(int val) {
            if (val < 50) {
                return "Unknown";
            } else if (val < 200) {
                return "Familiar";
            } else if (val < 400) {
                return "Recognized";
            } else if (val < 600) {
                return "Distinguished";
            } else if (val < 800) {
                return "Famous";
            } else if (val < 900) {
                return "Illustrious";
            } else {
                return "Legendary";
            }
        }

        public static string getInfamyString(int val) {
            if (val < 50) {
                return "Innocent";
            } else if (val < 200) {
                return "Deviant";
            } else if (val < 400) {
                return "Disreputable";
            } else if (val < 600) {
                return "Wanted";
            } else if (val < 800) {
                return "Infamous";
            } else if (val < 900) {
                return "Notorious";
            } else {
                return "Diabolical";
            }
        }

        public static string getReputationString(int fam, int infam) {
            int totalRep = fam + infam;
            double repRatio;
            if(totalRep == 0) {
                repRatio = 0;
            } else {
                repRatio = (double)(fam - infam) / (double)totalRep;
            }

            if(totalRep < 750) {
                if(repRatio < -0.5) {
                    return "Criminal";
                } else if(repRatio <= 0.5) {
                    return "Citizen";
                } else {
                    return "Patron";
                }
            } else {
                if (repRatio < -0.5) {
                    return "Villain";
                } else if (repRatio <= 0.5) {
                    return "Celebrity";
                } else {
                    return "Hero";
                }
            }
        }
    }
}
