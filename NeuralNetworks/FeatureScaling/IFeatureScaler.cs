// <copyright file="IFeatureScaler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.FeatureScaling
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Interface for a feature scaler, which is class of stastical techniques
    /// for ensuring that the values of data items (features) in a data set
    /// have a similar scale, which can improve the performance of machine learning
    /// algorithms.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.datacamp.com/tutorial/normalization-vs-standardization">
    /// Normalisation vs Standardisation (datacamp).
    /// </see>
    /// Standardisation and normalisation are two common feature scaling techniques
    /// with the following heirarchy.
    /// <list type="bullet">
    /// <item>Feature scaling.</item>
    /// <list type="bullet">
    /// <item>Normalisation.</item>
    /// <list type="bullet">
    /// <item>Min-max normalisation (scales to a specified range, e.g. 0..1).</item>
    /// <item>Log normalisation (scales to a specified range, e.g. 0..1, using a logarithmic transformation)</item>
    /// <item>Decimal scaling normalisation (scales to a specified range, e.g. 0..1, by dividing by a power of 10).</item>
    /// <item>Mean normalisation (scales to a specified range, e.g. -1..1, by subtracting the mean and dividing by the range).</item>
    /// </list>
    /// <item>Standardisation (no sub techniques known so far).</item>
    /// </list>
    /// </list>
    /// </remarks>
    public interface IFeatureScaler
    {
        /// <summary>
        /// Scales the supplied vector.
        /// </summary>
        /// <param name="unscaledVector">The vector to scale.</param>
        /// <returns>The scaled vector.</returns>
        Vector Scale(Vector unscaledVector);

        // TODO: ScaleBack method, which is the inverse function of the Scale method.
    }
}
