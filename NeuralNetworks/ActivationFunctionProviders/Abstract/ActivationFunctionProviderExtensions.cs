// <copyright file="ActivationFunctionProviderExtensions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders.Abstract
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Extension methods that provide vector overloads for <see cref="IActivationFunctionProvider"/>.
    /// Non-breaking: no changes to the interface or existing implementations required.
    /// </summary>
    public static class ActivationFunctionProviderExtensions
    {
        // TODO: consider adding these methods to IActivationFunctionProvider

        /// <summary>
        /// Applies the activation function element-wise to the specified vector.
        /// </summary>
        /// <param name="provider">The activation function provider.</param>
        /// <param name="input">The input vector.</param>
        /// <returns>A vector containing the activation of each element.</returns>
        public static Vector CalculateActivation(this IActivationFunctionProvider provider, Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];

            for (var i = 0; i < n; i++)
            {
                dst[i] = provider.CalculateActivation(src[i]);
            }

            return new Vector(dst);
        }

        /// <summary>
        /// Applies the gradient function element-wise to the specified vector.
        /// </summary>
        /// <param name="provider">The activation function provider.</param>
        /// <param name="input">The input vector.</param>
        /// <returns>A vector containing the gradient of each element.</returns>
        public static Vector CalculateGradient(this IActivationFunctionProvider provider, Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];

            for (var i = 0; i < n; i++)
            {
                dst[i] = provider.CalculateGradient(src[i]);
            }

            return new Vector(dst);
        }
    }
}