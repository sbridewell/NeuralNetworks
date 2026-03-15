// <copyright file="ScalarMultiplicationForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms
{
    using Sde.NeuralNetworks;

    /// <summary>
    /// Form to demonstrate scalar multiplication of a vector.
    /// </summary>
    public partial class ScalarMultiplicationForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScalarMultiplicationForm"/> class.
        /// </summary>
        public ScalarMultiplicationForm()
        {
            this.InitializeComponent();
            this.vectorTextBox1.Validated += this.VectorTextBox_Validated;
            this.numericUpDown1.Validated += this.VectorTextBox_Validated;
            this.vectorTextBox1.Value = new Vector(new double[] { 1, 2, 3 });
            this.numericUpDown1.Value = 2;
            this.ShowResult();
        }

        private void VectorTextBox_Validated(object? sender, EventArgs e) => this.ShowResult();

        private void ShowResult()
        {
            var vector = this.vectorTextBox1.Value;
            var scalar = (double)this.numericUpDown1.Value;
            if (vector.Dimension == 0)
            {
                return;
            }

            var result = vector.Multiply(scalar);
            this.labelResult.Text = $"{vector.ToString()}{Environment.NewLine}"
                + $"*{Environment.NewLine}"
                + $"{scalar}{Environment.NewLine}"
                + $"={Environment.NewLine}"
                + $"{result.ToString()}";
        }
    }
}
