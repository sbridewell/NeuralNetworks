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
        private Vector fittedMeans;
        private Vector fittedStandardDeviations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZScoreScaler"/> class with
        /// pre-fitted means and standard deviations.
        /// </summary>
        /// <param name="means">
        /// Mean values of each of the data items in the samples.
        /// One mean value per data item (feature).
        /// </param>
        /// <param name="standardDeviations">
        /// Standard deviations of each of the data items in the samples.
        /// One standard deviation value per data item (feature).
        /// </param>
        /// <exception cref="ArgumentException">
        /// The dimensions of the means and standard deviations vectors do not match.
        /// </exception>
        public ZScoreScaler(Vector means, Vector standardDeviations)
        {
            if (means.Dimension != standardDeviations.Dimension)
            {
                var msg = "Means and standard deviations must have the same dimension.";
                throw new ArgumentException(msg);
            }

            this.fittedMeans = means;
            this.fittedStandardDeviations = standardDeviations;
        }

        /// <summary>
        /// Fits a ZScoreScaler from a sequence of sample vectors (per-feature
        /// statistics across samples).
        /// </summary>
        /// <param name="samples">
        /// Sequence of sample vectors (each vector is one sample with features as
        /// elements).
        /// </param>
        /// <returns>A fitted <see cref="ZScoreScaler"/>.</returns>
        public static ZScoreScaler Fit(IEnumerable<Vector> samples)
        {
            // TODO: can this Fit functionality be part of IFestureScaler? Is any of its logic common to all implementations?
            if (samples is null)
            {
                throw new ArgumentNullException(nameof(samples));
            }

            var arr = samples.ToArray();
            if (arr.Length == 0)
            {
                var msg = "At least one sample is required to fit the scaler.";
                throw new ArgumentException(msg, nameof(samples));
            }

            int dimension = arr[0].Dimension;
            var means = new double[dimension];
            var sumOfSquares = new double[dimension];
            int n = arr.Length;

            for (int i = 0; i < n; i++)
            {
                var elements = arr[i].Elements;
                if (elements.Length != dimension)
                {
                    var msg = "All sample vectors must have the same dimension.";
                    throw new ArgumentException(msg, nameof(samples));
                }

                for (int j = 0; j < dimension; j++)
                {
                    means[j] += elements[j];
                    sumOfSquares[j] += elements[j] * elements[j];
                }
            }

            for (int j = 0; j < dimension; j++)
            {
                means[j] /= n;
            }

            var standardDeviations = new double[dimension];
            for (int j = 0; j < dimension; j++)
            {
                // population variance
                var variance = (sumOfSquares[j] / n) - (means[j] * means[j]);
                standardDeviations[j] = Math.Sqrt(Math.Max(0.0, variance));
                if (standardDeviations[j] == 0.0)
                {
                    standardDeviations[j] = 1.0; // avoid division-by-zero
                }
            }

            return new ZScoreScaler(new Vector(means), new Vector(standardDeviations));
        }

        /// <inheritdoc/>
        public Vector Scale(Vector unscaledVector)
        {
            var unscaledElements = unscaledVector.Elements;

            // If we've been fitted with per-feature stats then use them
            if (this.fittedMeans.Dimension != unscaledVector.Dimension)
            {
                var msg = "Fitted means and standard deviations must have the same dimension as the unscaled vector.";
                throw new InvalidOperationException(msg);
            }

            var scaled = new double[unscaledElements.Length];
            var means = this.fittedMeans.Elements;
            var standardDeviations = this.fittedStandardDeviations.Elements;
            for (var i = 0; i < scaled.Length; i++)
            {
                scaled[i] = (unscaledElements[i] - means[i]) / standardDeviations[i];
            }

            return new Vector(scaled);

            // TODO: unreachable / unnecessary code?
            ////var mean = unscaledElements.Average();

            ////// TODO: is it right to use sample: false here?
            ////// Probably not, because the point of a neural network is to learn from
            ////// a sample of the population, in order to make predictions about members
            ////// of the population that were not in the sample.
            ////var standardDeviation = unscaledElements.StandardDeviation(sample: false);
            ////var scaledElements = new double[unscaledElements.Length];
            ////if (!double.IsFinite(standardDeviation) || standardDeviation == 0.0)
            ////{
            ////    // If the standard deviation is zero, all values are the same
            ////    var zeroes = new double[unscaledElements.Length];
            ////    return new Vector(zeroes);
            ////}

            ////for (var i = 0; i < unscaledElements.Length; i++)
            ////{
            ////    scaledElements[i] = (unscaledElements[i] - mean) / standardDeviation;
            ////}

            ////// Save single vector stats so ScaleBack can invert the scaling
            ////this.fittedMeans = new Vector(
            ////    Enumerable.Repeat(mean, unscaledElements.Length).ToArray());
            ////var stdsArray = Enumerable.Repeat(standardDeviation == 0.0 ? 1.0 : standardDeviation, unscaledElements.Length).ToArray();
            ////this.fittedStandardDeviations = new Vector(stdsArray);

            ////return new Vector(scaledElements.ToArray());
        }

        /// <inheritdoc/>
        public Vector ScaleBack(Vector scaledVector)
        {
            if (this.fittedMeans.Dimension != scaledVector.Dimension)
            {
                var msg = "Fitted means and standard deviations must have the same dimension as the scaled vector.";
                throw new InvalidOperationException(msg);
            }

            var scaled = scaledVector.Elements;
            var means = this.fittedMeans.Elements;
            var stds = this.fittedStandardDeviations.Elements;
            var scaledBack = new double[scaled.Length];
            for (var i = 0; i < scaled.Length; i++)
            {
                scaledBack[i] = (scaled[i] * stds[i]) + means[i];
            }

            return new Vector(scaledBack);
        }
    }
}
