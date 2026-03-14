// <copyright file="TrainingDatum.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.OtherImplementations.SingleNeuron
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

#pragma warning disable SA1101 // Prefix local calls with this
    /// <summary>
    /// A single item in a set of data used to train a neural network.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TrainingDatum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDatum"/> class.
        /// </summary>
        /// <param name="inputs">The inputs to the network.</param>
        /// <param name="output">The expected output from the network for the given inputs.</param>
        public TrainingDatum(int[] inputs, float output)
        {
            Inputs = inputs;
            DesiredOutput = output;
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
            foreach (var input in Inputs)
            {
                sb.Append($"{input}, ");
            }

            sb.Append($"Output: {DesiredOutput}]");
            return sb.ToString();
        }
    }
#pragma warning restore SA1101 // Prefix local calls with this
}
