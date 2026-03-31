// <copyright file="ILayerMaths.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using Sde.NeuralNetworks.ActivationFunctionProviders;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Abstraction for the pure linear / activation / gradient math required by a layer.
    /// Extracted so the computations can be unit tested and replaced for performance benchmarking.
    /// </summary>
    public interface ILayerMaths
    {
        /// <summary>
        /// Computes the linear pre-activation values for a layer: z = W * x + b.
        /// The resulting vector contains one element per destination neuron (one row of <paramref name="weights"/>).
        /// </summary>
        /// <param name="weights">
        /// The weight matrix. Rows correspond to destination neurons (outputs) and columns to source values (inputs).
        /// </param>
        /// <param name="biases">
        /// The bias vector. Its length must equal the number of rows of <paramref name="weights"/>.
        /// </param>
        /// <param name="inputs">
        /// The input vector. Its length must equal the number of columns of <paramref name="weights"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector"/> containing the pre-activation values (one element per destination neuron).
        /// </returns>
        Vector ComputePreActivations(Matrix weights, Vector biases, Vector inputs);

        /// <summary>
        /// Applies the supplied activation function element-wise to the provided pre-activation vector.
        /// </summary>
        /// <param name="preActivations">The vector of pre-activation values to which the activation function will be applied.</param>
        /// <param name="activationProvider">Provider that supplies the activation function used to transform each element.</param>
        /// <returns>
        /// A <see cref="Vector"/> containing the activated outputs (same length as <paramref name="preActivations"/>).
        /// </returns>
        Vector ApplyActivation(Vector preActivations, IActivationFunctionProvider activationProvider);

        /// <summary>
        /// Computes the per-neuron local gradients (deltas) for a layer using the supplied pre-activations,
        /// outputs and expected outputs. The typical formula is:
        /// <c>delta[i] = (expectedOutputs[i] - outputs[i]) * f'(preActivations[i])</c>.
        /// </summary>
        /// <param name="preActivations">Pre-activation values (z) for each neuron.</param>
        /// <param name="outputs">Activated outputs produced from <paramref name="preActivations"/>.</param>
        /// <param name="expectedOutputs">Target outputs used to compute the error term for each neuron.</param>
        /// <param name="activationProvider">Provider used to evaluate the derivative of the activation function.</param>
        /// <returns>
        /// A <see cref="Vector"/> of deltas (local gradients), one element per neuron.
        /// </returns>
        Vector ComputeDeltas(Vector preActivations, Vector outputs, Vector expectedOutputs, IActivationFunctionProvider activationProvider);

        /// <summary>
        /// Builds the weight-gradient matrix from the supplied input vector and per-neuron deltas.
        /// Each row <c>r</c> of the returned matrix contains the gradient for the weights feeding neuron <c>r</c>
        /// and is computed as the input vector scaled by <c>deltas[r]</c>.
        /// </summary>
        /// <param name="inputs">The input vector to the layer (length = number of weight columns).</param>
        /// <param name="deltas">Per-neuron deltas (length = number of weight rows).</param>
        /// <returns>
        /// A <see cref="Matrix"/> where each row <c>r</c> equals <c>inputs * deltas[r]</c>. The matrix dimensions
        /// are (rows = <paramref name="deltas"/>.Dimension) x (columns = <paramref name="inputs"/>.Dimension).
        /// </returns>
        Matrix BuildWeightGradients(Vector inputs, Vector deltas);
    }
}
