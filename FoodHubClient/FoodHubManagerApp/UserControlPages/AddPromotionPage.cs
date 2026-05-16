using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    public partial class AddPromotionPage : UserControl
    {
        ManagerApp form;
        private int restaurantId = 1;
        public int RestaurantId
        {
            get => restaurantId;
            //set { restaurantId = value; txtRestaurantId.Text = value.ToString();
            internal set;
        }
        private int managerId = 1;
        public int ManagerId
        {
            get => managerId;
            //set { restaurantId = value; txtManagerId.Text = value.ToString(); }
            internal set;
        }

        public AddPromotionPage(ManagerApp form)
        {
            InitializeComponent();
            this.form = form;

            navBarControl1.SelectedIndex = 0;

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
                    form.ChangeScreen(this, RestaurantId, ManagerId, 2);
                    break;
                case 2: // Review
                    form.ChangeScreen(this, RestaurantId, ManagerId, 3);
                    break;
            }
        }

        private void buttonControl1_Load(object sender, EventArgs e)
        {
            form.ChangeScreen(this, RestaurantId, ManagerId, 1);
        }

        private void buttonControl2_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Save Promotion Clicked!");
            form.ChangeScreen(this, RestaurantId, ManagerId, 1);
		}
    }
}
