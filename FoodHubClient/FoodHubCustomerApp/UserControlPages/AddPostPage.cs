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
        private string placeholderText = "Your opinion";
        private Color placeholderColor = Color.Gray;
        private Color textColor = Color.Black;

        public AddPostPage()
        {
            InitializeComponent();
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
