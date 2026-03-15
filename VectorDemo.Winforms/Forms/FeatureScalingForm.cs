// <copyright file="FeatureScalingForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms.Forms
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Form to demonstrate feature scaling of a vector.
    /// </summary>
    public partial class FeatureScalingForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureScalingForm"/> class.
        /// </summary>
        public FeatureScalingForm()
        {
            this.InitializeComponent();
            this.vectorTextBox1.Validated += this.VectorTextBox_Validated;
            this.vectorTextBox1.Value = new Vector(new double[] { 100, 101, 102, 103 });
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

            var euclidianScaled = vector.ScaleUsingEuclidian();
            this.labelEuclidianResult.Text = "The result of Euclidian scaling of "
                + $"{vector.ToString()}{Environment.NewLine}"
                + $"is {euclidianScaled}";

            var minMaxScaled = vector.ScaleUsingMinMax();
            this.labelMinMaxResult.Text = "The result of min-max scaling of "
                + $"{vector.ToString()}{Environment.NewLine}"
                + $"is {minMaxScaled}";

            var zScoresScaled = vector.ScaleUsingZScores();
            this.labelZScoreResult.Text = "The result of z-scores scaling of "
                + $"{vector.ToString()}{Environment.NewLine}"
                + $"is {zScoresScaled}";
        }
    }
}
