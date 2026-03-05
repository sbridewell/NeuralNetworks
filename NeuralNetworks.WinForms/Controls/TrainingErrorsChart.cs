// <copyright file="TrainingErrorsChart.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Controls
{
    using System.ComponentModel;
    using System.Windows.Forms.DataVisualization.Charting;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// User control which displays a chart of the training errors over time.
    /// </summary>
    public partial class TrainingErrorsChart : UserControl
    {
        private bool chartErrorSeriesInitialised = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingErrorsChart"/> class.
        /// </summary>
        public TrainingErrorsChart()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the neural network whose training errors are to be displayed on the chart.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INeuralNetwork? Network { get; set; }

        /// <summary>
        /// Updates the training errors chart with the latest error values from the neural network.
        /// </summary>
        public void UpdateErrorsChart()
        {
            var net = this.Network;
            if (net == null || this.errorsChart == null)
            {
                return;
            }

            // Initialise series the first time we have a valid network instance.
            if (!this.chartErrorSeriesInitialised)
            {
                this.errorsChart.Series.Clear();

                if (this.errorsChart.ChartAreas.Count == 0)
                {
                    return;
                }

                var area = this.errorsChart.ChartAreas[0];
                area.AxisX.Minimum = 0;
                area.AxisX.Title = "Epoch";
                area.AxisY.Title = "Error";

                // Clear any existing legends and add two dedicated legends:
                // - one for the hidden-layer mean squared error
                // - one for the output-layer mean squared error
                this.errorsChart.Legends.Clear();

                var hiddenLegend = new Legend("HiddenLegend")
                {
                    // leave Docking unset so Position takes full control
                    IsDockedInsideChartArea = false,
                };

                var outputLegend = new Legend("OutputLegend")
                {
                    // leave Docking unset so Position takes full control
                    IsDockedInsideChartArea = false,
                };

                // Place the two legends side-by-side above the chart area using absolute positions
                // Positions are percentages: (x, y, width, height).
                // Slight vertical offset (y) keeps them above the plotting area.
                hiddenLegend.Font = new Font(this.errorsChart.Font.FontFamily, 14);
                hiddenLegend.Position = new ElementPosition(5f, 0f, 45f, 8f);  // left legend
                outputLegend.Font = new Font(this.errorsChart.Font.FontFamily, 14);
                outputLegend.Position = new ElementPosition(50f, 0f, 45f, 8f); // right legend

                this.errorsChart.Legends.Add(hiddenLegend);
                this.errorsChart.Legends.Add(outputLegend);

                // Series that plot the aggregated mean-squared-errors.
                var seriesHiddenErrors = new Series("Hidden errors")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Legend = hiddenLegend.Name,
                    LegendText = "Hidden layer MSE",
                };
                this.errorsChart.Series.Add(seriesHiddenErrors);

                var seriesOutputErrors = new Series("Output errors")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Legend = outputLegend.Name,
                    LegendText = "Output layer MSE",
                };
                this.errorsChart.Series.Add(seriesOutputErrors);

                this.chartErrorSeriesInitialised = true;
            }

            // Append current errors if available.
            try
            {
                var epochX = net.CurrentIteration;
                var seriesName1 = "Output errors";
                if (this.errorsChart.Series.IndexOf(seriesName1) >= 0)
                {
                    this.errorsChart.Series[seriesName1].Points.AddXY(epochX, net.OutputLayerMeanSquaredError);
                }

                var seriesName2 = "Hidden errors";
                if (this.errorsChart.Series.IndexOf(seriesName2) >= 0)
                {
                    this.errorsChart.Series[seriesName2].Points.AddXY(epochX, net.HiddenLayerMeanSquaredError);
                }

                // Keep X axis range sensible while training.
                if (this.errorsChart.ChartAreas.Count > 0)
                {
                    var area = this.errorsChart.ChartAreas[0];
                    if (net.NumberOfIterations > 0)
                    {
                        // Keep the axis progressive during training.
                        area.AxisX.Maximum = net.CurrentIteration;
                    }

                    this.errorsChart.Invalidate();
                }
            }
            catch
            {
                // Defensive: ignore transient read errors while the network is being updated on another thread.
            }
        }

        /// <summary>
        /// Clears the training errors chart and forces re-initialisation on the next update.
        /// This should be called before starting a new training run to ensure that the chart
        /// is reset and ready to display the new training errors.
        /// </summary>
        public void ResetErrorsChart()
        {
            if (this.errorsChart != null && this.Network != null)
            {
                this.errorsChart.Series.Clear();
                this.chartErrorSeriesInitialised = false;
                if (this.errorsChart.ChartAreas.Count > 0)
                {
                    var area = this.errorsChart.ChartAreas[0];
                    area.AxisX.Minimum = 0;
                    area.AxisX.Maximum = this.Network.NumberOfIterations;
                    area.AxisX.Title = "Epoch";
                    area.AxisY.Title = "Error";
                    area.AxisX.TitleFont = new Font(this.errorsChart.Font.FontFamily, 14);
                    area.AxisY.TitleFont = new Font(this.errorsChart.Font.FontFamily, 14);
                    area.AxisY.IsStartedFromZero = false;
                }
            }
        }
    }
}
