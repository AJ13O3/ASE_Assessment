using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class Rectangle
    {
        private Graphics graphics;
        private Color penColour;
        private bool fillStatus;
        private int currentXLocation;
        private int currentYLocation;

        public Rectangle(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        public void Draw(int width, int height)
        {
            if (!fillStatus)
            {
                using (Pen pen = new Pen(penColour))
                {
                    graphics.DrawRectangle(pen, currentXLocation, currentYLocation, width, height);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(penColour))
                {
                    graphics.FillRectangle(brush, currentXLocation, currentYLocation, width, height);
                }
            }
        }
    }
}
