// <copyright file="IErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Interface for a providor of error functions (loss / cost functions) for neural networks.
    /// </summary>
    public interface IErrorFunctionProvider
    {
        // TODO: accept and receive double[] to support multiple neurons in a layer.
        /// <summary>
        /// Calculates the error between an expected value and an actual value.
        /// </summary>
        /// <param name="expectedOutput">The expected output value.</param>
        /// <param name="actualOutput">The actual output value.</param>
        /// <param name="stabilityEpsilon">
        /// Small positive value used to avoid division by zero and improve numerical stability.
        /// Defaults to 1e-12.
        /// </param>
        /// <returns>The calculated error.</returns>
        double CalculateError(double expectedOutput, double actualOutput/*, double stabilityEpsilon = 1e-12*/);

        double CalculateGradient(double expectedOutput, double actualOutput/*, double stabilityEpsilon = 1e-12*/);
    }
}
