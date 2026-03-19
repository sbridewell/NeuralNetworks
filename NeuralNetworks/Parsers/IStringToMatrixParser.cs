// <copyright file="IStringToMatrixParser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Parsers
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Interface for a parser which parses a string representation
    /// of a <see cref="Matrix"/>.
    /// </summary>
    public interface IStringToMatrixParser : IParser<Matrix>
    {
    }
}
