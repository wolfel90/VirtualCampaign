using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class GamePrimitive {
        public long netID { get; set; } = -1;
        public long localID { get; set; } = -1;
        public string title { get; set; } = "";
        private List<string> modulesList = new List<string>();
        private string _modules = "";
        public string modules { get { return _modules; } set { SetModules(value); } }
        private List<string> tagsList = new List<string>();

        public void SetTags(string str) {
            tagsList.Clear();
            if (str == null) {
                return;
            }
            if (!String.IsNullOrWhiteSpace(str)) {
                
                if(str.Contains('|')) {
                    string[] s = str.Split('|');
                    foreach(string tag in s) {
                        tagsList.Add(tag);
                    }
                } else {
                    tagsList.Add(str);
                }
            }
        }

        public void AddTag(string tag) {
            if(tag != null) {
                if (!tagsList.Contains(tag)) {
                    tagsList.Add(tag);
                }
            }
        }

        public void RemoveTag(string tag) {
            tagsList.Remove(tag);
        }

        public bool HasTag(string tag) {
            return tagsList.Contains(tag);
        }

        public List<string> GetTags() {
            return tagsList;
        }

        public void SetModules(string str) {
            if(str == null) {
                _modules = "";
                modulesList.Clear();
                return;
            }
            _modules = str;
            modulesList.Clear();
            if(!String.IsNullOrWhiteSpace(str)) {
                if(str.Contains('|')) {
                    string[] s = str.Split('|');
                    foreach (string mod in s) {
                        modulesList.Add(mod);
                    }
                } else {
                    modulesList.Add(str);
                }
            }
        }

        public void AddModule(string str) {
            if (!modulesList.Contains(str)) {
                modulesList.Add(str);
                _modules = "";
                foreach (string s in modulesList) {
                    if (!_modules.Equals("")) _modules += "|";
                    _modules += s;
                }
            }
        }

        public void RemoveModule(string str) {
            if (modulesList.Contains(str)) {
                modulesList.Remove(str);
                foreach (string s in modulesList) {
                    if (!_modules.Equals("")) _modules += "|";
                    _modules += s;
                }
            }
        }

        public bool IsInModule(string module) {
            if(modulesList.Count == 0) {
                if(module.ToLower() == "core") {
                    return true; // Empty module lists belong to Core by default
                }
            }
            return modulesList.Contains(module);
        }

        public bool IsModularized() {
            return modulesList.Count > 0;
        }
    }
}
