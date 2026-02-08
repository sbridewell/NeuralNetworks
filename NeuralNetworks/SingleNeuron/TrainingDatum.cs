// <copyright file="TrainingDatum.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.SingleNeuron
{
    using System.Text;

    /// <summary>
    /// A single item in a set of data used to train a neural network.
    /// </summary>
    public class TrainingDatum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDatum"/> class.
        /// </summary>
        /// <param name="inputs">The inputs to the network.</param>
        /// <param name="output">The expected output from the network for the given inputs.</param>
        public TrainingDatum(int[] inputs, float output)
        {
            this.Inputs = inputs;
            this.DesiredOutput = output;
        }

        /// <summary>
        /// Gets or sets the inputs to the network.
        /// </summary>
        public int[] Inputs { get; set; }

        /// <summary>
        /// Gets or sets the expected output from the network for the given inputs..
        /// </summary>
        public float DesiredOutput { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Inputs: [");
            foreach (var input in this.Inputs)
            {
                sb.Append($"{input}, ");
            }

            sb.Append($"Output: {this.DesiredOutput}]");
            return sb.ToString();
        }
    }
}
