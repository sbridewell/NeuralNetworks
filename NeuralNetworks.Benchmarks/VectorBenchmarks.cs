
namespace Sde.NeuralNetworks.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    /// <summary>
    /// Benchmarks comparing the <see cref="Vector"/> class to equivalent array-based
    /// representations for common vector operations.
    /// </summary>
    [MemoryDiagnoser]
    public class VectorBenchmarks
    {
        /// <summary>
        /// Length of the test vectors.
        /// </summary>
        [Params(1000, 10000)]
        public int Length { get; set; }

        private Vector vA = null!;
        private Vector vB = null!;
        private double[] arrA = null!;
        private double[] arrB = null!;
        private double[][] jagged = null!;
        private double[,] twoD = null!;

        private object? _result;

        /// <summary>
        /// Prepares the data structures used by the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            arrA = new double[Length];
            arrB = new double[Length];
            jagged = new double[1][] { new double[Length] };
            twoD = new double[1, Length];

            for (var i = 0; i < Length; i++)
            {
                var v = (i % 10) + 1.0;
                arrA[i] = v;
                arrB[i] = v + 1.0;
                jagged[0][i] = arrA[i];
                twoD[0, i] = arrA[i];
            }

            vA = new Vector(arrA);
            vB = new Vector(arrB);
        }

        /// <summary>
        /// Dot product using the Vector class implementation.
        /// </summary>
        [Benchmark]
        public double DotProduct_VectorClass()
        {
            // Arrange/Act
            var result = vA.MultiplyUsingDotProduct(vB);

            // Assert (store to prevent optimization)
            _result = result;
            return result;
        }

        /// <summary>
        /// Dot product using 1-dimensional arrays.
        /// </summary>
        [Benchmark]
        public double DotProduct_Array1D()
        {
            // Arrange/Act
            double sum = 0.0;
            for (var i = 0; i < Length; i++)
            {
                sum += arrA[i] * arrB[i];
            }

            // Assert
            _result = sum;
            return sum;
        }

        /// <summary>
        /// Dot product using a jagged array with a single row.
        /// </summary>
        [Benchmark]
        public double DotProduct_Jagged()
        {
            // Arrange/Act
            var row = jagged[0];
            double sum = 0.0;
            for (var i = 0; i < Length; i++)
            {
                sum += row[i] * arrB[i];
            }

            // Assert
            _result = sum;
            return sum;
        }

        /// <summary>
        /// Dot product using a two-dimensional array with a single row.
        /// </summary>
        [Benchmark]
        public double DotProduct_TwoD()
        {
            // Arrange/Act
            double sum = 0.0;
            for (var i = 0; i < Length; i++)
            {
                sum += twoD[0, i] * arrB[i];
            }

            // Assert
            _result = sum;
            return sum;
        }
    }
}
