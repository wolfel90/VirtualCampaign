using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class AtlasTag : ContentTag {
        public string imgSrc { get; set; }
        public float scale;

        public AtlasTag() : base() {
            imgSrc = "";
            scale = 1;
        }
    }
}
