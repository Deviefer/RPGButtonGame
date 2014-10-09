using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace TextButtonRPG
{
    public partial class Form1 : Form
    {
        Hero player = new Hero();
        Monster m = new Monster();
        List<string> monsterList;
        string monsterName;
        Item[] inventory = new Item[50];
        string[] invNames = new string[50];
        private bool inBattle = false;
        private bool autoAttack = false;
        private int monsterCount = 0, goldGained = 0, expGained = 0; //autoattack stats for fun
        Timer aa = new Timer();
        //ContextMenuStrip rightClickItem;

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Intro intro = new Intro();
            intro.ShowDialog();
            if (!intro.getSkipIntro())
            {
                NamingBox popup = new NamingBox();
                popup.ShowDialog();
                player.setName(popup.name);
                ClassSelector cs = new ClassSelector();
                cs.ShowDialog();
                player.setJob(cs.job);
                player.setSlotsInUse(0);
            }
            else
            {
                loadGame();
                intro.Dispose();
            }
            //rightClickItem = new ContextMenuStrip();
            //rightClickItem.Opening += new CancelEventHandler(rightClickItem_Open);

            textBox2.Text = player.getPlayerInfo();
            for (int i = 0; i < player.getSlotsInUse(); i++ )
                player.invNames[i] = player.getInventory(i).getName();
            listBox1.DataSource = player.invNames;
            //listBox1.ContextMenuStrip = rightClickItem;
        }

        //BATTLE/FIND MONSTER BUTTON
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!inBattle)
            {
                startBattle();
            }
            else if (inBattle)
            {
                attackMonster();
                attackPlayer();
                updateMonsterText();
                if (m.getCurHealth() <= 0)
                {
                    rollDrop();
                    dieMonster();
                }
                else if (player.getCurHealth() <= 0)
                {
                    diePlayer();
                }
                updatePlayerText();
            }
        }

        //UPDATES PLAYER INFO
        public void updatePlayerText()
        {
            textBox2.Text = player.getPlayerInfo();
        }

        //UPDATES MONSTER INFO
        public void updateMonsterText()
        {
            textBox3.Text = m.getMonsterInfo();
        }

        //ATTACK THE MONSTER
        public void attackMonster()
        {
            Random r1 = new Random();
            int dmg = player.getDamage(r1);
            int bonusDmg;
            if (r1.Next(100) < player.getCritChance())
            {
                m.setCurHealth(m.getCurHealth() - (dmg*2));
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - You CRIT for " + (dmg*2) + " damage!!!\r\n");
            }
            else
            {
                m.setCurHealth(m.getCurHealth() - dmg);
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - You attacked for " + dmg + " damage!\r\n");
            }
            if (player.getWeapon().getSuffix() == " of Heartbreak")
            {
                m.setCurHealth(m.getCurHealth() - 5);
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getWeapon().getBaseName() + "'s Heartbreak did 5 pure damage.\r\n");
            }
            if (player.getWeapon().getSuffix() == " of Shock")
            {
                bonusDmg = r1.Next(1, 9);
                m.setCurHealth(m.getCurHealth() - bonusDmg);
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getWeapon().getBaseName() + "'s Shock did " + bonusDmg + " electric damage.\r\n");
            }
            if (player.getWeapon().getSuffix() == " of Flame")
            {
                bonusDmg = r1.Next(10, 16); //16 because it goes up to 15
                m.setCurHealth(m.getCurHealth() - bonusDmg);
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getWeapon().getBaseName() + "'s Flame did " + bonusDmg + " fire damage.\r\n");
            }
        }

        //ATTACK THE PLAYER
        public void attackPlayer()
        {
            int mDmg = m.getDamage(player);
            player.setCurHealth(player.getCurHealth() - mDmg);
            textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - You were damaged by " + m.getName() + " for " + mDmg + " damage!\r\n");
        }

        //START THE BATTLE
        public void startBattle()
        {
            Random r1 = new Random();
            checkTier(r1);
            m = new Monster(monsterName);
            m.setMonsterStats(monsterName, r1);
            textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + m.getName() + " has appeared!\r\n");
            textBox3.Text = m.getMonsterInfo();
            inBattle = true;
            button1.Text = "Attack";
        }

        //DIE MONSTER
        public void dieMonster(bool aa = false)
        {
            inBattle = false;
            if (aa)
            {
                goldGained += m.getGold();
                expGained += m.getExp();
            }
            player.setExp(player.getExp() + m.getExp());
            player.setGold(player.getGold() + m.getGold());
            if (player.getExp() >= player.getExpTable(player.getLevel()))
            {
                player.setExp(player.getExp() - player.getExpTable(player.getLevel()));
                player.levelUp();
            }
            textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - You have gained " + m.getExp() + " exp and " + m.getGold() + " gold!\r\n");
            button1.Text = "Find Monster";
            textBox3.Text = "";
        }

        //DIE PLAYER
        public void diePlayer()
        {
            textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - You died and lost " + player.getExp() / 2 + " exp and " + (int)(player.getGold() / 1.5) + " gold.\r\n");
            player.die();
            inBattle = false;
            button1.Text = "Find Monster";
            textBox3.Text = "";
        }

        //HANDLES DIFFERENT SELECTIONS IN INVENTORY
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                textBox4.Text = player.getInventory(listBox1.SelectedIndex).printItemInfo();
            }
        }

        /*private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }*/

        /*private void rightClickItem_Open(object sender, CancelEventArgs e)
        {
            rightClickItem.Items.Clear();
            if (listBox1.SelectedIndex != -1)
            {
                if (player.getInventory(listBox1.SelectedIndex].getSlot() == Item.Slot.Weapon)
                {
                    rightClickItem.Items.Add("Equip Weapon");
                    rightClickItem.Click += new System.EventHandler(equip_weapon);
                }
                else if (player.getInventory(listBox1.SelectedIndex].getSlot() == Item.Slot.Armor)
                {
                    rightClickItem.Items.Add("Equip Armor");
                    rightClickItem.Click += new System.EventHandler(equip_armor);
                }
                else if (player.getInventory(listBox1.SelectedIndex].getSlot() == Item.Slot.Helmet)
                {
                    rightClickItem.Items.Add("Equip Helmet");
                    rightClickItem.Click += new System.EventHandler(equip_helmet);
                }
            }
        }

        public void equip_helmet(object sender, EventArgs e)
        {
            player.equipHelmet(player.getInventory(listBox1.SelectedIndex]);
            updatePlayerText();
            if (player.getHelmet() != player.getInventory(listBox1.SelectedIndex])
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getInventory(listBox1.SelectedIndex].getName() + " has been equipped.\r\n");
        }

        public void equip_armor(object sender, EventArgs e)
        {
            player.equipArmor(player.getInventory(listBox1.SelectedIndex]);
            updatePlayerText();
            if (player.getArmor() != player.getInventory(listBox1.SelectedIndex])
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getInventory(listBox1.SelectedIndex].getName() + " has been equipped.\r\n");
        }

        public void equip_weapon(object sender, EventArgs e)
        {
            player.equipWeapon(player.getInventory(listBox1.SelectedIndex]);
            updatePlayerText();
            if (player.getWeapon() != player.getInventory(listBox1.SelectedIndex])
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getInventory(listBox1.SelectedIndex].getName() + " has been equipped.\r\n");
        }*/

        public void randomMonster()
        {
            Random r1 = new Random();
            double multiplier = 1 + r1.NextDouble();
            m = new Monster(m.randomMonsterName(),
                            (int)(((double)player.getLevel() + 50) * multiplier),       //health
                            (int)(((double)player.getLevel() + 25) * multiplier),       //mana
                            (int)(((double)player.getLevel() * 5) * multiplier),        //exp
                            (int)((double)player.getLevel() * multiplier),              //level
                            (int)(((double)player.getLevel() * 10) * multiplier));      //gold
        }

        //CHECKS MONSTER TIER TO FIGHT
        public void checkTier(Random r1)
        {
            monsterList = new List<string>();
            if (player.getLevel() < 3)
            {
                monsterList.Add("Taargus");
                monsterList.Add("Zort");
                monsterName = monsterList[r1.Next(0, monsterList.Count)];
            }
            else if (player.getLevel() >= 3 && player.getLevel() < 5)
            {
                monsterList.Add("Zort");
                monsterList.Add("Margatron");
                monsterList.Add("Mort");
                monsterName = monsterList[r1.Next(0, monsterList.Count)];
            }
            else if (player.getLevel() >= 5 && player.getLevel() < 8)
            {
                monsterList.Add("Margatron");
                monsterList.Add("Mort");
                monsterList.Add("Darbadoo");
                monsterName = monsterList[r1.Next(0, monsterList.Count)];
            }
        }

        //SAVE GAME BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            saveGame();
        }

        //LOAD GAME BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            loadGame();
        }

        //LOADS THE GAME FROM %username%\MyDocuments\RPGButton
        public void loadGame()
        {
            string loadPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RPGButton", "save.yolo");
            string line;
            using (System.IO.StreamReader file = new System.IO.StreamReader(loadPath))
            {
                try
                {
                    line = file.ReadLine(); //name
                    player.setName(line);
                    line = file.ReadLine(); //Level
                    player.setLevel(Convert.ToInt32(line));
                    line = file.ReadLine(); //Max Health
                    player.setMaxHealth(Convert.ToInt32(line));
                    line = file.ReadLine(); //Max Mana
                    player.setMaxMana(Convert.ToInt32(line));
                    line = file.ReadLine(); //Exp
                    player.setExp(Convert.ToInt32(line));
                    line = file.ReadLine(); //Class
                    if (line == "Warrior")
                        player.setJob(Hero.Job.Warrior);
                    else if (line == "Bowman")
                        player.setJob(Hero.Job.Bowman);
                    else if (line == "Ninja")
                        player.setJob(Hero.Job.Ninja);
                    else
                        player.setJob(Hero.Job.Mage);
                    line = file.ReadLine(); //Str
                    player.setStr(Convert.ToInt32(line));
                    line = file.ReadLine(); //Agi
                    player.setAgi(Convert.ToInt32(line));
                    line = file.ReadLine(); //Dex
                    player.setDex(Convert.ToInt32(line));
                    line = file.ReadLine(); //Intel
                    player.setInt(Convert.ToInt32(line));
                    line = file.ReadLine(); //Gold
                    player.setGold(Convert.ToInt32(line));
                    line = file.ReadLine(); //# items in inventory
                    player.setSlotsInUse(Convert.ToInt32(line));
                    for (int i = 0; i < player.getSlotsInUse(); i++)
                    {

                        line = file.ReadLine(); //item name
                        //player.getInventory(i] = new Item(line);
                        player.inventory[i] = new Item(line);
                        line = file.ReadLine(); //item suffix
                        player.getInventory(i).setSuffix(line);
                        player.getInventory(i).handleSuffix(line);
                        player.invNames[i] = player.getInventory(i).getName();
                    }
                    //update health/inventory to sync with form
                    updateInventory();
                    player.setCurHealth(player.getMaxHealth());
                    player.setCurMana(player.getMaxMana());
                }
                catch
                {
                }
            }
            updatePlayerText();
            textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getName() + " file loaded. \r\n");
        }

        //SAVES THE GAME TO %username%\MyDocuments\RPGButton\save.yolo
        public void saveGame()
        {
            string savePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!System.IO.Directory.Exists(savePath + @"\RPGButton"))
            {
                System.IO.Directory.CreateDirectory(savePath + @"\RPGButton");
            }
            savePath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RPGButton", "save.yolo");
            using(System.IO.StreamWriter file = new System.IO.StreamWriter(savePath))
            { 
                try
                {
                    file.Write(player.getPlayerStats() + "\r\n");
                    file.WriteLine(player.getSlotsInUse());
                    for (int i = 0; i < player.getSlotsInUse(); i++)
                    {
                        file.WriteLine(player.getInventory(i).getBaseName());
                        file.WriteLine(player.getInventory(i).getSuffix());
                    }
                }
                catch
                {
                }
            }
            textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - Game Saved. \r\n");
        }

        //ROLLS FOR AN ITEM AFTER MONSTER DEATH
        public void rollDrop()
        {
            Random chance = new Random();
            if ((chance.Next(100) + 1) < 20)
            {
                if (m.getDropList().Count > 0)
                {
                    Item item = new Item(m.getDropList()[chance.Next(0, m.getDropList().Count)].getName());
                    item.randomSuffix(chance);
                    //player.getInventory(player.getSlotsInUse()] = item;
                    player.addItem(item);
                    player.invNames[player.getSlotsInUse()] = item.getName();
                    updateInventory();
                    textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - You have found " + item.getName() + "!\r\n");
                }
            }
        }

        //UPDATE INVENTORY
        public void updateInventory()
        {
            for (int i = 0; i < player.getSlotsInUse(); i++ )
            {
                if (player.invNames[i].Contains(" (equipped)") && !player.getInventory(i).isEquipped())
                {
                    player.invNames[i] = player.invNames[i].Substring(0, player.invNames[i].Length - 11);
                }
            }
            listBox1.DataSource = null;
            listBox1.DataSource = player.invNames;
        }

        //REMOVE BUTTON
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                List<Item> temp = new List<Item>(player.inventory);
                List<string> temp2 = new List<string>(player.invNames);
                temp.RemoveAt(listBox1.SelectedIndex);
                player.inventory = temp.ToArray();
                temp2.RemoveAt(listBox1.SelectedIndex);
                player.invNames = temp2.ToArray();
                player.decSlots();
                updateInventory();
            }
        }

        //EQUIP BUTTON
        private void button5_Click(object sender, EventArgs e)
        {
            if (player.getInventory(listBox1.SelectedIndex).getSlot() == Item.Slot.Helmet)
            {
                if (player.helmEquipped())
                    player.unequipItem(player.getHelmet());
                player.equipHelmet(player.getInventory(listBox1.SelectedIndex));
                player.getInventory(listBox1.SelectedIndex).setEquipped();
                updatePlayerText();
                player.invNames[listBox1.SelectedIndex] += " (equipped)";
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getInventory(listBox1.SelectedIndex).getName() + " has been equipped.\r\n");
                updateInventory();
            }
            else if (player.getInventory(listBox1.SelectedIndex).getSlot() == Item.Slot.Armor)
            {
                if (player.armorEquipped())
                {
                    player.unequipItem(player.getArmor());
                }
                player.equipArmor(player.getInventory(listBox1.SelectedIndex));
                player.getInventory(listBox1.SelectedIndex).setEquipped();
                updatePlayerText();
                player.invNames[listBox1.SelectedIndex] += " (equipped)";
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getInventory(listBox1.SelectedIndex).getName() + " has been equipped.\r\n");
                updateInventory();
            }
            else if (player.getInventory(listBox1.SelectedIndex).getSlot() == Item.Slot.Weapon)
            {
                if (player.weapEquipped())
                    player.unequipItem(player.getWeapon());
                player.equipWeapon(player.getInventory(listBox1.SelectedIndex));
                player.getInventory(listBox1.SelectedIndex).setEquipped();
                updatePlayerText();
                player.invNames[listBox1.SelectedIndex] += " (equipped)";
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - " + player.getInventory(listBox1.SelectedIndex).getName() + " has been equipped.\r\n");
                updateInventory();
            }
        }

        //UP BUTTON
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                Item temp = new Item(player.getInventory(listBox1.SelectedIndex - 1));
                string temp2 = player.invNames[listBox1.SelectedIndex - 1];
                player.inventory[listBox1.SelectedIndex - 1] = player.getInventory(listBox1.SelectedIndex);
                player.inventory[listBox1.SelectedIndex] = temp;
                player.invNames[listBox1.SelectedIndex - 1] = player.invNames[listBox1.SelectedIndex];
                player.invNames[listBox1.SelectedIndex] = temp2;
                if (player.getInventory(listBox1.SelectedIndex).isEquipped())
                {
                    player.invNames[listBox1.SelectedIndex - 1] += " (equipped)";
                }
                else if (player.getInventory(listBox1.SelectedIndex - 1).isEquipped())
                    player.invNames[listBox1.SelectedIndex + 1] += " (equipped)";
                updateInventory();
            }
        }

        //DOWN BUTTON
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                if (player.getInventory(listBox1.SelectedIndex) != player.getInventory(player.getSlotsInUse() - 1))
                {
                    Item temp = new Item(player.getInventory(listBox1.SelectedIndex + 1));
                    string temp2 = player.invNames[listBox1.SelectedIndex + 1];
                    player.inventory[listBox1.SelectedIndex + 1] = player.getInventory(listBox1.SelectedIndex);
                    player.inventory[listBox1.SelectedIndex] = temp;
                    player.invNames[listBox1.SelectedIndex + 1] = player.invNames[listBox1.SelectedIndex];
                    player.invNames[listBox1.SelectedIndex] = temp2;
                    if (player.getInventory(listBox1.SelectedIndex).isEquipped())
                    {
                        player.invNames[listBox1.SelectedIndex + 1] += " (equipped)";
                    }
                    else if (player.getInventory(listBox1.SelectedIndex + 1).isEquipped())
                        player.invNames[listBox1.SelectedIndex - 1] += " (equipped)";
                    updateInventory();
                }
            }
        }

        //AUTOATTACK BUTTON
        private void button8_Click(object sender, EventArgs e)
        {
            if (autoAttack)
                autoAtk(false);
            else
                autoAtk(true);
        }

        //turn autoattack on/off
        private void autoAtk(bool on)
        {
            autoAttack = on;
            if (player.getAgi() < 501)
                timer1.Interval = 1000 - player.getAgi() * 2 + 5;
            else
                timer1.Interval = 5;
            if (autoAttack)
            {
                timer1.Start();
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - Autoattack started.\r\n");
            }
            else
            {
                timer1.Stop();
                textBox1.AppendText(DateTime.Now.ToShortTimeString() + " - Autoattack stopped. You killed " +monsterCount+ " monsters and gained " +expGained+ " exp and " +goldGained+ " gold.\r\n");
                monsterCount = 0;
                expGained = 0;
                goldGained = 0;
            }
        }

        //EVERY TICK OF THE TIMER
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!inBattle)
            {
                startBattle();
            }
            else if (inBattle)
            {
                attackMonster();
                attackPlayer();
                updateMonsterText();
                if (m.getCurHealth() <= 0)
                {
                    rollDrop();
                    dieMonster(true);
                    monsterCount++;
                }
                else if (player.getCurHealth() <= 0)
                {
                    diePlayer();
                }
                updatePlayerText();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Shop shop = new Shop(0, player);
            shop.Show();
        }
    }
}
