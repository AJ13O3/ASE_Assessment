// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 01-12-2024
//
// Last Modified By : amanj
// Last Modified On : 01-12-2024
// ***********************************************************************
// <copyright file="Rectangle.cs" company="ASE_Assessment">
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
    /// Class Rectangle.
    /// </summary>
    public class Rectangle
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
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="penColour">The pen colour.</param>
        /// <param name="fillStatus">if set to <c>true</c> [fill status].</param>
        /// <param name="xLocation">The x location.</param>
        /// <param name="yLocation">The y location.</param>
        public Rectangle(Graphics graphics, Color penColour, bool fillStatus, int xLocation, int yLocation)
        {
            this.graphics = graphics;
            this.penColour = penColour;
            this.fillStatus = fillStatus;
            currentXLocation = xLocation;
            currentYLocation = yLocation;
        }

        /// <summary>
        /// Draws rectangle with the specified width and height.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
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
