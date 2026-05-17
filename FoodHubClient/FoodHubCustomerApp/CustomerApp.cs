using FoodHubCustomerApp.Model;
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
        UserReviewPage userReviewPage;

        public CustomerApp()
        {
            InitializeComponent();
            homePage = new HomePage(this) { Dock = DockStyle.Fill };
            addPostPage = new AddPostPage(this) { Dock = DockStyle.Fill };
            demoLoginPage = new DemoLoginPage(this) { Dock = DockStyle.Fill };
            userDetailPage = new UserDetailPage(this) { Dock = DockStyle.Fill };
            userReviewPage = new UserReviewPage(this) { Dock = DockStyle.Fill };
            //ChangeScreen(homePage);
            //ChangeScreen(addPostPage);
            //ChangeScreen(demoLoginPage);
        }
        private void CustomerApp_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Controls.Clear();
            this.Controls.Add(demoLoginPage);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Controls.Contains(homePage))
            {
                homePage.RefreshData();
            }
            else if (this.Controls.Contains(userReviewPage))
            {
                userReviewPage.Refresh();
            }
        }

        public void ChangeScreen(object sender, int action, UserRsp userData, object data = null)
        {
            if (action == 2002)
            {
                if (data != null)
                {
                    this.Controls.Clear();
                    homePage.UserData = userData;
                    homePage.SearchText = data.ToString();
                    this.Controls.Add(homePage);
                }
            } else if (action == 1001)
            {
                if (userData != null)
                {
                    this.Controls.Clear();
                    homePage.UserData = userData;
                    this.Controls.Add(homePage);
                }
            } else if (action == 0)
            {
                this.Controls.Clear();
                this.Controls.Add(demoLoginPage);
            }
            else if (action == 3)
            {
                if (userData != null)
                {
                    this.Controls.Clear();
                    userDetailPage.UserData = userData;
                    this.Controls.Add(userDetailPage);
                }
            }
            if (sender is DemoLoginPage)
            {
                if (action == 0)
                {
                    if (userData != null)
                    {
                        this.Controls.Clear();
                        homePage.UserData = userData;
                        this.Controls.Add(homePage);
                    }
                }
            }
            if (sender is HomePage)
            {
                if (action == 4)
                {
                    if (userData != null && data != null)
                    {
                        this.Controls.Clear();
                        userReviewPage.UserData = userData;
                        userReviewPage.ResPrevious = (RestaurantRecommendationRsp)data;
                        this.Controls.Add(userReviewPage);
                    }
                }
            }
            // Back Button from UserDetailPage to HomePage (Patt เอ้ง)
            if (sender is UserDetailPage)
            {
                if (action == 4)
                {
                    if (userData != null)
                    {
                        this.Controls.Clear();
                        homePage.UserData = userData;
                        this.Controls.Add(homePage);
                    }
                }
            }
            if (sender is UserReviewPage)
            {
                if (action == 4)
                {
                    if (userData != null && data != null)
                    {
                        this.Controls.Clear();
                        addPostPage.UserData = userData;
                        addPostPage.ResPrevious = (RestaurantRecommendationRsp)data;
                        this.Controls.Add(addPostPage);
                    }
                }
            }
            if (sender is AddPostPage)
            {
                // Back Button from AddPostPage to HomePage (Patt เอ้ง)
                if (action == 4)
                {
                    if (userData != null)
                    {
                        this.Controls.Clear();
                        homePage.UserData = userData;
                        this.Controls.Add(homePage);
                    }
                }
                else if (action == 5)
                {
                    if (userData != null && data != null)
                    {
                        this.Controls.Clear();
                        userReviewPage.UserData = userData;
                        userReviewPage.ResPrevious = (RestaurantRecommendationRsp)data;
                        this.Controls.Add(userReviewPage);
                    }
                }
            }
        }
    }
}
