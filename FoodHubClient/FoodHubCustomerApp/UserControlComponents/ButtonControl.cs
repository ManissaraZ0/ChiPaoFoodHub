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

namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class ButtonControl : UserControl
    {
        // Enum สำหรับเลือกโหมดปุ่ม
        public enum ButtonStyle { Fill, Outline }

        // Properties ที่จะโผล่ในหน้าต่าง Properties ของ Visual Studio
        [Category("Custom Props")]
        public string ButtonText { get; set; } = "Button";

        [Category("Custom Props")]
        public Color ButtonColor { get; set; } = StylePalette.DarkRed;

        [Category("Custom Props")]
        public Color FontColor { get; set; } = Color.White;

        [Category("Custom Props")]
        public ButtonStyle FillMode { get; set; } = ButtonStyle.Fill;

        private bool isHovered = false;

        public ButtonControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // ลดการกะพริบ
            this.BackColor = Color.Transparent; // ให้พื้นหลังโปร่งใสตาม Parent
            this.Cursor = Cursors.Hand; // เปลี่ยนเมาส์เป็นรูปมือ
        }

        // จับเหตุการณ์เมาส์เพื่อทำ Hover Effect
        protected override void OnMouseEnter(EventArgs e) { isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { isHovered = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color finalBgColor;
            Color finalTextColor;
            Color finalBorderColor;

            if (FillMode == ButtonStyle.Fill)
            {
                if (isHovered)
                {
                    // Inverse ของ Fill: พื้นหลังต้องเป็นสีสว่าง (FontColor) ตัวหนังสือเป็นสีเข้ม (ButtonColor)
                    finalBgColor = FontColor;
                    finalTextColor = ButtonColor;
                    finalBorderColor = ButtonColor;
                }
                else
                {
                    finalBgColor = ButtonColor;
                    finalTextColor = FontColor;
                    finalBorderColor = Color.Transparent;
                }
            }
            else // Mode: Outline
            {
                if (isHovered)
                {
                    // Inverse ของ Outline: พื้นหลังต้องเป็นสีเข้ม (ButtonColor) ตัวหนังสือต้องเป็นสีสว่าง (FontColor)
                    finalBgColor = ButtonColor;
                    finalTextColor = FontColor; // <--- จุดนี้แหละครับ ต้องตั้ง FontColor เป็นสีขาวใน Properties
                    finalBorderColor = ButtonColor;
                }
                else
                {
                    finalBgColor = Color.Transparent;
                    finalTextColor = ButtonColor; // ปกติของ Outline ให้ใช้สีปุ่มเป็นสีฟอนต์จะสวยกว่า
                    finalBorderColor = ButtonColor;
                }
            }

            // --- ส่วนการวาด (เหมือนเดิม) ---
            int radius = this.Height - 1;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (GraphicsPath path = GetRoundedPath(rect, radius))
            {
                if (finalBgColor != Color.Transparent)
                {
                    using (SolidBrush brush = new SolidBrush(finalBgColor))
                        g.FillPath(brush, path);
                }

                using (Pen pen = new Pen(finalBorderColor, 2))
                    g.DrawPath(pen, path);
            }

            TextRenderer.DrawText(g, ButtonText, this.Font, rect, finalTextColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
