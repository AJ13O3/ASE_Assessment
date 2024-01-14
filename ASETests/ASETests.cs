// ***********************************************************************
// Assembly         : ASETests
// Author           : amanj
// Created          : 11-13-2023
//
// Last Modified By : amanj
// Last Modified On : 01-14-2024
// ***********************************************************************
// <copyright file="ASETests.cs" company="ASETests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ASE_Assessment;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace ASETests

{
    /// <summary>
    /// Defines test class ASETests.
    /// </summary>
    [TestClass]
    public class ASETests
    {

        /// <summary>
        /// Defines the test for the moveto command with a valid input.
        /// </summary>
        [TestMethod]
        public void TestMoveToCommand()
        {
            // Arrange
            int newXLocation = 20;
            int newYLocation = 30;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("moveTo "+newXLocation+ " " +newYLocation);

            // Assert
            Assert.AreEqual(newXLocation, parser.currentXLocation, "X location not updated correctly");
            Assert.AreEqual(newYLocation, parser.currentYLocation, "Y location not updated correctly");
        }

        /// <summary>
        ///  Defines the test for the moveto command with an invalid input.
        /// </summary>
        [TestMethod]
        public void TestMoveToNegative()
        {
            // Arrange
            int newXLocation = -50;
            int newYLocation = -30;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act & Assert
            try
            {
                parser.ProcessCommand("moveTo " + newXLocation + " " + newYLocation);
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an CommandException, but a different exception was thrown: " + altException.Message);
            }
        }

        /// <summary>
        /// Defines the test for the drawto command with a valid input.
        /// </summary>
        [TestMethod]
        public void TestDrawToCommand()
        {
            // Arrange
            int startX = 10;
            int startY = 10;
            int endX = 20;
            int endY = 30;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Starting position
            parser.ProcessCommand("moveTo " + startX + " " + startY);

            // Act
            parser.ProcessCommand("drawTo " + endX + " " + endY);

            // Assert

            Assert.AreEqual(endX, parser.currentXLocation, "X location not updated correctly after drawTo");
            Assert.AreEqual(endY, parser.currentYLocation, "Y location not updated correctly after drawTo");

        }

        /// <summary>
        /// Defines the test for the drawto command with an invalid input.
        /// </summary>
        [TestMethod]
        public void TestDrawToNegative()
        {
            // Arrange
            int startX = -10;
            int startY = -10;
            int endX = -20;
            int endY = -30;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act & Assert
            try
            {
                parser.ProcessCommand("moveTo " + startX + " " + startY);
                parser.ProcessCommand("drawTo " + endX + " " + endY);
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an CommandException, but a different exception was thrown: " + altException.Message);
            }
        }

        /// <summary>
        /// Defines the test for the Reset Command.
        /// </summary>
        [TestMethod]
        public void TestResetCommand()
        {
            // Arrange
            int startX = 50;
            int startY = 50;
            int endX = 10;
            int endY = 10;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Starting position
            parser.ProcessCommand("moveTo " + startX + " " + startY);

            // Act
            parser.ProcessCommand("reset");

            // Assert

            Assert.AreEqual(endX, parser.currentXLocation, "X location not updated correctly after reset");
            Assert.AreEqual(endY, parser.currentYLocation, "Y location not updated correctly after reset");

        }

        /// <summary>
        /// Defines the test for the DrawRectangle command.
        /// </summary>
        [TestMethod]
        public void TestDrawRectangle()
        {
            // Arrange
            int width = -50;
            int height = -30;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act & Assert
            try
            {
                parser.ProcessCommand("rectangle " + width + " " + height);
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an CommandException, but a different exception was thrown: " + altException.Message);
            }
        }

        /// <summary>
        /// Defines the test for the DrawCircle commnand.
        /// </summary>
        [TestMethod]
        public void TestDrawCircle()
        {
            // Arrange
            int radius = -50;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act & Assert
            try
            {
                parser.ProcessCommand("circle " + radius);
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an CommandException, but a different exception was thrown: " + altException.Message);
            }
        }

        /// <summary>
        /// Defines the test for the DrawTriangle command.
        /// </summary>
        [TestMethod]
        public void TestDrawTriangle()
        {
            // Arrange
            int baseLength = -10;
            int height = -30;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act & Assert
            try
            {
                parser.ProcessCommand("triangle " + baseLength + " " + height);
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an CommandException, but a different exception was thrown: " + altException.Message);
            }
        }

        /// <summary>
        /// Defines the test for the FillOn command.
        /// </summary>
        [TestMethod]
        public void TestFillOn()
        {
            // Arrange
            bool newFillStatus = true;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("fill on");

            // Assert
            Assert.AreEqual(newFillStatus, parser.fillStatus, "Fill status not updated correctly");
        }

        /// <summary>
        /// Defines the test for the FillOff command.
        /// </summary>
        [TestMethod]
        public void TestFillOff()
        {
            // Arrange
            bool newFillStatus = false;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("fill on");
            parser.ProcessCommand("fill off");

            // Assert
            Assert.AreEqual(newFillStatus, parser.fillStatus, "Fill status not updated correctly");
        }

        /// <summary>
        /// Defines the test for the pen colour feature.
        /// </summary>
        [TestMethod]
        public void TestPenColour()
        {
            // Arrange
            Color newPenColour = Color.Green;
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("green pen");

            // Assert
            Assert.AreEqual(newPenColour, parser.currentPenColour, "Pen colour not updated correctly");
        }

        /// <summary>
        /// Defines the test for an invalid command.
        /// </summary>
        [TestMethod]
        public void TestInvalidCommand () 
        {
            // Arrange
            string command = "agfvayjkisgd";
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act & Assert
            try
            {
                parser.ProcessCommand(command);
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an CommandException, but a different exception was thrown: " + altException.Message);
            }
        }

        /// <summary>
        /// Defines the test for declaring and modifying variables.
        /// </summary>
        [TestMethod]
        public void TestVariableDeclarationAndModification()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox); 

            // Act
            parser.ProcessCommand("x = 10");
            parser.ProcessCommand("x = 20");

            // Assert
            Assert.IsTrue(parser.variables.ContainsKey("x"), "Variable was not declared.");
            Assert.AreEqual(20, parser.variables["x"], "Variable value was not modified correctly.");
        }

        /// <summary>
        /// Defines the test for processing expressions in the variable declaration.
        /// </summary>
        [TestMethod]
        public void TestVariableExpression()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("x = 10");
            parser.ProcessCommand("x = x + 20");

            // Assert
            Assert.AreEqual(30, parser.variables["x"], "Variable value was not modified correctly.");
        }

        /// <summary>
        /// Defines the test for if statements.
        /// </summary>
        [TestMethod]
        public void TestIfStatementExecution()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);
            parser.variables["x"] = 10;

            // Act & Assert
            try
            {
                parser.ProcessCommand("if x > 20");
                parser.ProcessCommand("thisisnotacommand"); //this shouldn't run, test will fail if it does
                parser.ProcessCommand("endif");                
            }
            catch(CommandException)
            {
                Assert.Fail("Did not expect an CommandException to be thrown");
            }
        }

        /// <summary>
        /// Defines the test for the loop command.
        /// </summary>
        [TestMethod]
        public void TestLoop()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("x = 0");
            parser.ProcessCommand("loop 3");
            parser.ProcessCommand("x = x + 10");
            parser.ProcessCommand("endloop");

            // Assert
            Assert.AreEqual(30, parser.variables["x"], "Variable value was not modified correctly.");
        }

        /// <summary>
        /// Defines the test for the syntax check feature.
        /// </summary>
        [TestMethod]
        public void TestSyntaxCheck()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            try
            {
                parser.VerifyCommand("circle -30");
                Assert.Fail("Expected an CommandException to be thrown");
            }
            catch (CommandException)
            {
                // pass
            }
        }

        /// <summary>
        /// Defines the test for methods with parameters.
        /// </summary>
        [TestMethod]
        public void TestMethodsWithParam()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("method func1 (n1)");
            parser.ProcessCommand("x = n1");
            parser.ProcessCommand("endmethod");
            parser.ProcessCommand("func1 (10)");


            // Assert
            Assert.IsTrue(parser.variables.ContainsKey("x"), "Variable was not declared.");
            Assert.AreEqual(10, parser.variables["x"], "Variable value was not modified correctly.");
        }

        /// <summary>
        /// Defines the test for methods without parameters.
        /// </summary>
        [TestMethod]
        public void TestMethodsWithoutParam()
        {
            // Arrange
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.ProcessCommand("method func1 ()");
            parser.ProcessCommand("x = 40");
            parser.ProcessCommand("endmethod");
            parser.ProcessCommand("func1 ()");


            // Assert
            Assert.IsTrue(parser.variables.ContainsKey("x"), "Variable was not declared.");
            Assert.AreEqual(40, parser.variables["x"], "Variable value was not modified correctly.");
        }

    }
}