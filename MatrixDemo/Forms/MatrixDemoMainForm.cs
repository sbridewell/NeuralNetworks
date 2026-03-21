// <copyright file="MatrixDemoMainForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace MatrixDemo
{
    using Sde.MatrixDemo.Forms;

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
            new MatrixAdditionForm().Show();
        }

        private void ButtonSubtraction_Click(object sender, EventArgs e)
        {
            new MatrixSubtractionForm().Show();
        }

        private void ButtonScalarMultiplication_Click(object sender, EventArgs e)
        {
            new ScalarMultiplicationForm().Show();
        }

        private void ButtonHadamardProduct_Click(object sender, EventArgs e)
        {
            new HadamardProductForm().Show();
        }

        private void ButtonMatrixMultiplication_Click(object sender, EventArgs e)
        {
            new MatrixMultiplicationForm().Show();
        }

        private void ButtonTranspose_Click(object sender, EventArgs e)
        {
            new TransposeForm().Show();
        }
    }
}
