﻿// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 11-08-2023
//
// Last Modified By : amanj
// Last Modified On : 01-14-2024
// ***********************************************************************
// <copyright file="CommandParser.cs" company="ASE_Assessment">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//TO DO 
// THREADS
namespace ASE_Assessment
{
    /// <summary>
    /// Class CommandParser.
    /// </summary>
    public class CommandParser
    {
        /// <summary>
        /// Graphics 
        /// </summary>
        private Graphics g;
        /// <summary>
        /// The current pen colour
        /// </summary>
        public Color currentPenColour = Color.Black;
        /// <summary>
        /// The current x location
        /// </summary>
        public int currentXLocation = 10;
        /// <summary>
        /// The current y location
        /// </summary>
        public int currentYLocation = 10;
        /// <summary>
        /// The fill status
        /// </summary>
        public bool fillStatus = false;
        /// <summary>
        /// The draw box
        /// </summary>
        private PictureBox drawBox;
        /// <summary>
        /// The program box
        /// </summary>
        private TextBox programBox;

        /// <summary>
        /// The variables
        /// </summary>
        public Dictionary<string, int> variables = new Dictionary<string, int>();

        /// <summary>
        /// If block
        /// </summary>
        private bool ifBlock = false;
        /// <summary>
        /// The execute command
        /// </summary>
        private bool executeCommand = true;

        /// <summary>
        /// The loop block
        /// </summary>
        private bool loopBlock = false;
        /// <summary>
        /// The loop count
        /// </summary>
        private int loopCount = 0;
        /// <summary>
        /// The loop commands
        /// </summary>
        private List<string> loopCommands = new List<string>();

        /// <summary>
        /// Variable declaration for UserMethod
        /// </summary>
        private UserMethod currentMethod;
        /// <summary>
        /// The user methods
        /// </summary>
        private Dictionary<string, UserMethod> userMethods = new Dictionary<string, UserMethod>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParser"/> class.
        /// </summary>
        /// <param name="pictureBox">The picture box.</param>
        /// <param name="programBox">The program box.</param>
        public CommandParser(PictureBox pictureBox, TextBox programBox)
        {
            drawBox = pictureBox;
            g = drawBox.CreateGraphics();
            this.programBox = programBox;
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for rectangle command: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for circle command: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for triangle command: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for star command: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for moveTo command: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for drawTo command: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid operation: {op}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for expression:{entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Not a valid command: {entry}</exception>
        public void ProcessCommand(string entry)
        {
            entry = entry.ToLower();
            entry = entry.Trim();
           
            if (entry.StartsWith("method"))
            {
                DefineMethod(entry);
            }

            else if (entry.StartsWith("endmethod"))
            {
                currentMethod = null;
            }

            else if (currentMethod != null)
            {
                currentMethod.Commands.Add(entry);
            }

            else if (entry.Contains("("))
            {
                string methodName = entry.Split(" ")[0];

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
                    throw new CommandException($"Invalid parameters for rectangle command: {entry}");
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
                    throw new CommandException($"Invalid parameters for circle command: {entry}");
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
                    throw new CommandException($"Invalid parameters for triangle command: {entry}");
                }
            }

            else if (entry.StartsWith("star"))
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 3)
                {
                    DrawStar(parts[1], parts[2]);                  
                }
                else
                {
                    throw new CommandException($"Invalid parameters for star command: {entry}");
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
                    throw new CommandException($"Invalid parameters for moveTo command: {entry}");
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
                    throw new CommandException($"Invalid parameters for drawTo command: {entry}");
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

            /// <summary>
            /// Handles assignment of values to variables.
            /// </summary>

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
                    int int1 = GetParameterValue(parts[2]);
                    string op = parts[3];
                    int int2 = GetParameterValue(parts[4]);
                    
                    switch (op) // found this in w3 schools
                    {
                        case "+":
                            variables[varName] = int1 + int2;
                            break;
                        case "-":
                            variables[varName] = int1 - int2;
                            break;
                        case "*":
                            variables[varName] = int1 * int2;
                            break;
                        case "/":
                            variables[varName] = int1 / int2;
                            break;
                        default:
                            throw new CommandException($"Invalid operation: {op}");
                    }

                }

                else
                {
                    throw new CommandException($"Invalid parameters for expression:{entry}");
                }
            }

            else if (entry.Length == 0)
            {
                return;
            }

            else
            {
                throw new CommandException($"Not a valid command: {entry}");
            }
        }

        /// <summary>
        /// Verifies the program.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <exception cref="ASE_Assessment.CommandException">Invalid method name: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Method '{parts[1]}' is already defined.</exception>
        /// <exception cref="ASE_Assessment.CommandException">Method '{parts[1]}' needs brackets around parameters.</exception>
        /// <exception cref="ASE_Assessment.CommandException">'Endmethod' command without a corresponding method start.</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameter for {parts[0]} command: {parts[i]}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid operation: {op}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid syntax for assignment: {entry}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameter for {parts[0]} command: {parts[1]}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameter for {parts[0]} command</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid command: {entry}</exception>
        public void VerifyCommand(string entry)
        {
            entry = entry.ToLower();
            entry = entry.Trim();

            if (entry.StartsWith("method"))
            {
                string[] parts = entry.Split(' ');
                if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1]))
                {
                    throw new CommandException($"Invalid method name: {entry}");
                }
                else if (userMethods.ContainsKey(parts[1]))
                {
                    throw new CommandException($"Method '{parts[1]}' is already defined.");
                }

                else if (!(parts[2].Contains("(") && parts[2].Contains(")"))) 
                {
                    throw new CommandException($"Method '{parts[1]}' needs brackets around parameters.");  
                }

            }

            else if (entry == "endmethod")
            {
                // Check if inside a method definition block 
                if (currentMethod == null)
                {
                    throw new CommandException("'Endmethod' command without a corresponding method start.");
                }
                currentMethod = null; // Reset the current method context
            }
            
            else if (entry.StartsWith("rectangle") || entry.StartsWith("circle") || entry.StartsWith("triangle") || entry.StartsWith("star") || entry.StartsWith("move") || entry.StartsWith("drawto") || entry.StartsWith("loop"))
            {
                string[] parts = entry.Split(' ');

                // Get parameter values of parts
                for (int i = 1; i < parts.Length; i++)
                {
                    var parameterValue = GetParameterValue(parts[i]);
                    if (parameterValue <= 0)
                    {
                        throw new CommandException($"Invalid parameter for {parts[0]} command: {parts[i]}");
                    }
                    
                }
                
            }

            else if (entry.Contains("="))
            {
                string[] parts = entry.Split(' ');
                string varName = parts[0];
                string expression = parts[1].Trim();

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
                    int int1 = GetParameterValue(parts[2]);
                    string op = parts[3];
                    int int2 = GetParameterValue(parts[4]);

                    switch (op) // found this in w3 schools
                    {
                        case "+":
                            variables[varName] = int1 + int2;
                            break;
                        case "-":
                            variables[varName] = int1 - int2;
                            break;
                        case "*":
                            variables[varName] = int1 * int2;
                            break;
                        case "/":
                            variables[varName] = int1 / int2;
                            break;
                        default:
                            throw new CommandException($"Invalid operation: {op}");
                    }

                }
                else
                {
                    throw new CommandException($"Invalid syntax for assignment: {entry}");
                }
            }

            else if (entry == "fill on" || entry == "fill off")
            {
                string[] parts = entry.Split(' ');

                if (parts.Length == 2)
                {
                    if (!(parts[1] == "on" || parts[1] == "off"))
                    {
                        throw new CommandException($"Invalid parameter for {parts[0]} command: {parts[1]}");
                    }
                }
                else
                {
                    throw new CommandException($"Invalid parameter for {parts[0]} command");
                }
            }

            else if (entry.Length == 0)
            {
                return;
            }

            else
            {
                throw new CommandException($"Invalid command: {entry}");
            }


        }
        /// <summary>
        /// Changes the colour of the pen.
        /// </summary>
        /// <param name="colour">The colour.</param>
        private void PenColour(Color colour)
        {
            currentPenColour = colour;
        }

        /// <summary>
        /// Moves to a point defined by the x and y integers which represent x and y co-ordinates.
        /// </summary>
        /// <param name="x">The x position of the pen.</param>
        /// <param name="y">The y position of the pen.</param>
        private void MoveTo(string x, string y)
        {
            int xVal = GetParameterValue(x);
            int yVal = GetParameterValue(y);

            currentXLocation = xVal;
            currentYLocation = yVal;
        }

        /// <summary>
        /// Draws to a point defined by the x and y integers which represent x and y co-ordinates.
        /// </summary>
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

        /// <summary>
        /// Resets the pen to it's default position.
        /// </summary>
        private void Reset()
        {
            MoveTo("10", "10");
            fillStatus = false;
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        private void DrawRectangle(string width, string height)
        {
            int widthVal = GetParameterValue(width);
            int heightVal = GetParameterValue(height);

            var rectangle = new Rectangle(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            rectangle.Draw(widthVal, heightVal);
        }

        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="radiusParameter">The radius parameter.</param>
        private void DrawCircle(string radiusParameter)
        {
            int radius = GetParameterValue(radiusParameter);
            var circle = new Circle(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            circle.Draw(radius);
        }

        /// <summary>
        /// Draws a triangle.
        /// </summary>
        /// <param name="baseLengthParameter">The base length parameter.</param>
        /// <param name="heightParameter">The height parameter.</param>
        private void DrawTriangle(string baseLengthParameter, string heightParameter)
        {
            int baseLength = GetParameterValue(baseLengthParameter);
            int height = GetParameterValue(heightParameter);
            var triangle = new Triangle(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            triangle.Draw(baseLength, height);
        }

        /// <summary>
        /// Draws the star.
        /// </summary>
        /// <param name="pointsParameter">The points parameter.</param>
        /// <param name="sizeMultiplierParameter">The size multiplier parameter.</param>
        private void DrawStar(string pointsParameter, string sizeMultiplierParameter)
        {
            int points = GetParameterValue(pointsParameter);
            if (points < 4) 
            {
                throw new CommandException($"Number of points in star has to be over 3.");
            }
            int sizeMultiplier = GetParameterValue(sizeMultiplierParameter);
            var star = new Star(g, currentPenColour, fillStatus, currentXLocation, currentYLocation);
            star.Draw(points, sizeMultiplier);
        }

        /// <summary>
        /// Returns the parameter value. Checks if parameter is in the variable list or not.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ASE_Assessment.CommandException">Parameter value cannot be negative: {value}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Parameter value cannot be negative: {parsedValue}</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameter: {parameter}</exception>
        private int GetParameterValue(string parameter)
        {
            parameter = parameter.Trim();
            // Check if the parameter is a variable name in the dictionary
            if (variables.TryGetValue(parameter, out int value))
            {
                if (value < 0)
                {
                    throw new CommandException($"Parameter value cannot be negative: {value}");
                }
                return value;
            }

            // If not a variable, parse parameter as an integer
            if (int.TryParse(parameter, out int parsedValue))
            {
                if (parsedValue < 0)
                {
                    throw new CommandException($"Parameter value cannot be negative: {parsedValue}");
                }
                return parsedValue;
            }

            // If the parameter is neither a variable nor an integer, throw an exception
            throw new CommandException($"Invalid parameter: {parameter}");
        }

        /// <summary>
        /// Set the fill mode to on.
        /// </summary>
        private void FillOn()
        {
            fillStatus = true;
        }

        /// <summary>
        /// Set the fill mode to off.
        /// </summary>
        private void FillOff()
        {
            fillStatus = false;
        }

        /// <summary>
        /// Evaluates condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ASE_Assessment.CommandException">Invalid operator in condition</exception>
        /// <exception cref="ASE_Assessment.CommandException">Invalid parameters for if condition</exception>
        private bool EvaluateCondition(string condition)
        {
            string[] parts = condition.Split(" ");

            if (parts.Length == 3)
            {
                string val1 = parts[0];
                string op = parts[1];
                string val2 = parts[2];
                int int1 = GetParameterValue(val1);
                int int2 = GetParameterValue(val2);
                
                switch (op)
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
                        throw new CommandException("Invalid operator in condition");
                }

            }
            else
            {
                throw new CommandException("Invalid parameters for if condition");
            }
        }

        /// <summary>
        /// Defines the method.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <exception cref="ASE_Assessment.CommandException">A method with the name '{methodName}' already exists.</exception>
        private void DefineMethod(string definition)
        {
            var parts = definition.Split(" ");

            var methodName = parts[1];                                       
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
                throw new CommandException($"A method with the name '{methodName}' already exists.");
            }
            currentMethod = new UserMethod
            {
                Parameters = parameters
            };
            
            userMethods.Add(methodName, currentMethod);// Store the method definition

        }
        /// <summary>
        /// Calls the method.
        /// </summary>
        /// <param name="call">The call.</param>
        /// <exception cref="ASE_Assessment.CommandException">Method '{methodName}' not defined.</exception>
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
                // Replace parameters with actual arguments
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
                throw new CommandException($"Method '{methodName}' not defined.");
            }

        }

        /// <summary>Clears the graphics.</summary>
        /// <param name="drawBox">The draw box.</param>
        public void ClearGraphics(PictureBox drawBox)
        {
            if (drawBox.Image != null)
            {
                drawBox.Image.Dispose(); 
            }
            drawBox.Image = new Bitmap(drawBox.Width, drawBox.Height);
        }

    }
}


