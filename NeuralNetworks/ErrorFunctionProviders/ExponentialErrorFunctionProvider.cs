// <copyright file="ExponentialErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Exponential error function provider.
    /// </summary>
    public class ExponentialErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            var error = Math.Pow(actualOutput - expectedOutput, 2);
            return Math.PI * Math.Exp(1 / Math.Exp(1 / Math.PI * error));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            return (2 / Math.PI) * (actualOutput - expectedOutput);
        }
    }
}
