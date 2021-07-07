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
    public partial class Start : Form
    {
        string str;
        public Start()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            str = $"https://oauth.vk.com/authorize?client_id={textBox1.Text}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends,wall&response_type=token&v=5.52";
            textBox2.Text = str;
            textBox3.Text = $"https://oauth.vk.com/authorize?client_id={textBox1.Text}&group_ids={textBox4.Text}&display=page&https://oauth.vk.com/blank.html&scope=messages,wall&response_type=token&v=5.131";

        }
    }
}
