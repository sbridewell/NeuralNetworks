// <copyright file="StringToMatrixParser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Parsers
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Parses a string representation of a <see cref="Matrix"/>.
    /// The string must consist of a sequence of <see cref="double"/>
    /// values, separated by commas or spaces.
    /// The string may optionally be enclosed in [] or {} braces.
    /// </summary>
    public class StringToMatrixParser : IStringToMatrixParser
    {
        private readonly char[] lineSeparators = new char[] { '\n', '\r' };
        private readonly char[] valueSeparators = new char[] { ',', ' ', '|', '[', ']', '{', '}' };

        /// <inheritdoc/>
        public Matrix Parse(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            bool success = this.TryParse(value, out Matrix result);
            if (success)
            {
                return result;
            }
            else
            {
                var msg = $"The string '{value}' could not be converted to an instance of {typeof(Vector).FullName}.";
                throw new FormatException(msg);
            }
        }

        /// <inheritdoc/>
        public bool TryParse(string value, out Matrix result)
        {
            var vectors = new List<Vector>();
            var vectorParser = new StringToVectorParser();
            var vectorStrings = value.Split(this.lineSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var vectorString in vectorStrings)
            {
                var vectorSuccess = vectorParser.TryParse(vectorString, out Vector vector);
                if (!vectorSuccess)
                {
                    result = new Matrix(Array.Empty<Vector>());
                    return false;
                }

                vectors.Add(vector);
            }

            result = new Matrix(vectors.ToArray());
            return true;
        }
    }
}
