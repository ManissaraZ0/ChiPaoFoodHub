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
using FoodHubManagerApp.Model;
using FoodHubManagerApp.Logics;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class UserListControl : UserControl
    {
        private UserRsp _item;

        public UserListControl(UserRsp item)
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

            // Profile icon
            int iconSize = 42;
            int iconX = 18;
            int iconY = (this.Height - iconSize) / 2;

            // วงกลมสีส้ม
            using (SolidBrush orangeBrush = new SolidBrush(Color.FromArgb(255, 85, 0)))
            {
                g.FillEllipse(orangeBrush, iconX, iconY, iconSize, iconSize);
            }

            // วงกลมสีขาวด้านใน
            int innerPadding = 6;
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

            // หัว
            int headSize = 8;
            int headX = iconX + (iconSize / 2) - (headSize / 2);
            int headY = iconY + 11;

            using (SolidBrush orangeBrush = new SolidBrush(Color.FromArgb(255, 85, 0)))
            {
                g.FillEllipse(orangeBrush, headX, headY, headSize, headSize);

                // ตัว
                g.FillPie(
                    orangeBrush,
                    iconX + 11,
                    iconY + 19,
                    20,
                    14,
                    0,
                    180
                );
            }

            int padX = 75;

            // Username (วาดชื่อผู้ใช้งาน)
            using (Font fontUsername = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                g.DrawString(_item.Username, fontUsername, brushBlack, padX, 12);
            }

            // Id (วาด Id เป็นสีเทาเล็กๆ ด้านล่าง)
            using (Font fontId = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            {
                g.DrawString($"Id: {_item.Id}", fontId, brushGray, padX, 37);
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
