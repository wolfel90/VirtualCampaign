using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.sys;

namespace VirtualCampaign.net {
    public class SQLParser {
        private Dictionary<string, object> currentData;

        public ArticleTag parseArticleTag(Dictionary<string, object> data) {
            currentData = data;
            ArticleTag art = new ArticleTag();

            art.netID = parseInt("id");
            art.name = parseString("title");
            art.bGuide = parseBoolean("guidebook");
            art.bGuide2 = parseBoolean("guidebook2");
            art.bLore = parseBoolean("lorebook");
            art.bLore2 = parseBoolean("lorebook2");
            art.bAtlas = parseBoolean("atlas");
            art.bTimeline = parseBoolean("timeline");
            art.bTrait = parseBoolean("trait");
            art.bTalent = parseBoolean("talent");
            art.bSchool = parseBoolean("school");
            art.bAbility = parseBoolean("ability");
            art.bRedirect = parseBoolean("redirect");
            art.setCategories(parseString("type"));
            art.SetProperties(parseString("properties"));
            art.creator = parseInt("creator");
            art.creationTime = parseDateTime("creation_time");
            art.editor = parseInt("editor");
            art.editTime = parseDateTime("edit_time");
            art.useWhitelist = parseBoolean("whitelist_protected");
            art.whitelistString = parseString("whitelist");

            currentData = null;
            return art;
        }

        public Article parseArticle(Dictionary<string, object> data) {
            Article art = new Article(parseArticleTag(data)); // Article inherits ArticleTag
            currentData = data;
            art.content = parseString("description");
            currentData = null;
            return art;
        }

        public UserTag parseUserTag(Dictionary<string, object> data) {
            currentData = data;
            UserTag result = new UserTag();
            result.userID = parseInt("id", -1);
            result.username = parseString("user_name");
            result.displayName = parseString("display_name");
            result.userRank = (int)parseInt("user_rank");
            
            currentData = null;
            return result;
        }

        public CharacterTag parseCharacterTag(Dictionary<string, object> data) {
            return parseCharacterTag(data, false);
        }

        public CharacterTag parseCharacterTag(Dictionary<string, object> data, bool classicCharacter) {
            currentData = data;
            CharacterTag result = new CharacterTag();
            result.classic = classicCharacter;
            if(classicCharacter) {
                result.netID = parseInt("char_id");
                result.name = parseString("name") + (string.IsNullOrWhiteSpace(parseString("family")) ? "" : " " + parseString("family"));
                result.owner = parseString("owner");
                result.gm = parseString("gm");
                result.useWhitelist = !parseBoolean("shared");
            } else {
                result.netID = parseInt("id");
                result.name = parseString("name");
                result.creator = parseInt("creator");
                result.useWhitelist = parseBoolean("whitelist_protected");
            }
            result.whitelistString = parseString("whitelist");
            
            currentData = null;
            return result;
        }

        public CharacterData parseCharacterData(Dictionary<string, object> data) {
            return parseCharacterData(data, false);
        }

        public CharacterData parseCharacterData(Dictionary<string, object> data, bool classicCharacter) {
            currentData = data;
            CharacterData result = new CharacterData();
            result.classic = classicCharacter;
            
            if (classicCharacter) {
                // Classic character parsing
                result.netID = parseInt("char_id");
                result.name = parseString("name") + (string.IsNullOrWhiteSpace(parseString("family")) ? "" : " " + parseString("family"));
                result.owner = parseString("owner");
                result.gm = parseString("gm");
                result.race = parseString("race");
                result.gender = parseString("gender");
                result.height = parseString("height");
                result.weight = parseString("weight");
                result.age = parseString("age");
                result.birth = parseString("birth");
                result.hair = parseString("hair");
                result.eyes = parseString("eye");
                result.home = parseString("home");
                result.appearance = parseString("appear");
                result.biography = parseString("bio");
                result.languages = parseString("lang");
                result.notes0 = parseString("notes");
                result.notes1 = parseString("notes1");
                result.notes2 = parseString("notes2");
                result.notes3 = parseString("notes3");
                result.level = (int)parseInt("lvl");
                result.strength = (int)parseInt("str");
                result.agility = (int)parseInt("agi");
                result.intelligence = (int)parseInt("intel");
                result.fortitude = (int)parseInt("fort");
                result.charisma = (int)parseInt("cha");
                result.hpCurrent = (int)parseInt("chp");
                result.hpMax = (int)parseInt("mhp");
                result.xpCurrent = (int)parseInt("xp");
                result.xpNext = (int)parseInt("next");
                result.conduct = (int)parseInt("conductval");
                result.morality = (int)parseInt("moralval");
                result.fame = (int)parseInt("fame");
                result.infamy = (int)parseInt("infamy");
                result.channelingAdjust = (int)parseInt("channel_adj");
                result.staminaAdjust = (int)parseInt("stamina_adj");
                result.traits = parseString("traits");
                result.talents = parseString("talents");
                result.schools = parseString("schools");
                result.abilities = parseString("abilities");
                result.auxAbilities = parseString("aux_abilities");
                string items = parseString("inv");
                items += "misc";
                string[] slots = new string[] { "back", "ammo2", "ammo3" };
                foreach(string s in slots) {
                    string p = parseString(s);
                    if(!String.IsNullOrWhiteSpace(p)) {
                        items += "[item]" + p + "[/item]";
                    }
                }
                result.ammo = parseString("ammo1");
                result.head = parseString("head");
                result.torso = parseString("torso");
                result.legs = parseString("legs");
                result.feet = parseString("feet");
                result.hands = parseString("arms");
                result.lh = parseString("lefthand");
                result.rh = parseString("righthand");
                result.neck = parseString("neck");
                result.waist = parseString("waist");
                result.ring1 = parseString("ringl1");
                result.ring2 = parseString("ringl2");
                result.ring3 = parseString("ringl3");
                result.ring4 = parseString("ringr1");
                result.ring5 = parseString("ringr2");
                result.ring6 = parseString("ringr3");
                result.items = items;
                result.wallet = "[mark]" + parseString("mark") + "[/mark][denarius]" + parseString("denarius") + "[/denarius][drake]" + parseString("drake") + 
                    "[/drake][florin]" + parseString("florin") + "[/florin][allervian]" + parseString("allervian") + "[/allervian]";

                result.useWhitelist = parseBoolean("whitelist_protected");
                result.whitelistString = parseString("whitelist");
            } else {
                // Modern character parsing
                result.netID = parseInt("id");
                result.name = parseString("name");
                result.creator = parseInt("creator");
                result.race = parseString("race");
                result.gender = parseString("gender");
                result.height = parseString("height");
                result.weight = parseString("weight");
                result.age = parseString("age");
                result.birth = parseString("birth");
                result.hair = parseString("hair");
                result.eyes = parseString("eyes");
                result.home = parseString("home");
                result.appearance = parseString("appearance");
                result.biography = parseString("biography");
                result.languages = parseString("languages");
                result.modules = parseString("modules");
                result.notes0 = parseString("notes_0");
                result.notes1 = parseString("notes_1");
                result.notes2 = parseString("notes_2");
                result.notes3 = parseString("notes_3");
                result.level = (int)parseInt("level");
                result.strength = (int)parseInt("strength");
                result.agility = (int)parseInt("agility");
                result.intelligence = (int)parseInt("intelligence");
                result.fortitude = (int)parseInt("fortitude");
                result.charisma = (int)parseInt("charisma");
                result.hpCurrent = (int)parseInt("hp_current");
                result.hpMax = (int)parseInt("hp_max");
                result.xpCurrent = (int)parseInt("xp_current");
                result.xpNext = (int)parseInt("xp_next");
                result.conduct = (int)parseInt("conduct");
                result.morality = (int)parseInt("morality");
                result.fame = (int)parseInt("fame");
                result.infamy = (int)parseInt("infamy");
                result.channelingAdjust = (int)parseInt("channel_adj");
                result.staminaAdjust = (int)parseInt("stamina_adj");
                result.exhaustion = (int)parseInt("exhaust");
                result.traits = parseString("traits");
                result.talents = parseString("talents");
                result.schools = parseString("schools");
                result.vocations = parseString("vocations");
                result.abilities = parseString("abilities");
                result.auxAbilities = parseString("aux_abilities");
                result.items = parseString("inventory");
                result.ammo = parseString("ammo");
                result.head = parseString("head");
                result.torso = parseString("torso");
                result.legs = parseString("legs");
                result.feet = parseString("feet");
                result.hands = parseString("hands");
                result.lh = parseString("lh");
                result.rh = parseString("rh");
                result.wieldMode = (int)parseInt("wield_mode");
                result.neck = parseString("neck");
                result.waist = parseString("waist");
                result.ring1 = parseString("ring1");
                result.ring2 = parseString("ring2");
                result.ring3 = parseString("ring3");
                result.ring4 = parseString("ring4");
                result.ring5 = parseString("ring5");
                result.ring6 = parseString("ring6");
                result.wallet = parseString("wallet");
                result.useWhitelist = parseBoolean("whitelist_protected");
                result.whitelistString = parseString("whitelist");
            }
            
            currentData = null;
            return result;
        }

        public Trait parseTrait(Dictionary<string, object> data) {
            currentData = data;
            Trait result = new Trait();
            int i = 0;
            result.netID = parseInt("id");
            result.title = parseString("title");
            result.description = StringFunctions.stripSystemTags(parseString("description")).Trim();
            result.cost = (int.TryParse(StringFunctions.ReadTag(parseString("properties"), "cost"), out i)) ? i : 0;
            result.type = StringFunctions.ReadTag(parseString("properties"), "type");
            result.setModifiers(StringFunctions.ReadTag(parseString("properties"), "mods"));
            result.SetModules(StringFunctions.ReadTag(parseString("properties"), "module"));
            result.SetTags(parseString("system_tags"));
            result.useWhitelist = parseBoolean("whitelist_protected");
            result.whitelistString = parseString("whitelist");
            result.active = StringFunctions.ReadTag(parseString("properties"), "active").Equals("1");
            currentData = null;
            return result;
        }

        public Talent parseTalent(Dictionary<string, object> data) {
            currentData = data;
            Talent result = new Talent();
            result.netID = parseInt("id");
            result.title = parseString("title");
            result.description = StringFunctions.stripSystemTags(parseString("description")).Trim();
            result.type = StringFunctions.ReadTag(parseString("properties"), "type");
            result.setModifiers(parseString("properties"));
            result.SetModules(StringFunctions.ReadTag(parseString("properties"), "module"));
            result.SetTags(parseString("system_tags"));
            result.proficiency = StringFunctions.ReadTag(parseString("properties"), "proficiency");
            result.useWhitelist = parseBoolean("whitelist_protected");
            result.whitelistString = parseString("whitelist");
            currentData = null;
            return result;
        }

        public School parseSchool(Dictionary<string, object> data) {
            currentData = data;
            School result = new School();
            int i = 0;
            result.netID = parseInt("id");
            result.title = parseString("title");
            result.description = StringFunctions.stripSystemTags(parseString("description")).Trim();
            result.level = (int.TryParse(StringFunctions.ReadTag(parseString("properties"), "level"), out i)) ? i : 0;
            result.special = (int.TryParse(StringFunctions.ReadTag(parseString("properties"), "special"), out i)) ? i : 0;
            result.SetModules(StringFunctions.ReadTag(parseString("properties"), "module"));
            result.SetTags(parseString("system_tags"));
            result.useWhitelist = parseBoolean("whitelist_protected");
            result.whitelistString = parseString("whitelist");
            currentData = null;
            return result;
        }

        public Ability parseAbility(Dictionary<string, object> data) {
            currentData = data;
            Ability result = new Ability();
            int i = 0;
            result.netID = parseInt("id");
            result.title = parseString("title");
            result.description = StringFunctions.stripSystemTags(parseString("description")).Trim();
            string props = parseString("properties");
            result.type = StringFunctions.ReadTag(props, "type");
            result.school = StringFunctions.ReadTag(props, "school");
            result.vocation = StringFunctions.ReadTag(props, "vocation");
            result.cost = StringFunctions.ReadTag(props, "cost");
            result.exhaustion = StringFunctions.ReadTag(props, "exhaust");
            result.range = StringFunctions.ReadTag(props, "range");
            result.duration = StringFunctions.ReadTag(props, "duration");
            result.level = int.TryParse(StringFunctions.ReadTag(props, "level"), out i) ? i : 0;
            string acc = StringFunctions.ReadTag(props, "accuracy_check").Trim().ToLower();
            result.accCheck = (acc.Equals("1") || acc.Equals("t") || acc.Equals("true") || acc.Equals("y") || acc.Equals("yes"));
            result.imgSrc = StringFunctions.ReadTag(parseString("properties"), "icon");
            result.SetModules(StringFunctions.ReadTag(parseString("properties"), "module"));
            result.SetTags(parseString("system_tags"));
            currentData = null;
            return result;
        }

        public ItemData parseBankItem(Dictionary<string, object> data) {
            ItemData result = new ItemData();
            currentData = data;
            result.netID = parseInt("id");
            result.type = (int)parseInt("type");
            result.title = parseString("title");
            result.briefTitle = parseString("brief_title");
            result.longType = parseString("long_type");
            result.modProf = parseString("mod_prof");
            result.weight = (int)parseInt("weight");
            result.materials = parseString("components");
            result.value = parseInt("value");
            result.dmg = parseString("dmg");
            result.mdmg = parseString("mdmg");
            result.pow = (int)parseInt("pow");
            result.mpow = (int)parseInt("mpow");
            result.rng = (int)parseInt("rng");
            result.mrng = (int)parseInt("mrng");
            result.unitSingle = parseString("unit_single");
            result.unitPlural = parseString("unit_plural");
            result.def = (int)parseInt("def");
            result.mdef = (int)parseInt("mdef");
            result.block = (int)parseInt("block");
            result.count = (int)parseInt("count");
            result.description = parseString("description");
            result.briefDescription = parseString("brief_description");
            result.mods = parseString("mods");
            result.stackable = parseBoolean("stackable");
            result.tier = (int)parseInt("tier", 0);
            result.onUseEffect = String.IsNullOrWhiteSpace(parseString("use_effect")) ? null : new MagicEffect(parseString("use_effect"));
            result.equipEffect = String.IsNullOrWhiteSpace(parseString("equip_effect")) ? null : new MagicEffect(parseString("equip_effect"));
            result.bgSrc = parseString("bg_src");
            result.iconSrc = parseString("icon_src");
            result.bgColor = (int)parseInt("bg_color");
            result.iconColor = (int)parseInt("icon_color");
            result.whitelistString = parseString("whitelist");
            currentData = null;
            return result;
        }

        public MagicEffect parseMagicEffect(Dictionary<string, object> data) {
            MagicEffect result = new MagicEffect();
            currentData = data;
            result.netID = parseInt("id");
            result.title = parseString("title");
            result.SetEffectType((int)parseInt("type"));
            result.Effect = parseString("description");
            result.SetQuality((int)parseInt("quality"));
            result.Rarity = (int)parseInt("rarity");
            result.Creator = (int)parseInt("creator");
            result.ReadProperties(parseString("properties"));
            currentData = null;
            return result;
        }

        public AtlasTag parseAtlasTag(Dictionary<string, object> data) {
            AtlasTag result = new AtlasTag();
            currentData = data;
            result.netID = parseInt("id");
            result.name = parseString("name");
            result.imgSrc = parseString("img");
            result.scale = parseFloat("scale");
            result.creator = (int)parseInt("creator");
            result.useWhitelist = parseBoolean("whitelist_protected");
            result.whitelistString = parseString("whitelist");
            currentData = null;
            return result;
        }

        public AtlasMarker parseAtlasMarker(Dictionary<string, object> data) {
            currentData = data;
            AtlasMarker result = new AtlasMarker();
            if(currentData.ContainsKey("properties")) {
                result.ParsePropertyString(currentData["properties"].ToString());
            }
            
            result.NetID = parseInt("id");
            result.Title = parseString("title");
            result.creator = (int)parseInt("creator");
            result.useWhitelist = parseBoolean("whitelist_protected");
            result.whitelistString = parseString("whitelist");
            result.Scales = false;
            currentData = null;
            return result;
        }

        private long parseInt(string tag) {
            return parseInt(tag, 0);
        }

        private long parseInt(string tag, long def) {
            long result = def;
            if(currentData.ContainsKey(tag)) {
                if (currentData[tag] is Int32) {
                    result = parseInt32(tag);
                } else if (currentData[tag] is UInt32) {
                    result = parseUInt32(tag);
                } else if(currentData[tag] is string) {
                    result = (long.TryParse((string)currentData[tag], out long r)) ? r : 0;
                }
            }

            return result;
        }

        private long parseUInt32(string tag) {
            return parseUInt32(tag, 0);
        }

        private long parseUInt32(string tag, long def) {
            if (currentData.ContainsKey(tag)) {
                if (currentData[tag] is UInt32) {
                    return Convert.ToInt32((UInt32)currentData[tag]);
                }
            }
            return def;
        }

        private long parseInt32(string tag) {
            return parseInt32(tag, 0);
        }

        private long parseInt32(string tag, long def) {
            if(currentData.ContainsKey(tag)) {
                if(currentData[tag] is Int32) {
                    return Convert.ToInt32((Int32)currentData[tag]);
                }
            }
            return def;
        }

        private float parseFloat(string tag) {
            if(currentData.ContainsKey(tag)) {
                if(currentData[tag] is float) {
                    return float.TryParse(currentData[tag].ToString(), out float f) ? f : 0;
                }
            }
            return 0;
        }

        private string parseString(string tag) {
            if(currentData.ContainsKey(tag)) {
                if(currentData[tag] is String) {
                    return (string)currentData[tag];
                }
            }
            return "";
        }

        private bool parseBoolean(string tag) {
            if (currentData.ContainsKey(tag)) {
                if (currentData[tag] is Boolean) {
                    return (bool)currentData[tag];
                }
            }
            return false;
        }

        private DateTime parseDateTime(string tag) {
            if (currentData.ContainsKey(tag)) {
                if (currentData[tag] is DateTime) {
                    return ((DateTime)currentData[tag]);
                }
            }
            return new DateTime();
        }
    }
    
}
