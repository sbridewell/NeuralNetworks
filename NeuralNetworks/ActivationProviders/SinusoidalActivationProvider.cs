// <copyright file="SinusoidalActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Sinusoidal activation function provider.
    /// </summary>
    public class SinusoidalActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return Math.Sin(input);
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return Math.Cos(input);
        }
    }
}
