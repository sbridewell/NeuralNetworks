// <copyright file="Standardiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.FeatureScaling
{
    /// <summary>
    /// <remarks>
    /// Standardsation is defined as standardised value = (original value - mean) / standard deviation.
    /// Greek letters mu and sigma are often used to represent mean and standard deviation
    /// respectively, so this can be written as standardised value = (original value - mu) / sigma.
    /// The result of standardisation is sometimes called z scores.
    /// Each data item (feature) in the data set must be standardised separately, so that if one
    /// feature is on a much larger scale than another, it does not dominate the learning process.
    /// </remarks>
    /// </summary>
    public class Standardiser : IFeatureScaler
    {
        /// <summary>
        /// Standardises the given unscaled values, which are expected to be in the form
        /// of a 2D array, where each row corresponds to a data item and each column
        /// corresponds to a feature.
        /// Standardisation is a feature scaling technique that transforms the data to
        /// have a mean of 0 and a standard deviation of 1, which can improve the
        /// performance of machine learning algorithms by ensuring that all features are
        /// on a similar scale.
        /// </summary>
        /// <param name="unscaledValues"><inheritdoc/></param>
        /// <returns>The scaled values.</returns>
        public double[,] Scale(double[,] unscaledValues)
        {
            var numberOfRecords = unscaledValues.GetLength(0);
            var numberOfFeatures = unscaledValues.GetLength(1);
            var means = new double[numberOfFeatures];
            var stdDevs = new double[numberOfFeatures];
            for (var featureIndex = 0; featureIndex < numberOfFeatures; featureIndex++)
            {
                var featureValues = new double[numberOfRecords];
                for (var recordIndex = 0; recordIndex < numberOfRecords; recordIndex++)
                {
                    featureValues[recordIndex] = unscaledValues[recordIndex, featureIndex];
                }

                means[featureIndex] = featureValues.Average();

                // TODO: is it right to use sample: false here?
                // Probably not, because the point of a neural network is to learn from
                // a sample of the population, in order to make predictions about members
                // of the population that were not in the sample.
                stdDevs[featureIndex] = featureValues.StandardDeviation(sample: false);
            }

            var standardisedData = new double[numberOfRecords, numberOfFeatures];
            for (var recordIndex = 0; recordIndex < numberOfRecords; recordIndex++)
            {
                for (var featureIndex = 0; featureIndex < numberOfFeatures; featureIndex++)
                {
                    // Standard deviation cannot be less than zero.
                    // If this ever happens then there's an error in the implementation of the standard deviation calculation.
                    if (stdDevs[featureIndex] < 0)
                    {
                        var msg = $"Standard deviation cannot be less than zero for feature index {featureIndex}. Value received: {stdDevs[featureIndex]}";
                        throw new InvalidOperationException(msg);
                    }

                    standardisedData[recordIndex, featureIndex]
                        = (unscaledValues[recordIndex, featureIndex] - means[featureIndex])
                        / stdDevs[featureIndex];
                }
            }

            return standardisedData;
        }

        /// <inheritdoc/>
        public Matrix Scale(Matrix unscaledValues)
        {
            var numberOfRecords = unscaledValues.RowCount;
            var numberOfFeatures = unscaledValues.ColumnCount;
            var means = new double[numberOfFeatures];
            var stdDevs = new double[numberOfFeatures];
            for (var featureIndex = 0; featureIndex < numberOfFeatures; featureIndex++)
            {
                var featureValues = unscaledValues.GetColumn(featureIndex);
                means[featureIndex] = featureValues.Average();

                // TODO: is it right to use sample: false here?
                stdDevs[featureIndex] = featureValues.StandardDeviation(sample: false);
            }

            var standardisedData = new double[numberOfRecords, numberOfFeatures];
            for (var recordIndex = 0; recordIndex < numberOfRecords; recordIndex++)
            {
                for (var featureIndex = 0; featureIndex < numberOfFeatures; featureIndex++)
                {
                    // Standard deviation cannot be less than zero.
                    // If this ever happens then there's an error in the implementation of the standard deviation calculation.
                    if (stdDevs[featureIndex] < 0)
                    {
                        var msg = $"Standard deviation cannot be less than zero for feature index {featureIndex}. Value received: {stdDevs[featureIndex]}";
                        throw new InvalidOperationException(msg);
                    }

                    standardisedData[recordIndex, featureIndex]
                        = (unscaledValues.GetRow(recordIndex)[featureIndex] - means[featureIndex])
                        / stdDevs[featureIndex];
                }
            }

            return new Matrix(standardisedData);
        }
    }
}
