using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.data;

namespace VirtualCampaign.events {
    public class ItemEventArgs : EventArgs {
        public ItemData preItem, postItem;

        public ItemEventArgs() : base() {
            preItem = null;
            postItem = null;
        }

        public ItemEventArgs(ItemData pre, ItemData post) : base() {
            this.preItem = pre;
            this.postItem = post;
        }
    }
}
