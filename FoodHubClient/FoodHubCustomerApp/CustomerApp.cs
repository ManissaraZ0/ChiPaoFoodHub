namespace FoodHubCustomerApp
{
    public partial class CustomerApp : Form
    {
        public CustomerApp()
        {
            InitializeComponent();
            RefreshData();
            flowLayoutPanel1.SizeChanged += FlowLayoutPanel1_SizeChanged;
        }

        // --- ฟังก์ชันหลักสำหรับโหลดข้อมูลและสร้างการ์ด ---
        private void RefreshData()
        {
            // 1. ล้างข้อมูลเก่าบนหน้าจอออกให้หมด
            flowLayoutPanel1.Controls.Clear();

            // 2. โหลดข้อมูล (ในที่นี้คือเรียกใช้ Mock Data)
            var items = GetMockData();

            // 3. วนลูปสร้างการ์ดทีละใบ
            for (int i = 0; i < items.Count; i++)
            {
                var card = new ItemCardControl(items[i]);

                // เพิ่ม Event สำหรับรับการคลิก
                card.Click += Card_Click;

                // นำการ์ดไปใส่ในกล่อง FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        // --- Event เมื่อมีการคลิกที่การ์ด ---
        private void Card_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลการ์ดที่ถูกคลิกมาตรวจสอบได้ (ถ้าต้องการ)
            ItemCardControl clickedCard = sender as ItemCardControl;
            MessageBox.Show("คุณได้คลิกการ์ดเรียบร้อยแล้ว!", "แจ้งเตือน");
        }

        // --- ฟังก์ชันสร้างข้อมูลจำลอง (Mock Data) ---
        private List<RestaurantItem> GetMockData()
        {
            var list = new List<RestaurantItem>();
            for (int i = 1; i <= 10; i++) // สร้างการ์ดจำลอง 10 ใบ
            {
                list.Add(new RestaurantItem
                {
                    Name = "Jui's Restaurant " + i,
                    Category = "Japan, Buffet",
                    Rating = 4.85,
                    CardColor = Color.OrangeRed // สีส้มอมแดงเหมือนในรูป
                });
            }
            return list;
        }

        private void FlowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            ResizeCards();
        }

        private void ResizeCards()
        {
            // ป้องกันการทำงานตอนที่ยังไม่มีการ์ด
            if (flowLayoutPanel1.Controls.Count == 0) return;

            int columns = 5; // ต้องการ 5 การ์ดต่อ 1 แถวเสมอ
            int marginPerCard = 20; // Margin ซ้าย 10 + ขวา 10

            // หักพื้นที่ออกเล็กน้อย (เผื่อขอบและ Scrollbar) เพื่อป้องกันการ์ดปัดตกบรรทัด
            int availableWidth = flowLayoutPanel1.ClientSize.Width - 15;

            // 1. คำนวณความกว้างและความสูงของการ์ดแต่ละใบ
            int newWidth = (availableWidth / columns) - marginPerCard;
            int newHeight = (int)(newWidth * (275.0 / 230.0)); // คงสัดส่วนเดิม

            // --- จุดที่เพิ่มเข้ามาเพื่อทำ Center Alignment ---
            // 2. คำนวณหาความกว้าง "รวมทั้งหมด" ที่การ์ด 5 ใบใช้ไปจริงๆ
            int totalContentWidth = columns * (newWidth + marginPerCard);

            // 3. หาพื้นที่ว่างที่เหลืออยู่ แล้วหาร 2 เพื่อเอาไปดันขอบซ้าย (Center)
            int paddingLeft = Math.Max(0, (flowLayoutPanel1.ClientSize.Width - totalContentWidth) / 2);

            flowLayoutPanel1.SuspendLayout();

            // 4. ตั้งค่า Padding ด้านซ้ายให้ FlowLayoutPanel ดัน Content ให้ตรงกลางเป๊ะ!
            // (ตั้งค่าขอบบนและล่างเป็น 10 เพื่อความสวยงาม)
            flowLayoutPanel1.Padding = new Padding(paddingLeft, 10, 0, 10);

            // 5. นำขนาดใหม่ไปอัปเดตให้การ์ดทุกใบ
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is ItemCardControl card)
                {
                    card.Size = new Size(newWidth, newHeight);
                }
            }

            flowLayoutPanel1.ResumeLayout(true);

            // ปิด-เปิด AutoScroll ใหม่ เพื่อกระตุ้นให้ Scroll Bar ทำงานปกติเวลากด Full Screen
            flowLayoutPanel1.AutoScroll = false;
            flowLayoutPanel1.AutoScroll = true;
        }
    }
}
