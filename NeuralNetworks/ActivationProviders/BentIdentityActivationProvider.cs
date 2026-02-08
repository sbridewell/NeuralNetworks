// <copyright file="BentIdentityActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Bent identity activation function provider.
    /// </summary>
    public class BentIdentityActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return ((Math.Sqrt(Math.Pow(input, 2) + 1) - 1) / 2) + input;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return (input / (2 * Math.Sqrt(Math.Pow(input, 2) + 1))) + 1;
        }
    }
}
