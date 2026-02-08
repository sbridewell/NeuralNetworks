// <copyright file="LinearActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Linear activation function provider.
    /// </summary>
    public class LinearActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return input;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return 1;
        }
    }
}
