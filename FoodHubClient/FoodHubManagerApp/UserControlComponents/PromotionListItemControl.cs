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
using FoodHubManagerApp.Logics;
using FoodHubManagerApp.Model;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class PromotionListItemControl : UserControl
    {
        private ManagerPromotionSummaryRsp _item;
        public ManagerPromotionSummaryRsp Data { get; private set; }

        public PromotionListItemControl(ManagerPromotionSummaryRsp item)
        {
            _item = item;
            Data = item;

            // ตั้งขนาดเริ่มต้น (ความกว้าง 450, ความสูง 70)
            this.Size = new Size(450, 70);
            this.Margin = new Padding(5); // ระยะห่างระหว่างบรรทัด
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // ทำให้พื้นหลังโปร่งใสเพื่อลบรอยหยักมุมขอบ
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // 1. เปิดโหมดความคมชัดสูงสุด (Anti-Aliasing)
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            int outerRadius = 10; // ความโค้งของกรอบนอก
            Rectangle rectOuter = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // --- 2. วาดกรอบนอกสีขาว และ เส้นขอบสีเทา ---
            using (GraphicsPath pathOuter = GetRoundedPath(rectOuter, outerRadius))
            {
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    g.FillPath(brushWhite, pathOuter);

                using (Pen penBorder = new Pen(Color.FromArgb(170, 170, 170), 1)) // สีเทากลางๆ
                    g.DrawPath(penBorder, pathOuter);
            }

            // --- 3. วาดข้อความฝั่งซ้าย (Title & Type) ---
            int padX = 18; // ระยะห่างจากขอบซ้าย

            // Promotion Title (ตัวหนา สีดำ)
            using (Font fontTitle = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                g.DrawString(_item.Title, fontTitle, brushBlack, padX, 12);
            }

            // Type A (ตัวบาง สีเทา)
            using (Font fontType = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
            {
                g.DrawString(_item.Conditions + " • " + _item.StartDate.ToString("yyyy-MM-dd HH:mm") + " - " + _item.EndDate.ToString("yyyy-MM-dd HH:mm"), fontType, brushGray, padX, 37);
            }

            // --- 4. วาดกล่องตัวเลข (Badge) ฝั่งขวา ---
            int badgeWidth = 75;
            int badgeHeight = 40;
            int badgeRadius = 8;

            // คำนวณพิกัดให้อยู่ชิดขวา และกึ่งกลางแนวตั้งพอดีเป๊ะ
            int badgeX = this.Width - badgeWidth - 15;
            int badgeY = (this.Height - badgeHeight) / 2;

            Rectangle rectBadge = new Rectangle(badgeX, badgeY, badgeWidth, badgeHeight);

            using (GraphicsPath pathBadge = GetRoundedPath(rectBadge, badgeRadius))
            {
                // ถมสีพื้นกล่อง Badge เป็นสีเทาอ่อน
                using (SolidBrush brushBadge = new SolidBrush(Color.FromArgb(225, 225, 225)))
                    g.FillPath(brushBadge, pathBadge);
            }

            // --- 5. วาดตัวเลขตรงกลางกล่อง Badge ---
            using (Font fontValue = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                // ใช้ StringFormat เพื่อตั้งค่าให้ตัวอักษรอยู่ "กึ่งกลาง" ของกล่อง Rectangle พอดี
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;     // กึ่งกลางแนวนอน
                sf.LineAlignment = StringAlignment.Center; // กึ่งกลางแนวตั้ง

                g.DrawString(_item.RemainingQuota.ToString(), fontValue, brushBlack, rectBadge, sf);
            }
        }

        // Helper: ฟังก์ชันวาดกรอบสี่เหลี่ยมมุมโค้ง
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
