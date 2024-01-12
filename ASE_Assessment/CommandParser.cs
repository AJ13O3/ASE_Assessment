using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//TO DO 
// THREADS
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

        public void ProcessCommand(string entry)
        {
            entry = entry.ToLower();
            entry = entry.Trim();

            if (entry.StartsWith("method"))
            {
                // Start of a method definition
                DefineMethod(entry);
            }

            else if (entry.StartsWith("endmethod"))
            {
                // End of a method definition
                // Assuming currentMethod is a field that tracks method being defined
                currentMethod = null;

            }

            else if (currentMethod != null)
            {
                // Add commands to the current method definition
                currentMethod.Commands.Add(entry);
            }

            else if (entry.Contains("("))
            {
                int parenthesisIndex = entry.IndexOf('(');
                string methodName = entry.Substring(0, parenthesisIndex).Trim();

                if (userMethods.ContainsKey(methodName))
                {
                    CallMethod(entry);
                }
            }

            else if (entry.StartsWith("if"))
            {
                ifBlock = true;
                executeCommand = EvaluateCondition(entry.Substring(3));
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
                        ProcessCommand(command);
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
                    DrawRectangle(parts[1], parts[2]);
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
                    DrawCircle(parts[1]);
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
                    DrawTriangle(parts[1], parts[2]);
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
                    MoveTo(parts[1], parts[2]);
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
                    DrawTo(parts[1], parts[2]);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for drawTo command.");
                }
            }

            else if (entry == "reset")
            {
                Reset();
            }

            else if (entry == "red pen")
            {
                PenColour(Color.Red);
            }

            else if (entry == "blue pen")
            {
                PenColour(Color.Blue);
            }

            else if (entry == "green pen")
            {
                PenColour(Color.Green);
            }

            else if (entry == "fill on")
            {
                FillOn();
            }

            else if (entry == "fill off")
            {
                FillOff();
            }

            else if (entry.Contains("run"))
            {
                foreach (string n in programBox.Lines)
                {
                    ProcessCommand(n);
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

                    if (op == "+")
                    {
                        variables[varName] = int1 + int2;
                    }
                    else if (op == "-")
                    {
                        variables[varName] = int1 - int2;
                    }
                    else if (op == "*")
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
        private void PenColour(Color colour)
        {
            currentPenColour = colour;
        }

        /// <summary>Moves to a point defined by the x and y integers which represent x and y co-ordinates.</summary>
        /// <param name="x">The x position of the pen.</param>
        /// <param name="y">The y position of the pen.</param>
        private void MoveTo(string x, string y)
        {
            int xVal = GetParameterValue(x);
            int yVal = GetParameterValue(y);

            currentXLocation = xVal;
            currentYLocation = yVal;
        }

        /// <summary>Draws to a point defined by the x and y integers which represent x and y co-ordinates.</summary>
        /// <param name="x">The x position of the pen.</param>
        /// <param name="y">The y position of the pen.</param>
        private void DrawTo(string x, string y)
        {
           
            int xVal = GetParameterValue(x);
            int yVal = GetParameterValue(y);

            Pen pen = new Pen(currentPenColour);
            g.DrawLine(pen, currentXLocation, currentYLocation, xVal, yVal);

            currentXLocation = xVal;
            currentYLocation = yVal;
        }

        /// <summary>Resets the pen to it's default position.</summary>
        private void Reset()
        {
            MoveTo("10", "10");
            fillStatus = false;
        }

        /// <summary>Draws a rectangle.</summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        private void DrawRectangle(string width, string height)
        {
            int widthVal = GetParameterValue(width);
            int heightVal = GetParameterValue(height);

            var rectangle = new Rectangle(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            rectangle.Draw(widthVal, heightVal);
        }

        /// <summary>Draws a circle.</summary>
        /// <param name="radius">The radius of the circle.</param>
        private void DrawCircle(string radiusParameter)
        {
            int radius = GetParameterValue(radiusParameter);
            var circle = new Circle(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            circle.Draw(radius);
        }

        /// <summary>Draws a triangle.</summary>
        /// <param name="baseLength">Length of the base.</param>
        /// <param name="height">The height of the triangle.</param>
        private void DrawTriangle(string baseLengthParameter, string heightParameter)
        {
            int baseLength = GetParameterValue(baseLengthParameter);
            int height = GetParameterValue(heightParameter);
            var triangle = new Triangle(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            triangle.Draw(baseLength, height);
        }

        private int GetParameterValue(string parameter)
        {
            // Check if the parameter is a variable name in the dictionary
            if (variables.TryGetValue(parameter, out int value))
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Parameter value cannot be negative: {parameter}");
                }
                return value;
            }

            // If not a variable, try parsing the parameter as an integer
            if (int.TryParse(parameter, out int parsedValue))
            {
                if (parsedValue < 0)
                {
                    throw new ArgumentException($"Parameter value cannot be negative: {parameter}");
                }
                return parsedValue;
            }

            // If the parameter is neither a variable nor an integer, throw an exception
            throw new ArgumentException($"Invalid parameter: {parameter}");
        }

        /// <summary>Set the fill mode to on.</summary>
        private void FillOn()
        {
            fillStatus = true;
        }

        /// <summary>Set the fill mode to off.</summary>
        private void FillOff()
        {
            fillStatus = false;
        }

        private bool EvaluateCondition(string condition)
        {
            // Evaluate the condition expression (eg count > size)
            string[] parts = condition.Split(" ");

            if (parts.Length == 3)
            {
                string val1 = parts[0];
                string op = parts[1];
                string val2 = parts[2];
                int int1 = GetParameterValue(val1);
                int int2 = GetParameterValue(val2);
                
                switch (op) // found this in w3 schools
                {
                    case ">":
                        return int1 > int2;
                    case "<":
                        return int1 < int2;
                    case "<=":
                        return int1 <= int2;
                    case "=>":
                        return int1 >= int2;
                    case "==":
                        return int1 == int2;
                    case "!=":
                        return int1 != int2;
                    default:
                        throw new ArgumentException("Invalid operator in condition");
                }

            }
            else
            {
                throw new ArgumentException("Invalid parameters for if condition");
            }
        }

        private void DefineMethod(string definition)
        {
            var parts = definition.Split(" ");

            var methodName = parts[1]; // Get the method name                                       
            var parametersPart = definition.Substring(definition.IndexOf('(') + 1);// Extracting the parameter list
            parametersPart = parametersPart.Substring(0, parametersPart.IndexOf(')')).Trim();

            List<string> parameters;
            if (!string.IsNullOrEmpty(parametersPart))
            {
                parameters = parametersPart.Split(',').Select(p => p.Trim()).ToList();
            }
            else
            {
                parameters = new List<string>(); // No parameters
            }

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

        private void CallMethod(string call)
        {
            // Split the invocation into the method name and arguments

            var parts = call.Split(" ");

            var methodName = parts[0].Trim();

            var arguments = call.Substring(call.IndexOf('(') + 1);// Extracting the arguments list
            arguments = arguments.Substring(0, arguments.IndexOf(')')).Trim();

            var argVals = parts.Length > 1 ? arguments.Split(',') : new string[0];

            // Check if the method exists
            if (userMethods.TryGetValue(methodName, out var method))
            {
                // Replace parameters with actual arguments and execute the method
                foreach (var command in method.Commands)
                {
                    var processedCommand = command;
                    if (argVals.Length > 0)
                    {
                        for (int i = 0; i < method.Parameters.Count; i++)
                        {
                            processedCommand = processedCommand.Replace(method.Parameters[i], argVals.Length > i ? argVals[i].Trim() : "");
                        }
                        ProcessCommand(processedCommand); // Execute the command
                    }
                    else
                    {
                        ProcessCommand(processedCommand);
                    }

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


