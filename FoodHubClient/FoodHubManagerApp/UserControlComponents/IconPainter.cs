using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubManagerApp.UserControlComponents
{
    public static class IconPainter
    {
        public static void DrawSearchIcon(Graphics g, int x, int y)
        {
            using (Pen penSearch = new Pen(Color.Black, 2f))
            {
                g.DrawEllipse(penSearch, x, y, 12, 12);
                g.DrawLine(penSearch, x + 10, y + 10, x + 16, y + 16);
            }
        }

        public static void DrawBellIcon(Graphics g, int x, int y)
        {
            using (Pen penBell = new Pen(Color.White, 3f))
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                penBell.LineJoin = LineJoin.Round;
                penBell.StartCap = LineCap.Round;
                penBell.EndCap = LineCap.Round;

                using (GraphicsPath bellPath = new GraphicsPath())
                {
                    bellPath.AddLine(x, y + 12, x, y + 8);
                    bellPath.AddArc(x, y, 16, 16, 180, 180);
                    bellPath.AddLine(x + 16, y + 8, x + 16, y + 12);
                    g.DrawPath(penBell, bellPath);
                }

                g.DrawLine(penBell, x - 2, y + 12, x + 18, y + 12);
                g.FillEllipse(brushWhite, x + 6, y + 15, 4, 4);
            }
        }

        public static void DrawHeartIcon(Graphics g, int x, int y)
        {
            using (Pen penHeart = new Pen(Color.White, 3f))
            using (GraphicsPath heartPath = new GraphicsPath())
            {
                penHeart.LineJoin = LineJoin.Round;
                penHeart.StartCap = LineCap.Round;
                penHeart.EndCap = LineCap.Round;

                heartPath.AddArc(x, y, 12, 12, 135, 225);
                heartPath.AddArc(x + 12, y, 12, 12, 180, 225);
                heartPath.AddLine(x + 22.24f, y + 10.24f, x + 12, y + 21);
                heartPath.AddLine(x + 12, y + 21, x + 1.76f, y + 10.24f);
                heartPath.CloseFigure();

                g.DrawPath(penHeart, heartPath);
            }
        }

        public static void DrawDefaultAvatar(Graphics g, int x, int y, int size)
        {
            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            using (GraphicsPath clipPath = new GraphicsPath())
            {
                GraphicsState state = g.Save();
                clipPath.AddEllipse(x, y, size, size);
                g.SetClip(clipPath);

                g.FillEllipse(brushWhite, x + 10, y + 6, 16, 16);
                g.FillEllipse(brushWhite, x + 4, y + 24, 28, 20);

                g.Restore(state);
            }
        }

        public static void DrawStar(Graphics g, int x, int y, int size, bool isFilled, Color starColor)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // คำนวณจุดยอดของดาว 5 แฉก (10 จุด)
            PointF[] points = new PointF[10];
            double outerRadius = size / 2.0;
            double innerRadius = outerRadius * 0.4; // ความลึกของแฉก
            PointF center = new PointF((float)(x + outerRadius), (float)(y + outerRadius));

            for (int i = 0; i < 10; i++)
            {
                // เริ่มวาดจากด้านบน (มุม -90 องศา)
                double angle = Math.PI * (i * 36 - 90) / 180.0;
                double r = (i % 2 == 0) ? outerRadius : innerRadius;
                points[i] = new PointF(
                    (float)(center.X + r * Math.Cos(angle)),
                    (float)(center.Y + r * Math.Sin(angle))
                );
            }

            if (isFilled)
            {
                using (SolidBrush brush = new SolidBrush(starColor))
                    g.FillPolygon(brush, points);
            }

            // วาดเส้นขอบสีดำเสมอเพื่อให้เห็นทรงดาวชัดเจน
            using (Pen pen = new Pen(Color.Black, 1.5f))
            {
                g.DrawPolygon(pen, points);
            }
        }

        public static void DrawSmileIcon(Graphics g, int x, int y)
        {
            using (Pen pen = new Pen(Color.White, 4f))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                // วงกลมหน้า
                g.DrawEllipse(pen, x, y, 50, 50);

                // ตาซ้าย
                g.FillEllipse(brush, x + 12, y + 15, 6, 6);

                // ตาขวา
                g.FillEllipse(brush, x + 32, y + 15, 6, 6);

                // ปากยิ้ม
                g.DrawArc(pen, x + 12, y + 18, 26, 20, 20, 140);
            }
        }

        public static void DrawSadIcon(Graphics g, int x, int y)
        {
            using (Pen pen = new Pen(Color.White, 4f))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                // วงกลมหน้า
                g.DrawEllipse(pen, x, y, 50, 50);

                // ตาซ้าย
                g.FillEllipse(brush, x + 12, y + 15, 6, 6);

                // ตาขวา
                g.FillEllipse(brush, x + 32, y + 15, 6, 6);

                // ปากเศร้า (คว่ำลง)
                g.DrawArc(pen, x + 12, y + 26, 26, 20, 200, 140);
            }
        }

        public static void DrawDiscountIcon(Graphics g, int x, int y, Color bgColor, Color fgColor)
        {
            // 1. วาดรูปดาว 10 แฉก (รอยหยักพื้นหลังสีดำ)
            int cx = x + 25; // จุดกึ่งกลางแกน X
            int cy = y + 25; // จุดกึ่งกลางแกน Y
            int outerRadius = 25; // รัศมีวงนอก (แฉกแหลม)
            int innerRadius = 19; // รัศมีวงใน (ร่องหยัก)
            int pointsCount = 10; // จำนวนแฉก

            PointF[] pts = new PointF[pointsCount * 2];
            for (int i = 0; i < pointsCount * 2; i++)
            {
                // สลับรัศมีวงนอกและวงในเพื่อสร้างรอยหยัก
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;

                // เริ่มวาดจากจุดบนสุด (-PI / 2)
                double angle = (i * Math.PI / pointsCount) - (Math.PI / 2);

                // คำนวณพิกัด x, y ด้วยตรีโกณมิติ
                pts[i] = new PointF(
                    (float)(cx + radius * Math.Cos(angle)),
                    (float)(cy + radius * Math.Sin(angle))
                );
            }

            // ถมสีดำลงในรูปทรงที่คำนวณไว้
            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                g.FillPolygon(bgBrush, pts);
            }

            // เปลี่ยนจากการล็อก Pen(Color.White, 3.5f) เป็นรับค่าตัวแปร
            using (Pen fgPen = new Pen(fgColor, 3.5f))
            {
                fgPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                fgPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                g.DrawLine(fgPen, x + 16, y + 34, x + 34, y + 16);
                g.DrawEllipse(fgPen, x + 13, y + 13, 7, 7);
                g.DrawEllipse(fgPen, x + 30, y + 30, 7, 7);
            }
        }

        public static void DrawTicketIcon(Graphics g, Rectangle bounds, Color bgColor, Color fgColor)
        {
            // เปิดโหมดวาดขอบเรียบเนียน (Anti-Alias)
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // กำหนดขนาดรัศมีต่างๆ
            float cornerRadius = bounds.Height * 0.15f;
            float notchRadius = bounds.Height * 0.15f;

            float d = cornerRadius * 2;
            float notchD = notchRadius * 2;

            // จุดกึ่งกลางแกน Y
            float midY = bounds.Y + (bounds.Height / 2f);

            using (GraphicsPath path = new GraphicsPath())
            {
                // 1. มุมซ้ายบน
                path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);

                // 2. มุมขวาบน
                path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);

                // 3. รอยแหว่งด้านขวา
                path.AddArc(bounds.Right - notchRadius, midY - notchRadius, notchD, notchD, 270, -180);

                // 4. มุมขวาล่าง
                path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);

                // 5. มุมซ้ายล่าง
                path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);

                // 6. รอยแหว่งด้านซ้าย
                path.AddArc(bounds.X - notchRadius, midY - notchRadius, notchD, notchD, 90, -180);

                path.CloseFigure();

                // 7. ถมสีพื้นตั๋วตามโหมดที่เลือก (bgColor)
                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    g.FillPath(bgBrush, path);
                }

                // 8. วาดเส้นขอบและรอยปรุเพื่อให้ดูเป็นตั๋วชัดเจนขึ้น (fgColor)
                using (Pen fgPen = new Pen(fgColor, 2.5f))
                {
                    // วาดเส้นขอบรอบตั๋ว
                    g.DrawPath(fgPen, path);

                    // เปลี่ยนหัวปากกาเป็นเส้นประ (Dash) สำหรับรอยปรุฉีกตั๋ว
                    fgPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    // คำนวณแกน X ตรงกลางตั๋ว
                    float midX = bounds.X + (bounds.Width / 2f);

                    // วาดเส้นประจากบนลงล่าง (เว้นขอบบนและล่างเล็กน้อยไม่ให้ชนเส้นขอบ)
                    g.DrawLine(fgPen, midX, bounds.Y + 4, midX, bounds.Bottom - 4);
                }
            }
        }

        public static void DrawReviewIcon(Graphics g, int x, int y, Color bgColor, Color fgColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush fgBrush = new SolidBrush(fgColor))
            {
                // ตำแหน่งกึ่งกลางแกน Y ของทั้ง 3 แถว (เพื่อให้สมดุลในกรอบ 50x50)
                float[] yCenters = { y + 11, y + 25, y + 39 };

                float starCx = x + 14;     // จุดกึ่งกลางแกน X ของดาว
                float outerRadius = 7.5f;  // รัศมีแฉกด้านนอกของดาว
                float innerRadius = 3.2f;  // รัศมีมุมเว้าด้านในของดาว

                for (int i = 0; i < 3; i++)
                {
                    float cy = yCenters[i];

                    // 1. วาดดาว 5 แฉก
                    PointF[] starPts = new PointF[10];
                    double angle = -Math.PI / 2; // เริ่มวาดจากจุดบนสุดของดาว
                    double step = Math.PI / 5;   // ระยะห่างของแต่ละมุม (36 องศา)

                    for (int j = 0; j < 10; j++)
                    {
                        // สลับรัศมีวงนอกและวงในเพื่อสร้างแฉกดาว
                        float r = (j % 2 == 0) ? outerRadius : innerRadius;
                        starPts[j] = new PointF(
                            (float)(starCx + r * Math.Cos(angle)),
                            (float)(cy + r * Math.Sin(angle))
                        );
                        angle += step;
                    }
                    g.FillPolygon(fgBrush, starPts);

                    // 2. วาดเส้นขีดแนวนอน (สี่เหลี่ยมผืนผ้า)
                    // เริ่มแกน X ที่ 27, ถอยแกน Y ขึ้นไปครึ่งนึงของความหนาเส้น (3) เพื่อให้อยู่กึ่งกลางดาวพอดี
                    // กว้าง 18, หนา 6
                    g.FillRectangle(fgBrush, x + 27, cy - 3, 18, 6);
                }
            }
        }
    }
}
