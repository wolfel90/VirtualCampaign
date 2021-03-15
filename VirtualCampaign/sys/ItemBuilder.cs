using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.data;
using VirtualCampaign.net;

namespace VirtualCampaign.sys {
    public class ItemBuilder {
        private ItemData _baseItem;
        private ItemData _resultItem;
        public ItemData resultItem { get { return _resultItem; } }
        public ItemData baseItem { get { return _baseItem;  } set { setBaseItem(value);  } }
        private MagicEffect _onUseEffect;
        public MagicEffect onUseEffect { get { return _onUseEffect; } set { setOnUseEffect(value); } }
        private MagicEffect _equipEffect;
        public MagicEffect equipEffect { get { return _equipEffect; } set { setEquipEffect(value); } }
        private List<ItemData> components;

        public ItemBuilder() {
            components = new List<ItemData>();
            baseItem = new ItemData();
            _resultItem = new ItemData();
            onUseEffect = null;
            equipEffect = null;
        }

        public void clear() {
            clearBaseItem();
            clearComponents();
        }

        public void setBaseItem(ItemData item) {
            if(item == null) {
                _baseItem = new ItemData();
            } else {
                _baseItem = item;
            }
            resolveItem();
        }

        public void clearBaseItem() {
            setBaseItem(null);
        }

        public void addComponent(ItemData item) {
            components.Add(item);
            resolveItem();
        }

        public void removeComponent(ItemData item) {
            components.Remove(item);
        }

        public void clearComponents() {
            components.Clear();
        }

        public void removeComponentAt(int index) {
            if(index >= 0 && index < components.Count) {
                components.RemoveAt(index);
            }
        }

        public bool hasComponent(ItemData item) {
            return components.Contains(item);
        }

        public void setOnUseEffect(MagicEffect effect) {
            _onUseEffect = effect;
            resolveItem();
        }

        public void setEquipEffect(MagicEffect effect) {
            _equipEffect = effect;
            resolveItem();
        }

        private void resolveItem() {
            if(baseItem?.type == ItemData.WEAPON_TYPE) {
                resolveWeapon();
            } else if(baseItem?.type == ItemData.ARMOR_TYPE) {
                resolveArmor();
            } else {
                resolveGeneric();
            }
        }

        private void resolveGeneric() {
            ItemData newItem = new ItemData();
            int index = 0;

            newItem.title = baseItem.title;
            newItem.briefTitle = baseItem.title;
            newItem.type = baseItem.type;
            newItem.longType = baseItem.longType;
            newItem.modProf = baseItem.modProf;

            newItem.weight = baseItem.weight;
            newItem.value = baseItem.value;
            newItem.dmg = baseItem.dmg;
            newItem.mdmg = baseItem.mdmg;
            newItem.pow = baseItem.pow;
            newItem.mpow = baseItem.mpow;
            newItem.def = baseItem.def;
            newItem.mdef = baseItem.mdef;
            newItem.rng = baseItem.rng;
            newItem.mrng = baseItem.mrng;
            newItem.block = baseItem.block;
            newItem.unitSingle = baseItem.unitSingle;
            newItem.unitPlural = baseItem.unitPlural;
            newItem.count = baseItem.count;
            newItem.mods = baseItem.mods;
            newItem.stackable = baseItem.stackable;
            newItem.equipped = false;
            newItem.starred = false;
            newItem.hidden = false;
            newItem.whitelistString = UserManager.currentUser.userID.ToString();
            newItem.bgSrc = baseItem.bgSrc;
            newItem.iconSrc = baseItem.iconSrc;
            newItem.bgColor = baseItem.bgColor;
            newItem.iconColor = baseItem.iconColor;
            foreach (ItemData item in components) {
                if (index == 0) {
                    newItem.title = item.title + " " + newItem.title;
                }
                newItem.weight += item.weight * item.count;
                newItem.value += item.value * item.count;
                newItem.materials += item.title + " x" + item.count + "  ";

                // TODO: Build damage concatenation algorithm
                newItem.dmg += " + " + item.dmg;
                newItem.mdmg += " + " + item.mdmg;
                newItem.pow += item.pow;
                newItem.mpow += item.mpow;
                newItem.def += item.def;
                newItem.mdef += item.mdef;
                newItem.rng += item.rng;
                newItem.mrng += item.mrng;
                newItem.block += item.block;
                if (string.IsNullOrWhiteSpace(newItem.mods)) {
                    newItem.mods = item.mods;
                } else {
                    newItem.mods += ";" + item.mods;
                }
                index++;
            }

            double valueMultiplier = 1.0D;
            if (onUseEffect != null) {
                newItem.onUseEffect = onUseEffect;
                if (!String.IsNullOrWhiteSpace(onUseEffect.Prefix)) {
                    newItem.title = onUseEffect.Prefix + " " + newItem.title;
                }
                if (!String.IsNullOrWhiteSpace(onUseEffect.Suffix)) {
                    if (newItem.title.Contains(" of ") && onUseEffect.Suffix.IndexOf("of ") == 0) {
                        newItem.title = newItem.title + " and " + onUseEffect.Suffix.Substring(3);
                    } else {
                        newItem.title = newItem.title + " " + onUseEffect.Suffix;
                    }
                }
                valueMultiplier += (onUseEffect.Rarity / 4 + 1) * (onUseEffect.Rarity / 4 + 1);
            }
            if (equipEffect != null) {
                newItem.equipEffect = equipEffect;
                if (!String.IsNullOrWhiteSpace(equipEffect.Prefix)) {
                    newItem.title = equipEffect.Prefix + " " + newItem.title;
                }
                if (!String.IsNullOrWhiteSpace(equipEffect.Suffix)) {
                    if (newItem.title.Contains(" of ") && equipEffect.Suffix.IndexOf("of ") == 0) {
                        newItem.title = newItem.title + " and " + equipEffect.Suffix.Substring(3);
                    } else {
                        newItem.title = newItem.title + " " + equipEffect.Suffix;
                    }
                }
                valueMultiplier += (equipEffect.Rarity / 4 + 1) * (equipEffect.Rarity / 4 + 1);
            }
            newItem.value = (long)((double)newItem.value * valueMultiplier);

            newItem.dmg = StringFunctions.simplifyDiceString(newItem.dmg);
            newItem.mdmg = StringFunctions.simplifyDiceString(newItem.mdmg);

            _resultItem = newItem;
        }

        private void resolveWeapon() {
            ItemData newItem = new ItemData();
            int index = 0;

            newItem.title = baseItem.title;
            newItem.briefTitle = baseItem.title;
            newItem.type = baseItem.type;
            newItem.longType = baseItem.longType;
            newItem.modProf = baseItem.modProf;

            newItem.weight = baseItem.weight;
            newItem.value = baseItem.value;
            newItem.dmg = baseItem.dmg;
            newItem.mdmg = baseItem.mdmg;
            newItem.pow = baseItem.pow;
            newItem.mpow = baseItem.mpow;
            newItem.def = baseItem.def;
            newItem.mdef = baseItem.mdef;
            newItem.rng = baseItem.rng;
            newItem.mrng = baseItem.mrng;
            newItem.block = baseItem.block;
            newItem.unitSingle = baseItem.unitSingle;
            newItem.unitPlural = baseItem.unitPlural;
            newItem.count = baseItem.count;
            newItem.mods = baseItem.mods;
            newItem.stackable = baseItem.stackable;
            newItem.equipped = false;
            newItem.starred = false;
            newItem.hidden = false;
            newItem.whitelistString = UserManager.currentUser.userID.ToString();
            newItem.bgSrc = baseItem.bgSrc;
            newItem.iconSrc = baseItem.iconSrc;
            newItem.bgColor = baseItem.bgColor;
            newItem.iconColor = baseItem.iconColor;
            foreach (ItemData item in components) {
                if (index == 0) {
                    newItem.title = item.title + " " + newItem.title;
                }
                newItem.weight += item.weight * item.count;
                newItem.value += item.value * item.count;
                newItem.materials += item.title + " x" + item.count + "  ";

                // TODO: Build damage concatenation algorithm
                newItem.dmg += " + " + item.dmg;
                newItem.mdmg += " + " + item.mdmg;
                newItem.pow += item.pow;
                newItem.mpow += item.mpow;
                newItem.rng += item.rng;
                newItem.mrng += item.mrng;
                newItem.block += item.block;
                if (string.IsNullOrWhiteSpace(newItem.mods)) {
                    newItem.mods = item.mods;
                } else {
                    newItem.mods += ";" + item.mods;
                }
                index++;
            }

            double valueMultiplier = 1.0D;
            if (onUseEffect != null) {
                newItem.onUseEffect = onUseEffect;
                if (!String.IsNullOrWhiteSpace(onUseEffect.Prefix)) {
                    newItem.title = onUseEffect.Prefix + " " + newItem.title;
                }
                if (!String.IsNullOrWhiteSpace(onUseEffect.Suffix)) {
                    if (newItem.title.Contains(" of ") && onUseEffect.Suffix.IndexOf("of ") == 0) {
                        newItem.title = newItem.title + " and " + onUseEffect.Suffix.Substring(3);
                    } else {
                        newItem.title = newItem.title + " " + onUseEffect.Suffix;
                    }
                }
                valueMultiplier += (onUseEffect.Rarity / 4 + 1) * (onUseEffect.Rarity / 4 + 1);
            }
            if (equipEffect != null) {
                newItem.equipEffect = equipEffect;
                if (!String.IsNullOrWhiteSpace(equipEffect.Prefix)) {
                    newItem.title = equipEffect.Prefix + " " + newItem.title;
                }
                if (!String.IsNullOrWhiteSpace(equipEffect.Suffix)) {
                    if (newItem.title.Contains(" of ") && equipEffect.Suffix.IndexOf("of ") == 0) {
                        newItem.title = newItem.title + " and " + equipEffect.Suffix.Substring(3);
                    } else {
                        newItem.title = newItem.title + " " + equipEffect.Suffix;
                    }
                }
                valueMultiplier += (equipEffect.Rarity / 4 + 1) * (equipEffect.Rarity / 4 + 1);
            }
            newItem.value = (long)((double)newItem.value * valueMultiplier);

            newItem.dmg = StringFunctions.simplifyDiceString(newItem.dmg);
            newItem.mdmg = StringFunctions.simplifyDiceString(newItem.mdmg);

            _resultItem = newItem;
        }

        private void resolveArmor() {
            ItemData newItem = new ItemData();
            int index = 0;

            newItem.title = baseItem.title;
            newItem.briefTitle = baseItem.title;
            newItem.type = baseItem.type;
            newItem.longType = baseItem.longType;
            newItem.modProf = baseItem.modProf;

            newItem.weight = baseItem.weight;
            newItem.value = baseItem.value;
            newItem.dmg = baseItem.dmg;
            newItem.mdmg = baseItem.mdmg;
            newItem.pow = baseItem.pow;
            newItem.mpow = baseItem.mpow;
            newItem.def = baseItem.def;
            newItem.mdef = baseItem.mdef;
            newItem.rng = baseItem.rng;
            newItem.mrng = baseItem.mrng;
            newItem.block = baseItem.block;
            newItem.unitSingle = baseItem.unitSingle;
            newItem.unitPlural = baseItem.unitPlural;
            newItem.count = baseItem.count;
            newItem.mods = baseItem.mods;
            newItem.stackable = baseItem.stackable;
            newItem.equipped = false;
            newItem.starred = false;
            newItem.hidden = false;
            newItem.whitelistString = UserManager.currentUser.userID.ToString();
            newItem.bgSrc = baseItem.bgSrc;
            newItem.iconSrc = baseItem.iconSrc;
            newItem.bgColor = baseItem.bgColor;
            newItem.iconColor = baseItem.iconColor;
            foreach (ItemData item in components) {
                if (index == 0) {
                    newItem.title = item.title + " " + newItem.title;
                }
                newItem.weight += item.weight * item.count;
                newItem.value += item.value * item.count;
                newItem.materials += item.title + " x" + item.count + "  ";

                // TODO: Build damage concatenation algorithm
                newItem.def += item.def;
                newItem.mdef += item.mdef;
                newItem.block += item.block;
                if (string.IsNullOrWhiteSpace(newItem.mods)) {
                    newItem.mods = item.mods;
                } else {
                    newItem.mods += ";" + item.mods;
                }
                index++;
            }

            double valueMultiplier = 1.0D;
            if (onUseEffect != null) {
                newItem.onUseEffect = onUseEffect;
                if (!String.IsNullOrWhiteSpace(onUseEffect.Prefix)) {
                    newItem.title = onUseEffect.Prefix + " " + newItem.title;
                }
                if (!String.IsNullOrWhiteSpace(onUseEffect.Suffix)) {
                    if (newItem.title.Contains(" of ") && onUseEffect.Suffix.IndexOf("of ") == 0) {
                        newItem.title = newItem.title + " and " + onUseEffect.Suffix.Substring(3);
                    } else {
                        newItem.title = newItem.title + " " + onUseEffect.Suffix;
                    }
                }
                valueMultiplier += (onUseEffect.Rarity / 4 + 1) * (onUseEffect.Rarity / 4 + 1);
            }
            if (equipEffect != null) {
                newItem.equipEffect = equipEffect;
                if (!String.IsNullOrWhiteSpace(equipEffect.Prefix)) {
                    newItem.title = equipEffect.Prefix + " " + newItem.title;
                }
                if (!String.IsNullOrWhiteSpace(equipEffect.Suffix)) {
                    if (newItem.title.Contains(" of ") && equipEffect.Suffix.IndexOf("of ") == 0) {
                        newItem.title = newItem.title + " and " + equipEffect.Suffix.Substring(3);
                    } else {
                        newItem.title = newItem.title + " " + equipEffect.Suffix;
                    }
                }
                valueMultiplier += (equipEffect.Rarity / 4 + 1) * (equipEffect.Rarity / 4 + 1);
            }
            newItem.value = (long)((double)newItem.value * valueMultiplier);

            newItem.dmg = StringFunctions.simplifyDiceString(newItem.dmg);
            newItem.mdmg = StringFunctions.simplifyDiceString(newItem.mdmg);

            _resultItem = newItem;
        }
    }
}
