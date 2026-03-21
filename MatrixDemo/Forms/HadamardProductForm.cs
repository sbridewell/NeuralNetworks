// <copyright file="HadamardProductForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>


namespace Sde.MatrixDemo.Forms
{
    /// <summary>
    /// Form to demonstrate the Hadamard product (element-wise multiplication) of two matrices.
    /// </summary>
    public partial class HadamardProductForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HadamardProductForm"/> class.
        /// </summary>
        public HadamardProductForm()
        {
            this.InitializeComponent();
            this.matrixTextBoxLeft.Validated += this.MatrixTextBox_Validated;
            this.matrixTextBoxRight.Validated += this.MatrixTextBox_Validated;
        }

        private void MatrixTextBox_Validated(object? sender, EventArgs e)
        {
            var left = this.matrixTextBoxLeft.Value;
            var right = this.matrixTextBoxRight.Value;
            if (left.RowCount == 0 || right.ColumnCount == 0)
            {
                return;
            }

            var result = left.CalculateHadamardProduct(right);
            this.labelResult.Text = result.ToString();
        }
    }
}
