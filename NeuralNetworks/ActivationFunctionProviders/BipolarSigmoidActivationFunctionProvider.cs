// <copyright file="BipolarSigmoidActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Bipolar sigmoid activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class BipolarSigmoidActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Bipolar sigmoid";

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
