// <copyright file="ZScoreScalerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.FeatureScaling
{
    using FluentAssertions;
    using Sde.NeuralNetworks.FeatureScaling;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Unit tests for the <see cref="ZScoreScaler"/> class.
    /// </summary>
    public class ZScoreScalerTest()
    {
        private record TestCase(Vector unscaledVector, Vector expectedScaledVector);

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
                    "1 5 3 5",
                    new TestCase(
                        unscaledVector: new Vector(new double[] { 1, 5, 3, 5 }),
                        expectedScaledVector: new Vector(new double[] { -1.507556722888818, 0.9045340337332909, -0.30151134457776363, 0.9045340337332909 })));
                data.Add(
                    "6 -1 4 4",
                    new TestCase(
                        unscaledVector: new Vector(new double[] { 6, -1, 4, 4 }),
                        expectedScaledVector: new Vector(new double[] { 1.0634101379502299, -1.643452031377628, 0.29002094671369905, 0.29002094671369905 })));
                return data;
            }
        }

        // TODO: revisit whether these tests are still needed
        /////// <summary>
        /////// Tests that the mean value in the scaled vector is zero.
        /////// </summary>
        /////// <param name="testCaseName">Name of the test case.</param>
        ////[Theory]
        ////[MemberData(nameof(TestCaseNames))]
        ////public void Scale_MeanIsZero(string testCaseName)
        ////{
        ////    // Arrange
        ////    var testCase = TestCases[testCaseName];
        ////    var scaler = new ZScoreScaler();

        ////    // Act
        ////    var actualScaledVector = scaler.Scale(testCase.unscaledVector);

        ////    // Assert
        ////    actualScaledVector.Elements.Average().Should().BeApproximately(0, 1e-9);
        ////}

        /////// <summary>
        /////// Tests that the standard deviation of the scaled value is 1.
        /////// </summary>
        /////// <param name="testCaseName">Name of the test case.</param>
        ////[Theory]
        ////[MemberData(nameof(TestCaseNames))]
        ////public void Scale_StandardDeviationIsOne(string testCaseName)
        ////{
        ////    // Arrange
        ////    var testCase = TestCases[testCaseName];
        ////    var scaler = new ZScoreScaler();

        ////    // Act
        ////    var actualScaledVector = scaler.Scale(testCase.unscaledVector);

        ////    // Assert
        ////    actualScaledVector.Elements.StandardDeviation().Should().BeApproximately(1, 1e-9);
        ////}

        /////// <summary>
        /////// Tests that the Scale method scales the vector to the expected values.
        /////// </summary>
        /////// <param name="testCaseName">Name of the test case.</param>
        ////[Theory]
        ////[MemberData(nameof(TestCaseNames))]
        ////public void Scale_ScalesToExpectedValues(string testCaseName)
        ////{
        ////    // Arrange
        ////    var testCase = TestCases[testCaseName];
        ////    var scaler = new ZScoreScaler();

        ////    // Act
        ////    var actualScaledVector = scaler.Scale(testCase.unscaledVector);

        ////    // Assert
        ////    actualScaledVector.Elements.Should().BeEquivalentTo(testCase.expectedScaledVector.Elements);
        ////}
    }
}
