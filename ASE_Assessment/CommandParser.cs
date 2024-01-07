using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private bool ifBlock = false;
        private bool executeCommand = true;
        private bool loopBlock = false;
        private int loopCount = 0;
        private List<string> loopCommands = new List<string>();
        private UserMethod currentMethod;
        private Dictionary<string, UserMethod> userMethods = new Dictionary<string, UserMethod>();

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

            if (entry.StartsWith("method"))
            {
                // Start of a method definition
                defineMethod(entry);
            }
            else if (entry.StartsWith("endmethod"))
            {
                // End of a method definition
                currentMethod = null; // Assuming currentMethod is a field that tracks method being defined
            }
            else if (currentMethod != null)
            {
                // Add commands to the current method definition
                currentMethod.Commands.Add(entry);
            }
            else if (userMethods.ContainsKey(entry.Split('(')[0]))
            {
                callMethod(entry);
            }
            else if (entry.StartsWith("if"))
            {
                ifBlock = true;
                executeCommand = evaluateCondition(entry.Substring(3));
            }

            else if (entry == "endif")
            {
                ifBlock = false;
                executeCommand = true;
            }

            else if (ifBlock && !executeCommand)
            {
                return; // Skip command if inside if block and condition is false
            }

            else if (entry.StartsWith("loop"))
            {
                loopBlock = true;
                int.TryParse(entry.Substring(5), out loopCount);

                loopCommands.Clear();
            }

            else if (entry == "endloop")
            {
                loopBlock = false;
                for (int i = 0; i < loopCount; i++)
                {
                    foreach (string command in loopCommands)
                    {
                        processCommand(command);
                    }
                }
                loopCommands.Clear();
            }

            else if (loopBlock)
            {
                loopCommands.Add(entry);
            }

            else if (entry.StartsWith("rectangle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 3)
                {
                    drawRectangle(parts[1], parts[2]);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for rectangle command.");
                }
            }

            else if (entry.StartsWith("circle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 2)
                {
                    drawCircle(parts[1]);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for circle command.");
                }
            }

            else if (entry.StartsWith("triangle"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 3)
                {
                    drawTriangle(parts[1], parts[2]);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for triangle command.");
                }
            }

            else if (entry.StartsWith("move"))
            {
                string[] parts = entry.Trim().Split(' ');

                if (parts.Length == 3)
                {
                    moveTo(parts[1], parts[2]);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for moveTo command.");
                }
            }

            else if (entry.StartsWith("drawto"))
            {
                string[] parts = entry.Split(' ');
                if (parts.Length == 3)
                {
                    drawTo(parts[1], parts[2]);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for drawTo command.");
                }
            }

            else if (entry == "reset")
            {
                reset();
            }

            else if (entry == "red pen")
            {
                penColour(Color.Red);
            }

            else if (entry == "blue pen")
            {
                penColour(Color.Blue);
            }

            else if (entry == "green pen")
            {
                penColour(Color.Green);
            }

            else if (entry == "fill on")
            {
                fillOn();
            }

            else if (entry == "fill off")
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
                string varName = parts[0];

                if (parts.Length == 3) 
                {    
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

                else if (parts.Length > 3)
                {
                    string val1 = parts[2];
                    int int1 = 0;
                    string op = parts[3];
                    string val2 = parts[4];
                    int int2 = 0;

                    if (variables.ContainsKey(val1))
                    {
                        int1 = variables[val1];
                    }
                    else
                    {
                        int.TryParse(val1, out int1) ;
                    }

                    if (variables.ContainsKey(val2))
                    {
                        int2 = variables[val2];
                    }
                    else
                    {
                        int.TryParse(val2, out int2);
                    }

                    if (op == "+")
                    {
                        variables[varName] = int1 + int2;
                    }
                    else if (op == "-")
                    {
                        variables[varName] = int1 - int2;
                    }
                    else if(op == "*")
                    {
                        variables[varName] = int1 * int2;
                    }
                    else if (op == "/")
                    {
                        variables[varName] = int1 / int2;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for expression");
                    }
                }

                else
                {                    
                   throw new ArgumentException("Invalid parameters for expression");
                }
            }
            else if (entry.Length == 0)
            {
                return;
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
        private void moveTo(string x, string y)
        {
            int xVal, yVal;

            if (variables.ContainsKey(x))
            {
                xVal = variables[x];
            }
            else if (!int.TryParse(x, out xVal))
            {
                throw new ArgumentException("Invalid x parameter for moveTo command.");
            }

            if (variables.ContainsKey(y))
            {
                yVal = variables[y];
            }
            else if (!int.TryParse(y, out yVal))
            {
                throw new ArgumentException("Invalid y parameter for moveTo command.");
            }

            currentXLocation = xVal;
            currentYLocation = yVal;
        }

        /// <summary>Draws to a point defined by the x and y integers which represent x and y co-ordinates.</summary>
        /// <param name="x">The x position of the pen.</param>
        /// <param name="y">The y position of the pen.</param>
        private void drawTo(string x, string y)
        {
            int xVal, yVal;

            if (variables.ContainsKey(x))
            {
                xVal = variables[x];
            }
            else if (!int.TryParse(x, out xVal))
            {
                throw new ArgumentException("Invalid x parameter for drawTo command.");
            }

            if (variables.ContainsKey(y))
            {
                yVal = variables[y];
            }
            else if (!int.TryParse(y, out yVal))
            {
                throw new ArgumentException("Invalid y parameter for drawTo command.");
            }

            Pen pen = new Pen(currentPenColour);
            g.DrawLine(pen, currentXLocation, currentYLocation, xVal, yVal);

            currentXLocation = xVal;
            currentYLocation = yVal;
        }

        /// <summary>Resets the pen to it's default position.</summary>
        private void reset()
        {
            moveTo("10","10");
            fillStatus = false;
        }

        /// <summary>Draws a rectangle.</summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        private void drawRectangle(string width, string height)
        {
            int widthVal, heightVal;

            // Check if widthParameter is a variable and get its value
            if (variables.ContainsKey(width))
            {
                widthVal = variables[width];
            }
            else if (!int.TryParse(width, out widthVal))
            {
                throw new ArgumentException("Invalid width parameter for rectangle command.");
            }

            // Check if heightParameter is a variable and get its value
            if (variables.ContainsKey(height))
            {
                heightVal = variables[height];
            }
            else if (!int.TryParse(height, out heightVal))
            {
                throw new ArgumentException("Invalid height parameter for rectangle command.");
            }

            if (!fillStatus)
            {
                Pen pen = new Pen(currentPenColour);
                g.DrawRectangle(pen, currentXLocation, currentYLocation, widthVal, heightVal);
            }
            else
            {
                Brush brush = new SolidBrush(currentPenColour);
                g.FillRectangle(brush, currentXLocation, currentYLocation, widthVal, heightVal);
            }
        }

        /// <summary>Draws a circle.</summary>
        /// <param name="radius">The radius of the circle.</param>
        private void drawCircle(string radius)
        {
            int radiusVal;

            if (variables.ContainsKey(radius))
            {
                radiusVal = variables[radius];
            }
            else if (!int.TryParse (radius, out radiusVal))
            {
                throw new ArgumentException("Invalid height parameter for rectangle command.");
            }
            if (!fillStatus)
            {
                Pen pen = new Pen(currentPenColour);
                g.DrawEllipse(pen, currentXLocation, currentYLocation, radiusVal * 2, radiusVal * 2);
            }
            else
            {
                Brush brush = new SolidBrush(currentPenColour);
                g.FillEllipse(brush, currentXLocation, currentYLocation, radiusVal * 2, radiusVal * 2);
            }

        }

        /// <summary>Draws a triangle.</summary>
        /// <param name="baseLength">Length of the base.</param>
        /// <param name="height">The height of the triangle.</param>
        private void drawTriangle(string baseLength, string height)
        {
            int baseLengthVal, heightVal;
            if (variables.ContainsKey(baseLength))
            {
                baseLengthVal = variables[baseLength];
            }
            else if (!int.TryParse(baseLength, out baseLengthVal))
            {
                
            }
            if (variables.ContainsKey(height))
            {
                heightVal = variables[height];
            }
            else if (!int.TryParse(height, out heightVal))
            {

            }
            Point p1 = new Point(currentXLocation, currentYLocation);
            Point p2 = new Point(currentXLocation + baseLengthVal, currentYLocation);
            Point p3 = new Point(currentXLocation + (baseLengthVal / 2), currentYLocation - heightVal);

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
        private bool evaluateCondition(string condition)
        {
            // Evaluate the condition expression (eg count > size)
            string[] parts = condition.Split(" ");

            if (parts.Length == 3)
            {
                string val1 = parts[0];
                int int1 = 0;
                string op = parts[1];
                string val2 = parts[2];
                int int2 = 0;

                if (variables.ContainsKey(val1))
                {
                    int1 = variables[val1];
                }
                else
                {
                    int.TryParse(val1, out int1);
                }

                if (variables.ContainsKey(val2))
                {
                    int2 = variables[val2];
                }
                else
                {
                    int.TryParse(val2, out int2);
                }

                if (op == ">")
                {
                    return int1 > int2;
                }
                else if (op == "<")
                {
                    return int1 < int2;
                }
                else if (op == "<=")
                {
                    return int1 <= int2;
                }
                else if (op == "=>")
                {
                    return int1 >= int2;
                }
                else if (op == "==")
                {
                    return int1 == int2;
                }
                else if (op == "!=")
                {
                    return int1 != int2;
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for if condition");

                }

            }
            else
            {
                throw new ArgumentException("Invalid parameters for if condition");
            }
        }

        private void defineMethod(string definition)
        {
            var parts = definition.Split(" ");
            var methodName = parts[1]; // Get the method name                                       
            var parametersPart = definition.Substring(definition.IndexOf('(') + 1);// Extracting the parameter list
            parametersPart = parametersPart.Substring(0, parametersPart.IndexOf(')')).Trim();
            var parameters = parametersPart.Split(',').Select(p => p.Trim()).ToList();

            if (userMethods.ContainsKey(methodName))
            {
               throw new ArgumentException($"A method with the name '{methodName}' already exists.");
            }
            currentMethod = new UserMethod
            {
                Parameters = parameters
            };

            userMethods.Add(methodName, currentMethod);// Store the method definition

        }
        private void callMethod(string invocation)
        {
            // Split the invocation into the method name and arguments
            var parts = invocation.Split(" ");
            var methodName = parts[0].Trim();
            var arguments = parts.Length > 1 ? parts[1].Split(',') : new string[0];

            // Check if the method exists
            if (userMethods.TryGetValue(methodName, out var method))
            {
                // Replace parameters with actual arguments and execute the method
                foreach (var command in method.Commands)
                {
                    var processedCommand = command;
                    for (int i = 0; i < method.Parameters.Count; i++)
                    {
                        processedCommand = processedCommand.Replace(method.Parameters[i], arguments.Length > i ? arguments[i].Trim() : "");
                    }
                    processCommand(processedCommand); // Execute the command
                }
            }
            else
            {
                // Handle the case when the method is not found
                throw new ArgumentException($"Method '{methodName}' not defined.");
            }
        }

    }
}


//TO DO 
// Amend shapes for variables, fix and finish loops, input validation, rest of assignment