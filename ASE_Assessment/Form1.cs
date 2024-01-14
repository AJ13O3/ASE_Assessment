// ***********************************************************************
// Assembly         : ASE_Assessment
// Author           : amanj
// Created          : 11-08-2023
//
// Last Modified By : amanj
// Last Modified On : 01-12-2024
// ***********************************************************************
// <copyright file="Form1.cs" company="ASE_Assessment">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class GraphicsLanguage.
    /// Implements the <see cref="Form" />
    /// </summary>
    /// <seealso cref="Form" />
    public partial class GraphicsLanguage : Form
    {
        /// <summary>
        /// The command parser
        /// </summary>
        private CommandParser commandParser;


        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsLanguage"/> class.
        /// </summary>
        public GraphicsLanguage()
        {
            InitializeComponent();
            commandParser = new CommandParser(drawBox, programBox);

        }

        /// <summary>
        /// Handles the Click event of the runButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void runButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (commandLine.Text.Length > 0)
                {
                    commandParser.ProcessCommand(commandLine.Text);
                    commandLine.Text = "";
                }
                else
                {
                    foreach (string line in programBox.Lines)
                    {
                        commandParser.ProcessCommand(line);
                    }
                }
            }
            catch (CommandException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the commandLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void commandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    commandParser.ProcessCommand(commandLine.Text);
                    commandLine.Text = "";
                }
                catch (CommandException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the saveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the openButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the KeyDown event of the programBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the VerifyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void VerifyButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string line in programBox.Lines)
                {
                    commandParser.VerifyCommand(line);
                }
                MessageBox.Show("No errors found.", "Verification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (CommandException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            commandParser.ClearGraphics(drawBox);
        }
    }


}