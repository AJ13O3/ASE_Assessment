namespace ASE_Assessment
{
    partial class GraphicsLanguage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            commandLine = new TextBox();
            runButton = new Button();
            drawBox = new PictureBox();
            programBox = new TextBox();
            openButton = new Button();
            saveButton = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)drawBox).BeginInit();
            SuspendLayout();
            // 
            // commandLine
            // 
            commandLine.Location = new Point(12, 356);
            commandLine.Multiline = true;
            commandLine.Name = "commandLine";
            commandLine.Size = new Size(776, 23);
            commandLine.TabIndex = 0;
            commandLine.KeyPress += commandLine_KeyPress;
            // 
            // runButton
            // 
            runButton.Location = new Point(12, 385);
            runButton.Name = "runButton";
            runButton.Size = new Size(75, 23);
            runButton.TabIndex = 1;
            runButton.Text = "Run";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += runButton_Click;
            // 
            // drawBox
            // 
            drawBox.BackColor = Color.White;
            drawBox.Location = new Point(403, 31);
            drawBox.Name = "drawBox";
            drawBox.Size = new Size(385, 319);
            drawBox.TabIndex = 2;
            drawBox.TabStop = false;
            // 
            // programBox
            // 
            programBox.Location = new Point(12, 31);
            programBox.Multiline = true;
            programBox.Name = "programBox";
            programBox.Size = new Size(385, 319);
            programBox.TabIndex = 3;
            // 
            // openButton
            // 
            openButton.Location = new Point(403, 385);
            openButton.Name = "openButton";
            openButton.Size = new Size(75, 23);
            openButton.TabIndex = 4;
            openButton.Text = "Open";
            openButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(713, 385);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // GraphicsLanguage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(saveButton);
            Controls.Add(openButton);
            Controls.Add(programBox);
            Controls.Add(drawBox);
            Controls.Add(runButton);
            Controls.Add(commandLine);
            Name = "GraphicsLanguage";
            Text = "GraphicsLanguage";
            ((System.ComponentModel.ISupportInitialize)drawBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox commandLine;
        private Button runButton;
        private PictureBox drawBox;
        private TextBox programBox;
        private Button openButton;
        private Button saveButton;
        private Label label1;
    }
}