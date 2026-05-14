using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp
{
    public partial class SectionHeaderControl : UserControl
    {
        // --- Properties ---
        private string _headerText = "Recommendation Restaurants";
        public string HeaderText
        {
            get => _headerText;
            set { _headerText = value; this.Invalidate(); } // วาดใหม่เมื่อเปลี่ยนข้อความ
        }

        public SectionHeaderControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.Height = 50; // ความสูงมาตรฐานของหัวข้อ
            this.Dock = DockStyle.Top; // ให้เกาะขอบบนของ Panel ที่นำไปวาง
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // --- 1. Fixed Margin ตามขนาดจอ 1280 (คือ 25px) ---
            // เราจะไม่ใช้ Dynamic ที่คำนวณตามการขยายของ Grid แล้วเพื่อให้ Gap คงที่
            int leftMargin = 25;
            int centerY = this.Height / 2;

            // --- 2. วาดแถบสีแดง (Accent Bar) ---
            int barWidth = 6;  // ปรับให้เพรียวลงตามเรฟ image_bc5bd6.png
            int barHeight = 28;
            int barY = centerY - (barHeight / 2);

            using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(190, 15, 20)))
            {
                g.FillRectangle(barBrush, leftMargin, barY, barWidth, barHeight);
            }

            // --- 3. วาดข้อความ HeaderText (แก้ Vertical Center ให้เป๊ะ) ---
            using (Font font = new Font("Segoe UI", 16f, FontStyle.Bold)) // ปรับขนาดลงเล็กน้อยให้ดูทันสมัย
            using (SolidBrush textBrush = new SolidBrush(Color.Black))
            {
                int textGap = 12; // ระยะห่างจากแถบแดง (Gap ที่คุณต้องการให้เท่ากับตอน 1280)
                int textX = leftMargin + barWidth + textGap;

                // ใช้ StringFormat เพื่อคุม Vertical Alignment ให้กึ่งกลางจากแกนกลางจริงๆ 
                // ไม่ใช่กึ่งกลางตามความสูงตัวอักษรที่รวมหาง (Descent)
                using (StringFormat sf = new StringFormat())
                {
                    sf.LineAlignment = StringAlignment.Center; // Vertical Center
                    sf.Alignment = StringAlignment.Near;      // Horizontal Left

                    // สร้าง Layout Rectangle สำหรับวาดข้อความให้ครอบคลุมความสูงของ Control
                    RectangleF textRect = new RectangleF(textX, 0, this.Width - textX, this.Height);

                    g.DrawString(_headerText, font, textBrush, textRect, sf);
                }
            }
        }

        // สั่งให้วาดใหม่เมื่อมีการ Resize หน้าจอ (เพื่อให้ Margin ขยับตามถ้าจำเป็น)
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
    }
}
