using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;
using System.IO;

namespace VirtualCampaign.data {
    public class ItemData {
        public const int GENERIC_TYPE = 0, WEAPON_TYPE = 1, ARMOR_TYPE = 2, ACCESSORY_TYPE = 3, CONSUMABLE_TYPE = 4, AMMUNITION_TYPE = 5, COMPONENT_TYPE = 6;
        public static string[] fieldsList;
        public static Dictionary<string, int> legacyTypes;
        public static List<string> defaultBGOptions;

        private Dictionary<string, int> modifierLookup;
        public long netID { get; set; } = -1;
        public long localID { get; set; } = -1;
        public string title { get; set; }
        public string briefTitle { get; set; }
        public string wlTitle { get { return getWhitelistedTitle(); } }
        public int type;
        public string longType { get; set; }
        public string modProf { get; set; }
        public int weight { get; set; }
        public string materials { get; set; }
        public long value { get; set; }
        public string dmg { get; set; }
        public string mdmg { get; set; }
        public int pow { get; set; }
        public int mpow { get; set; }
        public int rng { get; set; }
        public int mrng { get; set; }
        public string unitSingle { get; set; }
        public string unitPlural { get; set; }
        public int def { get; set; }
        public int mdef { get; set; }
        public int block { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public string briefDescription { get; set; }
        public string mods { get { return generateModifierString(); } set { setModifiers(value); } }
        public bool stackable { get; set; }
        public bool equipped { get; set; }
        public bool starred { get; set; }
        public bool hidden { get; set; }
        public int tier { get; set; }
        public string whitelistString { get; set; }
        public string bgSrc { get; set; }
        public string iconSrc { get; set; }
        public int bgColor { get; set; }
        public int iconColor { get; set; }
        public MagicEffect onUseEffect { get; set; }
        public MagicEffect equipEffect { get; set; }
        private bool classic;

        static ItemData() {
            legacyTypes = new Dictionary<string, int>();
                legacyTypes.Add("Accessory", ACCESSORY_TYPE);
                legacyTypes.Add("Ammunition", AMMUNITION_TYPE);
                legacyTypes.Add("Amulet", ACCESSORY_TYPE);
                legacyTypes.Add("Axe", WEAPON_TYPE);
                legacyTypes.Add("Belt", ACCESSORY_TYPE);
                legacyTypes.Add("Blade", WEAPON_TYPE);
                legacyTypes.Add("Bow", WEAPON_TYPE);
                legacyTypes.Add("Chestpiece", ARMOR_TYPE);
                legacyTypes.Add("Cloak", ACCESSORY_TYPE);
                legacyTypes.Add("Component", COMPONENT_TYPE);
                legacyTypes.Add("Consumable", CONSUMABLE_TYPE);
                legacyTypes.Add("Container", GENERIC_TYPE);
                legacyTypes.Add("Gloves", ARMOR_TYPE);
                legacyTypes.Add("Hammer", WEAPON_TYPE);
                legacyTypes.Add("Helm", ARMOR_TYPE);
                legacyTypes.Add("Instrument", GENERIC_TYPE);
                legacyTypes.Add("Magic Glove", WEAPON_TYPE);
                legacyTypes.Add("Martial Glove", WEAPON_TYPE);
                legacyTypes.Add("Leggings", ARMOR_TYPE);
                legacyTypes.Add("Pistol", WEAPON_TYPE);
                legacyTypes.Add("Polearm", WEAPON_TYPE);
                legacyTypes.Add("Rifle", WEAPON_TYPE);
                legacyTypes.Add("Ring", ACCESSORY_TYPE);
                legacyTypes.Add("Shield", ARMOR_TYPE);
                legacyTypes.Add("Simple", GENERIC_TYPE);
                legacyTypes.Add("Shoes", ARMOR_TYPE);
                legacyTypes.Add("Staff", WEAPON_TYPE);
                legacyTypes.Add("Sword", WEAPON_TYPE);
                legacyTypes.Add("Torso", ARMOR_TYPE);
                legacyTypes.Add("Wand", WEAPON_TYPE);
            defaultBGOptions = new List<string>();
            string line;
            StringReader reader = new StringReader(VirtualCampaign.Properties.ItemIcons.bg_options);
            while ((line = reader.ReadLine()) != null) {
                defaultBGOptions.Add(line);
            }

            // Fields used for Item Bank
            fieldsList = new string[] { "id", "type", "title", "brief_title", "long_type", "mod_prof", "weight", "components", "value", "dmg", "mdmg", "pow", "mpow", "rng", "mrng", "unit_single",
                "unit_plural", "def", "mdef", "block", "count", "description", "brief_description", "mods", "stackable", "tier", "use_effect", "equip_effect", "bg_src", "icon_src", "bg_color",
                "icon_color", "whitelist" };
        }

        public ItemData() {
            classic = false;
            netID = -1;
            localID = 0;
            title = "";
            briefTitle = "";
            type = GENERIC_TYPE;
            longType = "";
            modProf = "";
            weight = 0;
            value = 0;
            materials = "";
            dmg = "";
            mdmg = "";
            pow = 0;
            mpow = 0;
            rng = 0;
            mrng = 0;
            def = 0;
            mdef = 0;
            unitSingle = "";
            unitPlural = "";
            count = 1;
            description = "";
            briefDescription = "";
            modifierLookup = new Dictionary<string, int>();
            stackable = false;
            bgSrc = "";
            iconSrc = "";
            bgColor = 0;
            iconColor = 0;
            onUseEffect = null;
            equipEffect = null;
            equipped = false;
            starred = false;
            hidden = false;
            tier = 0;
            whitelistString = "";
        }

        public ItemData(string str) {
            long l;
            int i;
            classic = false;
            netID = long.TryParse(StringFunctions.ReadTag(str, "net-id"), out l) ? l : -1;
            localID = long.TryParse(StringFunctions.ReadTag(str, "local-id"), out l) ? l : -1;
            title = StringFunctions.ReadTag(str, "title");
            briefTitle = StringFunctions.ReadTag(str, "btitle");
            type = int.TryParse(StringFunctions.ReadTag(str, "type"), out i) ? i : selectStringType(StringFunctions.ReadTag(str, "type"));
            longType = StringFunctions.ReadTag(str, "long-type");
            modProf = StringFunctions.ReadTag(str, "mod-prof");
            weight = int.TryParse(StringFunctions.ReadTag(str, "weight"), out i) ? i : 1;
            value = long.TryParse(StringFunctions.ReadTag(str, "value"), out l) ? l : 0;
            materials = StringFunctions.ReadTag(str, "materials");
            dmg = StringFunctions.ReadTag(str, "dmg");
            mdmg = StringFunctions.ReadTag(str, "mdmg");
            pow = int.TryParse(StringFunctions.ReadTag(str, "pow"), out i) ? i : 0;
            mpow = int.TryParse(StringFunctions.ReadTag(str, "mpow"), out i) ? i : 0;
            rng = int.TryParse(StringFunctions.ReadTag(str, "rng"), out i) ? i : 0;
            mrng = int.TryParse(StringFunctions.ReadTag(str, "mrng"), out i) ? i : 0;
            def = int.TryParse(StringFunctions.ReadTag(str, "def"), out i) ? i : 0;
            mdef = int.TryParse(StringFunctions.ReadTag(str, "mdef"), out i) ? i : 0;
            unitSingle = StringFunctions.ReadTag(str, "unit-s");
            unitPlural = StringFunctions.ReadTag(str, "unit-p");
            count = int.TryParse(StringFunctions.ReadTag(str, "count"), out i) ? i : 1;
            description = StringFunctions.ReadTag(str, "description");
            if(str.Contains("[desc]") && str.Contains("[/desc]")) { // Legacy support tag
                description = StringFunctions.ReadTag(str, "desc");
                classic = true;
            }
            briefDescription = StringFunctions.ReadTag(str, "bdescription");
            modifierLookup = new Dictionary<string, int>();
            setModifiers(StringFunctions.ReadTag(str, "mods"));
            stackable = StringFunctions.ReadTag(str, "stackable").Equals("1");
            bgSrc = StringFunctions.ReadTag(str, "bg-src");
            iconSrc = StringFunctions.ReadTag(str, "icon-src");
            bgColor = int.TryParse(StringFunctions.ReadTag(str, "bg-color"), out i) ? i : 0;
            iconColor = int.TryParse(StringFunctions.ReadTag(str, "icon-color"), out i) ? i : 0;
            if(classic) {
                selectLegacyIcon();
                bgColor = int.TryParse(StringFunctions.ReadTag(str, "filter"), out i) ? i : 0;
                iconColor = bgColor;
            }
            if(String.IsNullOrWhiteSpace(StringFunctions.ReadTag(str, "use-effect"))) {
                onUseEffect = null;
            } else {
                onUseEffect = new MagicEffect(StringFunctions.ReadTag(str, "use-effect"));
            }
            
            if(String.IsNullOrWhiteSpace(StringFunctions.ReadTag(str, "equip-effect"))) {
                equipEffect = null;
            } else {
                equipEffect = new MagicEffect(StringFunctions.ReadTag(str, "equip-effect"));
            }
            
            equipped = StringFunctions.ReadTag(str, "equipped").Equals("1");
            starred = StringFunctions.ReadTag(str, "starred").Equals("1");
            hidden = StringFunctions.ReadTag(str, "hidden").Equals("1");
            tier = int.TryParse(StringFunctions.ReadTag(str, "tier"), out i) ? i : 0;
            whitelistString = StringFunctions.ReadTag(str, "whitelist");
            if(classic) {
                longType = StringFunctions.ReadTag(str, "type");
            }
        }

        // Copy Constructor. Update if fields change
        public ItemData(ItemData original) {
            classic    = original.classic;
            netID      = original.netID;
            localID    = original.localID;
            title      = original.title;
            briefTitle = original.briefTitle;
            type       = original.type;
            longType   = original.longType;
            modProf    = original.modProf;
            weight     = original.weight;
            value = original.value;
            materials = original.materials;
            dmg = original.dmg;
            mdmg = original.mdmg;
            pow = original.pow;
            mpow = original.mpow;
            rng = original.rng;
            mrng = original.mrng;
            def = original.def;
            mdef = original.mdef;
            unitSingle = original.unitSingle;
            unitPlural = original.unitPlural;
            count = original.count;
            description = original.description;
            briefDescription = original.briefDescription;
            modifierLookup = new Dictionary<string, int>();
            setModifiers(original.generateModifierString());
            stackable = original.stackable;
            bgSrc = original.bgSrc;
            iconSrc = original.iconSrc;
            bgColor = original.bgColor;
            iconColor = original.iconColor;
            onUseEffect = original.onUseEffect;
            equipEffect = original.equipEffect;
            equipped = original.equipped;
            starred = original.starred;
            hidden = original.hidden;
            tier = original.tier;
            whitelistString = original.whitelistString;
        }

        public string getWhitelistedTitle() {
            if(hidden) {
                return briefTitle;
            } else {
                return title;
            }
        }

        private int selectStringType(string check) {
            if(legacyTypes.ContainsKey(check)) {
                classic = true;
                return legacyTypes[check];
            }
            return GENERIC_TYPE;
        }

        private void selectLegacyIcon() {
            switch(type) {
                case GENERIC_TYPE:
                    iconSrc = "orb01";
                    break;
                case WEAPON_TYPE:
                    iconSrc = "sword01";
                    break;
                case ARMOR_TYPE:
                    iconSrc = "cuirass01";
                    break;
                case ACCESSORY_TYPE:
                    iconSrc = "bag01";
                    break;
                case CONSUMABLE_TYPE:
                    iconSrc = "food01";
                    break;
                case AMMUNITION_TYPE:
                    iconSrc = "rope01";
                    break;
                case COMPONENT_TYPE:
                    iconSrc = "orb01";
                    break;
            }
            int i = new Random().Next(0, defaultBGOptions.Count);
            bgSrc = defaultBGOptions[i];
        }

        public string GetTypeString() {
            return GetTypeString(type);
        }

        public static string GetTypeString(int val) {
            switch(val) {
                case ACCESSORY_TYPE:
                    return "Accessory";
                case AMMUNITION_TYPE:
                    return "Ammunition";
                case ARMOR_TYPE:
                    return "Armor";
                case COMPONENT_TYPE:
                    return "Component";
                case CONSUMABLE_TYPE:
                    return "Consumable";
                case WEAPON_TYPE:
                    return "Weapon";
                case GENERIC_TYPE:
                default:
                    return "Generic";
            }
        }

        public string getDescriptiveString() {
            string result = "";

            int permission = (hidden ? 0 : 1);
            /*
            if(hidden) {
                permission = WhitelistHandler.getCurrentUserPermissionLevel(whitelistString);
            } else {
                permission = 1;
            }
            */
            
            result += (permission >= 1 ? title : briefTitle) + Environment.NewLine;
            if(stackable) {
                result += count + " " + (count == 1 ? unitSingle : unitPlural) + Environment.NewLine;
            }
            result += longType + Environment.NewLine;
            result += "Weight: " + weight + "  Value: " + (permission >= 1 ? value.ToString() : "Unknown") + Environment.NewLine;
            if(type == WEAPON_TYPE) {
                if(!string.IsNullOrWhiteSpace(dmg) || pow > 0) {
                    result += "DMG: " + dmg + "   POW: " + pow + "   Range: " + rng + Environment.NewLine;
                }
                if (!string.IsNullOrWhiteSpace(mdmg) || mpow > 0) {
                    result += "MDMG: " + mdmg + "   MPOW: " + mpow + "   M. Range: " + mrng + Environment.NewLine;
                }
            } else if(type == ARMOR_TYPE) {
                if(def > 0) {
                    result += "DEF: " + def + "   ";
                }
                if(mdef > 0) {
                    result += "MDEF: " + mdef + "   ";
                }
                if(block > 0) {
                    result += "Block: " + block + "   ";
                }
                result += Environment.NewLine;
            } else if(type == COMPONENT_TYPE) {
                result += "DMG: " + dmg + "   POW: " + pow + Environment.NewLine;
                result += "MDMG: " + mdmg + "   MPOW: " + mpow + Environment.NewLine;
                result += "DEF: " + def + "   MDEF: " + mdef + "   Block: " + block + Environment.NewLine;
                if(!string.IsNullOrWhiteSpace(mods)) {
                    result += "Mods: " + mods + Environment.NewLine;
                }
            }

            if(onUseEffect != null && permission >= 1) {
                result += Environment.NewLine + "Activated Effect: " + onUseEffect.Effect;
            }
            if(equipEffect != null && permission >= 1) {
                result += Environment.NewLine + "Passive Effect: " + equipEffect.Effect;
            }

            result += Environment.NewLine + Environment.NewLine + (permission >= 1 ? description : briefDescription);

            return result;
        }

        public string generateString() {
            string result = "[item]" +
                "[net-id]" + netID + "[/net-id]" +
                "[title]" + title + "[/title]" +
                "[btitle]" + briefTitle + "[/btitle]" +
                "[type]" + type + "[/type]" +
                "[long-type]" + longType + "[/long-type]" + 
                "[mod-prof]" + modProf + "[/mod-prof]" +
                "[weight]" + weight + "[/weight]" +
                "[value]" + value + "[/value]" +
                "[dmg]" + dmg + "[/dmg]" +
                "[mdmg]" + mdmg + "[/mdmg]" +
                "[pow]" + pow + "[/pow]" +
                "[mpow]" + mpow + "[/mpow]" +
                "[rng]" + rng + "[/rng]" +
                "[mrng]" + mrng + "[/mrng]" +
                "[def]" + def + "[/def]" +
                "[mdef]" + mdef + "[/mdef]" +
                "[unit-s]" + unitSingle + "[/unit-s]" +
                "[unit-p]" + unitPlural + "[/unit-p]" +
                "[description]" + description + "[/description]" +
                "[bdescription]" + briefDescription + "[/bdescription]" +
                "[mods]" + mods + "[/mods]" +
                "[stackable]" + (stackable ? "1" : "0") + "[/stackable]" +
                "[tier]" + tier + "[/tier]" +
                "[bg-src]" + bgSrc + "[/bg-src]" +
                "[icon-src]" + iconSrc + "[/icon-src]" +
                "[bg-color]" + bgColor + "[/bg-color]" +
                "[icon-color]" + iconColor + "[/icon-color]" +
                "[equipped]" + (equipped ? "1" : "0") + "[/equipped]" +
                "[starred]" + (starred ? "1" : "0") + "[/starred]" +
                (onUseEffect == null ? "" : "[use-effect]" + onUseEffect.generateString() + "[/use-effect]") +
                (equipEffect == null ? "" : "[equip-effect]" + equipEffect.generateString() + "[/equip-effect]") +
                "[hidden]" + (hidden ? "1" : "0") + "[/hidden]" +
                "[whitelist]" + whitelistString + "[/whitelist]" +
                "[/item]";
            return result;
        }

        public int getModifierValue(string key) {
            return getModifierValue(key, true, true);
        }

        public string[] getModifierKeySet() {
            List<string> keys = new List<string>();
            foreach (string s in modifierLookup.Keys) keys.Add(s);
            if(onUseEffect != null) {
                string[] k = onUseEffect.GetModifierKeySet();
                foreach(string s in k) {
                    if (!keys.Contains(s)) keys.Add(s);
                }
            }
            if (equipEffect != null) {
                string[] k = equipEffect.GetModifierKeySet();
                foreach (string s in k) {
                    if (!keys.Contains(s)) keys.Add(s);
                }
            }
            return keys.ToArray<string>();
        }

        public int getModifierValue(string key, bool includeOnUseEffect, bool includeEquipEffect) {
            int baseline = modifierLookup.ContainsKey(key) ? modifierLookup[key] : 0;
            if (includeOnUseEffect && onUseEffect != null) baseline += onUseEffect.GetModifierValue(key);
            if (includeEquipEffect && equipEffect != null) baseline += equipEffect.GetModifierValue(key);
            return baseline;
        }

        public void setModifiers(string m) {
            modifierLookup.Clear();
            addModifiers(m);
        }

        public void addModifiers(string m) {
            if (!String.IsNullOrWhiteSpace(m)) {
                string[] split;
                if (m.Contains(";")) {
                    split = m.Split(';');
                } else {
                    split = new string[] { m };
                }
                string[] pair;
                foreach (string s in split) {
                    if (s.Contains(":")) {
                        pair = s.Split(':');
                        if (pair.Length == 2) {
                            if (int.TryParse(pair[1], out int i)) {
                                if (modifierLookup.ContainsKey(pair[0])) {
                                    modifierLookup[pair[0]] = modifierLookup[pair[0]] + i;
                                } else {
                                    modifierLookup.Add(pair[0], i);
                                }
                            }
                        }
                    }
                }
            }
        }

        public string generateModifierString() {
            string result = "";
            foreach(string s in modifierLookup.Keys) {
                result += s + ":" + modifierLookup[s] + ";";
            }
            return result;
        }

        // Use for Item Bank
        public string[][] buildMySQLArray() {
            string[][] result = new string[2][] { new string[fieldsList.Length - 1], new string[fieldsList.Length - 1] }; // Ignore ID field

            int fieldIndex = 0;
            for(int i = 0; i < result[0].Length; i++) {
                if(fieldIndex < fieldsList.Length) {
                    switch(fieldsList[fieldIndex]) {
                        case "id":
                            ++fieldIndex;
                            --i;
                            break;
                        case "type":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = type.ToString();
                            break;
                        case "title":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = title;
                            break;
                        case "brief_title":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = briefTitle;
                            break;
                        case "long_type":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = longType;
                            break;
                        case "mod_prof":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = modProf;
                            break;
                        case "weight":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = weight.ToString();
                            break;
                        case "components":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = materials;
                            break;
                        case "value":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = value.ToString();
                            break;
                        case "dmg":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = dmg;
                            break;
                        case "mdmg":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = mdmg;
                            break;
                        case "pow":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = pow.ToString();
                            break;
                        case "mpow":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = mpow.ToString();
                            break;
                        case "rng":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = rng.ToString();
                            break;
                        case "mrng":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = mrng.ToString();
                            break;
                        case "unit_single":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = unitSingle;
                            break;
                        case "unit_plural":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = unitPlural;
                            break;
                        case "def":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = def.ToString();
                            break;
                        case "mdef":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = mdef.ToString();
                            break;
                        case "block":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = block.ToString();
                            break;
                        case "count":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = count.ToString();
                            break;
                        case "description":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = description;
                            break;
                        case "brief_description":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = briefDescription;
                            break;
                        case "mods":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = mods;
                            break;
                        case "stackable":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = stackable ? "1" : "0";
                            break;
                        case "tier":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = tier.ToString();
                            break;
                        case "use_effect":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = onUseEffect == null ? "" : onUseEffect.generateString();
                            break;
                        case "equip_effect":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = equipEffect == null ? "" : equipEffect.generateString();
                            break;
                        case "bg_src":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = bgSrc;
                            break;
                        case "icon_src":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = iconSrc;
                            break;
                        case "bg_color":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = bgColor.ToString();
                            break;
                        case "icon_color":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = iconColor.ToString();
                            break;
                        case "whitelist":
                            result[0][i] = fieldsList[fieldIndex++];
                            result[1][i] = whitelistString;
                            break;
                    }
                } else {
                    break;
                }
            }
            return result;
        }
    }
}
