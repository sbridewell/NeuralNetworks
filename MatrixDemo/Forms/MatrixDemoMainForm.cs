// <copyright file="MatrixDemoMainForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace MatrixDemo
{
    /// <summary>
    /// Main form in the matrix demo application.
    /// </summary>
    public partial class MatrixDemoMainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixDemoMainForm"/> class.
        /// </summary>
        public MatrixDemoMainForm()
        {
            this.InitializeComponent();
        }

        private void ButtonAddition_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Addition not implemented yet.");
        }

        private void ButtonSubtraction_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Subtraction not implemented yet.");
        }

        private void ButtonVectorMultiplication_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vector multiplication not implemented yet.");
        }

        private void ButtonHadamardProduct_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hadamard product not implemented yet.");
        }

        private void ButtonMatrixMultiplication_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Matrix multiplication not implemented yet.");
        }

        private void ButtonTranspose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Transpose not implemented yet.");
        }
    }
}
