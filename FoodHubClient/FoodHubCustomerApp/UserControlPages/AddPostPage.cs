using FoodHubCustomerApp.Logics;
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
            UserSession.Username = "OscarPattJuiFilmHeng";
            navBarControl.RefreshUserProfile();

            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "Add Your Review";
            headRec.Dock = DockStyle.Top;
            rightSectionHeaderControl.Controls.Add(headRec);
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => MessageBox.Show("กลับหน้าแรก");
            navBarControl.HeartClicked += (s, e) => MessageBox.Show("การกดถูกใจ");
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => MessageBox.Show($"เปิดบัญชี: {UserSession.Username}");

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
                    MessageBox.Show("กรุณาให้ดาวหรือพิมพ์รีวิวก่อนกดส่งนะจ๊ะ", "เตือนความจำ");
                    return;
                }

                // --- จุดที่ส่งข้อมูลไปบันทึก ---
                bool success = SaveReviewToDatabase(rating, reviewText);

                if (success)
                {
                    MessageBox.Show($"บันทึกรีวิว {rating} ดาว เรียบร้อยแล้ว!", "สำเร็จ");
                    // หลังจากเซฟเสร็จ อาจจะล้างหน้าจอหรือเด้งกลับหน้าแรก
                    ClearForm();
                }
            };

            // เมื่อกดปุ่ม Cancel
            secondaryBtn.Click += (s, e) => {
                var result = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการยกเลิกการเขียนรีวิว?",
                                             "ยืนยัน", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // คำสั่งกลับหน้าหลัก (ขึ้นอยู่กับการจัดการ Navigation ของคุณ)
                    this.Hide();
                }
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
