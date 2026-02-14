// <copyright file="Normaliser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Helper methods for normalising data to fit a given range of values
    /// whilst preserving the distribution of the data.
    /// </summary>
    public static class Normaliser
    {
        /// <summary>
        /// Normalia=ses the supplied array such that the lowest value mapes to the
        /// lowerBound parameter, the highest value maps to the upperBound parameter,
        /// and the distribution of the data is preserved.
        /// </summary>
        /// <param name="upperBound">Inclusive upper bound of the normalised array.</param>
        /// <param name="lowerBound">Inclusive lower bound of the normalised array.</param>
        /// <param name="data">The array to normalise.</param>
        /// <returns>The normalised array.</returns>
        public static int[] Normalise(int upperBound, int lowerBound, int[] data)
        {
            if (data.Length == 0)
            {
                return Array.Empty<int>();
            }

            var dataMin = int.MaxValue;
            var dataMax = int.MinValue;

            // Find the min and max values in the data
            foreach (var value in data)
            {
                if (value < dataMin)
                {
                    dataMin = value;
                }
                else if (value > dataMax)
                {
                    dataMax = value;
                }
            }

            // Handle the case where all values are the same
            if (dataMin == dataMax)
            {
                var midPoint = (upperBound + lowerBound) / 2;
                var uniformData = new int[data.Length];
                for (int i = 0; i < uniformData.Length; i++)
                {
                    uniformData[i] = midPoint;
                }

                return uniformData;
            }

            // Normalise the data
            var normalisedData = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                normalisedData[i] = lowerBound + ((data[i] - dataMin) * (upperBound - lowerBound) / (dataMax - dataMin));
            }

            return normalisedData;
        }

        /// <summary>
        /// Normalizes the specified data to fit within the provided upper and lower
        /// bounds and returns the result as a byte array.
        /// </summary>
        /// <param name="upperBound">
        /// The maximum value of the normalized range. Must be greater than or equal to <paramref name="lowerBound"/>.
        /// </param>
        /// <param name="lowerBound">
        /// The minimum value of the normalized range. Must be less than or equal to <paramref name="upperBound"/>.
        /// </param>
        /// <param name="data">
        /// The array to normalize.
        /// </param>
        /// <returns>
        /// An array containing the normalized data, scaled to fit within the specified bounds.
        /// </returns>
        public static int[] Normalise(int upperBound, int lowerBound, double[] data)
        {
            // TODO: revisit the Normalise logic - networkVisualiser shows flipping from -255 to 255 and also stuck at 0.
            if (data.Length == 0)
            {
                return Array.Empty<int>();
            }

            if (upperBound < lowerBound)
            {
                throw new ArgumentException("Upper bound must be greater than or equal to lower bound.");
            }

            var dataMin = double.MaxValue;
            var dataMax = double.MinValue;

            // Find the min and max values in the data
            foreach (var value in data)
            {
                if (value < dataMin)
                {
                    dataMin = value;
                }

                if (value > dataMax)
                {
                    dataMax = value;
                }
            }

            // Handle the case where all values are the same
            if (Math.Abs(dataMin - dataMax) < 0.001)
            {
                var midPoint = (upperBound + lowerBound) / 2;
                var uniformData = new int[data.Length];
                for (var i = 0; i < uniformData.Length; i++)
                {
                    uniformData[i] = midPoint;
                }

                return uniformData;
            }

            // Normalise the data
            int[] normalisedData = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                var scaled = lowerBound + ((data[i] - dataMin) * (upperBound - lowerBound) / (dataMax - dataMin));
                var rounded = (int)Math.Round(scaled, MidpointRounding.AwayFromZero);
                if (rounded < lowerBound)
                {
                    rounded = lowerBound;
                }
                else if (rounded > upperBound)
                {
                    rounded = upperBound;
                }

                normalisedData[i] = rounded;
            }

            return normalisedData;
        }

        /// <summary>
        /// Normalises a jagged array of double arrays so each inner array is scaled to the
        /// inclusive range defined by <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
        /// The distribution of values is preserved per inner array.
        /// </summary>
        /// <param name="upperBound">
        /// Inclusive upper bound of the normalised arrays.
        /// </param>
        /// <param name="lowerBound">
        /// Inclusive lower bound of the normalised arrays.
        /// </param>
        /// <param name="data">
        /// The jagged array to normalise. Inner arrays may be null (treated as empty).
        /// </param>
        /// <returns>
        /// A jagged int array where each inner array is the normalised equivalent of the corresponding input array.
        /// </returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design")]
        public static int[][] Normalise(int upperBound, int lowerBound, double[][] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (upperBound < lowerBound)
            {
                throw new ArgumentException("Upper bound must be greater than or equal to lower bound.");
            }

            if (data.Length == 0)
            {
                return Array.Empty<int[]>();
            }

            var result = new int[data.Length][];
            for (var i = 0; i < data.Length; i++)
            {
                // Treat null inner arrays as empty arrays to preserve shape
                var row = data[i] ?? Array.Empty<double>();
                result[i] = Normalise(upperBound, lowerBound, row);
            }

            return result;
        }
    }
}
