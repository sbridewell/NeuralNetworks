namespace Sde.NeuralNetworks.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;
    using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
    using Sde.NeuralNetworks;

    /// <summary>
    /// Class containing the entry point for the benchmark application.
    /// </summary>
    /// <remarks>
    /// To run the benchmarks, open a Visual Studio command prompt, run the following .net CLI command from the folder containing this project.
    /// <code>dotnet run -c Release</code>
    /// </remarks>
    public static class BenchmarkProgram
    {
        /// <summary>
        /// Entry point of the benchmark application. Runs the registered benchmarks.
        /// </summary>
        /// <param name="args">The command-line arguments forwarded to BenchmarkDotNet.</param>
        public static void Main(string[] args)
        {
            // Prevent McAffee from killing off the benchmarking process
            var config = DefaultConfig.Instance.AddJob(
                Job.MediumRun.WithLaunchCount(1).WithToolchain(InProcessNoEmitToolchain.Instance));

            // Run benchmarks for vector and matrix implementations.
            //var switcher = BenchmarkSwitcher.FromTypes(new[] { typeof(VectorBenchmarks), typeof(MatrixBenchmarks) });
            //switcher.Run(args);
            BenchmarkRunner.Run<VectorBenchmarks>();
            BenchmarkRunner.Run<MatrixBenchmarks>();
        }
    }


}
