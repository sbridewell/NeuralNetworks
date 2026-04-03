// <copyright file="LinearActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Linear activation function provider.
    /// The activation function is f(x) = x, and the gradient is f'(x) = 1.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class LinearActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Linear";

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
