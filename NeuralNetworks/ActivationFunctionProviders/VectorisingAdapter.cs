// <copyright file="VectorisingAdapter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.ActivationFunctionProviders
{
    using System;
    using System.Threading.Tasks;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// A runtime-adaptable wrapper that exposes vectorised methods for any
    /// scalar-only <see cref="IActivationFunctionProvider"/> implementation and
    /// allows selection of the iteration strategy at runtime.
    /// </summary>
    public sealed class VectorisingAdapter : IActivationFunctionProvider
    {
        private readonly IActivationFunctionProvider inner;
        private readonly IterationStrategy strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorisingAdapter"/> class.
        /// .</summary>
        /// <param name="inner">The scalar provider to wrap.</param>
        /// <param name="strategy">Iteration strategy to use for vector methods.</param>
        public VectorisingAdapter(IActivationFunctionProvider inner, IterationStrategy strategy)
        {
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
            this.strategy = strategy;
        }

        /// <summary>Supported iteration strategies.</summary>
        public enum IterationStrategy
        {
            /// <summary>
            /// Use a basic for loop.
            /// </summary>
            ForLoop,

            /// <summary>
            /// Use a <see cref="Span{T}"/> for potential performance improvements.
            /// </summary>
            Span,

            /// <summary>
            /// Use Parallel.For for potential performance improvements.
            /// </summary>
            Parallel,
        }

        /// <inheritdoc/>
        public string DisplayName => this.inner.DisplayName;

        /// <inheritdoc/>
        public double CalculateActivation(double input) => this.inner.CalculateActivation(input);

        /// <summary>Vectorised activation which dispatches to the chosen strategy.</summary>
        /// <param name="input">The input vector.</param>
        /// <returns>A new vector containing the results of the activation function.</returns>
        public Vector CalculateActivation(Vector input)
        {
            return this.strategy switch
            {
                IterationStrategy.ForLoop => this.CalculateActivationForLoop(input),
                IterationStrategy.Span => this.CalculateActivationSpan(input),
                IterationStrategy.Parallel => this.CalculateActivationParallel(input),
                _ => throw new InvalidOperationException("Unknown iteration strategy."),
            };
        }

        /// <inheritdoc/>
        public double CalculateGradient(double input) => this.inner.CalculateGradient(input);

        /// <summary>Vectorised gradient which dispatches to the chosen strategy.</summary>
        /// <param name="input">The input vector for which to calculate the gradient.</param>
        /// <returns>A new vector containing the results of the gradient function.</returns>
        public Vector CalculateGradient(Vector input)
        {
            return this.strategy switch
            {
                IterationStrategy.ForLoop => this.CalculateGradientForLoop(input),
                IterationStrategy.Span => this.CalculateGradientSpan(input),
                IterationStrategy.Parallel => this.CalculateGradientParallel(input),
                _ => throw new InvalidOperationException("Unknown iteration strategy."),
            };
        }

        private Vector CalculateActivationForLoop(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];
            for (var i = 0; i < n; i++)
            {
                dst[i] = this.inner.CalculateActivation(src[i]);
            }

            return new Vector(dst);
        }

        private Vector CalculateActivationSpan(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements.AsSpan();
            var dst = new double[n].AsSpan();
            for (var i = 0; i < n; i++)
            {
                dst[i] = this.inner.CalculateActivation(src[i]);
            }

            return new Vector(dst.ToArray());
        }

        private Vector CalculateActivationParallel(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];
            Parallel.For(0, n, i => dst[i] = this.inner.CalculateActivation(src[i]));
            return new Vector(dst);
        }

        private Vector CalculateGradientForLoop(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];
            for (var i = 0; i < n; i++)
            {
                dst[i] = this.inner.CalculateGradient(src[i]);
            }

            return new Vector(dst);
        }

        private Vector CalculateGradientSpan(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements.AsSpan();
            var dst = new double[n].AsSpan();
            for (var i = 0; i < n; i++)
            {
                dst[i] = this.inner.CalculateGradient(src[i]);
            }

            return new Vector(dst.ToArray());
        }

        private Vector CalculateGradientParallel(Vector input)
        {
            var n = input.Dimension;
            var src = input.Elements;
            var dst = new double[n];
            Parallel.For(0, n, i => dst[i] = this.inner.CalculateGradient(src[i]));
            return new Vector(dst);
        }
    }
}