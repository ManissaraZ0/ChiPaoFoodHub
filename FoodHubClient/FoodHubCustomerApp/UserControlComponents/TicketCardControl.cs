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

namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class TicketCardControl : UserControl
    {
        private TicketItem _item;

        // --- 1. เพิ่ม Constructor สำหรับหน้า Design ---
        public TicketCardControl()
        {
            InitializeComponent();

            // ข้อมูลจำลองสำหรับให้หน้า Design วาดรูปได้
            _item = new TicketItem
            {
                Title = "Drink Promotion",
                Subtitle = "Film's Restaurant",
                SaveText = "SAVE",
                DiscountValue = "99%"
            };

            SetupControl();
        }

        // --- 2. Constructor สำหรับการใช้งานจริง ---
        public TicketCardControl(TicketItem item)
        {
            InitializeComponent();
            _item = item;
            SetupControl();
        }

        // --- 3. ฟังก์ชันรวมการตั้งค่าพื้นฐาน ---
        private void SetupControl()
        {
            this.Size = new Size(170, 110);
            this.Margin = new Padding(10);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;

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

            Color cardRedColor = Color.FromArgb(190, 15, 20);
            int radius = 15;
            Rectangle rectCard = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath pathCard = GetRoundedPath(rectCard, radius))
            using (SolidBrush brushRed = new SolidBrush(cardRedColor))
            {
                g.FillPath(brushRed, pathCard);
            }

            int cX = this.Width / 2;
            using (Pen whitePen = new Pen(Color.White, 2f))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                g.DrawLine(whitePen, cX, 10, cX, 15);
                g.DrawLine(whitePen, cX - 12, 15, cX + 12, 15);
                g.DrawLines(whitePen, new Point[] {
                    new Point(cX - 10, 15),
                    new Point(cX - 6, 35),
                    new Point(cX + 6, 35),
                    new Point(cX + 10, 15)
                });
                g.FillEllipse(whiteBrush, cX - 5, 28, 2.5f, 2.5f);
                g.FillEllipse(whiteBrush, cX + 2, 28, 2.5f, 2.5f);
                g.FillEllipse(whiteBrush, cX - 1.5f, 23, 2.5f, 2.5f);
            }

            StringFormat sfCenter = new StringFormat();
            sfCenter.Alignment = StringAlignment.Center;
            sfCenter.LineAlignment = StringAlignment.Center;

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

            int badgeW = 120;
            int badgeH = 26;
            int badgeX = (this.Width - badgeW) / 2;
            int badgeY = 75;

            Rectangle rectOuterBadge = new Rectangle(badgeX, badgeY, badgeW, badgeH);
            using (GraphicsPath pathOuterBadge = GetRoundedPath(rectOuterBadge, 6))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.FillPath(brushWhite, pathOuterBadge);
            }

            Rectangle rectSaveText = new Rectangle(badgeX, badgeY, 60, badgeH);
            using (Font fontSave = new Font("Segoe UI", 9.5f, FontStyle.Bold))
            using (SolidBrush brushRedText = new SolidBrush(cardRedColor))
            {
                g.DrawString(_item.SaveText, fontSave, brushRedText, rectSaveText, sfCenter);
            }

            int innerW = 54;
            int innerH = 22;
            int innerX = badgeX + badgeW - innerW - 2;
            int innerY = badgeY + 2;

            Rectangle rectInnerBadge = new Rectangle(innerX, innerY, innerW, innerH);
            using (GraphicsPath pathInnerBadge = GetRoundedPath(rectInnerBadge, 5))
            using (SolidBrush brushInnerRed = new SolidBrush(cardRedColor))
            {
                g.FillPath(brushInnerRed, pathInnerBadge);
            }

            using (Font fontDiscount = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                g.DrawString(_item.DiscountValue, fontDiscount, brushWhite, rectInnerBadge, sfCenter);
            }
        }

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