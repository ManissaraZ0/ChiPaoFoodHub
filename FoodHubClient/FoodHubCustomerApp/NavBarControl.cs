using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp
{
    public partial class NavBarControl : UserControl
    {
        // --- Properties สำหรับปรับแต่งจากหน้า Design ---
        private Image _logoImage;
        public Image LogoImage
        {
            get => _logoImage;
            set { _logoImage = value; Invalidate(); } // เปลี่ยนรูปแล้ววาดใหม่ทันที
        }

        private string _username = "Username";
        public string Username
        {
            get => _username;
            set { _username = value; Invalidate(); }
        }

        // กล่องข้อความค้นหา (TextBox จริงๆ ที่ซ่อนอยู่ในการ์ด)
        private TextBox txtSearch;

        public NavBarControl()
        {
            InitializeComponent();

            // ป้องกันภาพกระพริบ และจัดการพื้นหลังโปร่งใส
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // 1. *** สร้าง TextBox ให้เสร็จก่อนเป็นอันดับแรก ***
            txtSearch = new TextBox();
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Font = new Font("Segoe UI", 12.5f, FontStyle.Regular);
            txtSearch.ForeColor = Color.Black;
            txtSearch.BackColor = Color.White;
            txtSearch.Text = "Restaurant";

            txtSearch.Enter += (s, e) => { if (txtSearch.Text == "Restaurant") txtSearch.Text = ""; };
            txtSearch.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) txtSearch.Text = "Restaurant"; };

            this.Controls.Add(txtSearch);

            // 2. *** ค่อยตั้งค่าขนาดและการจัดวาง (มันจะไปกระตุ้น OnResize) ***
            this.MinimumSize = new Size(1280, 80);
            this.Size = new Size(1280, 80);
            this.Dock = DockStyle.Top;
        }

        // จัดการตำแหน่งของ Search Box เวลาที่มีการย่อ/ขยายหน้าต่าง
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // เช็คก่อนว่า txtSearch ถูกสร้างหรือยัง (ป้องกัน Designer Error)
            if (txtSearch != null)
            {
                int searchWidth = 550;
                int searchHeight = 46;
                int searchX = (this.Width - searchWidth) / 2;
                int searchY = (this.Height - searchHeight) / 2;

                txtSearch.Location = new Point(searchX + 55, searchY + 12);
                txtSearch.Width = searchWidth - 70;
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // --- 1. วาดพื้นหลังไล่สี (Gradient Background) ---
            Rectangle rectBg = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                rectBg,
                Color.FromArgb(190, 15, 20), // แดงเข้ม (ซ้าย)
                Color.FromArgb(255, 60, 0),  // ส้มสว่าง (ขวา)
                LinearGradientMode.Horizontal))
            {
                g.FillRectangle(bgBrush, rectBg);
            }

            // --- 2. วาด Logo (ทางซ้ายมือ) ---
            if (_logoImage != null)
            {
                // กำหนดกรอบให้โลโก้ (กว้างสุด 180, สูงสุด 50) เพื่อรักษาสัดส่วน
                int logoH = 50;
                int logoW = (int)((float)_logoImage.Width / _logoImage.Height * logoH);
                if (logoW > 200) { logoW = 200; logoH = (int)((float)_logoImage.Height / _logoImage.Width * logoW); }

                int logoY = (this.Height - logoH) / 2;
                g.DrawImage(_logoImage, new Rectangle(40, logoY, logoW, logoH));
            }

            // --- 3. วาดช่องค้นหาทรงแคปซูล (ตรงกลาง) ---
            int searchWidth = 550;
            int searchHeight = 46;
            int searchX = (this.Width - searchWidth) / 2;
            int searchY = (this.Height - searchHeight) / 2;

            Rectangle rectSearch = new Rectangle(searchX, searchY, searchWidth, searchHeight);
            using (GraphicsPath pathSearch = GetRoundedPath(rectSearch, searchHeight / 2)) // ความโค้งครึ่งนึงของความสูง = ทรงแคปซูล
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                g.FillPath(whiteBrush, pathSearch);
            }

            // วาดไอคอนแว่นขยาย (อยู่ในกรอบค้นหา)
            using (Pen penBlack = new Pen(Color.FromArgb(30, 30, 30), 2f))
            {
                g.DrawEllipse(penBlack, searchX + 20, searchY + 12, 14, 14); // วงกลม
                g.DrawLine(penBlack, searchX + 31, searchY + 23, searchX + 38, searchY + 30); // ด้ามจับ
            }

            // --- 4. วาดไอคอนและข้อมูลฝั่งขวา (Right Align) ---
            int currentRightX = this.Width - 40; // จุดเริ่มต้นขวาสุด

            // 4.1 วาด Username Text
            using (Font fontUser = new Font("Segoe UI", 12f, FontStyle.Bold))
            {
                float textWidth = g.MeasureString(_username, fontUser).Width;
                currentRightX -= (int)textWidth; // ขยับ X มาทางซ้ายตามขนาดตัวอักษร
                g.DrawString(_username, fontUser, Brushes.White, currentRightX, (this.Height - 22) / 2);
            }

            // 4.2 วาด User Avatar (รูปคนในวงกลมขาว)
            currentRightX -= 60; // เว้นระยะห่างและขยับมาวาด Avatar
            int avatarY = (this.Height - 38) / 2;
            g.FillEllipse(Brushes.White, currentRightX, avatarY, 38, 38);

            // วาดเงาคนสีแดงด้านใน
            using (SolidBrush redIconBrush = new SolidBrush(Color.FromArgb(230, 40, 10)))
            {
                // วาดหัว
                g.FillEllipse(redIconBrush, currentRightX + 11, avatarY + 7, 16, 16);

                // วาดตัว (ใช้การวาดวงรีแล้วตัดขอบให้อยู่ในวงกลมใหญ่)
                GraphicsState state = g.Save();
                GraphicsPath avatarPath = new GraphicsPath();
                avatarPath.AddEllipse(currentRightX, avatarY, 38, 38);
                g.SetClip(avatarPath);
                g.FillEllipse(redIconBrush, currentRightX + 4, avatarY + 24, 30, 30); // ไหล่/ตัว
                g.Restore(state);
            }

            // 4.3 วาดไอคอน กระดิ่ง (Bell)
            currentRightX -= 55;
            int bellY = (this.Height - 22) / 2;
            using (Pen penWhite = new Pen(Color.White, 2f))
            {
                g.DrawArc(penWhite, currentRightX + 4, bellY, 14, 14, 180, 180); // โดมด้านบน
                g.DrawLine(penWhite, currentRightX + 4, bellY + 7, currentRightX + 1, bellY + 17); // บานพับซ้าย
                g.DrawLine(penWhite, currentRightX + 18, bellY + 7, currentRightX + 21, bellY + 17); // บานพับขวา
                g.DrawLine(penWhite, currentRightX + 1, bellY + 17, currentRightX + 21, bellY + 17); // ฐาน
                g.DrawArc(penWhite, currentRightX + 8, bellY + 16, 6, 6, 0, 180); // ลูกตุ้ม
            }

            // 4.4 วาดไอคอน หัวใจ (Heart Outline)
            currentRightX -= 55;
            int heartY = (this.Height - 20) / 2;
            using (Pen penWhite = new Pen(Color.White, 2f))
            {
                // ใช้ Bezier Curve วาดหัวใจให้เนียน
                g.DrawBezier(penWhite, currentRightX + 11, heartY + 5, currentRightX + 11, heartY - 5, currentRightX, heartY - 5, currentRightX, heartY + 5);
                g.DrawBezier(penWhite, currentRightX, heartY + 5, currentRightX, heartY + 13, currentRightX + 11, heartY + 18, currentRightX + 11, heartY + 22);

                g.DrawBezier(penWhite, currentRightX + 11, heartY + 5, currentRightX + 11, heartY - 5, currentRightX + 22, heartY - 5, currentRightX + 22, heartY + 5);
                g.DrawBezier(penWhite, currentRightX + 22, heartY + 5, currentRightX + 22, heartY + 13, currentRightX + 11, heartY + 18, currentRightX + 11, heartY + 22);
            }
        }

        // Helper: สำหรับวาดทรงแคปซูลของช่อง Search
        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
