// <copyright file="GeneralisedKLErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Generalised Kullback-Leibler error function provider.
    /// </summary>
    public class GeneralisedKLErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            return new KullbackLeiblerErrorFunctionProvider().CalculateError(expectedOutput, actualOutput) - expectedOutput + actualOutput;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            return (expectedOutput + actualOutput) / actualOutput;
        }
    }
}
