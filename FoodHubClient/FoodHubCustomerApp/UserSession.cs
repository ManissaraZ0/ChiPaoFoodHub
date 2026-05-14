using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp
{
    public static class UserSession
    {
        // เก็บชื่อผู้ใช้
        public static string Username { get; set; }

        // (Optional) เก็บรูปโปรไฟล์ ถ้าในอนาคตมีรูปจริงๆ
        public static Image AvatarImage { get; set; }

        // เช็คว่ามีคน Login อยู่ไหม
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Username);

        // ฟังก์ชันล้างข้อมูลตอน Logout
        public static void Logout()
        {
            Username = null;
            AvatarImage = null;
        }
    }
}
