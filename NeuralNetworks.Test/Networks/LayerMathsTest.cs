// <copyright file="LayerMathsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.Networks
{
    using Sde.NeuralNetworks.ActivationFunctionProviders;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Networks;

    /// <summary>
    /// Unit tests for <see cref="ILayerMaths"/> implementations.
    /// </summary>
    public class LayerMathsTest
    {
        /// <summary>
        /// Gets the names of the ILayerMaths implementations to test.
        /// </summary>
        public static TheoryData<string> TestSubjectNames => new TheoryData<string>(TestSubjects.Keys.ToArray());

        private static Dictionary<string, ILayerMaths> TestSubjects
        {
            get
            {
                var data = new Dictionary<string, ILayerMaths>();
                data.Add(nameof(LayerMaths), new LayerMaths());
                return data;
            }
        }

        /// <summary>
        /// Smoke test for ComputePreActivations: verifies z = W * x + b.
        /// </summary>
        /// <param name="testSubjectName">The name of the ILayerMaths implementation to test.</param>
        [Theory]
        [MemberData(nameof(TestSubjectNames))]
        public void ComputePreActivations_Returns_WxPlusB(string testSubjectName)
        {
            // Arrange
            var testSubject = TestSubjects[testSubjectName];

            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0, 2.0, 3.0 }),   // row 0
                new Vector(new double[] { 4.0, 5.0, 6.0 }),   // row 1
            });

            var biases = new Vector(new double[] { 0.5, -1.0 });
            var inputs = new Vector(new double[] { 2.0, -1.0, 0.5 });

            // Act
            // expected:
            // row0 = 1*2 + 2*(-1) + 3*0.5 + 0.5 = 2 -2 +1.5 +0.5 = 2.0
            // row1 = 4*2 + 5*(-1) + 6*0.5 -1.0 = 8 -5 +3 -1 = 5.0
            var pre = testSubject.ComputePreActivations(weights, biases, inputs);

            // Assert
            Assert.Equal(2.0, pre[0], 6);
            Assert.Equal(5.0, pre[1], 6);
        }

        /// <summary>
        /// Verify ApplyActivation applies the activation elementwise.
        /// </summary>
        /// <param name="testSubjectName">The name of the ILayerMaths implementation to test.</param>
        [Theory]
        [MemberData(nameof(TestSubjectNames))]
        public void ApplyActivation_AppliesFunctionElementwise(string testSubjectName)
        {
            // Arrange
            var testSubject = TestSubjects[testSubjectName];
            var pre = new Vector(new double[] { 2.0, 5.0 });
            var activation = new ScaleActivationProvider(2.0);

            // Act
            var outputs = testSubject.ApplyActivation(pre, activation);

            // Assert
            Assert.Equal(4.0, outputs[0], 6);
            Assert.Equal(10.0, outputs[1], 6);
        }

        /// <summary>
        /// Verify ComputeDeltas uses (expected - output) * f'(preActivation).
        /// </summary>
        /// <param name="testSubjectName">The name of the ILayerMaths implementation to test.</param>
        [Theory]
        [MemberData(nameof(TestSubjectNames))]
        public void ComputeDeltas_CalculatesLocalGradients(string testSubjectName)
        {
            // Arrange
            var testSubject = TestSubjects[testSubjectName];
            var pre = new Vector(new double[] { 2.0, 5.0 });
            var outputs = new Vector(new double[] { 4.0, 10.0 }); // from scale x2 of pre
            var expected = new Vector(new double[] { 3.0, 4.0 });
            var activation = new ScaleActivationProvider(2.0); // gradient = 2

            // Act
            // delta0 = (3 - 4) * 2 = -2
            // delta1 = (4 - 10) * 2 = -12
            var deltas = testSubject.ComputeDeltas(pre, outputs, expected, activation);

            // Assert
            Assert.Equal(-2.0, deltas[0], 6);
            Assert.Equal(-12.0, deltas[1], 6);
        }

        /// <summary>
        /// Verify BuildWeightGradients constructs each row as inputs * delta[r].
        /// </summary>
        /// <param name="testSubjectName">The name of the ILayerMaths implementation to test.</param>
        [Theory]
        [MemberData(nameof(TestSubjectNames))]
        public void BuildWeightGradients_ConstructsCorrectRows(string testSubjectName)
        {
            // Arrange
            var testSubject = TestSubjects[testSubjectName];
            var inputs = new Vector(new double[] { 2.0, -1.0, 0.5 });
            var deltas = new Vector(new double[] { -2.0, -12.0 });

            // Act
            // expected row0 = inputs * -2 => [-4, 2, -1]
            // expected row1 = inputs * -12 => [-24, 12, -6]
            var grads = testSubject.BuildWeightGradients(inputs, deltas);

            // Assert
            Assert.Equal(-4.0, grads.RowVectors[0][0], 6);
            Assert.Equal(2.0, grads.RowVectors[0][1], 6);
            Assert.Equal(-1.0, grads.RowVectors[0][2], 6);

            Assert.Equal(-24.0, grads.RowVectors[1][0], 6);
            Assert.Equal(12.0, grads.RowVectors[1][1], 6);
            Assert.Equal(-6.0, grads.RowVectors[1][2], 6);
        }

        /// <summary>
        /// A test activation provider that scales activation values and returns a constant gradient.
        /// Useful for deterministic unit tests that require predictable activation and gradient behaviour.
        /// </summary>
        private sealed class ScaleActivationProvider : IActivationFunctionProvider
        {
            /// <summary>
            /// The scaling factor applied to activations and gradients.
            /// </summary>
            private readonly double scale;

            /// <summary>
            /// Initializes a new instance of the <see cref="ScaleActivationProvider"/> class.
            /// </summary>
            /// <param name="scale">The factor to multiply inputs and gradients by.</param>
            public ScaleActivationProvider(double scale) => this.scale = scale;

            /// <summary>
            /// Gets a human-readable name for this activation provider.
            /// </summary>
            public string DisplayName => $"Scale({this.scale})";

            /// <summary>
            /// Calculates the activation for the supplied input.
            /// Returns <c>input * scale</c>.
            /// </summary>
            /// <param name="input">The input value.</param>
            /// <returns>The scaled activation value.</returns>
            public double CalculateActivation(double input) => input * this.scale;

            /// <summary>
            /// Calculates the derivative of the activation function.
            /// For the scale activation this is constant and equal to <c>scale</c>.
            /// </summary>
            /// <param name="input">The input value (not used).</param>
            /// <returns>The constant gradient value.</returns>
            public double CalculateGradient(double input) => this.scale;
        }
    }
}
