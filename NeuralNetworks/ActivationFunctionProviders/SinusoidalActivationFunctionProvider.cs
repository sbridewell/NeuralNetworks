// <copyright file="SinusoidalActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Sinusoidal activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class SinusoidalActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Sinusoidal";

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
