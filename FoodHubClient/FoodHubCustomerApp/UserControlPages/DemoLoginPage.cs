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
using FoodHubCustomerApp.UserControlComponents;

namespace FoodHubCustomerApp.UserControlPages
{
    public partial class DemoLoginPage : UserControl
    {
        private readonly ServiceMock _service;

        public DemoLoginPage()
        {
            InitializeComponent();
            _service = new ServiceMock();

            // ผูก Events ต่างๆ (รวมถึง Observer)
            SetupEventHandlers();

            // สั่งให้ Service ทำงาน
            _service.FetchUsers();
        }

        private void SetupEventHandlers()
        {
            // Layout Event
            flowLayoutPanel1.SizeChanged += (s, e) => ResizeCards();

            // *** Observer: รอรับข้อมูลจาก Service เมื่อโหลดเสร็จ ***
            _service.OnUsersLoaded += UpdateUserCards;
        }

        // ฟังก์ชันนี้ทำงานอัตโนมัติเมื่อ Service สั่ง Invoke()
        private void UpdateUserCards(List<UserItem> items)
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
                var card = new UserListControl(item);

                card.Click += (s, e) =>
                {
                    MessageBox.Show(
                        $"คุณเลือก: {item.Username}",
                        "แจ้งเตือน"
                    );
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
                if (control is UserListControl card)
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
    }
}
