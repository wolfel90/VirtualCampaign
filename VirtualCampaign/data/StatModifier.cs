using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class StatModifier {
        public const int ADD_MODE = 0, MULTIPLY_MODE = 1;

        public string source { get; set; } = "";
        public string stat { get; set; } = "";
        public double modValue { get; set; } = 0;
        public int mode { get; set; } = 0;

        public StatModifier() {
            stat = "";
            mode = ADD_MODE;
            modValue = 0;
        }

        public StatModifier(String str) {
            stat = StringFunctions.ReadTag(str, "stat");
            mode = int.TryParse(StringFunctions.ReadTag(str, "mode"), out int i) ? i : ADD_MODE;
            modValue = double.TryParse(StringFunctions.ReadTag(str, "mode"), out double d) ? d : 0;
        }

        public string generateString() {
            return "[modifier][stat]" + stat + "[/stat][value]" + modValue + "[/value][mode]" + mode + "[/mode][/modifier]";
        }
    }
}
