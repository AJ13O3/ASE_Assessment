using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class CommandParser
    {
        private Graphics g;
        public Color currentPenColour = Color.Black;
        public int currentXLocation = 10;
        public int currentYLocation = 10;
        public bool fillStatus = false;
        private PictureBox drawBox;
        private TextBox programBox;
        private Dictionary<string, int> variables = new Dictionary<string, int>();

        public CommandParser(PictureBox pictureBox, TextBox programBox)
        {
            drawBox = pictureBox;
            g = drawBox.CreateGraphics();
            this.programBox = programBox;
        }

        public void processCommand(string entry)
        {
            entry = entry.ToLower();
            entry = entry.Trim();

            if (entry.Contains("rectangle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 3 && int.TryParse(parts[1], out int width) && int.TryParse(parts[2], out int height))
                {
                    if (width>0 && height > 0)
                    {
                        drawRectangle(width, height);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for rectangle command.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for rectangle command.");
                }
            }

            else if (entry.Contains("circle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 2 && int.TryParse(parts[1], out int radius))
                {
                    if (radius > 0)
                    {
                        drawCircle(radius);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for circle command.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for circle command.");
                }
            }

            else if (entry.Contains("triangle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 3 && int.TryParse(parts[1], out int baseLength) && int.TryParse(parts[2], out int height))
                {

                    if (baseLength > 0 && height > 0)
                    {
                        drawTriangle(baseLength, height);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for triangle command.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for triangle command.");
                }
            }

            else if (entry.Contains("move"))
            {
                string[] parts = entry.Split(' ');
                if (parts.Length == 3 && int.TryParse(parts[1], out int xLoc) && int.TryParse(parts[2], out int yLoc))
                {
                    if (xLoc > 0 && yLoc > 0)
                    {
                        moveTo(xLoc, yLoc);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for moveTo command.");
                    }
                    
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for moveTo command.");
                }
            }


            else if (entry.Contains("drawto"))
            {
                string[] parts = entry.Split(' ');
                if (parts.Length == 3 && int.TryParse(parts[1], out int xLoc) && int.TryParse(parts[2], out int yLoc))
                {
                    if (xLoc > 0 && yLoc > 0)
                    {
                        drawTo(xLoc, yLoc);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for drawTo command.");
                    }

                    currentXLocation = xLoc;
                    currentYLocation = yLoc;
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for drawTo command.");
                }
            }

            else if (entry.Contains("reset"))
            {
                reset();
            }

            else if (entry.Contains("red pen"))
            {
                penColour(Color.Red);
            }

            else if (entry.Contains("blue pen"))
            {
                penColour(Color.Blue);
            }

            else if (entry.Contains("green pen"))
            {
                penColour(Color.Green);
            }

            else if (entry.Contains("fill on"))
            {
                fillOn();
            }

            else if (entry.Contains("fill off"))
            {
                fillOff();
            }

            else if (entry.Contains("run"))
            {
                foreach (string n in programBox.Lines)
                {
                    processCommand(n);
                }


            }

            else if (entry.Contains("="))
            {
                string[] parts = entry.Split(' ');
                if (parts.Length == 3) 
                {    
                    string varName = parts[0];
                    int.TryParse(parts[2], out int varValue);
                    if (variables.ContainsKey(varName))
                    {
                        variables[varName] = varValue;
                    }
                    else
                    {
                        variables.Add(varName, varValue);
                    }
                    
                    
                }
            }
            else if(entry.Contains("print dict"))
            {
                foreach (KeyValuePair<string, int> kvp in variables)
                {  
                    Debug.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }

            else
            {
                throw new InvalidOperationException("Not a valid command");
            }
        }


        /// <summary>Changes the colour of the pen.</summary>
        /// <param name="colour">The colour.</param>
        private void penColour(Color colour)
        {
            currentPenColour = colour;
        }

        /// <summary>Moves to a point defined by the x and y integers which represent x and y co-ordinates.</summary>
        /// <param name="x">The x position of the pen.</param>
        /// <param name="y">The y position of the pen.</param>
        private void moveTo(int x, int y)
        {
            currentXLocation = x;
            currentYLocation = y;
        }

        /// <summary>Draws to a point defined by the x and y integers which represent x and y co-ordinates.</summary>
        /// <param name="x">The x position of the pen.</param>
        /// <param name="y">The y position of the pen.</param>
        private void drawTo(int x, int y)
        {
            Pen pen = new Pen(currentPenColour);
            g.DrawLine(pen, currentXLocation, currentYLocation, x, y);
        }

        /// <summary>Resets the pen to it's default position.</summary>
        private void reset()
        {
            moveTo(10, 10);
            fillStatus = false;
        }

        /// <summary>Draws a rectangle.</summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        private void drawRectangle(int width, int height)
        {

            if (!fillStatus)
            {
                Pen pen = new Pen(currentPenColour);
                g.DrawRectangle(pen, currentXLocation, currentYLocation, width, height);
            }
            else
            {
                Brush brush = new SolidBrush(currentPenColour);
                g.FillRectangle(brush, currentXLocation, currentYLocation, width, height);
            }
        }

        /// <summary>Draws a circle.</summary>
        /// <param name="radius">The radius of the circle.</param>
        private void drawCircle(int radius)
        {
            if (!fillStatus)
            {
                Pen pen = new Pen(currentPenColour);
                g.DrawEllipse(pen, currentXLocation, currentYLocation, radius * 2, radius * 2);
            }
            else
            {
                Brush brush = new SolidBrush(currentPenColour);
                g.FillEllipse(brush, currentXLocation, currentYLocation, radius * 2, radius * 2);
            }

        }

        /// <summary>Draws a triangle.</summary>
        /// <param name="baseLength">Length of the base.</param>
        /// <param name="height">The height of the triangle.</param>
        private void drawTriangle(int baseLength, int height)
        {
            Point p1 = new Point(currentXLocation, currentYLocation);
            Point p2 = new Point(currentXLocation + baseLength, currentYLocation);
            Point p3 = new Point(currentXLocation + (baseLength / 2), currentYLocation - height);

            Point[] shapePoints = { p1, p2, p3 };

            if (!fillStatus)
            {
                Pen pen = new Pen(currentPenColour);
                g.DrawPolygon(pen, shapePoints);
            }
            else
            {
                Brush brush = new SolidBrush(currentPenColour);
                g.FillPolygon(brush, shapePoints);
            }

        }

        /// <summary>Set the fill mode to on.</summary>
        private void fillOn()
        {
            fillStatus = true;
        }

        /// <summary>Set the fill mode to off.</summary>
        private void fillOff()
        {
            fillStatus = false;
        }

    }
}
