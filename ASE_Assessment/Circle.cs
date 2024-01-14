// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 01-12-2024
//
// Last Modified By : amanj
// Last Modified On : 01-12-2024
// ***********************************************************************
// <copyright file="Circle.cs" company="ASE_Assessment">
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
    /// Class Circle.
    /// </summary>
    public class Circle
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
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="penColour">The pen colour.</param>
        /// <param name="fillStatus">if set to <c>true</c> [fill status].</param>
        /// <param name="xLocation">The x location.</param>
        /// <param name="yLocation">The y location.</param>
        public Circle(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        /// <summary>
        /// Draws circle with the specified radius.
        /// </summary>
        /// <param name="radius">The radius.</param>
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
