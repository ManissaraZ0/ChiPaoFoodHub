using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubManagerApp.Model;
using FoodHubManagerApp.UserControlPages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace FoodHubManagerApp
{
    public partial class ManagerApp : Form
    {
        PromotionsPage promotions;
        AddPromotionPage addPromotion;
        TicketsPage tickets;
        //ReviewsPage reviews;

        public ManagerApp()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            promotions = new PromotionsPage(this) { Dock = DockStyle.Fill };
            addPromotion = new AddPromotionPage(this) { Dock = DockStyle.Fill };
            tickets = new TicketsPage(this) { Dock = DockStyle.Fill };
            //reviews = new ReviewsPage(this) { Dock = DockStyle.Fill };
        }

        private void ManagerApp_Load(object sender, EventArgs e)
        {
            this.Controls.Add(promotions);
        }

        public void ChangeScreen(object sender, int restaurantId, int managerId, int action)
        {
            Debug.WriteLine($"ChangeScreen called with sender: {sender.GetType().Name}, restaurantId: {restaurantId}, managerId: {managerId}, action: {action}");
            if (sender is PromotionsPage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    this.Controls.Add(promotions);
                }
                else if (action == 1)
                {
                    this.Controls.Clear();
                    addPromotion.RestaurantId = restaurantId;
                    addPromotion.ManagerId = managerId;
                    this.Controls.Add(addPromotion);
                }
                else if (action == 2)
                {
                    this.Controls.Clear();
                    tickets.RestaurantId = restaurantId;
                    tickets.ManagerId = managerId;
                    this.Controls.Add(tickets);
                }
                //    else if (action == 3)
                //    {
                //        this.Controls.Clear();
                //        reviews.RestaurantId = restaurantId;
                //        reviews.ManagerId = managerId;
                //        this.Controls.Add(reviews);
                //    }
            }
            if (sender is AddPromotionPage)
            {
                if (action == 1)
                {
                    this.Controls.Clear();
                    promotions.RestaurantId = restaurantId;
                    promotions.ManagerId = managerId;
                    this.Controls.Add(promotions);
                }
                else if (action == 2)
                {
                    this.Controls.Clear();
                    tickets.RestaurantId = restaurantId;
                    tickets.ManagerId = managerId;
                    this.Controls.Add(tickets);
                }
                //    else if (action == 3)
                //    {
                //        this.Controls.Clear();
                //        reviews.RestaurantId = restaurantId;
                //        reviews.ManagerId = managerId;
                //        this.Controls.Add(reviews);
                //    }
            }
            if (sender is TicketsPage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    this.Controls.Add(tickets);
                }
                else if (action == 1)
                {
                    this.Controls.Clear();
                    promotions.RestaurantId = restaurantId;
                    promotions.ManagerId = managerId;
                    this.Controls.Add(promotions);
                }
                //else if (action == 2)
                //{
                //    this.Controls.Clear();
                //    reviews.RestaurantId = restaurantId;
                //    reviews.ManagerId = managerId;
                //    this.Controls.Add(reviews);
                //}
            }
            //if (sender is ReviewsPage)
            //{
            //    if (action == 0)
            //    {
            //        this.Controls.Clear();
            //        this.Controls.Add(reviews);
            //    }
            //    else if (action == 1)
            //    {
            //        this.Controls.Clear();
            //        tickets.RestaurantId = restaurantId;
            //        tickets.ManagerId = managerId;
            //        this.Controls.Add(tickets);
            //    }
            //    else if (action == 2)
            //    {
            //        this.Controls.Clear();
            //        promotions.RestaurantId = restaurantId;
            //        promotions.ManagerId = managerId;
            //        this.Controls.Add(promotions);
            //    }
            //}
        }
    }
}
