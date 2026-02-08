// <copyright file="NormaliserTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test
{
    using FluentAssertions;

    /// <summary>
    /// Unit tests for the <see cref="Normaliser"/> class.
    /// </summary>
    public class NormaliserTest
    {
        /// <summary>
        /// Gets the names of the test cases for normalising double[] to int[]..
        /// </summary>
        public static TheoryData<string> DoubleToIntTestCaseNames => new (DoubleToIntTestCases.Keys);

        /// <summary>
        /// Gets the names of the test cases for normalising double[][] to int[][].
        /// </summary>
        public static TheoryData<string> DoubleDoubleToIntIntTestCaseNames => new (DoubleDoubleToIntIntTestCases.Keys);

        private static Dictionary<string, DoubleToIntTestCase> DoubleToIntTestCases
        {
            get
            {
                var data = new Dictionary<string, DoubleToIntTestCase>();
                data.Add("first", new DoubleToIntTestCase(new double[] { 10, 20, 30, 40, 50 }, 100, 0, new int[] { 0, 25, 50, 75, 100 }));
                data.Add("second", new DoubleToIntTestCase(new double[] { 10, 20, 30, 40, 50 }, 0, -100, new int[] { -100, -75, -50, -25, 0 }));
                data.Add("third", new DoubleToIntTestCase(new double[] { 10, 20, 30, 40, 50 }, 100, -100, new int[] { -100, -50, 0, 50, 100 }));
                data.Add("4", new DoubleToIntTestCase(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 }, 100, 0, new int[] { 0, 25, 50, 75, 100 }));
                data.Add("5", new DoubleToIntTestCase(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0, -100, new int[] { -100, -75, -50, -25, 0 }));
                data.Add("6", new DoubleToIntTestCase(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 }, 100, -100, new int[] { -100, -50, 0, 50, 100 }));
                data.Add("7", new DoubleToIntTestCase(new double[] { 1000, 2000, 3000, 4000, 5000 }, 100, 0, new int[] { 0, 25, 50, 75, 100 }));
                data.Add("8", new DoubleToIntTestCase(new double[] { 1000, 2000, 3000, 4000, 5000 }, 0, -100, new int[] { -100, -75, -50, -25, 0 }));
                data.Add("9", new DoubleToIntTestCase(new double[] { 1000, 2000, 3000, 4000, 5000 }, 100, -100, new int[] { -100, -50, 0, 50, 100 }));
                data.Add("11", new DoubleToIntTestCase(new double[] { -0.1, -0.2, -0.3, -0.4, -0.5 }, 0, -100, new int[] { 0, -25, -50, -75, -100 }));
                data.Add("12", new DoubleToIntTestCase(new double[] { -0.1, -0.2, -0.3, -0.4, -0.5 }, 100, -100, new int[] { 100, 50, 0, -50, -100 }));
                data.Add("13", new DoubleToIntTestCase(new double[] { 5000, 4000, 3000, 2000, 1000 }, 100, 0, new int[] { 100, 75, 50, 25, 0 }));
                data.Add("14", new DoubleToIntTestCase(new double[] { 5000, 4000, 3000, 2000, 1000 }, 0, -100, new int[] { 0, -25, -50, -75, -100 }));
                data.Add("15", new DoubleToIntTestCase(new double[] { 5000, 4000, 3000, 2000, 1000 }, 100, -100, new int[] { 100, 50, 0, -50, -100 }));
                return data;
            }
        }

        private static Dictionary<string, DoubleDoubleToIntIntTestCase> DoubleDoubleToIntIntTestCases
        {
            get
            {
                var data = new Dictionary<string, DoubleDoubleToIntIntTestCase>();

                // Single-row cases mirroring DoubleToIntTestCases
                data.Add(
                    "first",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 10, 20, 30, 40, 50 } },
                    100,
                    0,
                    new int[][] { new int[] { 0, 25, 50, 75, 100 } }));

                data.Add(
                    "second",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 10, 20, 30, 40, 50 } },
                    0,
                    -100,
                    new int[][] { new int[] { -100, -75, -50, -25, 0 } }));

                data.Add(
                    "third",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 10, 20, 30, 40, 50 } },
                    100,
                    -100,
                    new int[][] { new int[] { -100, -50, 0, 50, 100 } }));

                data.Add(
                    "4",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 } },
                    100,
                    0,
                    new int[][] { new int[] { 0, 25, 50, 75, 100 } }));

                data.Add(
                    "5",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 } },
                    0,
                    -100,
                    new int[][] { new int[] { -100, -75, -50, -25, 0 } }));

                data.Add(
                    "6",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 } },
                    100,
                    -100,
                    new int[][] { new int[] { -100, -50, 0, 50, 100 } }));

                data.Add(
                    "7",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 1000, 2000, 3000, 4000, 5000 } },
                    100,
                    0,
                    new int[][] { new int[] { 0, 25, 50, 75, 100 } }));

                data.Add(
                    "8",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 1000, 2000, 3000, 4000, 5000 } },
                    0,
                    -100,
                    new int[][] { new int[] { -100, -75, -50, -25, 0 } }));

                data.Add(
                    "9",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 1000, 2000, 3000, 4000, 5000 } },
                    100,
                    -100,
                    new int[][] { new int[] { -100, -50, 0, 50, 100 } }));

                data.Add(
                    "10",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { -0.1, -0.2, -0.3, -0.4, -0.5 } },
                    100,
                    0,
                    new int[][] { new int[] { 100, 75, 50, 25, 0 } }));

                data.Add(
                    "11",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { -0.1, -0.2, -0.3, -0.4, -0.5 } },
                    0,
                    -100,
                    new int[][] { new int[] { 0, -25, -50, -75, -100 } }));

                data.Add(
                    "12",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { -0.1, -0.2, -0.3, -0.4, -0.5 } },
                    100,
                    -100,
                    new int[][] { new int[] { 100, 50, 0, -50, -100 } }));

                data.Add(
                    "13",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 5000, 4000, 3000, 2000, 1000 } },
                    100,
                    0,
                    new int[][] { new int[] { 100, 75, 50, 25, 0 } }));

                data.Add(
                    "14",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 5000, 4000, 3000, 2000, 1000 } },
                    0,
                    -100,
                    new int[][] { new int[] { 0, -25, -50, -75, -100 } }));

                data.Add(
                    "15",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][] { new double[] { 5000, 4000, 3000, 2000, 1000 } },
                    100,
                    -100,
                    new int[][] { new int[] { 100, 50, 0, -50, -100 } }));

                // Multi-row and special cases
                data.Add(
                    "multi1",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][]
                    {
                        new double[] { 10, 20, 30 },
                        new double[] { 40, 50, 60 },
                    },
                    100,
                    0,
                    new int[][] { new int[] { 0, 50, 100 }, new int[] { 0, 50, 100 }, }));

                data.Add(
                    "emptyInner",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][]
                    {
                        new double[0],
                        new double[] { 1.0, 2.0 },
                    },
                    100,
                    0,
                    new int[][] { new int[0], new int[] { 0, 100 }, }));

                data.Add(
                    "singleValue",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][]
                    {
                        new double[] { 5.0 },
                        new double[] { 10.0, 10.0 },
                    },
                    100,
                    0,
                    new int[][] { new int[] { 50 }, new int[] { 50, 50 }, }));

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                data.Add(
                    "nullInner",
                    new DoubleDoubleToIntIntTestCase(
                    new double[][]
                    {
                        null,
                        new double[] { 10, 20 },
                    },
                    100,
                    0,
                    new int[][] { new int[0], new int[] { 0, 100 }, }));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                return data;
            }
        }

        private record DoubleToIntTestCase(double[] inputs, int upperBound, int lowwerBound, int[] expectedOutputs);

        private record DoubleDoubleToIntIntTestCase(double[][] inputs, int upperBound, int lowerBound, int[][] expectedOutputs);

        /// <summary>
        /// Tests the Normalise method which accepts a one dimensional array of doubles
        /// and returns a one dimensional array of integers.
        /// </summary>
        /// <param name="testCaseName">Name of the test case to execute.</param>
        [Theory]
        [MemberData(nameof(DoubleToIntTestCaseNames))]
        public void NormaliseDoubleArray_ReturnIntArray_ShouldReturnNormalisedArray(string testCaseName)
        {
            // Arrange
            var testCase = DoubleToIntTestCases[testCaseName];
            int lowerBound = testCase.lowwerBound;
            int upperBound = testCase.upperBound;

            // Act
            var result = Normaliser.Normalise(upperBound, lowerBound, testCase.inputs);

            // Assert
            result.Should().BeEquivalentTo(testCase.expectedOutputs);
        }

        /// <summary>
        /// Tests the Normalise method which accepts a jagged array of doubles
        /// and returns a jagged array of integeres.
        /// </summary>
        /// <param name="testCaseName">Name of the test cae to execute.</param>
        [Theory]
        [MemberData(nameof(DoubleDoubleToIntIntTestCaseNames))]
        public void NormaliseDoubleDoubleArray_ReturnIntArray_ShouldReturnNormalisedArray(string testCaseName)
        {
            // Arrange
            var testCase = DoubleDoubleToIntIntTestCases[testCaseName];
            int lowerBound = testCase.lowerBound;
            int upperBound = testCase.upperBound;

            // Act
            var result = Normaliser.Normalise(upperBound, lowerBound, testCase.inputs);

            // Assert
            result.Should().BeEquivalentTo(testCase.expectedOutputs);
        }
    }
}
