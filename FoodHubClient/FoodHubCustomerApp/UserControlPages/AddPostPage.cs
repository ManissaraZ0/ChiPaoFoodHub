using FoodHubCustomerApp.Logics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp.UserControlPages
{
    public partial class AddPostPage : UserControl
    {
        public AddPostPage()
        {
            InitializeComponent();
            SetupUI();
            SetupEventHandlers();
        }

        private void SetupUI()
        {
            // ตั้งค่าบัญชีผู้ใช้
            UserSession.Username = "OscarPattJuiFilmHeng";
            navBarControl.RefreshUserProfile();
        }

        private void SetupEventHandlers()
        {
            // UI Events (ใช้ Lambda ย่อโค้ดให้สั้นลง ไม่ต้องสร้าง Method แยกให้รก)
            navBarControl.LogoClicked += (s, e) => MessageBox.Show("กลับหน้าแรก");
            navBarControl.HeartClicked += (s, e) => MessageBox.Show("การกดถูกใจ");
            navBarControl.BellClicked += (s, e) => MessageBox.Show("แสดงการแจ้งเตือน");
            navBarControl.ProfileClicked += (s, e) => MessageBox.Show($"เปิดบัญชี: {UserSession.Username}");
        }
    }
}
