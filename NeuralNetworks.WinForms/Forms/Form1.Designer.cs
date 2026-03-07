namespace Sde.NeuralNetworks.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            progressBar1 = new ProgressBar();
            buttonStop = new Button();
            buttonGo = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openTrainedModelToolStripMenuItem = new ToolStripMenuItem();
            saveCurrentModelToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();
            textBoxStatus = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(12, 137);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1217, 23);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 15;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonStop.Location = new Point(147, 38);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(133, 37);
            buttonStop.TabIndex = 12;
            buttonStop.Text = "Stop training";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += ButtonStopClick;
            // 
            // buttonGo
            // 
            buttonGo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonGo.Location = new Point(12, 36);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new Size(129, 39);
            buttonGo.TabIndex = 5;
            buttonGo.Text = "Start training";
            buttonGo.UseVisualStyleBackColor = false;
            buttonGo.Click += ButtonGo_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1241, 33);
            menuStrip1.TabIndex = 18;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openTrainedModelToolStripMenuItem, saveCurrentModelToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(53, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // openTrainedModelToolStripMenuItem
            // 
            openTrainedModelToolStripMenuItem.Name = "openTrainedModelToolStripMenuItem";
            openTrainedModelToolStripMenuItem.Size = new Size(253, 30);
            openTrainedModelToolStripMenuItem.Text = "Open trained model";
            openTrainedModelToolStripMenuItem.Click += OpenTrainedModelToolStripMenuItem_Click;
            // 
            // saveCurrentModelToolStripMenuItem
            // 
            saveCurrentModelToolStripMenuItem.Name = "saveCurrentModelToolStripMenuItem";
            saveCurrentModelToolStripMenuItem.Size = new Size(253, 30);
            saveCurrentModelToolStripMenuItem.Text = "Save current model";
            saveCurrentModelToolStripMenuItem.Click += SaveCurrentModelToolStripMenuItem_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.AddToRecent = false;
            saveFileDialog1.Filter = "JSON files|*.json";
            saveFileDialog1.Title = "Save network to file";
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddToRecent = false;
            openFileDialog1.Filter = "JSON files|*.json";
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Title = "Browse for trained network file";
            // 
            // textBoxStatus
            // 
            textBoxStatus.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxStatus.Location = new Point(301, 41);
            textBoxStatus.Multiline = true;
            textBoxStatus.Name = "textBoxStatus";
            textBoxStatus.ReadOnly = true;
            textBoxStatus.Size = new Size(928, 90);
            textBoxStatus.TabIndex = 19;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1241, 169);
            Controls.Add(textBoxStatus);
            Controls.Add(progressBar1);
            Controls.Add(menuStrip1);
            Controls.Add(buttonStop);
            Controls.Add(buttonGo);
            Name = "Form1";
            Text = "Neural Network Playground";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonGo;
        private Button buttonStop;
        private ProgressBar progressBar1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openTrainedModelToolStripMenuItem;
        private ToolStripMenuItem saveCurrentModelToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private TextBox textBoxStatus;
    }
}
