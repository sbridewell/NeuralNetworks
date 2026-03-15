namespace Sde.VectorDemo.Winforms.Forms
{
    partial class SubtractionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubtractionForm));
            label1 = new Label();
            vectorTextBoxFirst = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            vectorTextBoxSecond = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            label2 = new Label();
            label3 = new Label();
            labelResult = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Location = new Point(8, 0);
            label1.Margin = new Padding(8, 0, 8, 0);
            label1.Name = "label1";
            label1.Size = new Size(898, 83);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // vectorTextBoxLeft
            // 
            vectorTextBoxFirst.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBoxFirst.Location = new Point(155, 88);
            vectorTextBoxFirst.Margin = new Padding(5);
            vectorTextBoxFirst.Name = "vectorTextBoxLeft";
            vectorTextBoxFirst.ShortcutsEnabled = false;
            vectorTextBoxFirst.Size = new Size(754, 33);
            vectorTextBoxFirst.TabIndex = 4;
            vectorTextBoxFirst.TextAlign = HorizontalAlignment.Center;
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
            tableLayoutPanel1.Location = new Point(14, 14);
            tableLayoutPanel1.Margin = new Padding(5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(909, 564);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // vectorTextBoxRight
            // 
            vectorTextBoxSecond.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBoxSecond.Location = new Point(155, 131);
            vectorTextBoxSecond.Margin = new Padding(5);
            vectorTextBoxSecond.Name = "vectorTextBoxRight";
            vectorTextBoxSecond.Size = new Size(754, 33);
            vectorTextBoxSecond.TabIndex = 5;
            vectorTextBoxSecond.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.Location = new Point(5, 83);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(140, 38);
            label2.TabIndex = 1;
            label2.Text = "First vector";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Location = new Point(5, 126);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(140, 38);
            label3.TabIndex = 2;
            label3.Text = "Second vector";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelResult
            // 
            labelResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(labelResult, 2);
            labelResult.Location = new Point(5, 169);
            labelResult.Margin = new Padding(5, 0, 5, 0);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(904, 395);
            labelResult.TabIndex = 3;
            labelResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SubtractionForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 591);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(5);
            Name = "SubtractionForm";
            Text = "Vector subtraction";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.VectorTextBox vectorTextBoxSecond;
        private Label label2;
        private Label label3;
        private Label labelResult;
        private Controls.VectorTextBox vectorTextBoxFirst;
    }
}