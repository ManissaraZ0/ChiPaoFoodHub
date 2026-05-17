using System;
using System.Drawing;
using System.Windows.Forms;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class DatePopupControl : UserControl
    {
        private MonthCalendar monthCalendar;
        private DateTimePicker timePicker;
        private Button btnOk;

        // สร้าง Event ไว้แจ้งเตือนเมื่อกดปุ่ม OK
        public event EventHandler<DateTime> OnDateSelected;

        public DatePopupControl(DateTime initialDate)
        {
            this.BackColor = Color.White;
            this.Size = new Size(230, 240);
            this.BorderStyle = BorderStyle.FixedSingle; // ใส่ขอบให้ดูเป็นกรอบ Popup

            monthCalendar = new MonthCalendar
            {
                Location = new Point(0, 0),
                MaxSelectionCount = 1,
                SelectionStart = initialDate
            };
            this.Controls.Add(monthCalendar);

            timePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Location = new Point(10, monthCalendar.Bottom + 5),
                Width = 120,
                Value = initialDate
            };
            this.Controls.Add(timePicker);

            btnOk = new Button
            {
                Text = "OK",
                Location = new Point(140, monthCalendar.Bottom + 4),
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightBlue
            };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar.SelectionStart;
            DateTime selectedTime = timePicker.Value;
            
            DateTime finalDate = new DateTime(
                selectedDate.Year, selectedDate.Month, selectedDate.Day,
                selectedTime.Hour, selectedTime.Minute, selectedTime.Second);

            // ส่งสัญญาณพร้อมข้อมูลวันที่กลับไป
            OnDateSelected?.Invoke(this, finalDate);
        }
    }
}