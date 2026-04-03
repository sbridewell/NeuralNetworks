// <copyright file="BentIdentityActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Bent identity activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class BentIdentityActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Bent identity";

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
