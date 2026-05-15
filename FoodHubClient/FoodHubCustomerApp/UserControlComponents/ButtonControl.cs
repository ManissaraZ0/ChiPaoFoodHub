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

            // คำนวณสีตอน Hover (ถ้า Hover ให้สีเข้มขึ้นหรืออ่อนลงนิดหน่อย)
            Color drawColor = isHovered ? ControlPaint.Light(ButtonColor, 0.2f) : ButtonColor;

            // วาดทรงแคปซูล (Pill Shape)
            int radius = this.Height - 1;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            GraphicsPath path = GetRoundedPath(rect, radius);

            if (FillMode == ButtonStyle.Fill)
            {
                // โหมด Fill: ถมสีพื้นหลัง
                using (SolidBrush brush = new SolidBrush(drawColor))
                    g.FillPath(brush, path);
            }
            else
            {
                // โหมด Outline: วาดเฉพาะเส้นขอบ
                using (Pen pen = new Pen(drawColor, 2))
                    g.DrawPath(pen, path);
            }

            // วาดตัวอักษรกึ่งกลางปุ่ม
            TextRenderer.DrawText(g, ButtonText, this.Font, rect, FontColor,
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
