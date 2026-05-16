using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using FoodHubManagerApp.Model; // ตรวจสอบ Namespace ของ ManagerRestaurantListRsp ให้ตรงกับโปรเจกต์ของคุณ
using FoodHubManagerApp.Logics;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class RestaurantListControl : UserControl
    {
        private ManagerRestaurantListRsp _item;

        public RestaurantListControl(ManagerRestaurantListRsp item)
        {
            _item = item;

            this.Height = 70;
            this.Margin = new Padding(5);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

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

            // Restaurant icon (Plate Icon)
            int iconSize = 42;
            int iconX = 18;
            int iconY = (this.Height - iconSize) / 2;

            // วงกลมรอบนอกจาน (สีส้ม)
            using (SolidBrush orangeBrush = new SolidBrush(Color.FromArgb(255, 85, 0)))
            {
                g.FillEllipse(orangeBrush, iconX, iconY, iconSize, iconSize);
            }

            // วงกลมด้านในจาน (สีขาว)
            int innerPadding = 4;
            Rectangle innerCircle = new Rectangle(
                iconX + innerPadding,
                iconY + innerPadding,
                iconSize - (innerPadding * 2),
                iconSize - (innerPadding * 2)
            );

            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(whiteBrush, innerCircle);
            }

            // ก้นจานด้านใน (สีเทาอ่อนๆ ให้มีมิติ)
            int centerPadding = 10;
            Rectangle centerCircle = new Rectangle(
                iconX + centerPadding,
                iconY + centerPadding,
                iconSize - (centerPadding * 2),
                iconSize - (centerPadding * 2)
            );

            using (SolidBrush lightGrayBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                g.FillEllipse(lightGrayBrush, centerCircle);
            }

            int padX = 75;

            // Restaurant Name (วาดชื่อร้านอาหาร)
            using (Font fontName = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                // ตรวจสอบความยาวชื่อร้านเพื่อไม่ให้ล้นกรอบ (ถ้าจำเป็น)
                g.DrawString(_item.Name, fontName, brushBlack, padX, 12);
            }

            // Restaurant Details (Id และ Category)
            using (Font fontDetail = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            {
                string detailText = $"ID: {_item.RestaurantId}  •  {_item.Category}";
                g.DrawString(detailText, fontDetail, brushGray, padX, 37);
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