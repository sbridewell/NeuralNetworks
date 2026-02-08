// <copyright file="KullbackLeiblerErrorFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ErrorFunctionProviders
{
    /// <summary>
    /// Hullback-Leibler error function provider.
    /// </summary>
    public class KullbackLeiblerErrorFunctionProvider : IErrorFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateError(double expectedOutput, double actualOutput)
        {
            return expectedOutput * Math.Log(expectedOutput / actualOutput);
        }

        /// <inheritdoc/>
        public double CalculateGradient(double expectedOutput, double actualOutput)
        {
            return expectedOutput / actualOutput;
        }
    }
}
