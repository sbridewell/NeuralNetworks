// <copyright file="ArctangentActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Arctangent activation function provider.
    /// </summary>
    public class ArctangentActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return Math.Atan(input);
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return (1 / Math.Pow(input, 2)) + 1;
        }
    }
}
