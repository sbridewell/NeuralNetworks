// <copyright file="SinusoidalActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>


// <copyright file="SinusoidalActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Sinusoidal activation function provider.
    /// </summary>
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
