using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextButtonRPG
{
    public class Item
    {
        int minDamage, maxDamage, str, dex, intel, agi, armor, reqLevel, critChance = 0;
        bool equipped = false;
        Hero.Job reqClass;
        string name;
        string suffix;
        bool suffixFlag; //triggers when the suffix does not match the slot of the item it's for
        string description;
        public string Description { get { return description; } }
        string[] suffixes = { " of Shining", " of Death", " of Brutality", " of Concannon", " of Shock", " of Heartbreak",/* " of Silence",*/ " of Flame", " of Luck" };
        Slot slot;

        public enum Slot
        {
            Helmet,
            Weapon,
            Armor
        }

        public Item(Item item)
        {
            name = item.getBaseName();
            minDamage = item.getMinDmg();
            maxDamage = item.getMaxDmg();
            armor = item.getArmor();
            str = item.getStr();
            dex = item.getDex();
            intel = item.getInt();
            agi = item.getAgi();
            slot = item.getSlot();
            reqLevel = item.getReqLevel();
            description = item.description;
        }

        public Item(string nm)
        {
            name = nm;
            setStats(nm);
        }

        public Item(string nm = "", int mnDamage = 0, int mxDamage = 0, int arm = 0, int st = 0, int dx = 0, int inte = 0, int ag = 0, Slot s = Slot.Weapon, int reqLvl = 0, string desc = "")
        {
            name = nm;
            minDamage = mnDamage;
            maxDamage = mxDamage;
            armor = arm;
            str = st;
            dex = dx;
            intel = inte;
            agi = ag;
            slot = s;
            reqLevel = reqLvl;
            description = desc;
        }

        public string getName()
        {
            if (suffix != null)
                return name + suffix;
            else
                return name;
        }

        public string getBaseName()
        {
            return name;
        }

        public Slot getSlot()
        {
            return slot;
        }

        public int getReqLevel()
        {
            return reqLevel;
        }

        public void setStats(string nm)
        {
            switch (nm)
            {
                case "Feather Sword":
                    minDamage = 4;
                    maxDamage = 9;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Feather Sword.";
                    break;
                case "Feather Helmet":
                    minDamage = 0;
                    maxDamage = 0;
                    armor = 3;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Helmet;
                    description = "A Feather Helmet.";
                    break;
                case "Feather Armor":
                    minDamage = 0;
                    maxDamage = 0;
                    armor = 5;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Armor;
                    description = "Some Feather Armor.";
                    break;
                case "Feather Bow":
                    minDamage = 4;
                    maxDamage = 9;
                    armor = 0;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Feather Bow.";
                    break;
                case "Feather Dagger":
                    minDamage = 3;
                    maxDamage = 8;
                    armor = 0;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Feather Dagger.";
                    break;
                case "Feather Axe":
                    minDamage = 6;
                    maxDamage = 10;
                    armor = 0;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Feather Axe.";
                    break;
                case "Feather Staff":
                    minDamage = 4;
                    maxDamage = 8;
                    armor = 0;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Feather Staff.";
                    break;
                case "Wooden Sword":
                    minDamage = 13;
                    maxDamage = 21;
                    armor = 0;
                    str = 2;
                    dex = 1;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Wooden Sword.";
                    break;
                case "Wooden Helm":
                    minDamage = 0;
                    maxDamage = 0;
                    armor = 4;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Helmet;
                    description = "A Wooden Helmet.";
                    break;
                case "Wooden Armor":
                    minDamage = 0;
                    maxDamage = 0;
                    armor = 8;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Armor;
                    description = "Some Wooden Armor.";
                    break;
                case "Wooden Bow":
                    minDamage = 13;
                    maxDamage = 21;
                    armor = 0;
                    str = 0;
                    dex = 3;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Wooden Bow.";
                    break;
                case "Wooden Dagger":
                    minDamage = 12;
                    maxDamage = 17;
                    armor = 0;
                    str = 1;
                    dex = 0;
                    intel = 0;
                    agi = 2;
                    slot = Slot.Weapon;
                    description = "A Wooden Dagger.";
                    break;
                case "Wooden Axe":
                    minDamage = 17;
                    maxDamage = 24;
                    armor = 0;
                    str = 2;
                    dex = 1;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Wooden Axe.";
                    break;
                case "Wooden Staff":
                    minDamage = 14;
                    maxDamage = 20;
                    armor = 0;
                    str = 0;
                    dex = 0;
                    intel = 4;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "A Wooden Staff.";
                    break;
                case "Iron Sword":
                    minDamage = 21;
                    maxDamage = 30;
                    armor = 0;
                    str = 3;
                    dex = 2;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "An Iron Sword.";
                    break;
                case "Iron Helm":
                    minDamage = 0;
                    maxDamage = 0;
                    armor = 8;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Helmet;
                    description = "An Iron Helmet.";
                    break;
                case "Iron Armor":
                    minDamage = 0;
                    maxDamage = 0;
                    armor = 14;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Armor;
                    description = "Some Iron Armor.";
                    break;
                case "Iron Bow":
                    minDamage = 21;
                    maxDamage = 30;
                    armor = 0;
                    str = 0;
                    dex = 5;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "An Iron Bow.";
                    break;
                case "Iron Dagger":
                    minDamage = 17;
                    maxDamage = 25;
                    armor = 0;
                    str = 2;
                    dex = 0;
                    intel = 0;
                    agi = 4;
                    slot = Slot.Weapon;
                    description = "An Iron Dagger.";
                    break;
                case "Iron Axe":
                    minDamage = 24;
                    maxDamage = 33;
                    armor = 0;
                    str = 4;
                    dex = 2;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "An Iron Axe.";
                    break;
                case "Iron Staff":
                    minDamage = 21;
                    maxDamage = 27;
                    armor = 0;
                    str = 0;
                    dex = 0;
                    intel = 6;
                    agi = 0;
                    slot = Slot.Weapon;
                    description = "An Iron Staff.";
                    break;
                default:
                    name = "";
                    minDamage = 0;
                    maxDamage = 0;
                    str = 0;
                    dex = 0;
                    intel = 0;
                    agi = 0;
                    slot = Slot.Weapon;
                    break;
            }
        }

        public string printItemInfo()
        {
            string s = name;
            if (suffix != null)
                s += suffix;
            s += "\r\nSlot: " + slot;
            if (minDamage > 0 && maxDamage > 0)
                s += "\r\nDamage: " + minDamage + "-" + maxDamage;
            if (armor > 0)
                s += "\r\nDefense: " + armor;
            if (str > 0)
                s += "\r\nStr: " + str;
            if (dex > 0)
                s += "  |  Dex: " + dex;
            if (agi > 0)
                s += "  |  Agi: " + agi;
            if (intel > 0)
                s += "  |  Int: " + intel;
            s += "\r\nDescription: " + description;
            return s;
        }

        public int getArmor()
        {
            return armor;
        }

        public void setName(string s)
        {
            name = s;
        }

        public int getStr()
        {
            return str;
        }

        public int getDex()
        {
            return dex;
        }

        public int getAgi()
        {
            return agi;
        }

        public int getInt()
        {
            return intel;
        }

        public int getCrit()
        {
            return critChance;
        }

        public int getMinDmg()
        {
            return minDamage;
        }

        public int getMaxDmg()
        {
            return maxDamage;
        }

        public void setEquipped()
        {
            if (!equipped)
                equipped = true;
            else
                equipped = false;
        }

        public bool isEquipped()
        {
            return equipped;
        }

        public void handleSuffix(string suff)
        {
            switch (suff)
            {
                case " of Shining":
                    str += 5;
                    dex += 5;
                    description += "\r\nShining: Adds 5 str/5 dex.";
                    suffixFlag = false;
                    break;
                case " of Death":
                    minDamage += minDamage/2;
                    maxDamage += maxDamage/2;
                    description += "\r\nDeath: 50% higher min/max damage.";
                    suffixFlag = false;
                    break;
                case " of Concannon":
                    str += 5;
                    dex += 4;
                    agi += 3;
                    intel += 2;
                    description += "\r\nConcannon: Adds 5 str/4 dex/3 agi/2 int.";
                    suffixFlag = false;
                    break;
                case " of Brutality":
                    minDamage += 10;
                    maxDamage += 10;
                    description += "\r\nBrutality: Adds 10 min/max damage.";
                    suffixFlag = false;
                    break;
                case " of Shock":
                    if (slot == Slot.Weapon)
                    {
                        intel += 5;
                        description += "\r\nShock: Gives 5 int. Deals 1-8 electric damage on attack.";
                        suffixFlag = false;
                    }
                    else
                        suffixFlag = true;
                    break;
                case " of Heartbreak":
                    if (slot == Slot.Weapon)
                    {
                        description += "\r\nHeartbreak: Deals 5 pure damage on attack.";
                        suffixFlag = false;
                    }
                    else
                        suffixFlag = true;
                    break;
                case " of Silence":
                    description += "\r\nSilence: 2% chance to silence for 2 turns.";
                    suffixFlag = false;
                    break;
                case " of Flame":
                    if (slot == Slot.Weapon)
                    {
                        description += "\r\nFlame: Conveys 10-15 fire damage on attack.";
                        suffixFlag = false;
                    }
                    else
                        suffixFlag = true;
                    break;
                case " of Luck":
                    description += "\r\nLuck: Adds 20% chance to crit.";
                    critChance += 20;
                    suffixFlag = false;
                    break;
                default:
                    break;
            }
        }

        public void randomSuffix(Random r)
        {
            int index = r.Next(0, suffixes.Length);
            int chance = r.Next(0, 99);
            if (chance < 25)
            {
                suffix = suffixes[index];
                handleSuffix(suffix);
                while (suffixFlag)
                {
                    index = r.Next(0, suffixes.Length);
                    suffix = suffixes[index];
                    handleSuffix(suffix);
                }
            }
        }

        public string getSuffix()
        {
            return suffix;
        }

        public void setSuffix(string s)
        {
            suffix = s;
        }
    }
}
