using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class ContentTag : GenericTag {
        public long creator { get; set; }
        public bool useWhitelist { get; set; }
        public string whitelistString { get; set; }

        public ContentTag() : base() {
            creator = 0;
            useWhitelist = false;
            whitelistString = "";
        }
    }
}
