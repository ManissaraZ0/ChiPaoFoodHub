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

//namespace ReviewCard
namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class ReviewCardControl : UserControl
    {
        private ManagerReviewDetailRsp _item;

        public ReviewCardControl(ManagerReviewDetailRsp item)
        {
            _item = item;

            // ตั้งขนาดเริ่มต้น (กว้าง 400, สูง 190 เพื่อให้พอดีกับข้อความหลายบรรทัด)
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

            // เปิดโหมดความคมชัดสูงสุด
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            g.Clear(parentColor);

            int radius = 15;
            Rectangle rectCard = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // --- 1. วาดพื้นหลังการ์ดสีเทาอ่อน (ไม่มีเส้นขอบ) ---
            using (GraphicsPath pathCard = GetRoundedPath(rectCard, radius))
            using (SolidBrush brushCard = new SolidBrush(Color.FromArgb(243, 243, 243))) // สีเทาอ่อนแบบในรูป
            {
                g.FillPath(brushCard, pathCard);
            }

            // --- 2. วาดไอคอนโปรไฟล์ (Avatar) วงกลม ---
            int avatarX = 20;
            int avatarY = 20;
            int avatarSize = 44;

            // บันทึกสถานะ Graphics ก่อนตัดขอบเขตวงกลม
            GraphicsState state = g.Save();

            GraphicsPath avatarPath = new GraphicsPath();
            avatarPath.AddEllipse(avatarX, avatarY, avatarSize, avatarSize);
            g.SetClip(avatarPath); // บังคับให้ทุกอย่างที่วาดหลังจากนี้ อยู่ในขอบเขตวงกลมเท่านั้น

            // ถมสีขาวเป็นพื้นหลัง
            g.FillEllipse(Brushes.White, avatarX, avatarY, avatarSize, avatarSize);

            // วาดรูปคนสีเทา (ส่วนหัว และ ส่วนตัว)
            using (SolidBrush brushIcon = new SolidBrush(Color.FromArgb(225, 225, 225)))
            {
                g.FillEllipse(brushIcon, avatarX + 13, avatarY + 9, 18, 18); // ส่วนหัว
                g.FillEllipse(brushIcon, avatarX + 7, avatarY + 29, 30, 25); // ส่วนลำตัว
            }

            g.Restore(state); // คืนค่าสถานะให้กลับมาวาดนอกวงกลมได้ตามปกติ

            // --- 3. วาดชื่อ Username และ คะแนน ---
            int textX = avatarX + avatarSize + 15;
            int textY = avatarY + 10; // ให้อยู่กึ่งกลาง Avatar

            using (Font fontUser = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                g.DrawString(_item.Username, fontUser, brushBlack, textX, textY);

                // หาความกว้างของ Username เพื่อจะได้วาดดาวต่อท้ายได้พอดี
                float nameWidth = g.MeasureString(_item.Username, fontUser).Width;
                float starX = textX + nameWidth + 5;

                // วาดรูปดาว
                string star = "★";
                using (Font fontStar = new Font("Segoe UI", 11f, FontStyle.Regular))
                using (SolidBrush brushStar = new SolidBrush(Color.FromArgb(255, 175, 0)))
                {
                    g.DrawString(star, fontStar, brushStar, starX, textY);
                    float starWidth = g.MeasureString(star, fontStar).Width;

                    // วาดตัวเลขคะแนน
                    using (Font fontRating = new Font("Segoe UI", 10f, FontStyle.Regular))
                    using (SolidBrush brushGray = new SolidBrush(Color.FromArgb(140, 140, 140)))
                    {
                        g.DrawString($"{_item.Rating:0.00}/5.00", fontRating, brushGray, starX + starWidth - 4, textY + 1);
                    }
                }
            }

            // --- 4. วาดข้อความรีวิวแบบหลายบรรทัด ---
            int reviewY = avatarY + avatarSize + 15; // เริ่มวาดใต้ Avatar
            RectangleF reviewRect = new RectangleF(20, reviewY, this.Width - 40, this.Height - reviewY - 10);

            using (Font fontReview = new Font("Segoe UI", 10.5f, FontStyle.Regular))
            using (SolidBrush brushBlack = new SolidBrush(Color.Black))
            {
                // ใส่ RectangleF เข้าไปใน DrawString เพื่อให้มันตัดบรรทัดอัตโนมัติเมื่อข้อความยาวเกิน
                g.DrawString(_item.Comment, fontReview, brushBlack, reviewRect);
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
