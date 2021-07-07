﻿using System;
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

        private async void GetInfoAsync()
        {
            await Task.Run(() => GetInfo());
        }

        private void GetInfo()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            double Check = 0;
            var api_user = new VkApi();
            var api_group = new VkApi();
            api_group.Authorize(new ApiAuthParams
            {
                AccessToken = "c2e2a8e85982fe45dcb6dcbaa428d0715a425150f21b8c6b3d40da47c73a127b745161d9178113a13d0df"
            });
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
            GetInfoAsync();
            listBox1.Items.Add("Загрузка...");
            listBox2.Items.Add("Загрузка...");
            listBox3.Items.Add("Загрузка...");
            listBox4.Items.Add("Загрузка...");
        }
    }
}