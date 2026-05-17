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
                    //// 1. นำข้อมูลมาใส่ (แอบเติมคำว่า "Category: " เข้าไปให้เหมือนหน้า Design)
                    resTitle.Text = resPrevious.Name;
                    categoryValue.Text = "Category: " + resPrevious.Category;
                    ratingLabel.Text = resPrevious.OverallRating.ToString("0.00") + "/5.00";
                    addrContent.Text = resPrevious.Address;

                    LoadTickets(resPrevious.RestaurantId);
                    LoadReviews(resPrevious.RestaurantId);

                    //// --- 2. การจัดการ Layout แบบไดนามิก (Dynamic Positioning) ---

                    //// รีเซ็ตให้คุมขนาดตัวเองอัตโนมัติก่อน
                    //lblRestaurantName.AutoSize = true;
                    //lblCategory.AutoSize = true;

                    //int gap = 15; // ระยะห่างระหว่างชื่อร้านกับ Category (15 px)
                    //int maxAllowedWidth = 896; // พื้นที่สูงสุดที่ตกลงกันไว้
                    //int totalWidth = lblRestaurantName.Width + gap + lblCategory.Width;

                    //if (totalWidth > maxAllowedWidth)
                    //{
                    //    // กรณีที่ 1: ชื่อร้านยาวเกินไป! 
                    //    // เราจะปิด AutoSize ของชื่อร้าน แล้วบังคับความกว้างไม่ให้เกินพื้นที่ที่เหลือ
                    //    lblRestaurantName.AutoSize = false;
                    //    lblRestaurantName.AutoEllipsis = true; // สั่งให้ใส่จุดไข่ปลา (...) อัตโนมัติเมื่อข้อความล้น
                    //    lblRestaurantName.Width = maxAllowedWidth - lblCategory.Width - gap;
                    //    lblRestaurantName.Height = 45; // ล็อคความสูงไว้ให้พอดีกับ Font 24pt
                    //}

                    //// 3. ย้ายตำแหน่ง lblCategory ไปต่อท้าย lblRestaurantName เสมอ
                    //lblCategory.Left = lblRestaurantName.Right + gap;

                    //// 4. (แถม) ปรับระดับความสูง (Baseline) ให้อยู่ในระนาบเดียวกันให้ดูสวยงาม
                    //// เนื่องจาก Font ชื่อร้านใหญ่กว่า เราเลยดึง Category ลงมาด้านล่างนิดหน่อย
                    //lblCategory.Top = lblRestaurantName.Top + (lblRestaurantName.Height - lblCategory.Height) - 4;
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

            // กำหนดสีพื้นหลังให้ปุ่ม Add Post โดยดึงสีมาจาก StylePalette
            //btnAddPost.BackColor = StylePalette.DarkRed;
        }

        public void RefreshData()
        {
            if (resPrevious != null)
            {
                LoadTickets(resPrevious.RestaurantId);
                LoadReviews(resPrevious.RestaurantId);
            }
        }

        private void SetupUI()
        {
            // ตั้งค่าบัญชีผู้ใช้
            navBarControl.RefreshUserProfile();

            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "Restaurant Detail";
            headRec.Dock = DockStyle.Top;
            sectionLeftHeaderControl.Controls.Add(headRec);
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => form.ChangeScreen(this, 0, userData);
            navBarControl.HeartClicked += (s, e) => MessageBox.Show("การกดถูกใจ");
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => form.ChangeScreen(this, 3, userData);
            addPostBtn.Click += (s, e) => form.ChangeScreen(this, 4, userData, resPrevious);
            backBtn.Click += (s, e) => form.ChangeScreen(this, 1001, userData);

            // เพื่อบอกให้ปุ่มรู้ว่าต้องใช้ฟังก์ชันนี้ตอนวาดตัวเอง
            //btnAddPost.Paint += btnAddPost_Paint;
            commentFlowLayoutPanel.SizeChanged += CommentFlowLayoutPanel_SizeChanged;
            flowTicketLayoutPanel.SizeChanged += FlowTicketLayoutPanel_SizeChanged;
        }

        //private void btnAddPost_Paint(object sender, PaintEventArgs e)
        //{
        //    // 1. ตัดขอบ PictureBox ให้กลายเป็นวงกลมเหมือนเดิม
        //    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        //    path.AddEllipse(0, 0, btnAddPost.Width - 1, btnAddPost.Height - 1);
        //    btnAddPost.Region = new Region(path);

        //    // 2. สร้างกรอบสี่เหลี่ยมเพื่อบอกระยะให้ IconPainter (เว้นขอบ Padding เข้ามาหน่อย ไอคอนจะได้ไม่ชนขอบปุ่ม)
        //    Rectangle iconBounds = new Rectangle(0, 0, btnAddPost.Width, btnAddPost.Height);

        //    // 3. เรียกใช้ฟังก์ชันวาดปากกาสีขาว ทับลงไปบนพื้นหลังสี DarkRed
        //    FoodHubCustomerApp.UserControlComponents.IconPainter.DrawPenIcon(e.Graphics, iconBounds);
        //}

        //private List<TicketItem> GetMockTickets()
        //{
        //    var list = new List<TicketItem>();
        //    for (int i = 0; i < 9; i++) 
        //    {
        //        list.Add(new TicketItem
        //        {
        //            Title = "Drink Promotion",
        //            Subtitle = "Film's Restaurant",
        //            SaveText = "SAVE",
        //            DiscountValue = "99%"
        //        });
        //    }
        //    return list;
        //}

        private void LoadTickets(int restaurantId)
        {
            flowTicketLayoutPanel.Controls.Clear();
            var items = Service.BrowsePromotions(restaurantId);

            var adaptedItems = items.Select(p => new TicketItem
            {
                PromotionId = p.Id,
                Title = p.Title,
                Subtitle = p.Conditions,
                SaveText = "Quota",
                DiscountValue = p.TotalQuota.ToString()
            }).ToList();

            foreach (var item in adaptedItems)
            {
                var ticketCard = new TicketCardControl(item);
                ticketCard.Click += (s, e) =>
                {
                    var result = DialogCard.Show("Are you sure you want to buy?", DialogCardType.Negative);
                    if (result == DialogResult.Yes) { /* ... */ }
                };

                flowTicketLayoutPanel.Controls.Add(ticketCard);
            }

            ResizeTicketCards();
        }

        private void FlowTicketLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            ResizeTicketCards();
        }

        private void ResizeTicketCards()
        {
            if (flowTicketLayoutPanel.Controls.Count == 0) return;
            flowTicketLayoutPanel.AutoScroll = false;
            flowTicketLayoutPanel.SuspendLayout();

            int totalColumns = 5;
            int marginEachSide = 6;
            int cardMarginHorizontal = marginEachSide * 2;

            int scrollbarWidth = 0;
            if (flowTicketLayoutPanel.Controls.Count > totalColumns)
            {
                scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
            }

            int availableWidth = flowTicketLayoutPanel.ClientSize.Width - scrollbarWidth;
            int targetWidth = (availableWidth - (cardMarginHorizontal * totalColumns)) / totalColumns;
            int remainder = (availableWidth - (cardMarginHorizontal * totalColumns)) % totalColumns;

            if (targetWidth < 80) targetWidth = 80;

            int index = 0;
            foreach (Control control in flowTicketLayoutPanel.Controls)
            {
                if (control is TicketCardControl ticketCard)
                {
                    ticketCard.Margin = new Padding(marginEachSide);

                    int finalWidth = targetWidth + (index < remainder ? 1 : 0);
                    ticketCard.Width = finalWidth;

                    index++;
                }
            }
            flowTicketLayoutPanel.AutoScroll = true;

            flowTicketLayoutPanel.ResumeLayout(true);
            flowTicketLayoutPanel.PerformLayout();
        }

        //private List<ReviewItem> GetMockReviewData()
        //{
        //    var list = new List<ReviewItem>();

        //    // จำลองข้อความรีวิวแบบในรูปเป๊ะๆ
        //    string mockText = "อาหารหลากหลาย\n" +
        //                      "แต่มีเพียงบางอย่างเท่านั้นที่อร่อย\n" +
        //                      "แต่ด้านบริการยอดเยี่ยมมากๆ ครับ\n" +
        //                      "พนักงานทุกคนกระตือรือร้น ยิ้มแย้ม สุภาพ\n" +
        //                      "บรรยากาศภายในร้านพลุกพล่าน";

        //    list.Add(new ReviewItem { Username = "User 1", Rating = 4.00, ReviewText = mockText });
        //    list.Add(new ReviewItem { Username = "User 2", Rating = 5.00, ReviewText = mockText });
        //    list.Add(new ReviewItem { Username = "User 3", Rating = 5.00, ReviewText = mockText });

        //    return list;
        //}

        private void LoadReviews(int restaurantId)
        {
            commentFlowLayoutPanel.Controls.Clear();

            var items = Service.GetReviewDetails(restaurantId);
            int targetWidth = commentFlowLayoutPanel.ClientSize.Width - 25;

            foreach (var item in items)
            {
                var reviewCard = new ReviewCardControl(item);
                reviewCard.Width = targetWidth;

                commentFlowLayoutPanel.Controls.Add(reviewCard);
            }

            ResizeReviewCards();
        }

        private void CommentFlowLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            ResizeReviewCards();
        }

        private void ResizeReviewCards()
        {
            int targetWidth = commentFlowLayoutPanel.ClientSize.Width - 25;
            commentFlowLayoutPanel.SuspendLayout();

            foreach (Control control in commentFlowLayoutPanel.Controls)
            {
                if (control is ReviewCardControl reviewCard)
                {
                    reviewCard.Width = targetWidth;
                }
            }

            commentFlowLayoutPanel.HorizontalScroll.Maximum = 0;
            commentFlowLayoutPanel.ResumeLayout(true);
            commentFlowLayoutPanel.PerformLayout();
        }
    }
}