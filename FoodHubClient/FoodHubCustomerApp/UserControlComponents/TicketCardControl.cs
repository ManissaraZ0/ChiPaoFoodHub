using FoodHubCustomerApp.Logics;
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

//namespace TicketCard
namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class TicketCardControl : UserControl
    {
        private TicketItem _item;

        public TicketCardControl(TicketItem item)
        {
            _item = item;

            // ขนาดการ์ดให้สัดส่วนใกล้เคียงรูปภาพ (กว้าง 170, สูง 110)
            this.Size = new Size(170, 110);
            this.Margin = new Padding(0);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // โหมดความคมชัดสูงสุด (ลดรอยหยัก)
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            // สีแดงเข้มตามต้นฉบับ
            Color cardRedColor = StylePalette.DarkRed;
            int radius = 15;
            Rectangle rectCard = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // --- 1. วาดพื้นหลังการ์ดสีแดง ---
            using (GraphicsPath pathCard = GetRoundedPath(rectCard, radius))
            using (SolidBrush brushRed = new SolidBrush(cardRedColor))
            {
                g.FillPath(brushRed, pathCard);
            }

            // --- 2. วาดไอคอนแก้วน้ำ (เรียกใช้จาก IconPainter) ---
            int cX = this.Width / 2;
            int cY = 15; // กำหนดจุดแกน Y ให้ตรงกับฝาแก้วเหมือนต้นฉบับ
            IconPainter.DrawBubbleTeaIcon(g, cX, cY, Color.White);

            // StringFormat สำหรับจัดข้อความให้อยู่กึ่งกลาง
            StringFormat sfCenter = new StringFormat();
            sfCenter.Alignment = StringAlignment.Center;
            sfCenter.LineAlignment = StringAlignment.Center;

            // --- 3. วาดข้อความ Title & Subtitle ---
            using (Font fontTitle = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.DrawString(_item.Title, fontTitle, brushWhite, new RectangleF(0, 40, this.Width, 20), sfCenter);
            }

            using (Font fontSub = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.DrawString(_item.Subtitle, fontSub, brushWhite, new RectangleF(0, 60, this.Width, 15), sfCenter);
            }

            // --- 4. วาดป้าย Badge ส่วนลด (ด้านล่าง) ---
            int badgeW = 120;
            int badgeH = 26;
            int badgeX = (this.Width - badgeW) / 2;
            int badgeY = 75;

            // 4.1 กรอบป้ายสีขาว (Outer Badge)
            Rectangle rectOuterBadge = new Rectangle(badgeX, badgeY, badgeW, badgeH);
            using (GraphicsPath pathOuterBadge = GetRoundedPath(rectOuterBadge, 6))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.FillPath(brushWhite, pathOuterBadge);
            }

            // 4.2 ข้อความ "SAVE" ฝั่งซ้าย
            Rectangle rectSaveText = new Rectangle(badgeX, badgeY, 60, badgeH);
            using (Font fontSave = new Font("Segoe UI", 9.5f, FontStyle.Bold))
            using (SolidBrush brushRedText = new SolidBrush(cardRedColor))
            {
                g.DrawString(_item.SaveText, fontSave, brushRedText, rectSaveText, sfCenter);
            }

            // 4.3 กล่องสีแดงด้านขวา (Inner Badge)
            int innerW = 54;
            int innerH = 22;
            int innerX = badgeX + badgeW - innerW - 2; // เว้นขอบขาวไว้ 2px
            int innerY = badgeY + 2;

            Rectangle rectInnerBadge = new Rectangle(innerX, innerY, innerW, innerH);
            using (GraphicsPath pathInnerBadge = GetRoundedPath(rectInnerBadge, 5))
            using (SolidBrush brushInnerRed = new SolidBrush(cardRedColor))
            {
                g.FillPath(brushInnerRed, pathInnerBadge);
            }

            // 4.4 ข้อความ "99%" ในกล่องสีแดง
            using (Font fontDiscount = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.DrawString(_item.DiscountValue, fontDiscount, brushWhite, rectInnerBadge, sfCenter);
            }
        }

        // Helper: วาดสี่เหลี่ยมมุมโค้ง
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
