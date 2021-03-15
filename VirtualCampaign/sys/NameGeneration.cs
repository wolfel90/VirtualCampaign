using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VirtualCampaign.sys {
    public class NameGeneration {
        private static Random rand;
        private enum BuildState { DEFINE_SOUND, FIND_VALIDS }
        private enum StressSequence { }
        private PhoneticsList phonetics;
        private PhoneticsList softPhonetics;
        private PhoneticsList hardPhonetics;
        private PhoneticsList initializerPhonetics;
        private bool populated;

        public NameGeneration() {
            phonetics = new PhoneticsList();
            softPhonetics = new PhoneticsList();
            hardPhonetics = new PhoneticsList();
            initializerPhonetics = new PhoneticsList();
            populated = false;
            rand = new Random();
        }

        public void PopulatePhonetics() {
            string line;
            PhoneticSound newSound;
            using (StringReader sr = new StringReader(Properties.Resources.Phonetics)) {
                while((line = sr.ReadLine()) != null) {
                    if(line.Length > 0) {
                        newSound = new PhoneticSound(line);
                        if(newSound.valid) {
                            phonetics.Add(newSound);
                            if(newSound.stress == PhoneticSound.Stress.SOFT) {
                                softPhonetics.Add(newSound);
                            } else if(newSound.stress == PhoneticSound.Stress.HARD) {
                                hardPhonetics.Add(newSound);
                            }
                            if (newSound.initializer) initializerPhonetics.Add(newSound);
                        }
                    }
                }
            }
        }

        public string PerformPhoneticAnalysis() {
            string result = "";
            string line;
            Dictionary<PhoneticSound, int> analysisResults = new Dictionary<PhoneticSound, int>();
            foreach(PhoneticSound p in phonetics) {
                analysisResults.Add(p, 1);
            }
            using(StringReader sr = new StringReader(Properties.Resources.Common_Words)) {
                while((line = sr.ReadLine()) != null) {
                    if(line.Length > 0) {
                        for(int i = 0; i < analysisResults.Count; ++i) {
                            if(line.Contains(analysisResults.Keys.ElementAt<PhoneticSound>(i).alpha)) {
                                analysisResults[analysisResults.Keys.ElementAt<PhoneticSound>(i)]++;
                            }
                        }
                    }
                }
            }

            char lead = 'a';
            foreach (PhoneticSound p in analysisResults.Keys) {
                if(p.alpha[0] != lead) {
                    result += "\n";
                    lead = p.alpha[0];
                }
                result += p.initializer ? "i" : " ";
                result += p.followsSoft ? "s" : " ";
                result += p.followsHard ? "h" : " ";
                result += "|";
                if(p.alpha.Length == 3) {
                    result += p.alpha;
                } else if(p.alpha.Length == 2) {
                    result += " " + p.alpha;
                } else {
                    result += "  " + p.alpha;
                }
                result += ":" + (p.stress == PhoneticSound.Stress.HARD ? "h" : "s");
                result += " |";
                result += p.followedBySoft ? "s" : " ";
                result += p.followedByHard ? "h" : " ";
                result += p.ender ? "e" : " ";
                result += "|";
                result += analysisResults[p] + "\n";
                //result += p.alpha + ": " + analysisResults[p] + "\n";
            }

            return result;
        }

        public string GenerateName() {
            string result = "";
            double chance = 1d;
            bool last = false;

            if(!populated) {
                PopulatePhonetics();
                populated = true;
            }

            PhoneticSound next = GetValidInitializer();
            if(next.alpha.Length > 0) {
                result += next.alpha[0].ToString().ToUpper();
                if(next.alpha.Length > 1) {
                    result += next.alpha.Substring(1);
                }
            }
            //result += next.alpha;

            do {
                last = rand.NextDouble() > chance;
                next = GetValidNext(next, last);
                result += next.alpha;
                chance *= 0.75d;
            } while (!last);

            return result;
        }

        private PhoneticSound GetValidInitializer() {
            if(initializerPhonetics.Count > 0) {
                int select = rand.Next(0, initializerPhonetics.Weight);
                for(int i = 0; i < initializerPhonetics.Count; ++i) {
                    select -= initializerPhonetics[i].weight;
                    if(select <= 0) {
                        return initializerPhonetics[i];
                    }

                }
            }
            return new PhoneticSound();
        }

        private PhoneticSound GetValidNext(PhoneticSound current, bool requireEnder) {
            PhoneticsList targetList = new PhoneticsList();
            if(current.followedByHard) {
                foreach (PhoneticSound p in hardPhonetics) {
                    if (current.stress == PhoneticSound.Stress.HARD && p.followsHard) {
                        if ((p.stress == PhoneticSound.Stress.HARD && current.followedByHard) || (p.stress == PhoneticSound.Stress.SOFT && current.followedBySoft)) {
                            if (!requireEnder || (requireEnder && p.ender)) {
                                targetList.Add(p);
                            }
                        }
                    } else if (current.stress == PhoneticSound.Stress.SOFT && p.followsSoft) {
                        if ((p.stress == PhoneticSound.Stress.HARD && current.followedByHard) || (p.stress == PhoneticSound.Stress.SOFT && current.followedBySoft)) {
                            if (!requireEnder || (requireEnder && p.ender)) {
                                targetList.Add(p);
                            }
                        }
                    }
                }
            }
            if (current.followedBySoft) {
                foreach (PhoneticSound p in softPhonetics) {
                    if (current.stress == PhoneticSound.Stress.HARD && p.followsHard) {
                        if ((p.stress == PhoneticSound.Stress.HARD && current.followedByHard) || (p.stress == PhoneticSound.Stress.SOFT && current.followedBySoft)) {
                            if (!requireEnder || (requireEnder && p.ender)) {
                                targetList.Add(p);
                            }
                        }
                    } else if (current.stress == PhoneticSound.Stress.SOFT && p.followsSoft) {
                        if ((p.stress == PhoneticSound.Stress.HARD && current.followedByHard) || (p.stress == PhoneticSound.Stress.SOFT && current.followedBySoft)) {
                            if (!requireEnder || (requireEnder && p.ender)) {
                                targetList.Add(p);
                            }
                        }
                    }
                }
            }

            if(targetList.Count > 0) {
                int select = rand.Next(0, targetList.Weight);
                for(int i = 0; i < targetList.Weight; ++i) {
                    select -= targetList[i].weight;
                    if (select <= 0) return targetList[i];
                }
            }
            return new PhoneticSound();

            /*
            int initial = rand.Next(0, phonetics.Count);
            int select = initial;
            do {
                PhoneticSound next = phonetics[select];
                if(current.stress == PhoneticSound.Stress.HARD && next.followsHard) {
                    if((next.stress == PhoneticSound.Stress.HARD && current.followedByHard) || (next.stress == PhoneticSound.Stress.SOFT && current.followedBySoft)) {
                        return next;
                    }
                } else if(current.stress == PhoneticSound.Stress.SOFT && next.followsSoft) {
                    if ((next.stress == PhoneticSound.Stress.HARD && current.followedByHard) || (next.stress == PhoneticSound.Stress.SOFT && current.followedBySoft)) {
                        return next;
                    }
                }

                select++;
                if (select >= phonetics.Count) select = 0;
            } while (initial != select);
            return phonetics[initial];
            */
        }
    }

    
    public class PhoneticSound {
        public string alpha;
        public enum Stress { SOFT, HARD }
        public bool followsHard, followsSoft;
        public bool followedByHard, followedBySoft;
        public bool initializer, ender;
        public Stress stress;
        public bool valid;
        public int weight;

        public PhoneticSound() {
            alpha = "";
            stress = Stress.SOFT;
            followsHard = false;
            followsSoft = false;
            followedByHard = false;
            followedBySoft = false;
            initializer = false;
            ender = false;
            valid = true;
            weight = 1;
        }

        public PhoneticSound(string a) {
            string[] split = a.Split('|');
            if(split.Length == 4) {
                string[] inner = split[1].Split(':');
                if(inner.Length == 2) {
                    alpha = inner[0].Trim();
                    stress = inner[1].Trim().Equals("s") ? Stress.SOFT : Stress.HARD;
                    followsSoft = split[0].Contains('s');
                    followsHard = split[0].Contains('h');
                    followedBySoft = split[2].Contains('s');
                    followedByHard = split[2].Contains('h');
                    initializer = split[0].Contains('i');
                    ender = split[2].Contains('e');
                    weight = int.TryParse(split[3], out int i) ? i : 1;
                    valid = true;
                } else {
                    valid = false;
                }
            } else {
                valid = false;
            }
        }
    }

    public class PhoneticsList : List<PhoneticSound> {
        private int _wgt;
        public int Weight { get { return _wgt; } }

        new public void Add(PhoneticSound s) {
            base.Add(s);
            _wgt += s.weight;
        }
    }
}
