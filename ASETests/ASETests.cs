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
            string command = $"move {newXLocation} {newYLocation}";
            PictureBox pictureBox = new PictureBox();
            TextBox programBox = new TextBox();
            CommandParser parser = new CommandParser(pictureBox, programBox);

            // Act
            parser.processCommand(command);

            // Assert
            Assert.AreEqual(newXLocation, parser.currentXLocation, "X location not updated correctly");
            Assert.AreEqual(newYLocation, parser.currentYLocation, "Y location not updated correctly");
        }
    }
}