using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextButtonRPG
{
    public class Hero
    {
        private int maxHealth, curHealth, maxMana, curMana, exp, level, str, agi, dex, intel, gold, minDamage, maxDamage, slotsInUse, defense, critChance;
        private int bonusStr = 0, bonusDex = 0, bonusAgi = 0, bonusInt = 0;
        private const int maxLevel = 20;
        private string name;
        private Job job;
        private int[] expTable = { 0, 20, 50, 100, 150, 250, 450, 700, 1000, 1500, 2100, 2800, 3500, 5000 };
        private Item weapon = new Item();
        private Item armor = new Item();
        private Item helmet = new Item();
        List<Item> equippedItems = new List<Item>();

        public enum Job
        {
            Warrior,
            Mage,
            Bowman,
            Ninja
        }

        public Hero(string nm = "", int h = 50, int m = 30, int lvl = 1, int st = 5, int ag = 5, int dx = 5, int inte = 5, int g = 0)
        {
            name = nm;
            maxHealth = h;
            curHealth = maxHealth;
            maxMana = m;
            curMana = maxMana;
            exp = 0;
            level = lvl;
            str = st;
            agi = ag;
            dex = dx;
            intel = inte;
            gold = g;
            equippedItems.Add(weapon);
            equippedItems.Add(armor);
            equippedItems.Add(helmet);
        }

        public int getMaxHealth()
        {
            return maxHealth;
        }

        public int getMaxMana()
        {
            return maxMana;
        }

        public int getCurHealth()
        {
            return curHealth;
        }

        public int getCurMana()
        {
            return curMana;
        }

        public int getLevel()
        {
            return level;
        }

        public int getExp()
        {
            return exp;
        }

        public int getExpTable(int lvl)
        {
            return expTable[lvl];
        }

        public int getDex()
        {
            return dex;
        }

        public int getStr()
        {
            return str;
        }

        public int getAgi()
        {
            return agi;
        }

        public int getIntel()
        {
            return intel;
        }

        public int getBonusStr()
        {
            return bonusStr;
        }

        public int getBonusDex()
        {
            return bonusDex;
        }

        public int getBonusAgi()
        {
            return bonusAgi;
        }

        public int getBonusInt()
        {
            return bonusInt;
        }

        public string getName()
        {
            return name;
        }
        
        public int getGold()
        {
            return gold;
        }

        public Job getJob()
        {
            return job;
        }

        public void setJob(Job j)
        {
            job = j;
            if (j == Job.Warrior)
            {
                str = 7;
                dex = 6;
                agi = 4;
                intel = 3;
            }
            else if (j == Job.Bowman)
            {
                str = 4;
                dex = 7;
                agi = 6;
                intel = 3;
            }
            else if (j == Job.Ninja)
            {
                str = 5;
                dex = 5;
                agi = 7;
                intel = 3;
            }
            else if (j == Job.Mage)
            {
                str = 3;
                dex = 4;
                agi = 5;
                intel = 8;
            }
            maxHealth += (int)(str / 1.5);
            curHealth = maxHealth;
            maxMana += intel;
            curMana = maxMana;
        }

        public void setName(string n)
        {
            name = n;
        }

        public void setLevel(int n)
        {
            level = n;
        }

        public void setMaxHealth(int n)
        {
            maxHealth = n;
        }

        public void setMaxMana(int n)
        {
            maxMana = n;
        }

        public void setCurHealth(int n)
        {
            curHealth = n;
        }

        public void setCurMana(int n)
        {
            curMana = n;
        }

        public void setGold(int n)
        {
            gold = n;
        }

        public void setExp(int n)
        {
            exp = n;
        }

        public void setStr(int n)
        {
            str = n;
        }

        public void setDex(int n)
        {
            dex = n;
        }

        public void setAgi(int n)
        {
            agi = n;
        }

        public void setInt(int n)
        {
            intel = n;
        }

        public void levelUp()
        {
            level++;
            if (job == Job.Warrior)
            {
                str += 5;
                agi += 2;
                dex += 3;
                intel += 1;
            }
            else if (job == Job.Bowman)
            {
                str += 2;
                agi += 3;
                dex += 5;
                intel += 1;
            }
            else if (job == Job.Ninja)
            {
                str += 3;
                agi += 5;
                dex += 2;
                intel += 2;
            }
            else if (job == Job.Mage)
            {
                str += 1;
                agi += 2;
                dex += 3;
                intel += 5;
            }
            maxHealth += (int)(str / 1.5);
            curHealth = maxHealth;
            maxMana += intel;
            curMana = maxMana;
        }

        public string getPlayerInfo()
        {
            calcDamage();
            string info = "Name: " + name + "\r\nClass: " + job + "   |   Gold: " + gold +
                "\r\nLevel: " + level + "   |   Exp: " + exp + "/" + expTable[level] +
                "\r\nHealth: " + curHealth + "/" + maxHealth + "   |   Mana: " + curMana + "/" + maxMana +
                "\r\nStr: " + str + "(+" + bonusStr + ")  |  Dex: " + dex + "(+" + bonusDex + ")\r\nAgi: " + agi + "(+" + bonusAgi + ")  |  Int: " + intel + "(+" +bonusInt+ ")" +
                "\r\nDamage: " + getMinDamage() + "-" + getMaxDamage() + "(" + getAvgDamage() + ")" +
                "\r\nDefense: " + getDefense() +
                 "\r\nWeapon: " + weapon.getName() +
                 "\r\nArmor: " + armor.getName() +
                 "\r\nHelmet: " + helmet.getName();
            return info;
                //"\r\nWeapon: " + weapon.getName();
                //"\r\nArmor: " + armor.getName();
        }

        public string getPlayerStats()
        {
            return name + "\r\n" + level + "\r\n" + maxHealth + "\r\n" + maxMana + "\r\n" + exp + "\r\n" + job + "\r\n" + (str-bonusStr) + "\r\n" + (agi-bonusAgi) + "\r\n" + (dex-bonusDex) + "\r\n" + (intel-bonusInt) + "\r\n" + gold;
        }

        public void calcDamage()
        {
            if (job == Job.Warrior)
            {
                minDamage = (str / 2) + (dex / 4) + weapon.getMinDmg() + armor.getMinDmg() + helmet.getMinDmg();
                maxDamage = str + (dex / 2) + weapon.getMaxDmg() + armor.getMaxDmg() + helmet.getMaxDmg();
            }
            else if (job == Job.Bowman)
            {
                minDamage = (dex / 2) + (str / 4) + weapon.getMinDmg() + armor.getMinDmg() + helmet.getMinDmg();
                maxDamage = dex + (str / 2) + weapon.getMaxDmg() + armor.getMaxDmg() + helmet.getMaxDmg();
            }
            else if (job == Job.Ninja)
            {
                minDamage = (agi / 2) + (dex / 4) + weapon.getMinDmg() + armor.getMinDmg() + helmet.getMinDmg();
                maxDamage = agi + (dex / 2) + weapon.getMaxDmg() + armor.getMaxDmg() + helmet.getMaxDmg();
            }
            else
            {
                minDamage = (intel / 2) + (dex / 4) + weapon.getMinDmg() + armor.getMinDmg() + helmet.getMinDmg();
                maxDamage = intel + (dex / 2) + weapon.getMaxDmg() + armor.getMaxDmg() + helmet.getMaxDmg();
            }
        }

        public int getAvgDamage()
        {
            return (minDamage + maxDamage) / 2;
        }

        public int getMinDamage()
        {
            return minDamage;
        }

        public int getMaxDamage()
        {
            return maxDamage;
        }

        public int getDamage(Random r1)
        {
            r1 = new Random();
            calcDamage();
            return r1.Next(minDamage, maxDamage);
        }

        public void equipWeapon(Item wep)
        {
            if (wep.getSlot() == Item.Slot.Weapon)
            {
                weapon = wep;
                str += wep.getStr();
                dex += wep.getDex();
                agi += wep.getAgi();
                intel += wep.getInt();
                bonusStr += wep.getStr();
                bonusDex += wep.getDex();
                bonusAgi += wep.getAgi();
                bonusInt += wep.getInt();
            }
        }

        public void equipArmor(Item arm)
        {
            if (arm.getSlot() == Item.Slot.Armor)
            {
                armor = arm;
                str += arm.getStr();
                dex += arm.getDex();
                agi += arm.getAgi();
                intel += arm.getInt();
                bonusStr += arm.getStr();
                bonusDex += arm.getDex();
                bonusAgi += arm.getAgi();
                bonusInt += arm.getInt();
            }
        }

        public void equipHelmet(Item helm)
        {
            if (helm.getSlot() == Item.Slot.Helmet)
            {
                helmet = helm;
                str += helm.getStr();
                dex += helm.getDex();
                agi += helm.getAgi();
                intel += helm.getInt();
                bonusStr += helm.getStr();
                bonusDex += helm.getDex();
                bonusAgi += helm.getAgi();
                bonusInt += helm.getInt();
            }
        }

        public void unequipItem(Item item)
        {
            string temp = item.getName();
            item.setEquipped();
            str -= item.getStr();
            dex -= item.getDex();
            agi -= item.getAgi();
            intel -= item.getInt();
        }

        public Item getWeapon()
        {
            return weapon;
        }

        public Item getArmor()
        {
            return armor;
        }

        public Item getHelmet()
        {
            return helmet;
        }

        public void die()
        {
            exp = exp / 2;
            gold = (int)(gold / 1.5);
            curHealth = maxHealth;
            curMana = maxMana;
        }

        public void setSlotsInUse(int n)
        {
            slotsInUse = n;
        }

        public int getSlotsInUse()
        {
            return slotsInUse;
        }

        public void incSlots()
        {
            slotsInUse++;
        }

        public void decSlots()
        {
            slotsInUse--;
        }

        public int getDefense()
        {
            defense = armor.getArmor() + helmet.getArmor();
            return defense;
        }

        public bool weapEquipped()
        {
            if (weapon != null)
                return true;
            else
                return false;
        }

        public bool helmEquipped()
        {
            if (helmet != null)
                return true;
            else
                return false;
        }

        public bool armorEquipped()
        {
            if (armor != null)
                return true;
            else
                return false;
        }

        public int getCritChance()
        {
            critChance = armor.getCrit() + weapon.getCrit() + helmet.getCrit();
            return critChance;
        }

        public List<Item> getEquippedItems()
        {
            return equippedItems;
        }
    }
}
