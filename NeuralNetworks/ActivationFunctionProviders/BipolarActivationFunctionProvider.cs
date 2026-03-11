// <copyright file="BipolarActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Bipolar activation function provider.
    /// </summary>
    public class BipolarActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Bipolar";

        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return input < 0 ? -1 : 1;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return 0;
        }
    }
}
