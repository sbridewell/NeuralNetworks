// <copyright file="NetworkVisualiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System.ComponentModel;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// Provides a visualiser for drawingg simple network diagrams, including circles and lines.
    /// </summary>
    public partial class NetworkVisualiser : UserControl
    {
        private static readonly int NodeRadius = 30;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkVisualiser"/> class.
        /// </summary>
        public NetworkVisualiser()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Gets or sets the network to visualise.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INeuralNetwork? Network { get; set; }

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Network == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            var inputLayerX = this.Width / 4;
            var hiddenLayerX = this.Width / 2;
            var outputLayerX = this.Width * 3 / 4;
            var inputLayerYs = new int[this.Network.InputSize];
            var hiddenLayerYs = new int[this.Network.HiddenSize];
            var outputLayerYs = new int[this.Network.OutputSize];

            var normalisedHiddenBiases = Normaliser.Normalise(255, -255, this.Network.HiddenBiases);
            var normalisedInputToHiddenWeights = Normaliser.Normalise(255, -255, this.Network.InputToHiddenWeights);
            var normalisedOutputBiases = Normaliser.Normalise(255, -255, this.Network.OutputBiases);
            var normalisedHiddenToOutputWeights = Normaliser.Normalise(255, -255, this.Network.HiddenToOutputWeights);

            for (var i = 0; i < this.Network.InputSize; i++)
            {
                // There are no neurons in the inputs layer, just inputs.
                inputLayerYs[i] = this.Height / (this.Network.InputSize + 1) * (i + 1);
            }

            for (var h = 0; h < this.Network.HiddenSize; h++)
            {
                hiddenLayerYs[h] = this.Height / (this.Network.HiddenSize + 1) * (h + 1);
                var colour = GetColour(normalisedHiddenBiases[h]);
                DrawNode(g, hiddenLayerX, hiddenLayerYs[h], colour);
            }

            for (var o = 0; o < this.Network.OutputSize; o++)
            {
                outputLayerYs[o] = this.Height / (this.Network.OutputSize + 1) * (o + 1);
                var colour = GetColour(normalisedOutputBiases[o]);
                DrawNode(g, outputLayerX, outputLayerYs[o], colour);
            }

            for (var i = 0; i < this.Network.InputSize; i++)
            {
                for (var h = 0; h < this.Network.HiddenSize; h++)
                {
                    var from = new Point(inputLayerX, inputLayerYs[i]);
                    var to = new Point(hiddenLayerX, hiddenLayerYs[h]);
                    var colour = GetColour(normalisedInputToHiddenWeights[i][h]);
                    DrawLine(g, from, to, colour);
                }
            }

            for (var h = 0; h < this.Network.HiddenSize; h++)
            {
                for (var o = 0; o < this.Network.OutputSize; o++)
                {
                    var from = new Point(hiddenLayerX, hiddenLayerYs[h]);
                    var to = new Point(outputLayerX, outputLayerYs[o]);
                    var colour = GetColour(normalisedHiddenToOutputWeights[h][o]);
                    DrawLine(g, from, to, colour);
                }
            }
        }

        /// <summary>
        /// Gets a colour representing a normalised value.
        /// </summary>
        /// <param name="normalisedValue">The value represented by the colour.</param>
        /// <returns>
        /// Red represents a large positive value, blue a large negative value, and black
        /// a value close to zero.
        /// </returns>
        private static Color GetColour(int normalisedValue)
        {
            return normalisedValue > 0 ?
                Color.FromArgb(Math.Abs(normalisedValue), 0, 0) :
                Color.FromArgb(0, 0, Math.Abs(normalisedValue));
        }

        /// <summary>
        /// Drawns a node (neuron) onto the control.
        /// </summary>
        /// <param name="g">Graphics object.</param>
        /// <param name="layerX">
        /// X coordinate for the layer which contains the node.
        /// </param>
        /// <param name="y">
        /// Y coordinate for the node.
        /// </param>
        /// <param name="colur">
        /// The colour of the node, representing its bias.
        /// Red indicates a high vpositive value, blue a high negative value,
        /// and black a value close to zero.
        /// </param>
        private static void DrawNode(Graphics g, int layerX, int y, Color colur)
        {
            var centre = new Point(layerX, y);
            using var brush = new SolidBrush(colur);
            g.FillEllipse(brush, centre.X - NodeRadius, centre.Y - NodeRadius, NodeRadius * 2, NodeRadius * 2);
        }

        /// <summary>
        /// Drawas a line connecting two nodes, representing the weight.
        /// </summary>
        /// <param name="g">Graphics object.</param>
        /// <param name="from">Coordinates of the "from" node.</param>
        /// <param name="to">Coordinates of the "to" node.</param>
        /// <param name="colour">
        /// Colour representing the weight.
        /// Red indicates a high vpositive value, blue a high negative value,
        /// and black a value close to zero.
        /// </param>
        private static void DrawLine(Graphics g, Point from, Point to, Color colour)
        {
            using var pen = new Pen(colour, 5);
            g.DrawLine(pen, from, to);
        }
    }
}
