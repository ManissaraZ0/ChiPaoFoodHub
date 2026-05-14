using System.IO;
namespace FoodHubCustomerApp
{
    public partial class CustomerApp : Form
    {
        private readonly Service _service;

        public CustomerApp()
        {
            InitializeComponent();
            _service = new Service();

            SetupUserSession();
            SetupNavBar();
            SetupEventHandlers(); // ลง Event ไว้ตั้งแต่เริ่มเปิด Form

            // สร้าง Header สำหรับร้านอาหารแนะนำ
            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "Recommendation Restaurants";
            sectionHeaderControl1.Controls.Add(headRec);

            _service.FetchRestaurants();
        }

        private void SetupUserSession()
        {
            UserSession.Username = "OscarPattJuiFilmHeng";
            navBarControl1.RefreshUserProfile();
        }

        private void SetupNavBar()
        {
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "logo.png");
            if (File.Exists(imagePath))
                navBarControl1.LogoImage = Image.FromFile(imagePath);
            else
                MessageBox.Show($"หาไฟล์โลโก้ไม่พบ กรุณาตรวจสอบที่: \n{imagePath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void SetupEventHandlers()
        {
            navBarControl1.LogoClicked += NavBarControl1_LogoClicked;
            navBarControl1.ProfileClicked += NavBarControl1_ProfileClicked;
            navBarControl1.HeartClicked += NavBarControl1_HeartClicked;
            navBarControl1.BellClicked += NavBarControl1_BellClicked;
            flowLayoutPanel1.SizeChanged += FlowLayoutPanel1_SizeChanged;

            // --- สำคัญ! ผูก Event (Subscribe) รอรับข้อมูลจาก Service ---
            _service.OnDataLoaded += RestaurantService_OnDataLoaded;
        }

        // ฟังก์ชันนี้จะทำงานอัตโนมัติ เมื่อ Service เตรียมข้อมูลเสร็จ
        private void RestaurantService_OnDataLoaded(object sender, DataEventArgs e)
        {
            // เคลียร์หน้าจอ
            flowLayoutPanel1.Controls.Clear();

            // นำข้อมูล (e.Items) จากซองจดหมายมาสร้างการ์ด
            foreach (var item in e.Items)
            {
                var card = new ItemCardControl(item);
                card.Click += Card_Click;
                flowLayoutPanel1.Controls.Add(card);
            }

            // จัด Layout ใหม่หลังจากสร้างการ์ดเสร็จ
            ResizeCards();
        }

        // Click Logo
        private void NavBarControl1_LogoClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"กลับหน้าแรก");
        }

        // Click Heart 
        private void NavBarControl1_HeartClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"การกดถูกใจ");
        }

        // Click Noticification
        private void NavBarControl1_BellClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"แสดงการแจ้งเตือน");
        }

        // Click Profile
        private void NavBarControl1_ProfileClicked(object sender, EventArgs e)
        {
            MessageBox.Show($"เปิดหน้ารายละเอียดของบัญชี: {UserSession.Username}");
        }

        // Click Card

        private void Card_Click(object sender, EventArgs e)
        {
            if (sender is ItemCardControl clickedCard)
            {
                MessageBox.Show("คุณได้คลิกการ์ดเรียบร้อยแล้ว!", "แจ้งเตือน");
            }
        }

        // Responsive Card Design
        private void FlowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            ResizeCards();
        }

        // Dynamic Card Layout (Fixed Columns)
        private void ResizeCards()
        {
            int totalItems = flowLayoutPanel1.Controls.Count;
            if (totalItems == 0) return;

            int columns = 5;
            int marginSize = 10; // ค่า Margin มาตรฐานที่เราอยากใช้
            int availableWidth = flowLayoutPanel1.ClientSize.Width - 15;

            int newWidth = (availableWidth / columns) - (marginSize * 2);
            int newHeight = (int)(newWidth * (275.0 / 230.0));

            // คำนวณหาจุดเริ่มต้นของแถวสุดท้าย
            // เช่น ถ้ามี 12 ใบ แถวสุดท้ายจะเริ่มที่ใบที่ 10 (Index 10, 11)
            int lastRowStartIndex = ((totalItems - 1) / columns) * columns;

            flowLayoutPanel1.SuspendLayout();

            for (int i = 0; i < totalItems; i++)
            {
                if (flowLayoutPanel1.Controls[i] is ItemCardControl card)
                {
                    card.Size = new Size(newWidth, newHeight);

                    // --- Logic การจัดการ Margin รายใบ ---
                    int top = marginSize;
                    int bottom = marginSize;

                    // 1. ถ้าอยู่แถวแรก (Index 0 ถึง columns-1) ให้ Top เป็น 0
                    if (i < columns) top = 0;

                    // 2. ถ้าอยู่แถวสุดท้าย (Index ตั้งแต่ lastRowStartIndex เป็นต้นไป) ให้ Bottom เป็น 0
                    if (i >= lastRowStartIndex) bottom = 0;

                    // กรณีพิเศษ: ถ้ามีการ์ดแค่แถวเดียว ทั้ง top และ bottom จะเป็น 0
                    card.Margin = new Padding(marginSize, top, marginSize, bottom);
                }
            }

            // ส่วนการคำนวณ Padding ของ FlowLayoutPanel เพื่อทำ Center Alignment
            int totalContentWidth = columns * (newWidth + (marginSize * 2));
            int paddingLeft = Math.Max(0, (flowLayoutPanel1.ClientSize.Width - totalContentWidth) / 2);
            flowLayoutPanel1.Padding = new Padding(paddingLeft, 10, 0, 10);

            flowLayoutPanel1.ResumeLayout(true);
            flowLayoutPanel1.AutoScroll = false;
            flowLayoutPanel1.AutoScroll = true;
        }
    }
}
