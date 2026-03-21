namespace Sde.MatrixDemo.Forms
{
    partial class ScalarMultiplicationForm
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
            matrixTextBox1 = new Sde.MatrixDemo.Controls.MatrixTextBox();
            label1 = new Label();
            numericUpDownScalar = new NumericUpDown();
            label2 = new Label();
            labelResult = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScalar).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(matrixTextBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(numericUpDownScalar, 2, 0);
            tableLayoutPanel1.Controls.Add(label2, 3, 0);
            tableLayoutPanel1.Controls.Add(labelResult, 4, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(802, 174);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // matrixTextBox1
            // 
            matrixTextBox1.Dock = DockStyle.Fill;
            matrixTextBox1.Font = new Font("Consolas", 14F);
            matrixTextBox1.Location = new Point(3, 3);
            matrixTextBox1.Multiline = true;
            matrixTextBox1.Name = "matrixTextBox1";
            matrixTextBox1.ScrollBars = ScrollBars.Vertical;
            matrixTextBox1.Size = new Size(242, 168);
            matrixTextBox1.TabIndex = 0;
            matrixTextBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(251, 0);
            label1.Name = "label1";
            label1.Size = new Size(20, 25);
            label1.TabIndex = 1;
            label1.Text = "*";
            // 
            // numericUpDownScalar
            // 
            numericUpDownScalar.DecimalPlaces = 3;
            numericUpDownScalar.Dock = DockStyle.Fill;
            numericUpDownScalar.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownScalar.Location = new Point(277, 3);
            numericUpDownScalar.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownScalar.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDownScalar.Name = "numericUpDownScalar";
            numericUpDownScalar.Size = new Size(242, 33);
            numericUpDownScalar.TabIndex = 2;
            numericUpDownScalar.ThousandsSeparator = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(525, 0);
            label2.Name = "label2";
            label2.Size = new Size(25, 25);
            label2.TabIndex = 3;
            label2.Text = "=";
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Dock = DockStyle.Fill;
            labelResult.Location = new Point(556, 0);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(243, 174);
            labelResult.TabIndex = 4;
            // 
            // ScalarMultiplicationForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 198);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "ScalarMultiplicationForm";
            Text = "Scalar multiplication";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScalar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Controls.MatrixTextBox matrixTextBox1;
        private Label label1;
        private NumericUpDown numericUpDownScalar;
        private Label label2;
        private Label labelResult;
    }
}