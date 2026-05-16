using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubManagerApp.UserControlComponents
{
    public enum DialogCardType
    {
        Positive,
        Negative
    }

    public partial class DialogCard : Form
    {
        public DialogCard(string message, DialogCardType cardType)
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

            nofifyLabel.Text = message;
            SetupTheme(cardType);

            this.MouseDown += DragForm;
            this.DoubleBuffered = true;

            tableLayoutPanel1.MouseDown += DragForm;
            tableLayoutPanel2.MouseDown += DragForm;
            tableLayoutPanel3.MouseDown += DragForm;
            tableLayoutPanel4.MouseDown += DragForm;
            nofifyLabel.MouseDown += DragForm;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void SetupTheme(DialogCardType cardType)
        {
            if (cardType == DialogCardType.Positive)
            {
                CreateIcon(true);
            }
            else
            {
                CreateIcon(false);
            }
        }

        private void CreateIcon(bool isPositive)
        {
            Panel iconPanel = new Panel();
            iconPanel.Dock = DockStyle.Fill;
            iconPanel.BackColor = Color.Transparent;

            iconPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (isPositive)
                {
                    IconPainter.DrawSmileIcon(
                        e.Graphics,
                        (iconPanel.Width - 50) / 2,
                        (iconPanel.Height - 50) / 2
                    );
                }
                else
                {
                    IconPainter.DrawSadIcon(
                        e.Graphics,
                        (iconPanel.Width - 50) / 2,
                        (iconPanel.Height - 50) / 2
                    );
                }
            };

            tableLayoutPanel4.Controls.Add(iconPanel, 1, 0);
        }

        // 3. กำหนดให้ปุ่มส่งค่า DialogResult กลับไป
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes; // ส่งค่า Yes
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;  // ส่งค่า No
            Close();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BeginInvoke(new Action(() =>
            {
                CenterToScreen();
                Opacity = 1;
            }));
        }

        // 4. สร้าง Static Method เพื่อให้เรียกใช้ได้ง่ายๆ เหมือน MessageBox
        public static DialogResult Show(string message, DialogCardType cardType)
        {
            using (var dialog = new DialogCard(message, cardType))
            {
                return dialog.ShowDialog(); // ใช้ ShowDialog() เพื่อหยุดโค้ดรอจนกว่าผู้ใช้จะกดปุ่ม
            }
        }

        // --- ส่วน Drag Form โค้ดเดิม ---
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
