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
using FoodHubManagerApp.UserControlComponents;
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
        ReviewsPage reviews;
        DemoLoginPage demoLoginPage;
        MyResSelectPage myResSelectPage;

        public ManagerApp()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            promotions = new PromotionsPage(this) { Dock = DockStyle.Fill };
            addPromotion = new AddPromotionPage(this) { Dock = DockStyle.Fill };
            tickets = new TicketsPage(this) { Dock = DockStyle.Fill };
            reviews = new ReviewsPage(this) { Dock = DockStyle.Fill };
            demoLoginPage = new DemoLoginPage(this) { Dock = DockStyle.Fill };
            myResSelectPage = new MyResSelectPage(this) { Dock = DockStyle.Fill };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Controls.Contains(reviews))
            {
                reviews.RefreshData();
            }
            else if (this.Controls.Contains(tickets))
            {
                tickets.RefreshData();
            }
        }

        private void ManagerApp_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Controls.Add(demoLoginPage);
        }

        public void ChangeScreen(object sender, int restaurantId, int managerId, int action)
        {
            if (action == 2002)
            {
                this.Controls.Clear();
                this.Controls.Add(demoLoginPage);
            }
            if (sender is DemoLoginPage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    myResSelectPage.ManagerId = (int)managerId;
                    this.Controls.Add(myResSelectPage);
                }
            }
            if (sender is MyResSelectPage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    promotions.RestaurantId = (int)restaurantId;
                    promotions.ManagerId = (int)managerId;
                    this.Controls.Add(promotions);
                }
                else if (action == 1)
                {
                    this.Controls.Clear();
                    this.Controls.Add(demoLoginPage);
                }
            }
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
                    addPromotion.RestaurantId = (int)restaurantId;
                    addPromotion.ManagerId = (int)managerId;
                    this.Controls.Add(addPromotion);
                }
                else if (action == 2)
                {
                    this.Controls.Clear();
                    tickets.RestaurantId = (int)restaurantId;
                    tickets.ManagerId = (int)managerId;
                    this.Controls.Add(tickets);
                }
                else if (action == 3)
                {
                    this.Controls.Clear();
                    reviews.RestaurantId = (int)restaurantId;
                    reviews.ManagerId = (int)managerId;
                    this.Controls.Add(reviews);
                }
            }
            if (sender is AddPromotionPage)
            {
                if (action == 1)
                {
                    this.Controls.Clear();
                    promotions.RestaurantId = (int)restaurantId;
                    promotions.ManagerId = (int)managerId;
                    this.Controls.Add(promotions);
                }
                else if (action == 2)
                {
                    this.Controls.Clear();
                    tickets.RestaurantId = (int)restaurantId;
                    tickets.ManagerId = (int)managerId;
                    this.Controls.Add(tickets);
                }
                else if (action == 3)
                {
                    this.Controls.Clear();
                    reviews.RestaurantId = (int)restaurantId;
                    reviews.ManagerId = (int)managerId;
                    this.Controls.Add(reviews);
                }
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
                    promotions.RestaurantId = (int)restaurantId;
                    promotions.ManagerId = (int)managerId;
                    this.Controls.Add(promotions);
                }
                else if (action == 2)
                {
                    this.Controls.Clear();
                    reviews.RestaurantId = (int)restaurantId;
                    reviews.ManagerId = (int)managerId;
                    this.Controls.Add(reviews);
                }
            }
            if (sender is ReviewsPage)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    this.Controls.Add(reviews);
                }
                else if (action == 1)
                {
                    this.Controls.Clear();
                    tickets.RestaurantId = (int)restaurantId;
                    tickets.ManagerId = (int)managerId;
                    this.Controls.Add(tickets);
                }
                else if (action == 2)
                {
                    this.Controls.Clear();
                    promotions.RestaurantId = (int)restaurantId;
                    promotions.ManagerId = (int)managerId;
                    this.Controls.Add(promotions);
                }
            }
            if (sender is DialogCard)
            {
                if (action == 0)
                {
                    this.Controls.Clear();
                    tickets.RestaurantId = (int)restaurantId;
                    tickets.ManagerId = (int)managerId;
                    this.Controls.Add(tickets);
                }
            }
        }
    }
}
