using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomerApp.UserControlComponents
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
    }
}
