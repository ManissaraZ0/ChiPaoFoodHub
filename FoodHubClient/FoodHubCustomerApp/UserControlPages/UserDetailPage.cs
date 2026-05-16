using FoodHubCustomerApp.Logics;
using FoodHubCustomerApp.Model;
using FoodHubCustomerApp.UserControlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp.UserControlPages
{
    public partial class UserDetailPage : UserControl
    {
        CustomerApp form;

        private UserRsp userData;

        public UserRsp UserData
        {
            get => userData;
            set
            {
                userData = value;
                UserSession.Username = userData.Username;
                navBarControl.RefreshUserProfile();
                usernameTitle.Text = userData.Username;
                LoadExpirePromotions();
                navBarControl.UserData = userData;
            }
        }

        public UserDetailPage(CustomerApp form)
        {
            InitializeComponent();
            this.form = form;
            SetupUI();
            SetupEventHandlers();
        }

        private void SetupUI()
        {
            // ตั้งค่าบัญชีผู้ใช้
            navBarControl.RefreshUserProfile();

            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "My Promotions";
            headRec.Dock = DockStyle.Top;
            rightSectionHeaderControl.Controls.Add(headRec);
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => form.ChangeScreen(this, 0, userData);
            navBarControl.HeartClicked += (s, e) => MessageBox.Show("การกดถูกใจ");
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => form.ChangeScreen(this, 3, userData);
        }

        private void LoadExpirePromotions()
        {
            flowLayoutPanel.Controls.Clear();

            //var items = GetMockExpireData();

            var items = Service.GetCustomerProfile(userData.Id).ActivePromotions.Select(p => new PromotionExpireItem
            {
                Title = p.Title,
                ExpireDate = p.EndDate.ToString("dd/MM/yyyy")
            }).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                var listItem = new PromotionExpireListControl(items[i]);

                // ให้กว้างเต็มจอเหมือนเดิม (เว้นที่ให้ Scrollbar)
                listItem.Width = flowLayoutPanel.ClientSize.Width - 20;

                flowLayoutPanel.Controls.Add(listItem);
            }
        }

        //private List<PromotionExpireItem> GetMockExpireData()
        //{
        //    var list = new List<PromotionExpireItem>();
        //    for (int i = 1; i <= 10; i++) // สร้างตามจำนวนแถวในรูปคือ 6 แถว
        //    {
        //        list.Add(new PromotionExpireItem
        //        {
        //            Title = "Promotion Title", // ถ้าอยากให้เลขรันตามด้วย ก็ใส่ + i ตรงนี้ครับ
        //            ExpireDate = "99/12/2077"
        //        });
        //    }
        //    return list;
        //}
    }
}
