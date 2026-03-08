// <copyright file="StatisticsExtensions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.FeatureScaling
{
    /// <summary>
    /// Extension methods used in statistics calculations.
    /// </summary>
    public static class StatisticsExtensions
    {
        /// <summary>
        /// Gets the standard deviation of the given values, which is a measure of
        /// the amount of variation or dispersion in a set of values.
        /// </summary>
        /// <param name="values">
        /// The values to get the standard deviation of.
        /// </param>
        /// <param name="sample">
        /// True to get the sample standard deviation.
        /// False to get the population standard deviation (default).
        /// </param>
        /// <returns>The standard deviation of the supplied values.</returns>
        /// <remarks>
        /// Standard deviation is calculated slightly differently depending on whether
        /// the values are a sample of the population or the whole population.
        /// A sample will be slightly less spread out than the population, so the
        /// standard deviation of a sample is calculated by dividing the sum of squares
        /// by one less than the number of values in the sample, whereas the standard
        /// deviation of a population is calculated by dividing the sum of squares by
        /// the number of values in the population.
        /// This is called Bessel's correction.
        /// </remarks>
        public static double StandardDeviation(this IEnumerable<double> values, bool sample = false)
        {
            var numberOfValues = values.Count();
            var mean = values.Average();
            var sumOfSquares = values.Select(v => Math.Pow(v - mean, 2)).Sum();
            var divisor = sample ? numberOfValues - 1 : numberOfValues;
            return Math.Sqrt(sumOfSquares / divisor);
        }
    }
}
