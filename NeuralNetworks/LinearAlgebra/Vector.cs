// <copyright file="Vector.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.LinearAlgebra
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Sde.NeuralNetworks.FeatureScaling;
    using Sde.NeuralNetworks.Parsers;

    /// <summary>
    /// Represents a one-dimensional array of double values,
    /// which can be used to represent a vector in the context
    /// of neural networks.
    /// </summary>
    /// <remarks>
    /// You do not need to supply the <see cref="IFeatureScaler"/> and
    /// <see cref="IStringToVectorParser"/> implementations.
    /// These parameters are only present to allow for mocking and dependency
    /// injection while unit testing, and if not supplied will be initialised
    /// automatically when they are first used.
    /// </remarks>
    public struct Vector(
        double[] array,
        IFeatureScaler? zScoreScaler = null,
        IFeatureScaler? euclidianScaler = null,
        IFeatureScaler? minMaxScaler = null)
    {
        #region properties and indexer

        /// <summary>
        /// Gets the dimension (number of elements) of the vector.
        /// </summary>
        public int Dimension => array.Length;

        /// <summary>
        /// Gets the elements of the vector.
        /// </summary>
        public double[] Elements => array;

        /// <summary>
        /// Gets or sets the value at the specified index in the vector.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>The value at the specified index.</returns>
        public double this[int index]
        {
            get
            {
#if DEBUG
                this.ThrowIfIndexOutOfRange(index);
#endif
                return array[index];
            }

            set
            {
#if DEBUG
                this.ThrowIfIndexOutOfRange(index);
#endif
                array[index] = value;
            }
        }

        #endregion

        #region operator overloads

        /// <summary>
        /// Adds the two vectors together and returns the resulting vector.
        /// </summary>
        /// <param name="left">The vector on the left hand side of the operator.</param>
        /// <param name="right">The vector on the right hand side of the operator.</param>
        /// <returns>The sum of the two vectors.</returns>
        public static Vector operator +(Vector left, Vector right)
        {
            return left.Add(right);
        }

        /// <summary>
        /// Subtracts the vector on the right from the vector on the left and returns the resulting vector.
        /// </summary>
        /// <param name="left">The vector on the left hand side of the operator.</param>
        /// <param name="right">The vector on the right hand side of the operator.</param>
        /// <returns>The difference between the two vectors.</returns>
        public static Vector operator -(Vector left, Vector right)
        {
            return left.Subtract(right);
        }

        /// <summary>
        /// Multiplies the vector by a scalar value and returns the resulting vector.
        /// </summary>
        /// <param name="vector">The vector to multiply.</param>
        /// <param name="scalar">The scalar to multiply.</param>
        /// <returns>The product of the scalar and the vector.</returns>
        public static Vector operator *(Vector vector, double scalar)
        {
            return vector.Multiply(scalar);
        }

        /// <summary>
        /// Multiplies the vector by a scalar value and returns the resulting vector.
        /// </summary>
        /// <param name="scalar">The scalar to multiply.</param>
        /// <param name="vector">The vector to multiply.</param>
        /// <returns>The product of the scalar and the vector.</returns>
        public static Vector operator *(double scalar, Vector vector)
        {
            return vector.Multiply(scalar);
        }

        // TODO: If the * operator is used on two vectors, should the result be the dot product or element-wise multiplication?
        #endregion

        #region equality

        /// <summary>
        /// Tests whether the supplied vector is value equal to the current vector,
        /// within a specified tolerance.
        /// </summary>
        /// <param name="otherVector">The vector to compare.</param>
        /// <param name="tolerance">The tolerance to use in the comparison.</param>
        /// <returns>True if the two vectors are value equal.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the supplied vector has a different dimension (number of
        /// elements) to the current vector.
        /// </exception>"
        public bool IsValueEqualTo(Vector otherVector, double tolerance = 1e-7)
        {
#if DEBUG
            this.ThrowIfLengthMismatch(otherVector);
#endif
            for (var i = 0; i < array.Length; i++)
            {
                if (Math.Abs(array[i] - otherVector[i]) > tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region basic maths methods

        /// <summary>
        /// Adds another vector to this vector and returns the resulting vector.
        /// This method does not mutate the original vector.
        /// </summary>
        /// <param name="otherVector">The vector to add to the current vector.</param>
        /// <returns>The sum of the two vectors.</returns>
        public Vector Add(Vector otherVector)
        {
#if DEBUG
            this.ThrowIfLengthMismatch(otherVector);
#endif
            var length = array.Length;
            var resultArray = new double[length];
            for (var i = 0; i < length; i++)
            {
                resultArray[i] = array[i] + otherVector[i];
            }

            return new Vector(resultArray);
        }

        /// <summary>
        /// Subtracts another vector from this vector and returns the resulting vector.
        /// This method does not mutate the original vector.
        /// </summary>
        /// <param name="otherVector">The vector to subtract from the original vector.</param>
        /// <returns>
        /// The result of subtracting the other vector from the current vector.
        /// </returns>
        public Vector Subtract(Vector otherVector)
        {
#if DEBUG
            this.ThrowIfLengthMismatch(otherVector);
#endif
            var length = array.Length;
            var resultArray = new double[length];
            for (var i = 0; i < length; i++)
            {
                resultArray[i] = array[i] - otherVector[i];
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
#if DEBUG
            this.ThrowIfLengthMismatch(otherVector);
#endif
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
#if DEBUG
            this.ThrowIfLengthMismatch(otherVector);
#endif
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

        #endregion

        #region cosine similarity

        /// <summary>
        /// Gets the cosine similarity between this vector and another vector.
        /// The cosine similarity is defined as the dot product of the two
        /// vectors divided by the product of their magnitudes.
        /// </summary>
        /// <param name="otherVector">The other vector.</param>
        /// <returns>The cosnie similarity.</returns>
        public double GetCosineSimilarity(Vector otherVector)
        {
#if DEBUG
            this.ThrowIfLengthMismatch(otherVector);
#endif
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

        #endregion

        #region magnitude

        /// <summary>
        /// Gets the Euclidian magnitude of the current vector.
        /// This is also known as the L2 norm of the vector, and is calculated as the
        /// square root of the sum of the squares of the elements in the vector, like
        /// Pythagoras' theorem.
        /// </summary>
        /// <returns>
        /// The straight-line distance from the origin to the position represented by the current vector.
        /// </returns>
        public double GetEuclidianMagnitude()
        {
            var sumOfSquares = this.Elements.Sum(e => Math.Pow(e, 2));
            return Math.Sqrt(sumOfSquares);
        }

        /// <summary>
        /// Gets the Manhattan magnitude of the current vector.
        /// This is also known as the L1 norm of the vector, and is calculated as the
        /// sum of the absolute values of each of the elements in the vector.
        /// </summary>
        /// <returns>
        /// The sum of the absolute values of the elements of the vector.
        /// </returns>
        public double GetManhattanMagnitude()
        {
            return this.Elements.Sum(e => Math.Abs(e));
        }

        #endregion

        #region scaling

        /// <summary>
        /// Normmalises the current vector using min-max normalisation and returns the resulting vector.
        /// The current vector is not mutated by this method.
        /// </summary>
        /// <returns>A normalised vector, scaled to a range of 0..1.</returns>
        public Vector ScaleUsingMinMax()
        {
            if (minMaxScaler == null)
            {
                minMaxScaler = new MinMaxScaler();
            }

            return minMaxScaler.Scale(this);
        }

        /// <summary>
        /// Normalises the current vector using L2 (Euclidian) normalisation and returns the resulting vector.
        /// The current vector is not mutated by this method.
        /// </summary>
        /// <returns>A normalised vector, scaled to a unit vector (range -1..1).</returns>
        public Vector ScaleUsingEuclidian()
        {
            if (euclidianScaler == null)
            {
                euclidianScaler = new EuclidianScaler();
            }

            return euclidianScaler.Scale(this);
        }

        /// <summary>
        /// Normalises the current vector using Z-score normalisation and returns the resulting vector.
        /// The current vector is not mutated by this method.
        /// Also known as standardisation.
        /// </summary>
        /// <returns>
        /// The vector, scaled so that it has a mean of zero and a standard deviation of 1.
        /// </returns>
        public Vector ScaleUsingZScores()
        {
            if (zScoreScaler == null)
            {
                zScoreScaler = new ZScoreScaler();
            }

            return zScoreScaler.Scale(this);
        }

        #endregion

        #region ToString

        /// <summary>
        /// Converts the current instance to a string.
        /// Element values are rounded to 4 significant figures, so the
        /// return value is not round-trip convertible to the original
        /// values.
        /// </summary>
        /// <returns>
        /// If we call the elements of the vector a, b and c, then
        /// the return value will be a string of the form "[a, b, c]".
        /// </returns>
        public override string ToString()
        {
            if (this.Elements is null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var element in this.Elements)
            {
                sb.Append($"{element.ToString("G4")}, ");
            }

            // remove final separator
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");
            return sb.ToString();
        }

        #endregion

        #region private methods

#if DEBUG
        [SuppressMessage(
            "Major Code Smell",
            "S112:General or reserved exceptions should never be thrown",
            Justification = "This is the most appropriate exception type for the situation.")]
        private void ThrowIfIndexOutOfRange(int index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException(
                    $"Index {index} is out of range for vector of length {this.Dimension}.");
            }
        }

        private void ThrowIfLengthMismatch(Vector otherVector)
        {
            if (array.Length != otherVector.Dimension)
            {
                throw new ArgumentException(
                    $"Vectors must have the same dimension. "
                    + $"Dimension of the current vector: {this.Dimension}. "
                    + $"Dimension of the other vector: {otherVector.Dimension}");
            }
        }
#endif

    #endregion
    }
}
