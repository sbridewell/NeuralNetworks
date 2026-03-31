// <copyright file="AbstractActivationFunctionProviderSpan.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders.Abstract
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Abstract base class that provides vectorised overloads for activation providers
    /// using Span&lt;double&gt; which may help the JIT and bounds-check elimination.
    /// </summary>
    public abstract class AbstractActivationFunctionProviderSpan : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public abstract string DisplayName { get; }

        /// <inheritdoc/>
        public abstract double CalculateActivation(double input);

        /// <inheritdoc/>
        public abstract double CalculateGradient(double input);

        /// <summary>
        /// Applies the activation function element-wise to a vector using Span&lt;double&gt;
        /// for potential performance benefits.
        /// </summary>
        /// <param name="input">
        /// The input vector to which the activation function will be applied.
        /// </param>
        /// <returns>The results of the activation function.</returns>
        public virtual Vector CalculateActivation(Vector input)
        {
            var length = input.Dimension;
            var src = input.Elements.AsSpan();
            var dst = new double[length].AsSpan();

            for (var i = 0; i < length; i++)
            {
                dst[i] = this.CalculateActivation(src[i]);
            }

            return new Vector(dst.ToArray());
        }

        /// <summary>
        /// Applies the derivative of the activation function element-wise to a vector
        /// using Span&lt;double&gt; for potential performance benefits.
        /// </summary>
        /// <param name="input">
        /// The input vector to which the derivative of the activation function will be applied.
        /// </param>
        /// <returns>The results of the derivative function.</returns>
        public virtual Vector CalculateGradient(Vector input)
        {
            var length = input.Dimension;
            var src = input.Elements.AsSpan();
            var dst = new double[length].AsSpan();

            for (var i = 0; i < length; i++)
            {
                dst[i] = this.CalculateGradient(src[i]);
            }

            return new Vector(dst.ToArray());
        }
    }
}