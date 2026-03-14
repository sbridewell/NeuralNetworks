// <copyright file="MinMaxScalerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.FeatureScaling
{
    using FluentAssertions;
    using Sde.NeuralNetworks.FeatureScaling;

    /// <summary>
    /// Unit tests for the <see cref="MinMaxScaler"/> class.
    /// </summary>
    public class MinMaxScalerTest
    {
        /// <summary>
        /// Test case for scaling a vector using min-max normalisation.
        /// </summary>
        /// <param name="unscaledVector">The vector to scale.</param>
        /// <param name="expectedScaledVector">The expected scaled vector.</param>
        public record TestCase(Vector unscaledVector, Vector expectedScaledVector);

        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> TestCaseNames => new TheoryData<string>(TestCases.Keys);

        private static Dictionary<string, TestCase> TestCases
        {
            get
            {
                var data = new Dictionary<string, TestCase>();
                data.Add(
                    "1 2 3 4",
                    new TestCase(
                        unscaledVector: new Vector(new double[] { 1, 2, 3, 4 }),
                        expectedScaledVector: new Vector(new double[] { 0, 0.3333333333333333, 0.666666666666666667, 1 })));
                data.Add(
                    "-12 55 0.2 -10",
                    new TestCase(
                        unscaledVector: new Vector(new double[] { -23, 55, 0.2, -10 }),
                        expectedScaledVector: new Vector(new double[] { 0, 1, 0.29743589743589743, 0.1666666666666666666 })));
                return data;
            }
        }

        /// <summary>
        /// Tests that the lowest valued element in the scaled vector is 0.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_MinimumElementIsZero(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var scaler = new MinMaxScaler();

            // Act
            var actualScaledVector = scaler.Scale(testCase.unscaledVector);

            // Assert
            actualScaledVector.Elements.Min().Should().BeApproximately(0, 1e-9);
        }

        /// <summary>
        /// Tests that the highest valued element in the scaled vector is 1.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_MaximumElementIsOne(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var scaler = new MinMaxScaler();

            // Act
            var actualScaledVector = scaler.Scale(testCase.unscaledVector);

            // Assert
            actualScaledVector.Elements.Max().Should().BeApproximately(1, 1e-9);
        }

        /// <summary>
        /// Tests that the values of the elements in the array scale to the
        /// expected values.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_ScalesToExpectedValues(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var scaler = new MinMaxScaler();

            // Act
            var actualScaledVector = scaler.Scale(testCase.unscaledVector);

            // Assert
            actualScaledVector.Elements.Should().BeEquivalentTo(testCase.expectedScaledVector.Elements);
        }
    }
}
