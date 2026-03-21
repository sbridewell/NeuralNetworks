namespace Sde.MatrixDemo.Forms
{
    partial class MatrixSubtractionForm
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
            matrixTextBoxLeft = new Sde.MatrixDemo.Controls.MatrixTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            matrixTextBoxRight = new Sde.MatrixDemo.Controls.MatrixTextBox();
            label1 = new Label();
            label2 = new Label();
            labelResult = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // matrixTextBoxLeft
            // 
            matrixTextBoxLeft.Dock = DockStyle.Fill;
            matrixTextBoxLeft.Font = new Font("Consolas", 14F);
            matrixTextBoxLeft.Location = new Point(3, 3);
            matrixTextBoxLeft.Multiline = true;
            matrixTextBoxLeft.Name = "matrixTextBoxLeft";
            matrixTextBoxLeft.ScrollBars = ScrollBars.Vertical;
            matrixTextBoxLeft.Size = new Size(328, 217);
            matrixTextBoxLeft.TabIndex = 0;
            matrixTextBoxLeft.TextAlign = HorizontalAlignment.Center;
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
            tableLayoutPanel1.Controls.Add(matrixTextBoxRight, 2, 0);
            tableLayoutPanel1.Controls.Add(matrixTextBoxLeft, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 3, 0);
            tableLayoutPanel1.Controls.Add(labelResult, 4, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1059, 223);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // matrixTextBoxRight
            // 
            matrixTextBoxRight.Dock = DockStyle.Fill;
            matrixTextBoxRight.Font = new Font("Consolas", 14F);
            matrixTextBoxRight.Location = new Point(363, 3);
            matrixTextBoxRight.Multiline = true;
            matrixTextBoxRight.Name = "matrixTextBoxRight";
            matrixTextBoxRight.ScrollBars = ScrollBars.Vertical;
            matrixTextBoxRight.Size = new Size(328, 217);
            matrixTextBoxRight.TabIndex = 3;
            matrixTextBoxRight.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(337, 0);
            label1.Name = "label1";
            label1.Size = new Size(20, 223);
            label1.TabIndex = 1;
            label1.Text = "-";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(697, 0);
            label2.Name = "label2";
            label2.Size = new Size(25, 223);
            label2.TabIndex = 2;
            label2.Text = "=";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Dock = DockStyle.Fill;
            labelResult.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelResult.Location = new Point(728, 0);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(328, 223);
            labelResult.TabIndex = 4;
            labelResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MatrixSubtractionForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 247);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "MatrixSubtractionForm";
            Text = "Matrix subtraction";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.MatrixTextBox matrixTextBoxLeft;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Controls.MatrixTextBox matrixTextBoxRight;
        private Label label2;
        private Label labelResult;
    }
}