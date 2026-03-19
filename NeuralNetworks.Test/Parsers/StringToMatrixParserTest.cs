// <copyright file="StringToMatrixParserTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.Parsers
{
    using FluentAssertions;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Parsers;

    /// <summary>
    /// Unit tests for the <see cref="StringToMatrixParserTest"/> class.
    /// </summary>
    public class StringToMatrixParserTest
    {
        /// <summary>
        /// Test case for parsing a matrix from a string.
        /// </summary>
        /// <param name="stringToParse">The string to parse.</param>
        /// <param name="expectedMatrix">The expected matrix.</param>
        public record ParseTestCase(string stringToParse, Matrix expectedMatrix);

        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> ParseTestCaseNames
            => new TheoryData<string>(ParseTestCases.Keys.ToArray());

#pragma warning disable SA1500 // Braces for multi-line statements should not share line
        private static Dictionary<string, ParseTestCase> ParseTestCases
        {
            get
            {
                var stringToParse = string.Empty;
                var data = new Dictionary<string, ParseTestCase>();
                stringToParse = "1 2 3\n4 5 6\n7 8 9";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        CreateMatrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } })));
                stringToParse = $"1,2,3{Environment.NewLine}4,5,6{Environment.NewLine}7,8,9";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        CreateMatrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } })));
                stringToParse = $"[1,2,3,4][{Environment.NewLine}][ 5, 6, 7, 8 ][{Environment.NewLine}[ 9 10 11 12 ]";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        CreateMatrix(new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } })));
                return data;
            }
        }
#pragma warning restore SA1500 // Braces for multi-line statements should not share line

        /// <summary>
        /// Tests that the Parse method can convert a valid vector string
        /// to a <see cref="Matrix"/> object.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ParseTestCaseNames))]
        public void Parse_HappyPath_ReturnsExpectedMatrix(string testCaseName)
        {
            // Arrange
            var testCase = ParseTestCases[testCaseName];
            var parser = new StringToMatrixParser();

            // Act
            var result = parser.Parse(testCase.stringToParse);

            // Assert
            result.Should().BeEquivalentTo(testCase.expectedMatrix);
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the string
        /// to parse cannot be converted to a <see cref="Matrix"/>.
        /// </summary>
        /// <param name="stringToParse">The string to parse.</param>
        [Theory]
        [InlineData("[1, 2, e, a")]
        public void Parse_InvalidString_Throws(string stringToParse)
        {
            // Arrange
            var parser = new StringToMatrixParser();

            // Act
            var action = () => parser.Parse(stringToParse);

            // Assert
            action.Should().ThrowExactly<FormatException>();
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the string
        /// to parse is null.
        /// </summary>
        [Fact]
        public void Parse_NullString_Throws()
        {
            // Arrange
            var parser = new StringToMatrixParser();

            // Act
            var action = () => parser.Parse(null!);

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        private static Matrix CreateMatrix(double[,] values)
        {
            var rowVectors = new List<Vector>();
            for (var i = 0; i < values.GetLength(0); i++)
            {
                var row = new double[values.GetLength(1)];
                for (var j = 0; j < values.GetLength(1); j++)
                {
                    row[j] = values[i, j];
                }

                var rowVector = new Vector(row);
                rowVectors.Add(rowVector);
            }

            return new Matrix(rowVectors.ToArray());
        }
    }
}
