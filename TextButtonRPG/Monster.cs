using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextButtonRPG
{
    class Monster
    {
        int maxHealth, maxMana, curHealth, curMana, exp, level, gold, minDamage, maxDamage;
        string name;
        string[] names = { "Taargus", "Margatron", "Darbadoo", "Plotiscre", "Mort", "Zort", "Zombie", "Skeleton", "MegaDragon" };
        List<Item> dropList = new List<Item>();
        int namesSize = 5;

        public Monster(string nm)
        {
            name = nm;
        }

        public Monster(string nm = "", int h = 50, int m = 50, int e = 10, int lvl = 1, int g = 5)
        {
            name = nm;
            maxHealth = h;
            maxMana = m;
            curHealth = maxHealth;
            curMana = maxMana;
            exp = e;
            level = lvl;
            gold = g;
        }

        public int getLevel()
        {
            return level;
        }

        public string getMonsterInfo()
        {
            return "Name: " +name+ "\r\nHealth: " + curHealth + "/" + maxHealth + "   |   Mana: " + curMana + "/" + maxMana + "\r\nLevel: " + level; 
        }

        public string getName()
        {
            return name;
        }

        public int getCurHealth()
        {
            return curHealth;
        }

        public int getExp()
        {
            return exp;
        }

        public int getGold()
        {
            return gold;
        }

        public void setCurHealth(int n)
        {
            curHealth = n;
        }

        public string randomMonsterName()
        {
            Random r = new Random();
            int index = r.Next(0, namesSize);
            return names[index];
        }

        public void setMonsterStats(string monsterName, Random r1)
        {
            switch (monsterName)
            {
                case "Taargus":
                    maxHealth = 15;
                    maxMana = 5;
                    curHealth = 15;
                    curMana = 5;
                    exp = 2;
                    level = 1;
                    gold = r1.Next(5, 10);
                    minDamage = 3;
                    maxDamage = 5;
                    dropList.Add(new Item("Feather Bow"));
                    dropList.Add(new Item("Feather Axe"));
                    dropList.Add(new Item("Feather Armor"));
                    break;
                case "Zort":
                    maxHealth = 25;
                    maxMana = 5;
                    curHealth = 25;
                    curMana = 5;
                    exp = 3;
                    level = 2;
                    gold = r1.Next(6, 11);
                    minDamage = 4;
                    maxDamage = 5;
                    dropList.Add(new Item("Feather Sword"));
                    dropList.Add(new Item("Feather Helmet"));
                    dropList.Add(new Item("Feather Dagger"));
                    dropList.Add(new Item("Feather Staff"));
                    break;
                case "Margatron":
                    maxHealth = 75;
                    maxMana = 20;
                    curHealth = 75;
                    curMana = 20;
                    exp = 10;
                    level = 3;
                    gold = r1.Next(8, 15);
                    minDamage = 6;
                    maxDamage = 9;
                    dropList.Add(new Item("Wooden Sword"));
                    dropList.Add(new Item("Wooden Staff"));
                    break;
                case "Mort":
                    maxHealth = 90;
                    maxMana = 25;
                    curHealth = 90;
                    curMana = 5;
                    exp = 13;
                    level = 4;
                    gold = r1.Next(13, 17);
                    minDamage = 8;
                    maxDamage = 11;
                    dropList.Add(new Item("Wooden Axe"));
                    dropList.Add(new Item("Wooden Bow"));
                    dropList.Add(new Item("Wooden Helm"));
                    break;
                case "Darbadoo":
                    maxHealth = 115;
                    maxMana = 60;
                    curHealth = 115;
                    curMana = 60;
                    exp = 20;
                    level = 5;
                    gold = r1.Next(15, 25);
                    minDamage = 10;
                    maxDamage = 15;
                    break;
                default:
                    maxHealth = 50;
                    maxMana = 25;
                    curHealth = 50;
                    curMana = 25;
                    exp = 5;
                    level = 1;
                    gold = r1.Next(5, 10);
                    minDamage = 15;
                    maxDamage = 20;
                    break;
            }
        }

        public int getDamage(Hero h)
        {
            Random r6 = new Random();
            int dmg = r6.Next(minDamage, maxDamage + 1) - h.getDefense();
            if (dmg < 0)
                dmg = 0;
            return dmg;
        }

        public List<Item> getDropList()
        {
            return dropList;
        }
    }
}
