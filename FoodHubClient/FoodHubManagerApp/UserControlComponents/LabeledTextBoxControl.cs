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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class LabeledTextBoxControl : UserControl
    {
        public LabeledTextBoxControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string PlaceholderText
        {
            get => textBox1.PlaceholderText;
            set => textBox1.PlaceholderText = value;
        }

        public string Value
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        private void PanelBorder_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Panel)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // ลบของเก่าก่อน
            g.Clear(panel.Parent?.BackColor ?? Color.White);

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            int radius = 20;

            using GraphicsPath path = RoundedRect(rect, radius);
            g.FillPath(Brushes.White, path);
            using Pen pen = new Pen(Color.LightGray, 1.5f);
            g.DrawPath(pen, path);
        }

        private GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
