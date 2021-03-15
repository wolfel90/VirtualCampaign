using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.net;

namespace VirtualCampaign.sys {
    public static class WhitelistHandler {
        public static int getCurrentUserPermissionLevel(string whitelist) {
            if (UserManager.currentUser.userID <= 0) return 0;
            return getPermissionLevel(whitelist, UserManager.currentUser.userID);
        }

        public static int getPermissionLevel(string whitelist, long id) {
            string[] levels;
            if(whitelist.Contains('~')) {
                levels = whitelist.Split('~');
            } else {
                levels = new string[] { whitelist };
            }

            for(int i = 0; i < levels.Length; ++i) {
                if (findID(levels[i], id)) return i + 1;
            }
            return 0;
        }

        private static bool findID(string str, long id) {
            string[] ids;
            if(str.Contains(',')) {
                ids = str.Split(',');
            } else {
                ids = new string[] { str };
            }
            long l;
            for(int i = 0; i < ids.Length; ++i) {
                if(long.TryParse(ids[i], out l)) {
                    if(l == id) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
