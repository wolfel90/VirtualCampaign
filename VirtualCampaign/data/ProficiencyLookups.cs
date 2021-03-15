using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VirtualCampaign.data {
    static class ProficiencyLookups {
        private static Dictionary<string, ProficiencyTable> classicProficiencies;
        private static ProficiencyTable weightAllowance;
        private static ProficiencyTable speed;
        private static ProficiencyTable accuracy;
        private static ProficiencyTable dodge;
        private static ProficiencyTable survival;
        private static ProficiencyTable regen;
        private static ProficiencyTable channeling;
        private static ProficiencyTable stamina;
        private static ProficiencyTable awareness;
        private static ProficiencyTable willpower;
        private static ProficiencyTable spirit;
        private static ProficiencyTable stealth;
        private static ProficiencyTable power;
        private static ProficiencyTable magicPower;
        private static ProficiencyTable baseDamage;
        private static ProficiencyTable baseMagicDamage;
        private static ProficiencyTable baseDefense;
        private static ProficiencyTable baseMagicDefense;
        private static ProficiencyTable lowestHP;
        private static ProficiencyTable lowHP;
        private static ProficiencyTable standardHP;
        private static ProficiencyTable highHP;
        private static ProficiencyTable highestHP;
        private static ProficiencyTable lowestEPMP;
        private static ProficiencyTable lowEPMP;
        private static ProficiencyTable standardEPMP;
        private static ProficiencyTable highEPMP;
        private static ProficiencyTable highestEPMP;

        static ProficiencyLookups() {
            classicProficiencies = new Dictionary<string, ProficiencyTable>();
            buildClassicProficiencyTables();
            weightAllowance = buildProficiencyTable(VirtualCampaign.Properties.Resources.WeightAllowance);
            power = buildProficiencyTable(VirtualCampaign.Properties.Resources.Power);
            magicPower = buildProficiencyTable(VirtualCampaign.Properties.Resources.MagicPower);
            speed = buildProficiencyTable(VirtualCampaign.Properties.Resources.Speed);
            accuracy = buildProficiencyTable(VirtualCampaign.Properties.Resources.Accuracy);
            dodge = buildProficiencyTable(VirtualCampaign.Properties.Resources.Dodge);
            survival = buildProficiencyTable(VirtualCampaign.Properties.Resources.Survival);
            regen = buildProficiencyTable(VirtualCampaign.Properties.Resources.Regen);
            channeling = buildProficiencyTable(VirtualCampaign.Properties.Resources.Channeling);
            stamina = buildProficiencyTable(VirtualCampaign.Properties.Resources.Stamina);
            awareness = buildProficiencyTable(VirtualCampaign.Properties.Resources.Awareness);
            willpower = buildProficiencyTable(VirtualCampaign.Properties.Resources.Willpower);
            stealth = buildProficiencyTable(VirtualCampaign.Properties.Resources.Stealth);
            spirit = buildProficiencyTable(VirtualCampaign.Properties.Resources.Spirit);
            baseDamage = buildProficiencyTable(VirtualCampaign.Properties.Resources.Base_Damage);
            baseMagicDamage = buildProficiencyTable(VirtualCampaign.Properties.Resources.Base_Magic_Damage);
            baseDefense = buildProficiencyTable(VirtualCampaign.Properties.Resources.Base_Defense);
            baseMagicDefense = buildProficiencyTable(VirtualCampaign.Properties.Resources.Base_Magic_Defense);
            lowestHP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Lowest_HP);
            lowHP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Low_HP);
            standardHP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Standard_HP);
            highHP = buildProficiencyTable(VirtualCampaign.Properties.Resources.High_HP);
            highestHP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Highest_HP);
            lowestEPMP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Lowest_EPMP);
            lowEPMP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Low_EPMP);
            standardEPMP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Standard_EPMP);
            highEPMP = buildProficiencyTable(VirtualCampaign.Properties.Resources.High_EPMP);
            highestEPMP = buildProficiencyTable(VirtualCampaign.Properties.Resources.Highest_EPMP);
        }

        public static double WeightAllowance(int key) {
            return weightAllowance.getValue(key);
        }

        public static double Power(int key) {
            return power.getValue(key);
        }

        public static double MagicPower(int key) {
            return magicPower.getValue(key);
        }

        public static double Speed(int key) {
            return speed.getValue(key);
        }

        public static double Accuracy(int key) {
            return accuracy.getValue(key);
        }

        public static double Dodge(int key) {
            return dodge.getValue(key);
        }

        public static double Survival(int key) {
            return survival.getValue(key);
        }

        public static double Regen(int key) {
            return regen.getValue(key);
        }

        public static double Channeling(int key) {
            return channeling.getValue(key);
        }

        public static double Stamina(int key) {
            return stamina.getValue(key);
        }

        public static double Awareness(int key) {
            return awareness.getValue(key);
        }

        public static double Willpower(int key) {
            return willpower.getValue(key);
        }

        public static double Spirit(int key) {
            return spirit.getValue(key);
        }

        public static double Stealth(int key) {
            return stealth.getValue(key);
        }

        public static double BaseDamage(int key) {
            return baseDamage.getValue(key);
        }

        public static double BaseMagicDamage(int key) {
            return baseMagicDamage.getValue(key);
        }

        public static double BaseDefense(int key) {
            return baseDefense.getValue(key);
        }

        public static double BaseMagicDefense(int key) {
            return baseMagicDefense.getValue(key);
        }

        public static double LowestHP(int key) {
            return lowestHP.getValue(key);
        }

        public static double LowHP(int key) {
            return lowHP.getValue(key);
        }

        public static double StandardHP(int key) {
            return standardHP.getValue(key);
        }

        public static double HighHP(int key) {
            return highHP.getValue(key);
        }

        public static double HighestHP(int key) {
            return highestHP.getValue(key);
        }

        public static double LowestEPMP(int key) {
            return lowestEPMP.getValue(key);
        }

        public static double LowEPMP(int key) {
            return lowEPMP.getValue(key);
        }

        public static double StandardEPMP(int key) {
            return standardEPMP.getValue(key);
        }

        public static double HighEPMP(int key) {
            return highEPMP.getValue(key);
        }

        public static double HighestEPMP(int key) {
            return highestEPMP.getValue(key);
        }

        public static double ClassicAttribute(string key, int level) {
            if(classicProficiencies.ContainsKey(key)) {
                return classicProficiencies[key].getValue(level);
            } else {
                return 0;
            }
        }

        static void buildClassicProficiencyTables() {
            string line, tag = "";
            string[] split;
            using(System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.ClassicAttributes)) {
                while((line = reader.ReadLine()) != null) {
                    split = (line.Contains(":") ? line.Split(':') : null);
                    if(split?.Length == 2) {
                        switch(split[0]) {
                            case "T":
                                tag = split[1];
                                if (!classicProficiencies.ContainsKey(tag)) classicProficiencies.Add(tag, new ProficiencyTable());
                                break;
                            case "N":
                                if(classicProficiencies.ContainsKey(tag)) {
                                    classicProficiencies[tag].negativeInfinite = int.TryParse(split[1], out int i) ? i : 0;
                                }
                                break;
                            case "P":
                                if (classicProficiencies.ContainsKey(tag)) {
                                    classicProficiencies[tag].positiveInfinite = int.TryParse(split[1], out int i) ? i : 0;
                                }
                                break;
                            default:
                                if (classicProficiencies.ContainsKey(tag)) {
                                    if(int.TryParse(split[0], out int i)) {
                                        if(double.TryParse(split[1], out double d)) {
                                            classicProficiencies[tag].Add(i, d);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        static ProficiencyTable buildProficiencyTable(string src) {
            ProficiencyTable result = new ProficiencyTable();
            string line, spec;
            int key;
            double val;
            using (System.IO.StringReader reader = new System.IO.StringReader(src)) {
                while ((line = reader.ReadLine()) != null) {
                    if (line.Contains('N') || line.Contains('P')) {
                        spec = parseLineSpecial(line, out val);
                        if (spec.Equals("N")) {
                            result.negativeInfinite = val;
                        } else if (spec.Equals("P")) {
                            result.positiveInfinite = val;
                        }
                    } else {
                        if ((key = parseLine(line, out val)) != -1000000) {
                            result.Add(key, val);
                        }
                    }

                }
            }
            return result;
        }

        static int parseLine(string line, out double val) {
            int key = -1000000;
            string[] split;
            if (line.Contains(':')) {
                split = line.Split(':');
                if (int.TryParse(split[0], out int i)) {
                    if (double.TryParse(split[1], out double d)) {
                        key = i;
                        val = d;
                        return key;
                    }
                }
            }
            val = 0;

            return key;
        }

        static string parseLineSpecial(string line, out double val) {
            string key = "";
            string[] split;
            if (line.Contains(':')) {
                split = line.Split(':');
                if (double.TryParse(split[1], out double d)) {
                    key = split[0];
                    val = d;
                    return key;
                }
            }
            val = 0;

            return key;
        }
    }

    class ProficiencyTable : Dictionary<int, double> {
        public int Highest { get { return _highest(); } }
        public int Lowst {  get { return _lowest(); } }
        public double positiveInfinite, negativeInfinite;

        public ProficiencyTable() : base() {
            positiveInfinite = 0;
            negativeInfinite = 0;
        }

        public double getValue(int key) {
            if(this.ContainsKey(key)) {
                return this[key];
            } else {
                int c;
                if(key > (c = _highest())) {
                    return this[c] + positiveInfinite * (key - c);
                } else if(key < (c = _lowest())) {
                    return this[c] + negativeInfinite * (c - key);
                } else {
                    return 0;
                }
            }
        }

        private int _highest() {
            int current = int.MinValue;
            foreach(int k in this.Keys) {
                if (k > current) current = k;
            }
            return current;
        }

        private int _lowest() {
            int current = int.MaxValue;
            foreach(int k in this.Keys) {
                if (k < current) current = k;
            }
            return current;
        }
    }
}
