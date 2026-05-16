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
        private TextBox _searchTextBox;
        private string _placeholder = "Search...";
        private bool _isPlaceholder = true;

        [Category("Appearance")]
        [Description("Placeholder text shown when the search box is empty.")]
        public string Placeholder
        {
            get => _placeholder;
            set
            {
                _placeholder = value;
                // อัปเดต TextBox ทันทีถ้ายังแสดง placeholder อยู่
                if (_isPlaceholder && _searchTextBox != null)
                {
                    _searchTextBox.Text = _placeholder;
                }
                Invalidate();
            }
        }

        // ให้ดึง text จาก TextBox (ไม่รวม placeholder)
        public string SearchText => _isPlaceholder ? "" : _searchTextBox.Text;

        // Event เมื่อ text เปลี่ยน
        public event EventHandler SearchTextChanged;
        public event EventHandler<string> SearchSubmitted; // ← เพิ่ม

        public SearchBarControl()
        {
            InitializeComponent();
            InitSearchTextBox();
        }

        private void InitSearchTextBox()
        {
            _searchTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                ForeColor = Color.Gray,
                Text = _placeholder,
                Cursor = Cursors.IBeam,
            };

            _searchTextBox.GotFocus += OnTextBoxGotFocus;
            _searchTextBox.LostFocus += OnTextBoxLostFocus;
            _searchTextBox.TextChanged += OnTextBoxTextChanged;
            _searchTextBox.KeyDown += OnTextBoxKeyDown;

            this.Controls.Add(_searchTextBox);
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SearchSubmitted?.Invoke(this, SearchText);
            }
        }

        private void OnTextBoxGotFocus(object sender, EventArgs e)
        {
            if (_isPlaceholder)
            {
                _searchTextBox.Text = "";
                _searchTextBox.ForeColor = Color.Black;
                _isPlaceholder = false;
            }
        }

        private void OnTextBoxLostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_searchTextBox.Text))
            {
                _isPlaceholder = true;
                _searchTextBox.ForeColor = Color.Gray;
                _searchTextBox.Text = _placeholder;
            }
        }

        private void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            if (!_isPlaceholder)
                SearchTextChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionTextBox();
        }

        private void PositionTextBox()
        {
            if (_searchTextBox == null) return;

            int searchBarWidth = 500;
            int searchBarHeight = 40;
            int searchX = (this.Width - searchBarWidth) / 2;
            int searchY = (this.Height - searchBarHeight) / 2;

            // icon กว้างประมาณ 20px + padding 20px ซ้าย + gap 8px = เริ่มที่ 48
            int iconOffset = 48;
            int textBoxWidth = searchBarWidth - iconOffset - 20; // -20 สำหรับ padding ขวา

            _searchTextBox.SetBounds(
                searchX + iconOffset,
                searchY + (searchBarHeight - _searchTextBox.Height) / 2,
                textBoxWidth,
                _searchTextBox.Height
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int searchBarWidth = 500;
            int searchBarHeight = 40;
            int searchX = (this.Width - searchBarWidth) / 2;
            int searchY = (this.Height - searchBarHeight) / 2;
            Rectangle rectSearch = new Rectangle(searchX, searchY, searchBarWidth, searchBarHeight);

            // ===== Shadow =====
            Rectangle rectShadow = new Rectangle(
                searchX - 2,
                searchY - 1,
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
                using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    g.FillPath(brushWhite, pathSearch);

                using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 0.3f))
                {
                    borderPen.Alignment = PenAlignment.Inset;
                    g.DrawPath(borderPen, pathSearch);
                }
            }

            // Search Icon
            IconPainter.DrawSearchIcon(g, searchX + 20, searchY + 12);

            // วาง TextBox ให้ถูกตำแหน่งทุกครั้งที่ Paint (ป้องกัน resize)
            PositionTextBox();
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