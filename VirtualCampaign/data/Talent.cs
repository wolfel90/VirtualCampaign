using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class Talent : GamePrimitive {
        public static int TALENT_MAX = 40;
        public string type { get; set; } = "";
        public string description { get; set; } = "";
        public string proficiency { get; set; } = "";
        private int _rank = 0;
        public int rank { get { return _rank; } set { _rank = (value < 0 ? 0 : (value > TALENT_MAX ? TALENT_MAX : value)); } }
        private int _progress = 0;
        public int progress { get { return _progress; } set { _progress = value; } }
        public List<StatModifier> modifiers { get; set; }
        public bool useWhitelist { get; set; }
        public string whitelistString { get; set; }

        public Talent() : base() {
            modifiers = new List<StatModifier>();
        }

        public Talent(string str) : this(str, false) {}

        public Talent(string str, bool classic) : base() {
            long l;
            netID = long.TryParse(StringFunctions.ReadTag(str, "net-id"), out l) ? l : -1;
            localID = long.TryParse(StringFunctions.ReadTag(str, "local-id"), out l) ? l : -1;
            title = StringFunctions.ReadTag(str, "title");
            type = StringFunctions.ReadTag(str, "type");
            description = StringFunctions.ReadTag(str, "description");
            proficiency = StringFunctions.ReadTag(str, "proficiency");
            if(classic) {
                double d, n, nm;
                n = double.TryParse(StringFunctions.ReadTag(str, "level"), out d) ? d : 0;
                nm = double.TryParse(StringFunctions.ReadTag(str, "level-max"), out d) ? d : 0;
                if(nm > 0) {
                    d = (n / nm) * 5;
                    _rank = (int)d;
                }
            } else {
                _rank = int.TryParse(StringFunctions.ReadTag(str, "rank"), out int i) ? i : 0;
                _progress = int.TryParse(StringFunctions.ReadTag(str, "progress"), out i) ? i : 0;
            }
            modifiers = new List<StatModifier>();
            setModifiers(str);
        } 

        public string generateString() {
            string result = "[talent][net-id]" + netID + "[/net-id][local-id]" + localID + "[/local-id][title]" + title + "[/title][description]" + description + "[/description][rank]"
                + _rank + "[/rank][progress]" + _progress + "[/progress][type]" + type + "[/type][proficiency]" + proficiency + "[/proficiency]";
            foreach (StatModifier sm in modifiers) {
                result += sm.generateString();
            }
            result += "[/talent]";
            return result;
        }

        public void setModifiers(string str) {
            modifiers.Clear();
            string[] mods = StringFunctions.ReadTagMulti(str, "modifier");
            foreach (string m in mods) {
                StatModifier mod = new StatModifier(m);
                mod.source = "Trait: " + title;
                modifiers.Add(mod);
            }
        }

        public override string ToString() {
            return String.IsNullOrWhiteSpace(title) ? "<Unnamed Talent>" : title;
        }
    }
}
