// <copyright file="MatrixSubtractionForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.MatrixDemo.Forms
{
    /// <summary>
    /// Form to demonstrate adding two matrices together.
    /// </summary>
    public partial class MatrixSubtractionForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixSubtractionForm"/> class.
        /// </summary>
        public MatrixSubtractionForm()
        {
            this.InitializeComponent();
            this.matrixTextBoxLeft.Validated += this.MatrixTextBox_Validated;
            this.matrixTextBoxRight.Validated += this.MatrixTextBox_Validated;
        }

        private void MatrixTextBox_Validated(object? sender, EventArgs e)
        {
            var left = this.matrixTextBoxLeft.Value;
            var right = this.matrixTextBoxRight.Value;
            if (left.RowCount != right.RowCount || left.ColumnCount != right.ColumnCount)
            {
                var msg = "Cannot subtract these matrices because they have different dimensions." + Environment.NewLine
                    + $"Left matrix dimensions: {left.RowCount}x{left.ColumnCount}" + Environment.NewLine
                    + $"Right matrix dimensions: {right.RowCount}x{right.ColumnCount}";
                this.labelResult.Text = msg;
            }
            else
            {
                var result = left - right;
                this.labelResult.Text = result.ToString();
            }
        }
    }
}
