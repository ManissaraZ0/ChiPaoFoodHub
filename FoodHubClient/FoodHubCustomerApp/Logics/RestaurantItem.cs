using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp.Logics
{
    // Detail สำหรับแสดงผลใน Card (Restaurant)
    public class RestaurantItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }
        public Color CardColor { get; set; }
    }
}
