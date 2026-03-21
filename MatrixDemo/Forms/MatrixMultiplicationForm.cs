// <copyright file="MatrixMultiplicationForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.MatrixDemo.Forms
{
    /// <summary>
    /// Form to demonstrate matrix multiplication.
    /// </summary>
    public partial class MatrixMultiplicationForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixMultiplicationForm"/> class.
        /// </summary>
        public MatrixMultiplicationForm()
        {
            this.InitializeComponent();
            this.matrixTextBoxLeft.Validated += this.MatrixTextBox_Validated;
            this.matrixTextBoxRight.Validated += this.MatrixTextBox_Validated;
        }

        private void MatrixTextBox_Validated(object? sender, EventArgs e)
        {
            var left = this.matrixTextBoxLeft.Value;
            var right = this.matrixTextBoxRight.Value;
            if (left.RowCount == 0 || right.RowCount == 0)
            {
                this.labelResult.Text = string.Empty;
                return;
            }

            try
            {
                var result = left.Multiply(right);
                this.labelResult.Text = result.ToString();
            }
            catch (Exception ex)
            {
                this.labelResult.Text = $"Error: {ex.Message}";
            }
        }
    }
}
