using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextButtonRPG
{
    public partial class Shop : Form
    {
        List<Item> itemList;
        List<string> itemNames;
        Hero player;

        public Shop()
        {
            InitializeComponent();
            itemList = new List<Item>();
            itemNames = new List<string>();
        }

        public Shop(int mapId, Hero h)
        {
            InitializeComponent();
            itemList = new List<Item>();
            itemNames = new List<string>();
            player = new Hero(h.getGold(), h.inventory, h.invNames, h.getSlotsInUse());
            populateShop(mapId);
            label3.Text = "Gold: " + player.getGold();
        }
        
        public void populateShop(int mapId)
        {
            switch (mapId)
            {
                case 0:
                    itemList.Add(new Item("Wooden Sword"));
                    itemNames.Add("Wooden Sword");
                    itemList.Add(new Item("Wooden Staff"));
                    itemNames.Add("Wooden Staff");
                    itemList.Add(new Item("Wooden Axe"));
                    itemNames.Add("Wooden Axe");
                    itemList.Add(new Item("Wooden Dagger"));
                    itemNames.Add("Wooden Dagger");
                    itemList.Add(new Item("Wooden Bow"));
                    itemNames.Add("Wooden Bow");
                    itemList.Add(new Item("Wooden Armor"));
                    itemNames.Add("Wooden Armor");
                    itemList.Add(new Item("Wooden Helm"));
                    itemNames.Add("Wooden Helm");
                    break;
                default:
                    break;
            }
            listBox1.DataSource = itemNames;
            listBox2.DataSource = player.invNames;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = itemList[listBox1.SelectedIndex].printItemInfo();
            textBox4.Text += "\r\nPrice: " + itemList[listBox1.SelectedIndex].getBuyPrice() + " gold";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (player.getGold() >= itemList[listBox1.SelectedIndex].getBuyPrice())
                {
                    player.addItem(new Item(itemList[listBox1.SelectedIndex].getName()));
                    player.setGold(player.getGold() - itemList[listBox1.SelectedIndex].getBuyPrice());
                    label3.Text = "Gold: " + player.getGold();
                    listBox2.DataSource = null;
                    listBox2.DataSource = player.invNames;
                }
                else
                    MessageBox.Show("Not Enough Gold");
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = player.getInventory(listBox1.SelectedIndex).printItemInfo();
            textBox4.Text += "\r\nSell Price: " + player.getInventory(listBox1.SelectedIndex).getSellPrice() + " gold";
        }

        public Hero getPlayer()
        {
            return player;
        }
    }
}
