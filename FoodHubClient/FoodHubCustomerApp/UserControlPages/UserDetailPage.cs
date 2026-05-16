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

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FoodHubCustomerApp.UserControlComponents;

namespace FoodHubCustomerApp.UserControlPages
{
    public partial class UserDetailPage : UserControl
    {
        CustomerApp form;
        private UserRsp userData;
        private bool useMockData = false;

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
            navBarControl.RefreshUserProfile();

            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "List of Promotions";
            headRec.Dock = DockStyle.Top;
            rightSectionHeaderControl.Controls.Add(headRec);
        }

        private void SetupEventHandlers()
        {
            navBarControl.LogoClicked += (s, e) => form.ChangeScreen(this, 0, userData);
            navBarControl.HeartClicked += (s, e) => MessageBox.Show("การกดถูกใจ");
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => form.ChangeScreen(this, 3, userData);

            flowLayoutPanel.Resize += (s, e) => ResizePromotions();
        }

        private void LoadExpirePromotions()
        {
            List<PromotionExpireItem> items;

            if (useMockData)
            {
                items = GetMockExpireData();
            }
            else
            {
                items = Service.GetCustomerProfile(userData.Id).ActivePromotions.Select(p => new PromotionExpireItem
                {
                    Title = p.Title,
                    ExpireDate = p.EndDate.ToString("dd/MM/yyyy")
                }).ToList();
            }

            DisplayPromotions(items);
        }

        private void DisplayPromotions(List<PromotionExpireItem> items)
        {
            flowLayoutPanel.SuspendLayout();
            flowLayoutPanel.Controls.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                var listItem = new PromotionExpireListControl(items[i]);
                listItem.Width = flowLayoutPanel.ClientSize.Width - (listItem.Margin.Left + listItem.Margin.Right);

                flowLayoutPanel.Controls.Add(listItem);
            }

            flowLayoutPanel.ResumeLayout(false);
            ResizePromotions();
        }

        private void ResizePromotions()
        {
            if (flowLayoutPanel.Controls.Count == 0) return;
            flowLayoutPanel.AutoScroll = false;
            flowLayoutPanel.SuspendLayout();

            int panelWidth = flowLayoutPanel.Width;
            int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;

            bool needVerticalScroll = (flowLayoutPanel.Controls.Count * 80) > flowLayoutPanel.Height;
            int verticalPadding = needVerticalScroll ? scrollBarWidth : 0;

            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                if (ctrl is PromotionExpireListControl card)
                {
                    int targetWidth = panelWidth - (card.Margin.Left + card.Margin.Right) - verticalPadding - 4;
                    if (targetWidth < 0) targetWidth = 0;

                    if (card.Width != targetWidth)
                    {
                        card.Width = targetWidth;
                    }
                }
            }

            flowLayoutPanel.ResumeLayout(true);
            flowLayoutPanel.AutoScroll = true;

            flowLayoutPanel.HorizontalScroll.Maximum = 0;
            flowLayoutPanel.HorizontalScroll.Visible = false;

            flowLayoutPanel.PerformLayout();
            flowLayoutPanel.Invalidate();
        }

        private List<PromotionExpireItem> GetMockExpireData()
        {
            var list = new List<PromotionExpireItem>();
            for (int i = 1; i <= 25; i++)
            {
                list.Add(new PromotionExpireItem
                {
                    Title = $"Promotion Title {i}",
                    ExpireDate = "99/12/2077"
                });
            }
            return list;
        }
    }
}
