// <copyright file="StatisticsExtensionsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.FeatureScaling
{
    using FluentAssertions;
    using Sde.NeuralNetworks.FeatureScaling;

    /// <summary>
    /// Unit tests for extension methods in the <see cref="StatisticsExtensions"/> class.
    /// </summary>
    public class StatisticsExtensionsTest
    {
        private record TestCase(
            IEnumerable<double> values,
            double expectedSampleStandardDeviation,
            double expectedPopulationStandardDeviation);

        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> TestCaseNames => new TheoryData<string>(TestCases.Keys.ToArray());

        /// <summary>
        /// Gets the actual test cases.
        /// </summary>
        /// <remarks>
        /// The standard deviations in the test data were calculated using
        /// the spreadsheet "feature scaling checker.xslx", which can be used
        /// to calculate additional test cases if needed.
        /// </remarks>
        private static Dictionary<string, TestCase> TestCases
        {
            get
            {
                var data = new Dictionary<string, TestCase>();

                data.Add(
                    "Feature 1",
                    new TestCase(
                        new double[] { 0, 42, 5 },
                        expectedSampleStandardDeviation: 22.94195574,
                        expectedPopulationStandardDeviation: 18.73202842));

                data.Add(
                    "Feature 2",
                    new TestCase(
                        new double[] { 400, -3, 12 },
                        expectedSampleStandardDeviation: 228.4651688,
                        expectedPopulationStandardDeviation: 186.5410291));

                return data;
            }
        }

        /// <summary>
        /// Tests that the StandardDeviation method returns the expected sample standard deviation
        /// for a given set of values.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void StandardDeviation_SampleStandardDeviation_ReturnsExpectedValue(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var values = testCase.values;

            // Act
            var result = values.StandardDeviation(sample: true);

            // Assert
            result.Should().BeApproximately(testCase.expectedSampleStandardDeviation, 1e-6);
        }

        /// <summary>
        /// Tests that the StandardDeviation method returns the expected sample standard deviation
        /// for a given set of values.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void StandardDeviation_PopulationStandardDeviation_ReturnsExpectedValue(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var values = testCase.values;

            // Act
            var result = values.StandardDeviation(sample: false);

            // Assert
            result.Should().BeApproximately(testCase.expectedPopulationStandardDeviation, 1e-6);
        }
    }
}
