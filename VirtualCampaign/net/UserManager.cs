using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using VirtualCampaign.data;

namespace VirtualCampaign.net {
    public class UserChangeEventArgs : EventArgs {
        public UserTag tag { get; set; }

        public UserChangeEventArgs() { }

        public UserChangeEventArgs(UserTag tag) {
            this.tag = tag;
        }
    }

    static class UserManager {
        public static event UserChanged UserEvent;
        public delegate void UserChanged(UserChangeEventArgs e);
        private static List<UserTag> knownUsers;
        private static List<long> friends;
        public static UserTag currentUser { get; set; }

        static UserManager() {
            knownUsers = new List<UserTag>();
            currentUser = new UserTag();
            friends = new List<long>();
        }

        public static bool attemptSignIn(string username, string pwd) {
            return attemptSignIn(username, pwd, out string k);
        }

        public static bool attemptSignIn(string username, string pwd, out string k) {
            return attemptSignIn(username, pwd, out k, true);
        }

        public static bool attemptSignIn(string username, string pwd, out string k, bool encrypt) {
            string check = "";
            string securePass = encrypt ? (saltAndEncrypt(username, pwd, out string sec) ? sec : "") : pwd;
            if (signIn(username, securePass, out UserTag tag, out check)) {
                currentUser = tag;
                if (!isUserKnown(tag.userID)) {
                    knownUsers.Add(tag);
                }
                Console.Out.WriteLine("User " + tag.username + " signed in - ID: " + tag.userID + " Rank: " + tag.userRank);
                UserEvent(new UserChangeEventArgs(tag));
                k = check;
                return true;
            }
            k = check;
            return false;
        }

        public static bool attemptRegistration(string username, string pwd) {
            return attemptRegistration(username, pwd, out string dummy);
        }

        public static bool attemptRegistration(string username, string pwd, out string response) {
            List<Dictionary<String, Object>> userSrc = SQLManager.runImmediateQuery("vc_users", 
                new string[] { "user_name" },
                "LOWER(`user_name`) = LOWER('" + SQLManager.filterForSQL(username) + "') LIMIT 1;");
            if (userSrc.Count > 0) {
                response = "Username already exists";
                return false;
            } else {
                string salt = generateNewSalt();
                string securePass = saltAndEncrypt(pwd, salt);
                bool success = SQLManager.runInsert("vc_users", new string[] { "user_name", "display_name", "user_pwd", "user_safe", "user_rank" },
                new string[] { username, username, securePass, salt, "1" });
                if (success) {
                    response = "User registered successfully!";
                    return true;
                } else {
                    response = "User could not be registered";
                    return false;
                }
            }
        }

        public static string generateNewSalt() {
            string result = "";
            using(var random = new RNGCryptoServiceProvider()) {
                byte[] salt = new byte[31];
                random.GetBytes(salt);
                result = Convert.ToBase64String(salt);
            }
            return result;
        }

        // When password and salt are directly available
        private static string saltAndEncrypt(string pwd, string salt) {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(pwd, Encoding.UTF8.GetBytes(salt), 4);
            byte[] res = pbkdf2.GetBytes(31);
            return System.Convert.ToBase64String(res);
        }

        // When encrypting using the on-file salt for the given user
        private static bool saltAndEncrypt(string usr, string sIn, out string sOut) {
            string salt = getSalt(usr);
            if(salt != "") {
                Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(sIn, Encoding.UTF8.GetBytes(salt), 4);

                byte[] res = pbkdf2.GetBytes(31);
                sOut = System.Convert.ToBase64String(res);
                return true;
            }
            sOut = "";
            return false;
        }

        private static bool signIn(string username, string pwd, out UserTag tag) {
            return signIn(username, pwd, out tag, out string k);
        }

        private static bool signIn(string username, string pwd, out UserTag tag, out string k) {
            tag = new UserTag();
            if (pwd != "") {
                List<Dictionary<String, Object>> userSrc = SQLManager.runImmediateQuery(
                    "vc_users", 
                    new string[] { "id", "user_name", "display_name", "user_rank", "user_friends" }, 
                    "`user_name` = '" + SQLManager.filterForSQL(username) + "' AND `user_pwd` =  '" + pwd + "' LIMIT 1;");
                if (userSrc.Count > 0) {
                    Dictionary<string, object> d = userSrc.ElementAt(0);
                    int i;
                    if (int.TryParse(d["id"].ToString(), out i)) {
                        tag.userID = i;
                        storeIP(i);
                    }
                    tag.username = d["user_name"].ToString();
                    tag.displayName = d["display_name"].ToString();
                    if (int.TryParse(d["user_rank"].ToString(), out i)) tag.userRank = i;

                    string f = d["user_friends"].ToString();
                    string[] split;
                    if(f.Contains(',')) {
                        split = f.Split(',');
                    } else {
                        split = new string[] { f };
                    }
                    foreach(string s in split) {
                        long u;
                        if(long.TryParse(s, out u)) {
                            friends.Add(u);
                        }
                    }
                    identifyUsers(friends.ToArray());
                    k = pwd;

                    return true;
                } else {
                    Console.Out.WriteLine("Username/Password Mismatch");
                }
            } else {
                Console.Out.WriteLine("Invalid Username");
            }
            k = "";
            return false;
        }

        public static void signOut() {
            currentUser.userID = -1;
            currentUser.username = "";
            currentUser.displayName = "";
            currentUser.userRank = 0;
            friends.Clear();
            UserEvent(new UserChangeEventArgs(currentUser));
        }

        private static void storeIP(int uID) {
            var client = new WebClient();
            Stream response = client.OpenRead("http://ayaseye.com/ipfind.php");
            StreamReader sr = new StreamReader(response);
            String ip = sr.ReadToEnd();
            SQLManager.runUpdate("vc_users", new string[] { "user_ip" }, new string[] { ip }, "id=" + uID);
            response.Close();
            client.Dispose();
        }

        private static string getSalt(String username) {
            List<Dictionary<String, Object>> userSrc = SQLManager.runImmediateQuery(
                "vc_users", new string[] { "user_safe" }, "`user_name` = '" + SQLManager.filterForSQL(username) + "' LIMIT 1;");

            if (userSrc.Count > 0) {
                if(userSrc.ElementAt(0).Keys.Contains("user_safe")) {
                    return userSrc.ElementAt(0)["user_safe"].ToString();
                }
            }
            return "";
        }

        public static UserTag getUserTag(long id) {
            foreach (UserTag tag in knownUsers) {
                if (tag.userID == id) {
                    return tag;
                }
            }

            if(identifyUser(id)) {
                foreach (UserTag tag in knownUsers) {
                    if (tag.userID == id) {
                        return tag;
                    }
                }
            }
            return null;
        }

        public static List<UserTag> getIdentifiedUsers() {
            return knownUsers;
        }

        public static string getUserDisplayName(long id) {
            foreach(UserTag tag in knownUsers) {
                if(tag.userID == id) {
                    return tag.displayName;
                }
            }

            if (identifyUser(id)) {
                foreach (UserTag tag in knownUsers) {
                    if (tag.userID == id) {
                        return tag.displayName;
                    }
                }
            }
            return "";
        }

        public static bool identifyUser(long id) {
            return identifyUsers(new long[] { id });
        }

        public static bool identifyUsers(long[] id) {
            bool result = false;
            string res = "";
            foreach (long l in id) {
                if (!String.IsNullOrEmpty(res)) res += " OR ";
                res += "`id` = " + l;
            }
            if (String.IsNullOrEmpty(res)) return false;
            List<Dictionary<String, Object>> userSrc = SQLManager.runImmediateQuery(
                "vc_users",
                new string[] { "id", "user_name", "display_name", "user_rank" },
                res);

            SQLParser parser = new SQLParser();
            foreach(Dictionary<string, object> d in userSrc) {
                UserTag newTag = parser.parseUserTag(d);
                if(newTag.userID <= 0) {
                    continue;
                }

                foreach(UserTag ut in knownUsers) {
                    if(ut.userID == newTag.userID) {
                        knownUsers.Remove(ut);
                        break;
                    }
                }

                knownUsers.Add(newTag);
                result = true;
            }
            return result;
        }

        public static bool isUserKnown(long id) {
            foreach(UserTag tag in knownUsers) {
                if (tag.userID == id) return true;
            }
            return false;
        }
    }
}
