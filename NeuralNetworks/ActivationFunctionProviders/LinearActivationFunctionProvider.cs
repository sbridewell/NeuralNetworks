// <copyright file="LinearActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>


// <copyright file="LinearActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Linear activation function provider.
    /// </summary>
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
