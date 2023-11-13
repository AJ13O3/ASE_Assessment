using ASE_Assessment;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

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
            parser.processCommand("moveTo "+newXLocation+ " " +newYLocation);

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
                parser.processCommand("moveTo " + newXLocation + " " + newYLocation);
                Assert.Fail("Expected an ArgumentException to be thrown");
            }
            catch (ArgumentException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an ArgumentException, but a different exception was thrown: " + altException.Message);
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
            parser.processCommand("moveTo " + startX + " " + startY);

            // Act
            parser.processCommand("drawTo " + endX + " " + endY);

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
                parser.processCommand("moveTo " + startX + " " + startY);
                parser.processCommand("drawTo " + endX + " " + endY);
                Assert.Fail("Expected an ArgumentException to be thrown");
            }
            catch (ArgumentException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an ArgumentException, but a different exception was thrown: " + altException.Message);
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
            parser.processCommand("moveTo " + startX + " " + startY);

            // Act
            parser.processCommand("reset");

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
                parser.processCommand("rectangle " + width + " " + height);
                Assert.Fail("Expected an ArgumentException to be thrown");
            }
            catch (ArgumentException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an ArgumentException, but a different exception was thrown: " + altException.Message);
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
                parser.processCommand("circle " + radius);
                Assert.Fail("Expected an ArgumentException to be thrown");
            }
            catch (ArgumentException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an ArgumentException, but a different exception was thrown: " + altException.Message);
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
                parser.processCommand("triangle " + baseLength + " " + height);
                Assert.Fail("Expected an ArgumentException to be thrown");
            }
            catch (ArgumentException)
            {
                // Test pass
            }
            catch (Exception altException)
            {
                Assert.Fail("Expected an ArgumentException, but a different exception was thrown: " + altException.Message);
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
            parser.processCommand("fill on");

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
            parser.processCommand("fill on");
            parser.processCommand("fill off");

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
            parser.processCommand("green pen");

            // Assert
            Assert.AreEqual(newPenColour, parser.currentPenColour, "Pen colour not updated correctly");
        }
    }
}