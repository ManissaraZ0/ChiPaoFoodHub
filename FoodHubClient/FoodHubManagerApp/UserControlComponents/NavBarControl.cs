using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class NavBarControl : UserControl
    {
        // --- Menu Item Model ---
        public class NavMenuItem
        {
            public string Text { get; set; }
            public Image Icon { get; set; }   // optional icon image
            public string IconText { get; set; } // fallback icon char/emoji (ถ้าไม่มีรูป)
        }
 
        // --- Properties ---
        private Image _logoImage;
        public Image LogoImage
        {
            get => _logoImage ?? Properties.Resources.logo;
            set { _logoImage = value; this.Invalidate(); }
        }
 
        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;

                    Invalidate(); // บังคับ redraw
                    Update();

                    SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ForceSelect(int index)
        {
            _selectedIndex = index;
            Invalidate();
            Update();
        }

        public event EventHandler SelectedIndexChanged;
 
        // --- Menu Items ---
        private readonly List<NavMenuItem> _menuItems = new List<NavMenuItem>
        {
            new NavMenuItem { IconText = "%", Text = "Promotion" },
            new NavMenuItem { IconText = "🎫", Text = "Ticket" },
            new NavMenuItem { IconText = "☰", Text = "Review" },
        };
 
        public List<NavMenuItem> MenuItems => _menuItems;
 
        // --- Layout ---
        private Rectangle _logoHitbox;
        private readonly List<Rectangle> _menuRects = new List<Rectangle>();
 
        private const int LogoTopPadding = 15;
        private const int MenuStartY = 110;   // ระยะจาก top ถึงเริ่ม menu
        private const int MenuItemHeight = 48;
        private const int MenuItemGap = 4;
        private const int MenuHorizPad = 12;
        private const int IconWidth = 32;
 
        public NavBarControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
        }

        //  Paint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
 
            Rectangle rectFull = new(0, 0, this.Width, this.Height);
 
            // 1. Background gradient (orange→red)
            using (var bgBrush = StylePalette.GetOrangeGradient(rectFull))
                g.FillRectangle(bgBrush, rectFull);
 
            // 2. Logo
            DrawLogo(g);
 
            // 3. Menu items
            DrawMenuItems(g);
        }
 
        private void DrawLogo(Graphics g)
        {
            if (LogoImage == null) return;
 
            int maxLogoW = 210, maxLogoH = 90;
            float scaleX = (float)maxLogoW / LogoImage.Width;
            float scaleY = (float)maxLogoH / LogoImage.Height;
            float scale = Math.Min(scaleX, scaleY);
            int newW = (int)(LogoImage.Width * scale);
            int newH = (int)(LogoImage.Height * scale);
 
            int logoX = (this.Width - newW) / 2;
            _logoHitbox = new Rectangle(logoX, LogoTopPadding, newW, newH);
            g.DrawImage(LogoImage, _logoHitbox);
        }

        private void DrawMenuItems(Graphics g)
        {
            _menuRects.Clear();

            int y = MenuStartY;
            int itemWidth = this.Width - (MenuHorizPad * 2);

            using var selectedBrush = new SolidBrush(Color.White);
            using var selectedTextBrush = new SolidBrush(Color.Black);
            using var normalTextBrush = new SolidBrush(Color.White);
            using var selectedFont = new Font("Segoe UI", 11f, FontStyle.Bold);
            using var normalFont = new Font("Segoe UI", 11f, FontStyle.Regular);
            using var iconFontSelected = new Font("Segoe UI", 13f, FontStyle.Bold);
            using var iconFontNormal = new Font("Segoe UI", 13f, FontStyle.Regular);

            for (int i = 0; i < _menuItems.Count; i++)
            {
                var item = _menuItems[i];
                bool selected = (i == _selectedIndex);

                var rect = new Rectangle(MenuHorizPad, y, itemWidth, MenuItemHeight);
                _menuRects.Add(rect);

                // 1. วาด background ของ item ที่ selected
                if (selected)
                {
                    using var path = GetRoundedRect(rect, 10);
                    g.FillPath(selectedBrush, path);
                }

                // 2. Icon area
                var iconRect = new Rectangle(rect.X + 8, rect.Y, IconWidth, MenuItemHeight);
                var textBrush = selected ? selectedTextBrush : normalTextBrush;
                var iconFont = selected ? iconFontSelected : iconFontNormal;

                // --- ส่วนที่แก้ไข: การวาด Icon ---
                // เช็คจากชื่อ Text หรือตัวแปรอื่น ๆ เพื่อแยกว่าจะวาดไอคอนไหน
                if (item.Text == "Promotion")
                {
                    // กำหนดสีและสลับพฤติกรรมตามโหมด
                    // โหมด Selected = พื้นดำ, เส้นขาว
                    // โหมด Normal = พื้นขาว, เส้นดำ (คุณสามารถปรับแก้สีตรงนี้ได้ถ้าย้อนแย้งกับดีไซน์รวม)
                    Color bgColor = selected ? Color.Black : Color.White;
                    Color fgColor = selected ? Color.White : Color.Black;

                    // ปรับขนาดการวาดให้เล็กลงเพื่อให้พอดีกับพื้นที่ของเมนู
                    // บันทึกสถานะก่อนหน้าของ Graphics
                    var state = g.Save();

                    // เลื่อนจุดศูนย์กลางและย่อส่วน (Scale) เพราะฟังก์ชัน IconPainter เดิมทำไว้ที่ 50x50
                    g.TranslateTransform(iconRect.X + 4, iconRect.Y + 12);
                    g.ScaleTransform(0.5f, 0.5f); // ย่อขนาดลง 50%

                    // เรียกใช้ฟังก์ชันที่แก้ไขให้รับสีได้ (ต้องไปอัปเดต IconPainter ของคุณเล็กน้อยดูด้านล่าง)
                    IconPainter.DrawDiscountIcon(g, 0, 0, bgColor, fgColor);

                    // คืนค่า Graphics ให้กลับเป็นปกติเพื่อวาดส่วนอื่นต่อ
                    g.Restore(state);
                }
                else if (item.Text == "Ticket")
                {
                    Color bgColor = selected ? Color.Black : Color.White;
                    Color fgColor = selected ? Color.White : Color.Black;

                    var state = g.Save();
                    g.TranslateTransform(iconRect.X + 4, iconRect.Y + 12);
                    g.ScaleTransform(0.5f, 0.5f);

                    // ส่ง Rectangle ขนาด 50x50 (จำลอง) เข้าไป เพราะเราย่อขนาดด้วย ScaleTransform ไว้แล้ว 50%
                    Rectangle ticketBounds = new Rectangle(0, 0, 50, 50);
                    IconPainter.DrawTicketIcon(g, ticketBounds, bgColor, fgColor);

                    g.Restore(state);
                }
                else if (item.Text == "Review")
                {
                    Color bgColor = selected ? Color.White : Color.Black;
                    Color fgColor = selected ? Color.Black : Color.White;

                    var state = g.Save();
                    g.TranslateTransform(iconRect.X + 4, iconRect.Y + 12);
                    g.ScaleTransform(0.5f, 0.5f);

                    // เรียกใช้ DrawReviewIcon 
                    IconPainter.DrawReviewIcon(g, 0, 0, bgColor, fgColor);

                    g.Restore(state);
                }
                else if (item.Icon != null)
                {
                    // (โค้ดวาดไอคอนรูปภาพเดิม)
                    int iconSize = 24;
                    int ix = iconRect.X + (iconRect.Width - iconSize) / 2;
                    int iy = iconRect.Y + (iconRect.Height - iconSize) / 2;
                    g.DrawImage(item.Icon, new Rectangle(ix, iy, iconSize, iconSize));
                }
                else if (!string.IsNullOrEmpty(item.IconText))
                {
                    // (โค้ดวาดตัวหนังสือเดิม เผื่อไอเทมอื่นยังต้องใช้อยู่)
                    var iconSize = g.MeasureString(item.IconText, iconFont);
                    float ix = iconRect.X + (iconRect.Width - iconSize.Width) / 2;
                    float iy = iconRect.Y + (iconRect.Height - iconSize.Height) / 2;
                    g.DrawString(item.IconText, iconFont, textBrush, ix, iy);
                }
                // --------------------------------

                // 3. Label
                var labelFont = selected ? selectedFont : normalFont;
                int labelX = rect.X + IconWidth + 12;
                int labelW = rect.Width - IconWidth - 16;
                var labelRect = new RectangleF(labelX, rect.Y, labelW, MenuItemHeight);
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(item.Text, labelFont, textBrush, labelRect, sf);

                y += MenuItemHeight + MenuItemGap;
            }
        }

        // ──────────────────────────────────────────────
        //  Mouse: click เพื่อ select
        // ──────────────────────────────────────────────
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            for (int i = 0; i < _menuRects.Count; i++)
            {
                if (_menuRects[i].Contains(e.Location))
                {
                    SelectedIndex = i;
                    break;
                }
            }
        }
 
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool onItem = false;
            foreach (var rect in _menuRects)
            {
                if (rect.Contains(e.Location)) { onItem = true; break; }
            }
            this.Cursor = onItem ? Cursors.Hand : Cursors.Default;
        }
 
        private static GraphicsPath GetRoundedRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
