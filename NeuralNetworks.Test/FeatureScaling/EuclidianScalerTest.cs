// <copyright file="EuclidianScalerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.FeatureScaling
{
    using FluentAssertions;
    using Sde.NeuralNetworks.FeatureScaling;

    /// <summary>
    /// Unit tests for the <see cref="EuclidianScaler"/> class.
    /// </summary>
    public class EuclidianScalerTest
    {
        /// <summary>
        /// Gets a test case for scaling using Euclidean (L2) normalisation.
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
                        expectedScaledVector: new Vector(new double[] { 0.18257418583505536, 0.3651483716701107, 0.5477225575051661, 0.7302967433402214 })));
                data.Add(
                    "-23 55 .02 -10",
                    new TestCase(
                        unscaledVector: new Vector(new double[] { -23, 55, 0.2, -10 }),
                        expectedScaledVector: new Vector(new double[] { -0.38048819544334944, 0.9098630760601835, 0.0033085930038552125, -0.16542965019276062 })));
                return data;
            }
        }

        /// <summary>
        /// Tests that the Euclidian magnitude of the scaled vector is 1.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_EuclidianMagnitudeIsOne(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var scaler = new EuclidianScaler();

            // Act
            var actualScaledVector = scaler.Scale(testCase.unscaledVector);

            // Assert
            actualScaledVector.GetEuclidianMagnitude().Should().BeApproximately(1, 1e-9);
        }

        /// <summary>
        /// Tests that the vector being scaled scales to the expected values.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_ScalesToExpectedValues(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var scaler = new EuclidianScaler();

            // Act
            var actualScaledVector = scaler.Scale(testCase.unscaledVector);

            // Assert
            actualScaledVector.Elements.Should().BeEquivalentTo(testCase.expectedScaledVector.Elements);
        }
    }
}
