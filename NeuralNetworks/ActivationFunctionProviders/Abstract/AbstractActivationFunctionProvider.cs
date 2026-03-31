// <copyright file="AbstractActivationFunctionProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders.Abstract
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Abstract base class that provides vectorised overloads for activation providers
    /// using a straightforward for-loop over the underlying array.
    /// Derive concrete activation providers from this class and implement the scalar
    /// methods.
    /// </summary>
    public abstract class AbstractActivationFunctionProvider : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Applies the activation function to a single scalar input.
        /// Concrete subclasses must implement this.
        /// </summary>
        /// <param name="input">The scalar input.</param>
        /// <returns>The activation value.</returns>
        public abstract double CalculateActivation(double input);

        /// <summary>
        /// Applies the gradient function to a single scalar input.
        /// Concrete subclasses must implement this.
        /// </summary>
        /// <param name="input">The scalar input.</param>
        /// <returns>The gradient value.</returns>
        public abstract double CalculateGradient(double input);

        /// <summary>
        /// Applies the activation function element-wise to a vector.
        /// Uses a simple for loop over the backing array.
        /// </summary>
        /// <param name="input">Input vector.</param>
        /// <returns>Vector of activations.</returns>
        public virtual Vector CalculateActivation(Vector input)
        {
            var length = input.Dimension;
            var src = input.Elements;
            var dst = new double[length];

            for (var i = 0; i < length; i++)
            {
                dst[i] = this.CalculateActivation(src[i]);
            }

            return new Vector(dst);
        }

        /// <summary>
        /// Applies the gradient function element-wise to a vector.
        /// Uses a simple for loop over the backing array.
        /// </summary>
        /// <param name="input">Input vector.</param>
        /// <returns>Vector of gradients.</returns>
        public virtual Vector CalculateGradient(Vector input)
        {
            var length = input.Dimension;
            var src = input.Elements;
            var dst = new double[length];

            for (var i = 0; i < length; i++)
            {
                dst[i] = this.CalculateGradient(src[i]);
            }

            return new Vector(dst);
        }
    }
}