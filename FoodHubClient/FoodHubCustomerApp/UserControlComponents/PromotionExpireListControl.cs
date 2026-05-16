using FoodHubCustomerApp.Logics;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class PromotionExpireListControl : UserControl
    {
        private PromotionExpireItem _item;

        public PromotionExpireListControl(PromotionExpireItem item)
        {
            _item = item;

            this.Size = new Size(450, 70);
            this.Margin = new Padding(5);
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

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            int outerRadius = 10;
            Rectangle rectOuter = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // วาดกรอบนอกและถมสีขาว
            using (GraphicsPath pathOuter = GetRoundedPath(rectOuter, outerRadius))
            {
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    g.FillPath(brushWhite, pathOuter);

                using (Pen penBorder = new Pen(Color.FromArgb(170, 170, 170), 1))
                    g.DrawPath(penBorder, pathOuter);
            }

            int padX = 18;

            // Promotion Title
            using (Font fontTitle = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                g.DrawString(_item.Title, fontTitle, brushBlack, padX, 12);
            }

            // Expire Date (วาดคำว่า Expire: นำหน้าให้เลย)
            using (Font fontExpire = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            {
                g.DrawString($"Expire: {_item.ExpireDate}", fontExpire, brushGray, padX, 37);
            }
        }

        // Helper: วาดกรอบสี่เหลี่ยมมุมโค้ง
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
