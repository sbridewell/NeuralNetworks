// <copyright file="StringToVectorParserTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.Parsers
{
    using FluentAssertions;
    using Sde.NeuralNetworks.Parsers;

    /// <summary>
    /// Unit tests for the <see cref="StringToVectorParser"/> class.
    /// </summary>
    public class StringToVectorParserTest
    {
        /// <summary>
        /// Test case for parsing a vector from a string.
        /// </summary>
        /// <param name="stringToParse">The string to parse.</param>
        /// <param name="expectedVector">The expected vector.</param>
        public record ParseTestCase(string stringToParse, Vector expectedVector);

        /// <summary>
        /// Gets the names of the parse test cases.
        /// </summary>
        public static TheoryData<string> ParseTestCaseNames
            => new TheoryData<string>(ParseTestCases.Keys.ToArray());

        private static Dictionary<string, ParseTestCase> ParseTestCases
        {
            get
            {
                var stringToParse = string.Empty;
                var data = new Dictionary<string, ParseTestCase>();

                stringToParse = "5, 4, 3, 2, 1";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        new Vector(new double[] { 5, 4, 3, 2, 1 })));

                stringToParse = "5 4 3 2 1";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        new Vector(new double[] { 5, 4, 3, 2, 1 })));

                stringToParse = "5  4    3  2 1";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        new Vector(new double[] { 5, 4, 3, 2, 1 })));

                stringToParse = "1 -0.0001 500, 42.4";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        new Vector(new double[] { 1, -1e-4, 0.5e3, 42.4 })));

                // This probably looks like overkill, but just removing all braces
                // is simpler than validating that they only appear at the start
                // and end.
                stringToParse = "[]{}1 -1 0.5], 42.4[]{}";
                data.Add(
                    stringToParse,
                    new ParseTestCase(
                        stringToParse,
                        new Vector(new double[] { 1, -1, 0.5, 42.4 })));

                return data;
            }
        }

        /// <summary>
        /// Testss that the Parse method can convert a valid vector string
        /// to a <see cref="Vector"/> instance.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ParseTestCaseNames))]
        public void Parse_HappyPath_ReturnsCorrectVector(string testCaseName)
        {
            // Arrange
            var testCase = ParseTestCases[testCaseName];
            var parser = new StringToVectorParser();

            // Act
            var actualVector = parser.Parse(testCase.stringToParse);

            // Assert
            actualVector.Elements.Should().BeEquivalentTo(testCase.expectedVector.Elements);
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the string
        /// to parse cannot be converted to a <see cref="Vector"/>.
        /// </summary>
        /// <param name="stringToParse">The string to parse.</param>
        [Theory]
        [InlineData("[1, 2, e, a")]
        public void Parse_InvalidString_Throws(string stringToParse)
        {
            // Arrange
            var parser = new StringToVectorParser();

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
            var parser = new StringToVectorParser();

            // Act
            var action = () => parser.Parse(null!);

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
