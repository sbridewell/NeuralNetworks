// <copyright file="StandardiserTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.FeatureScaling
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Sde.NeuralNetworks.FeatureScaling;

    /// <summary>
    /// Unit tests for the <see cref="Standardiser"/> class.
    /// </summary>
    public class StandardiserTest(ITestOutputHelper output)
    {
        private record TestCase(double[,] data, double[,] standardised);

        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> TestCaseNames => new TheoryData<string>(TestCases.Keys);

        /// <summary>
        /// Gets the actual test cases.
        /// </summary>
        /// <remarks>
        /// Standardised values are calculated using the spreadsheet
        /// "feature scaling checker.xslx", and use population standard
        /// deviation rather than sample standard deviation.
        /// </remarks>
        [SuppressMessage(
            "StyleCop.CSharp.ReadabilityRules",
            "SA1118:Parameter should not span multiple lines",
            Justification = "This is more readable, especially for large datasets")]
        private static Dictionary<string, TestCase> TestCases
        {
            get
            {
                var dictionary = new Dictionary<string, TestCase>();

                dictionary.Add(
                    "2 features, 3 records",
                    new TestCase(
                        new double[,]
                        {
                            { 0, 400 },
                            { 42, -3 },
                            { 5, 12 },
                        }, new double[,]
                        {
                            { -0.836357191, 1.413451335 },
                            { 1.405791874, -0.7469313 },
                            { -0.569434683, -0.666520035 },
                        }));

                dictionary.Add(
                    "2 features, 4 records",
                    new TestCase(
                        new double[,]
                        {
                            { 1, 6 },
                            { 5, -1 },
                            { 3, 4 },
                            { 5, 4 },
                        }, new double[,]
                        {
                            { -1.507556723, 1.063410138 },
                            { 0.904534034, -1.643452031 },
                            { -0.301511345, 0.290020947 },
                            { 0.904534034, 0.290020947 },
                        }));

                return dictionary;
            }
        }

        /// <summary>
        /// Tests that the Scale method returns a dataset in which the mean value
        /// of each feature is one.
        /// </summary>
        /// <param name="testCaseName">
        /// Name of the test case representing the data to be scaled.
        /// </param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_EachFeatureHasAMeanOfZero(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            output.WriteLine($"Number of records: {testCase.data.GetLength(0)}"); // sense check that we've got features and samples the right way round
            output.WriteLine($"Number of features: {testCase.data.GetLength(1)}");
            var standardiser = new Standardiser();

            // Act
            var scaled = standardiser.Scale(testCase.data);

            // Assert
            for (var featureIndex = 0; featureIndex < scaled.GetLength(1); featureIndex++)
            {
                var sum = 0.0;
                for (var sampleIndex = 0; sampleIndex < scaled.GetLength(0); sampleIndex++)
                {
                    sum += scaled[sampleIndex, featureIndex];
                }

                var mean = sum / scaled.GetLength(0);
                var msg = $"The mean of each feature should be zero for feature index {featureIndex}";
                mean.Should().BeApproximately(0, 1e-6, because: msg);
            }
        }

        /// <summary>
        /// Tests that the Scale method returns a dataset in which the standard deviation
        /// of each feature is one.
        /// </summary>
        /// <param name="testCaseName">
        /// Name of the test case representing the data to be scaled.
        /// </param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_EachFeatureHasAStandardDeviationOfOne(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var standardiser = new Standardiser();

            // Act
            var scaled = standardiser.Scale(testCase.data);

            // Assert
            var numberOfRecords = scaled.GetLength(0);
            var numberOfFeatures = scaled.GetLength(1);
            for (var featureIndex = 0; featureIndex < numberOfFeatures; featureIndex++)
            {
                var sum = 0.0;
                for (var sampleIndex = 0; sampleIndex < numberOfRecords; sampleIndex++)
                {
                    sum += scaled[sampleIndex, featureIndex];
                }

                var mean = sum / scaled.GetLength(0);
                var sumOfSquares = 0.0;
                for (var sampleIndex = 0; sampleIndex < numberOfRecords; sampleIndex++)
                {
                    sumOfSquares += Math.Pow(scaled[sampleIndex, featureIndex] - mean, 2);
                }

                // TODO: This is calculating the population standard deviation, but should we be calculating the sample standard deviation instead?
                var standardDeviation = Math.Sqrt(sumOfSquares / scaled.GetLength(0));
                var msg = $"The standard deviation of each feature should be one for feature index {featureIndex}";
                standardDeviation.Should().BeApproximately(1, 1e-6, because: msg);
            }
        }

        /// <summary>
        /// Tests that the Scale method scales the data to the expected values.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_ScalesToExpectedValues(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var standardiser = new Standardiser();

            // Act
            var scaled = standardiser.Scale(testCase.data);

            // Assert
            for (var recordIndex = 0; recordIndex < scaled.GetLength(0); recordIndex++)
            {
                for (var featureIndex = 0; featureIndex < scaled.GetLength(1); featureIndex++)
                {
                    var expectedValue = testCase.standardised[recordIndex, featureIndex];
                    var actualValue = scaled[recordIndex, featureIndex];
                    var msg = $"The scaled value should be {expectedValue} for record index {recordIndex} and feature index {featureIndex}";
                    actualValue.Should().BeApproximately(expectedValue, 1e-6, because: msg);
                }
            }
        }

        /// <summary>
        /// Tests that the Scale method returns a dataset in which the mean value
        /// of each feature is one.
        /// </summary>
        /// <param name="testCaseName">
        /// Name of the test case representing the data to be scaled.
        /// </param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_Matrix_EachFeatureHasAMeanOfZero(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            output.WriteLine($"Number of records: {testCase.data.GetLength(0)}"); // sense check that we've got features and samples the right way round
            output.WriteLine($"Number of features: {testCase.data.GetLength(1)}");
            var standardiser = new Standardiser();

            // Act
            var scaled = standardiser.Scale(new Matrix(testCase.data));

            // Assert
            for (var featureIndex = 0; featureIndex < scaled.ColumnCount; featureIndex++)
            {
                var sum = 0.0;
                for (var sampleIndex = 0; sampleIndex < scaled.RowCount; sampleIndex++)
                {
                    sum += scaled[sampleIndex, featureIndex];
                }

                var mean = sum / scaled.RowCount;
                var msg = $"The mean of each feature should be zero for feature index {featureIndex}";
                mean.Should().BeApproximately(0, 1e-6, because: msg);
            }
        }

        /// <summary>
        /// Tests that the Scale method returns a dataset in which the standard deviation
        /// of each feature is one.
        /// </summary>
        /// <param name="testCaseName">
        /// Name of the test case representing the data to be scaled.
        /// </param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_Matrix_EachFeatureHasAStandardDeviationOfOne(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var standardiser = new Standardiser();

            // Act
            var scaled = standardiser.Scale(new Matrix(testCase.data));

            // Assert
            var numberOfRecords = scaled.RowCount;
            var numberOfFeatures = scaled.ColumnCount;
            for (var featureIndex = 0; featureIndex < numberOfFeatures; featureIndex++)
            {
                var sum = 0.0;
                for (var sampleIndex = 0; sampleIndex < numberOfRecords; sampleIndex++)
                {
                    sum += scaled[sampleIndex, featureIndex];
                }

                var mean = sum / scaled.RowCount;
                var sumOfSquares = 0.0;
                for (var sampleIndex = 0; sampleIndex < numberOfRecords; sampleIndex++)
                {
                    sumOfSquares += Math.Pow(scaled[sampleIndex, featureIndex] - mean, 2);
                }

                // TODO: This is calculating the population standard deviation, but should we be calculating the sample standard deviation instead?
                var standardDeviation = Math.Sqrt(sumOfSquares / scaled.RowCount);
                var msg = $"The standard deviation of each feature should be one for feature index {featureIndex}";
                standardDeviation.Should().BeApproximately(1, 1e-6, because: msg);
            }
        }

        /// <summary>
        /// Tests that the Scale method scales the data to the expected values.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void Scale_Matrix_ScalesToExpectedValues(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var standardiser = new Standardiser();

            // Act
            var scaled = standardiser.Scale(new Matrix(testCase.data));

            // Assert
            for (var recordIndex = 0; recordIndex < scaled.RowCount; recordIndex++)
            {
                for (var featureIndex = 0; featureIndex < scaled.ColumnCount; featureIndex++)
                {
                    var expectedValue = testCase.standardised[recordIndex, featureIndex];
                    var actualValue = scaled[recordIndex, featureIndex];
                    var msg = $"The scaled value should be {expectedValue} for record index {recordIndex} and feature index {featureIndex}";
                    actualValue.Should().BeApproximately(expectedValue, 1e-6, because: msg);
                }
            }
        }
    }
}
