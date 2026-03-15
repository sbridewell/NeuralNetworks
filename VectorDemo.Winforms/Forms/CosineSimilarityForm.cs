// <copyright file="CosineSimilarityForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms.Forms
{
    using Sde.NeuralNetworks;

    /// <summary>
    /// Form to demonstrate calculating the cosine similarity of two vectors.
    /// </summary>
    public partial class CosineSimilarityForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CosineSimilarityForm"/> class.
        /// </summary>
        public CosineSimilarityForm()
        {
            this.InitializeComponent();
            this.vectorTextBoxFirst.Validated += this.VectorTextBox_Validated;
            this.vectorTextBoxSecond.Validated += this.VectorTextBox_Validated;
            this.vectorTextBoxFirst.Value = new Vector(new double[] { 1, 2 });
            this.vectorTextBoxSecond.Value = new Vector(new double[] { 2, 4 });
            this.ShowResult();
        }

        private void VectorTextBox_Validated(object? sender, EventArgs e) => this.ShowResult();

        private void ShowResult()
        {
            var first = this.vectorTextBoxFirst.Value;
            var second = this.vectorTextBoxSecond.Value;
            if (first.Dimension == 0 || second.Dimension == 0)
            {
                return;
            }

            if (first.Dimension != second.Dimension)
            {
                var msg = $"Cannot calculate the cosine similarity of these vectors because they have different dimensions.{Environment.NewLine}"
                    + $"First vector dimension: {first.Dimension}{Environment.NewLine}"
                    + $"Second vector dimension: {second.Dimension}";
                this.labelResult.Text = msg;
            }
            else
            {
                var result = first.GetCosineSimilarity(second);
                this.labelResult.Text = $"Cosine similarity of {Environment.NewLine}"
                    + $"{first.ToString()}{Environment.NewLine}"
                    + $"and{Environment.NewLine}"
                    + $"{second.ToString()}{Environment.NewLine}"
                    + $"is{Environment.NewLine}"
                    + $"{result.ToString()}";
            }
        }
    }
}
