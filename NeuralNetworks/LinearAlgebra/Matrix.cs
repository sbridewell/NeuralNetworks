// <copyright file="Matrix.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.LinearAlgebra
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A matrix represents a two-dimensional array of floating point numbers and
    /// implements operations which can be performed on them, such as addition and multiplication.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The size of a matrix is expressed as m x n, where m is the number of rows and n is the
    /// number of columns.
    /// </para>
    /// <para>
    /// If the matrix is a dataset used to train a neural network, then the rows represent the
    /// samples and the columns represent the features.
    /// In the example of house price data, each row is a house, and each column is a data item
    /// such as the price, the number of bedrooms, the size of the house in square feet, the year
    /// it was built, and so on.
    /// </para>
    /// <para>
    /// Other applications of matrices include:
    /// <list type="bullet">
    /// <item>
    /// Holding the weights of the connections between two layers of a neural network, where each
    /// row represents the destinations of the connections and each column represents the sources
    /// of the connections.
    /// </item>
    /// <item>
    /// The identity matrix, which is a square matrix poppulated with ones on the top-left to
    /// bottom-right diagonal and zeroes everywhere else.
    /// Multiplying a matrix by the identity matrix returns the original matrix.
    /// </item>
    /// <item>
    /// The zero matrix, where every element is zero.
    /// </item>
    /// <item>
    /// The diagonal matrix, where only the elements on the top-left to bottom-right diagonal
    /// are populated, and all other elements are zero.
    /// This is used in scaling operations and eigenvalue decomposition.
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// The reasons why matrices are used so widely in machine learning are:
    /// <list type="bullet">
    /// <item>
    /// Training data is organised asmatrices for efficient processing.
    /// </item>
    /// <item>
    /// Neural networks store all learned parameters as matrices.
    /// </item>
    /// <item>
    /// Transforming data from one representation to another is a matrix operation.
    /// </item>
    /// <item>
    /// GPUs are specificcally designed to process matrix operations in parallel, which is why they
    /// are esssential for machine learning.
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    public class Matrix
    {
        /// <summary>
        /// Internal storage for the values in the matrix.
        /// The first index is the row index and the second index is the column index.
        /// </summary>
        private readonly double[,] array;

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rowVectors">
        /// An array of vectors, where each vector represents a row of the matrix.
        /// </param>
        public Matrix(Vector[] rowVectors)
        {
            ArgumentNullException.ThrowIfNull(rowVectors);
            var rowCount = this.RowCount = rowVectors.Length;
            var columnCount = this.ColumnCount = rowVectors[0].Dimension;
            if (!rowVectors.All(v => v.Dimension == columnCount))
            {
                var msg = "All row vectors must have the same dimension (number of elements).";
                throw new ArgumentException(msg);
            }

            this.array = new double[rowCount, columnCount];
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    this.array[rowIndex, columnIndex] = rowVectors[rowIndex][columnIndex];
                }
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the number of rows in the matrix.
        /// Stored as a property for performance reasons - no need to call GetLength(0) on
        /// the array every time we need to know the number of rows.
        /// </summary>
        public int RowCount { get; }

        /// <summary>
        /// Gets the number of columns in the matrix.
        /// Stored as a property for performance reasons - no need to call GetLength(1) on
        /// the array every time we need to know the number of columns.
        /// </summary>
        public int ColumnCount { get; }

        /// <summary>
        /// Gets an array of vectors, each of which represents a row of the matrix.
        /// </summary>
        public Vector[] RowVectors
        {
            get
            {
                // TODO: if this is called multiple times, consider caching it - better speed but more memory use
                var rowVectors = new Vector[this.RowCount];
                for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
                {
                    double[] elements = new double[this.ColumnCount];
                    for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                    {
                        elements[columnIndex] = this.array[rowIndex, columnIndex];
                    }

                    rowVectors[rowIndex] = new Vector(elements);
                }

                return rowVectors;
            }
        }

        /// <summary>
        /// Gets an array of vectors, each of which represents a column of the matrix.
        /// </summary>
        public Vector[] ColumnVectors
        {
            get
            {
                // TODO: if this is called multiple times, consider caching it - better speed but more memory use
                var columnVectors = new Vector[this.ColumnCount];
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    double[] elements = new double[this.RowCount];
                    for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
                    {
                        elements[rowIndex] = this.array[rowIndex, columnIndex];
                    }

                    columnVectors[columnIndex] = new Vector(elements);
                }

                return columnVectors;
            }
        }

        #endregion

        #region operators

        /// <summary>
        /// Tests whether two matrices are value equal.
        /// Because we cannot use == to reliably test for equality of floating point numbers,
        /// we check whether the element values are within 10^-7 of each other.
        /// For efficiency, we return false as soon as we find a condition which indicates
        /// inequality, rather than checking every element of the matrices.
        /// </summary>
        /// <param name="left">Left hand operand.</param>
        /// <param name="right">Right hand operand.</param>
        /// <returns>True if the matrices are value equal, otherwise false.</returns>
        public static bool operator ==(Matrix left, Matrix right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            if (left.RowCount != right.RowCount || left.ColumnCount != right.ColumnCount)
            {
                return false;
            }

            for (var rowIndex = 0; rowIndex < left.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < left.ColumnCount; columnIndex++)
                {
                    var leftValue = left.array[rowIndex, columnIndex];
                    var rightValue = right.array[rowIndex, columnIndex];
                    if (Math.Abs(leftValue - rightValue) > 1e-7)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Tests whether two matrices are not value equal.
        /// </summary>
        /// <param name="left">Left hand operand.</param>
        /// <param name="right">Right hand operand.</param>
        /// <returns>False if the matrices are value equal, otherwise true.</returns>
        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Adds two matrices together and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">Left hand operand.</param>
        /// <param name="right">Right hand operand.</param>
        /// <returns>The element-wise sum of the two matrices.</returns>
        public static Matrix operator +(Matrix left, Matrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return left.Add(right);
        }

        /// <summary>
        /// Subtracts one matrix from another and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">Left hand operand.</param>
        /// <param name="right">Right hand operand.</param>
        /// <returns>The element-wise difference between the two matrices.</returns>
        public static Matrix operator -(Matrix left, Matrix right)
        {
            ArgumentNullException.ThrowIfNull(left);
            return left.Subtract(right);
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != typeof(Matrix))
            {
                return false;
            }

            return this == (Matrix)obj;
        }

        /// <summary>
        /// Returns a hash code for the current matrix.
        /// The has is derived from the matrix dimensions and each element quantised to
        /// 1e-7 so that it is consistent with the equality operator, which uses the
        /// same tolerance.
        /// </summary>
        /// <remarks>
        /// Required by code analysis because the == operator is implemented.
        /// </remarks>
        /// <returns>A hash code for the current matrix.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            const int precision = 7; // to match the tolerance used in the equality operator
            var hc = default(HashCode);
            hc.Add(this.RowCount);
            hc.Add(this.ColumnCount);
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    // Round the value to the specified precision to ensure that values which are considered equal
                    // in the equality operator also produce the same hash code.
                    var roundedValue = Math.Round(this.array[rowIndex, columnIndex], precision);
                    hc.Add(roundedValue);
                }
            }

            return hc.ToHashCode();
        }

        /// <summary>
        /// Adds the supplied matrix to this matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="otherMatrix">The matrix to add.</param>
        /// <returns>The sum of the two matrices.</returns>
        public Matrix Add(Matrix otherMatrix)
        {
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
            var newRowVectors = new List<Vector>();
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                double[] elements = new double[this.ColumnCount];
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    elements[columnIndex] = this.array[rowIndex, columnIndex] + otherMatrix.array[rowIndex, columnIndex];
                }

                newRowVectors.Add(new Vector(elements));
            }

            return new Matrix(newRowVectors.ToArray());
        }

        /// <summary>
        /// Subtracts the supplied matrix from this matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="otherMatrix">The matrix to subtract from the current matrix.</param>
        /// <returns>The result of the subtraction operation.</returns>
        /// <remarks>
        /// Matrix subtraction is used by neural networks during training to update its weights,
        /// by subtracting gradients from the current weights to get the new weights.
        /// </remarks>
        public Matrix Subtract(Matrix otherMatrix)
        {
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
            var newRowVectors = new List<Vector>();
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                double[] elements = new double[this.ColumnCount];
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    elements[columnIndex] = this.array[rowIndex, columnIndex] - otherMatrix.array[rowIndex, columnIndex];
                }

                newRowVectors.Add(new Vector(elements));
            }

            return new Matrix(newRowVectors.ToArray());
        }

        /// <summary>
        /// Multiplies this matrix by the supplied scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="scalar">The value to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        /// <remarks>
        /// Scalar multiplication is used by neural networks during training to update its weights,
        /// by multiplying the learning rate (a small value like 0.01) by the gradients to get the
        /// amount to subtract from the current weights.
        /// </remarks>
        public Matrix Multiply(double scalar)
        {
            var newRowVectors = new Vector[this.RowCount];
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                var elements = new double[this.ColumnCount];
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    elements[columnIndex] = this.array[rowIndex, columnIndex] * scalar;
                }

                newRowVectors[rowIndex] = new Vector(elements);
            }

            return new Matrix(newRowVectors);
        }

        // TODO: is there another way of multiplying two matrices together, in the same way that vectors can be element-wise or dot product multiplied?

        /// <summary>
        /// Multiplies the current matrix by the supplied matrix element-wise and returns the result
        /// as a new matrix.
        /// Also known as the Hadamard product.
        /// </summary>
        /// <param name="otherMatrix">The matrix to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        public Matrix MultiplyElementWise(Matrix otherMatrix)
        {
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
            var newRowVectors = new Vector[this.RowCount];
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                var elements = new double[this.ColumnCount];
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    elements[columnIndex] = this.array[rowIndex, columnIndex] * otherMatrix.array[rowIndex, columnIndex];
                }

                newRowVectors[rowIndex] = new Vector(elements);
            }

            return new Matrix(newRowVectors);
        }

        /// <summary>
        /// Flips the matrix along its top-left to bottom-right diagonal, returning a new matrix
        /// where the rows of the original matrix become the columns and the columns of the original
        /// matrix become the rows.
        /// This method does not mutate the original matrix.
        /// Training of neural networks uses transposition during backpropagation.
        /// </summary>
        /// <returns>The transposed matrix.</returns>
        /// <remarks>
        /// The transposition of a matrix A is represented as A^T (not to be confused with raising
        /// to a power, which uses similar looking syntax).
        /// </remarks>
        public Matrix Transpose()
        {
            var columnVectors = this.ColumnVectors;
            return new Matrix(columnVectors);
        }

        #endregion

        #region private methods

        private void ThrowIfDimensionMismatch(Matrix otherMatrix)
        {
            if (this.RowCount != otherMatrix.RowCount || this.ColumnCount != otherMatrix.ColumnCount)
            {
                throw new ArgumentException(
                    $"Matrices must have the same dimensions. " +
                    $"This matrix has dimensions {this.RowCount}x{this.ColumnCount}, " +
                    $"but the other matrix has dimensions {otherMatrix.RowCount}x{otherMatrix.ColumnCount}.");
            }
        }

        #endregion
    }
}
