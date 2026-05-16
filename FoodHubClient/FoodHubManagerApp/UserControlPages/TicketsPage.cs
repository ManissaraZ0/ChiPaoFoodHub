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
using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    public partial class TicketsPage : UserControl
    {
        ManagerApp form;
        private int restaurantId = 1;
        public int RestaurantId
        {
            get => restaurantId;
            set { 
                restaurantId = value; 
                navBarControl1.SelectedIndex = 1; // เลือก Ticket tab เมื่อเปลี่ยนร้าน
            }
        }
        private int managerId = 1;
        public int ManagerId
        {
            get => managerId;
            set
            {
                managerId = value;
            }
        }

        public TicketsPage(ManagerApp form)
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
            navBarControl1.LogoClicked += (s, e) => form.ChangeScreen(this, 0, 0, 2002);

            // Layout Event
            flowLayoutPanel1.Resize += (s, e) => ResizeCards();

            // Search Event
            searchBar1.SearchSubmitted += (s, keyword) =>
            {
                var filtered = GetMockData()
                    .Where(p => p.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                UpdateUserCards(filtered);
            };

            // *** Observer: รอรับข้อมูลจาก Service เมื่อโหลดเสร็จ ***
            //_service.OnUsersLoaded += UpdateUserCards;
            //var users = Service.GetAllUsers();
            // Select Only Customer Role
            //UpdateUserCards(users.Where(u => u.Role == "client").ToList());
        }

        // ฟังก์ชันสร้างข้อมูลจำลอง (Mock Data) สำหรับ Promotion
        private List<TicketItem> GetMockData()
        {
            var list = new List<TicketItem>();
            for (int i = 1; i <= 10; i++) // สร้างลิสต์จำลอง 10 บรรทัด
            {
                list.Add(new TicketItem
                {
                    Title = "Promotion Title " + i, // ใส่ + i เพื่อให้เห็นความแตกต่างของแต่ละบรรทัด
                    Id = i,
                    UserId = i * 10
                });
            }
            return list;
        }

        // ฟังก์ชันนี้ทำงานอัตโนมัติเมื่อ Service สั่ง Invoke()
        private void UpdateUserCards(List<TicketItem> items)
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
                var card = new TicketListItemControl(item);

                card.AcceptClicked += (s, e) =>
                {
                    var result = DialogCard.Show($"Confirm Ticket #{item.Id}", DialogCardType.Positive);

                    // เช็คผลลัพธ์เหมือน MessageBox ปกติ
                    if (result == DialogResult.Yes)
                    {
                        form.ChangeScreen(this, RestaurantId, ManagerId, 0);
                    }
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
                if (control is TicketListItemControl card)
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
                    form.ChangeScreen(this, RestaurantId, ManagerId, 1);
                    break;
                case 1: // Ticket
                    form.ChangeScreen(this, RestaurantId, ManagerId, 0);
                    break;
                case 2: // Review
                    form.ChangeScreen(this, RestaurantId, ManagerId, 2);
                    break;
            }
        }
    }
}
