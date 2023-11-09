using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assessment
{
    public class CommandParser
    {
        private Graphics g;
        private Color currentPenColour = Color.Black;
        private int currentXLocation = 10;
        private int currentYLocation = 10;
        private bool fillStatus = false;
        private PictureBox drawBox;
        private TextBox programBox;

        public CommandParser(PictureBox pictureBox, TextBox programBox)
        {
            drawBox = pictureBox;
            g = drawBox.CreateGraphics();
            this.programBox = programBox;
        }

        public void processCommand(string entry)
        {
            entry = entry.ToLower();

            if (entry.Contains("rectangle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 3 && int.TryParse(parts[1], out int width) && int.TryParse(parts[2], out int height))
                {
                    drawRectangle(width, height);
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
                    drawCircle(radius);
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
                    drawTriangle(baseLength, height);
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
                    moveTo(xLoc, yLoc);
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
                    drawTo(xLoc, yLoc);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for moveTo command.");
                }
            }

            else if (entry.Contains("reset"))
            {
                moveTo(10, 10);
                fillStatus = false;
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
                fillStatus = true;
            }

            else if (entry.Contains("fill off"))
            {
                fillStatus = false;
            }

            else if (entry.Contains("run"))
            {
                foreach (string n in programBox.Lines)
                {
                    processCommand(n);
                }


            }

            else
            {
                throw new InvalidOperationException("Not a valid command");
            }
        }

        private void penColour(Color colour)
        {
            currentPenColour = colour;
        }

        private void moveTo(int x, int y)
        {
            currentXLocation = x;
            currentYLocation = y;
        }

        private void drawTo(int x, int y)
        {
            Pen pen = new Pen(currentPenColour);
            g.DrawLine(pen, currentXLocation, currentYLocation, x, y);
        }

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

    }
}
