// <copyright file="MagnitudeForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms.Forms
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Form to demonstrate calculating the magnitude of a vector.
    /// </summary>
    public partial class MagnitudeForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagnitudeForm"/> class.
        /// </summary>
        public MagnitudeForm()
        {
            this.InitializeComponent();
            this.vectorTextBox1.Validated += this.VectorTextBox_Validated;
            this.vectorTextBox1.Value = new Vector(new double[] { 3, 4 });
            this.ShowResult();
        }

        private void VectorTextBox_Validated(object? sender, EventArgs e) => this.ShowResult();

        private void ShowResult()
        {
            var vector = this.vectorTextBox1.Value;
            if (vector.Dimension == 0)
            {
                return;
            }

            var euclidianMagnitude = vector.GetEuclidianMagnitude();
            this.labelEuclidianResult.Text = "The Euclidian magnitude of "
                + $"{vector.ToString()}{Environment.NewLine}"
                + $"is {euclidianMagnitude}";

            var manhattanMagnitude = vector.GetManhattanMagnitude();
            this.labelManhattanResult.Text = "The Manhattan magnitude of "
                + $"{vector.ToString()}{Environment.NewLine}"
                + $"is {manhattanMagnitude}";
        }
    }
}
