using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class MagicEffect : GamePrimitive {
        public enum EffectType : int {
            Triggered_Effect, Continuous_Effect, Triggered_Armor_Effect, Continuous_Armor_Effect, Triggered_Weapon_Effect, Continuous_Weapon_Effect
        }
        public enum EffectQuality : int {
            Neutral, Positive, Negative
        }
        private Dictionary<string, string> Properties;
        private Dictionary<string, int> Mods;
        public string School { get; set; }
        public string Effect { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        private EffectQuality _Quality;
        public EffectQuality Quality { get { return _Quality; } set { SetQuality(value); } }
        private EffectType _Type;
        public EffectType Type { get { return _Type; } set { SetEffectType(value); } }
        public int Rarity { get; set; }
        public int Creator { get; set; }

        public MagicEffect() : this("") {
            Properties = new Dictionary<string, string>();
            Mods = new Dictionary<string, int>();
            School = "";
            Effect = "";
            Prefix = "";
            Suffix = "";
            Quality = EffectQuality.Neutral;
            Type = EffectType.Triggered_Effect;
            Rarity = 0;
            Creator = 0;
        }

        public MagicEffect(string str) {
            Properties = new Dictionary<string, string>();
            Mods = new Dictionary<string, int>();
            netID = long.TryParse(StringFunctions.ReadTag(str, "id"), out long nid) ? nid : -1;
            title = StringFunctions.ReadTag(str, "title");
            School = StringFunctions.ReadTag(str, "school");
            Effect = StringFunctions.ReadTag(str, "effect");
            Prefix = StringFunctions.ReadTag(str, "prefix");
            Suffix = StringFunctions.ReadTag(str, "suffix");
            SetQuality(int.TryParse(StringFunctions.ReadTag(str, "quality"), out int q) ? (EffectQuality)q : EffectQuality.Neutral);
            SetEffectType(int.TryParse(StringFunctions.ReadTag(str, "type"), out int t) ? (EffectType)t : EffectType.Triggered_Effect);
            Rarity = int.TryParse(StringFunctions.ReadTag(str, "rarity"), out int r) ? r : 0;
            Creator = int.TryParse(StringFunctions.ReadTag(str, "creator"), out int c) ? c : 0;
            SetModifiers(StringFunctions.ReadTag(str, "mods"));
        }

        public void SetEffectType(int val) {
            if (Enum.IsDefined(typeof(EffectType), val)) {
                SetEffectType((EffectType)val);
            } else {
                SetEffectType(EffectType.Triggered_Effect);
            }
        }

        public void SetEffectType(EffectType val) {
            _Type = val;
        }

        public void SetQuality(int val) {
            if(Enum.IsDefined(typeof(EffectQuality), val)) {
                SetQuality((EffectQuality)val);
            } else {
                SetQuality(EffectQuality.Neutral);
            }
        }

        public void SetQuality(EffectQuality val) {
            _Quality = val;
        }

        public bool HasModifier(string key) {
            return Mods.ContainsKey(key);
        }

        public string[] GetModifierKeySet() {
            return Mods.Keys.ToArray<string>();
        }

        public int GetModifierValue(string key) {
            return Mods.ContainsKey(key) ? Mods[key] : 0;
        }

        public void SetModifiers(string str) {
            Mods.Clear();
            AddModifiers(str);
        }

        public void AddModifiers(string str) {
            if (str != null) {
                string[] newMods;
                if (str.Contains(';')) {
                    newMods = str.Split(';');
                } else {
                    newMods = new string[] { };
                }
                int index;
                string key;
                foreach (string s in newMods) {
                    index = s.IndexOf(':');
                    if (index > 0) {
                        key = s.Substring(0, index);
                        if (index == s.Length - 1) {
                            if(!Mods.ContainsKey(key)) Mods.Add(key, 0);
                        } else {
                            if (Mods.ContainsKey(key)) {
                                Mods[key] = int.TryParse(s.Substring(index + 1), out int i) ? Mods[key] + i : Mods[key];
                            } else {
                                Mods.Add(key, int.TryParse(s.Substring(index + 1), out int i) ? i : 0);
                            }
                        }
                    }
                }
            }
        }

        public void ReadProperties(string str) {
            Properties.Clear();
            if (str != null) {
                string[] newProps = StringFunctions.TagFinder(str);
                foreach (string s in newProps) {
                    string[] adds = StringFunctions.ReadTagMulti(str, s);
                    foreach (string a in adds) {
                        switch(s.ToLower()) {
                            case "school":
                                School = a;
                                break;
                            case "prefix":
                                Prefix = a;
                                break;
                            case "suffix":
                                Suffix = a;
                                break;
                            case "mods":
                                SetModifiers(a);
                                break;
                            default:
                                if (Properties.ContainsKey(s)) {
                                    Properties[s] = a;
                                } else {
                                    Properties.Add(s, a);
                                }
                                break;
                        }
                        
                    }
                }
            }
        }

        public string GetProperty(string prop) {
            if(Properties.ContainsKey(prop)) {
                return Properties[prop];
            } else {
                switch(prop.ToLower()) {
                    case "school":
                        return School;
                    case "prefix":
                        return Prefix;
                    case "suffix":
                        return Suffix;
                }
            }
            return "";
        }

        public string GeneratePropertiesString() {
            string result = "";
            result += "[school]" + School + "[/school]";
            result += "[prefix]" + Prefix + "[/prefix]";
            result += "[suffix]" + Suffix + "[/suffix]";
            result += Mods.Count > 0 ? "[mods]" + GenerateModsString() + "[/mods]" : "";
            foreach (string key in Properties.Keys) {
                if (Properties[key] != null) {
                    result += "[" + key + "]" + Properties[key] + "[/" + key + "]";
                }
            }
            return result;
        }

        public string GenerateModsString() {
            string result = "";
            foreach(string s in Mods.Keys) {
                result += s + ":" + Mods[s] + ";";
            }
            return result;
        }

        public string generateString() {
            string result = "[magic-effect]" +
                "[id]" + netID + "[/id]" +
                "[title]" + title + "[/title]" +
                "[school]" + School + "[/school]" +
                "[type]" + (int)Type + "[/type]" +
                "[quality]" + (int)Quality + "[/quality]" +
                "[rarity]" + Rarity.ToString() + "[/rarity]" +
                "[prefix]" + Prefix + "[/prefix]" +
                "[suffix]" + Suffix + "[/suffix]" +
                "[effect]" + Effect + "[/effect]" +
                (Mods.Count > 0 ? ("[mods]" + GenerateModsString() + "[/mods]") : "") +
                "[/magic-effect]";
            return result;
        }

        public override string ToString() {
            return title;
        }
    }
}
