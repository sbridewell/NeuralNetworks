// <copyright file="IParser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Parsers
{
    /// <summary>
    /// Interfaace for parsers which parse a string and return an
    /// instance of <typeparamref name="TReturn"/>.
    /// </summary>
    /// <typeparam name="TReturn">
    /// The type of object returned by the parser.
    /// </typeparam>
    public interface IParser<TReturn>
    {
        /// <summary>
        /// Converts the supplied string representation of a
        /// <typeparamref name="TReturn"/> to its
        /// <typeparamref name="TReturn"/> equivalent.
        /// </summary>
        /// <param name="value">A string containing the value to convert.</param>
        /// <param name="result">
        /// If parsing was successful then a <typeparamref name="TReturn"/>
        /// instance represented by <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// True if parsing was successful, otherwise false.
        /// </returns>
        bool TryParse(string value, out TReturn result);

        /// <summary>
        /// Converts the supplied string representation of a
        /// <typeparamref name="TReturn"/> to its
        /// <typeparamref name="TReturn"/> equivalent.
        /// </summary>
        /// <param name="value">A string containing the value to convert.</param>
        /// <returns>
        /// A <typeparamref name="TReturn"/> instance represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is null.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="value"/> could not be converted to an instance of
        /// <typeparamref name="TReturn"/>.
        /// </exception>
        TReturn Parse(string value);
    }
}
