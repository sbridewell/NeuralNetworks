namespace Sde.VectorDemo.Winforms.Forms
{
    partial class CosineSimilarityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CosineSimilarityForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            labelResult = new Label();
            vectorTextBoxFirst = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            vectorTextBoxSecond = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(labelResult, 0, 3);
            tableLayoutPanel1.Controls.Add(vectorTextBoxFirst, 1, 1);
            tableLayoutPanel1.Controls.Add(vectorTextBoxSecond, 1, 2);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(877, 415);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(871, 75);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 75);
            label2.Name = "label2";
            label2.Size = new Size(131, 39);
            label2.TabIndex = 1;
            label2.Text = "First vector";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 114);
            label3.Name = "label3";
            label3.Size = new Size(131, 39);
            label3.TabIndex = 2;
            label3.Text = "Second vector";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(labelResult, 2);
            labelResult.Dock = DockStyle.Fill;
            labelResult.Location = new Point(3, 153);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(871, 262);
            labelResult.TabIndex = 3;
            labelResult.Text = "labelResult";
            labelResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // vectorTextBoxFirst
            // 
            vectorTextBoxFirst.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBoxFirst.Location = new Point(140, 78);
            vectorTextBoxFirst.Name = "vectorTextBoxFirst";
            vectorTextBoxFirst.Size = new Size(734, 33);
            vectorTextBoxFirst.TabIndex = 4;
            // 
            // vectorTextBoxSecond
            // 
            vectorTextBoxSecond.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBoxSecond.Location = new Point(140, 117);
            vectorTextBoxSecond.Name = "vectorTextBoxSecond";
            vectorTextBoxSecond.Size = new Size(734, 33);
            vectorTextBoxSecond.TabIndex = 5;
            // 
            // CosineSimilarityForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 439);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(5);
            Name = "CosineSimilarityForm";
            Text = "CosineSimilarityForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label labelResult;
        private Controls.VectorTextBox vectorTextBoxFirst;
        private Controls.VectorTextBox vectorTextBoxSecond;
    }
}