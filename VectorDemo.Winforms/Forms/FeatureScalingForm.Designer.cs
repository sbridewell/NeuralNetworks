namespace Sde.VectorDemo.Winforms.Forms
{
    partial class FeatureScalingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureScalingForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            labelEuclidianResult = new Label();
            label4 = new Label();
            labelMinMaxResult = new Label();
            label5 = new Label();
            labelZScoreResult = new Label();
            vectorTextBox1 = new Sde.VectorDemo.Winforms.Controls.VectorTextBox();
            button1 = new Button();
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
            tableLayoutPanel1.Controls.Add(labelEuclidianResult, 0, 3);
            tableLayoutPanel1.Controls.Add(label4, 0, 4);
            tableLayoutPanel1.Controls.Add(labelMinMaxResult, 0, 5);
            tableLayoutPanel1.Controls.Add(label5, 0, 6);
            tableLayoutPanel1.Controls.Add(labelZScoreResult, 0, 7);
            tableLayoutPanel1.Controls.Add(vectorTextBox1, 1, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(847, 617);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(841, 125);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 125);
            label2.Name = "label2";
            label2.Size = new Size(128, 25);
            label2.TabIndex = 1;
            label2.Text = "Enter a vector";
            // 
            // label3
            // 
            label3.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label3, 2);
            label3.Location = new Point(3, 164);
            label3.Name = "label3";
            label3.Size = new Size(837, 75);
            label3.TabIndex = 2;
            label3.Text = resources.GetString("label3.Text");
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEuclidianResult
            // 
            labelEuclidianResult.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(labelEuclidianResult, 2);
            labelEuclidianResult.Dock = DockStyle.Fill;
            labelEuclidianResult.Location = new Point(3, 239);
            labelEuclidianResult.Name = "labelEuclidianResult";
            labelEuclidianResult.Size = new Size(841, 84);
            labelEuclidianResult.TabIndex = 3;
            labelEuclidianResult.Text = "Euclidian result";
            labelEuclidianResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label4, 2);
            label4.Location = new Point(3, 323);
            label4.Name = "label4";
            label4.Size = new Size(824, 75);
            label4.TabIndex = 4;
            label4.Text = resources.GetString("label4.Text");
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelMinMaxResult
            // 
            labelMinMaxResult.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(labelMinMaxResult, 2);
            labelMinMaxResult.Dock = DockStyle.Fill;
            labelMinMaxResult.Location = new Point(3, 398);
            labelMinMaxResult.Name = "labelMinMaxResult";
            labelMinMaxResult.Size = new Size(841, 84);
            labelMinMaxResult.TabIndex = 5;
            labelMinMaxResult.Text = "min-max result";
            labelMinMaxResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label5, 2);
            label5.Location = new Point(3, 482);
            label5.Name = "label5";
            label5.Size = new Size(796, 50);
            label5.TabIndex = 6;
            label5.Text = "Z-score scaling, also known as standardisation, scales the elements of the vector so that their mean is zero and their standard deviation is 1.";
            // 
            // labelZScoreResult
            // 
            labelZScoreResult.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(labelZScoreResult, 2);
            labelZScoreResult.Dock = DockStyle.Fill;
            labelZScoreResult.Location = new Point(3, 532);
            labelZScoreResult.Name = "labelZScoreResult";
            labelZScoreResult.Size = new Size(841, 85);
            labelZScoreResult.TabIndex = 7;
            labelZScoreResult.Text = "z-score result";
            labelZScoreResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // vectorTextBox1
            // 
            vectorTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBox1.Location = new Point(137, 128);
            vectorTextBox1.Name = "vectorTextBox1";
            vectorTextBox1.Size = new Size(707, 33);
            vectorTextBox1.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(855, -30);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Something to tab to so that the text box loses focus";
            button1.UseVisualStyleBackColor = true;
            // 
            // FeatureScalingForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 641);
            Controls.Add(button1);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(5);
            Name = "FeatureScalingForm";
            Text = "Feature scaling";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label labelEuclidianResult;
        private Label label4;
        private Label labelMinMaxResult;
        private Label label5;
        private Label labelZScoreResult;
        private Controls.VectorTextBox vectorTextBox1;
        private Button button1;
    }
}