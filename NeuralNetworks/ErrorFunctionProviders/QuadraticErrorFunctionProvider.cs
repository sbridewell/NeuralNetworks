// <copyright file="QuadraticErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Quadratic error function provider.
    /// </summary>
    public class QuadraticErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            return 0.5 * Math.Pow(expectedOutput - actualOutput, 2);
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            // TODO: possibly should be actualOutput - expectedOutput?
            return expectedOutput - actualOutput;
        }
    }
}
