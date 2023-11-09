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
        private Graphics drawingGraphics;
        private Color currentPenColour = Color.Black;
        private int currentXLocation = 10;
        private int currentYLocation = 10;

        public CommandParser(Graphics graphics)
        {
            drawingGraphics = graphics;
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
            else if (entry.Contains("moveto"))
            {
                string[] parts = entry.Split(' ');
                if (parts.Length == 3 && int.TryParse(parts[1], out int xLoc) && int.TryParse(parts[2], out int yLoc))
                {
                    moveTo(xLoc,yLoc);
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

            else if (entry == "clear")
            {

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
            drawingGraphics.DrawLine(pen, currentXLocation, currentYLocation, x, y);
        }


        private void drawRectangle(int width, int height)
        {
            Pen pen = new Pen(currentPenColour);
            drawingGraphics.DrawRectangle(pen, currentXLocation, currentYLocation, width, height);
        }

    }
}
