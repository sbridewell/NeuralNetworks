// <copyright file="TransposeForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.MatrixDemo.Forms
{
    /// <summary>
    /// Form to demonstrate transposing a matrix.
    /// </summary>
    public partial class TransposeForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransposeForm"/> class.
        /// </summary>
        public TransposeForm()
        {
            this.InitializeComponent();
            this.matrixTextBox1.Validated += this.MatrixTextBox1_Validated;
        }

        private void MatrixTextBox1_Validated(object? sender, EventArgs e)
        {
            var matrix = this.matrixTextBox1.Value;
            var result = matrix.Transpose();
            this.labelResult.Text = result.ToString();
        }
    }
}
