// <copyright file="SincActivationProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Sinc activation function provider.
    /// </summary>
    public class SincActivationProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        [SuppressMessage(
            "Major Bug",
            "S1244:Floating point numbers should not be tested for equality",
            Justification = "Just to make sure we're not dividing by zero.")]
        public double CalculateActivation(double input)
        {
            return input == 0 ? 1 : Math.Sin(input) / input;
        }

        /// <inheritdoc/>
        [SuppressMessage(
            "Major Bug",
            "S1244:Floating point numbers should not be tested for equality",
            Justification = "Just to make sure we're not dividing by zero.")]
        public double CalculateGradient(double input)
        {
            return input == 0 ?
                0 :
                (Math.Cos(input) / input) - (Math.Sin(input) / Math.Pow(input, 2));
        }
    }
}
