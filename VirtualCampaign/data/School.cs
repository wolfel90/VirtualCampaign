using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class School : GamePrimitive {
        public static int SCHOOL_MAX = 40;
        public string description { get; set; }
        private int _level;
        public int level { get { return _level; } set { _level = (value < 0 ? 0 : (value > SCHOOL_MAX ? SCHOOL_MAX : value)); } }
        private int _special;
        public int special { get { return _special; } set { _special = (value < 0 ? 0 : (value > SCHOOL_MAX ? SCHOOL_MAX : value)); } }
        public int progress;
        private Dictionary<int, List<object>> schoolUnlocks;
        public bool useWhitelist { get; set; }
        public string whitelistString { get; set; }

        public School() : this("") {
        }

        public School(string str) : base() {
            netID = long.TryParse(StringFunctions.ReadTag(str, "net-id"), out long l) ? l : -1;
            title = StringFunctions.ReadTag(str, "title");
            description = StringFunctions.ReadTag(str, "description");
            level = int.TryParse(StringFunctions.ReadTag(str, "level"), out int i) ? i : 0;
            special = int.TryParse(StringFunctions.ReadTag(str, "special"), out i) ? i : 0;
            progress = int.TryParse(StringFunctions.ReadTag(str, "progress"), out i) ? i : 0;
            schoolUnlocks = new Dictionary<int, List<object>>();
        }

        public string generateString() {
            string result = "[school]" +
                "[net-id]" + netID + "[/net-id]" +
                "[title]" + title + "[/title]" +
                "[description]" + description + "[/description]" +
                "[level]" + level + "[/level]" +
                "[special]" + special + "[/special]" +
                "[progress]" + progress + "[/progress]" +
            "[/school]";
            return result;
        }

        public override string ToString() {
            return String.IsNullOrWhiteSpace(title) ? "<Unnamed School>" : title;
        }
    }
}
