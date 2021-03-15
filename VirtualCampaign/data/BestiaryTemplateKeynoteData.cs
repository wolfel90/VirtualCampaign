using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.data {
    public class BestiaryTemplateKeynoteData {
        public int strength, agility, intelligence, fortitude, charisma;
        public int hp, ep, mp;
        public int regen, accuracy, db, speed, survival, awareness, wgtAllow, willpower, stealth;
        public int conduct, morality, conductVariance, moralityVariance;
        public string dmg, mdmg;
        public int def, mdef;
        public string attacks, defenses;

        public string GenerateDataString() {
            string result = "";
            result += "[str]" + strength + "[/str]";
            result += "[agi]" + agility + "[/agi]";
            result += "[intel]" + intelligence + "[/intel]";
            result += "[fort]" + fortitude + "[/fort]";
            result += "[cha]" + charisma + "[/cha]";
            return result;
        }
    }
}
