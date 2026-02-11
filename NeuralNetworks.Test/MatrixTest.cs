// <copyright file="MatrixTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test
{
    using System;
    using FluentAssertions;
    using Sde.NeuralNetworks;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Matrix"/> class.
    /// </summary>
    public class MatrixTest
    {
        /// <summary>
        /// Verifies that row and column counts and the indexer get and set values correctly.
        /// </summary>
        [Fact]
        public void RowAndColumnAndIndexer_GetAndSet_WorksAsExpected()
        {
            // Arrange
            var data = new double[,]
            {
                { 1.0, 2.0, 3.0 },
                { 4.0, 5.0, 6.0 },
            };
            var m = new Matrix(data);

            // Act
            var rows = m.RowCount;
            var cols = m.ColumnCount;
            var first = m[0, 0];
            var middle = m[0, 1];
            var last = m[1, 2];
            m[0, 1] = 9.5;
            var updated = m[0, 1];

            // Assert
            rows.Should().Be(2);
            cols.Should().Be(3);
            first.Should().Be(1.0);
            middle.Should().Be(2.0);
            last.Should().Be(6.0);
            updated.Should().Be(9.5);
        }

        /// <summary>
        /// Verifies that the indexer throws when indices are out of range.
        /// </summary>
        [Fact]
        public void Indexer_OutOfRange_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            var m = new Matrix(new double[,] { { 1.0 } });

            // Act
            Action negRow = () => { _ = m[-1, 0]; };
            Action negCol = () => { _ = m[0, -1]; };
            Action tooLargeRow = () => { _ = m[1, 0]; };
            Action tooLargeCol = () => { _ = m[0, 1]; };

            // Assert
            negRow.Should().Throw<IndexOutOfRangeException>();
            negCol.Should().Throw<IndexOutOfRangeException>();
            tooLargeRow.Should().Throw<IndexOutOfRangeException>();
            tooLargeCol.Should().Throw<IndexOutOfRangeException>();
        }

        /// <summary>
        /// Ensures adding two matrices of the same dimensions returns an element-wise sum.
        /// </summary>
        [Fact]
        public void Add_SameDimensions_ReturnsElementWiseSum()
        {
            // Arrange
            var a = new Matrix(new double[,]
            {
                { 1.0, 2.0 },
                { 3.0, 4.0 },
            });
            var b = new Matrix(new double[,]
            {
                { 5.0, 6.0 },
                { 7.0, 8.0 },
            });

            // Act
            var sum = a.Add(b);

            // Assert
            sum.RowCount.Should().Be(2);
            sum.ColumnCount.Should().Be(2);
            sum[0, 0].Should().Be(6.0);
            sum[0, 1].Should().Be(8.0);
            sum[1, 0].Should().Be(10.0);
            sum[1, 1].Should().Be(12.0);
        }

        /// <summary>
        /// Verifies that adding matrices of differing dimensions throws an <see cref="ArgumentException"/>.
        /// </summary>
        [Fact]
        public void Add_DifferentDimensions_ThrowsArgumentException()
        {
            // Arrange
            var a = new Matrix(new double[,] { { 1.0, 2.0 } });
            var b = new Matrix(new double[,]
            {
                { 1.0 },
                { 2.0 },
            });

            // Act
            Action act = () => a.Add(b);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        /// <summary>
        /// Ensures multiplying a matrix by a scalar scales every element.
        /// </summary>
        [Fact]
        public void Multiply_Scalar_ReturnsScaledMatrix()
        {
            // Arrange
            var a = new Matrix(new double[,]
            {
                { 1.5, -2.0 },
                { 0.0, 4.0 },
            });

            // Act
            var scaled = a.Multiply(2.0);

            // Assert
            scaled[0, 0].Should().BeApproximately(3.0, 1e-9);
            scaled[0, 1].Should().BeApproximately(-4.0, 1e-9);
            scaled[1, 0].Should().BeApproximately(0.0, 1e-9);
            scaled[1, 1].Should().BeApproximately(8.0, 1e-9);
        }

        /// <summary>
        /// Confirms matrix multiplication produces the expected result for conforming dimensions.
        /// </summary>
        [Fact]
        public void Multiply_Matrix_ValidDimensions_ReturnsProduct()
        {
            // Arrange
            var a = new Matrix(new double[,]
            {
                { 1.0, 2.0, 3.0 },
                { 4.0, 5.0, 6.0 },
            }); // 2x3
            var b = new Matrix(new double[,]
            {
                { 7.0, 8.0 },
                { 9.0, 10.0 },
                { 11.0, 12.0 },
            }); // 3x2

            // Act
            var product = a.Multiply(b); // expected 2x2

            // Assert
            product.RowCount.Should().Be(2);
            product.ColumnCount.Should().Be(2);
            product[0, 0].Should().BeApproximately(58.0, 1e-9);
            product[0, 1].Should().BeApproximately(64.0, 1e-9);
            product[1, 0].Should().BeApproximately(139.0, 1e-9);
            product[1, 1].Should().BeApproximately(154.0, 1e-9);
        }

        /// <summary>
        /// Verifies that multiplying matrices with incompatible dimensions throws an <see cref="ArgumentException"/>.
        /// </summary>
        [Fact]
        public void Multiply_Matrix_InvalidDimensions_ThrowsArgumentException()
        {
            // Arrange
            var a = new Matrix(new double[,]
            {
                { 1.0, 2.0 },
                { 3.0, 4.0 },
            }); // 2x2
            var b = new Matrix(new double[,]
            {
                { 1.0, 2.0 },
                { 3.0, 4.0 },
                { 5.0, 6.0 },
            }); // 3x2

            // Act
            Action act = () => a.Multiply(b);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
