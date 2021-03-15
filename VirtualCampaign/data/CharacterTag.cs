using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class CharacterTag : ContentTag {
        public string owner = ""; // Legacy support only, do not use this field
        public string gm = ""; // Legacy support only, do not use this field
        public bool classic = false;

        public override string ToString() {
            return String.IsNullOrWhiteSpace(name) ? "<Unnamed Character>" : name;
        }
    }
}
