// <copyright file="AbstractActivationFunctionProviderParallel.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders.Abstract
{
    using System.Threading.Tasks;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Abstract base class that provides vectorised overloads for activation providers
    /// using parallel iteration (Parallel.For). Use this to benchmark parallel performance.
    /// </summary>
    public abstract class AbstractActivationFunctionProviderParallel : IActivationFunctionProvider
    {
        /// <inheritdoc/>
        public abstract string DisplayName { get; }

        /// <inheritdoc/>
        public abstract double CalculateActivation(double input);

        /// <inheritdoc/>
        public abstract double CalculateGradient(double input);

        /// <inheritdoc/>
        public virtual Vector CalculateActivation(Vector input)
        {
            var length = input.Dimension;
            var src = input.Elements;
            var dst = new double[length];

            Parallel.For(0, length, i => dst[i] = this.CalculateActivation(src[i]));

            return new Vector(dst);
        }

        /// <inheritdoc/>
        public virtual Vector CalculateGradient(Vector input)
        {
            var length = input.Dimension;
            var src = input.Elements;
            var dst = new double[length];

            Parallel.For(0, length, i => dst[i] = this.CalculateGradient(src[i]));

            return new Vector(dst);
        }
    }
}