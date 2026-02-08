// <copyright file="QuadraticEquationTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.Quadratics
{
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using FluentAssertions;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// Unit tests for the <see cref="QuadraticEquation"/> class.
    /// </summary>
    public class QuadraticEquationTest
    {
        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> TestCaseNames =>
            new (TestCases.Keys);

        /// <summary>
        /// Gets the test cases.
        /// </summary>
        [SuppressMessage(
            "StyleCop.CSharp.SpacingRules",
            "SA1000:Keywords should be spaced correctly",
            Justification = "Feels like I'm fighting the IDE.")]
        private static Dictionary<string, TestCase> TestCases
        {
            get
            {
                var cases = new Dictionary<string, TestCase>
                {
                    { "x^2=1", new(1, 0, -1, new(1, 0), new(-1, 0)) },
                    { "x^2=4", new(1, 0, -4, new(2, 0), new(-2, 0)) },
                    { "x^2=9", new(1, 0, -9, new(3, 0), new(-3, 0)) },
                    { "x^2=-1", new(1, 0, 1, new(0, 1), new(0, -1)) },
                    { "x^2=-4", new(1, 0, 4, new(0, 2), new(0, -2)) },
                    { "x^2=-9", new(1, 0, 9, new(0, 3), new(0, -3)) },
                };
                return cases;
            }
        }

        private record TestCase(
            double a,
            double b,
            double c,
            Complex expectedRoot1,
            Complex expectedRoot2);

        /// <summary>
        /// Tests various happy path scenarios for the SolveForX method.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(TestCaseNames))]
        public void SolveForX_HappyPath(string testCaseName)
        {
            // Arrange
            var testCase = TestCases[testCaseName];
            var equation = new QuadraticEquation
            {
                A = testCase.a,
                B = testCase.b,
                C = testCase.c,
            };

            // Act
            var results = equation.SolveForX();

            // Assert
            results.Length.Should().Be(2);
            results[0].Should().Be(testCase.expectedRoot1);
            results[1].Should().Be(testCase.expectedRoot2);
        }
    }
}
