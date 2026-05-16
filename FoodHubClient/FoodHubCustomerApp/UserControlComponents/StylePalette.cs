using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace FoodHubCustomerApp
{
    public static class StylePalette
    {
        public static readonly Color DarkRed = Color.FromArgb(192, 7, 7);         // #C00707
        public static readonly Color PrimaryOrange = Color.FromArgb(255, 68, 0);    // #FF4400
        public static readonly Color LightOrange = Color.FromArgb(255, 179, 63);    // #FFB33F
        public static readonly Color PrimaryBlue = Color.FromArgb(19, 78, 142);     // #134E8E
        public static readonly Color PrimaryGreen = Color.FromArgb(111, 150, 12);

        // --- ธีม Gradient ---
        // ธีมสีส้มโบราณ Gradient
        public static LinearGradientBrush GetOrangeGradient(Rectangle bounds)
        {
            return new LinearGradientBrush(
                bounds,
                DarkRed,        
                PrimaryOrange, 
                LinearGradientMode.ForwardDiagonal);
        }
    }
}
