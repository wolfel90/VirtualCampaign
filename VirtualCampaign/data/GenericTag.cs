using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class GenericTag {
        public long netID { get; set; }
        public string name { get; set; }

        public GenericTag() {
            netID = -1;
            name = "";
        }

        public override string ToString() {
            return name;
        }
    }
}