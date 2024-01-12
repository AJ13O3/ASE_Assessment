using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class Circle
    {
        private Graphics graphics;
        private Color penColour;
        private bool fillStatus;
        private int currentXLocation;
        private int currentYLocation;

        public Circle(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        public void Draw(int radius)
        {
            if (!fillStatus)
            {
                using (Pen pen = new Pen(penColour))
                {
                    graphics.DrawEllipse(pen, currentXLocation - radius, currentYLocation - radius, radius * 2, radius * 2);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(penColour))
                {
                    graphics.FillEllipse(brush, currentXLocation - radius, currentYLocation - radius, radius * 2, radius * 2);
                }
            }
        }
    }
}
