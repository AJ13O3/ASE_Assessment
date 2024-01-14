using ASE_Assessment;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace ASETests

{
    [TestClass]
    public class ASETests
    {

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
        [TestMethod]
        public void TestPen()
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
    }
}