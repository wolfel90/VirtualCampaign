using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.data;

namespace VirtualCampaign.sys {
    public class CompoundedStat {
        public static string BASELINE = "baseline", ADJUST = "adjust", RACE = "Race";
        public event EventHandler ValueChanged;
        public int Baseline { get { return getBaseline(); } set { setBaseline(value); } }
        private Dictionary<string, int> values;
        private Dictionary<string, string> hints;
        public int Value { get { return compound(); } }
        public string statKey { get; set; }

        public CompoundedStat() : this("") {
        }

        public CompoundedStat(string key) {
            values = new Dictionary<string, int>();
            hints = new Dictionary<string, string>();
            statKey = key;
        }

        public void setAddend(string key, int value) {
            setAddend(key, value, null);
        }

        public void setAddend(string key, int value, string hint) {
            if (hint == null) {
                if (hints.ContainsKey(key)) hints.Remove(key);
            } else {
                if (hints.ContainsKey(key)) {
                    hints[key] = hint;
                } else {
                    hints.Add(key, hint);
                }
            }

            if (values.ContainsKey(key)) {
                if (values[key] != value) {
                    values[key] = value;
                    OnValueChanged(EventArgs.Empty);
                }
            } else {
                values.Add(key, value);
                OnValueChanged(EventArgs.Empty);
            }
        }

        public bool hasAddend(string key) {
            return values.ContainsKey(key);
        }

        public void removeAddend(string key) {
            values.Remove(key);
            hints.Remove(key);
            OnValueChanged(EventArgs.Empty);
        }

        public string[] GetAllAddends() {
            return values.Keys.ToArray<string>();
        }

        public int getAddend(string key) {
            return (values.ContainsKey(key) ? values[key] : 0);
        }

        public string getHint(string key) {
            if(hints.ContainsKey(key)) {
                return hints[key];
            } else if(values.ContainsKey(key)) {
                return key;
            } else {
                return "";
            }
        }

        private int getBaseline() {
            return getAddend(BASELINE);
        }

        private void setBaseline(int value) {
            setAddend(BASELINE, value);
        }

        private int compound() {
            int result = 0;
            foreach(string s in values.Keys) {
                result += values[s];
            }
            double additor = 0;
            double multiplier = 1;

            result = (int)(((double)result + additor) * multiplier);

            return result;
        }

        protected virtual void OnValueChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }
    }
}
