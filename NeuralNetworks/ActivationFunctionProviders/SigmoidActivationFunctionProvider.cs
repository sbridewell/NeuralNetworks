// <copyright file="SigmoidActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Sigmoid activation function provider.
    /// </summary>
    public class SigmoidActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Sigmoid";

        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return Math.Exp(-input) / Math.Pow(1 + Math.Exp(-input), 2);
        }
    }
}
