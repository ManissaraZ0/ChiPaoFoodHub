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
        // --- Properties ---
        private Image _logoImage;
        public Image LogoImage
        {
            get { return _logoImage; }
            set { _logoImage = value; this.Invalidate(); } // วาดใหม่ทันทีเมื่อเปลี่ยนรูป
        }

        // --- Events & Hitbox ---
        public event EventHandler ProfileClicked;
        public event EventHandler LogoClicked;
        public event EventHandler BellClicked;
        public event EventHandler HeartClicked;

        private Rectangle _profileHitbox;
        private Rectangle _logoHitbox;
        private Rectangle _bellHitbox;
        private Rectangle _heartHitbox;

        private TextBox _txtSearch;

        // --- Constructor ---
        public NavBarControl()
        {
            InitializeComponent();

            this.Size = new Size(1200, 70);
            this.Dock = DockStyle.Top; // ยึดติดขอบบนสุดของจอ
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;

            // สร้าง TextBox แบบไร้ขอบสำหรับเป็นช่อง Search
            _txtSearch = new TextBox();
            _txtSearch.BorderStyle = BorderStyle.None;
            _txtSearch.Font = new Font("Segoe UI", 11f, FontStyle.Regular);
            
            _txtSearch.Text = "Restaurant";
            _txtSearch.ForeColor = Color.Gray;
            _txtSearch.TabStop = false;

            _txtSearch.Enter += TxtSearch_Enter;
            _txtSearch.Leave += TxtSearch_Leave;

            _txtSearch.BackColor = Color.White;

            this.Controls.Add(_txtSearch);
            this.Resize += NavBarControl_Resize;
        }

        // ฟังก์ชันสั่งให้วาดใหม่ (ใช้ตอนเปลี่ยนบัญชีคน Login)
        public void RefreshUserProfile()
        {
            this.Invalidate();
        }

        // จัดตำแหน่งกล่องข้อความให้อยู่กลางจอ (Center ทั้งแนวนอนและแนวตั้ง)
        private void NavBarControl_Resize(object sender, EventArgs e)
        {
            int searchBarWidth = 500;
            int searchBarHeight = 40;

            // --- ดึงค่าความกว้าง Scrollbar ออกมาลบ เพื่อหา Center ที่แท้จริง ---
            int scrollWidth = SystemInformation.VerticalScrollBarWidth;
            int usableWidth = this.Width - scrollWidth;

            int searchX = (usableWidth - searchBarWidth) / 2;
            int searchY = (this.Height - searchBarHeight) / 2;

            // คำนวณให้ TextBox อยู่ตรงกลาง (Vertical Center) ของช่อง Search พอดี
            int txtHeight = _txtSearch.Height;
            int txtY = searchY + ((searchBarHeight - txtHeight) / 2);

            _txtSearch.Location = new Point(searchX + 50, txtY);
            _txtSearch.Width = searchBarWidth - 60;

            this.Invalidate();
        }

        // --- วาด UI ทั้งหมด (GDI+) ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rectFull = new Rectangle(0, 0, this.Width, this.Height);

            // 1. วาดพื้นหลัง Gradient
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                rectFull, Color.FromArgb(190, 15, 20), Color.FromArgb(255, 80, 0), LinearGradientMode.Horizontal))
            {
                g.FillRectangle(bgBrush, rectFull);
            }

            // --- ชดเชยพื้นที่ Scrollbar ให้ Margin ฝั่งขวา ---
            int scrollWidth = SystemInformation.VerticalScrollBarWidth;
            int leftMargin = 25;
            int rightMargin = 25 + scrollWidth; // บวกระยะ Scrollbar ให้ฝั่งขวาขยับหนีออกมา

            // 2. วาดโลโก้ (รักษา Aspect Ratio และ Center แนวตั้ง)
            if (LogoImage != null)
            {
                // กำหนดขนาดพื้นที่ "สูงสุด" ที่เราอนุญาตให้โลโก้แสดงได้
                int maxLogoW = 160;
                int maxLogoH = 60;

                // คำนวณอัตราส่วน (Scale) ของความกว้างและความสูง
                float scaleX = (float)maxLogoW / LogoImage.Width;
                float scaleY = (float)maxLogoH / LogoImage.Height;

                // เลือกอัตราส่วนที่ "น้อยที่สุด" เพื่อให้ภาพพอดีกรอบโดยไม่ล้นและไม่เสียสัดส่วน
                float scale = Math.Min(scaleX, scaleY);

                // คำนวณขนาดกว้าง-ยาวใหม่จากสัดส่วนที่ได้
                int newWidth = (int)(LogoImage.Width * scale);
                int newHeight = (int)(LogoImage.Height * scale);

                // คำนวณแกน Y ใหม่เพื่อให้โลโก้อยู่กึ่งกลางแนวตั้งเสมอ ไม่ว่ารูปจะเตี้ยลงแค่ไหน
                int logoY = (this.Height - newHeight) / 2;

                _logoHitbox = new Rectangle(leftMargin, logoY, newWidth, newHeight);
                g.DrawImage(LogoImage, _logoHitbox);
            }

            // 3. วาด Search Bar (ตรงกลางเป๊ะของ Usable Width)
            int usableWidth = this.Width - scrollWidth;
            int searchBarWidth = 500;
            int searchBarHeight = 40;
            int searchX = (usableWidth - searchBarWidth) / 2;
            int searchY = (this.Height - searchBarHeight) / 2;
            Rectangle rectSearch = new Rectangle(searchX, searchY, searchBarWidth, searchBarHeight);

            using (GraphicsPath pathSearch = GetPillShape(rectSearch))
            {
                Rectangle rectShadow = new Rectangle(searchX, searchY + 2, searchBarWidth, searchBarHeight);
                using (GraphicsPath pathShadow = GetPillShape(rectShadow))
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                {
                    g.FillPath(shadowBrush, pathShadow);
                }

                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                {
                    g.FillPath(brushWhite, pathSearch);
                }
            }

            // ไอคอนแว่นขยาย 
            using (Pen penSearch = new Pen(Color.Black, 2f))
            {
                int iconX = searchX + 20;
                int iconY = searchY + 12;
                g.DrawEllipse(penSearch, iconX, iconY, 12, 12);
                g.DrawLine(penSearch, iconX + 10, iconY + 10, iconX + 16, iconY + 16);
            }

            // 4. วาดไอคอนฝั่งขวา (เริ่มคำนวณจาก rightMargin ที่ขยับหนี Scrollbar แล้ว)
            int currentX = this.Width - rightMargin;
            int centerY = this.Height / 2;

            // --- 4.1 วาดข้อความ Username (ให้อยู่ขวาสุด) ---
            string displayUsername = UserSession.IsLoggedIn ? UserSession.Username : "Guest";
            using (Font fontUser = new Font("Segoe UI", 11.5f, FontStyle.Bold))
            using (SolidBrush brushWhiteText = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(displayUsername, fontUser);
                currentX -= (int)textSize.Width;
                g.DrawString(displayUsername, fontUser, brushWhiteText, currentX, centerY - (textSize.Height / 2));
            }

            // --- 4.2 วาดรูป Avatar วงกลม (ให้อยู่ทางซ้ายของ Username) ---
            int gapTextToAvatar = 12; // ระยะห่างระหว่างรูปกับชื่อ
            currentX -= gapTextToAvatar;

            int avatarSize = 36;
            currentX -= avatarSize; // ขยับแกน X ไปทางซ้ายเพื่อเตรียมวาดรูป
            int avatarY = centerY - (avatarSize / 2);

            // คำนวณพื้นที่คลิก (Hitbox) ให้ครอบคลุมตั้งแต่ Avatar ลากยาวไปจบที่ Username
            int hitWidth = (this.Width - rightMargin) - currentX;
            _profileHitbox = new Rectangle(currentX, avatarY, hitWidth, avatarSize);

            using (Pen penWhite = new Pen(Color.White, 2f))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.DrawEllipse(penWhite, currentX, avatarY, avatarSize, avatarSize);

                if (UserSession.AvatarImage != null)
                {
                    GraphicsState state = g.Save();
                    GraphicsPath clipPath = new GraphicsPath();
                    clipPath.AddEllipse(currentX, avatarY, avatarSize, avatarSize);
                    g.SetClip(clipPath);
                    g.DrawImage(UserSession.AvatarImage, new Rectangle(currentX, avatarY, avatarSize, avatarSize));
                    g.Restore(state);
                }
                else
                {
                    GraphicsPath clipPath = new GraphicsPath();
                    clipPath.AddEllipse(currentX, avatarY, avatarSize, avatarSize);
                    g.SetClip(clipPath);
                    g.FillEllipse(brushWhite, currentX + 10, avatarY + 6, 16, 16);
                    g.FillEllipse(brushWhite, currentX + 4, avatarY + 24, 28, 20);
                    g.ResetClip();
                }
            }

            // --- 4.3 วาดไอคอนกระดิ่ง Bell ---
            currentX -= 35; // ระยะห่างถัดจาก Avatar
            _bellHitbox = new Rectangle(currentX - 5, centerY - 15, 30, 35);
            using (Pen penBell = new Pen(Color.White, 3f))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                // ตั้งค่าหัวปากกาและรอยต่อให้โค้งมนแบบเดียวกับหัวใจ
                penBell.LineJoin = LineJoin.Round;
                penBell.StartCap = LineCap.Round;
                penBell.EndCap = LineCap.Round;

                int bx = currentX;
                int by = centerY - 9;

                // 1. วาดตัวระฆัง (ลากเส้นต่อกันเพื่อให้รอยต่อสมูท ไม่หักศอก)
                GraphicsPath bellPath = new GraphicsPath();
                bellPath.AddLine(bx, by + 12, bx, by + 8); // ลากเส้นขึ้นด้านซ้าย
                bellPath.AddArc(bx, by, 16, 16, 180, 180); // วาดส่วนโค้งหลังคา
                bellPath.AddLine(bx + 16, by + 8, bx + 16, by + 12); // ลากเส้นลงด้านขวา

                g.DrawPath(penBell, bellPath);

                // 2. วาดเส้นขีดฐานระฆัง
                g.DrawLine(penBell, bx - 2, by + 12, bx + 18, by + 12);

                // 3. วาดลูกตุ้มด้านล่าง (ขยับแกน Y ลงมานิดนึงเพื่อให้พ้นเส้นที่หนาขึ้น)
                g.FillEllipse(brushWhite, bx + 6, by + 15, 4, 4);
            }

            // --- 4.4 วาดไอคอนหัวใจ Heart ---
            currentX -= 40; // ระยะห่างถัดจาก Bell
            _heartHitbox = new Rectangle(currentX - 5, centerY - 15, 30, 35);
            using (Pen penHeart = new Pen(Color.White, 3f))
            {
                // จุดเด่นของรหัสนี้คือการทำ LineJoin ให้มุมแหลมของหัวใจโค้งมนอย่างสมูท
                penHeart.LineJoin = LineJoin.Round;
                penHeart.StartCap = LineCap.Round;
                penHeart.EndCap = LineCap.Round;

                int hx = currentX;
                int hy = centerY - 10;

                GraphicsPath heartPath = new GraphicsPath();
                // วงกลมซ้าย (องศาเริ่ม 135 กวาดไป 225 องศา)
                heartPath.AddArc(hx, hy, 12, 12, 135, 225);
                // วงกลมขวา
                heartPath.AddArc(hx + 12, hy, 12, 12, 180, 225);
                // ลากเส้นทะแยงลงมาจุดต่ำสุดตรงกลาง
                heartPath.AddLine(hx + 22.24f, hy + 10.24f, hx + 12, hy + 21);
                // ลากเส้นกลับไปหาจุดเริ่มต้นของวงกลมซ้าย
                heartPath.AddLine(hx + 12, hy + 21, hx + 1.76f, hy + 10.24f);

                heartPath.CloseFigure();

                // วาดเส้นตาม Path ที่กำหนดไว้
                g.DrawPath(penHeart, heartPath);
            }
        }

        // --- Logic การกดเมาส์ (คลิก Profile) ---
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // เช็คว่าเมาส์ชี้โดนปุ่มใดปุ่มหนึ่งหรือไม่
            bool isHovering = _profileHitbox.Contains(e.Location) ||
                             _logoHitbox.Contains(e.Location) ||
                             _bellHitbox.Contains(e.Location) ||
                             _heartHitbox.Contains(e.Location);

            if (isHovering)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // ตรวจสอบว่าคลิกโดนพื้นที่ไหน แล้วยิง Event นั้นๆ
            if (_profileHitbox.Contains(e.Location))
                ProfileClicked?.Invoke(this, EventArgs.Empty);

            else if (_logoHitbox.Contains(e.Location))
                LogoClicked?.Invoke(this, EventArgs.Empty);

            else if (_bellHitbox.Contains(e.Location))
                BellClicked?.Invoke(this, EventArgs.Empty);

            else if (_heartHitbox.Contains(e.Location))
                HeartClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            // เมื่อคลิกที่ช่องค้นหา ถ้ายังเป็นคำว่า Restaurant ให้เคลียร์ทิ้งและเปลี่ยนสีเป็นตัวอักษรปกติ (สีดำ)
            if (_txtSearch.Text == "Restaurant" && _txtSearch.ForeColor == Color.Gray)
            {
                _txtSearch.Text = "";
                _txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            // เมื่อคลิกที่อื่น (เอาเมาส์ออก) ถ้าช่องค้นหาว่างเปล่า ให้ใส่คำว่า Restaurant สีเทากลับเข้าไป
            if (string.IsNullOrWhiteSpace(_txtSearch.Text))
            {
                _txtSearch.Text = "Restaurant";
                _txtSearch.ForeColor = Color.Gray;
            }
        }

        private GraphicsPath GetPillShape(Rectangle bounds)
        {
            int radius = bounds.Height;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 90, 180);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, -90, 180);
            path.CloseFigure();
            return path;
        }
    }
}
