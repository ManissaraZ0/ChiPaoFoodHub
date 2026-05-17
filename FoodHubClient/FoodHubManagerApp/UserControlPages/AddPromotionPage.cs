using FoodHubCustomerApp.UserControlComponents;
using FoodHubManagerApp.Logics;
using FoodHubManagerApp.Model;
using FoodHubManagerApp.UserControlComponents;
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

namespace FoodHubManagerApp.UserControlPages
{
    public partial class AddPromotionPage : UserControl
    {
        ManagerApp form;

        private int restaurantId;
        public int RestaurantId
        {
            get => restaurantId;
            set { restaurantId = value; }
        }
        private int managerId;
        public int ManagerId
        {
            get => managerId;
            set { managerId = value; }
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
                DialogCard.Show($"Promotion quota must be a valid integer.", DialogCardType.Negative, false, true);
                return;
            }

            int promotionQuota = int.Parse(labeledTextBoxControl3.Value);

            if (promotionQuota <= 0)
            {
                DialogCard.Show($"Promotion quota must be a positive integer.", DialogCardType.Negative, false, true);
                return;
            }

            DateTime startDate = labeledDateTimePicker2.Value;

            DateTime endDate = labeledDateTimePicker1.Value;

            if (startDate < DateTime.Now)
            {
                DialogCard.Show($"Promotion start date must be in the future.", DialogCardType.Negative, false, true);
                return;
            }

            if (endDate <= startDate)
            {
                DialogCard.Show($"Promotion end date must be in the future.", DialogCardType.Negative, false, true);
                return;
            }

            try
            {
                double.Parse(labeledTextBoxControl4.Value);
            }
            catch (FormatException)
            {
                DialogCard.Show($"Promotion price must be a valid number.", DialogCardType.Negative, false, true);
                return;
            }

            double promotionPrice = double.Parse(labeledTextBoxControl4.Value);

            if (promotionPrice <= 0)
            {
                DialogCard.Show($"Promotion price must be a positive number.", DialogCardType.Negative, false, true);
                return;
            }

            if (string.IsNullOrWhiteSpace(promotionTitle) || string.IsNullOrWhiteSpace(promotionDescription) || string.IsNullOrWhiteSpace(labeledTextBoxControl3.Value) || string.IsNullOrWhiteSpace(labeledTextBoxControl4.Value))
            {
                DialogCard.Show($"Please fill in all the fields.", DialogCardType.Negative, false, true);
                return;
            }

            // Show All Input
            //MessageBox.Show($"Promotion Title: {promotionTitle}\nPromotion Description: {promotionDescription}\nPromotion Quota: {promotionQuota}\nPromotion End Date: {endDate}");

            // Show Current RestaurantId and ManagerId
            //MessageBox.Show($"Current RestaurantId: {RestaurantId}\nCurrent ManagerId: {ManagerId}");

            var confirmResult = DialogCard.Show("Are you sure to add promotion?", DialogCardType.Positive, true, false);

            if (confirmResult == DialogResult.Yes)
            {
                var success = Service.AddPromotion(RestaurantId, ManagerId, new AddPromotionReq
                {
                    Title = promotionTitle,
                    Price = promotionPrice,
                    Conditions = promotionDescription,
                    TotalQuota = promotionQuota,
                    StartDate = startDate,
                    EndDate = endDate
                });

                if (success == null)
                {
                    DialogCard.Show($"Failed to add promotion. Please try again.", DialogCardType.Negative, false, true);
                    return;
                }

                DialogCard.Show($"Promotion added successfully!", DialogCardType.Positive, false, true);
                form.ChangeScreen(this, data: new DataDetail { RestaurantId = RestaurantId, ManagerId = ManagerId }, 1);
            }
        }
    }
}
