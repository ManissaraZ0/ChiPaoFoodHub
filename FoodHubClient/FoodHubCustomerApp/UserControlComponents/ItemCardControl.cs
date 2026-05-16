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
using FoodHubCustomerApp.Model;
using FoodHubCustomerApp.UserControlComponents;

namespace FoodHubCustomerApp
{
    public partial class ItemCardControl : UserControl
    {
        private RestaurantRecommendationRsp _item;
        private Image _cardImage;

        // --- Constants ---
        private const int BaseWidth = 230;
        private const int BaseHeight = 275;
        private const int CardRadius = 16;
        private const int HeaderHeight = 170;
        private const int PadX = 12;

        // --- Cached Fonts  ---
        private readonly Font _categoryFont = new Font("Segoe UI", 8.5f, FontStyle.Regular);
        private readonly Font _nameFont = new Font("Segoe UI", 11.5f, FontStyle.Bold);
        private readonly Font _starFont = new Font("Segoe UI", 10.5f, FontStyle.Regular);
        private readonly Font _ratingFont = new Font("Segoe UI", 8.5f, FontStyle.Regular);

        public Image CardImage
        {
            get => _cardImage;
            set { _cardImage = value; Invalidate(); }
        }

        public ItemCardControl(RestaurantRecommendationRsp item)
        {
            _item = item;

            this.Size = new Size(BaseWidth, BaseHeight);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // ตั้งค่า Rendering Quality
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // จัดการ Background เพื่อลดปัญหาขอบดำ
            Color bgColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            if (bgColor == Color.Transparent) bgColor = Color.WhiteSmoke;
            g.Clear(bgColor);

            // Scale Transform
            float scaleX = (float)this.Width / BaseWidth;
            float scaleY = (float)this.Height / BaseHeight;
            g.ScaleTransform(scaleX, scaleY);

            // พื้นที่ของส่วนต่างๆ
            Rectangle rectCard = new Rectangle(0, 0, BaseWidth - 1, BaseHeight - 1);
            Rectangle rectHeader = new Rectangle(0, 0, BaseWidth - 1, HeaderHeight);

            // วาดการ์ดหลัก
            using (GraphicsPath pathCard = GetRoundedPath(rectCard, CardRadius))
            {
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    g.FillPath(brushWhite, pathCard);

                // วาดส่วน Header (รูปภาพ หรือ สี Gradient)
                using (GraphicsPath pathHeader = GetTopRoundedPath(rectHeader, CardRadius))
                {
                    if (_cardImage != null)
                    {
                        var state = g.Save();
                        g.SetClip(pathHeader);
                        g.DrawImage(_cardImage, rectHeader);
                        g.Restore(state);
                    }
                    else
                    {
                        using (var brushGradient = StylePalette.GetOrangeGradient(rectHeader))
                        {
                            g.FillPath(brushGradient, pathHeader);
                        }
                    }
                }

                using (Pen penBorder = new Pen(Color.FromArgb(210, 210, 210), 1))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    g.DrawPath(penBorder, pathCard);
                }
            }
            DrawCardText(g);
        }

        private void DrawCardText(Graphics g)
        {
            int currentY = HeaderHeight + 8;

            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            using (SolidBrush brushBlack = new SolidBrush(Color.FromArgb(30, 30, 30)))
            using (SolidBrush brushStar = new SolidBrush(Color.FromArgb(255, 175, 0)))
            {
                // Category
                g.DrawString($"Category: {_item.Category}", _categoryFont, brushGray, PadX, currentY);

                // Name
                currentY += 18;
                g.DrawString(_item.Name, _nameFont, brushBlack, PadX - 2, currentY);

                // Rating & Star
                currentY += 25;
                string star = "★";
                g.DrawString(star, _starFont, brushStar, PadX, currentY - 2);

                float starWidth = g.MeasureString(star, _starFont).Width;
                g.DrawString($"{_item.OverallRating:0.00}/5.00", _ratingFont, brushGray, PadX + starWidth - 6, currentY);
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
