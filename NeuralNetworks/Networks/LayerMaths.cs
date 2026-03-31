// <copyright file="LayerMaths.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using Sde.NeuralNetworks.ActivationFunctionProviders;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Default implementation of <see cref="ILayerMaths"/> using the project's Matrix/Vector APIs.
    /// </summary>
    public sealed class LayerMaths : ILayerMaths
    {
        /// <inheritdoc/>
        public Vector ComputePreActivations(Matrix weights, Vector biases, Vector inputs)
        {
            return weights.Multiply(inputs).Add(biases);
        }

        /// <inheritdoc/>
        public Vector ApplyActivation(Vector preActivations, IActivationFunctionProvider activationProvider)
        {
            return activationProvider.CalculateActivation(preActivations);
        }

        /// <inheritdoc/>
        public Vector ComputeDeltas(Vector preActivations, Vector outputs, Vector expectedOutputs, IActivationFunctionProvider activationProvider)
        {
            return activationProvider
                .CalculateGradient(preActivations)
                .MultiplyElementWise(expectedOutputs.Subtract(outputs));
        }

        /// <inheritdoc/>
        public Matrix BuildWeightGradients(Vector inputs, Vector deltas)
        {
            var rows = new Vector[deltas.Dimension];
            for (var r = 0; r < deltas.Dimension; r++)
            {
                rows[r] = inputs.Multiply(deltas[r]);
            }

            return new Matrix(rows);
        }
    }
}
