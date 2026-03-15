namespace Sde.VectorDemo.Winforms.Forms
{
    partial class MagnitudeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagnitudeForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            labelEuclidianResult = new Label();
            label4 = new Label();
            labelManhattanResult = new Label();
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
            tableLayoutPanel1.Controls.Add(labelManhattanResult, 0, 5);
            tableLayoutPanel1.Controls.Add(vectorTextBox1, 1, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(868, 469);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(862, 25);
            label1.TabIndex = 0;
            label1.Text = "The magnitude of a vector is the distance between the origin and its end point.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 25);
            label2.Name = "label2";
            label2.Size = new Size(311, 39);
            label2.TabIndex = 1;
            label2.Text = "Enter a vector to see its magnitudes";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label3, 2);
            label3.Location = new Point(3, 64);
            label3.Name = "label3";
            label3.Size = new Size(861, 100);
            label3.TabIndex = 2;
            label3.Text = resources.GetString("label3.Text");
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEuclidianResult
            // 
            labelEuclidianResult.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(labelEuclidianResult, 2);
            labelEuclidianResult.Dock = DockStyle.Fill;
            labelEuclidianResult.Location = new Point(3, 164);
            labelEuclidianResult.Name = "labelEuclidianResult";
            labelEuclidianResult.Size = new Size(862, 127);
            labelEuclidianResult.TabIndex = 3;
            labelEuclidianResult.Text = "labelEuclidianResult";
            labelEuclidianResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label4, 2);
            label4.Location = new Point(3, 291);
            label4.Name = "label4";
            label4.Size = new Size(843, 50);
            label4.TabIndex = 4;
            label4.Text = "The Manhattan magnitude (also known as the L1 magnitude) of a vector is calculated as the sum of each of the elements of the vector.";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelManhattanResult
            // 
            labelManhattanResult.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(labelManhattanResult, 2);
            labelManhattanResult.Dock = DockStyle.Fill;
            labelManhattanResult.Location = new Point(3, 341);
            labelManhattanResult.Name = "labelManhattanResult";
            labelManhattanResult.Size = new Size(862, 128);
            labelManhattanResult.TabIndex = 5;
            labelManhattanResult.Text = "labelManhattanResult";
            labelManhattanResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // vectorTextBox1
            // 
            vectorTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vectorTextBox1.Location = new Point(320, 28);
            vectorTextBox1.Name = "vectorTextBox1";
            vectorTextBox1.Size = new Size(545, 33);
            vectorTextBox1.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(-50, -50);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Something to tab to so that the vector control loses focus and validates its contents";
            button1.UseVisualStyleBackColor = true;
            // 
            // MagnitudeForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 493);
            Controls.Add(button1);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(5);
            Name = "MagnitudeForm";
            Text = "Magnitudes of vectors";
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
        private Label labelManhattanResult;
        private Controls.VectorTextBox vectorTextBox1;
        private Button button1;
    }
}