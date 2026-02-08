// <copyright file="TestResultsGrid.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System.ComponentModel;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// A data grid view for displaying the results of tests on a neural network,
    /// including the expected output and the actual output for each test case.
    /// </summary>
    public partial class TestResultsGrid : DataGridView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestResultsGrid"/> class.
        /// </summary>
        public TestResultsGrid()
        {
            this.InitializeComponent();
            this.Network = new NeuralNetworkSingleHiddenLayer();
        }

        /// <summary>
        /// Gets or sets the neural network whose test results are being displayed in the grid.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INeuralNetworkSingleHiddenLayer Network { get; set; }

        /// <summary>
        /// Populates the grid with the results of testing the neural network on the provided test inputs and expected outputs.
        /// </summary>
        /// <param name="testInputs">The inputs to the testing process.</param>
        /// <param name="expected">The expected outputs from the testing process.</param>
        public void Populate(double[][] testInputs, double[][] expected)
        {
            for (var i = 0; i < this.Network.InputSize; i++)
            {
                this.Columns.Add($"i{i}", $"i{i}");
            }

            for (var i = 0; i < this.Network.OutputSize; i++)
            {
                this.Columns.Add($"o{i}", $"o{i}");
            }

            for (var i = 0; i < this.Network.OutputSize; i++)
            {
                this.Columns.Add($"e{i}", $"e{i}");
            }

            this.ReadOnly = true;
            for (var i = 0; i < testInputs.Length; i++)
            {
                var a = testInputs[i][0];
                var b = testInputs[i][1];
                var c = testInputs[i][2];
                var predicted = this.Network.Predict(testInputs[i]);
                this.Rows.Add(
                    a,
                    b,
                    c,
                    $"{predicted[0]:F2}",
                    $"{predicted[1]:F2}",
                    $"{predicted[2]:F2}",
                    $"{predicted[3]:F2}",
                    $"{expected[i][0]:F2}",
                    $"{expected[i][0]:F2}",
                    $"{expected[i][1]:F2}",
                    $"{expected[i][1]:F2}");
            }

            this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}
