using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class Triangle
    {
        private Graphics graphics;
        private Color penColour;
        private bool fillStatus;
        private int currentXLocation;
        private int currentYLocation;

        public Triangle(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        public void Draw(int baseLength, int height)
        {
            Point p1 = new Point(currentXLocation, currentYLocation);
            Point p2 = new Point(currentXLocation + baseLength, currentYLocation);
            Point p3 = new Point(currentXLocation + baseLength / 2, currentYLocation - height);

            Point[] points = { p1, p2, p3 };

            if (!fillStatus)
            {
                using (Pen pen = new Pen(penColour))
                {
                    graphics.DrawPolygon(pen, points);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(penColour))
                {
                    graphics.FillPolygon(brush, points);
                }
            }
        }
    }
}
