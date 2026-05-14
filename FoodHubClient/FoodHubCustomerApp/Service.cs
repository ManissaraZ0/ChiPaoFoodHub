using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp
{
    public class Service
    {
        public event EventHandler<DataEventArgs> OnDataLoaded;
        // เปลี่ยนเป็นดึงข้อมูลจาก Database
        // เอาข้อมูลไปแสดงเป็นการ์ดสีส้ม ๆ สุดน่ารัก

        public void FetchRestaurants()
        {
            // Mock Up Data Sample (ในอนาคตเปลี่ยนเป็นดึงจาก Database ตรงนี้ได้เลย)
            var list = new List<RestaurantItem>();
            for (int i = 1; i <= 10; i++)
            {
                // Args ดูที่ RestaurantItem.cs
                list.Add(new RestaurantItem
                {
                    Name = "Jui's Restaurant " + i,
                    Category = "Japan, Buffet",
                    Rating = 4.85,
                    CardColor = Color.OrangeRed
                });
            }
            OnDataLoaded?.Invoke(this, new DataEventArgs(list));
        }
    }
}
