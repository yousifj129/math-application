using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_application
{
    public class myChart : UserControl
    {
        public enum ChartType
        {
            Line,
            Bar
        };
        public List<PointF> DataPoints { get; set; }
        public ChartType ChartT { get; set; }
        public Color LineColor { get; set; }

        public PointF displacement = new PointF();
        // Constructor
        public myChart()
        {
            DataPoints = new List<PointF>();
            ChartT = ChartType.Line;
            LineColor = Color.Black;
            graphics = this.CreateGraphics();

        }
        public myChart(List<PointF> points)
        {
            DataPoints = points;
            ChartT = ChartType.Line;
            LineColor = Color.Black;
            graphics = this.CreateGraphics();
            graphics.ScaleTransform(Width, Height);

        }
        Graphics graphics;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

        }
        public void paint()
        {
            if (ChartT == ChartType.Line)
            {
                using (Pen pen = new Pen(LineColor))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Draw x-axis line
                    pen.Color = Color.Gray;
                    PointF startAxisX = new PointF(0, displacement.Y);
                    PointF endAxisX = new PointF(Width, displacement.Y);
                    graphics.DrawLine(pen, startAxisX, endAxisX);

                    // Draw y-axis line
                    PointF startAxisY = new PointF(displacement.X, 0);
                    PointF endAxisY = new PointF(displacement.X, Height);
                    graphics.DrawLine(pen, startAxisY, endAxisY);

                    // Draw grid lines
                    int gridSize = 10; // Adjust the grid size as desired
                    pen.Color = Color.LightGray;

                    // Vertical grid lines
                    for (float x = gridSize; x < Width; x += gridSize)
                    {
                        PointF startGridLine = new PointF(x + displacement.X, 0);
                        PointF endGridLine = new PointF(x + displacement.X, Height);
                        graphics.DrawLine(pen, startGridLine, endGridLine);
                    }

                    // Horizontal grid lines
                    for (float y = gridSize; y < Height; y += gridSize)
                    {
                        PointF startGridLine = new PointF(0, y + displacement.Y);
                        PointF endGridLine = new PointF(Width, y + displacement.Y);
                        graphics.DrawLine(pen, startGridLine, endGridLine);
                    }

                    graphics.DrawCurve(pen,DataPoints.ToArray());
                    
                }
            }
        }
    }
}
