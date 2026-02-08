// <copyright file="ActivationFunctionControl.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System.Windows.Forms.DataVisualization.Charting;
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// User control to display the graphs of an activation function and its gradient.
    /// </summary>
    public partial class ActivationFunctionControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationFunctionControl"/> class.
        /// </summary>
        public ActivationFunctionControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the activation function provider to display.
        /// </summary>
        /// <param name="provider">The provider to display.</param>
        public void SetActivationProvider(IActivationFunctionProvider provider)
        {
            this.ShowOnChart(provider.CalculateActivation, "Activation function", this.chartActivationFunction);
            this.ShowOnChart(provider.CalculateGradient, "Gradient function", this.chartGradientFunction);
        }

        private void ShowOnChart(Func<double, double> function, string legend, Chart chart, double maxAbsoluteOutput = 8.0)
        {
            // If chart is null or caller supplied an invalid threshold, do nothing.
            if (chart == null || maxAbsoluteOutput <= 0.0)
            {
                return;
            }

            const double start = -8.0;
            const double end = 8.0;
            const double step = 0.25;
            int count = (int)Math.Ceiling((end - start) / step) + 1;

            // Generate points, filter out any where the function result is NaN/Infinity
            // or exceeds the allowed absolute output range.
            var allPoints = Enumerable.Range(0, count)
                .Select(i =>
                {
                    var x = start + (i * step);
                    return new { Input = x, Output = function(x) };
                })
                .ToList();

            var points = allPoints
                .Where(p => !double.IsNaN(p.Output) && !double.IsInfinity(p.Output) && Math.Abs(p.Output) < (double)decimal.MaxValue)
                .ToList();

            // If no points remain after filtering, clear the chart and return.
            if (points.Count == 0)
            {
                chart.Series.Clear();
                chart.DataSource = null;
                chart.Invalidate();
                return;
            }

            // Configure the chart for a simple line graph and bind the filtered data.
            chart.Series.Clear();

            var series = new Series(legend)
            {
                ChartType = SeriesChartType.Line,
                XValueMember = "Input",
                YValueMembers = "Output",
                BorderWidth = 2,
            };

            chart.Series.Add(series);
            chart.DataSource = points;
            chart.DataBind();

            // Constrain the axes to the visible (filtered) range and draw the axes at x=0 and y=0.
            if (chart.ChartAreas.Count > 0)
            {
                var area = chart.ChartAreas[0];

                // Hide internal grid lines (both major and minor) for X and Y axes.
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisX.MinorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = false;
                area.AxisY.MinorGrid.Enabled = false;

                // Ensure the axis lines themselves remain visible.
                area.AxisX.LineWidth = Math.Max(1, area.AxisX.LineWidth);
                area.AxisY.LineWidth = Math.Max(1, area.AxisY.LineWidth);

                // Set ranges so 0 is within the chart area (activation controls use symmetric ranges).
                area.AxisX.Minimum = points.Min(p => p.Input);
                area.AxisX.Maximum = points.Max(p => p.Input);
                area.AxisY.Minimum = -maxAbsoluteOutput;
                area.AxisY.Maximum = maxAbsoluteOutput;

                // Draw the axes crossing at 0 (x axis crosses at y=0; y axis crosses at x=0).
                // The crossing coordinates are in the same units as the axis values.
                area.AxisX.Crossing = 0;
                area.AxisY.Crossing = 0;

                area.RecalculateAxesScale();
            }

            chart.Invalidate();
        }
    }
}
