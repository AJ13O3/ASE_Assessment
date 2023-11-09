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
            commandParser = new CommandParser(drawBox);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            commandParser.processCommand(commandLine.Text);
            commandLine.Text = null;
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