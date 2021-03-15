using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public static class StatKeywords {
        public static Dictionary<string, string> translations;

        static StatKeywords() {
            translations = new Dictionary<string, string>();
            using(System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.StatKeywords)) {
                string[] split;
                string line;
                while((line = reader.ReadLine()) != null) {
                    split = line.Split(':');
                    if(split.Length == 2) {
                        translations.Add(split[0], split[1]);
                    }
                }
            }
        }

        public static string Translate(string term) {
            return Translate(term, false);
        }

        public static string Translate(string term, bool exact) {
            if(exact) {
                return translations.Keys.Contains(term) ? translations[term] : term;
            } else {
                term = term.Trim();
                foreach(string s in translations.Keys) {
                    if(s.Trim().Equals(term, StringComparison.CurrentCultureIgnoreCase)) {
                        return translations[s];
                    }
                }
                return term;
            }
        }

        public static string ReverseTranslate(string word) {
            return ReverseTranslate(word, false);
        }

        public static string ReverseTranslate(string word, bool exact) {
            if(exact) {
                foreach(string s in translations.Keys) {
                    if (translations[s] == word) return s;
                }
                return word;
            } else {
                word = Regex.Replace(word, @"\s+", "");
                foreach (string s in translations.Keys) {
                    if (Regex.Replace(word, @"\s+", "").Equals(word, StringComparison.CurrentCultureIgnoreCase)) return s;
                }
                return word;
            }
        }
    }
}
