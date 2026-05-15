using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodHubManagerApp.UserControlComponents;

namespace FoodHubManagerApp.UserControlPages
{
    public partial class PromotionsPage : UserControl
    {
        public PromotionsPage(ManagerApp form)
        {
            InitializeComponent();

            SetupUI();
        }

        private void SetupUI()
        {
            SectionHeaderControl headRec = new SectionHeaderControl();
            headRec.HeaderText = "List of Promotions";
            headRec.Dock = DockStyle.Top;
            splitContainer3.Panel1.Controls.Add(headRec);
        }
    }
}
