// <copyright file="SoftPlusActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// SoftPlus activation function provider.
    /// </summary>
    public class SoftPlusActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return Math.Log(1 + Math.Exp(input));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }
    }
}
