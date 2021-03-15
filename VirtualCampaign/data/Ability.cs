using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class Ability : GamePrimitive {
        public string type { get; set; } = "";
        public string school { get; set; } = "";
        public string vocation { get; set; } = "";
        public string description { get; set; } = "";
        public string cost { get; set; } = "";
        public string exhaustion { get; set; } = "";
        public string range { get; set; } = "";
        public string duration { get; set; } = "";
        public int level { get; set; } = 0;
        public bool useWhitelist { get; set; }
        public string whitelistString { get; set; }
        public string imgSrc { get; set; } = "";
        public bool accCheck { get; set; } = true;

        public Ability() : this("") {

        }

        public Ability(string str) : base() {
            long l;
            netID = long.TryParse(StringFunctions.ReadTag(str, "net-id"), out l) ? l : -1;
            localID = long.TryParse(StringFunctions.ReadTag(str, "local-id"), out l) ? l : -1;
            title = StringFunctions.ReadTag(str, "title");
            type = StringFunctions.ReadTag(str, "type");
            school = StringFunctions.ReadTag(str, "school");
            vocation = StringFunctions.ReadTag(str, "vocation");
            cost = StringFunctions.ReadTag(str, "cost");
            exhaustion = StringFunctions.ReadTag(str, "exhaust");
            range = StringFunctions.ReadTag(str, "range");
            duration = StringFunctions.ReadTag(str, "duration");
            level = int.TryParse(StringFunctions.ReadTag(str, "level"), out int i) ? i : 0;
            description = StringFunctions.ReadTag(str, "description");
            string acc = StringFunctions.ReadTag(str, "accuracy_check").ToLower().Trim();
            accCheck = acc.Equals("t") || acc.Equals("true") || acc.Equals("1") || acc.Equals("yes") || acc.Equals("y");
            imgSrc = StringFunctions.ReadTag(str, "icon");
        }

        public string generateString() {
            string result = "[ability]" +
                "[net-id]" + netID + "[/net-id]" +
                "[local-id]" + localID + "[/local-id]" +
                "[title]" + title + "[/title]" +
                "[description]" + description + "[/description]" +
                "[type]" + type + "[/type]" +
                "[school]" + school + "[/school]" +
                "[vocation]" + vocation + "[/vocation]" +
                "[cost]" + cost + "[/cost]" +
                "[exhaust]" + exhaustion + "[/exhaust]" +
                "[duration]" + duration + "[/duration]" +
                "[range]" + range + "[/range]" +
                "[accuracy_check]" + (accCheck ? "1" : "0") + "[/accuracy_check]" +
                "[level]" + level + "[/level]" +
                "[icon]" + imgSrc + "[/icon]" +
                "[/ability]";
            return result;
        }

        public override string ToString() {
            return String.IsNullOrWhiteSpace(title) ? "<Unnamed Ability>" : title;
        }
    }
}
