// <copyright file="ArctangentActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Arctangent activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class ArctangentActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Arctangent";

        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return Math.Atan(input);
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            //return (1 / Math.Pow(input, 2)) + 1; // original implementation - this is way off

            // copied from HyperbolicTangent - it's close at small x valus but diverges at
            // large x values, which is what we expect since arctangent approaches pi/2 as
            // x goes to infinity, while hyperbolic tangent approaches 1
            return 1 - Math.Pow(Math.Tanh(input), 2);
        }
    }
}
