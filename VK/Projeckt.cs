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
    public partial class Projeckt : Form
    {
        List<int> LikeList = new List<int>();
        List<int> LikeListSorted = new List<int>();
        List<int> ViewList = new List<int>();
        List<int> IDList = new List<int>();
        Graphics g;
        Pen pen;
        int x, y;
        public Projeckt()
        {
            InitializeComponent();
            g = CreateGraphics();
        }
        private void GetInfo()
        {
            LikeListSorted.Clear();
            ViewList.Clear();
            LikeList.Clear();
            IDList.Clear();
            var api_user = new VkApi();
            api_user.Authorize(new ApiAuthParams
            {
                AccessToken = UserToken.Text
            });
            var List = api_user.Wall.Get(new WallGetParams
            {
                OwnerId = Convert.ToInt32(textBox2.Text)
            });
            foreach(var item in List.WallPosts)
            {
                LikeList.Add(Convert.ToInt32(item.Likes.Count));
                ViewList.Add(Convert.ToInt32(item.Views.Count));
                IDList.Add(Convert.ToInt32(item.Id));
            }
        }

        private void DrawGraphics()
        {
            g.Clear(Color.White);
            ViewList.Sort();
            foreach(int item in LikeList)
            {
                LikeListSorted.Add(item);
            }
            LikeListSorted.Sort();
            x = 130;
            y = 400;

            g.DrawLine(new Pen(Color.Black, 4f), 180, 400, 800, 400);
            g.DrawLine(new Pen(Color.Black, 4f), 180, 400, 180, 50);

            for (int i = 0; i < LikeList.Count(); i++)
            {
                y = 400 - LikeListSorted[i]*350 / LikeListSorted.Last();
                x += 400 / IDList.Count;
                g.DrawString(Convert.ToString(LikeList[i]), new Font("Times New Roman", 8, FontStyle.Regular), new SolidBrush(Color.Black), new PointF(150, y));
                g.DrawString(Convert.ToString(IDList[i]), new Font("Times New Roman", 8, FontStyle.Regular), new SolidBrush(Color.Black), new PointF(x, 410));
            }

            x = 180;
            y = 400;

            for (int i = 0; i <LikeList.Count(); i++)
            {
                x += 370 / IDList.Count;
                y = 400 - LikeList[i] * 350 / LikeListSorted.Last();
                
                g.DrawLine(new Pen(Color.Red, 8f), x, 400, x, y);
                x += 8;
                y = 400 - ViewList[i] * 350 * LikeListSorted.Last();
                g.DrawLine(new Pen(Color.Blue, 8f), x, 400, x, y);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInfo();
            DrawGraphics();
        }
    }
}
