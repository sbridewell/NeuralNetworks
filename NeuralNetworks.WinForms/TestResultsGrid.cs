// <copyright file="TestResultsGrid.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
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
            this.Network = new NeuralNetwork();
        }

        /// <summary>
        /// Gets or sets the neural network whose test results are being displayed in the grid.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INeuralNetwork Network { get; set; }

        /// <summary>
        /// Populates the grid with the results of testing the neural network on the provided
        /// test inputs and expected outputs.
        /// </summary>
        /// <param name="testInputs">The inputs to the testing process.</param>
        /// <param name="expected">The expected outputs from the testing process.</param>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design")]
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
                var predicted = this.Network.Predict(testInputs[i]);
                var row = new DataGridViewRow();
                foreach (var input in testInputs[i])
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = $"{input:F2}" });
                }

                foreach (var output in predicted)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = $"{output:F2}" });
                }

                foreach (var expectedOutput in expected[i])
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = $"{expectedOutput:F2}" });
                }

                this.Rows.Add(row);
            }

            this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}
