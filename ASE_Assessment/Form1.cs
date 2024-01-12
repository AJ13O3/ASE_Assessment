using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private void runButton_Click(object sender, EventArgs e)
        {
            if (commandLine.Text.Length > 0)
            {
                commandParser.ProcessCommand(commandLine.Text);
                commandLine.Text = null;
            }
            else
            {
                foreach (string n in programBox.Lines)
                {
                    commandParser.ProcessCommand(n);
                }
            }
        }

        private void commandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                commandParser.ProcessCommand(commandLine.Text);
                commandLine.Text = null;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "C:\\Users\\amanj\\source\\repos\\ASE_Assessment";

            saveFileDialog.Title = "Save Program File";

            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";

            saveFileDialog.DefaultExt = "txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, programBox.Text);
            }

        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C:\\Users\\amanj\\source\\repos\\ASE_Assessment";

            openFileDialog.Title = "Save Program File";

            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            openFileDialog.DefaultExt = "txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                programBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void programBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                int selectionIndex = programBox.SelectionStart;
                programBox.Text = programBox.Text.Insert(selectionIndex, "\t");
                programBox.SelectionStart = selectionIndex + 1;

                // Prevent the default behavior of the Tab key
            }

        }
    }


}