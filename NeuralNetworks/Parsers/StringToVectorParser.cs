// <copyright file="StringToVectorParser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Parsers
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Parses a string representation of a <see cref="Vector"/>.
    /// The string must consist of a sequence of <see cref="double"/>
    /// values, separated by commas or spaces.
    /// The string may optionally be enclosed in [] or {} braces.
    /// </summary>
    public class StringToVectorParser : IStringToVectorParser
    {
        private readonly char[] separators = { ',', ' ', '[', ']', '{', '}' };

        /// <inheritdoc/>
        public bool TryParse(string value, out Vector result)
        {
            var elementStrings = value.Split(this.separators).Where(e => !string.IsNullOrWhiteSpace(e));
            var elementList = new List<double>();
            foreach (var elementString in elementStrings)
            {
                var elementSuccess = double.TryParse(elementString, out double elementDouble);
                if (!elementSuccess)
                {
                    // The element isn't one of the separator characters, but
                    // also isn't numeric
                    result = new Vector(elementList.ToArray());
                    return false;
                }

                elementList.Add(elementDouble);
            }

            result = new Vector(elementList.ToArray());
            return true;
        }

        /// <inheritdoc/>
        public Vector Parse(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            bool success = this.TryParse(value, out Vector result);
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
    }
}
