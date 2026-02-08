// <copyright file="HyperbolicTangentActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Hyperbolic tangent activation function provider.
    /// </summary>
    public class HyperbolicTangentActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return Math.Tanh(input);
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return 1 - Math.Pow(Math.Tanh(input), 2);
        }
    }
}
