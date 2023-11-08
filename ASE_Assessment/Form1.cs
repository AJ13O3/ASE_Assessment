using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASE_Assessment
{
    public partial class GraphicsLanguage : Form
    {
        private CommandParser commandParser;
        public GraphicsLanguage()
        {
            InitializeComponent();
            commandParser = new CommandParser(drawBox.CreateGraphics());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            commandParser.processCommand(commandLine.Text);
            commandLine.Text = null;
        }

        private void commandLine_TextChanged(object sender, EventArgs e)
        {

        }

        private void commandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                commandParser.processCommand(commandLine.Text);
                commandLine.Text = null;
            }
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        }
    }
    public class CommandParser
    {
        private Graphics drawingGraphics;

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
                    DrawRectangle(width, height);
                }
                else
                {
                    throw new ArgumentException("Invalid parameters for rectangle command.");
                }
            }
        }

        private void DrawRectangle(int width, int height)
        {
            Pen pen = new Pen(Color.Black); // You can customize the pen color and other properties here
            drawingGraphics.DrawRectangle(pen, 10, 10, width, height);
        }
    }

}
