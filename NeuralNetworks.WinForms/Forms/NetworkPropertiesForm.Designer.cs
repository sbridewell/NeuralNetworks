namespace Sde.NeuralNetworks.WinForms
{
    partial class NetworkPropertiesForm
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
            numericUpDownNodesInHiddenLayer = new NumericUpDown();
            numericUpDownMomentum = new NumericUpDown();
            numericUpDownLearningRate = new NumericUpDown();
            numericUpDownNumberOfIterations = new NumericUpDown();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNodesInHiddenLayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMomentum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLearningRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNumberOfIterations).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDownNodesInHiddenLayer
            // 
            numericUpDownNodesInHiddenLayer.Font = new Font("Segoe UI", 14.25F);
            numericUpDownNodesInHiddenLayer.Location = new Point(415, 117);
            numericUpDownNodesInHiddenLayer.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownNodesInHiddenLayer.Name = "numericUpDownNodesInHiddenLayer";
            numericUpDownNodesInHiddenLayer.Size = new Size(119, 33);
            numericUpDownNodesInHiddenLayer.TabIndex = 7;
            numericUpDownNodesInHiddenLayer.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // numericUpDownMomentum
            // 
            numericUpDownMomentum.DecimalPlaces = 3;
            numericUpDownMomentum.Font = new Font("Segoe UI", 14.25F);
            numericUpDownMomentum.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownMomentum.Location = new Point(415, 79);
            numericUpDownMomentum.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownMomentum.Name = "numericUpDownMomentum";
            numericUpDownMomentum.Size = new Size(119, 33);
            numericUpDownMomentum.TabIndex = 6;
            numericUpDownMomentum.Value = new decimal(new int[] { 1, 0, 0, 196608 });
            // 
            // numericUpDownLearningRate
            // 
            numericUpDownLearningRate.DecimalPlaces = 3;
            numericUpDownLearningRate.Font = new Font("Segoe UI", 14.25F);
            numericUpDownLearningRate.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownLearningRate.Location = new Point(415, 41);
            numericUpDownLearningRate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownLearningRate.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownLearningRate.Name = "numericUpDownLearningRate";
            numericUpDownLearningRate.Size = new Size(119, 33);
            numericUpDownLearningRate.TabIndex = 5;
            numericUpDownLearningRate.Value = new decimal(new int[] { 1, 0, 0, 196608 });
            // 
            // numericUpDownNumberOfIterations
            // 
            numericUpDownNumberOfIterations.Font = new Font("Segoe UI", 14.25F);
            numericUpDownNumberOfIterations.Location = new Point(415, 3);
            numericUpDownNumberOfIterations.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownNumberOfIterations.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownNumberOfIterations.Name = "numericUpDownNumberOfIterations";
            numericUpDownNumberOfIterations.Size = new Size(119, 33);
            numericUpDownNumberOfIterations.TabIndex = 4;
            numericUpDownNumberOfIterations.ThousandsSeparator = true;
            numericUpDownNumberOfIterations.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 76);
            label3.Name = "label3";
            label3.Size = new Size(406, 38);
            label3.TabIndex = 2;
            label3.Text = "Momentum: The amount by which the weights and biases are changed in the same direction as they were changed in the previous epoch of training";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.7225342F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.2774677F));
            tableLayoutPanel1.Controls.Add(numericUpDownNodesInHiddenLayer, 1, 3);
            tableLayoutPanel1.Controls.Add(numericUpDownMomentum, 1, 2);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Controls.Add(label6, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(numericUpDownLearningRate, 1, 1);
            tableLayoutPanel1.Controls.Add(numericUpDownNumberOfIterations, 1, 0);
            tableLayoutPanel1.Controls.Add(label7, 0, 3);
            tableLayoutPanel1.Location = new Point(1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(537, 155);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(406, 38);
            label5.TabIndex = 0;
            label5.Text = "Number of iterations: How many times to present the training data to the network during training";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 38);
            label6.Name = "label6";
            label6.Size = new Size(406, 38);
            label6.TabIndex = 5;
            label6.Text = "Learning rate: How much the weights and biases can change in a single iteration";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(3, 114);
            label7.Name = "label7";
            label7.Size = new Size(406, 41);
            label7.TabIndex = 7;
            label7.Text = "The number of nodes (neurons) in each hidden layer of the network";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NetworkPropertiesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 158);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "NetworkPropertiesForm";
            Text = "Network properties";
            ((System.ComponentModel.ISupportInitialize)numericUpDownNodesInHiddenLayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMomentum).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLearningRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNumberOfIterations).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label3;
        private NumericUpDown numericUpDownMomentum;
        private NumericUpDown numericUpDownLearningRate;
        private NumericUpDown numericUpDownNumberOfIterations;
        private NumericUpDown numericUpDownNodesInHiddenLayer;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label6;
        private Label label7;
    }
}