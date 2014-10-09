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
    public partial class ClassSelector : Form
    {
        public Hero.Job job;

        public ClassSelector()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            job = Hero.Job.Warrior;
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            job = Hero.Job.Mage;
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            job = Hero.Job.Bowman;
            Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            job = Hero.Job.Ninja;
            Dispose();
        }
    }
}
