using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class LabeledDateTimePicker : UserControl
    {
        private DateTime _value = DateTime.Now;

        public LabeledDateTimePicker()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // ตั้ง Event วาดขอบมน
            panelBorder.Paint += PanelBorder_Paint;
            panelBorder.Resize += (s, e) => panelBorder.Invalidate();

            // ผูก Event ให้กดที่ TextBox หรือ Panel ก็เปิดปฏิทินได้
            textBox1.Click += OpenCalendarPopup;
            panelBorder.Click += OpenCalendarPopup;

            UpdateDisplay();
        }

        // Property สำหรับหัวข้อ
        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        // Property สำหรับวันเวลา (นำไปบันทึกลง Database ได้เลย)
        public DateTime Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            textBox1.Text = _value.ToString("dd/MM/yyyy, HH:mm:ss");
        }

        // เมธอดเปิด Popup ปฏิทิน
        private void OpenCalendarPopup(object sender, EventArgs e)
        {
            // 1. สร้าง UserControl ปฏิทินที่เตรียมไว้ (DatePopupControl)
            var popupControl = new DatePopupControl(_value);

            // 2. สร้างโครงสร้าง Dropdown 
            var hostControl = new ToolStripControlHost(popupControl)
            {
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            var dropDownPopup = new ToolStripDropDown();
            dropDownPopup.Items.Add(hostControl);

            // 3. รอรับสัญญาณตอนกด OK
            popupControl.OnDateSelected += (s, selectedDate) =>
            {
                this.Value = selectedDate; // อัปเดตค่ากลับมา
                dropDownPopup.Close();     // สั่งปิด Dropdown
            };

            // 4. สั่งให้ Dropdown โชว์ออกมาใต้ Panel พอดี
            dropDownPopup.Show(panelBorder, new Point(0, panelBorder.Height));
        }

        // โค้ดวาดขอบมน (เหมือนของคุณเป๊ะ 100%)
        private void PanelBorder_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Panel)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // --- ส่วนที่แก้ไข: ดักจับสี Transparent เพื่อแก้ปัญหามุมดำ ---
            Color bgColor = this.BackColor;
            Control currentParent = this.Parent;

            // วนหา Parent ชั้นนอกสุดจนกว่าจะเจอสีที่ไม่ใช่ Transparent
            while (bgColor == Color.Transparent && currentParent != null)
            {
                bgColor = currentParent.BackColor;
                currentParent = currentParent.Parent;
            }

            // ถ้าหาไม่ได้จริงๆ (หรือเป็น Transparent ทั้งหมด) ให้ใช้สีขาวเป็น Default
            if (bgColor == Color.Transparent)
            {
                bgColor = Color.White;
            }

            // ลบของเก่าด้วยสีที่หามาได้ (จะไม่มีสีดำโผล่มาแล้ว)
            g.Clear(bgColor);
            // -----------------------------------------------------

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