namespace Sde.MatrixDemo.Forms
{
    partial class TransposeForm
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
            labelResult = new Label();
            button1 = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(matrixTextBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(labelResult, 2, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(431, 190);
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
            matrixTextBox1.Size = new Size(180, 184);
            matrixTextBox1.TabIndex = 0;
            matrixTextBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(189, 0);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 1;
            label1.Text = "^T =";
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Dock = DockStyle.Fill;
            labelResult.Location = new Point(248, 0);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(180, 190);
            labelResult.TabIndex = 2;
            labelResult.TextAlign = ContentAlignment.TopCenter;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Location = new Point(29, -40);
            button1.Name = "button1";
            button1.Size = new Size(335, 35);
            button1.TabIndex = 1;
            button1.Text = "So we have something to tab away to";
            button1.UseVisualStyleBackColor = true;
            // 
            // TransposeForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 214);
            Controls.Add(button1);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "TransposeForm";
            Text = "Transpose a matrix";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Controls.MatrixTextBox matrixTextBox1;
        private Label label1;
        private Label labelResult;
        private Button button1;
    }
}