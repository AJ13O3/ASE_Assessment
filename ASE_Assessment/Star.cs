// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 01-14-2024
//
// Last Modified By : amanj
// Last Modified On : 01-14-2024
// ***********************************************************************
// <copyright file="Star.cs" company="ASE_Assessment">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ASE_Assessment
{
    /// <summary>
    /// Class Star.
    /// </summary>
    public class Star
    {
        /// <summary>
        /// The graphics
        /// </summary>
        private Graphics graphics;
        /// <summary>
        /// The pen colour
        /// </summary>
        private Color penColour;
        /// <summary>
        /// The fill status
        /// </summary>
        private bool fillStatus;
        /// <summary>
        /// The current x location
        /// </summary>
        private int currentXLocation;
        /// <summary>
        /// The current y location
        /// </summary>
        private int currentYLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Star"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="penColour">The pen colour.</param>
        /// <param name="fillStatus">if set to <c>true</c> [fill status].</param>
        /// <param name="xLocation">The x location.</param>
        /// <param name="yLocation">The y location.</param>
        public Star(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        /// <summary>
        /// Draws the star based on the calculated points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="sizeMultiplier">The size multiplier.</param>
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

        /// <summary>
        /// Calculates the points of the star.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="sizeMultiplier">The size multiplier.</param>
        /// <param name="centerX">The center x.</param>
        /// <param name="centerY">The center y.</param>
        /// <returns>PointF[].</returns>
        private PointF[] CalculateStarPoints(int points, int sizeMultiplier, int centerX, int centerY)
        {
            PointF[] starPoints = new PointF[points * 2];
            double angleBetweenPoints = Math.PI / points;
            float outerRadius = 10 * sizeMultiplier; // Outer radius of the star
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