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

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class SearchBarControl : UserControl
    {
        public SearchBarControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            // 3. วาด Search Bar 
            int searchBarWidth = 500;
            int searchBarHeight = 40;
            int searchX = (this.Width - searchBarWidth) / 2;
            int searchY = (this.Height - searchBarHeight) / 2;

            Rectangle rectSearch = new Rectangle(searchX, searchY, searchBarWidth, searchBarHeight);

            // ===== Shadow =====
            Rectangle rectShadow = new Rectangle(
                searchX - 2,     // ขยายซ้าย
                searchY - 1,     // ขยายบน
                searchBarWidth + 4,
                searchBarHeight + 4
            );

            using (GraphicsPath pathShadow = GetPillShape(rectShadow))
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(35, 0, 0, 0)))
            {
                g.FillPath(shadowBrush, pathShadow);
            }

            // ===== Main search bar =====
            using (GraphicsPath pathSearch = GetPillShape(rectSearch))
            {
                // fill สีขาว
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                {
                    g.FillPath(brushWhite, pathSearch);
                }

                // border สีเทาบางๆ
                using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 0.3f))
                {
                    borderPen.Alignment = PenAlignment.Inset;
                    g.DrawPath(borderPen, pathSearch);
                }
            }

            // Search Icon
            IconPainter.DrawSearchIcon(g, searchX + 20, searchY + 12);
        }

        private GraphicsPath GetPillShape(Rectangle bounds)
        {
            int radius = bounds.Height;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 90, 180);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, -90, 180);
            path.CloseFigure();
            return path;
        }
    }
}
