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
    public partial class ReviewsPage : UserControl
    {
        ManagerApp form;
        private int restaurantId;
        public int RestaurantId
        {
            get => restaurantId;
            set
            {
                restaurantId = value;
                navBarControl1.SelectedIndex = 2; // เลือก Review tab เมื่อเปลี่ยนร้าน
                var allReviews = Service.GetReviewDetails(RestaurantId);
                RenderReviews(allReviews);
            }
        }
        private int managerId;
        public int ManagerId
        {
            get => managerId;
            set
            {
                managerId = value;
            }
        }

        public ReviewsPage(ManagerApp form)
        {
            InitializeComponent();
            this.form = form;

            SetupEventHandlers();

            //var allReviews = GetMockReviewData();
            RefreshData();

            navBarControl1.SelectedIndexChanged += NavBarControl1_SelectedIndexChanged;
        }

        public void RefreshData()
        {
            var allReviews = Service.GetReviewDetails(RestaurantId);
            RenderReviews(allReviews);
        }

        private void SetupEventHandlers()
        {
            navBarControl1.LogoClicked += (s, e) => form.ChangeScreen(this, 0, 2002);

            // Search Event
            searchBar1.SearchSubmitted += (s, keyword) =>
            {
                //var filtered = GetMockReviewData()
                var filtered = Service.GetReviewDetails(RestaurantId)
                    .Where(p => p.Comment.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                RenderReviews(filtered);
            };
        }

        private void RenderReviews(List<ManagerReviewDetailRsp> items)
        {
            if (InvokeRequired)
            {
                Invoke(() => RenderReviews(items));
                return;
            }

            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            foreach (var item in items)
            {
                var reviewCard = new ReviewCardControl(item);
                reviewCard.Margin = new Padding(0, 5, 0, 5); // ไม่มี margin ซ้าย-ขวา
                flowLayoutPanel1.Controls.Add(reviewCard);
            }

            flowLayoutPanel1.ResumeLayout(true);

            ResizeCards();
        }

        // ฟังก์ชันสร้าง Mock Data สำหรับรีวิว
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
        //    list.Add(new ReviewItem { Username = "User 3", Rating = 5.00, ReviewText = mockText+"หมีเนย" });

        //    return list;
        //}

        //// โหลดเข้า FlowLayoutPanel
        //private void LoadReviews()
        //{
        //    flowLayoutPanel1.Controls.Clear();
        //    var items = GetMockReviewData();

        //    foreach (var item in items)
        //    {
        //        var reviewCard = new ReviewCardControl(item);
        //        reviewCard.Margin = new Padding(0, 5, 0, 5); // ไม่มี margin ซ้าย-ขวา
        //        flowLayoutPanel1.Controls.Add(reviewCard);
        //    }

        //    // ต้อง resize หลัง Add เสร็จแล้ว
        //    ResizeCards();
        //}

        private void ResizeCards()
        {
            int cardWidth = flowLayoutPanel1.ClientSize.Width - 2; // -2 กันชน
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                c.Width = cardWidth;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResizeCards();
        }

        private void NavBarControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (navBarControl1.SelectedIndex)
            {
                case 0: // Promotion
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 2);
                    break;
                case 1: // Ticket
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 1);
                    break;
                case 2: // Review
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 0);
                    break;
            }
        }
    }
}
