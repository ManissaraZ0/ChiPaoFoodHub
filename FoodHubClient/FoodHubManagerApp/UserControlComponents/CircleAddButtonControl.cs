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

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class CircleAddButtonControl : UserControl
    {
        private Color buttonColor = StylePalette.DarkRed;
        private Color plusColor = Color.White;

        public event EventHandler ButtonClick;

        public CircleAddButtonControl()
        {
            InitializeComponent();

            this.Click += (s, e) =>
            {
                ButtonClick?.Invoke(this, e);
            };

            this.Size = new Size(120, 120);
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // วาดวงกลม
            using (SolidBrush brush = new SolidBrush(buttonColor))
            {
                g.FillEllipse(brush, 0, 0, Width - 1, Height - 1);
            }

            // วาดเครื่องหมาย +
            using (Pen pen = new Pen(plusColor, 6))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int centerX = Width / 2;
                int centerY = Height / 2;
                int plusSize = Width / 5;

                // เส้นแนวนอน
                g.DrawLine(pen,
                    centerX - plusSize,
                    centerY,
                    centerX + plusSize,
                    centerY);

                // เส้นแนวตั้ง
                g.DrawLine(pen,
                    centerX,
                    centerY - plusSize,
                    centerX,
                    centerY + plusSize);
            }
        }

        // Property สำหรับเปลี่ยนสีปุ่ม
        public Color ButtonColor
        {
            get => buttonColor;
            set
            {
                buttonColor = value;
                Invalidate();
            }
        }

        // Property สำหรับเปลี่ยนสีเครื่องหมาย +
        public Color PlusColor
        {
            get => plusColor;
            set
            {
                plusColor = value;
                Invalidate();
            }
        }
    }
}
