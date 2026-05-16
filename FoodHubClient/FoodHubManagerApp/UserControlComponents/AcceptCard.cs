using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FoodHubManagerApp.UserControlComponents
{
    public partial class AcceptCard : Form
    {
        private int _ticketId;
        public AcceptCard(int ticketId)
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true
            );

            Opacity = 0;
            ShowInTaskbar = false;

            _ticketId = ticketId;

            label1.Text = $"Confirm Ticket #{_ticketId}";

            CreateSmileIcon();

            this.MouseDown += DragForm;
            this.DoubleBuffered = true;

            tableLayoutPanel1.MouseDown += DragForm;
            tableLayoutPanel2.MouseDown += DragForm;
            tableLayoutPanel3.MouseDown += DragForm;
            tableLayoutPanel4.MouseDown += DragForm;
            label1.MouseDown += DragForm;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // ให้ layout/render เสร็จก่อน
            BeginInvoke(new Action(() =>
            {
                CenterToScreen();

                Opacity = 1;
            }));
        }

        private void CreateSmileIcon()
        {
            Panel iconPanel = new Panel();
            iconPanel.Dock = DockStyle.Fill;
            iconPanel.BackColor = Color.Transparent;

            iconPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                IconPainter.DrawSmileIcon(
                    e.Graphics,
                    (iconPanel.Width - 50) / 2,
                    (iconPanel.Height - 50) / 2
                );
            };

            // column = 1, row = 0
            tableLayoutPanel4.Controls.Add(iconPanel, 1, 0);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            int lParam
        );

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(
                    this.Handle,
                    WM_NCLBUTTONDOWN,
                    HTCAPTION,
                    0
                );
            }
        }
    }
}
