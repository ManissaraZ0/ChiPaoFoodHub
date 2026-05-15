using FoodHubCustomerApp.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp
{
    public class ServiceMock
    {
        // สร้าง Event (Observer Pattern) 
        public event Action<List<RestaurantItem>> OnRestaurantsLoaded;

        public void FetchRestaurants()
        {
            List<RestaurantItem> items = GetMockData();
            OnRestaurantsLoaded?.Invoke(items);
        }

        private List<RestaurantItem> GetMockData()
        {
            // ข้อมูลจำลองสำหรับทดสอบ
            return new List<RestaurantItem>
            {
                new RestaurantItem { Name = "Sushi Masa", Category = "Japan, Buffet", Rating = 4.85 },
                new RestaurantItem { Name = "KFC", Category = "Fast Food", Rating = 4.00 },
                new RestaurantItem { Name = "MK Suki", Category = "Suki, Family", Rating = 4.50 },
                new RestaurantItem { Name = "Starbucks", Category = "Cafe, Beverage", Rating = 4.70 },
                new RestaurantItem { Name = "Momo Paradise", Category = "Shabu, Buffet", Rating = 4.90 },
                new RestaurantItem { Name = "Sushi Masa", Category = "Japan, Buffet", Rating = 4.85 },
                new RestaurantItem { Name = "KFC", Category = "Fast Food", Rating = 4.00 },
                new RestaurantItem { Name = "MK Suki", Category = "Suki, Family", Rating = 4.50 },
                new RestaurantItem { Name = "Starbucks", Category = "Cafe, Beverage", Rating = 4.70 },
                new RestaurantItem { Name = "Momo Paradise", Category = "Shabu, Buffet", Rating = 4.90 }
            };
        }
    }
}
