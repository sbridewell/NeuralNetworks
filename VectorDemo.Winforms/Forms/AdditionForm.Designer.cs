namespace Sde.VectorDemo.Winforms.Forms
{
    partial class AdditionForm
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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            vectorTextBoxSecond = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            label2 = new Label();
            label3 = new Label();
            labelResult = new Label();
            vectorTextBoxFirst = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Location = new Point(5, 0);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(887, 50);
            label1.TabIndex = 0;
            label1.Text = "The result of adding two vectors together is a vector of the same dimension whose elements are the sum of the corresponding elements of the two vectors.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(vectorTextBoxSecond, 1, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(labelResult, 0, 3);
            tableLayoutPanel1.Controls.Add(vectorTextBoxFirst, 1, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(894, 460);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // vectorTextBoxSecond
            // 
            vectorTextBoxSecond.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBoxSecond.Location = new Point(153, 92);
            vectorTextBoxSecond.Name = "vectorTextBoxSecond";
            vectorTextBoxSecond.Size = new Size(741, 33);
            vectorTextBoxSecond.TabIndex = 5;
            vectorTextBoxSecond.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 50);
            label2.Name = "label2";
            label2.Size = new Size(144, 39);
            label2.TabIndex = 1;
            label2.Text = "First vector";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 89);
            label3.Name = "label3";
            label3.Size = new Size(144, 39);
            label3.TabIndex = 2;
            label3.Text = "Second vector";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelResult
            // 
            labelResult.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(labelResult, 2);
            labelResult.Location = new Point(3, 128);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(891, 332);
            labelResult.TabIndex = 3;
            labelResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // vectorTextBoxFirst
            // 
            vectorTextBoxFirst.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBoxFirst.Location = new Point(153, 53);
            vectorTextBoxFirst.Name = "vectorTextBoxFirst";
            vectorTextBoxFirst.ShortcutsEnabled = false;
            vectorTextBoxFirst.Size = new Size(741, 33);
            vectorTextBoxFirst.TabIndex = 4;
            vectorTextBoxFirst.TextAlign = HorizontalAlignment.Center;
            // 
            // AdditionForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(918, 484);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(5);
            Name = "AdditionForm";
            Text = "Vector addition";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label2;
        private Label label3;
        private Label labelResult;
        private Controls.VectorTextBox vectorTextBoxFirst;
        private Controls.VectorTextBox vectorTextBoxSecond;
    }
}