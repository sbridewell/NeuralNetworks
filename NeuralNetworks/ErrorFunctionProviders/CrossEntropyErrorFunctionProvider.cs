// <copyright file="CrossEntropyErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Cross entropy error function provider.
    /// </summary>
    public class CrossEntropyErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            return (expectedOutput * Math.Log(actualOutput)) + ((1 - expectedOutput) * Math.Log(1 - actualOutput));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            return (actualOutput - expectedOutput) / ((actualOutput + 1) * actualOutput);
        }
    }
}
