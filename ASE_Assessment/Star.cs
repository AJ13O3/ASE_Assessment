namespace ASE_Assessment
{
    public class Star
    {
        private Graphics graphics;
        private Color penColour;
        private bool fillStatus;
        private int currentXLocation;
        private int currentYLocation;

        public Star(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        public void Draw(int points, int sizeMultiplier)
        {
            PointF[] starPoints = CalculateStarPoints(points, sizeMultiplier, currentXLocation, currentYLocation);
            if (!fillStatus)
            {
                using (Pen pen = new Pen(penColour))
                {
                    graphics.DrawPolygon(pen, starPoints);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(penColour))
                {
                    graphics.FillPolygon(brush, starPoints);
                }
            }
        }

        private PointF[] CalculateStarPoints(int points, int sizeMultiplier, int centerX, int centerY)
        {
            PointF[] starPoints = new PointF[points * 2];
            double angleBetweenPoints = Math.PI / points;
            float outerRadius = 100 * sizeMultiplier; // Outer radius of the star
            float innerRadius = outerRadius / 2;      // Inner radius of the star

            for (int i = 0; i < points * 2; i++)
            {
                float radius = i % 2 == 0 ? outerRadius : innerRadius;
                double angle = angleBetweenPoints * i;
                float x = (float)(centerX + Math.Cos(angle) * radius);
                float y = (float)(centerY + Math.Sin(angle) * radius);
                starPoints[i] = new PointF(x, y);
            }

            return starPoints;
        }
    }
}