using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubManagerApp.UserControlPages
{
    public partial class TicketsPage : UserControl
    {
        ManagerApp form;
        private int restaurantId = 1;
        public int RestaurantId
        {
            get => restaurantId;
            set { 
                restaurantId = value; 
                navBarControl1.SelectedIndex = 1; // เลือก Ticket tab เมื่อเปลี่ยนร้าน
            }
        }
        private int managerId = 1;
        public int ManagerId
        {
            get => managerId;
            set
            {
                managerId = value;
            }
        }

        public TicketsPage(ManagerApp form)
        {
            InitializeComponent();
            this.form = form;

            navBarControl1.SelectedIndexChanged += NavBarControl1_SelectedIndexChanged;
        }

        private void NavBarControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (navBarControl1.SelectedIndex)
            {
                case 0: // Promotion
                    form.ChangeScreen(this, RestaurantId, ManagerId, 1);
                    break;
                case 1: // Ticket
                    form.ChangeScreen(this, RestaurantId, ManagerId, 0);
                    break;
                case 2: // Review
                    form.ChangeScreen(this, RestaurantId, ManagerId, 2);
                    break;
            }
        }
    }
}
