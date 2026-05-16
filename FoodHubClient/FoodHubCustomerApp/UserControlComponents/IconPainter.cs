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

        public static void DrawBackIcon(Graphics g, Rectangle bounds, Color outlineColor, float strokeWidth, bool isHovered)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float offset = strokeWidth / 2f;
            RectangleF rectOuter = new RectangleF(
                bounds.X + offset,
                bounds.Y + offset,
                bounds.Width - (offset * 2) - 1,
                bounds.Height - (offset * 2) - 1
            );

            Color circleColor = outlineColor;
            Color arrowColor = isHovered ? Color.White : outlineColor;

            if (isHovered)
            {
                using (SolidBrush brushFill = new SolidBrush(circleColor))
                {
                    g.FillEllipse(brushFill, rectOuter);
                }
            }
            else
            {
                using (Pen penCircle = new Pen(circleColor, strokeWidth))
                {
                    g.DrawEllipse(penCircle, rectOuter);
                }
            }

            float cx = rectOuter.X + rectOuter.Width / 2f;
            float cy = rectOuter.Y + rectOuter.Height / 2f;

            float hSpan = rectOuter.Width * 0.08f;  
            float vSpan = rectOuter.Height * 0.18f; 

            PointF[] arrowPoints = new PointF[]
            {
            new PointF(cx + hSpan, cy - vSpan), 
            new PointF(cx - hSpan, cy),       
            new PointF(cx + hSpan, cy + vSpan)  
            };

            using (Pen penArrow = new Pen(arrowColor, strokeWidth))
            {
                penArrow.LineJoin = LineJoin.Round;
                penArrow.StartCap = LineCap.Round;
                penArrow.EndCap = LineCap.Round;

                g.DrawLines(penArrow, arrowPoints);
            }
        }

        public static void DrawPenIcon(Graphics g, Rectangle bounds)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsState state = g.Save();

            // 1. หาจุดกึ่งกลางของกล่องที่จะวาด
            float cx = bounds.X + bounds.Width / 2f;
            float cy = bounds.Y + bounds.Height / 2f;

            // 2. ย้ายจุดอ้างอิงไปตรงกลาง แล้วหมุนแกนวาดไป 45 องศาตามเข็มนาฬิกา
            // ทริค: พอเราวาดปากกาในแนวตั้งชี้ลงพื้น มันจะเอียงไปชี้ที่มุมซ้ายล่างพอดี!
            g.TranslateTransform(cx, cy);
            g.RotateTransform(45);

            // 3. กำหนดสัดส่วนของปากกา (อิงตามขนาดพื้นที่ bounds)
            float w = bounds.Width * 0.28f;  // ความกว้างของตัวด้าม
            float halfW = w / 2f;
            float h = bounds.Height * 0.65f; // ความยาวรวมของปากกา
            float halfH = h / 2f;

            using (SolidBrush brushWhite = new SolidBrush(Color.White))
            {
                // ส่วนที่ 1: หัวด้านบนสุด (ทำขอบบนให้โค้งมนแบบในรูปต้นฉบับ)
                float capHeight = h * 0.2f;
                g.FillEllipse(brushWhite, -halfW, -halfH, w, w); // ขอบโค้งมน
                g.FillRectangle(brushWhite, -halfW, -halfH + (w / 2f), w, capHeight - (w / 2f)); // เติมเนื้อให้เต็ม

                // ส่วนที่ 2: ด้ามปากกาตรงกลาง
                float gap = h * 0.05f; // เส้นร่องว่างๆ ระหว่างชิ้นส่วน
                float bodyY = -halfH + capHeight + gap;
                float bodyHeight = h * 0.45f;
                g.FillRectangle(brushWhite, -halfW, bodyY, w, bodyHeight);

                // ส่วนที่ 3: ปลายปากกา (รูปสามเหลี่ยม)
                float tipY = bodyY + bodyHeight + gap;
                PointF[] tipPoints = new PointF[]
                {
                    new PointF(-halfW, tipY), // มุมซ้ายบนของสามเหลี่ยม
                    new PointF(halfW, tipY),  // มุมขวาบนของสามเหลี่ยม
                    new PointF(0, halfH)      // ปลายแหลมสุดชี้ลงล่าง
                };
                g.FillPolygon(brushWhite, tipPoints);
            }

            g.Restore(state);
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
    }
}
