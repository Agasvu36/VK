using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VK
{
    public partial class Menu : Form
    {
        Form1 User;
        Group Group;
        public Menu()
        {
            InitializeComponent();
            User = new Form1();
            Group = new Group();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Group.ShowDialog();
        }
    }
}
