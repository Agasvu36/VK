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
    public partial class Form1 : Form
    {
        int CountDigit;
        public Form1()
        {
            InitializeComponent();
            // обработать исключения!
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            CountDigit = 0;
            if (textBox1.Text != null && textBox1.Text != "")
            {
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if(Char.IsDigit(textBox1.Text[i]) == true)
                    {
                        CountDigit++;
                    }
                    
                }
                if (CountDigit == textBox1.Text.Length)
                {

                    GetInfo();

                }
                else
                {
                    MessageBox.Show("ID введён неправильно", "Ошибка");
                }

            }
            else
            {
                MessageBox.Show("ID введён неправильно","Ошибка");
            }
        }

        private void GetInfo() 
        {
            var api_user = new VkApi();

            api_user.Authorize(new ApiAuthParams
            {
                AccessToken = UserToken.Text
            });

            var UserInfo = api_user.Users.Get(new long[] { Convert.ToInt32(textBox1.Text) }, VkNet.Enums.Filters.ProfileFields.Photo200Orig).FirstOrDefault();

            if (UserInfo.IsDeactivated == false & UserInfo.IsClosed == false)
            {
                pictureBox1.Image = null;

                listBox1.Items.Clear();
                listBox3.Items.Clear();
                listBox3.Items.Add($"Имя пользоваетля : {Encoding.UTF8.GetString(Encoding.Default.GetBytes(UserInfo.FirstName))}");
                listBox3.Items.Add($"Фамилия пользователя : {Encoding.UTF8.GetString(Encoding.Default.GetBytes(UserInfo.LastName))}");
                pictureBox1.ImageLocation = UserInfo.Photo200Orig.OriginalString;

                var List = api_user.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = Convert.ToInt32(textBox1.Text),
                    Fields = VkNet.Enums.Filters.ProfileFields.All
                });
                int date;
                int summdate = 0;
                int count = 0;
                foreach (User user in List)
                {
                    if(user.BirthDate != null && DateTime.Parse(user.BirthDate).Year != 2021)
                    {
                        date = DateTime.Now.Year - DateTime.Parse(user.BirthDate).Year;
                        if (DateTime.Parse(user.BirthDate).Date > DateTime.Today)
                        {
                            date--;
                        }
                        summdate += date;
                        count++;
                    }
                    else
                    {
                        date = 0;
                    }
                    
                    listBox1.Items.Add($"{Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName))} {Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.LastName))} ID : {user.Id}");
                }
                summdate /= count;
                listBox3.Items.Add($"Возраст пользователя : {summdate} (путём подсчёта среднего возраста друзей)");
                listBox3.Items.Add($"Количество друзей : {listBox1.Items.Count}");
            }
            else
            {
                pictureBox1.Image = null;
                listBox1.Items.Clear();
                listBox3.Items.Clear();
                listBox3.Items.Add("Пользователь удалён или заблокирован");
            };

            //var getFollowers = api_user.Groups.GetMembers(new GroupsGetMembersParams()
            //{
            //    GroupId = GroupID.Text,
            //    Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            //});

            //foreach (User user in getFollowers)
            //{
            //    listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName)));
            //}
        }
    }
}
