using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubCustomerApp;
using FoodHubManagerApp.Logics;
using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    public partial class PromotionsPage : UserControl
    {
        ManagerApp form;
        private int restaurantId = 1;
        public int RestaurantId
        {
            get => restaurantId;
            set { 
                restaurantId = value;
                navBarControl1.SelectedIndex = 0;
            }
        }
        private int managerId = 1;
        public int ManagerId
        {
            get => managerId;
            set { managerId = value; }
        }

        public PromotionsPage(ManagerApp form)
        {
            InitializeComponent();
            this.form = form;

            SetupEventHandlers(); // <-- เพิ่มตรงนี้

            this.Load += (s, e) =>
            {
                UpdateUserCards(GetMockData());
            };

            navBarControl1.SelectedIndexChanged += NavBarControl1_SelectedIndexChanged;
        }

        private void SetupEventHandlers()
        {
            // Layout Event
            flowLayoutPanel1.Resize += (s, e) => ResizeCards();

            // *** Observer: รอรับข้อมูลจาก Service เมื่อโหลดเสร็จ ***
            //_service.OnUsersLoaded += UpdateUserCards;
            //var users = Service.GetAllUsers();
            // Select Only Customer Role
            //UpdateUserCards(users.Where(u => u.Role == "client").ToList());
        }

        // ฟังก์ชันสร้างข้อมูลจำลอง (Mock Data) สำหรับ Promotion
        private List<PromotionItem> GetMockData()
        {
            var list = new List<PromotionItem>();
            for (int i = 1; i <= 10; i++) // สร้างลิสต์จำลอง 10 บรรทัด
            {
                list.Add(new PromotionItem
                {
                    Title = "Promotion Title " + i, // ใส่ + i เพื่อให้เห็นความแตกต่างของแต่ละบรรทัด
                    Type = "Type A",
                    Value = "99"
                });
            }
            return list;
        }

        // ฟังก์ชันนี้ทำงานอัตโนมัติเมื่อ Service สั่ง Invoke()
        private void UpdateUserCards(List<PromotionItem> items)
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
                var card = new PromotionListItemControl(item);

                card.Click += (s, e) =>
                {
                    MessageBox.Show(
                        $"คุณเลือก: {item.Title}",
                        "แจ้งเตือน"
                    );
                };

                flowLayoutPanel1.Controls.Add(card);
            }

            flowLayoutPanel1.Controls.Add(new Panel()
            {
                Size = new Size(1, 20)
            });

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
                if (control is PromotionListItemControl card)
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

        private void NavBarControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (navBarControl1.SelectedIndex)
            {
                case 0: // Promotion
                    form.ChangeScreen(this, RestaurantId, ManagerId, 0);
                    break;
                case 1: // Ticket
                    form.ChangeScreen(this, RestaurantId, ManagerId, 2);
                    break;
                case 2: // Review
                    form.ChangeScreen(this, RestaurantId, ManagerId, 3);
                    break;
            }
        }

        private void circleAddButtonControl1_Load(object sender, EventArgs e)
        {
            form.ChangeScreen(this, RestaurantId, ManagerId, 1);
        }
    }
}
