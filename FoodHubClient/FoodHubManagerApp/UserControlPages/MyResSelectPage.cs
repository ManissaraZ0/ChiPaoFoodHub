using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubManagerApp.Logics;
using FoodHubManagerApp.Model;
using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    public partial class MyResSelectPage : UserControl
    {
        ManagerApp form;

        //private readonly ServiceMock _service;

        private int managerId;
        public int ManagerId
        {
            get => managerId;
            set
            {
                managerId = value;
                var restaurants = Service.GetManagedRestaurants(ManagerId);
                UpdateUserCards(restaurants);
            }
        }

        public MyResSelectPage(ManagerApp form)
        {
            InitializeComponent();
            this.form = form;
            //_service = new ServiceMock();

            // ผูก Events ต่างๆ (รวมถึง Observer)
            SetupEventHandlers();

            // สั่งให้ Service ทำงาน
            //_service.FetchUsers();
        }

        private void SetupEventHandlers()
        {
            // Layout Event
            flowLayoutPanel1.SizeChanged += (s, e) => ResizeCards();

            // *** Observer: รอรับข้อมูลจาก Service เมื่อโหลดเสร็จ ***
            //_service.OnUsersLoaded += UpdateUserCards;
        }

        // ฟังก์ชันนี้ทำงานอัตโนมัติเมื่อ Service สั่ง Invoke()
        private void UpdateUserCards(List<ManagerRestaurantListRsp> items)
        {
            // ป้องกัน Thread ชนกัน กรณี Service ไปดึงข้อมูลแบบ Async
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateUserCards(items)));
                return;
            }

            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            foreach (var item in items)
            {
                var card = new RestaurantListControl(item);

                card.Click += (s, e) =>
                {
                    //MessageBox.Show(
                    //    $"คุณ {ManagerId} เลือก: {item.RestaurantId}",
                    //    "แจ้งเตือน"
                    //);
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = item.RestaurantId, ManagerId = ManagerId }, 0);
                };

                flowLayoutPanel1.Controls.Add(card);
            }

            flowLayoutPanel1.ResumeLayout(true);

            ResizeCards();
        }

        private void ResizeCards()
        {
            if (flowLayoutPanel1.Controls.Count == 0)
                return;

            int sidePadding = 20;
            int cardHeight = 70;

            int cardWidth =
                flowLayoutPanel1.ClientSize.Width
                - sidePadding * 2;

            flowLayoutPanel1.SuspendLayout();

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is RestaurantListControl card)
                {
                    card.Size = new Size(
                        cardWidth,
                        cardHeight
                    );

                    card.Margin = new Padding(
                        0,
                        8,
                        0,
                        8
                    );
                }
            }

            // ใช้ panel padding แทน margin ซ้ายขวา
            flowLayoutPanel1.Padding = new Padding(
                sidePadding,
                10,
                sidePadding,
                10
            );

            flowLayoutPanel1.ResumeLayout();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            form.ChangeScreen(this, data: new DataDetail { ManagerId = ManagerId }, action: 1);
        }
    }
}
