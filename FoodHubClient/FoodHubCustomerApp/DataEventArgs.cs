using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp
{
    public class DataEventArgs : EventArgs
    {
        public List<RestaurantItem> Items { get; }
        public DataEventArgs(List<RestaurantItem> items)
        {
            Items = items;
        }
    }
}
