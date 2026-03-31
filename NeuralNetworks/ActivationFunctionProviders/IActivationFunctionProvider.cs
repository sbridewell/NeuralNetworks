// <copyright file="IActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Interface for a provider of activation functions for neural networks.
    /// </summary>
    public interface IActivationFunctionProvider : IHaveADisplayName
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
        /// Applies the activation function element-wise to the specified vector.
        /// Default implementation forwards to the scalar <see cref="CalculateActivation(double)"/>.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>A vector with the activation applied to each element.</returns>
        Vector CalculateActivation(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];
            for (var i = 0; i < n; i++)
            {
                dst[i] = this.CalculateActivation(src[i]);
            }

            return new Vector(dst);
        }

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

        /// <summary>
        /// Applies the gradient function element-wise to the specified vector.
        /// Default implementation forwards to the scalar <see cref="CalculateGradient(double)"/>.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>A vector with the gradient applied to each element.</returns>
        Vector CalculateGradient(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];
            for (var i = 0; i < n; i++)
            {
                dst[i] = this.CalculateGradient(src[i]);
            }

            return new Vector(dst);
        }
    }
}
