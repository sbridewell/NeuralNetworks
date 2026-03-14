// <copyright file="VectorTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test
{
    using System;
    using FluentAssertions;
    using Moq;
    using Sde.NeuralNetworks;
    using Sde.NeuralNetworks.FeatureScaling;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Vector"/> class.
    /// </summary>
    public class VectorTest
    {
        #region test case record definitions

        /// <summary>
        /// Test case where the two vectors are of different dimension,
        /// which should cause an exception to be thrown.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public record DifferentDimensionTestCase(Vector left, Vector right);

        /// <summary>
        /// Test case for an equality test.
        /// </summary>
        /// <param name="left">The left hand side operand.</param>
        /// <param name="right">The right hand side operand.</param>
        /// <param name="tolerance">The tolerance to use when comparing.</param>
        /// <param name="expectedResult">Expected result.</param>
        public record EqualityTestCase(Vector left, Vector right, double tolerance, bool expectedResult);

        /// <summary>
        /// Test case for an operation which acts on two vectors and returns
        /// a vector.
        /// </summary>
        /// <param name="left">The left hand side operand.</param>
        /// <param name="right">The right hand side operand.</param>
        /// <param name="expectedResult">Expected result.</param>
        public record TwoVectorOperandsReturnsVectorTestCase(Vector left, Vector right, Vector expectedResult);

        /// <summary>
        /// Test case for a dot product operation.
        /// </summary>
        /// <param name="left">The left hand side operand.</param>
        /// <param name="right">The right hand side operand.</param>
        /// <param name="expectedResult">Expected result.</param>
        public record DotProductTestCase(Vector left, Vector right, double expectedResult);

        /// <summary>
        /// Test case for a scalar multiplication operation.
        /// </summary>
        /// <param name="vector">The vecto to multiply.</param>
        /// <param name="scalar">The scalar to multiply.</param>
        /// <param name="expectedResult">Expected result.</param>
        public record ScalarMultiplicationTestCase(Vector vector, double scalar, Vector expectedResult);

        /// <summary>
        /// Tesst case for a magnitude calculation.
        /// </summary>
        /// <param name="vector">The vector to get the magnitude of.</param>
        /// <param name="expectedMagnitude">The expected magnitude.</param>
        public record MagnitudeTestCase(Vector vector, double expectedMagnitude);

        /// <summary>
        /// Test case for cosine similarity.
        /// </summary>
        /// <param name="left">The left hand side operand.</param>
        /// <param name="right">The right hand side operand.</param>
        /// <param name="expectedResult">Expected result.</param>
        public record CosineSimilarityTestCase(Vector left, Vector right, double expectedResult);

        /// <summary>
        /// Test case for the ToString method.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        /// <param name="expectedString">The expected string representation.</param>
        public record ToStringTestCase(Vector vector, string expectedString);

        #endregion

        #region member test data

        /// <summary>
        /// Gets the names of test cases with vectors of two different dimension.
        /// </summary>
        public static TheoryData<string> DifferentDimensionTestCaseNames
            => new TheoryData<string>(DifferentDimensionTestCases.Keys);

        /// <summary>
        /// Gets the names of the equality test cases.
        /// </summary>
        public static TheoryData<string> EqualityTestCaseNames
            => new TheoryData<string>(EqualityTestCases.Keys);

        /// <summary>
        /// Gets the names of the addition test cases.
        /// </summary>
        public static TheoryData<string> AdditionTestCaseNames
            => new TheoryData<string>(AdditionTestCases.Keys);

        /// <summary>
        /// Gets the names of the subtraction test cases.
        /// </summary>
        public static TheoryData<string> SubtractionTestCaseNames
            => new TheoryData<string>(SubtractionTestCases.Keys);

        /// <summary>
        /// Gets the names of the element-wise multiplication test cases.
        /// </summary>
        public static TheoryData<string> ElementWiseMultiplicationTestCaseNames
            => new TheoryData<string>(ElementwiseMultiplicationTestCases.Keys);

        /// <summary>
        /// Gets the names of the dot product multiplication test cases.
        /// </summary>
        public static TheoryData<string> DotProductTestCaseNames
            => new TheoryData<string>(DotProductTestCases.Keys);

        /// <summary>
        /// Gets the names of the scalar multiplication test cases.
        /// </summary>
        public static TheoryData<string> ScalarMultiplicationTestCaseNames
            => new TheoryData<string>(ScalarMultiplicationTestCases.Keys);

        /// <summary>
        /// Gets the names of the Euclidean magnitude test cases.
        /// </summary>
        public static TheoryData<string> EuclidianMagnitudeTestCaseNames
            => new TheoryData<string>(EuclidianMagnitudeTestCases.Keys);

        /// <summary>
        /// Gets the names of the Manhattan magnitude test cases.
        /// </summary>
        public static TheoryData<string> ManhattanMagnitudeTestCaseNames
            => new TheoryData<string>(ManhattanMagnitudeTestCases.Keys);

        /// <summary>
        /// Gets the names of the cosime similarity test cases.
        /// </summary>
        public static TheoryData<string> CosineSimilarityTestCaseNames
            => new TheoryData<string>(CosineSimilarityTestCases.Keys);

        /// <summary>
        /// Gets the names of the ToString test cases.
        /// </summary>
        public static TheoryData<string> ToStringTestCaseNames
            => new TheoryData<string>(ToStringTestCases.Keys);

        #endregion

        #region test case data

        private static Dictionary<string, DifferentDimensionTestCase> DifferentDimensionTestCases
        {
            get
            {
                var data = new Dictionary<string, DifferentDimensionTestCase>();
                data.Add(
                    "Left longer than right",
                    new DifferentDimensionTestCase(
                        left: new Vector(new double[] { 1.0, 2.0 }),
                        right: new Vector(new double[] { 1.0 })));
                data.Add(
                    "Right longer than left",
                    new DifferentDimensionTestCase(
                        left: new Vector(new double[] { 1.0 }),
                        right: new Vector(new double[] { 1.0, 2.0 })));
                return data;
            }
        }

        private static Dictionary<string, EqualityTestCase> EqualityTestCases
        {
            get
            {
                var data = new Dictionary<string, EqualityTestCase>();
                data.Add(
                    "One element same",
                    new EqualityTestCase(
                        left: new Vector(new double[] { 1.0 }),
                        right: new Vector(new double[] { 1.0 }),
                        tolerance: 1e-10,
                        expectedResult: true));
                data.Add(
                    "One element different",
                    new EqualityTestCase(
                        left: new Vector(new double[] { 42, -5 }),
                        right: new Vector(new double[] { 41, -5 }),
                        tolerance: 1e-10,
                        expectedResult: false));
                return data;
            }
        }

        private static Dictionary<string, TwoVectorOperandsReturnsVectorTestCase> AdditionTestCases
        {
            get
            {
                var data = new Dictionary<string, TwoVectorOperandsReturnsVectorTestCase>();
                data.Add(
                    "One element",
                    new TwoVectorOperandsReturnsVectorTestCase(
                        left: new Vector(new double[] { 2 }),
                        right: new Vector(new double[] { 3 }),
                        expectedResult: new Vector(new double[] { 5 })));
                data.Add(
                    "Two elements",
                    new TwoVectorOperandsReturnsVectorTestCase(
                        left: new Vector(new double[] { 1, -2 }),
                        right: new Vector(new double[] { -1, 4 }),
                        expectedResult: new Vector(new double[] { 0, 2 })));
                return data;
            }
        }

        private static Dictionary<string, TwoVectorOperandsReturnsVectorTestCase> SubtractionTestCases
        {
            get
            {
                var data = new Dictionary<string, TwoVectorOperandsReturnsVectorTestCase>();
                data.Add(
                    "One element",
                    new TwoVectorOperandsReturnsVectorTestCase(
                        left: new Vector(new double[] { 2 }),
                        right: new Vector(new double[] { 3 }),
                        expectedResult: new Vector(new double[] { -1 })));
                data.Add(
                    "Two elements",
                    new TwoVectorOperandsReturnsVectorTestCase(
                        left: new Vector(new double[] { 1, -2 }),
                        right: new Vector(new double[] { -1, 4 }),
                        expectedResult: new Vector(new double[] { 2, -6 })));
                return data;
            }
        }

        private static Dictionary<string, TwoVectorOperandsReturnsVectorTestCase> ElementwiseMultiplicationTestCases
        {
            get
            {
                var data = new Dictionary<string, TwoVectorOperandsReturnsVectorTestCase>();
                data.Add(
                    "One element",
                    new TwoVectorOperandsReturnsVectorTestCase(
                        left: new Vector(new double[] { 2 }),
                        right: new Vector(new double[] { 3 }),
                        expectedResult: new Vector(new double[] { 6 })));
                data.Add(
                    "Two elements",
                    new TwoVectorOperandsReturnsVectorTestCase(
                        left: new Vector(new double[] { 2, 3 }),
                        right: new Vector(new double[] { 4, -1.5 }),
                        expectedResult: new Vector(new double[] { 8, -4.5 })));
                return data;
            }
        }

        private static Dictionary<string, DotProductTestCase> DotProductTestCases
        {
            get
            {
                var data = new Dictionary<string, DotProductTestCase>();
                data.Add(
                    "One element",
                    new DotProductTestCase(
                        left: new Vector(new double[] { 2 }),
                        right: new Vector(new double[] { 3 }),
                        expectedResult: 6.0));
                data.Add(
                    "Two elements",
                    new DotProductTestCase(
                        left: new Vector(new double[] { 2, 3 }),
                        right: new Vector(new double[] { 4, -1.5 }),
                        expectedResult: 8 - 4.5));
                return data;
            }
        }

        private static Dictionary<string, ScalarMultiplicationTestCase> ScalarMultiplicationTestCases
        {
            get
            {
                var data = new Dictionary<string, ScalarMultiplicationTestCase>();
                data.Add(
                    "One element",
                    new ScalarMultiplicationTestCase(
                        vector: new Vector(new double[] { 2 }),
                        scalar: 4,
                        expectedResult: new Vector(new double[] { 8 })));
                data.Add(
                    "Two elements",
                    new ScalarMultiplicationTestCase(
                        vector: new Vector(new double[] { 2, 3 }),
                        scalar: 5,
                        expectedResult: new Vector(new double[] { 10, 15 })));
                return data;
            }
        }

        private static Dictionary<string, MagnitudeTestCase> EuclidianMagnitudeTestCases
        {
            get
            {
                var data = new Dictionary<string, MagnitudeTestCase>();
                data.Add(
                    "Zero vector",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 0, 0 }),
                        expectedMagnitude: 0.0));
                data.Add(
                    "Unit vector 1",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 1, 0 }),
                        expectedMagnitude: 1.0));
                data.Add(
                    "Unit vector 2",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 0, 1 }),
                        expectedMagnitude: 1.0));
                data.Add(
                    "Pythagorean triple",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 3, 4 }),
                        expectedMagnitude: 5.0));
                return data;
            }
        }

        private static Dictionary<string, MagnitudeTestCase> ManhattanMagnitudeTestCases
        {
            get
            {
                var data = new Dictionary<string, MagnitudeTestCase>();
                data.Add(
                    "Zero vector",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 0, 0 }),
                        expectedMagnitude: 0.0));
                data.Add(
                    "Unit vector 1",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 1, 0 }),
                        expectedMagnitude: 1.0));
                data.Add(
                    "Unit vector 2",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 0, 1 }),
                        expectedMagnitude: 1.0));
                data.Add(
                    "Pythagorean triple",
                    new MagnitudeTestCase(
                        vector: new Vector(new double[] { 3, 4 }),
                        expectedMagnitude: 7.0));
                return data;
            }
        }

        private static Dictionary<string, CosineSimilarityTestCase> CosineSimilarityTestCases
        {
            get
            {
                var data = new Dictionary<string, CosineSimilarityTestCase>();
                data.Add(
                    "Same direction",
                    new CosineSimilarityTestCase(
                        left: new Vector(new double[] { 1, 0 }),
                        right: new Vector(new double[] { 2, 0 }),
                        expectedResult: 1.0));
                data.Add(
                    "Opposite direction",
                    new CosineSimilarityTestCase(
                        left: new Vector(new double[] { 1, 0 }),
                        right: new Vector(new double[] { -2, 0 }),
                        expectedResult: -1.0));
                data.Add(
                    "Orthogonal 1",
                    new CosineSimilarityTestCase(
                        left: new Vector(new double[] { 1, 0 }),
                        right: new Vector(new double[] { 0, 1 }),
                        expectedResult: 0.0));
                data.Add(
                    "Orthogonal 2",
                    new CosineSimilarityTestCase(
                        left: new Vector(new double[] { 0, 1 }),
                        right: new Vector(new double[] { 1, 0 }),
                        expectedResult: 0.0));
                return data;
            }
        }

        private static Dictionary<string, ToStringTestCase> ToStringTestCases
        {
            get
            {
                var data = new Dictionary<string, ToStringTestCase>();
                data.Add(
                    "1",
                    new ToStringTestCase(
                        vector: new Vector(new double[] { 1.0 }),
                        expectedString: "[1]"));
                data.Add(
                    "1, -2.5",
                    new ToStringTestCase(
                        vector: new Vector(new double[] { 1.0, -2.5 }),
                        expectedString: "[1, -2.5]"));
                data.Add(
                    "scientific notation",
                    new ToStringTestCase(
                        vector: new Vector(new double[] { 123456789, 0.000000123456789 }),
                        expectedString: "[1.235E+08, 1.235E-07]"));
                return data;
            }
        }

        #endregion

        #region equality tests

        /// <summary>
        /// Tests the <see cref="Vector.IsValueEqualTo"/> method for scenarios where no
        /// exceptions are expected.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(EqualityTestCaseNames))]
        public void IsValueEqualTo_HappyPathTests(string testCaseName)
        {
            // Arrange
            var testCase = EqualityTestCases[testCaseName];

            // Act
            var result = testCase.left.IsValueEqualTo(testCase.right, testCase.tolerance);

            // Assert
            result.Should().Be(testCase.expectedResult);
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the <see cref="Vector.IsValueEqualTo"/>
        /// method is called on two vectors of different dimension.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void IsValueEqualTo_DifferentDimension_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => testCase.left.IsValueEqualTo(testCase.right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region indexer tests

        /// <summary>
        /// Verifies that the <see cref="Vector.Dimension"/> property and the indexer
        /// correctly get and set values.
        /// </summary>
        [Fact]
        public void DimensionAndIndexer_GetAndSet_WorksAsExpected()
        {
            // Arrange
            var data = new double[] { 1.0, 2.0, 3.0 };
            var v = new Vector(data);

            // Act
            var length = v.Dimension;
            var first = v[0];
            var second = v[1];
            var third = v[2];
            v[1] = 4.5;
            var updatedSecond = v[1];

            // Assert
            length.Should().Be(3);
            first.Should().Be(1.0);
            second.Should().Be(2.0);
            third.Should().Be(3.0);
            updatedSecond.Should().Be(4.5);
        }

        /// <summary>
        /// Verifies that accessing the indexer with an out-of-range index
        /// throws an <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public void Indexer_OutOfRange_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            var v = new Vector(new double[] { 1.0 });

            // Act
            Action actNeg = () => { _ = v[-1]; };
            Action actTooLarge = () => { _ = v[1]; };

            // Assert
            actNeg.Should().ThrowExactly<IndexOutOfRangeException>();
            actTooLarge.Should().ThrowExactly<IndexOutOfRangeException>();
        }

        #endregion

        #region addition tests

        /// <summary>
        /// Ensures that adding two vectors of the same length returns
        /// a new vector with element-wise sums.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(AdditionTestCaseNames))]
        public void Add_SameLengthVectors_ReturnsElementWiseSum(string testCaseName)
        {
            // Arrange
            var testCase = AdditionTestCases[testCaseName];

            // Act
            var sum = testCase.left.Add(testCase.right);

            // Assert
            sum.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Verifies that adding vectors of different lengths throws an
        /// <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void Add_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => testCase.left.Add(testCase.right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        /// <summary>
        /// Ensures that adding two vectors of the same length using the + operator
        /// returns a new vector with element-wise sums.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(AdditionTestCaseNames))]
        public void PlusOperator_SameLengthVectors_ReturnsElementWiseSum(string testCaseName)
        {
            // Arrange
            var testCase = AdditionTestCases[testCaseName];

            // Act
            var sum = testCase.left + testCase.right;

            // Assert
            sum.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Verifies that adding vectors of different lengths using the
        /// + operator throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void PlusOperator_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => _ = testCase.left + testCase.right;

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region subtraction tests

        /// <summary>
        /// Tests that subtracting two vectors of the same length returns a new vector
        /// returns a new vector with element-wise differences.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(SubtractionTestCaseNames))]
        public void Subtract_SameLengthVectors_ReturnsElementWiseDifference(string testCaseName)
        {
            // Arrange
            var testCase = SubtractionTestCases[testCaseName];

            // Act
            var difference = testCase.left.Subtract(testCase.right);

            // Assert
            difference.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Verifies that the Subtract method throws an ArgumentException when called
        /// with operands of different lengths.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void Subtract_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => testCase.left.Subtract(testCase.right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        /// <summary>
        /// Tests that subtracting two vectors of the same length using the minus
        /// operator returns a new vector with element-wise differences.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(SubtractionTestCaseNames))]
        public void MinusOpereator_SameLengthVectors_ReturnsElementWiseDifference(string testCaseName)
        {
            // Arrange
            var testCase = SubtractionTestCases[testCaseName];

            // Act
            var difference = testCase.left - testCase.right;

            // Assert
            difference.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Verifies that the minus operator throws an ArgumentException when called
        /// with operands of different lengths.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void MinusOperator_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => _ = testCase.left - testCase.right;

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region dot product multiplication tests

        /// <summary>
        /// Confirms that the dot product implementation returns the expected
        /// scalar value.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DotProductTestCaseNames))]
        public void MultiplyUsingDotProduct_ComputesCorrectScalar(string testCaseName)
        {
            // Arrange
            var testCase = DotProductTestCases[testCaseName];

            // Act
            var result = testCase.left.MultiplyUsingDotProduct(testCase.right);

            // Assert
            result.Should().BeApproximately(testCase.expectedResult, 1e-9);
        }

        /// <summary>
        /// Tests that the correct exception is thrown when trying to perform dot
        /// product multiplication on vectors of different lengths.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void MultiplyUsingDotProduct_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => testCase.left.MultiplyUsingDotProduct(testCase.right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region element-wise multiplication tests

        /// <summary>
        /// Ensures element-wise multiplication produces the expected vector.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ElementWiseMultiplicationTestCaseNames))]
        public void MultiplyElementWise_ReturnsElementWiseProduct(string testCaseName)
        {
            // Arrange
            var testCase = ElementwiseMultiplicationTestCases[testCaseName];

            // Act
            var result = testCase.left.MultiplyElementWise(testCase.right);

            // Assert
            result.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Verifies that the MultiplyElementWise method throws an ArgumentException when called with arrays of
        /// different lengths.
        /// </summary>
        /// <remarks>This test ensures that the MultiplyElementWise method enforces input validation by
        /// throwing an exception when the input arrays do not have matching dimensions.</remarks>
        /// <param name="testCaseName">The name of the test case representing a pair of arrays with different lengths.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void MultiplyElementWise_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => testCase.left.MultiplyElementWise(testCase.right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region scalar multiplication tests

        /// <summary>
        /// Verifies that multiplying a vector by a scalar scales each element
        /// appropriately.
        /// </summary>
        /// <param name="testCaseNames">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ScalarMultiplicationTestCaseNames))]
        public void Multiply_Scalar_ReturnsScaledVector(string testCaseNames)
        {
            // Arrange
            var testCase = ScalarMultiplicationTestCases[testCaseNames];

            // Act
            var result = testCase.vector.Multiply(testCase.scalar);

            // Assert
            result.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Tests that the * operator returns the correct vector when the left
        /// hand operand is a scalar and the right hand operand is a vector.
        /// </summary>
        /// <param name="testCaseNames">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ScalarMultiplicationTestCaseNames))]
        public void StarOperator_ScalarByVector_ReturnsScaledVector(string testCaseNames)
        {
            // Arrange
            var testCase = ScalarMultiplicationTestCases[testCaseNames];

            // Act
            var result = testCase.scalar * testCase.vector;

            // Assert
            result.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        /// <summary>
        /// Tests that the * operator returns the correct vector when the left
        /// hand operand is a vector and the right hand operand is a scalar.
        /// </summary>
        /// <param name="testCaseNames">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ScalarMultiplicationTestCaseNames))]
        public void StarOperator_VectorByScalar_ReturnsScaledVector(string testCaseNames)
        {
            // Arrange
            var testCase = ScalarMultiplicationTestCases[testCaseNames];

            // Act
            var result = testCase.vector * testCase.scalar;

            // Assert
            result.Elements.Should().BeEquivalentTo(testCase.expectedResult.Elements);
        }

        #endregion

        #region magnitude tests

        /// <summary>
        /// Tests that the GetEuclidianMagnitude method returns the expected value.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(EuclidianMagnitudeTestCaseNames))]
        public void GetEuclidianMagnitude_ReturnsExpectedResult(string testCaseName)
        {
            // Arrange
            var testCase = EuclidianMagnitudeTestCases[testCaseName];

            // Act
            var magnitude = testCase.vector.GetEuclidianMagnitude();

            // Assert
            magnitude.Should().BeApproximately(testCase.expectedMagnitude, 1e-9);
        }

        /// <summary>
        /// Tests that the GetManhattanMagnitude method returns the expected value.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ManhattanMagnitudeTestCaseNames))]
        public void GetManhattanMagnitude_ReturnsCorrectMagnitude(string testCaseName)
        {
            // Arrange
            var testCase = ManhattanMagnitudeTestCases[testCaseName];

            // Act
            var magnitude = testCase.vector.GetManhattanMagnitude();

            // Assert
            magnitude.Should().BeApproximately(testCase.expectedMagnitude, 1e-9);
        }

        #endregion

        #region cosine similarity tests

        /// <summary>
        /// Tests that the GetCosineSimilarity method returns the expected value.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(CosineSimilarityTestCaseNames))]
        public void CosineSimilarity_ReturnsExpectedResult(string testCaseName)
        {
            // Arrange
            var testCase = CosineSimilarityTestCases[testCaseName];

            // Act
            var similarity = testCase.left.GetCosineSimilarity(testCase.right);

            // Assert
            similarity.Should().BeApproximately(testCase.expectedResult, 1e-9);
        }

        /// <summary>
        /// Tests that the GetCosineSimilarity method throws the correct exception
        /// when comparing vectors of different lengths..
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(DifferentDimensionTestCaseNames))]
        public void CosineSimilarity_DifferentLength_ThrowsArgumentException(string testCaseName)
        {
            // Arrange
            var testCase = DifferentDimensionTestCases[testCaseName];

            // Act
            Action act = () => testCase.left.GetCosineSimilarity(testCase.right);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        #endregion

        #region scaling tests

        /// <summary>
        /// Tests that the ScaleUsingMinMax method calls the expected scaler and returns the expected result.
        /// </summary>
        [Fact]
        public void ScaleUsingMinMax_CallsMinMaxScaler()
        {
            // Arrange
            var mockScaler = new Mock<IFeatureScaler>();
            var unscaledVector = new Vector(
                new double[] { 1.0, 2.0, 3.0 },
                minMaxScaler: mockScaler.Object);
            var expectedScaledVector = new Vector(new double[] { 0.0, 0.5, 1.0 });
            mockScaler.Setup(s => s.Scale(It.IsAny<Vector>())).Returns(expectedScaledVector);

            // Act
            var actualScaledVector = unscaledVector.ScaleUsingMinMax();

            // Assert
            mockScaler.Verify(s => s.Scale(unscaledVector), Times.Once);
            actualScaledVector.Elements.Should().BeEquivalentTo(expectedScaledVector.Elements);
        }

        /// <summary>
        /// Tests that the ScaleUsingEuclidian method calls the expected scaler and returns the expected result.
        /// </summary>
        [Fact]
        public void ScaleUsingEuclidian_CallsEuclidianScaler()
        {
            // Arrange
            var mockScaler = new Mock<IFeatureScaler>();
            var unscaledVector = new Vector(
                new double[] { 1, 2, 3 },
                euclidianScaler: mockScaler.Object);
            var expectedScaledVector = new Vector(new double[] { 0.26, 0.53, 0.8 });
            mockScaler.Setup(s => s.Scale(It.IsAny<Vector>())).Returns(expectedScaledVector);

            // Act
            var actualScaledVector = unscaledVector.ScaleUsingEuclidian();

            // Assert
            mockScaler.Verify(s => s.Scale(unscaledVector), Times.Once);
            actualScaledVector.Elements.Should().BeEquivalentTo(expectedScaledVector.Elements);
        }

        /// <summary>
        /// Tests that the ScaleUsingZScores method calls the expected scaler and returns the expected result.
        /// </summary>
        [Fact]
        public void ScaleUsingZScores_CallsZScoreScaler()
        {
            // Arrange
            var mockScaler = new Mock<IFeatureScaler>();
            var unscaledVector = new Vector(
                new double[] { 1, 2, 3 },
                zScoreScaler: mockScaler.Object);
            var expectedScaledVector = new Vector(new double[] { -1.0, 0.0, 1.0 });
            mockScaler.Setup(s => s.Scale(It.IsAny<Vector>())).Returns(expectedScaledVector);

            // Act
            var actualScaledVector = unscaledVector.ScaleUsingZScores();

            // Assert
            mockScaler.Verify(s => s.Scale(unscaledVector), Times.Once);
            actualScaledVector.Elements.Should().BeEquivalentTo(expectedScaledVector.Elements);
        }

        /// <summary>
        /// Tests that the caller does not need to supply a Euclidian scaler to the vector constructor.
        /// </summary>
        [Fact]
        public void ScaleUsingEuclidian_NoScalerSupplied_DoesNotThrow()
        {
            // Arrange
            var vector = new Vector(new double[] { 0 });

            // Act
            _ = vector.ScaleUsingEuclidian();

            // Nothing to assert - the Vector instantiated its own EuclidianScaler
            true.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the caller does not need to supply a min-max scaler to the vector constructor.
        /// </summary>
        [Fact]
        public void ScaleUsingMinMax_NoScalersSupplied_DoesNotThrow()
        {
            // Arrange
            var vector = new Vector(new double[] { 0 });

            // Act
            _ = vector.ScaleUsingMinMax();

            // Nothing to assert - the Vector instantiated its own MinMaxScaler
            true.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the caller does not need to supply a z-scores scaler to the vector constructor.
        /// </summary>
        [Fact]
        public void ScaleUsingZScores_NoScalerSupplied_DoesNotThrow()
        {
            // Arrange
            var vector = new Vector(new double[] { 0 });

            // Act
            _ = vector.ScaleUsingZScores();

            // Nothing to assert - the Vector instantiated its own ZScoreScaler
            true.Should().BeTrue();
        }

        #endregion

        #region ToString tests

        /// <summary>
        /// Tests that the ToString method returns the expected string representation..
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ToStringTestCaseNames))]
        public void ToString_HappyPath_ReturnsExpectedString(string testCaseName)
        {
            // Arrange
            var testCase = ToStringTestCases[testCaseName];

            // Act
            var actualString = testCase.vector.ToString();

            // Assert
            actualString.Should().Be(testCase.expectedString);
        }
        #endregion
    }
}
