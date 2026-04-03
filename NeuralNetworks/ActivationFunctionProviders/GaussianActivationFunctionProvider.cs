// <copyright file="GaussianActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Gaussian activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
    public class GaussianActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Gaussian";

        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            ////return Math.Exp(Math.Pow(-input, 2)); // original - negative sign on input has no effect
            return Math.Exp(Math.Pow(input, 2));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            ////return 2 * input * Math.Exp(Math.Pow(-input, 2)); // Original - negative sign on input has no effect
            return 2 * input * Math.Exp(Math.Pow(input, 2));
        }
    }
}
