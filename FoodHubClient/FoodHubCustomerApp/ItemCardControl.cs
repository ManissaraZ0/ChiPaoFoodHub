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
    public partial class ItemCardControl : UserControl
    {
        private RestaurantItem _item;
        private Image _cardImage;

        // 1. Property สำหรับรับรูปภาพ
        public Image CardImage
        {
            get => _cardImage;
            set { _cardImage = value; Invalidate(); }
        }

        public ItemCardControl(RestaurantItem item)
        {
            _item = item;

            // ขนาดตั้งต้น (Base Size) 
            this.Size = new Size(230, 275);
            this.Margin = new Padding(10);
            this.Cursor = Cursors.Hand;
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
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; // ทำให้ภาพคมชัดเวลาถูกย่อ/ขยาย

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            // --- เวทมนตร์ (Magic Scale) ---
            // คำนวณสัดส่วนการขยายจากขนาดดั้งเดิม (230x275)
            float scaleX = this.Width / 230f;
            float scaleY = this.Height / 275f;
            g.ScaleTransform(scaleX, scaleY); // ย่อ/ขยายกราฟิกทั้งหมดอัตโนมัติ!

            // *** ต่อจากบรรทัดนี้ เราใช้พิกัดและตัวเลขดั้งเดิมได้ทั้งหมด! ***
            int baseWidth = 230;
            int baseHeight = 275;
            int radius = 16;
            int headerHeight = 170;
            int padX = 12;

            // เปลี่ยน this.Width/Height เป็น baseWidth/baseHeight
            Rectangle rectCard = new Rectangle(0, 0, baseWidth - 1, baseHeight - 1);

            using (GraphicsPath pathCard = GetRoundedPath(rectCard, radius))
            {
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    g.FillPath(brushWhite, pathCard);

                Rectangle rectHeader = new Rectangle(0, 0, baseWidth - 1, headerHeight);
                using (GraphicsPath pathHeader = GetTopRoundedPath(rectHeader, radius))
                {
                    if (_cardImage != null)
                    {
                        // ** ระบบรูปภาพ: ตัดขอบโค้งและใส่รูปลงไป **
                        var state = g.Save();
                        g.SetClip(pathHeader);
                        g.DrawImage(_cardImage, rectHeader);
                        g.Restore(state);
                    }
                    else
                    {
                        // ** กรณีไม่มีรูป: ไล่เฉดสีเหมือนเดิม **
                        using (LinearGradientBrush brushOrange = new LinearGradientBrush(
                            rectHeader, Color.FromArgb(215, 20, 10), Color.FromArgb(255, 87, 0), LinearGradientMode.ForwardDiagonal))
                        {
                            g.FillPath(brushOrange, pathHeader);
                        }
                    }
                }

                using (Pen penBorder = new Pen(Color.FromArgb(210, 210, 210), 1))
                    g.DrawPath(penBorder, pathCard);
            }

            int currentY = headerHeight + 8;

            using (Font fontCat = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            {
                g.DrawString($"Category: {_item.Category}", fontCat, brushGray, padX, currentY);
            }

            currentY += 18;

            using (Font fontName = new Font("Segoe UI", 11.5f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.FromArgb(30, 30, 30)))
            {
                g.DrawString(_item.Name, fontName, brushBlack, padX - 2, currentY);
            }

            currentY += 25;

            string star = "★";
            using (Font fontStar = new Font("Segoe UI", 10.5f, FontStyle.Regular))
            using (SolidBrush brushStar = new SolidBrush(Color.FromArgb(255, 175, 0)))
            {
                g.DrawString(star, fontStar, brushStar, padX, currentY - 2);
                float starWidth = g.MeasureString(star, fontStar).Width;

                using (Font fontRating = new Font("Segoe UI", 8.5f, FontStyle.Regular))
                using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
                {
                    g.DrawString($"{_item.Rating:0.00}/5.00", fontRating, brushGray, padX + starWidth - 6, currentY);
                }
            }
        }

        // Helper Functions
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

        private GraphicsPath GetTopRoundedPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddLine(bounds.Right, bounds.Bottom, bounds.Right, bounds.Bottom);
            path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Bottom);
            path.CloseFigure();
            return path;
        }
    }
}
