// <copyright file="LogisticActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    /// <summary>
    /// Logistic activation function provider.
    /// </summary>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/q/36384249/16563198"/>.
    /// </remarks>
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
            //// The below is equivalent to:
            ////return (1 / (1 + Math.Exp(-input))) * (1 - (1 / (1 + Math.Exp(-input))));
            //// but is easier to read.
            return this.CalculateActivation(input) * (1 - this.CalculateActivation(input)); // original

            // Finding the derivative using the quotient rule gives the following,
            // which also passes the unit tests but looks less efficient
            ////return Math.Exp(-input) / Math.Pow(1 + Math.Exp(-input), 2);
        }
    }
}
