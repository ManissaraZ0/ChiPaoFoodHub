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

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class TicketListItemControl : UserControl
    {
        private TicketItem _item;
        // ── Public Properties ──────────────────────────────────────────────
        public string TicketID
        {
            get => lblTicketID.Text;
            set => lblTicketID.Text = value;
        }

        public string SubInfo
        {
            get => lblSubInfo.Text;
            set => lblSubInfo.Text = value;
        }

        // ── Event ──────────────────────────────────────────────────────────
        public event EventHandler AcceptClicked;

        // ── Controls ───────────────────────────────────────────────────────
        private Label lblTicketID;
        private Label lblSubInfo;
        private RoundedButton btnAccept;

        // ── Constructor ────────────────────────────────────────────────────
        // ✅ เพิ่ม constructor เปล่าสำหรับ Designer
        public TicketListItemControl()
        {
            InitializeComponent();
            InitializeComponents();
        }

        // ✅ constructor จริงที่ใช้งาน
        public TicketListItemControl(TicketItem item)
        {
            _item = item;
            InitializeComponent();
            InitializeComponents();

            TicketID = $"Ticket: {item.Id}";
            SubInfo = $"User ID: {item.UserId}, {item.Title}";
        }

        private void InitializeComponents()
        {
            lblTicketID = new Label();
            lblSubInfo = new Label();
            btnAccept = new RoundedButton();

            SuspendLayout();

            // ── Control itself ─────────────────────────────────────────────
            BackColor = Color.White;
            Size = new Size(960, 90);
            Padding = new Padding(0);
            Margin = new Padding(0, 6, 0, 6);

            // ── lblTicketID ────────────────────────────────────────────────
            lblTicketID.Text = "Ticket ID";
            lblTicketID.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            lblTicketID.ForeColor = Color.Black;
            lblTicketID.Location = new Point(20, 12);
            lblTicketID.AutoSize = true;

            // ── lblSubInfo ─────────────────────────────────────────────────
            lblSubInfo.Text = "User ID: XXXX, Promotion Title, Type A";
            lblSubInfo.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            lblSubInfo.ForeColor = Color.FromArgb(120, 120, 120);
            lblSubInfo.Location = new Point(20, 40);
            lblSubInfo.AutoSize = true;

            // ── btnAccept ──────────────────────────────────────────────────
            btnAccept.Text = "Accept";
            btnAccept.Font = new Font("Segoe UI", 11f, FontStyle.Regular);
            btnAccept.ForeColor = Color.White;
            btnAccept.BackColor = Color.FromArgb(180, 20, 20);   // dark red
            btnAccept.Size = new Size(120, 48);
            btnAccept.Location = new Point(820, 21);
            btnAccept.Cursor = Cursors.Hand;
            btnAccept.CornerRadius = 12;
            btnAccept.Click += BtnAccept_Click;

            Controls.AddRange(new Control[] { lblTicketID, lblSubInfo, btnAccept });

            ResumeLayout(false);
        }

        // ── Paint border / card background ─────────────────────────────────
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = 14;

            using (GraphicsPath path = RoundedRect(rect, radius))
            using (Pen pen = new Pen(Color.FromArgb(210, 210, 210), 1.5f))
            {
                g.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Re-position button when control resizes
            btnAccept.Location = new Point(Width - btnAccept.Width - 20, (Height - btnAccept.Height) / 2);
            Invalidate();
        }

        // ── Event handler ──────────────────────────────────────────────────
        private void BtnAccept_Click(object sender, EventArgs e)
        {
            AcceptClicked?.Invoke(this, e);
        }

        // ── Helper: rounded rectangle path ────────────────────────────────
        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // ════════════════════════════════════════════════════════════════════════
    // RoundedButton — custom Button with rounded corners
    // ════════════════════════════════════════════════════════════════════════
    public class RoundedButton : Button
    {
        public int CornerRadius { get; set; } = 10;

        private bool _isHovered;
        private bool _isPressed;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.DoubleBuffer, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isPressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Choose shade based on state
            Color bg = BackColor;
            if (_isPressed)
                bg = ControlPaint.Dark(BackColor, 0.15f);
            else if (_isHovered)
                bg = ControlPaint.Light(BackColor, 0.05f);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (GraphicsPath path = RoundedRect(rect, CornerRadius))
            using (SolidBrush brush = new SolidBrush(bg))
            {
                g.FillPath(brush, path);
            }

            // Draw text
            TextRenderer.DrawText(g, Text, Font, rect, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
