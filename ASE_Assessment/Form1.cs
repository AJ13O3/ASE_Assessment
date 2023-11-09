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
            commandParser = new CommandParser(drawBox, programBox);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (commandLine.Text.Length > 0)
            {
                commandParser.processCommand(commandLine.Text);
                commandLine.Text = null;
            }
            else
            {
                foreach (string n in programBox.Lines)
                {
                    commandParser.processCommand(n);
                }
            }
        }

        private void commandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                commandParser.processCommand(commandLine.Text);
                commandLine.Text = null;
            }
        }

    }


}