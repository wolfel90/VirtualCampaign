using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class Trait : GamePrimitive {
        public string type { get; set; } = "";
        public string description { get; set; } = "";
        public int cost { get; set; } = 0;
        private Dictionary<string, int> modifierLookup;
        public bool active { get; set; }
        public bool useWhitelist { get; set; }
        public string whitelistString { get; set; }

        public Trait() : base() {
            modifierLookup = new Dictionary<string, int>();
        }

        public Trait(String str) : base() {
            long l;
            netID = long.TryParse(StringFunctions.ReadTag(str, "net-id"), out l) ? l : -1;
            localID = long.TryParse(StringFunctions.ReadTag(str, "local-id"), out l) ? l : -1;
            title = StringFunctions.ReadTag(str, "title");
            type = StringFunctions.ReadTag(str, "type");
            description = StringFunctions.ReadTag(str, "description");
            cost = int.TryParse(StringFunctions.ReadTag(str, "cost"), out int i) ? i : 0;
            active = StringFunctions.ReadTag(str, "active").Equals("1");
            modifierLookup = new Dictionary<string, int>();
            setModifiers(StringFunctions.ReadTag(str, "mods"));
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

        public string[] getModifierKeys() {
            return modifierLookup.Keys.ToArray<string>();
        }

        public int getModifierValue(string key) {
            if(modifierLookup.ContainsKey(key)) {
                return modifierLookup[key];
            }
            return 0;
        }

        public string generateString() {
            string result = "[trait][net-id]" + netID + "[/net-id][local-id]" + localID + "[/local-id][title]" + title + "[/title][description]" + description + "[/description][cost]"
                + cost + "[/cost][active]" + (active ? "1" : "0") + "[/active][type]" + type + "[/type]";
            if(modifierLookup.Count > 0) {
                result += "[mods]";
                foreach(string s in modifierLookup.Keys) {
                    result += s + ":" + modifierLookup[s] + ";";
                }
                result += "[/mods]";
            }
            result += "[/trait]";
            return result;
        }

        public override string ToString() {
            return String.IsNullOrWhiteSpace(title) ? "<Unnamed Trait>" : title;
        }
    }
}
