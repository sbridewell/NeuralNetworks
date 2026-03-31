// <copyright file="Matrix.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.LinearAlgebra
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

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

        // TODO: constructor which accepts a params array of arrays, e.g. new Matrix({ 1, 2 }, { 3, 4 });

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rowVectors">
        /// An array of vectors, where each vector represents a row of the matrix.
        /// </param>
        public Matrix(Vector[] rowVectors)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(rowVectors);
#endif
            if (rowVectors.Length == 0)
            {
                this.array = new double[0, 0];
                return;
            }

            var rowCount = this.RowCount = rowVectors.Length;
            var columnCount = this.ColumnCount = rowVectors[0].Dimension;
#if DEBUG
            if (!rowVectors.All(v => v.Dimension == columnCount))
            {
                var sb = new StringBuilder();
                for (var vectorIndex = 0; vectorIndex < rowVectors.Length; vectorIndex++)
                {
                    sb.Append($"{rowVectors[vectorIndex].Dimension}");
                    if (vectorIndex < rowVectors.Length - 1)
                    {
                        sb.Append(", ");
                    }
                }

                var msg = "All row vectors must have the same dimension (number of elements). "
                    + $"The supplied vectors have dimensions {sb.ToString()}";
                throw new ArgumentException(msg);
            }
#endif

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

        // TODO: if RowVectors and ColumnVectors are called multiple times, consider caching them - better speed but more memory use

        /// <summary>
        /// Gets an array of vectors, each of which represents a row of the matrix.
        /// </summary>
        public Vector[] RowVectors
        {
            get
            {
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
#if DEBUG
            ArgumentNullException.ThrowIfNull(left);
#endif
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
#if DEBUG
            ArgumentNullException.ThrowIfNull(left);
#endif
            return left.Subtract(right);
        }

        #region scalar multiplication operators

        /// <summary>
        /// Multiplies a matrix by a scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">The matrix to multiply.</param>
        /// <param name="right">The scalar value to multiply by.</param>
        /// <returns>The result oof the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public static Matrix operator *(Matrix left, double right)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(left);
#endif
            return left.Multiply(right);
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">The matrix to multiply.</param>
        /// <param name="right">The scalar value to multiply by.</param>
        /// <returns>The result oof the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public static Matrix operator *(Matrix left, int right)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(left);
#endif
            return left.Multiply(right);
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">The matrix to multiply.</param>
        /// <param name="right">The scalar value to multiply by.</param>
        /// <returns>The result oof the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public static Matrix operator *(Matrix left, decimal right)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(left);
#endif
            return left.Multiply(right);
        }

        /// <summary>
        /// Multiplies a scalar value by a matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">The scalar value to multiply.</param>
        /// <param name="right">The matrix to multiply by.</param>
        /// <returns>The result oof the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public static Matrix operator *(double left, Matrix right)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(right);
#endif
            return right.Multiply(left);
        }

        /// <summary>
        /// Multiplies a scalar value by a matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">The scalar value to multiply.</param>
        /// <param name="right">The matrix to multiply by.</param>
        /// <returns>The result oof the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public static Matrix operator *(int left, Matrix right)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(right);
#endif
            return right.Multiply(left);
        }

        /// <summary>
        /// Multiplies a scalar value by a matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="left">The scalar value to multiply.</param>
        /// <param name="right">The matrix to multiply by.</param>
        /// <returns>The result oof the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public static Matrix operator *(decimal left, Matrix right)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(right);
#endif
            return right.Multiply(left);
        }

        #endregion

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
#if DEBUG
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
#endif
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
        /// Adds the elements of the specified matrix to the current matrix, modifying the current matrix in place.
        /// </summary>
        /// <param name="otherMatrix">The matrix to add.</param>
        public void AddInPlace(Matrix otherMatrix)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
#endif
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    this.array[rowIndex, columnIndex] += otherMatrix.array[rowIndex, columnIndex];
                }
            }
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
#if DEBUG
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
#endif
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
        /// Subtracts the elements of the specified matrix from the current matrix, modifying the current matrix in place.
        /// </summary>
        /// <param name="otherMatrix">The matrix to subtract.</param>
        public void SubtractInPlace(Matrix otherMatrix)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
#endif
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    this.array[rowIndex, columnIndex] -= otherMatrix.array[rowIndex, columnIndex];
                }
            }
        }

        #region scalar multiplication methods

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

        /// <summary>
        /// Multiplies this matrix by the supplied scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="scalar">The value to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public Matrix Multiply(int scalar)
        {
            return this.Multiply((double)scalar);
        }

        /// <summary>
        /// Multiplies this matrix by the supplied scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="scalar">The value to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        [ExcludeFromCodeCoverage]
        public Matrix Multiply(decimal scalar)
        {
            return this.Multiply((double)scalar);
        }

        #endregion

        #region multiply this matrix by a vector

        /// <summary>
        /// Multiplies the current matrix by the supplied vector and returns the result as a new vector.
        /// </summary>
        /// <param name="vector">The vector to multiply by.</param>
        /// <returns>A vector with 1 element per row of the current matrix.</returns>
        public Vector Multiply(Vector vector)
        {
#if DEBUG
            if (this.ColumnCount != vector.Dimension)
            {
                var msg = "In order to multiply a matrix by a vector, the number of columns in the matrix must equal the "
                    + "dimension of the vector. "
                    + $"The matrix has {this.ColumnCount} columns, but the vector has dimension {vector.Dimension}.";
                throw new ArgumentException(msg);
            }
#endif

            var resultElements = new double[this.RowCount];
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                var thisRow = this.RowVectors[rowIndex];
                var dotProduct = thisRow.MultiplyUsingDotProduct(vector);
                resultElements[rowIndex] = dotProduct;
            }

            return new Vector(resultElements);
        }

        #endregion

        #region matrix multiplication

        /// <summary>
        /// Multiplies the current matrix by the supplied matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="otherMatrix">The matrix to multiply by.</param>
        /// <returns>
        /// A matrix composed of the dot products of the rows of the current matrix with the columns
        /// of the supplied matrix.
        /// </returns>
        /// <remarks>
        /// Matrix multiplication is associative, i.e. (AB)C = A(BC).
        /// Matrix multiplication is distributive, i.e. A(B + C) = AB + AC.
        /// Matrix multiplication is not commutative, i.e. AB does not necessarily equal BA.
        /// The non-commutativity is not a limitation, it reflects reality.
        /// Rotating an image and then scaling it does not produce the same result as scaling it and then
        /// rotating it, so the order of operations matters.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// The matrices cannot be multiplied because the number of columns in the first matrix does not equal the
        /// number of rows in the second matrix, or the number of rows in the first matrix does not equal the
        /// number of columns in the second matrix.
        /// </exception>
        public Matrix Multiply(Matrix otherMatrix)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(otherMatrix);
            if (this.RowCount != otherMatrix.ColumnCount || this.ColumnCount != otherMatrix.RowCount)
            {
                var msg = "In order to multiply two matrices, the number of columns in the first matrix must equal the "
                    + "number of rows in the second matrix and the number of rows in the first matrix must equal the number "
                    + "of columns in the second matrix. "
                    + $"The first matrix has dimensions {this.RowCount}x{this.ColumnCount}, but the second matrix has "
                    + $"dimensions {otherMatrix.RowCount}x{otherMatrix.ColumnCount}.";
                throw new ArgumentException(msg);
            }
#endif

            var resultRowCount = this.RowCount;
            var resultColumnCount = otherMatrix.ColumnCount;
            var thisRows = this.RowVectors;
            var otherColumns = otherMatrix.ColumnVectors;
            var resultRowVectors = new Vector[resultRowCount];
            for (var resultRowIndex = 0; resultRowIndex < resultRowCount; resultRowIndex++)
            {
                var resultRow = new double[resultColumnCount];
                for (var resultColumnIndex = 0; resultColumnIndex < resultColumnCount; resultColumnIndex++)
                {
                    var thisRow = thisRows[resultRowIndex];
                    var otherColumn = otherColumns[resultColumnIndex];
                    var dotProduct = thisRow.MultiplyUsingDotProduct(otherColumn);
                    resultRow[resultColumnIndex] = dotProduct;
                }

                resultRowVectors[resultRowIndex] = new Vector(resultRow);
            }

            var result = new Matrix(resultRowVectors);
            return result;
        }

        #endregion

        /// <summary>
        /// Multiplies the current matrix by the supplied matrix element-wise and returns the result
        /// as a new matrix.
        /// Also known as the Hadamard product.
        /// </summary>
        /// <param name="otherMatrix">The matrix to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        public Matrix CalculateHadamardProduct(Matrix otherMatrix)
        {
#if DEBUG
            ArgumentNullException.ThrowIfNull(otherMatrix);
            this.ThrowIfDimensionMismatch(otherMatrix);
#endif
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

        /// <inheritdoc/>
        public override string ToString()
        {
            if (this.RowCount == 0 || this.ColumnCount == 0)
            {
                return string.Empty;
            }

            var elementsAsStrings = new string[this.RowCount, this.ColumnCount];
            var maxWidths = new int[this.ColumnCount];
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    var elementAsString = this.array[rowIndex, columnIndex].ToString("G4");
                    elementsAsStrings[rowIndex, columnIndex] = elementAsString;
                    maxWidths[columnIndex] = Math.Max(maxWidths[columnIndex], elementAsString.Length);
                }
            }

            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    sb.Append(elementsAsStrings[rowIndex, columnIndex].PadLeft(maxWidths[columnIndex]));
                    if (columnIndex < this.ColumnCount - 1)
                    {
                        sb.Append(", ");
                    }
                }

                if (rowIndex < this.RowCount - 1)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        #endregion

        #region private methods

#if DEBUG
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
#endif

        #endregion
    }
}
