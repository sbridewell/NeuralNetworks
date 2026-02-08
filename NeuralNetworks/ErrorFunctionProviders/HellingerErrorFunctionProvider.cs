// <copyright file="HellingerErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Hellinger error function provider.
    /// </summary>
    public class HellingerErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            var error = Math.Pow(Math.Sqrt(actualOutput) - Math.Sqrt(expectedOutput), 2);
            return (1 / Math.Sqrt(2)) * error;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            return (Math.Sqrt(actualOutput) - Math.Sqrt(expectedOutput)) / (Math.Sqrt(2) * Math.Sqrt(actualOutput));
        }
    }
}
