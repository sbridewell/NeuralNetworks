
using BenchmarkDotNet.Attributes;

namespace Sde.NeuralNetworks.Benchmarks
{
    /// <summary>
    /// Benchmarks comparing the <see cref="Matrix"/> class to equivalent
    /// array-based representations for matrix multiplication.
    /// </summary>
    [MemoryDiagnoser]
    public class MatrixBenchmarks
    {
        /// <summary>
        /// Size (rows/columns) for square test matrices.
        /// </summary>
        [Params(64, 128)]
        public int N { get; set; }

        private Matrix mA = null!;
        private Matrix mB = null!;
        private double[,] arr2D_A = null!;
        private double[,] arr2D_B = null!;
        private double[][] jaggedA = null!;
        private double[][] jaggedB = null!;
        private double[] flatA = null!;
        private double[] flatB = null!;

        private object? _result;

        /// <summary>
        /// Prepares matrices used in the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            arr2D_A = new double[N, N];
            arr2D_B = new double[N, N];
            jaggedA = new double[N][];
            jaggedB = new double[N][];
            flatA = new double[N * N];
            flatB = new double[N * N];

            for (var i = 0; i < N; i++)
            {
                jaggedA[i] = new double[N];
                jaggedB[i] = new double[N];
                for (var j = 0; j < N; j++)
                {
                    var v = ((i * N) + j) % 10 + 1.0;
                    arr2D_A[i, j] = v;
                    arr2D_B[i, j] = v + 1.0;
                    jaggedA[i][j] = arr2D_A[i, j];
                    jaggedB[i][j] = arr2D_B[i, j];
                    flatA[i * N + j] = arr2D_A[i, j];
                    flatB[i * N + j] = arr2D_B[i, j];
                }
            }

            mA = new Matrix(arr2D_A);
            mB = new Matrix(arr2D_B);
        }

        /// <summary>
        /// Matrix multiplication using the Matrix class implementation.
        /// </summary>
        [Benchmark]
        public Matrix Multiply_MatrixClass()
        {
            // Arrange/Act
            var product = mA.Multiply(mB);

            // Assert
            _result = product;
            return product;
        }

        /// <summary>
        /// Matrix multiplication using a two-dimensional array.
        /// </summary>
        [Benchmark]
        public double[,] Multiply_Array2D()
        {
            // Arrange
            var result = new double[N, N];

            // Act
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    double sum = 0.0;
                    for (var k = 0; k < N; k++)
                    {
                        sum += arr2D_A[i, k] * arr2D_B[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            // Assert
            _result = result;
            return result;
        }

        /// <summary>
        /// Matrix multiplication using jagged arrays.
        /// </summary>
        [Benchmark]
        public double[][] Multiply_Jagged()
        {
            // Arrange
            var result = new double[N][];
            for (var i = 0; i < N; i++) result[i] = new double[N];

            // Act
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    double sum = 0.0;
                    for (var k = 0; k < N; k++)
                    {
                        sum += jaggedA[i][k] * jaggedB[k][j];
                    }

                    result[i][j] = sum;
                }
            }

            // Assert
            _result = result;
            return result;
        }

        /// <summary>
        /// Matrix multiplication using flattened one-dimensional arrays.
        /// </summary>
        [Benchmark]
        public double[] Multiply_Flat()
        {
            // Arrange
            var result = new double[N * N];

            // Act
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    double sum = 0.0;
                    var baseI = i * N;
                    for (var k = 0; k < N; k++)
                    {
                        sum += flatA[baseI + k] * flatB[k * N + j];
                    }

                    result[baseI + j] = sum;
                }
            }

            // Assert
            _result = result;
            return result;
        }
    }
}
