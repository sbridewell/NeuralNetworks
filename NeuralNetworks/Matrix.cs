// <copyright file="Matrix.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A two-dimensional array of double values, which can
    /// be used to represent a matrix in the context of neural
    /// networks.
    /// </summary>
    [SuppressMessage(
        "Blocker Code Smell",
        "S2368:Public methods should not have multidimensional array parameters",
        Justification = "By design")]
    public class Matrix(double[,] array)
    {
        private readonly double[][] rowCache = new double[array.GetLength(0)][];
        private readonly double[][] columnCache = new double[array.GetLength(1)][];

        /// <summary>
        /// Gets the number of rows in the matrix.
        /// </summary>
        public int RowCount => array.GetLength(0);

        /// <summary>
        /// Gets the number of columns in the matrix.
        /// </summary>
        public int ColumnCount => array.GetLength(1);

        /// <summary>
        /// Gets or sets the value at the supplied indices.
        /// </summary>
        /// <param name="columnIndex">X index (column number).</param>
        /// <param name="rowNumber">Y index (row number).</param>
        /// <returns>The value at the supplied indices.</returns>
        public double this[int columnIndex, int rowNumber]
        {
            get
            {
                this.ThrowIfIndexOutOfRange(columnIndex, rowNumber);
                return array[columnIndex, rowNumber];
            }

            set
            {
                this.ThrowIfIndexOutOfRange(columnIndex, rowNumber);
                array[columnIndex, rowNumber] = value;
            }
        }

        /// <summary>
        /// Gets all the values in one row of the current matrix.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index.</param>
        /// <returns>The values in the row.</returns>
        [SuppressMessage(
            "Major Code Smell",
            "S112:General or reserved exceptions should never be thrown",
            Justification = "This is the most appropriate exception type for the situation.")]
        public double[] GetRow(int rowIndex)
        {
            if (this.rowCache[rowIndex] != null)
            {
                return this.rowCache[rowIndex];
            }

            // TODO: would it be better to return a Vector instead of an array?
            if (rowIndex < 0 || rowIndex >= this.RowCount)
            {
                var msg = $"Row index {rowIndex} is out of range for matrix with {this.RowCount} rows.";
                throw new IndexOutOfRangeException(msg);
            }

            var row = new double[this.ColumnCount];
            for (int columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
            {
                row[columnIndex] = array[rowIndex, columnIndex];
            }

            this.rowCache[rowIndex] = row;
            return row;
        }

        /// <summary>
        /// Gets all the values in one column of the current matrix.
        /// </summary>
        /// <param name="columnIndex">The zero-based column index.</param>
        /// <returns>The values in the row.</returns>
        [SuppressMessage(
            "Major Code Smell",
            "S112:General or reserved exceptions should never be thrown",
            Justification = "This is the most appropriate exception type for the situation.")]
        public double[] GetColumn(int columnIndex)
        {
            if (this.columnCache[columnIndex] != null)
            {
                return this.columnCache[columnIndex];
            }

            // TODO: would it be better to return a Vector instead of an array?
            if (columnIndex < 0 || columnIndex >= this.ColumnCount)
            {
                var msg = $"Column index {columnIndex} is out of range for matrix with {this.ColumnCount} columns.";
                throw new IndexOutOfRangeException(msg);
            }

            var column = new double[this.RowCount];
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                column[rowIndex] = array[rowIndex, columnIndex];
            }

            this.columnCache[columnIndex] = column;
            return column;
        }

        /// <summary>
        /// Adds the supplied matrix to this matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="matrix">The matrix to add.</param>
        /// <returns>The sum of the two matrices.</returns>
        public Matrix Add(Matrix matrix)
        {
            this.ThrowIfDimensionMismatch(matrix);
            double[,] resultArray = new double[this.RowCount, this.ColumnCount];
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    resultArray[i, j] = array[i, j] + matrix[i, j];
                }
            }

            return new Matrix(resultArray);
        }

        /// <summary>
        /// Multiplies this matrix by the supplied scalar value and returns the result as a new matrix.
        /// </summary>
        /// <param name="scalar">The value to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        public Matrix Multiply(double scalar)
        {
            double[,] resultArray = new double[this.RowCount, this.ColumnCount];
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    resultArray[i, j] = array[i, j] * scalar;
                }
            }

            return new Matrix(resultArray);
        }

        /// <summary>
        /// Multiplies this matrix by the supplied matrix and returns the result as a new matrix.
        /// </summary>
        /// <param name="otherMatrix">The matrix to multiply by.</param>
        /// <returns>The dot product of the two matrices.</returns>
        public Matrix Multiply(Matrix otherMatrix)
        {
            if (this.ColumnCount != otherMatrix.RowCount)
            {
                throw new ArgumentException(
                    $"Cannot multiply: number of columns in the first matrix ({this.ColumnCount}) " +
                    $"must equal the number of rows in the second matrix ({otherMatrix.RowCount}).");
            }

            double[,] resultArray = new double[this.RowCount, otherMatrix.ColumnCount];
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < otherMatrix.ColumnCount; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < this.ColumnCount; k++)
                    {
                        sum += array[i, k] * otherMatrix[k, j];
                    }

                    resultArray[i, j] = sum;
                }
            }

            return new Matrix(resultArray);
        }

        [SuppressMessage(
            "Major Code Smell",
            "S112:General or reserved exceptions should never be thrown",
            Justification = "This is the most appropriate exception type for the situation.")]
        private void ThrowIfIndexOutOfRange(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= this.RowCount)
            {
                throw new IndexOutOfRangeException(
                    $"Row index {rowIndex} is out of range for matrix with {this.RowCount} rows.");
            }

            if (columnIndex < 0 || columnIndex >= this.ColumnCount)
            {
                throw new IndexOutOfRangeException(
                    $"Column index {columnIndex} is out of range for matrix with {this.ColumnCount} columns.");
            }
        }

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
    }
}
