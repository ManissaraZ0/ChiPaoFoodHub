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
    public partial class HomePage : UserControl
    {
        CustomerApp form;

        private readonly ServiceMock _service;

        private UserRsp userData;

        public UserRsp UserData {
            get => userData;
            set { 
                userData = value;
                UserSession.Username = userData.Username;
                navBarControl.RefreshUserProfile();
            }
        }

        public HomePage(CustomerApp form)
        {
            InitializeComponent();
            this.form = form;
            //_service = new ServiceMock();

            // ตั้งค่า UI
            SetupUI();
            // ผูก Events ต่างๆ (รวมถึง Observer)
            SetupEventHandlers();

            // สั่งให้ Service ทำงาน
            //_service.FetchRestaurants();
        }

        private void SetupUI()
        {
            navBarControl.RefreshUserProfile();

            // สร้าง Header สำหรับร้านอาหารแนะนำ
            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "Recommendation Restaurants";
            headRec.Dock = DockStyle.Top;
            sectionHeaderControl.Controls.Add(headRec);
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => form.ChangeScreen(this, 0, userData);
            navBarControl.HeartClicked += (s, e) => MessageBox.Show("การกดถูกใจ");
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => form.ChangeScreen(this, 3, userData);

            // Layout Event
            flowContentLayoutPanel.SizeChanged += (s, e) => ResizeCards();

            // *** Observer: รอรับข้อมูลจาก Service เมื่อโหลดเสร็จ ***
            //_service.OnRestaurantsLoaded += UpdateRestaurantCards;
            var restaurants = Service.GetRecommendedRestaurants();
            UpdateRestaurantCards(restaurants);
        }

        // ฟังก์ชันนี้ทำงานอัตโนมัติเมื่อ Service สั่ง Invoke()
        private void UpdateRestaurantCards(List<RestaurantRecommendationRsp> items)
        {
            // ป้องกัน Thread ชนกัน กรณี Service ไปดึงข้อมูลแบบ Async
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateRestaurantCards(items)));
                return;
            }

            flowContentLayoutPanel.SuspendLayout();
            flowContentLayoutPanel.Controls.Clear();

            // วาดการ์ด
            foreach (var item in items)
            {
                var card = new ItemCardControl(item);
                // แนบ Event แจ้งเตือนเมื่อการ์ดถูกคลิก
                card.Click += (s, e) => 
                {
                    //MessageBox.Show($"คุณคลิกร้าน: {item.Name}", "แจ้งเตือน");
                    form.ChangeScreen(this, 4, userData, item);
                };
                flowContentLayoutPanel.Controls.Add(card);
            }

            flowContentLayoutPanel.ResumeLayout(true);
            ResizeCards();
        }

        private void ResizeCards()
        {
            int totalItems = flowContentLayoutPanel.Controls.Count;
            if (totalItems == 0) return;

            int columns = 5;
            int marginSize = 10;
            int availableWidth = flowContentLayoutPanel.ClientSize.Width - 15;

            int newWidth = (availableWidth / columns) - (marginSize * 2);
            int newHeight = (int)(newWidth * (275.0 / 230.0));
            int lastRowStartIndex = ((totalItems - 1) / columns) * columns;

            flowContentLayoutPanel.SuspendLayout();

            for (int i = 0; i < totalItems; i++)
            {
                if (flowContentLayoutPanel.Controls[i] is ItemCardControl card)
                {
                    card.Size = new Size(newWidth, newHeight);

                    int top = (i < columns) ? 0 : marginSize;
                    int bottom = (i >= lastRowStartIndex) ? 0 : marginSize;

                    card.Margin = new Padding(marginSize, top, marginSize, bottom);
                }
            }

            int totalContentWidth = columns * (newWidth + (marginSize * 2));
            int paddingLeft = Math.Max(0, (flowContentLayoutPanel.ClientSize.Width - totalContentWidth) / 2);
            flowContentLayoutPanel.Padding = new Padding(paddingLeft, 10, 0, 10);

            flowContentLayoutPanel.ResumeLayout(true);
            flowContentLayoutPanel.AutoScroll = false;
            flowContentLayoutPanel.AutoScroll = true;
        }
    }
}
