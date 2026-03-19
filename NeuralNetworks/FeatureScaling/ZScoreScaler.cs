// <copyright file="ZScoreScaler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.FeatureScaling
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// <remarks>
    /// Standardisation is defined as standardised value = (original value - mean) / standard deviation.
    /// Greek letters mu and sigma are often used to represent mean and standard deviation
    /// respectively, so this can be written as standardised value = (original value - mu) / sigma.
    /// The result of standardisation is sometimes called z scores.
    /// Each data item (feature) in the data set must be standardised separately, so that if one
    /// feature is on a much larger scale than another, it does not dominate the learning process.
    /// </remarks>
    /// </summary>
    public class ZScoreScaler : IFeatureScaler
    {
        /// <inheritdoc/>
        public Vector Scale(Vector unscaledVector)
        {
            var unscaledElements = unscaledVector.Elements;
            var mean = unscaledElements.Average();

            // TODO: is it right to use sample: false here?
            // Probably not, because the point of a neural network is to learn from
            // a sample of the population, in order to make predictions about members
            // of the population that were not in the sample.
            var standardDeviation = unscaledElements.StandardDeviation(sample: false);
            var scaledElements = new List<double>();
            foreach (var unscaledElement in unscaledElements)
            {
                scaledElements.Add((unscaledElement - mean) / standardDeviation);
            }

            return new Vector(scaledElements.ToArray());
        }
    }
}
