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
    public partial class SectionHeaderControl : UserControl
    {
        // --- Constants ---
        private const int LeftMargin = 25;
        private const int BarWidth = 6;
        private const int BarHeight = 28;
        private const int TextGap = 12;

        // --- Cached Resources ---
        private readonly Font _headerFont;
        private readonly SolidBrush _textBrush;
        private readonly SolidBrush _barBrush;
        private readonly StringFormat _textFormat;

        // --- Properties ---
        private string _headerText = "";
        public string HeaderText
        {
            get => _headerText;
            set { _headerText = value; this.Invalidate(); }
        }

        public SectionHeaderControl()
        {
            InitializeComponent();

            // ตั้งค่า GDI+ ให้ลดการกระพริบ และสั่งให้วาดใหม่เมื่อ Resize อัตโนมัติ
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw,
                true);
            this.UpdateStyles();

            this.BackColor = Color.Transparent;
            this.Height = 50;
            this.Dock = DockStyle.Top;

            // สร้าง Resources เก็บไว้ล่วงหน้า
            _headerFont = new Font("Segoe UI", 16f, FontStyle.Bold);
            _textBrush = new SolidBrush(Color.Black);
            _barBrush = new SolidBrush(StylePalette.DarkRed);
            _textFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center, // Vertical Center
                Alignment = StringAlignment.Near       // Horizontal Left
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int centerY = this.Height / 2;

            // วาดแถบสีแดง (Accent Bar)
            int barY = centerY - (BarHeight / 2);
            g.FillRectangle(_barBrush, LeftMargin, barY, BarWidth, BarHeight);

            // วาดข้อความ HeaderText
            int textX = LeftMargin + BarWidth + TextGap;
            RectangleF textRect = new RectangleF(textX, 0, this.Width - textX, this.Height);

            g.DrawString(_headerText, _headerFont, _textBrush, textRect, _textFormat);
        }
    }
}
