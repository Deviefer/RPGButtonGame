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
    public partial class Intro : Form
    {
        bool skipIntroduction = false;

        public Intro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            skipIntro(true);
            Dispose();
        }

        public void skipIntro(bool b)
        {
            skipIntroduction = b;
        }

        public bool getSkipIntro()
        {
            return skipIntroduction;
        }
    }
}
