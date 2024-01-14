// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 01-12-2024
//
// Last Modified By : amanj
// Last Modified On : 01-12-2024
// ***********************************************************************
// <copyright file="Triangle.cs" company="ASE_Assessment">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    /// <summary>
    /// Class Triangle.
    /// </summary>
    public class Triangle
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
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="penColour">The pen colour.</param>
        /// <param name="fillStatus">if set to <c>true</c> [fill status].</param>
        /// <param name="xLocation">The x location.</param>
        /// <param name="yLocation">The y location.</param>
        public Triangle(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        /// <summary>
        /// Draws the triangle with specified base length.
        /// </summary>
        /// <param name="baseLength">Length of the base.</param>
        /// <param name="height">The height.</param>
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
