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
    public partial class AddPostPage : UserControl
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
                navBarControl.UserData = userData;
            }
        }

        private RestaurantRecommendationRsp resPrevious;

        public RestaurantRecommendationRsp ResPrevious
        {
            get => resPrevious;
            set
            {
                resPrevious = value;
                if (resPrevious != null)
                {
                    restaurantTitle.Text = resPrevious.Name;
                    //restaurantCategory.Text = resPrevious.Category;
                    //restaurantRatingScore.Text = resPrevious.OverallRating.ToString("0.00") + "/5.00";
                    //restaurantDescription.Text = resPrevious.Address;
                }
            }
        }

        private string placeholderText = "Your opinion";
        private Color placeholderColor = Color.Gray;
        private Color textColor = Color.Black;

        public AddPostPage(CustomerApp form)
        {
            this.AutoSize = false;
            InitializeComponent();
            this.form = form;
            this.DoubleBuffered = true;
            SetupUI();
            SetupEventHandlers();
            SetupPlaceholder();
        }

        private void SetupUI()
        {
            // ตั้งค่าบัญชีผู้ใช้
            navBarControl.RefreshUserProfile();

            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "Add Your Review";
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

            inputReviewPanel.Paint += inputReviewPanel_Paint;
            inputReviewPanel.Click += (s, e) => txtReview.Focus();

            primaryBtn.Click += (s, e) => {
                int rating = starRatingControl.SelectedRating;
                string reviewText = txtReview.Text;

                // จัดการกรณีที่ไม่ได้พิมพ์อะไรเลย (ยังเป็น Placeholder อยู่)
                if (reviewText == placeholderText) reviewText = "";

                // ตัวอย่างการตรวจสอบข้อมูลเบื้องต้น (Validation)
                if (string.IsNullOrWhiteSpace(reviewText) && rating == 0)
                {
                    var result = DialogCard.Show("Please rate or review before submit.", DialogCardType.Negative, false, true);
                    return;
                }

                // --- จุดที่ส่งข้อมูลไปบันทึก ---
                bool success = SaveReviewToDatabase(rating, reviewText);

                if (success)
                {
                    DialogCard.Show($"Review saved successfully!", DialogCardType.Positive, false, true);
                    ClearForm();
                }
            };

            // เมื่อกดปุ่ม Cancel
            secondaryBtn.Click += (s, e) => {
                var result = DialogCard.Show("Are you sure you want to cancel?", DialogCardType.Negative);

                // เช็คผลลัพธ์เหมือน MessageBox ปกติ
                if (result == DialogResult.Yes)
                {
                    form.ChangeScreen(this, 5, userData, resPrevious);
                }
            };

            backBtn.Click += (s, e) => {
                form.ChangeScreen(this, 4, userData);
            };
        }

        private void SetupPlaceholder()
        {
            // ตั้งค่าเริ่มต้น
            txtReview.Text = placeholderText;
            txtReview.ForeColor = placeholderColor;

            // เมื่อเอาเมาส์ไปคลิก (Enter)
            txtReview.Enter += (s, e) =>
            {
                if (txtReview.Text == placeholderText)
                {
                    txtReview.Text = "";
                    txtReview.ForeColor = textColor;
                }
            };

            // เมื่อคลิกออกไปที่อื่น (Leave)
            txtReview.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtReview.Text))
                {
                    txtReview.Text = placeholderText;
                    txtReview.ForeColor = placeholderColor;
                }
            };
        }

        private void inputReviewPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = (Panel)sender;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // สีของเส้นขอบ (ใช้สีเทาอ่อนๆ เหมือนในรูป)
            Color borderColor = Color.FromArgb(180, 180, 180);
            int radius = 15;

            // วาดพื้นหลังสีขาวให้โค้งตาม
            Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
            using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundedPath(rect, radius))
            {
                // ถมสีพื้นหลัง
                using (SolidBrush brush = new SolidBrush(Color.White))
                    e.Graphics.FillPath(brush, path);

                // วาดเส้นขอบ
                using (Pen pen = new Pen(borderColor, 1))
                    e.Graphics.DrawPath(pen, path);
            }
        }

        private bool SaveReviewToDatabase(int rating, string review)
        {
            try
            {
                // ตรงนี้คือส่วนที่คุณจะไปต่อยอดเขียน SQL หรือส่งไปหา List ส่วนกลาง
                // เช่น: ReviewController.Submit(UserSession.Username, rating, review);
                Service.SubmitReview(resPrevious.RestaurantId, new SubmitReviewReq
                {
                    UserId = userData.Id,
                    Rating = rating,
                    Comment = review
                });
                return true;
            }
            catch { return false; }
        }

        private void ClearForm()
        {
            // ล้างค่าใน Form เพื่อรอรับรีวิวใหม่
            starRatingControl.SelectedRating = 0;
            txtReview.Text = placeholderText;
            txtReview.ForeColor = placeholderColor;
            starRatingControl.Invalidate(); // ให้ดาวกลับเป็นสีว่าง
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
