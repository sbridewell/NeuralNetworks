// <copyright file="ScalarMultiplicationForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.MatrixDemo.Forms
{
    /// <summary>
    /// Form to demonstrate multiplication of a matrix by a scalar.
    /// </summary>
    public partial class ScalarMultiplicationForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScalarMultiplicationForm"/> class.
        /// </summary>
        public ScalarMultiplicationForm()
        {
            this.InitializeComponent();
            this.matrixTextBox1.Validated += this.MatrixTextBox_Validated;
            this.numericUpDownScalar.Validated += this.MatrixTextBox_Validated;
        }

        private void MatrixTextBox_Validated(object? sender, EventArgs e)
        {
            var left = this.matrixTextBox1.Value;
            var right = this.numericUpDownScalar.Value;
            if (left.RowCount == 0 || left.ColumnCount == 0)
            {
                return;
            }

            var result = left * right;
            this.labelResult.Text = result.ToString();
        }
    }
}
