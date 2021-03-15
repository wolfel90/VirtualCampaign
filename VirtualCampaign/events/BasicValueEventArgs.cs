using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  Basic event class for any simple event that needs to pass an object value
 */

namespace VirtualCampaign.events {
    public class BasicValueEventArgs : EventArgs {
        public object value;

        public BasicValueEventArgs() : this(null) {}

        public BasicValueEventArgs(object val) {
            value = val;
        }
    }
}
