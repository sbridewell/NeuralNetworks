// <copyright file="VectorTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test
{
    using System;
    using FluentAssertions;
    using Sde.NeuralNetworks;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Vector"/> class.
    /// </summary>
    public class VectorTest
    {
        /// <summary>
        /// Verifies that the <see cref="Vector.Length"/> property and the indexer
        /// correctly get and set values.
        /// </summary>
        [Fact]
        public void LengthAndIndexer_GetAndSet_WorksAsExpected()
        {
            // Arrange
            var data = new double[] { 1.0, 2.0, 3.0 };
            var v = new Vector(data);

            // Act
            var length = v.Length;
            var first = v[0];
            var second = v[1];
            var third = v[2];
            v[1] = 4.5;
            var updatedSecond = v[1];

            // Assert
            length.Should().Be(3);
            first.Should().Be(1.0);
            second.Should().Be(2.0);
            third.Should().Be(3.0);
            updatedSecond.Should().Be(4.5);
        }

        /// <summary>
        /// Verifies that accessing the indexer with an out-of-range index
        /// throws an <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public void Indexer_OutOfRange_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            var v = new Vector(new double[] { 1.0 });

            // Act
            Action actNeg = () => { _ = v[-1]; };
            Action actTooLarge = () => { _ = v[1]; };

            // Assert
            actNeg.Should().Throw<IndexOutOfRangeException>();
            actTooLarge.Should().Throw<IndexOutOfRangeException>();
        }

        /// <summary>
        /// Ensures that adding two vectors of the same length returns
        /// a new vector with element-wise sums.
        /// </summary>
        [Fact]
        public void Add_SameLengthVectors_ReturnsElementWiseSum()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 2.0, 3.0 });
            var b = new Vector(new double[] { 4.0, 5.0, 6.0 });

            // Act
            var sum = a.Add(b);

            // Assert
            sum.Length.Should().Be(3);
            sum[0].Should().Be(5.0);
            sum[1].Should().Be(7.0);
            sum[2].Should().Be(9.0);
        }

        /// <summary>
        /// Verifies that adding vectors of different lengths throws an
        /// <see cref="ArgumentException"/>.
        /// </summary>
        [Fact]
        public void Add_DifferentLength_ThrowsArgumentException()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 2.0 });
            var b = new Vector(new double[] { 1.0 });

            // Act
            Action act = () => a.Add(b);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        /// <summary>
        /// Confirms that the dot product implementation returns the expected
        /// scalar value.
        /// </summary>
        [Fact]
        public void MultiplyUsingDotProduct_ComputesCorrectScalar()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 3.0, -5.0 });
            var b = new Vector(new double[] { 4.0, -2.0, -1.0 });

            // Act
            // dot = 1*4 + 3*(-2) + (-5)*(-1) = 4 -6 +5 = 3
            var dot = a.MultiplyUsingDotProduct(b);

            // Assert
            dot.Should().BeApproximately(3.0, 1e-9);
        }

        /// <summary>
        /// Ensures element-wise multiplication produces the expected vector.
        /// </summary>
        [Fact]
        public void MultiplyElementWise_ReturnsElementWiseProduct()
        {
            // Arrange
            var a = new Vector(new double[] { 2.0, 3.0 });
            var b = new Vector(new double[] { 4.0, -1.5 });

            // Act
            var product = a.MultiplyElementWise(b);

            // Assert
            product.Length.Should().Be(2);
            product[0].Should().Be(8.0);
            product[1].Should().BeApproximately(-4.5, 1e-9);
        }

        /// <summary>
        /// Verifies that multiplying a vector by a scalar scales each element
        /// appropriately.
        /// </summary>
        [Fact]
        public void Multiply_Scalar_ReturnsScaledVector()
        {
            // Arrange
            var a = new Vector(new double[] { 1.5, -2.0 });

            // Act
            var scaled = a.Multiply(2.0);

            // Assert
            scaled.Length.Should().Be(2);
            scaled[0].Should().BeApproximately(3.0, 1e-9);
            scaled[1].Should().BeApproximately(-4.0, 1e-9);
        }

        /// <summary>
        /// Checks that the Euclidean magnitude is computed correctly.
        /// </summary>
        [Fact]
        public void GetMagnitude_ComputesEuclideanNorm()
        {
            // Arrange
            var v = new Vector(new double[] { 3.0, 4.0 });

            // Act
            var magnitude = v.GetMagnitude();

            // Assert
            magnitude.Should().BeApproximately(5.0, 1e-9);
        }

        /// <summary>
        /// Ensures cosine similarity is 1 for vectors pointing in the same direction.
        /// </summary>
        [Fact]
        public void GetCosineSimilarity_SameDirection_IsOne()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 0.0 });
            var b = new Vector(new double[] { 2.0, 0.0 });

            // Act
            var similarity = a.GetCosineSimilarity(b);

            // Assert
            similarity.Should().BeApproximately(1.0, 1e-9);
        }

        /// <summary>
        /// Ensures cosine similarity is -1 for vectors pointing in opposite directions.
        /// </summary>
        [Fact]
        public void GetCosineSimilarity_OppositeDirection_IsMinusOne()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 0.0 });
            var b = new Vector(new double[] { -2.0, 0.0 });

            // Act
            var similarity = a.GetCosineSimilarity(b);

            // Assert
            similarity.Should().BeApproximately(-1.0, 1e-9);
        }

        /// <summary>
        /// Ensures cosine similarity is approximately 0 for orthogonal vectors.
        /// </summary>
        [Fact]
        public void GetCosineSimilarity_Orthogonal_IsZero()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 0.0 });
            var b = new Vector(new double[] { 0.0, 1.0 });

            // Act
            var similarity = a.GetCosineSimilarity(b);

            // Assert
            similarity.Should().BeApproximately(0.0, 1e-9);
        }

        /// <summary>
        /// Verifies that operations that require equal-length vectors throw
        /// an <see cref="ArgumentException"/> when lengths differ.
        /// </summary>
        [Fact]
        public void Operations_DifferentLength_ThrowArgumentException()
        {
            // Arrange
            var a = new Vector(new double[] { 1.0, 2.0, 3.0 });
            var b = new Vector(new double[] { 1.0, 2.0 });

            // Act
            Action add = () => a.Add(b);
            Action dot = () => a.MultiplyUsingDotProduct(b);
            Action elem = () => a.MultiplyElementWise(b);
            Action cos = () => a.GetCosineSimilarity(b);

            // Assert
            add.Should().Throw<ArgumentException>();
            dot.Should().Throw<ArgumentException>();
            elem.Should().Throw<ArgumentException>();
            cos.Should().Throw<ArgumentException>();
        }
    }
}
