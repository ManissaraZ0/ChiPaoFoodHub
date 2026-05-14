using FoodHubCustomerApp.Logics;
using FoodHubCustomerApp.UserControlComponents;
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
        private Image _logoImage;
        public Image LogoImage
        {
            get => _logoImage;
            set { _logoImage = value; this.Invalidate(); }
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

        private readonly Font _userFont = new Font("Segoe UI", 11.5f, FontStyle.Bold);

        // --- Constructor ---
        public NavBarControl()
        {
            InitializeComponent();

            this.SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            this.UpdateStyles();

            this.BackColor = Color.Transparent;

            int targetHeight = 70;
            this.Height = targetHeight;
            this.MinimumSize = new Size(0, targetHeight);
            this.MaximumSize = new Size(0, targetHeight);
            this.Dock = DockStyle.Fill;

            _txtSearch = new TextBox();
            _txtSearch.BorderStyle = BorderStyle.None;
            _txtSearch.Font = new Font("Segoe UI", 11f, FontStyle.Regular);
            _txtSearch.Text = "Restaurant";
            _txtSearch.ForeColor = Color.Gray;
            _txtSearch.TabStop = false;
            _txtSearch.BackColor = Color.White;

            _txtSearch.Enter += TxtSearch_Enter;
            _txtSearch.Leave += TxtSearch_Leave;

            this.Controls.Add(_txtSearch);
            this.Resize += NavBarControl_Resize;
        }

        public void RefreshUserProfile()
        {
            this.Invalidate();
        }

        private void NavBarControl_Resize(object sender, EventArgs e)
        {
            int searchBarWidth = 500;
            int searchBarHeight = 40;

            int searchX = (this.Width - searchBarWidth) / 2;
            int searchY = (this.Height - searchBarHeight) / 2;

            int txtHeight = _txtSearch.Height;
            int txtY = searchY + ((searchBarHeight - txtHeight) / 2);

            _txtSearch.Location = new Point(searchX + 50, txtY);
            _txtSearch.Width = searchBarWidth - 60;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rectFull = new Rectangle(0, 0, this.Width, this.Height);

            // 1. วาดพื้นหลัง Gradient จาก StylePalette
            using (var bgBrush = StylePalette.GetOrangeGradient(rectFull))
            {
                g.FillRectangle(bgBrush, rectFull);
            }

            int leftMargin = 25;
            int rightMargin = 25; // กลับมาใช้ Margin ธรรมดา
            int centerY = this.Height / 2;

            // 2. วาดโลโก้
            if (LogoImage != null)
            {
                int maxLogoW = 160;
                int maxLogoH = 60;

                float scaleX = (float)maxLogoW / LogoImage.Width;
                float scaleY = (float)maxLogoH / LogoImage.Height;
                float scale = Math.Min(scaleX, scaleY);

                int newWidth = (int)(LogoImage.Width * scale);
                int newHeight = (int)(LogoImage.Height * scale);
                int logoY = (this.Height - newHeight) / 2;

                _logoHitbox = new Rectangle(leftMargin, logoY, newWidth, newHeight);
                g.DrawImage(LogoImage, _logoHitbox);
            }

            // 3. วาด Search Bar 
            int searchBarWidth = 500;
            int searchBarHeight = 40;
            int searchX = (this.Width - searchBarWidth) / 2;
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

            // Search Icon
            IconPainter.DrawSearchIcon(g, searchX + 20, searchY + 12);

            // วาดส่วนโปรไฟล์ฝั่งขวา
            int currentX = this.Width - rightMargin;

            // Username
            string displayUsername = UserSession.IsLoggedIn ? UserSession.Username : "Guest";
            using (SolidBrush brushWhiteText = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(displayUsername, _userFont);
                currentX -= (int)textSize.Width;
                g.DrawString(displayUsername, _userFont, brushWhiteText, currentX, centerY - (textSize.Height / 2));
            }

            // Avatar
            int gapTextToAvatar = 12;
            int avatarSize = 36;

            currentX -= (gapTextToAvatar + avatarSize);
            int avatarY = centerY - (avatarSize / 2);

            int hitWidth = (this.Width - rightMargin) - currentX;
            _profileHitbox = new Rectangle(currentX, avatarY, hitWidth, avatarSize);

            using (Pen penWhite = new Pen(Color.White, 2f))
            {
                g.DrawEllipse(penWhite, currentX, avatarY, avatarSize, avatarSize);

                if (UserSession.AvatarImage != null)
                {
                    GraphicsState state = g.Save();
                    using (GraphicsPath clipPath = new GraphicsPath())
                    {
                        clipPath.AddEllipse(currentX, avatarY, avatarSize, avatarSize);
                        g.SetClip(clipPath);
                        g.DrawImage(UserSession.AvatarImage, new Rectangle(currentX, avatarY, avatarSize, avatarSize));
                    }
                    g.Restore(state);
                }
                else
                {
                    // Avatar Icon
                    IconPainter.DrawDefaultAvatar(g, currentX, avatarY, avatarSize);
                }
            }

            // Bell (Notification)
            currentX -= 35;
            _bellHitbox = new Rectangle(currentX - 5, centerY - 15, 30, 35);
            IconPainter.DrawBellIcon(g, currentX, centerY - 9);

            // Heart
            currentX -= 40;
            _heartHitbox = new Rectangle(currentX - 5, centerY - 15, 30, 35);
            IconPainter.DrawHeartIcon(g, currentX, centerY - 10);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool isHovering = _profileHitbox.Contains(e.Location) ||
                              _logoHitbox.Contains(e.Location) ||
                              _bellHitbox.Contains(e.Location) ||
                              _heartHitbox.Contains(e.Location);

            this.Cursor = isHovering ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (_profileHitbox.Contains(e.Location)) ProfileClicked?.Invoke(this, EventArgs.Empty);
            else if (_logoHitbox.Contains(e.Location)) LogoClicked?.Invoke(this, EventArgs.Empty);
            else if (_bellHitbox.Contains(e.Location)) BellClicked?.Invoke(this, EventArgs.Empty);
            else if (_heartHitbox.Contains(e.Location)) HeartClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (_txtSearch.Text == "Restaurant" && _txtSearch.ForeColor == Color.Gray)
            {
                _txtSearch.Text = "";
                _txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
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