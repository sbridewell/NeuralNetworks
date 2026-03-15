// <copyright file="MatrixTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test
{
    using System;
    using System.Security.AccessControl;
    using FluentAssertions;
    using Sde.NeuralNetworks;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Matrix"/> class.
    /// </summary>
    public class MatrixTest
    {
        #region test case record definitions

        /// <summary>
        /// Test case for equality or inequality of two matrices.
        /// </summary>
        /// <param name="left">The left hand side matrix.</param>
        /// <param name="right">The right hand side matrix.</param>
        /// <param name="shouldBeEqual">
        /// True if the matrices should be considered equal, otherwise false.
        /// </param>
        public record MatrixEqualityTestCase(Matrix left, Matrix right, bool shouldBeEqual);

        /// <summary>
        /// Test case for an operation which takes a matrix as input and produces a matrix as output.
        /// </summary>
        /// <param name="inputMatrix">The input matrix.</param>
        /// <param name="expectedResult">The expected result of the operation.</param>
        public record MatrixMatrixTestCase(Matrix inputMatrix, Matrix expectedResult);

        /// <summary>
        /// Test case for an operation which takes two matrices as input and produces a matrix as output.
        /// such as addition or multiplication.
        /// </summary>
        /// <param name="left">The left hand operand.</param>
        /// <param name="right">The right hand operand.</param>
        /// <param name="expectedResult">The expected result of the operation.</param>
        public record MatrixMatrixMatrixTestCase(Matrix left, Matrix right, Matrix expectedResult);

        /// <summary>
        /// Test case for an operation which takes a matrix and a scalar as input and produces a matrix as output.
        /// </summary>
        /// <param name="matrix">The input matrix</param>
        /// <param name="scalar">The input scalar.</param>
        /// <param name="expectedResult">The expected result of the operation.</param>
        public record MatrixScalarMatrixTestCase(Matrix matrix, double scalar, Matrix expectedResult);

        /// <summary>
        /// Test case for an operation which takes a matrix as input and produces an array of vectors as output.
        /// </summary>
        /// <param name="inputMatrix">The input matrix.</param>
        /// <param name="expectedVectors">The expected result of the operation.</param>
        public record MatrixVectorArrrayTestCase(Matrix inputMatrix, Vector[] expectedVectors);

        #endregion

        #region test case names

        /// <summary>
        /// Gets the names of the equality test cases.
        /// </summary>
        public static TheoryData<string> EqualityTestCaseNames => new TheoryData<string>(EqualityTestCases.Keys);

        /// <summary>
        /// Gets the names of the column vector test cases.
        /// </summary>
        public static TheoryData<string> ColumnVectorsTestCaseNames => new TheoryData<string>(ColumnVectorsTestCases.Keys);

        /// <summary>
        /// Gets the names of the row vector test cases.
        /// </summary>
        public static TheoryData<string> RowVectorsTestCaseNames => new TheoryData<string>(RowVectorsTestCases.Keys);

        /// <summary>
        /// Gets the names of the addition test cases.
        /// </summary>
        public static TheoryData<string> AdditionTestCaseNames => new TheoryData<string>(AdditionTestCases.Keys);

        /// <summary>
        /// Gets the names of the subtraction test cases.
        /// </summary>
        public static TheoryData<string> SubtractionTestCaseNames => new TheoryData<string>(SubtractionTestCases.Keys);

        /// <summary>
        /// Gets the names of the scalar multiplication test cases.
        /// </summary>
        public static TheoryData<string> ScalarMultiplicationTestCaseNames => new TheoryData<string>(ScalarMultiplicationTestCases.Keys);

        /// <summary>
        /// Gets the names of the element-wise multiplication test cases.
        /// </summary>
        public static TheoryData<string> ElementWiseMultiplicationTestCaseNames
            => new TheoryData<string>(ElementWiseMultiplicationTestCases.Keys);

        /// <summary>
        /// Gets the names of the transpose test cases.
        /// </summary>
        public static TheoryData<string> TransposeTestCaseNames => new TheoryData<string>(TransponseTestCases.Keys);

        #endregion

        #region test cases

        private static Dictionary<string, MatrixEqualityTestCase> EqualityTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixEqualityTestCase>();
                var sameInstance = CreateMatrix(new double[,] { { 1.0 } });
                data.Add(
                    "Same instance",
                    new MatrixEqualityTestCase(
                        left: sameInstance,
                        right: sameInstance,
                        shouldBeEqual: true));
                data.Add(
                    "Right hand side is null",
                    new MatrixEqualityTestCase(
                        left: CreateMatrix(new double[,] { { 1.0 } }),
                        right: null!,
                        shouldBeEqual: false));
                data.Add(
                    "Same dimensions and values",
                    new MatrixEqualityTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        shouldBeEqual: true));
                data.Add(
                    "Same dimensions different values",
                    new MatrixEqualityTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 5.0 },
                        }),
                        shouldBeEqual: false));
                data.Add(
                    "Different row counts",
                    new MatrixEqualityTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 1.0 },
                            { 2.0 },
                        }),
                        shouldBeEqual: false));
                data.Add(
                    "Different column counts",
                    new MatrixEqualityTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                        }),
                        shouldBeEqual: false));
                return data;
            }
        }

#pragma warning disable SA1118 // Parameter should not span multiple lines
        private static Dictionary<string, MatrixVectorArrrayTestCase> ColumnVectorsTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixVectorArrrayTestCase>();
                data.Add(
                    "2x2",
                    new MatrixVectorArrrayTestCase(
                        inputMatrix: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        expectedVectors: new[]
                        {
                            new Vector(new double[] { 1.0, 3.0 }),
                            new Vector(new double[] { 2.0, 4.0 }),
                        }));
                data.Add(
                    "2x3",
                    new MatrixVectorArrrayTestCase(
                        inputMatrix: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0, 3.0 },
                            { 4.0, 5.0, 6.0 },
                        }),
                        expectedVectors: new[]
                        {
                            new Vector(new double[] { 1.0, 4.0 }),
                            new Vector(new double[] { 2.0, 5.0 }),
                            new Vector(new double[] { 3.0, 6.0 }),
                        }));
                return data;
            }
        }

        private static Dictionary<string, MatrixVectorArrrayTestCase> RowVectorsTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixVectorArrrayTestCase>();
                data.Add(
                    "2x2",
                    new MatrixVectorArrrayTestCase(
                        inputMatrix: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        expectedVectors: new[]
                        {
                            new Vector(new double[] { 1.0, 2.0 }),
                            new Vector(new double[] { 3.0, 4.0 }),
                        }));
                data.Add(
                    "2x3",
                    new MatrixVectorArrrayTestCase(
                        inputMatrix: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0, 3.0 },
                            { 4.0, 5.0, 6.0 },
                        }),
                        expectedVectors: new[]
                        {
                            new Vector(new double[] { 1.0, 2.0, 3.0 }),
                            new Vector(new double[] { 4.0, 5.0, 6.0 }),
                        }));
                return data;
            }
        }
#pragma warning restore SA1118 // Parameter should not span multiple lines

        private static Dictionary<string, MatrixMatrixMatrixTestCase> AdditionTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixMatrixMatrixTestCase>();
                data.Add(
                    "2x2",
                    new MatrixMatrixMatrixTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 5.0, 6.0 },
                            { 7.0, 8.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 6.0, 8.0 },
                            { 10.0, 12.0 },
                        })));
                data.Add(
                    "2x3",
                    new MatrixMatrixMatrixTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0, 3.0 },
                            { 4.0, 5.0, 6.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 7.0, 8.0, 9.0 },
                            { 10.0, 11.0, 12.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 8.0, 10.0, 12.0 },
                            { 14.0, 16.0, 18.0 },
                        })));
                return data;
            }
        }

        private static Dictionary<string, MatrixMatrixMatrixTestCase> SubtractionTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixMatrixMatrixTestCase>();
                data.Add(
                    "2x2",
                    new MatrixMatrixMatrixTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 2.0, 4.0 },
                            { 6.0, 8.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 5.0, 6.0 },
                            { 7.0, 8.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { -3.0, -2.0 },
                            { -1.0, 0.0 },
                        })));
                data.Add(
                    "2x3",
                    new MatrixMatrixMatrixTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 2.0, 4.0, 6.0 },
                            { 8.0, 10.0, 12.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 7.0, 8.0, 9.0 },
                            { 10.0, 11.0, 12.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { -5.0, -4.0, -3.0 },
                            { -2.0, -1.0, 0.0 },
                        })));
                return data;
            }
        }

        private static Dictionary<string, MatrixScalarMatrixTestCase> ScalarMultiplicationTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixScalarMatrixTestCase>();
                data.Add(
                    "2x2 times 2.0",
                    new MatrixScalarMatrixTestCase(
                        matrix: CreateMatrix(new double[,]
                        {
                            { 1.5, -2.0 },
                            { 0.0, 4.0 },
                        }),
                        scalar: 2.0,
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 3.0, -4.0 },
                            { 0.0, 8.0 },
                        })));
                data.Add(
                    "2x3 times -1.5",
                    new MatrixScalarMatrixTestCase(
                        matrix: CreateMatrix(new double[,]
                        {
                            { 1.0, -2.0, 3.0 },
                            { -4.0, 5.0, -6.0 },
                        }),
                        scalar: -1.5,
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { -1.5, 3.0, -4.5 },
                            { 6.0, -7.5, 9.0 },
                        })));
                return data;
            }
        }

        private static Dictionary<string, MatrixMatrixMatrixTestCase> ElementWiseMultiplicationTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixMatrixMatrixTestCase>();
                data.Add(
                    "2x2",
                    new MatrixMatrixMatrixTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 5.0, 6.0 },
                            { 7.0, 8.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 5.0, 12.0 },
                            { 21.0, 32.0 },
                        })));
                data.Add(
                    "2x3",
                    new MatrixMatrixMatrixTestCase(
                        left: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0, 3.0 },
                            { 4.0, 5.0, 6.0 },
                        }),
                        right: CreateMatrix(new double[,]
                        {
                            { 7.0, 8.0, 9.0 },
                            { 10.0, 11.0, 12.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 7.0, 16.0, 27.0 },
                            { 40.0, 55.0, 72.0 },
                        })));
                return data;
            }
        }

        private static Dictionary<string, MatrixMatrixTestCase> TransponseTestCases
        {
            get
            {
                var data = new Dictionary<string, MatrixMatrixTestCase>();
                data.Add(
                    "2x2",
                    new MatrixMatrixTestCase(
                        inputMatrix: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0 },
                            { 3.0, 4.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 1.0, 3.0 },
                            { 2.0, 4.0 },
                        })));
                data.Add(
                    "2x3",
                    new MatrixMatrixTestCase(
                        inputMatrix: CreateMatrix(new double[,]
                        {
                            { 1.0, 2.0, 3.0 },
                            { 4.0, 5.0, 6.0 },
                        }),
                        expectedResult: CreateMatrix(new double[,]
                        {
                            { 1.0, 4.0 },
                            { 2.0, 5.0 },
                            { 3.0, 6.0 },
                        })));
                return data;
            }
        }

        #endregion

        #region constructor tests

        /// <summary>
        /// Tests that the constructor throws the correct exception when not all of
        /// the supplied row vectors have the same dimension.
        /// </summary>
        [Fact]
        public void Constructor_RowVectorsNotTheSameLength_Throws()
        {
            // Arrange
            var rowVectors = new[]
            {
                new Vector(new double[] { 1.0, 2.0 }),
                new Vector(new double[] { 3.0 }),
            };

            // Act
            Action act = () => new Matrix(rowVectors);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region ColumnVectors and RowVectors tests

        /// <summary>
        /// Tests that the ColumnVectors property returns the expected column vectors for a given input matrix.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ColumnVectorsTestCaseNames))]
        public void ColumnVectors_ReturnsExpectedVectors(string testCaseName)
        {
            // Arrange
            var testCase = ColumnVectorsTestCases[testCaseName];

            // Act
            var actualColumnVectors = testCase.inputMatrix.ColumnVectors;

            // Assert
            for (var vectorIndex = 0; vectorIndex < testCase.expectedVectors.Length; vectorIndex++)
            {
                actualColumnVectors[vectorIndex].Elements.Should().BeEquivalentTo(
                    testCase.expectedVectors[vectorIndex].Elements,
                    because: $"at vector {vectorIndex}");
            }
        }

        /// <summary>
        /// Tests that the RowVectors property returns the expected row vectors for a given input matrix.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(RowVectorsTestCaseNames))]
        public void RowVectors_ReturnsExpectedVectors(string testCaseName)
        {
            // Arrange
            var testCase = RowVectorsTestCases[testCaseName];

            // Act
            var actualRowVectors = testCase.inputMatrix.RowVectors;

            // Assert
            for (var vectorIndex = 0; vectorIndex < testCase.expectedVectors.Length; vectorIndex++)
            {
                actualRowVectors[vectorIndex].Elements.Should().BeEquivalentTo(
                    testCase.expectedVectors[vectorIndex].Elements,
                    because: $"at vector {vectorIndex}");
            }
        }

        #endregion

        #region equality tests

        /// <summary>
        /// Verifies that the Equals method returns the expected result for the specified test case.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(EqualityTestCaseNames))]
        public void Equals_ReturnsCorrectValue(string testCaseName)
        {
            // Arrange
            var testCase = EqualityTestCases[testCaseName];

            // Act
            var actualResult = testCase.left.Equals(testCase.right);

            // Assert
            actualResult.Should().Be(testCase.shouldBeEqual);
        }

        /// <summary>
        /// Verifie that the != operator returns the expected result for the specified test case.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(EqualityTestCaseNames))]
        public void InequalityOperator_ReturnsCorrectValue(string testCaseName)
        {
            // Arrange
            var testCase = EqualityTestCases[testCaseName];

            // Act
            var actualResult = testCase.left != testCase.right;

            // Assert
            actualResult.Should().Be(!testCase.shouldBeEqual);
        }

        /// <summary>
        /// Tests that using the == operator with a null value on the left hand side returns false.
        /// </summary>
        [Fact]
        public void EqualsOperator_LeftHandSideIsNull_ReturnsFalse()
        {
            // Arrange
            Matrix? a = null;
            var b = CreateMatrix(new double[,]
            {
                { 1.0, 2.0 },
                { 3.0, 4.0 },
            });

            // Act
            var areEqual = a! == b;

            // Assert
            areEqual.Should().BeFalse();
        }

        #endregion

        #region addition tests

        /// <summary>
        /// Ensures adding two matrices of the same dimensions returns an element-wise sum.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(AdditionTestCaseNames))]
        public void Add_HappyPath_ReturnsElementWiseSum(string testCaseName)
        {
            // Arrange
            var testCase = AdditionTestCases[testCaseName];

            // Act
            var actualResult = testCase.left.Add(testCase.right);

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Ensures adding two matrices of the same dimensions using the + operator returns an element-wise sum.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(AdditionTestCaseNames))]
        public void AddOperator_HappyPath_ReturnsElementWiseSum(string testCaseName)
        {
            // Arrange
            var testCase = AdditionTestCases[testCaseName];

            // Act
            var actualResult = testCase.left + testCase.right;

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Verifies that adding matrices of differing dimensions throws an <see cref="ArgumentException"/>.
        /// </summary>
        [Fact]
        public void Add_DifferentDimensions_ThrowsArgumentException()
        {
            // Arrange
            var left = CreateMatrix(new double[,]
            {
                { 1.0, 2.0 },
            });
            var right = CreateMatrix(new double[,]
            {
                { 1.0 },
                { 2.0 },
            });

            // Act
            Action act = () => left.Add(right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region subtraction tests

        /// <summary>
        /// Ensures subtracting two matrices of the same dimensions returns an element-wise difference.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(SubtractionTestCaseNames))]
        public void Subtract_HappyPath_ReturnsElementWiseDifference(string testCaseName)
        {
            // Arrange
            var testCase = SubtractionTestCases[testCaseName];

            // Act
            var actualResult = testCase.left.Subtract(testCase.right);

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Ensures subtracting two matrices of the same dimensions using
        /// the - operator returns an element-wise difference.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(SubtractionTestCaseNames))]
        public void SubtractOperator_HappyPath_ReturnsElementWiseDifference(string testCaseName)
        {
            // Arrange
            var testCase = SubtractionTestCases[testCaseName];

            // Act
            var actualResult = testCase.left - testCase.right;

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Verifies that subtracting matrices of differing dimensions throws an <see cref="ArgumentException"/>.
        /// </summary>
        [Fact]
        public void Subtract_DifferentDimensions_ThrowsArgumentException()
        {
            // Arrange
            var left = CreateMatrix(new double[,]
            {
                { 1.0, 2.0 },
            });
            var right = CreateMatrix(new double[,]
            {
                { 1.0 },
                { 2.0 },
            });

            // Act
            Action act = () => left.Subtract(right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region multiplication tests

        /// <summary>
        /// Tests that multiplying a matrix by a scalar scales every element.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ScalarMultiplicationTestCaseNames))]
        public void Multiply_Scalar_ReturnsScaledMatrix(string testCaseName)
        {
            // Arrange
            var testCase = ScalarMultiplicationTestCases[testCaseName];

            // Act
            var actualResult = testCase.matrix.Multiply(testCase.scalar);

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Tests that element-wise multiplication of two matrices retuns the expected result.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ElementWiseMultiplicationTestCaseNames))]
        public void MultiplyElementWise_ReturnsCorrectMatric(string testCaseName)
        {
            // Arrange
            var testCase = ElementWiseMultiplicationTestCases[testCaseName];

            // Act
            var actualResult = testCase.left.MultiplyElementWise(testCase.right);

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Tests that element-wise multiplication of two matrices is commutative,
        /// i.e. that multiplying left by right produces the same result as multiplying
        /// right by left.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ElementWiseMultiplicationTestCaseNames))]
        public void MultiplyElementWise_IsCommutative(string testCaseName)
        {
            // Arrange
            var testCase = ElementWiseMultiplicationTestCases[testCaseName];

            // Act
            var actualResult = testCase.right.MultiplyElementWise(testCase.left);

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        #endregion

        #region transpose tests

        /// <summary>
        /// Tests that the Transpose method returns the expected result for a given input matrix.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TransposeTestCaseNames))]
        public void Transpose_ReturnsExpectedMatrix(string testCaseName)
        {
            // Arrange
            var testCase = TransponseTestCases[testCaseName];

            // Act
            var actualResult = testCase.inputMatrix.Transpose();

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.expectedResult);
        }

        /// <summary>
        /// Proves that transposing a matrix and then transposing the result returns the
        /// original matrix, i.e. that (A^T)^T = A.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TransposeTestCaseNames))]
        public void Transpose_Twice_ReturnsOriginalMatrix(string testCaseName)
        {
            // Arrange
            var testCase = TransponseTestCases[testCaseName];

            // Act
            var actualResult = testCase.inputMatrix.Transpose().Transpose();

            // Assert
            actualResult.Should().BeEquivalentTo(testCase.inputMatrix);
        }

        /// <summary>
        /// Proves that transposing the sum of two matrices produces the same result as
        /// transposing each matrix and then adding the results, i.e. that (A + B)^T = A^T + B^T.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TransposeTestCaseNames))]
        public void Transpose_DistributesOverAddition(string testCaseName)
        {
            // Arrange
            var testCase = TransponseTestCases[testCaseName];

            // Act
            var leftHandSide = testCase.inputMatrix.Add(testCase.inputMatrix).Transpose();
            var rightHandSide = testCase.inputMatrix.Transpose().Add(testCase.inputMatrix.Transpose());

            // Assert
            leftHandSide.Should().BeEquivalentTo(rightHandSide);
        }

        /// <summary>
        /// Proves that transposing a scalar multiple of a matrix produces the same result as
        /// multiplying the transpose of the matrix by the same scalar, i.e. that (cA)^T = c(A^T).
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TransposeTestCaseNames))]
        public void Transpose_DistributesOverScalarMultiplication(string testCaseName)
        {
            // Arrange
            var testCase = TransponseTestCases[testCaseName];
            var scalar = 2.0;

            // Act
            var leftHandSide = testCase.inputMatrix.Multiply(scalar).Transpose();
            var rightHandSide = testCase.inputMatrix.Transpose().Multiply(scalar);

            // Assert
            leftHandSide.Should().BeEquivalentTo(rightHandSide);
        }

        #endregion

        /// <summary>
        /// Helper method to simplify the syntax of creating matrices in the test data.
        /// </summary>
        /// <param name="data">Data to populate the matrix with.</param>
        /// <returns>The populated matrix.</returns>
        private static Matrix CreateMatrix(double[,] data)
        {
            var rowCount = data.GetLength(0);
            var columnCount = data.GetLength(1);
            var rowVectors = new List<Vector>();
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var rowValues = new double[columnCount];
                for (var colIndex = 0; colIndex < data.GetLength(1); colIndex++)
                {
                    rowValues[colIndex] = data[rowIndex, colIndex];
                }

                rowVectors.Add(new Vector(rowValues));
            }

            return new Matrix(rowVectors.ToArray());
        }
    }
}
