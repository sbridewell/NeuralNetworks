// <copyright file="GaussianActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Gaussian activation function provider.
    /// </summary>
    public class GaussianActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return Math.Exp(Math.Pow(-input, 2));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return 2 * input * Math.Exp(Math.Pow(-input, 2));
        }
    }
}
