using ASE_Assessment;
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
    }
}