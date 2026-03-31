namespace Sde.NeuralNetworks.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using Sde.NeuralNetworks.ActivationFunctionProviders.Abstract;
    using Sde.NeuralNetworks.LinearAlgebra;
    using System;

    /// <summary>
    /// Benchmarks element-wise activation and gradient calculation for three
    /// iteration strategies: simple for-loop, Span-based, and Parallel.For.
    /// Parallel.For is the fastest (on my laptop anyway).
    /// </summary>
    [MemoryDiagnoser]
    public class ActivationFunctionProviderIterationBenchmarks
    {
        private Vector input = default!;
        private AbstractActivationFunctionProvider forLoopProvider = default!;
        private AbstractActivationFunctionProviderSpan spanProvider = default!;
        private AbstractActivationFunctionProviderParallel parallelProvider = default!;

        /// <summary>
        /// Vector sizes to test. Benchmarks will run once per value.
        /// </summary>
        [Params(1_000, 10_000, 100_000)]
        public int Length { get; set; }

        /// <summary>
        /// Global setup that initialises the input vector and concrete providers.
        /// </summary>
        [GlobalSetup]
        public void GlobalSetup()
        {
            var rnd = new Random(42);
            var arr = new double[this.Length];
            for (var i = 0; i < arr.Length; i++)
            {
                // values in range [-10, 10]
                arr[i] = (rnd.NextDouble() - 0.5) * 20.0;
            }

            this.input = new Vector(arr);

            // Concrete providers inherit the abstract bases and implement scalar methods.
            this.forLoopProvider = new TanhForLoopProvider();
            this.spanProvider = new TanhSpanProvider();
            this.parallelProvider = new TanhParallelProvider();
        }

        /// <summary>Activation using simple for-loop implementation.</summary>
        [Benchmark(Baseline = true)]
        public Vector ForLoop_Activation() => this.forLoopProvider.CalculateActivation(this.input);

        /// <summary>Activation using Span-based implementation.</summary>
        [Benchmark]
        public Vector Span_Activation() => this.spanProvider.CalculateActivation(this.input);

        /// <summary>Activation using Parallel.For implementation.</summary>
        [Benchmark]
        public Vector Parallel_Activation() => this.parallelProvider.CalculateActivation(this.input);

        /// <summary>Gradient using simple for-loop implementation.</summary>
        [Benchmark]
        public Vector ForLoop_Gradient() => this.forLoopProvider.CalculateGradient(this.input);

        /// <summary>Gradient using Span-based implementation.</summary>
        [Benchmark]
        public Vector Span_Gradient() => this.spanProvider.CalculateGradient(this.input);

        /// <summary>Gradient using Parallel.For implementation.</summary>
        [Benchmark]
        public Vector Parallel_Gradient() => this.parallelProvider.CalculateGradient(this.input);
    }

    /// <summary>
    /// Concrete provider deriving from <see cref="AbstractActivationFunctionProvider"/>.
    /// Uses hyperbolic tangent as the activation to keep scalar work identical across strategies.
    /// </summary>
    public sealed class TanhForLoopProvider : AbstractActivationFunctionProvider
    {
        /// <inheritdoc/>
        public override string DisplayName => "Tanh - ForLoop";

        /// <inheritdoc/>
        public override double CalculateActivation(double input) => Math.Tanh(input);

        /// <inheritdoc/>
        public override double CalculateGradient(double input)
        {
            var t = Math.Tanh(input);
            return 1.0 - (t * t);
        }
    }

    /// <summary>
    /// Concrete provider deriving from <see cref="AbstractActivationFunctionProviderSpan"/>.
    /// Identical scalar semantics to ensure fair benchmarking.
    /// </summary>
    public sealed class TanhSpanProvider : AbstractActivationFunctionProviderSpan
    {
        /// <inheritdoc/>
        public override string DisplayName => "Tanh - Span";

        /// <inheritdoc/>
        public override double CalculateActivation(double input) => Math.Tanh(input);

        /// <inheritdoc/>
        public override double CalculateGradient(double input)
        {
            var t = Math.Tanh(input);
            return 1.0 - (t * t);
        }
    }

    /// <summary>
    /// Concrete provider deriving from <see cref="AbstractActivationFunctionProviderParallel"/>.
    /// Identical scalar semantics to ensure fair benchmarking.
    /// </summary>
    public sealed class TanhParallelProvider : AbstractActivationFunctionProviderParallel
    {
        /// <inheritdoc/>
        public override string DisplayName => "Tanh - Parallel";

        /// <inheritdoc/>
        public override double CalculateActivation(double input) => Math.Tanh(input);

        /// <inheritdoc/>
        public override double CalculateGradient(double input)
        {
            var t = Math.Tanh(input);
            return 1.0 - (t * t);
        }
    }
}