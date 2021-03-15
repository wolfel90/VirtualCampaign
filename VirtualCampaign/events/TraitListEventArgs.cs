using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.controls;
using VirtualCampaign.data;

namespace VirtualCampaign.events {
    public class TraitListEventArgs : EventArgs {
        public Trait PreviousTrait;
        public TraitPanel Target;

        public TraitListEventArgs() : base() {
            Target = null;
            PreviousTrait = null;
        }

        public TraitListEventArgs(TraitPanel t) : base() {
            Target = t;
            PreviousTrait = null;
        }

        public TraitListEventArgs(TraitPanel target, Trait previous) : base() {
            Target = target;
            PreviousTrait = previous;
        }
    }
}
