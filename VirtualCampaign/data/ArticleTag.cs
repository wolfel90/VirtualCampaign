using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class ArticleTag : ContentTag {
        List<string> categories = new List<string>();
        //private Dictionary<string, string> propertiesLookup;
        public long editor { get; set; }
        public DateTime creationTime { get; set; }
        public DateTime editTime { get; set; }
        public bool bGuide, bGuide2, bLore, bLore2, bAtlas, bTimeline, bTrait, bTalent, bSchool, bAbility, bRedirect;
        private Dictionary<string, List<string>> Props;
        private string _properties;
        public string properties { get { return _properties; } set { SetProperties(value); } }

        public ArticleTag() : base() {
            name = "";
            netID = -1;
            creator = -1;
            editor = -1;
            creationTime = new DateTime();
            editTime = new DateTime();
            useWhitelist = false;
            whitelistString = "";
            bGuide = false;
            bGuide2 = false;
            bLore = false;
            bLore2 = false;
            bAtlas = false;
            bTimeline = false;
            bTrait = false;
            bTalent = false;
            bSchool = false;
            bAbility = false;
            bRedirect = false;
            Props = new Dictionary<string, List<string>>();
        }

        public void SetProperties(string str) {
            _properties = str;
            ParsePropertyString(str, true);
        }

        public void ParsePropertyString(string str) {
            ParsePropertyString(str, true);
        }

        public void ParsePropertyString(string str, bool overwrite) {
            if (overwrite) Props.Clear();
            if (str != null) {
                string[] newProps = StringFunctions.TagFinder(str);
                foreach (string s in newProps) {
                    string[] adds = StringFunctions.ReadTagMulti(str, s);
                    if (s != "property") {
                        foreach (string a in adds) {
                            AddProperty(s, a);
                        }
                    } else {
                        // Legacy support
                        foreach (string a in adds) {
                            string propName = StringFunctions.ReadTag(a, "name");
                            string propValue = StringFunctions.ReadTag(a, "value");
                            AddProperty(propName, propValue);
                        }
                        // End Legacy support
                    }
                }
            }
        }

        public void AddProperty(string key, string val) {
            if (Props.ContainsKey(key)) {
                SetProperty(key, val, Props[key].Count);
            } else {
                SetProperty(key, val, 0);
            }
        }

        public void SetProperty(string key, string val) {
            SetProperty(key, val, 0);
        }

        public void SetProperty(string key, string val, int index) {
            if (!Props.ContainsKey(key)) Props.Add(key, new List<string>());
            if (index >= 0 && index < Props[key].Count) {
                Props[key][index] = val;
            } else if (index == Props[key].Count) {
                Props[key].Add(val);
            }
        }

        public bool HasProperty(string key) {
            return Props.ContainsKey(key);
        }

        public string GetProperty(string key) {
            return GetProperty(key, 0);
        }

        public string GetProperty(string key, int index) {
            if (Props.ContainsKey(key)) {
                if (index >= 0 && index < Props[key].Count) {
                    return Props[key][index];
                }
            }
            return "";
        }

        public string GeneratePropertiesString() {
            string result = "";
            foreach (string key in Props.Keys) {
                if (Props[key] != null) {
                    foreach (string val in Props[key]) {
                        result += "[" + key + "]" + val + "[/" + key + "]";
                    }
                }
            }
            return result;
        }

        public bool IsInModule(string modName) {
            if (modName.ToLower().Equals("core")) return true;
            if(Props.ContainsKey("module")) {
                string[] mods = Props["module"].ToString().Split('|');
                foreach(string s in mods) {
                    if(s.ToLower().Equals(modName.ToLower())) {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public void setCategories(string str) {
            categories.Clear();
        }

        public void mergeCategories(string str) {
            string[] cats = str.Split('~');
            for (int i = 0; i < cats.Length; i++) {
                cats[i] = cats[i].Trim(' ');

                if (!categories.Contains(cats[i])) categories.Add(cats[i]);
            }
        }

        public string generateCategoryString() {
            string result = "";
            foreach(string s in categories) {
                if (!String.IsNullOrWhiteSpace(result)) result += "~";
                result += s;
            }
            return result;
        }
    }
}
