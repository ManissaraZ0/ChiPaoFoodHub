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
using FoodHubManagerApp.Logics;
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
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 1);
                    break;
                case 1: // Ticket
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 2);
                    break;
                case 2: // Review
                    form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 3);
                    break;
            }
        }

        private void buttonControl1_Load(object sender, EventArgs e)
        {
            form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 1);
        }

        private void buttonControl2_Load(object sender, EventArgs e)
        {
            string promotionTitle = labeledTextBoxControl1.Value;
            string promotionDescription = labeledTextBoxControl2.Value;

            try
            {
                int.Parse(labeledTextBoxControl3.Value);
            }
            catch (FormatException)
            {
                MessageBox.Show("Promotion quota must be a valid integer.");
                return;
            }

            int promotionQuota = int.Parse(labeledTextBoxControl3.Value);

            if (promotionQuota <= 0)
            {
                MessageBox.Show("Promotion quota must be a positive integer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(promotionTitle) || string.IsNullOrWhiteSpace(promotionDescription) || string.IsNullOrWhiteSpace(labeledTextBoxControl3.Value))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }


            form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 1);
		}
    }
}
