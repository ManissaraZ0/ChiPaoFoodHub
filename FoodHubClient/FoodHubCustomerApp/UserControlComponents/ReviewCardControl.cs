using FoodHubCustomerApp.UserControlPages;
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
using FoodHubCustomerApp.Logics;

namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class ReviewCardControl : UserControl
    {
        private ReviewItem _item;

        // --- 1. เพิ่ม Constructor สำหรับหน้า Design ---
        public ReviewCardControl()
        {
            InitializeComponent();

            // ข้อมูลจำลองสำหรับให้หน้า Design วาดรูปได้
            _item = new ReviewItem
            {
                Username = "Sample User",
                Rating = 5.0f,
                ReviewText = "นี่คือข้อความรีวิวตัวอย่างสำหรับแสดงผลบนหน้า Design"
            };

            SetupControl();
        }

        // --- 2. Constructor สำหรับการใช้งานจริง ---
        public ReviewCardControl(ReviewItem item)
        {
            InitializeComponent();
            _item = item;
            SetupControl();
        }

        // --- 3. ฟังก์ชันรวมการตั้งค่าพื้นฐาน ---
        private void SetupControl()
        {
            this.Size = new Size(400, 190);
            this.Margin = new Padding(10);
            this.Cursor = Cursors.Default;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            int radius = 15;
            Rectangle rectCard = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath pathCard = GetRoundedPath(rectCard, radius))
            using (SolidBrush brushCard = new SolidBrush(Color.FromArgb(243, 243, 243)))
            {
                g.FillPath(brushCard, pathCard);
            }

            int avatarX = 20;
            int avatarY = 20;
            int avatarSize = 44;

            GraphicsState state = g.Save();

            GraphicsPath avatarPath = new GraphicsPath();
            avatarPath.AddEllipse(avatarX, avatarY, avatarSize, avatarSize);
            g.SetClip(avatarPath);

            g.FillEllipse(Brushes.White, avatarX, avatarY, avatarSize, avatarSize);

            using (SolidBrush brushIcon = new SolidBrush(Color.FromArgb(225, 225, 225)))
            {
                g.FillEllipse(brushIcon, avatarX + 13, avatarY + 9, 18, 18);
                g.FillEllipse(brushIcon, avatarX + 7, avatarY + 29, 30, 25);
            }

            g.Restore(state);

            int textX = avatarX + avatarSize + 15;
            int textY = avatarY + 10;

            using (Font fontUser = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                g.DrawString(_item.Username, fontUser, brushBlack, textX, textY);

                float nameWidth = g.MeasureString(_item.Username, fontUser).Width;
                float starX = textX + nameWidth + 5;

                string star = "★";
                using (Font fontStar = new Font("Segoe UI", 11f, FontStyle.Regular))
                using (SolidBrush brushStar = new SolidBrush(Color.FromArgb(255, 175, 0)))
                {
                    g.DrawString(star, fontStar, brushStar, starX, textY);
                    float starWidth = g.MeasureString(star, fontStar).Width;

                    using (Font fontRating = new Font("Segoe UI", 10f, FontStyle.Regular))
                    using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
                    {
                        g.DrawString($"{_item.Rating:0.00}/5.00", fontRating, brushGray, starX + starWidth - 4, textY + 1);
                    }
                }
            }

            int reviewY = avatarY + avatarSize + 15;
            RectangleF reviewRect = new RectangleF(20, reviewY, this.Width - 40, this.Height - reviewY - 10);

            using (Font fontReview = new Font("Segoe UI", 10.5f, FontStyle.Regular))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                g.DrawString(_item.ReviewText, fontReview, brushBlack, reviewRect);
            }
        }

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