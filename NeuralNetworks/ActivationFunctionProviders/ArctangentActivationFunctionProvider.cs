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
            ////return (1 / Math.Pow(input, 2)) + 1; // original implementation - this is way off

            // Correct derivative of arctangent: d/dx atan(x) = 1 / (1 + x^2).
            // This is always finite and decays as 1/x^2 for large |x|.
            return 1.0 / (1.0 + (input * input));
        }
    }
}
