using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class UserTag {
        public long userID { get; set; } = -1;
        public string username { get; set; } = "";
        public string displayName { get; set; } = "";
        public int userRank { get; set; } = 0;

        public override string ToString() {
            if (!String.IsNullOrEmpty(displayName)) {
                return this.displayName;
            } else if(!String.IsNullOrEmpty(username)) {
                return this.username;
            } else {
                return "User ID: " + userID;
            }
        }
    }
}
