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

            this.Height = 70;
            this.Margin = new Padding(5, 5, 5, 0); // ปรับกั้นระยะ ซ้าย, บน, ขวา, ล่าง ให้พอดี
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            this.Anchor = AnchorStyles.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_item == null) return;

            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            int outerRadius = 10;
            int borderOffset = 1;
            Rectangle rectOuter = new Rectangle(borderOffset, borderOffset, this.Width - (borderOffset * 2) - 1, this.Height - (borderOffset * 2) - 1);

            using (GraphicsPath pathOuter = GetRoundedPath(rectOuter, outerRadius))
            {
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    g.FillPath(brushWhite, pathOuter);

                using (Pen penBorder = new Pen(Color.FromArgb(170, 170, 170), 1))
                    g.DrawPath(penBorder, pathOuter);
            }

            int padX = 18;
            using (Font fontTitle = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                // ใส่ StringFormat เพื่อให้ Text ตัดคำได้ถ้าชื่อยาวเกินไป และใช้ RectangleF ตามความกว้าง UserControl
                g.DrawString(_item.Title, fontTitle, brushBlack,
                    new RectangleF(padX, 12, this.Width - (padX * 2) - 1, 25));
            }

            using (Font fontExpire = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            {
                g.DrawString($"Expire: {_item.ExpireDate}", fontExpire, brushGray,
                    new RectangleF(padX, 37, this.Width - (padX * 2) - 1, 25));
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            if (bounds.Width <= d) d = bounds.Width; // กันระบบค้างถ้าความกว้างเหลือน้อยกว่ารัศมีมุมโค้ง
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
