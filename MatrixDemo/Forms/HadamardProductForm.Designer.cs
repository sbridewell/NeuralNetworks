namespace Sde.MatrixDemo.Forms
{
    partial class HadamardProductForm
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
            matrixTextBoxLeft = new Sde.MatrixDemo.Controls.MatrixTextBox();
            label1 = new Label();
            matrixTextBoxRight = new Sde.MatrixDemo.Controls.MatrixTextBox();
            label2 = new Label();
            labelResult = new Label();
            tableLayoutPanel1.SuspendLayout();
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
            tableLayoutPanel1.Controls.Add(matrixTextBoxLeft, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(matrixTextBoxRight, 2, 0);
            tableLayoutPanel1.Controls.Add(label2, 3, 0);
            tableLayoutPanel1.Controls.Add(labelResult, 4, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(684, 186);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // matrixTextBoxLeft
            // 
            matrixTextBoxLeft.Dock = DockStyle.Fill;
            matrixTextBoxLeft.Font = new Font("Consolas", 14F);
            matrixTextBoxLeft.Location = new Point(3, 3);
            matrixTextBoxLeft.Multiline = true;
            matrixTextBoxLeft.Name = "matrixTextBoxLeft";
            matrixTextBoxLeft.ScrollBars = ScrollBars.Vertical;
            matrixTextBoxLeft.Size = new Size(203, 180);
            matrixTextBoxLeft.TabIndex = 0;
            matrixTextBoxLeft.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(212, 0);
            label1.Name = "label1";
            label1.Size = new Size(20, 25);
            label1.TabIndex = 1;
            label1.Text = "*";
            // 
            // matrixTextBoxRight
            // 
            matrixTextBoxRight.Dock = DockStyle.Fill;
            matrixTextBoxRight.Font = new Font("Consolas", 14F);
            matrixTextBoxRight.Location = new Point(238, 3);
            matrixTextBoxRight.Multiline = true;
            matrixTextBoxRight.Name = "matrixTextBoxRight";
            matrixTextBoxRight.ScrollBars = ScrollBars.Vertical;
            matrixTextBoxRight.Size = new Size(203, 180);
            matrixTextBoxRight.TabIndex = 2;
            matrixTextBoxRight.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(447, 0);
            label2.Name = "label2";
            label2.Size = new Size(25, 25);
            label2.TabIndex = 3;
            label2.Text = "=";
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Dock = DockStyle.Fill;
            labelResult.Location = new Point(478, 0);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(203, 186);
            labelResult.TabIndex = 4;
            labelResult.TextAlign = ContentAlignment.TopCenter;
            // 
            // HadamardProductForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 210);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 5, 5, 5);
            Name = "HadamardProductForm";
            Text = "Hadamard product";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Controls.MatrixTextBox matrixTextBoxLeft;
        private Label label1;
        private Controls.MatrixTextBox matrixTextBoxRight;
        private Label label2;
        private Label labelResult;
    }
}