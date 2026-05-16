using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomerApp.UserControlComponents
{
    public partial class BackButtonControl : UserControl
    {
        private bool _isHovered = false;
        private Color _outlineColor = Color.Black;
        private float _strokeWidth = 2.5f;

        public Color OutlineColor
        {
            get => _outlineColor;
            set { _outlineColor = value; Invalidate(); }
        }

        public float StrokeWidth
        {
            get => _strokeWidth;
            set { _strokeWidth = value; Invalidate(); }
        }

        public BackButtonControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);

            this.BackColor = Color.Transparent; 
            this.Cursor = Cursors.Hand;         
            this.Size = new Size(45, 45);     
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            IconPainter.DrawBackIcon(e.Graphics, this.ClientRectangle, _outlineColor, _strokeWidth, _isHovered);
        }
    }
}
