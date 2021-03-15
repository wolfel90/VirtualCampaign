using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.events {

    public class AttributePanelEventArgs {
        public string AttributeTag;

        public AttributePanelEventArgs() : this("") {}

        public AttributePanelEventArgs(string t) {
            AttributeTag = t;
        }
    }
}
