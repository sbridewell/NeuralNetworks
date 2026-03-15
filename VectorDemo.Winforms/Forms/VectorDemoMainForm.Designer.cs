namespace Sde.VectorDemo.Winforms.Forms
{
    partial class VectorDemoMainForm
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
            buttonAddition = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonScaling = new Button();
            buttonCosineSimilarity = new Button();
            buttonMagnitude = new Button();
            buttonElementWiseMultiplication = new Button();
            buttonScalarMultiplication = new Button();
            buttonDotProduct = new Button();
            buttonSubtraction = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAddition
            // 
            buttonAddition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonAddition.AutoSize = true;
            buttonAddition.Location = new Point(3, 3);
            buttonAddition.Name = "buttonAddition";
            buttonAddition.Size = new Size(624, 35);
            buttonAddition.TabIndex = 0;
            buttonAddition.Text = "Vector addition (vector + vector = vector)";
            buttonAddition.UseVisualStyleBackColor = true;
            buttonAddition.Click += ButtonAddition_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(buttonScaling, 0, 7);
            tableLayoutPanel1.Controls.Add(buttonCosineSimilarity, 0, 6);
            tableLayoutPanel1.Controls.Add(buttonMagnitude, 0, 5);
            tableLayoutPanel1.Controls.Add(buttonElementWiseMultiplication, 0, 4);
            tableLayoutPanel1.Controls.Add(buttonScalarMultiplication, 0, 3);
            tableLayoutPanel1.Controls.Add(buttonDotProduct, 0, 2);
            tableLayoutPanel1.Controls.Add(buttonSubtraction, 0, 1);
            tableLayoutPanel1.Controls.Add(buttonAddition, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(630, 328);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonScaling
            // 
            buttonScaling.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonScaling.AutoSize = true;
            buttonScaling.Location = new Point(3, 290);
            buttonScaling.Name = "buttonScaling";
            buttonScaling.Size = new Size(624, 35);
            buttonScaling.TabIndex = 7;
            buttonScaling.Text = "Feature scaling (normalisation / standardisation)";
            buttonScaling.UseVisualStyleBackColor = true;
            buttonScaling.Click += ButtonScaling_Click;
            // 
            // buttonCosineSimilarity
            // 
            buttonCosineSimilarity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCosineSimilarity.AutoSize = true;
            buttonCosineSimilarity.Location = new Point(3, 249);
            buttonCosineSimilarity.Name = "buttonCosineSimilarity";
            buttonCosineSimilarity.Size = new Size(624, 35);
            buttonCosineSimilarity.TabIndex = 6;
            buttonCosineSimilarity.Text = "Cosine similarity (do two vectors point in the same direction?)";
            buttonCosineSimilarity.UseVisualStyleBackColor = true;
            buttonCosineSimilarity.Click += ButtonCosineSimilarity_Click;
            // 
            // buttonMagnitude
            // 
            buttonMagnitude.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonMagnitude.AutoSize = true;
            buttonMagnitude.Location = new Point(3, 208);
            buttonMagnitude.Name = "buttonMagnitude";
            buttonMagnitude.Size = new Size(624, 35);
            buttonMagnitude.TabIndex = 5;
            buttonMagnitude.Text = "Magnitudes (how long is the vector?)";
            buttonMagnitude.UseVisualStyleBackColor = true;
            buttonMagnitude.Click += ButtonMagnitude_Click;
            // 
            // buttonElementWiseMultiplication
            // 
            buttonElementWiseMultiplication.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonElementWiseMultiplication.AutoSize = true;
            buttonElementWiseMultiplication.Location = new Point(3, 167);
            buttonElementWiseMultiplication.Name = "buttonElementWiseMultiplication";
            buttonElementWiseMultiplication.Size = new Size(624, 35);
            buttonElementWiseMultiplication.TabIndex = 4;
            buttonElementWiseMultiplication.Text = "Element-wise multiplication (vector * vector = vector)";
            buttonElementWiseMultiplication.UseVisualStyleBackColor = true;
            buttonElementWiseMultiplication.Click += ButtonElementWiseMultiplication_Click;
            // 
            // buttonScalarMultiplication
            // 
            buttonScalarMultiplication.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonScalarMultiplication.AutoSize = true;
            buttonScalarMultiplication.Location = new Point(3, 126);
            buttonScalarMultiplication.Name = "buttonScalarMultiplication";
            buttonScalarMultiplication.Size = new Size(624, 35);
            buttonScalarMultiplication.TabIndex = 3;
            buttonScalarMultiplication.Text = "Scalar multiplication (vector * scalar = vector)";
            buttonScalarMultiplication.UseVisualStyleBackColor = true;
            buttonScalarMultiplication.Click += ButtonScalarMultiplication_Click;
            // 
            // buttonDotProduct
            // 
            buttonDotProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonDotProduct.AutoSize = true;
            buttonDotProduct.Location = new Point(3, 85);
            buttonDotProduct.Name = "buttonDotProduct";
            buttonDotProduct.Size = new Size(624, 35);
            buttonDotProduct.TabIndex = 2;
            buttonDotProduct.Text = "Dot product multiplication (vector * vector = scalar)";
            buttonDotProduct.UseVisualStyleBackColor = true;
            buttonDotProduct.Click += ButtonDotProduct_Click;
            // 
            // buttonSubtraction
            // 
            buttonSubtraction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonSubtraction.AutoSize = true;
            buttonSubtraction.Location = new Point(3, 44);
            buttonSubtraction.Name = "buttonSubtraction";
            buttonSubtraction.Size = new Size(624, 35);
            buttonSubtraction.TabIndex = 1;
            buttonSubtraction.Text = "Vector subtraction (vector - vector = vector)";
            buttonSubtraction.UseVisualStyleBackColor = true;
            buttonSubtraction.Click += ButtonSubtraction_Click;
            // 
            // VectorDemoMainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 352);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(5);
            Name = "VectorDemoMainForm";
            Text = "Linear algebra - vectors";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonAddition;
        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonSubtraction;
        private Button buttonDotProduct;
        private Button buttonScaling;
        private Button buttonCosineSimilarity;
        private Button buttonMagnitude;
        private Button buttonElementWiseMultiplication;
        private Button buttonScalarMultiplication;
    }
}
