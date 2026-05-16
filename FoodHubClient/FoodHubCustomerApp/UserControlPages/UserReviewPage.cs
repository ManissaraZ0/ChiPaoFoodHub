using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubCustomerApp.Logics;
using FoodHubCustomerApp.Model;

namespace FoodHubCustomerApp.UserControlPages
{
    public partial class UserReviewPage : UserControl
    {
        CustomerApp form;

        private UserRsp userData;

        private RestaurantRecommendationRsp resPrevious;

        public RestaurantRecommendationRsp ResPrevious
        {
            get => resPrevious;
            set
            {
                resPrevious = value;
                if (resPrevious != null)
                {
                    // 1. นำข้อมูลมาใส่ (แอบเติมคำว่า "Category: " เข้าไปให้เหมือนหน้า Design)
                    lblRestaurantName.Text = resPrevious.Name;
                    lblCategory.Text = "Category: " + resPrevious.Category;
                    lblRatingScore.Text = resPrevious.OverallRating.ToString("0.00") + "/5.00";
                    lblRestaurantDescription.Text = resPrevious.Address;

                    // --- 2. การจัดการ Layout แบบไดนามิก (Dynamic Positioning) ---

                    // รีเซ็ตให้คุมขนาดตัวเองอัตโนมัติก่อน
                    lblRestaurantName.AutoSize = true;
                    lblCategory.AutoSize = true;

                    int gap = 15; // ระยะห่างระหว่างชื่อร้านกับ Category (15 px)
                    int maxAllowedWidth = 896; // พื้นที่สูงสุดที่ตกลงกันไว้
                    int totalWidth = lblRestaurantName.Width + gap + lblCategory.Width;

                    if (totalWidth > maxAllowedWidth)
                    {
                        // กรณีที่ 1: ชื่อร้านยาวเกินไป! 
                        // เราจะปิด AutoSize ของชื่อร้าน แล้วบังคับความกว้างไม่ให้เกินพื้นที่ที่เหลือ
                        lblRestaurantName.AutoSize = false;
                        lblRestaurantName.AutoEllipsis = true; // สั่งให้ใส่จุดไข่ปลา (...) อัตโนมัติเมื่อข้อความล้น
                        lblRestaurantName.Width = maxAllowedWidth - lblCategory.Width - gap;
                        lblRestaurantName.Height = 45; // ล็อคความสูงไว้ให้พอดีกับ Font 24pt
                    }

                    // 3. ย้ายตำแหน่ง lblCategory ไปต่อท้าย lblRestaurantName เสมอ
                    lblCategory.Left = lblRestaurantName.Right + gap;

                    // 4. (แถม) ปรับระดับความสูง (Baseline) ให้อยู่ในระนาบเดียวกันให้ดูสวยงาม
                    // เนื่องจาก Font ชื่อร้านใหญ่กว่า เราเลยดึง Category ลงมาด้านล่างนิดหน่อย
                    lblCategory.Top = lblRestaurantName.Top + (lblRestaurantName.Height - lblCategory.Height) - 4;
                }
            }
        }

        public UserRsp UserData
        {
            get => userData;
            set
            {
                userData = value;
                UserSession.Username = userData.Username;
                navBarControl.UserData = userData;
                //navBarControl.RefreshUserProfile();
            }
        }

        public UserReviewPage(CustomerApp form)
        {
            InitializeComponent();
            this.form = form;
            SetupUI();
            SetupEventHandlers();

            // กำหนดสีพื้นหลังให้ปุ่ม Add Post โดยดึงสีมาจาก StylePalette
            btnAddPost.BackColor = StylePalette.DarkRed;
        }
        public void RefreshData()
        {
            //
        }

        private void SetupUI()
        {
            // ตั้งค่าบัญชีผู้ใช้
            navBarControl.RefreshUserProfile();
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => form.ChangeScreen(this, 0, userData);
            navBarControl.HeartClicked += (s, e) => form.ChangeScreen(this, 4, userData, resPrevious);
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => form.ChangeScreen(this, 3, userData);

            // เพื่อบอกให้ปุ่มรู้ว่าต้องใช้ฟังก์ชันนี้ตอนวาดตัวเอง
            btnAddPost.Paint += btnAddPost_Paint;
        }

        private void btnAddPost_Paint(object sender, PaintEventArgs e)
        {
            // 1. ตัดขอบ PictureBox ให้กลายเป็นวงกลมเหมือนเดิม
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, btnAddPost.Width - 1, btnAddPost.Height - 1);
            btnAddPost.Region = new Region(path);

            // 2. สร้างกรอบสี่เหลี่ยมเพื่อบอกระยะให้ IconPainter (เว้นขอบ Padding เข้ามาหน่อย ไอคอนจะได้ไม่ชนขอบปุ่ม)
            Rectangle iconBounds = new Rectangle(0, 0, btnAddPost.Width, btnAddPost.Height);

            // 3. เรียกใช้ฟังก์ชันวาดปากกาสีขาว ทับลงไปบนพื้นหลังสี DarkRed
            FoodHubCustomerApp.UserControlComponents.IconPainter.DrawPenIcon(e.Graphics, iconBounds);
        }
    }
}