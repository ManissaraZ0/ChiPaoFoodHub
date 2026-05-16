using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubManagerApp.Logics;

public class PromotionItem
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string Value { get; set; } // ใส่เป็น string เพื่อความยืดหยุ่น (เช่น "99", "150")
}
