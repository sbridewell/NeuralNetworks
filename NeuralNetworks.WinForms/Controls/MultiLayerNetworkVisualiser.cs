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
        private IMultiLayerNetwork? network;

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
            var g = e.Graphics;
            g.Clear(SystemColors.Window);

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
                g.DrawString(
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

            int cols = neuronCounts.Length;
            int margin = 12;
            int colWidth = Math.Max(
                80,
                (this.ClientSize.Width - (margin * 2)) / Math.Max(1, cols));
            int nodeRadius = 8;

            for (int col = 0; col < cols; col++)
            {
                int x = margin + (col * colWidth) + (colWidth / 2);
                int rows = neuronCounts[col];
                for (int row = 0; row < rows; row++)
                {
                    int y
                        = margin
                        + (
                            (this.ClientSize.Height - (2 * margin))
                            * (row + 1)
                            / (rows + 1));
                    var nodeRect = new Rectangle(
                        x - nodeRadius,
                        y - nodeRadius,
                        nodeRadius * 2,
                        nodeRadius * 2);
                    g.FillEllipse(Brushes.White, nodeRect);
                    g.DrawEllipse(Pens.Black, nodeRect);
                }
            }

            // Draw connectors (simple lines) and optionally colour by weight magnitude (coarse).
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
                for (int src = 0; src < srcCount; src++)
                {
                    int sx = margin + (layerIndex * colWidth) + (colWidth / 2);
                    int sy = margin + ((this.ClientSize.Height - (2 * margin)) * (src + 1) / (srcCount + 1));
                    for (int dst = 0; dst < dstCount; dst++)
                    {
                        int dx = margin + ((layerIndex + 1) * colWidth) + (colWidth / 2);
                        int dy = margin + ((this.ClientSize.Height - (2 * margin)) * (dst + 1) / (dstCount + 1));

                        // weight value at row=dst, col=src if dimensions match; guard access
                        var w = rowVectors[dst][src];

                        // If weight is NaN, draw a bold red connector and log once
                        if (double.IsNaN(w))
                        {
                            using var pen = new Pen(Color.Red, 2);
                            g.DrawLine(pen, sx, sy, dx, dy);
#if VERBOSE
                            System.Diagnostics.Debug.WriteLine(
                                $"Warning: NaN weight detected at layer {layerIndex}, src {src} -> dst {dst}");
#endif
                        }
                        else
                        {
                            // Finite weight - draw with clearly visible colour / alpha so connectors are obvious
                            // TODO: change the colour scheme so the differences in weights is more visible
                            var intensity = Math.Min(1.0f, (float)(Math.Min(5.0, Math.Abs(w)) / 5.0));
                            using var pen = new Pen(Color.FromArgb(
                                (int)(50 + (intensity * 205)),
                                Color.DarkBlue))
                            {
                                Width = 1.5f,
                            };
                            g.DrawLine(pen, sx, sy, dx, dy);
                        }
                    }
                }
            }

            // Draw MSE summary in top-left
            var mseText = $"Output MSE: {net.OutputLayerMeanSquaredError:F4}";
            if (net.HiddenLayerMeanSquaredErrors.Count > 0)
            {
                var mses = string.Join(
                    ", ",
                    net.HiddenLayerMeanSquaredErrors.Select(m => m.ToString("F3")));
                mseText += $"  Hidden MSEs: {mses}";
            }

            g.DrawString(mseText, this.Font, Brushes.Black, new PointF(margin, 2));
        }

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
    }
}