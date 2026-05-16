using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubCustomerApp.Logics;
using FoodHubCustomerApp.Model;

namespace FoodHubCustomerApp.UserControlPages
{
    public partial class UserReviewPage : UserControl
    {
        CustomerApp form;

        private UserRsp userData;

        private RestaurantRecommendationRsp resPrevious;

        public RestaurantRecommendationRsp ResPrevious
        {
            get => resPrevious;
            set
            {
                resPrevious = value;
                if (resPrevious != null)
                {
                    lblRestaurantName.Text = resPrevious.Name;
                    lblCategory.Text = resPrevious.Category;
                    lblRatingScore.Text = resPrevious.OverallRating.ToString("0.00") + "/5.00";
                    lblRestaurantDescription.Text = resPrevious.Address;
                }
            }
        }

        public UserRsp UserData
        {
            get => userData;
            set
            {
                userData = value;
                UserSession.Username = userData.Username;
                navBarControl.UserData = userData;
                //navBarControl.RefreshUserProfile();
            }
        }

        public UserReviewPage(CustomerApp form)
        {
            InitializeComponent();
            this.form = form;
            SetupUI();
            SetupEventHandlers();
        }
        public void RefreshData()
        {
            //
        }

        private void SetupUI()
        {
            // ตั้งค่าบัญชีผู้ใช้
            navBarControl.RefreshUserProfile();
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => form.ChangeScreen(this, 0, userData);
            navBarControl.HeartClicked += (s, e) => form.ChangeScreen(this, 4, userData, resPrevious);
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => form.ChangeScreen(this, 3, userData);
        }
    }
}
