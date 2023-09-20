using System.Drawing;
using System.Drawing.Drawing2D;

namespace math_application
{
    public class charting
    {

        public static Bitmap DrawLinesOnPictureBox(List<PointF> points, int width, int height, float scale, PointF displacement = new PointF())
        {
            int bitmapWidth = (int)(width * scale );
            int bitmapHeight = (int)(height * scale);
            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(Color.Blue))
                {
                    pen.Width = 1;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Draw x-axis line
                    pen.Color = Color.Gray;
                    PointF startAxisX = new PointF(0, displacement.Y);
                    PointF endAxisX = new PointF(bitmapWidth, displacement.Y);
                    graphics.DrawLine(pen, startAxisX, endAxisX);

                    // Draw y-axis line
                    PointF startAxisY = new PointF(displacement.X, 0);
                    PointF endAxisY = new PointF(displacement.X, bitmapHeight);
                    graphics.DrawLine(pen, startAxisY, endAxisY);

                    // Draw grid lines
                    int gridSize = 20; // Adjust the grid size as desired
                    pen.Color = Color.LightGray;

                    // Vertical grid lines
                    for (float x = gridSize; x < bitmapWidth; x += gridSize)
                    {
                        PointF startGridLine = new PointF(x + displacement.X, 0);
                        PointF endGridLine = new PointF(x + displacement.X, bitmapHeight);
                        graphics.DrawLine(pen, startGridLine, endGridLine);
                    }

                    // Horizontal grid lines
                    for (float y = gridSize; y < bitmapHeight; y += gridSize)
                    {
                        PointF startGridLine = new PointF(0, y + displacement.Y);
                        PointF endGridLine = new PointF(bitmapWidth, y + displacement.Y);
                        graphics.DrawLine(pen, startGridLine, endGridLine);
                    }

                    pen.Color = Color.Blue;
                    pen.Width = 2;


                    PointF[] pointFs= new PointF[points.Count];
                    for (int i = 0; i < pointFs.Length; i++)
                    {
                        pointFs[i] = displacement;

                    }


                    graphics.DrawCurve(pen, AddPointArrays(ScalePoints(points.ToArray(), scale), pointFs));


                }
            }

            return bitmap;
        }
        public static PointF[] AddPointArrays(PointF[] array1, PointF[] array2)
        {
            if (array1.Length != array2.Length)
                throw new ArgumentException("Arrays must have the same length.");

            PointF[] sumArray = new PointF[array1.Length];

            for (int i = 0; i < array1.Length; i++)
            {
                PointF p1 = array1[i];
                PointF p2 = array2[i];
                float sumX = p1.X + p2.X;
                float sumY = p1.Y + p2.Y;
                sumArray[i] = new PointF(sumX, sumY);
            }

            return sumArray;
        }
        public static PointF AddPoints(PointF p1, PointF p2)
        {
            float sumX = p1.X + p2.X;
            float sumY = p1.Y + p2.Y;
            return new PointF(sumX, sumY);
        }
        public static PointF[] ScalePoints(PointF[] points, float scale)
        {
            PointF[] scaledPoints = new PointF[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                float scaledX = points[i].X * scale;
                float scaledY = points[i].Y * scale;
                scaledPoints[i] = new PointF(scaledX, scaledY);
            }

            return scaledPoints;
        }

        public static PointF ScalePoint(PointF point, float scale)
        {
            float scaledX = point.X * scale;
            float scaledY = point.Y * scale;
            return new PointF(scaledX, scaledY);
        }
    }
}