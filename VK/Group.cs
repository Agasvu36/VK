using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VK
{
    public partial class Group : Form
    {
        public Group()
        {
            InitializeComponent();
        }

       

        private void GetInfo()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            double Check = 0;
            var api_user = new VkApi();

            api_user.Authorize(new ApiAuthParams
            {

                AccessToken = UserToken.Text
            });

            string[] ID = textBox1.Text.Split(',');
            if (textBox1.Text != null && textBox1.Text != "" && textBox2.Text != null && textBox2.Text != "" && double.TryParse(textBox2.Text, out Check))
            {
                for (int i = 0; i < ID.Length; i++)
                {
                    if (double.TryParse(ID[i], out Check))
                    {
                        var List = api_user.Wall.Get(new WallGetParams
                        {
                            OwnerId = Convert.ToInt32(ID[i])
                        });
                        listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(List.WallPosts.First().Text)));
                        listBox2.Items.Add(List.WallPosts.First().OwnerId);
                        listBox3.Items.Add(List.WallPosts.First().Likes.Count);
                        listBox4.Items.Add(List.WallPosts.First().Views.Count);

                        var repost = api_user.Wall.Repost(@object: $"wall{List.WallPosts.First().OwnerId}_{List.WallPosts.First().Id}", message: "tex", groupId: 205694137, markAsAds: false);
                    }
                }

            }
            else
            {
                MessageBox.Show("Ошибка", "Неправильные параметры");
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInfo();
        }
    }
}
