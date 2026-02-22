namespace Sde.NeuralNetworks.WinForms
{
    partial class TrainingDataPropertiesForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            numericUpDownUpperBound = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            label2 = new Label();
            numericUpDownLowerBound = new NumericUpDown();
            label3 = new Label();
            numericUpDownIncrement = new NumericUpDown();
            numericUpDownPercentageOfTrainingData = new NumericUpDown();
            comboBoxDataProvider = new ComboBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUpperBound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLowerBound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownIncrement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPercentageOfTrainingData).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 231F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 295F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(numericUpDownUpperBound, 3, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(label5, 2, 3);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(numericUpDownLowerBound, 1, 2);
            tableLayoutPanel1.Controls.Add(label3, 2, 2);
            tableLayoutPanel1.Controls.Add(numericUpDownIncrement, 1, 3);
            tableLayoutPanel1.Controls.Add(numericUpDownPercentageOfTrainingData, 3, 3);
            tableLayoutPanel1.Controls.Add(comboBoxDataProvider, 0, 1);
            tableLayoutPanel1.Location = new Point(3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(791, 148);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 4);
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(785, 30);
            label1.TabIndex = 1;
            label1.Text = "Data provider: The thing which creates the data to train and test the network. The problem that the network is intended to solve. This determines the number of inputs and outputs in the network";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numericUpDownUpperBound
            // 
            numericUpDownUpperBound.Dock = DockStyle.Fill;
            numericUpDownUpperBound.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownUpperBound.Location = new Point(670, 72);
            numericUpDownUpperBound.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownUpperBound.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDownUpperBound.Name = "numericUpDownUpperBound";
            numericUpDownUpperBound.Size = new Size(118, 33);
            numericUpDownUpperBound.TabIndex = 6;
            numericUpDownUpperBound.TextAlign = HorizontalAlignment.Right;
            numericUpDownUpperBound.ThousandsSeparator = true;
            numericUpDownUpperBound.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 108);
            label4.Name = "label4";
            label4.Size = new Size(225, 40);
            label4.TabIndex = 4;
            label4.Text = "Increment: the difference between sequential values in the training data";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(375, 108);
            label5.Name = "label5";
            label5.Size = new Size(289, 40);
            label5.TabIndex = 5;
            label5.Text = "The percentage of the training data to use for testing the trained network instead of for training it (1-99)";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 69);
            label2.Name = "label2";
            label2.Size = new Size(225, 39);
            label2.TabIndex = 2;
            label2.Text = "Lower bound: The lowest value to use in the training data";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numericUpDownLowerBound
            // 
            numericUpDownLowerBound.Dock = DockStyle.Fill;
            numericUpDownLowerBound.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownLowerBound.Location = new Point(234, 72);
            numericUpDownLowerBound.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownLowerBound.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDownLowerBound.Name = "numericUpDownLowerBound";
            numericUpDownLowerBound.Size = new Size(135, 33);
            numericUpDownLowerBound.TabIndex = 0;
            numericUpDownLowerBound.TextAlign = HorizontalAlignment.Right;
            numericUpDownLowerBound.ThousandsSeparator = true;
            numericUpDownLowerBound.Value = new decimal(new int[] { 50, 0, 0, int.MinValue });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(375, 69);
            label3.Name = "label3";
            label3.Size = new Size(289, 39);
            label3.TabIndex = 3;
            label3.Text = "Upper bound: The highest value to use in the training data";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numericUpDownIncrement
            // 
            numericUpDownIncrement.DecimalPlaces = 2;
            numericUpDownIncrement.Dock = DockStyle.Fill;
            numericUpDownIncrement.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownIncrement.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownIncrement.Location = new Point(234, 111);
            numericUpDownIncrement.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numericUpDownIncrement.Name = "numericUpDownIncrement";
            numericUpDownIncrement.Size = new Size(135, 33);
            numericUpDownIncrement.TabIndex = 7;
            numericUpDownIncrement.TextAlign = HorizontalAlignment.Right;
            numericUpDownIncrement.ThousandsSeparator = true;
            numericUpDownIncrement.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // numericUpDownPercentageOfTrainingData
            // 
            numericUpDownPercentageOfTrainingData.Dock = DockStyle.Fill;
            numericUpDownPercentageOfTrainingData.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownPercentageOfTrainingData.Location = new Point(670, 111);
            numericUpDownPercentageOfTrainingData.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numericUpDownPercentageOfTrainingData.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownPercentageOfTrainingData.Name = "numericUpDownPercentageOfTrainingData";
            numericUpDownPercentageOfTrainingData.Size = new Size(118, 33);
            numericUpDownPercentageOfTrainingData.TabIndex = 8;
            numericUpDownPercentageOfTrainingData.TextAlign = HorizontalAlignment.Right;
            numericUpDownPercentageOfTrainingData.ThousandsSeparator = true;
            numericUpDownPercentageOfTrainingData.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // comboBoxDataProvider
            // 
            tableLayoutPanel1.SetColumnSpan(comboBoxDataProvider, 4);
            comboBoxDataProvider.Dock = DockStyle.Fill;
            comboBoxDataProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDataProvider.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxDataProvider.FormattingEnabled = true;
            comboBoxDataProvider.Location = new Point(3, 33);
            comboBoxDataProvider.Name = "comboBoxDataProvider";
            comboBoxDataProvider.Size = new Size(785, 33);
            comboBoxDataProvider.TabIndex = 0;
            // 
            // TrainingDataPropertiesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 153);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "TrainingDataPropertiesForm";
            Text = "Training data properties";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUpperBound).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLowerBound).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownIncrement).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPercentageOfTrainingData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private NumericUpDown numericUpDownLowerBound;
        private Label label1;
        private ComboBox comboBoxDataProvider;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private NumericUpDown numericUpDownUpperBound;
        private NumericUpDown numericUpDownIncrement;
        private NumericUpDown numericUpDownPercentageOfTrainingData;
    }
}