// <copyright file="BipolarSigmoidActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Bipolar sigmoid activation function provider.
    /// </summary>
    public class BipolarSigmoidActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return (1 - Math.Exp(-input)) / (1 + Math.Exp(-input));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            double activatedValue = this.CalculateActivation(input);
            return 0.5 * (1 + activatedValue) * (1 - activatedValue);
        }
    }
}
