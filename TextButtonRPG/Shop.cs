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

        public Shop()
        {
            InitializeComponent();
            itemList = new List<Item>();
        }
        
        public void populateShop(int mapId)
        {
            switch (mapId)
            {
                case 0:
                    itemList.Add(new Item("Wooden Sword"));
                    itemList.Add(new Item("Wooden Staff"));
                    itemList.Add(new Item("Wooden Axe"));
                    itemList.Add(new Item("Wooden Dagger"));
                    itemList.Add(new Item("Wooden Bow"));
                    itemList.Add(new Item("Wooden Armor"));
                    itemList.Add(new Item("Wooden Helm"));
                    break;
                default:
                    break;
            }
        }
    }
}
