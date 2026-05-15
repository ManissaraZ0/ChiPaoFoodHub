using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp.Logics
{
    public class TicketItem
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string SaveText { get; set; } // เช่น "SAVE"
        public string DiscountValue { get; set; } // เช่น "99%"
    }
}
