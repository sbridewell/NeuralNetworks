// <copyright file="SubtractionForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms.Forms
{
    using Sde.NeuralNetworks;

    /// <summary>
    /// Form to demonstrate vector subtraction.
    /// </summary>
    public partial class SubtractionForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionForm"/> class.
        /// </summary>
        public SubtractionForm()
        {
            this.InitializeComponent();
            this.vectorTextBoxFirst.Validated += this.VectorTextBox_Validated;
            this.vectorTextBoxSecond.Validated += this.VectorTextBox_Validated;
            this.vectorTextBoxFirst.Value = new Vector(new double[] { 1, 2, 3 });
            this.vectorTextBoxSecond.Value = new Vector(new double[] { 4, 5, 6 });
            this.ShowResult();
        }

        private void VectorTextBox_Validated(object? sender, EventArgs e) => this.ShowResult();

        private void ShowResult()
        {
            var first = this.vectorTextBoxFirst
                .Value;
            var second = this.vectorTextBoxSecond.Value;
            if (first.Dimension == 0 || second.Dimension == 0)
            {
                return;
            }

            if (first.Dimension != second.Dimension)
            {
                var msg = $"Cannot subtract these vectors because they have different dimensions.{Environment.NewLine}"
                    + $"First vector dimension: {first.Dimension}{Environment.NewLine}"
                    + $"Second vector dimension: {second.Dimension}";
                this.labelResult.Text = msg;
            }
            else
            {
                var result = first - second;
                this.labelResult.Text = $"{first.ToString()}{Environment.NewLine}"
                    + $"-{Environment.NewLine}"
                    + $"{second.ToString()}{Environment.NewLine}"
                    + $"={Environment.NewLine}"
                    + $"{result.ToString()}";
            }
        }
    }
}