// <copyright file="IActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationProviders
{
    /// <summary>
    /// Interface for a provider of activation functions for neural networks.
    /// </summary>
    public interface IActivationFunctionProvider
    {
        /// <summary>
        /// Applies the activation function to the specified input value.
        /// </summary>
        /// <param name="input">
        /// The input value to which the activation function is applied.
        /// </param>
        /// <returns>
        /// The result of applying the activation function to the input value.
        /// </returns>
        double CalculateActivation(double input);

        /// <summary>
        /// Applies the derivative (gradient) of the activation function to the specified input value.
        /// </summary>
        /// <param name="input">
        /// The input value to which the derivative of the activation function is applied.
        /// </param>
        /// <returns>
        /// The result of applying the derivative of the activation function to the input value.
        /// </returns>
        double CalculateGradient(double input);
    }
}
