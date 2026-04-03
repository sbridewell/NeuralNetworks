// <copyright file="RectifiedLinearUnitActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Rectified Linear Unit (ReLU) activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class RectifiedLinearUnitActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Rectified linear unit";

        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return input < 0 ? 0 : input;
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return input < 0 ? 0 : 1;
        }
    }
}
