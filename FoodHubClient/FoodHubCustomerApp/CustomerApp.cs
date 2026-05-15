using FoodHubCustomerApp.UserControlPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp
{
    public partial class CustomerApp : Form
    {
        HomePage homePage;
        AddPostPage addPostPage;
        DemoLoginPage demoLoginPage;
        UserDetailPage userDetailPage;

        public CustomerApp()
        {
            InitializeComponent();
            homePage = new HomePage(this) { Dock = DockStyle.Fill };
            addPostPage = new AddPostPage(this) { Dock = DockStyle.Fill };
            demoLoginPage = new DemoLoginPage(this) { Dock = DockStyle.Fill };
            userDetailPage = new UserDetailPage(this) { Dock = DockStyle.Fill };
            //ChangeScreen(homePage);
            //ChangeScreen(addPostPage);
            //ChangeScreen(demoLoginPage);
        }
        private void CustomerApp_Load(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(demoLoginPage);
        }

        public void ChangeScreen(UserControl targetScreen)
        {
            this.Controls.Clear();
            this.Controls.Add(targetScreen);
            targetScreen.BringToFront();
        }

        public void ChangeScreen(object sender, int userId, string username, int action)
        {
            if (sender is DemoLoginPage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    homePage.UserId = userId;
                    homePage.Username = username;
                    this.Controls.Add(homePage);
                }
            }
            if (sender is HomePage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    //Detail.UserId = userId;
                    //this.Controls.Add(demoLoginPage);
                }
            }
        }
    }
}
