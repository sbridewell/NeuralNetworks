// <copyright file="Vector.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a one-dimensional array of double values,
    /// which can be used to represent a vector in the context
    /// of neural networks.
    /// </summary>
    public class Vector(double[] array)
    {
        /// <summary>
        /// Gets the length (number of elements) of the vector.
        /// </summary>
        public int Length => array.Length;

        /// <summary>
        /// Gets or sets the value at the specified index in the vector.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>The value at the specified index.</returns>
        public double this[int index]
        {
            get
            {
                this.ThrowIfIndexOutOfRange(index);
                return array[index];
            }

            set
            {
                this.ThrowIfIndexOutOfRange(index);
                array[index] = value;
            }
        }

        /// <summary>
        /// Adds another vector to this vector and returns the resulting vector.
        /// </summary>
        /// <param name="otherVector">The vector to add to the current vector.</param>
        /// <returns>The sum of the two vectors.</returns>
        public Vector Add(Vector otherVector)
        {
            this.ThrowIfLengthMismatch(otherVector);
            var length = array.Length;
            var resultArray = new double[length];
            for (var i = 0; i < length; i++)
            {
                resultArray[i] = array[i] + otherVector[i];
            }

            return new Vector(resultArray);
        }

        /// <summary>
        /// Multiplies this vector with another vector using the dot product and
        /// returns the resulting scalar value. The dot product is calculated as the
        /// sum of the products of corresponding elements from the two vectors.
        /// </summary>
        /// <param name="otherVector">The other vector.</param>
        /// <returns>The dot product of the two vectors.</returns>
        public double MultiplyUsingDotProduct(Vector otherVector)
        {
            this.ThrowIfLengthMismatch(otherVector);
            var length = array.Length;
            var result = 0.0;
            for (var i = 0; i < length; i++)
            {
                result += array[i] * otherVector[i];
            }

            return result;
        }

        /// <summary>
        /// Multiplies this vector with another vector using element-wise
        /// multiplication and returns the resulting vector.
        /// </summary>
        /// <param name="otherVector">The vector to multiply by.</param>
        /// <returns>
        /// The result of element-wise multiplication of the two vectors.
        /// </returns>
        public Vector MultiplyElementWise(Vector otherVector)
        {
            this.ThrowIfLengthMismatch(otherVector);
            var length = array.Length;
            var resultArray = new double[length];
            for (var i = 0; i < length; i++)
            {
                resultArray[i] = array[i] * otherVector[i];
            }

            return new Vector(resultArray);
        }

        /// <summary>
        /// Multiplies this vector by a scalar value and returns the resulting vector.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>The result of the scalar multiplication.</returns>
        public Vector Multiply(double scalar)
        {
            var length = array.Length;
            var resultArray = new double[length];
            for (var i = 0; i < length; i++)
            {
                resultArray[i] = array[i] * scalar;
            }

            return new Vector(resultArray);
        }

        /// <summary>
        /// Calculates the Euclidean magnitude of the vector represented by the array.
        /// </summary>
        /// <returns>
        /// The magnitude of the vector as a double. The value is always non-negative.
        /// </returns>
        public double GetMagnitude()
        {
            var sumOfSquares = 0.0;
            foreach (var value in array)
            {
                sumOfSquares += value * value;
            }

            return Math.Sqrt(sumOfSquares);
        }

        /// <summary>
        /// Gets the cosine similarity between this vector and another vector.
        /// The cosine similarity is defined as the dot product of the two
        /// vectors divided by the product of their magnitudes.
        /// </summary>
        /// <param name="otherVector">The other vector.</param>
        /// <returns>The cosnie similarity.</returns>
        public double GetCosineSimilarity(Vector otherVector)
        {
            this.ThrowIfLengthMismatch(otherVector);
            var dotProduct = 0.0;
            var magnitudeA = 0.0;
            var magnitudeB = 0.0;
            for (var i = 0; i < array.Length; i++)
            {
                dotProduct += array[i] * otherVector[i];
                magnitudeA += array[i] * array[i];
                magnitudeB += otherVector[i] * otherVector[i];
            }

            ////if (magnitudeA == 0 || magnitudeB == 0)
            ////{
            ////    return 0.0; // Define cosine similarity as 0 if either vector has zero magnitude.
            ////}

            return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }

        [SuppressMessage(
            "Major Code Smell",
            "S112:General or reserved exceptions should never be thrown",
            Justification = "This is the most appropriate exception type for the situation.")]
        private void ThrowIfIndexOutOfRange(int index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException(
                    $"Index {index} is out of range for vector of length {this.Length}.");
            }
        }

        private void ThrowIfLengthMismatch(Vector otherVector)
        {
            if (array.Length != otherVector.Length)
            {
                throw new ArgumentException(
                    $"Vectors must have the same length. "
                    + "Length of the current vector: {this.Length}. "
                    + "Length of the other vector: {otherVector.Length}");
            }
        }
    }
}
