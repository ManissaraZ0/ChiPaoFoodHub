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

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class NavBarControl : UserControl
    {
        private Image _logoImage;
        public Image LogoImage
        {
            get => _logoImage ?? Properties.Resources.logo;
            set { _logoImage = value; this.Invalidate(); }
        }

        private Rectangle _logoHitbox;

        public NavBarControl()
        {
            InitializeComponent();
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
                int maxLogoW = 210;
                int maxLogoH = 90;

                float scaleX = (float)maxLogoW / LogoImage.Width;
                float scaleY = (float)maxLogoH / LogoImage.Height;
                float scale = Math.Min(scaleX, scaleY);

                int newWidth = (int)(LogoImage.Width * scale);
                int newHeight = (int)(LogoImage.Height * scale);

                // ให้อยู่ด้านบน (padding 15px)
                int topPadding = 15;
                int logoY = topPadding;
                int logoX = (this.Width - newWidth) / 2;

                _logoHitbox = new Rectangle(
                    logoX,
                    logoY,
                    newWidth,
                    newHeight);

                g.DrawImage(LogoImage, _logoHitbox);
            }
        }
    }
}
