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
        public Form1()
        {
            InitializeComponent();
            // обработать исключения!
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var api_user = new VkApi();

            api_user.Authorize(new ApiAuthParams
            {
                AccessToken = textBox1.Text
            });
            var List = api_user.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.All
            });

            foreach (User user in List)
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName)));

            var getFollowers = api_user.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = "ru2ch",
                Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            });
            foreach (User user in getFollowers)
                listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName)));
        }
    }
}
