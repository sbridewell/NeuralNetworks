namespace MatrixDemo
{
    partial class MatrixDemoMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonAddition = new Button();
            buttonSubtraction = new Button();
            buttonVectorMultiplication = new Button();
            buttonHadamardProduct = new Button();
            buttonMatrixMultiplication = new Button();
            buttonTranspose = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(buttonAddition, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonSubtraction, 0, 1);
            tableLayoutPanel1.Controls.Add(buttonVectorMultiplication, 0, 2);
            tableLayoutPanel1.Controls.Add(buttonHadamardProduct, 0, 3);
            tableLayoutPanel1.Controls.Add(buttonMatrixMultiplication, 0, 4);
            tableLayoutPanel1.Controls.Add(buttonTranspose, 0, 5);
            tableLayoutPanel1.Location = new Point(19, 20);
            tableLayoutPanel1.Margin = new Padding(5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel1.Size = new Size(631, 274);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonAddition
            // 
            buttonAddition.Dock = DockStyle.Fill;
            buttonAddition.Location = new Point(5, 5);
            buttonAddition.Margin = new Padding(5);
            buttonAddition.Name = "buttonAddition";
            buttonAddition.Size = new Size(621, 38);
            buttonAddition.TabIndex = 0;
            buttonAddition.Text = "Addition";
            buttonAddition.UseVisualStyleBackColor = true;
            buttonAddition.Click += ButtonAddition_Click;
            // 
            // buttonSubtraction
            // 
            buttonSubtraction.AutoSize = true;
            buttonSubtraction.Dock = DockStyle.Fill;
            buttonSubtraction.Location = new Point(3, 51);
            buttonSubtraction.Name = "buttonSubtraction";
            buttonSubtraction.Size = new Size(625, 35);
            buttonSubtraction.TabIndex = 1;
            buttonSubtraction.Text = "Subtraction";
            buttonSubtraction.UseVisualStyleBackColor = true;
            buttonSubtraction.Click += ButtonSubtraction_Click;
            // 
            // buttonVectorMultiplication
            // 
            buttonVectorMultiplication.AutoSize = true;
            buttonVectorMultiplication.Dock = DockStyle.Fill;
            buttonVectorMultiplication.Location = new Point(3, 92);
            buttonVectorMultiplication.Name = "buttonVectorMultiplication";
            buttonVectorMultiplication.Size = new Size(625, 35);
            buttonVectorMultiplication.TabIndex = 2;
            buttonVectorMultiplication.Text = "Vector multiplication";
            buttonVectorMultiplication.UseVisualStyleBackColor = true;
            buttonVectorMultiplication.Click += ButtonVectorMultiplication_Click;
            // 
            // buttonHadamardProduct
            // 
            buttonHadamardProduct.AutoSize = true;
            buttonHadamardProduct.Dock = DockStyle.Fill;
            buttonHadamardProduct.Location = new Point(3, 133);
            buttonHadamardProduct.Name = "buttonHadamardProduct";
            buttonHadamardProduct.Size = new Size(625, 35);
            buttonHadamardProduct.TabIndex = 3;
            buttonHadamardProduct.Text = "Hadamard product (element-wise multiplication)";
            buttonHadamardProduct.UseVisualStyleBackColor = true;
            buttonHadamardProduct.Click += ButtonHadamardProduct_Click;
            // 
            // buttonMatrixMultiplication
            // 
            buttonMatrixMultiplication.AutoSize = true;
            buttonMatrixMultiplication.Dock = DockStyle.Fill;
            buttonMatrixMultiplication.Location = new Point(3, 174);
            buttonMatrixMultiplication.Name = "buttonMatrixMultiplication";
            buttonMatrixMultiplication.Size = new Size(625, 35);
            buttonMatrixMultiplication.TabIndex = 4;
            buttonMatrixMultiplication.Text = "Matrix multiplication";
            buttonMatrixMultiplication.UseVisualStyleBackColor = true;
            buttonMatrixMultiplication.Click += ButtonMatrixMultiplication_Click;
            // 
            // buttonTranspose
            // 
            buttonTranspose.AutoSize = true;
            buttonTranspose.Dock = DockStyle.Fill;
            buttonTranspose.Location = new Point(3, 215);
            buttonTranspose.Name = "buttonTranspose";
            buttonTranspose.Size = new Size(625, 35);
            buttonTranspose.TabIndex = 5;
            buttonTranspose.Text = "Transpose";
            buttonTranspose.UseVisualStyleBackColor = true;
            buttonTranspose.Click += ButtonTranspose_Click;
            // 
            // MatrixDemoMainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(669, 314);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "MatrixDemoMainForm";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonAddition;
        private Button buttonSubtraction;
        private Button buttonVectorMultiplication;
        private Button buttonHadamardProduct;
        private Button buttonMatrixMultiplication;
        private Button buttonTranspose;
    }
}
