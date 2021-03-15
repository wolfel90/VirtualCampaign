using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.sys {
    public class DieRoll {
        static Random rand;
        private int _c, _d;
        public int count { get { return _c; } set { _c = value; } }
        public int die { get { return _d; } set { _d = value >= 1 ? value : 1; } }
        
        static DieRoll() {
            rand = new Random();
        }

        public static bool TryParse(string val, out DieRoll roll) {
            if(val != null) {
                val = val.ToLower();
                int index = val.IndexOf('d');
                if (index >= 0 && index < val.Length - 1) {
                    string left, right;
                    int c, d;
                    if (index > 0) {
                        left = val.Substring(0, index);
                    } else {
                        left = "1";
                    }
                    right = val.Substring(index + 1);
                    bool valid = int.TryParse(left, out c);
                    valid = int.TryParse(right, out d);
                    if (valid) {
                        roll = new DieRoll(c, d);
                        return true;
                    }
                }
            }
            roll = new DieRoll(1, 0);
            return false;
        }

        public DieRoll(string val) {
            val = val.ToLower();
            int index = val.IndexOf('d');
            if (index >= 0 && index < val.Length - 1) {
                if (index == 0) {
                    count = 1;
                } else {
                    count = int.TryParse(val.Substring(0, index), out int c) ? c : 0;
                }
                die = int.TryParse(val.Substring(index + 1), out int d) ? d : 1;
            } else {
                count = 0;
                die = 1;
            }
        }

        public DieRoll(int c, int d) {
            count = c >= 0 ? c : 0;
            die = d >= 1 ? d : 1;
        }

        public int Roll() {
            if (count == 0) return 0;
            if (die == 1) return count;
            int total = 0;
            for (int i = 0; i < count; ++i) {
                total += rand.Next(1, die + 1);
            }
            return total;
        }

        public int Roll(out int[] rolls) {
            rolls = new int[count];
            int total = 0;
            for (int i = 0; i < count; ++i) {
                rolls[i] = rand.Next(1, die + 1);
                total += rolls[i];
            }
            return total;
        }

        public override string ToString() {
            return count + "d" + die;
        }
    }
}
