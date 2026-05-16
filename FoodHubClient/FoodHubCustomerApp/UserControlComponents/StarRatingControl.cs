using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class StarRatingControl : UserControl
    {
        public event EventHandler<int> RatingClicked;

        public int SelectedRating { get; set; } = 0; // คะแนนที่เลือกจริง
        private int hoverRating = 0;               // คะแนนตอนเมาส์ชี้
        private int starSize = 30;                 // ขนาดของดาวแต่ละดวง
        private int spacing = 10;                  // ช่องไฟระหว่างดาว

        public StarRatingControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(200, 40);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            // คำนวณว่าเมาส์อยู่บนดาวดวงที่เท่าไหร่
            int currentHover = (e.X / (starSize + spacing)) + 1;
            if (currentHover > 5) currentHover = 5;

            if (hoverRating != currentHover)
            {
                hoverRating = currentHover;
                Invalidate(); // สั่งให้วาดใหม่
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hoverRating = 0; // เมื่อเมาส์ออก ให้เลิกโชว์สถานะ Hover
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // คำนวณดาวที่คลิก
            int clickedRating = (e.X / (starSize + spacing)) + 1;
            if (clickedRating > 5) clickedRating = 5;

            // Logic Toggle: ถ้ากดซ้ำดวงเดิมให้เป็น 0
            if (SelectedRating == clickedRating)
                SelectedRating = 0;
            else
                SelectedRating = clickedRating;

            // สั่งวาดหน้าจอใหม่
            Invalidate();

            // ส่งสัญญาณบอกหน้า AddPostPage (Pop-up จะทำงานเพราะบรรทัดนี้จ๊ะ)
            RatingClicked?.Invoke(this, SelectedRating);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // ใช้ hoverRating ถ้ามีการเอาเมาส์มาชี้ ถ้าไม่มีให้ใช้ SelectedRating
            int displayRating = (hoverRating > 0) ? hoverRating : SelectedRating;

            for (int i = 1; i <= 5; i++)
            {
                int x = (i - 1) * (starSize + spacing);
                bool isFilled = i <= displayRating;
                Color color = isFilled ? Color.Gold : Color.Transparent;

                IconPainter.DrawStar(e.Graphics, x, 5, starSize, isFilled, color);
            }
        }
    }
}
