// <copyright file="LogisticActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>


// <copyright file="LogisticActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Logistic activation function provider.
    /// </summary>
    public class LogisticActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public string DisplayName => "Logistic";

        /// <inheritdoc/>
        public double CalculateActivation(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input)
        {
            return CalculateActivation(input) * (1 - CalculateActivation(input));
        }
    }
}
