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

namespace FoodHubCustomerApp.UserControlComponents
{
    // ป้องกันไฟล์ทับของจุ้ย
    public partial class ImageScreenControlNew : UserControl
    {
        private Image _displayImage;

        // Property สำหรับรับรูปภาพเข้ามา
        public Image DisplayImage
        {
            get { return _displayImage; }
            set
            {
                _displayImage = value;
                this.Invalidate(); // สั่งให้วาด UI ใหม่ทันทีเมื่อมีการเปลี่ยนรูป
            }
        }

        public ImageScreenControlNew()
        {
            this.Size = new Size(650, 350);
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // จำเป็นต้องเปิด Transparent ไว้เพื่อให้ขอบที่เกลี่ยสี (Anti-Alias) กลืนไปกับพื้นหลังของ Form
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // 1. เปิดโหมดความคมชัดและเกลี่ยขอบให้เนียนกริบ (Anti-Alias)
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int radius = 15;

            // สร้างกรอบโดยหักลบออก 1 พิกเซล เพื่อให้มีพื้นที่เหลือให้ระบบวาดเส้นเกลี่ยขอบ (Anti-Alias) ได้ ไม่โดนตัดทิ้ง
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            {
                if (_displayImage != null)
                {
                    // **วิธีทำให้ขอบรูปภาพเนียน: ใช้ TextureBrush**
                    // สร้างหน้ากระดาษจำลองขนาดเท่า Control เพื่อดึงภาพมาขยายให้พอดีก่อน
                    using (Bitmap bmp = new Bitmap(this.Width, this.Height))
                    {
                        using (Graphics gBmp = Graphics.FromImage(bmp))
                        {
                            gBmp.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gBmp.DrawImage(_displayImage, 0, 0, this.Width, this.Height);
                        }

                        // เปลี่ยนภาพจำลองให้กลายเป็น "พู่กันลายรูปภาพ" แล้วระบายลงในขอบโค้ง
                        using (TextureBrush tb = new TextureBrush(bmp))
                        {
                            g.FillPath(tb, path);
                        }
                    }
                }
                else
                {
                    // **กรณีไม่มีรูปภาพ: วาด Gradient** (FillPath รองรับขอบเนียนโดยอัตโนมัติ)
                    // เราดึง Rectangle แบบไม่หักลบขอบมาใช้กับ Gradient Brush เพื่อให้สีไปถึงขอบพอดี
                    Rectangle gradientRect = new Rectangle(0, 0, this.Width, this.Height);
                    using (LinearGradientBrush gradientBrush = StylePalette.GetOrangeGradient(gradientRect))
                    {
                        g.FillPath(gradientBrush, path);
                    }
                }
            }
        }

        // Helper: ฟังก์ชันวาดกรอบสี่เหลี่ยมมุมโค้ง
        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
