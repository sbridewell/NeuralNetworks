// <copyright file="VisualisationForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Forms
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Windows.Forms;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// A form which displays a visual representation of the network's structure and
    /// other information about it.
    /// </summary>
    public partial class VisualisationForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualisationForm"/> class.
        /// </summary>
        public VisualisationForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the neural network to visualise.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INeuralNetwork Network
        {
            get
            {
                return this.networkVisualiser1.Network!;
            }

            set
            {
                this.networkVisualiser1.Network = value;
                this.trainingErrorsChart1.Network = value;
                this.testResultsGrid1.Network = value;
                this.predictionControl1.Network = value;
            }
        }

        /// <summary>
        /// Disables user input.
        /// </summary>
        public void DisableUserInput()
        {
            this.predictionControl1.DisableUserInput();
        }

        /// <summary>
        /// Enables user input.
        /// </summary>
        public void EnableUserInput()
        {
            this.predictionControl1.EnableUserInput();
        }

        /// <summary>
        /// Invalidates the visual representation of the control, causing a redraw on the next paint operation.
        /// </summary>
        public new void Invalidate()
        {
            this.networkVisualiser1.Invalidate();
            this.trainingErrorsChart1.Invalidate();
        }

        /// <summary>
        /// Updates the training errors chart with the latest error values from the neural network.
        /// </summary>
        public void UpdateErrorsChart()
        {
            this.trainingErrorsChart1.UpdateErrorsChart();
        }

        /// <summary>
        /// Clears the training errors chart, removing all data points and resetting it to its initial state.
        /// </summary>
        public void ResetErrorsChart()
        {
            this.trainingErrorsChart1.ResetErrorsChart();
        }

        /// <summary>
        /// Removes all the rows and columns from the test results grid.
        /// </summary>
        public void ClearTestResults()
        {
            this.testResultsGrid1.Rows.Clear();
            this.testResultsGrid1.Columns.Clear();
        }

        /// <summary>
        /// Updates the test results grid with the results of testing the trained network.
        /// </summary>
        /// <param name="testInputs">Inputs to the test.</param>
        /// <param name="expected">
        /// The expected outputs corresponding to the supplied inputs.
        /// </param>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design")]
        [SuppressMessage(
            "StyleCop.CSharp.SpacingRules",
            "SA1011:Closing square brackets should be spaced correctly",
            Justification = "Contradicts SA1018 Nullable type symbol should not be preceded by a space")]
        public void DisplayTestResults(double[][]? testInputs, double[][]? expected)
        {
            this.testResultsGrid1.Populate(testInputs, expected);
        }

        /// <summary>
        /// Displays a JSON representation of the network's properties.
        /// </summary>
        public void DisplayNetworkJson()
        {
            var json = JsonSerializer.Serialize(this.Network, new JsonSerializerOptions { WriteIndented = true });
            this.textBoxJson.Text = json;
        }
    }
}
