// <copyright file="MultiLayerNetworkVisualiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Controls
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Networks;

    /// <summary>
    /// Control that visualises an <see cref="IMultiLayerNetwork"/> instance.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// The control renders a simple layered view: input nodes (left), then one
    /// box per layer with neurons as circles.
    /// </item>
    /// <item>
    /// The control listens for <see cref="IMultiLayerNetwork.TrainingProgressChanged"/>
    /// and repaints on updates. Handlers marshal to the UI thread.
    /// </item>
    /// <item>
    /// Keep visuals inexpensive; heavy inspection should use a dedicated inspector
    /// UI.
    /// </item>
    /// </list>
    /// </remarks>
    public partial class MultiLayerNetworkVisualiser : UserControl
    {
        private readonly int margin = 12;
        private readonly int nodeRadius = 8;
        private IMultiLayerNetwork? network;
        private int colWidth;
        private int cols;
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
        private PointF[][]? neuronPositions;
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLayerNetworkVisualiser"/>
        /// class.
        /// </summary>
        public MultiLayerNetworkVisualiser()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Gets or sets the multi-layer network to visualise.
        /// Setting the property attaches event handlers and requests an initial
        /// repaint.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IMultiLayerNetwork? Network
        {
            get => this.network;
            set
            {
                if (ReferenceEquals(this.network, value))
                {
                    return;
                }

                if (this.network != null)
                {
                    this.network.TrainingProgressChanged
                        -= this.Network_TrainingProgressChanged;
                    this.network.TrainingStarted
                        -= this.Network_TrainingStarted;
                    this.network.TrainingCompleted
                        -= this.Network_TrainingCompleted;
                    this.network.TrainingStopped
                        -= this.Network_TrainingStopped;
                }

                this.network = value;

                if (this.network != null)
                {
                    this.network.TrainingProgressChanged
                        += this.Network_TrainingProgressChanged;
                    this.network.TrainingStarted
                        += this.Network_TrainingStarted;
                    this.network.TrainingCompleted
                        += this.Network_TrainingCompleted;
                    this.network.TrainingStopped
                        += this.Network_TrainingStopped;
                }

                this.RequestRedraw();
            }
        }

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO: break down into smaller methods
            base.OnPaint(e);
#if VERBOSE
            System.Diagnostics.Debug.WriteLine($"OnPaint called. Visible={this.Visible}, Size = {this.ClientSize}");
#endif

            var graphics = e.Graphics;
            graphics.Clear(SystemColors.Window);

            var net = this.network;
#if VERBOSE
            System.Diagnostics.Debug.WriteLine($"Network null? {net == null}");
#endif
            if (net == null)
            {
                using var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                };
                graphics.DrawString(
                    "No network",
                    this.Font,
                    Brushes.Gray,
                    this.ClientRectangle,
                    sf);
                return;
            }

            // Compute layout
            int inputCount = Math.Max(1, net.NumberOfInputs);
            var layers = net.Layers;
            int layerCount = layers.Count;

            // build neuron counts: start with inputs (left column), then
            // destination neuron count for each layer (Weights.RowCount)
            int[] neuronCounts = new int[layerCount + 1];
            neuronCounts[0] = inputCount;
            for (int i = 0; i < layerCount; i++)
            {
                var w = layers[i].Weights;
                neuronCounts[i + 1] = Math.Max(1, w.RowCount);
            }

            this.ComputeNeuronPositions(neuronCounts);
            this.DrawNeurons(graphics);

            // Draw connectors (simple lines) and optionally colour by weight magnitude (coarse).
            this.DrawAllConnectors(layerCount, neuronCounts, graphics);

            // Draw MSE summary in top-left
            var mseText = $"Output MSE: {net.OutputLayerMeanSquaredError:F4}";
            if (net.HiddenLayerMeanSquaredErrors.Count > 0)
            {
                var mses = string.Join(
                    ", ",
                    net.HiddenLayerMeanSquaredErrors.Select(m => m.ToString("F3")));
                mseText += $"  Hidden MSEs: {mses}";
            }

            graphics.DrawString(
                mseText,
                this.Font,
                Brushes.Black,
                new PointF(this.margin, 2));
        }

        #region event handlers

        private void Network_TrainingStarted(object? sender, EventArgs e)
            => this.RequestRedraw();

        private void Network_TrainingCompleted(object? sender, EventArgs e)
            => this.RequestRedraw();

        private void Network_TrainingStopped(object? sender, EventArgs e)
            => this.RequestRedraw();

        private void Network_TrainingProgressChanged(
            object? sender,
            TrainingProgressEventArgs e)
            => this.RequestRedraw();

        #endregion

        private void RequestRedraw()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)(() => this.Invalidate()));
            }
            else
            {
                this.Invalidate();
            }
        }

        private void DrawNeurons(Graphics graphics)
        {
            var positions = this.neuronPositions ?? Array.Empty<PointF[]>();
            for (var columnIndex = 0; columnIndex < this.cols; columnIndex++)
            {
                var column = columnIndex < positions.Length
                    ? positions[columnIndex]
                    : Array.Empty<PointF>();
                for (var row = 0; row < column.Length; row++)
                {
                    var pos = column[row];
                    var nodeRect = new RectangleF(
                        pos.X - this.nodeRadius,
                        pos.Y - this.nodeRadius,
                        this.nodeRadius * 2,
                        this.nodeRadius * 2);
                    graphics.FillEllipse(Brushes.White, nodeRect);
                    graphics.DrawEllipse(Pens.Black, nodeRect);
                }
            }
        }

        private void DrawAllConnectors(int layerCount, int[] neuronCounts, Graphics graphics)
        {
            var layers = this.network!.Layers;
            for (int layerIndex = 0; layerIndex < layerCount; layerIndex++)
            {
                // connectors from column layerIndex (sources) to column layerIndex+1 (destinations)
                var weightMatrix = layers[layerIndex].Weights; // rows = dest neurons, cols = sources
                int srcCount = neuronCounts[layerIndex];
                int dstCount = neuronCounts[layerIndex + 1];

#if DEBUG
                // Validate matrix dimensions match the visualiser's expected topology.
                // Treat mismatches as fatal to make issues obvious during development.
                if (weightMatrix.RowCount != dstCount || weightMatrix.ColumnCount != srcCount)
                {
                    throw new InvalidOperationException(
                        $"Layer {layerIndex}: weight matrix dimensions ({weightMatrix.RowCount}x{weightMatrix.ColumnCount}) " +
                        $"do not match expected connector dimensions ({dstCount}x{srcCount}).");
                }
#endif

                // Cache row vectors once per layer to avoid repeated allocations
                // in the inner loop.
                var rowVectors = weightMatrix.RowVectors;
#if VERBOSE
                System.Diagnostics.Debug.WriteLine($"Layer {layerIndex} weight size={weightMatrix.RowCount}x{weightMatrix.ColumnCount}");
#endif
                this.DrawConnectors(layerIndex, srcCount, dstCount, rowVectors, graphics);
            }
        }

        private void DrawConnectors(
            int layerIndex,
            int srcCount,
            int dstCount,
            Vector[] rowVectors,
            Graphics graphics)
        {
            var positions = this.neuronPositions ?? Array.Empty<PointF[]>();
            var fromColumn = layerIndex < positions.Length
                ? positions[layerIndex]
                : Array.Empty<PointF>();
            var toColumn = (layerIndex + 1) < positions.Length
                ? positions[layerIndex + 1]
                : Array.Empty<PointF>();
            for (var fromNeuron = 0; fromNeuron < srcCount; fromNeuron++)
            {
                var fromPosition = fromNeuron < fromColumn.Length
                    ? fromColumn[fromNeuron]
                    : PointF.Empty;
                for (var toNeuron = 0; toNeuron < dstCount; toNeuron++)
                {
                    var toPosition = toNeuron < toColumn.Length
                        ? toColumn[toNeuron]
                        : PointF.Empty;
                    var weight = rowVectors[toNeuron][fromNeuron];
                    this.DrawConnector(
                        fromPosition,
                        toPosition,
                        weight,
                        graphics);
                }
            }
        }

        private void DrawConnector(
            PointF fromPosition,
            PointF toPosition,
            double weight,
            Graphics graphics)
        {
            // If weight is NaN, draw a bold red connector
            if (double.IsNaN(weight))
            {
                using var pen = new Pen(Color.Red, 2);
                graphics.DrawLine(pen, fromPosition, toPosition);
#if VERBOSE
                            System.Diagnostics.Debug.WriteLine(
                                $"Warning: NaN weight detected at layer {layerIndex}, src {fromNeuron} -> dst {toNeuron}");
#endif
            }
            else
            {
                // Finite weight - draw with clearly visible colour / alpha so connectors are obvious
                // TODO: change the colour scheme so the differences in weights is more visible
                var intensity = Math.Min(1.0f, (float)(Math.Min(5.0, Math.Abs(weight)) / 5.0));
                using var pen = new Pen(Color.FromArgb(
                    (int)(50 + (intensity * 205)),
                    Color.DarkBlue))
                {
                    Width = 1.5f,
                };
                graphics.DrawLine(pen, fromPosition, toPosition);
            }
        }

        private void ComputeNeuronPositions(int[] neuronCounts)
        {
            this.cols = neuronCounts.Length;
            this.colWidth = Math.Max(
                80,
                (this.ClientSize.Width - (this.margin * 2)) / Math.Max(1, this.cols));

            var positions = new PointF[this.cols][];
            for (var columnIndex = 0; columnIndex < this.cols; columnIndex++)
            {
                var rows = Math.Max(0, neuronCounts[columnIndex]);
                positions[columnIndex] = new PointF[rows];
                var x = this.margin
                    + (columnIndex * this.colWidth)
                    + (this.colWidth / 2);
                for (var row = 0; row < rows; row++)
                {
                    var y
                        = this.margin
                        + (
                            (this.ClientSize.Height - (2 * this.margin))
                            * (row + 1)
                            / (rows + 1));
                    positions[columnIndex][row] = new PointF(x, y);
                }
            }

            this.neuronPositions = positions;
        }
    }
}