// <copyright file="ItakuraSaitoErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Itakura-Saito error function provider.
    /// </summary>
    public class ItakuraSaitoErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            return (expectedOutput / actualOutput) - Math.Log(expectedOutput / actualOutput) - 1;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            return (expectedOutput + Math.Pow(actualOutput, 2)) / Math.Pow(actualOutput, 2);
        }
    }
}
