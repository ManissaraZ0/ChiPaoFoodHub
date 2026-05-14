using FoodHubCustomerApp.UserControlPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp
{
    public partial class CustomerApp : Form
    {
        HomePage homePage;

        public CustomerApp()
        {
            InitializeComponent();
            homePage = new HomePage() { Dock = DockStyle.Fill };
            ChangeScreen(homePage);
        }

        public void ChangeScreen(UserControl targetScreen)
        {
            this.Controls.Clear();
            this.Controls.Add(targetScreen);
            targetScreen.BringToFront();
        }
    }
}
