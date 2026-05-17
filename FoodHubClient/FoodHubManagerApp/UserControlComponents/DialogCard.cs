using FoodHubManagerApp;
using FoodHubManagerApp.UserControlComponents;
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

namespace FoodHubCustomerApp.UserControlComponents
{
    public enum DialogCardType
    {
        Positive,
        Negative
    }

    public partial class DialogCard : Form
    {
        public DialogCard(string message, DialogCardType cardType, bool showCancelButton = true, bool showIcon = true)
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

            // --- เพิ่มการตั้งค่า Font และการขึ้นบรรทัดใหม่ตรงนี้ ---
            nofifyLabel.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);

            // ปิด AutoSize เพื่อบังคับให้ Label อยู่ในกรอบและปัดข้อความลงมาบรรทัดใหม่
            nofifyLabel.AutoSize = false;
            nofifyLabel.Dock = DockStyle.Fill;
            nofifyLabel.TextAlign = ContentAlignment.MiddleCenter;

            primaryBtn.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            secondaryBtn.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            // ----------------------------------------

            nofifyLabel.Text = message;

            SetupTheme(cardType, showIcon);
            SetupButtons(showCancelButton);

            this.MouseDown += DragForm;
            this.DoubleBuffered = true;

            tableLayoutPanel1.MouseDown += DragForm;
            tableLayoutPanel2.MouseDown += DragForm;
            tableLayoutPanel3.MouseDown += DragForm;
            tableLayoutPanel4.MouseDown += DragForm;
            nofifyLabel.MouseDown += DragForm;
        }

        private void SetupTheme(DialogCardType cardType, bool showIcon)
        {
            bool isPositive = (cardType == DialogCardType.Positive);

            if (isPositive)
            {
                this.BackColor = StylePalette.PrimaryGreen;

                primaryBtn.FontColor = StylePalette.PrimaryGreen;
                secondaryBtn.FontColor = StylePalette.PrimaryGreen;
            }
            else
            {
                this.BackColor = StylePalette.DarkRed;

                primaryBtn.FontColor = StylePalette.DarkRed;
                secondaryBtn.FontColor = StylePalette.DarkRed;
            }

            // แสดง Icon เฉพาะเมื่อ showIcon เป็น true เท่านั้น
            if (showIcon)
            {
                CreateIcon(isPositive);
            }
        }

        private void SetupButtons(bool showCancelButton)
        {
            if (!showCancelButton)
            {
                // กรณีมีปุ่มเดียว (แจ้งเตือน) ให้ซ่อนปุ่ม Cancel
                secondaryBtn.Visible = false;

                // ปรับแต่ง Layout ของ TableLayoutPanel2 ให้มีแค่ 1 คอลัมน์ และกว้าง 100%
                tableLayoutPanel2.ColumnStyles.Clear();
                tableLayoutPanel2.ColumnCount = 1;
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                // ย้ายปุ่ม OK มาไว้ที่คอลัมน์แรกสุด
                tableLayoutPanel2.Controls.Remove(primaryBtn);
                tableLayoutPanel2.Controls.Add(primaryBtn, 0, 0);

                // ยกเลิก Dock และใช้ Anchor None เพื่อให้ปุ่มลอยอยู่กึ่งกลาง Cell
                primaryBtn.Dock = DockStyle.None;
                primaryBtn.Anchor = AnchorStyles.None;
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
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

        // เพิ่มพารามิเตอร์มารับค่าทาง Static Method
        public static DialogResult Show(string message, DialogCardType cardType, bool showCancelButton = true, bool showIcon = true)
        {
            using (var dialog = new DialogCard(message, cardType, showCancelButton, showIcon))
            {
                return dialog.ShowDialog();
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
