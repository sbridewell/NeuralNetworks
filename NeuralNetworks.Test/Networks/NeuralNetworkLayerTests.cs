// <copyright file="NeuralNetworkLayerTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.Networks
{
    using FluentAssertions;
    using Sde.NeuralNetworks.ActivationFunctionProviders;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Networks;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="NeuralNetworkLayer"/> class.
    /// </summary>
    public class NeuralNetworkLayerTests
    {
        #region test case record definitons

        /// <summary>
        /// Record definition for a test case for the FeedForward method.
        /// </summary>
        /// <param name="weights">Weights of the inputs to the layer.</param>
        /// <param name="biases">Biases of the neurons in the layer.</param>
        /// <param name="learningRate">Learning rate.</param>
        /// <param name="momentum">Momentum.</param>
        /// <param name="inputs">Inputs to the FeedForward method.</param>
        /// <param name="expectedOutputs">Expected outputs from the layer.</param>
        public record FeedForwardTestCase(
            Matrix weights,
            Vector biases,
            double learningRate,
            double momentum,
            Vector inputs,
            Vector expectedOutputs);

        /// <summary>
        /// Record definition for a test case for the ApplyGradientsWithMomentum method.
        /// </summary>
        /// <param name="initialWeights">The initial weights of the inputs to the layer.</param>
        /// <param name="initialBiases">The initial biases of the neurons in the layer.</param>
        /// <param name="learningRate">Learning rate.</param>
        /// <param name="momentum">Momentum.</param>
        /// <param name="weightGradients">The gradients to apply to the weights.</param>
        /// <param name="biasGradients">The gradients to apply to the biases.</param>
        /// <param name="expectedWeights">
        /// The expected weights of the inputs to the layer after applying the gradients.
        /// </param>
        /// <param name="expectedBiases">
        /// The expected biases of the neurons in the layer after applying the gradients.
        /// </param>
        /// <param name="expectedPreviousWeightDeltas">
        /// The expected previous weight deltas (velocity) stored in the layer after applying the gradients.
        /// </param>
        /// <param name="expectedPreviousBiasDeltas">
        /// The expected previous bias deltas (velocity) stored in the layer after applying the gradients.
        /// </param>
        public record ApplyGradientsTestCase(
            Matrix initialWeights,
            Vector initialBiases,
            double learningRate,
            double momentum,
            Matrix weightGradients,
            Vector biasGradients,
            Matrix expectedWeights,
            Vector expectedBiases,
            Matrix expectedPreviousWeightDeltas,
            Vector expectedPreviousBiasDeltas);

        #endregion

        #region test case names

        /// <summary>
        /// Gets the names of the feed forward test cases.
        /// </summary>
        public static TheoryData<string> FeedForwardTestCaseNames => new TheoryData<string>(FeedForwardTestCases.Keys.ToArray());

        /// <summary>
        /// Gets the names of the apply gradients test cases.
        /// </summary>
        public static TheoryData<string> ApplyGradientsTestCaseNames => new TheoryData<string>(ApplyGradientsTestCases.Keys.ToArray());

        #endregion

        #region test case data

        private static Dictionary<string, FeedForwardTestCase> FeedForwardTestCases
        {
            get
            {
                var data = new Dictionary<string, FeedForwardTestCase>();

                //// expected row0 = 1*2 + 2*(-1) + 3*0.5 + 0.5 = 2.0
                //// expected row1 = 4*2 + 5*(-1) + 6*0.5 -1.0 = 5.0
                data.Add(
                    "Simple 2x3 layer with linear activation",
                    new FeedForwardTestCase(
                        weights: new Matrix(new[]
                        {
                            new Vector(new double[] { 1.0, 2.0, 3.0 }),   // row 0
                            new Vector(new double[] { 4.0, 5.0, 6.0 }),   // row 1
                        }),
                        biases: new Vector(new double[] { 0.5, -1.0 }),
                        learningRate: 0.01,
                        momentum: 0,
                        inputs: new Vector(new double[] { 2.0, -1.0, 0.5 }),
                        expectedOutputs: new Vector(new double[] { 2.0, 5.0 })));
                return data;
            }
        }

        private static Dictionary<string, ApplyGradientsTestCase> ApplyGradientsTestCases
        {
            get
            {
                var data = new Dictionary<string, ApplyGradientsTestCase>();
                data.Add(
                    "Sinple 2x3",
                    new ApplyGradientsTestCase(
                        initialWeights: new Matrix(new[]
                        {
                            new Vector(new double[] { 1, 0 }),   // row 0
                            new Vector(new double[] { 0, 1 }),   // row 1
                        }),
                        initialBiases: new Vector(new double[] { 0, 0 }),
                        learningRate: 0.5,
                        momentum: 0.2,
                        weightGradients: new Matrix(new[]
                        {
                            new Vector(new double[] { 2.0, -4.0 }),   // gradient for row 0
                            new Vector(new double[] { 1.0,  3.0 }),   // gradient for row 1
                        }),
                        biasGradients: new Vector(new double[] { -2, 1 }),
                        expectedPreviousWeightDeltas: new Matrix(new[]
                        {
                            new Vector(new double[] { 1, -2 }),
                            new Vector(new double[] { 0.5, 1.5 }),
                        }),
                        expectedPreviousBiasDeltas: new Vector(new double[] { -1, 0.5 }),
                        expectedWeights: new Matrix(new[]
                        {
                            new Vector(new double[] { 0, 2 }),
                            new Vector(new double[] { -0.5, -0.5 }),
                        }),
                        expectedBiases: new Vector(new double[] { 1, -0.5 })));
                return data;
            }
        }

        #endregion

        #region FeedForward tests

        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.FeedForward"/> computes W*x + b when using a linear activation.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(FeedForwardTestCaseNames))]
        public void FeedForward_ComputesWxPlusB(string testCaseName)
        {
            // Arrange
            var testCase = FeedForwardTestCases[testCaseName];
            var layer = new NeuralNetworkLayer(
                testCase.weights,
                testCase.biases,
                testCase.learningRate,
                testCase.momentum);

            // Act
            var outputs = layer.FeedForward(testCase.inputs);

            // Assert
            outputs.Should().Be(testCase.expectedOutputs);
        }

#if DEBUG
        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.FeedForward"/> throws when the input vector dimension
        /// does not match the weights' number of columns.
        /// </summary>
        [Fact]
        public void FeedForward_Throws_WhenInputDimensionMismatch()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0, 0.0, 0.0 }),
                new Vector(new double[] { 0.0, 1.0, 0.0 }),
            });

            var biases = new Vector(new double[] { 0.0, 0.0 });
            var layer = new NeuralNetworkLayer(weights, biases);

            var wrongInputs = new Vector(new double[] { 1.0, 2.0 }); // dimension 2 vs expected 3

            // Act & Assert
            Assert.Throws<ArgumentException>(() => layer.FeedForward(wrongInputs));
        }

        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.FeedForward"/> throws when weights are not initialised.
        /// </summary>
        [Fact]
        public void FeedForward_Throws_WhenWeightsNotInitialised()
        {
            // Arrange
            var layer = new NeuralNetworkLayer(); // default ctor leaves Weights empty
            var inputs = new Vector(new double[] { 1.0 });

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => layer.FeedForward(inputs));
        }
#endif

        /// <summary>
        /// Tests that the FeedForward method throws the correct exception when the dimension of the biases
        /// does not match the number of weights.
        /// </summary>
        [Fact]
        public void FeedForward_BiasesDimensionNotSameAsWeightsRowCount_Throws()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0, 0.0 }),
                new Vector(new double[] { 0.0, 1.0 }),
            });
            var biases = new Vector(new double[] { 0.0 }); // dimension mismatch (should be length 2)
            var layer = new NeuralNetworkLayer(weights, biases);
            var inputs = new Vector(new double[] { 1.0, 2.0 });

            // Act
            var action = () => layer.FeedForward(inputs);

            // Assert
            action.Should().ThrowExactly<InvalidOperationException>();
        }

        #endregion

        #region ApplyGradientsWithMomentum tests

        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.ApplyGradientsWithMomentum"/>
        /// updates weights and biases and stores the velocity (previous deltas).
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ApplyGradientsTestCaseNames))]
        public void ApplyGradientsWithMomentum_UpdatesParametersAndStoresVelocities(string testCaseName)
        {
            // Arrange
            var testCase = ApplyGradientsTestCases[testCaseName];
            var layer = new NeuralNetworkLayer(testCase.initialWeights, testCase.initialBiases, testCase.learningRate, testCase.momentum);

            // Act
            layer.ApplyGradientsWithMomentum(testCase.weightGradients, testCase.biasGradients);

            // Assert
            layer.PreviousWeightDeltas.Should().Be(testCase.expectedPreviousWeightDeltas);
            layer.PreviousBiasDeltas.Should().Be(testCase.expectedPreviousBiasDeltas);
            layer.Weights.Should().Be(testCase.expectedWeights);
        }

        /// <summary>
        /// Verifies that successive calls to <see cref="NeuralNetworkLayer.ApplyGradientsWithMomentum"/>
        /// respect the momentum term when computing velocity.
        /// </summary>
        [Fact]
        public void ApplyGradientsWithMomentum_AppliesMomentumOnSubsequentCalls()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 0.0 }),
            });

            var biases = new Vector(new double[] { 0.0 });

            // learningRate = 1, momentum = 0.5
            var layer = new NeuralNetworkLayer(weights, biases, learningRate: 1.0, momentum: 0.5);

            var grad1 = new Matrix(new[] { new Vector(new double[] { 2.0 }) });
            var biasGrad1 = new Vector(new double[] { 1.0 });

            var grad2 = new Matrix(new[] { new Vector(new double[] { 4.0 }) });
            var biasGrad2 = new Vector(new double[] { 2.0 });

            // Act - first update
            layer.ApplyGradientsWithMomentum(grad1, biasGrad1);

            // velocity1 = 1*grad1 = 2, bias velocity = 1
            Assert.Equal(2.0, layer.PreviousWeightDeltas.RowVectors[0][0], 6);
            Assert.Equal(1.0, layer.PreviousBiasDeltas[0], 6);
            Assert.Equal(-2.0, layer.Weights.RowVectors[0][0], 6); // 0 - 2

            // Act - second update
            layer.ApplyGradientsWithMomentum(grad2, biasGrad2);

            // velocity2 = momentum * prev + lr * grad2 = 0.5*2 + 4 = 1 + 4 = 5
            // bias velocity2 = 0.5*1 + 2 = 2.5
            Assert.Equal(5.0, layer.PreviousWeightDeltas.RowVectors[0][0], 6);
            Assert.Equal(2.5, layer.PreviousBiasDeltas[0], 6);

            // weights updated: prev weight was -2, subtract velocity2 => -7
            Assert.Equal(-7.0, layer.Weights.RowVectors[0][0], 6);

            // biases updated: prev bias was -1 (0 - 1), subtract 2.5 => -3.5
            Assert.Equal(-3.5, layer.Biases[0], 6);
        }

#if DEBUG
        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.ApplyGradientsWithMomentum"/> throws
        /// on null weight gradients as required by the interface contract (debug builds).
        /// </summary>
        [Fact]
        public void ApplyGradientsWithMomentum_ThrowsOnNullWeightGradients()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0 }),
            });

            var biases = new Vector(new double[] { 0.0 });
            var layer = new NeuralNetworkLayer(weights, biases);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => layer.ApplyGradientsWithMomentum(null!, new Vector(new double[] { 0.0 })));
        }
#endif

        #endregion

        #region CalculateGradients tests

        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.CalculateGradients"/> computes gradients,
        /// applies them and returns the input error signal.
        /// </summary>
        [Fact]
        public void CalculateGradients_ComputesAndAppliesGradientsAndReturnsInputError()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0, 0.0 }),   // neuron0
                new Vector(new double[] { 0.0, 1.0 }),   // neuron1
            });

            var biases = new Vector(new double[] { 0.0, 0.0 });

            // Use learningRate = 1.0 so velocity == gradient (previous deltas zero).
            var layer = new NeuralNetworkLayer(weights, biases, learningRate: 1.0, momentum: 0);

            var inputs = new Vector(new double[] { 2.0, 3.0 });
            var expectedOutputs = new Vector(new double[] { 1.0, 4.0 });

            // Act
            var inputError = layer.CalculateGradients(inputs, expectedOutputs);

            // Assert
            // deltas = expected - output = [-1, 1]
            // weight gradients rows:
            // row0 = inputs * -1 => [-2, -3]
            // row1 = inputs * 1  => [2, 3]
            // New weights (old - gradients):
            Assert.Equal(3.0, layer.Weights.RowVectors[0][0], 6);
            Assert.Equal(3.0, layer.Weights.RowVectors[0][1], 6);
            Assert.Equal(-2.0, layer.Weights.RowVectors[1][0], 6);
            Assert.Equal(-2.0, layer.Weights.RowVectors[1][1], 6);

            // Biases updated
            Assert.Equal(1.0, layer.Biases[0], 6);
            Assert.Equal(-1.0, layer.Biases[1], 6);

            // Previous deltas stored
            Assert.Equal(-2.0, layer.PreviousWeightDeltas.RowVectors[0][0], 6);
            Assert.Equal(-3.0, layer.PreviousWeightDeltas.RowVectors[0][1], 6);
            Assert.Equal(2.0, layer.PreviousWeightDeltas.RowVectors[1][0], 6);
            Assert.Equal(3.0, layer.PreviousWeightDeltas.RowVectors[1][1], 6);

            Assert.Equal(-1.0, layer.PreviousBiasDeltas[0], 6);
            Assert.Equal(1.0, layer.PreviousBiasDeltas[1], 6);

            // Input error signal returned is W^T (updated) * deltas (where deltas = [-1, 1]):
            // W^T columns are [3, -2] and [3, -2]
            Assert.Equal(-5.0, inputError[0], 6);
            Assert.Equal(-5.0, inputError[1], 6);
        }

#if DEBUG
        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.CalculateGradients"/> throws when input dimension
        /// does not match weights' columns.
        /// </summary>
        [Fact]
        public void CalculateGradients_Throws_WhenInputDimensionMismatch()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0, 0.0 }),
            });

            var biases = new Vector(new double[] { 0.0 });
            var layer = new NeuralNetworkLayer(weights, biases);

            var inputs = new Vector(new double[] { 1.0 }); // dimension 1 (mismatch)
            var expected = new Vector(new double[] { 0.0 });

            // Act & Assert
            Assert.Throws<ArgumentException>(() => layer.CalculateGradients(inputs, expected));
        }

        /// <summary>
        /// Verifies that <see cref="NeuralNetworkLayer.CalculateGradients"/> throws when expected output length
        /// does not match layer output size.
        /// </summary>
        [Fact]
        public void CalculateGradients_Throws_WhenExpectedOutputDimensionMismatch()
        {
            // Arrange
            var weights = new Matrix(new[]
            {
                new Vector(new double[] { 1.0, 0.0 }),
                new Vector(new double[] { 0.0, 1.0 }),
            });

            var biases = new Vector(new double[] { 0.0, 0.0 });
            var layer = new NeuralNetworkLayer(weights, biases);

            var inputs = new Vector(new double[] { 1.0, 2.0 });
            var wrongExpected = new Vector(new double[] { 1.0 }); // should be length 2

            // Act & Assert
            Assert.Throws<ArgumentException>(() => layer.CalculateGradients(inputs, wrongExpected));
        }
#endif

        #endregion

        #region constructor tests

        /// <summary>
        /// Verifies that when no activation provider is supplied the layer defaults to a linear activation provider.
        /// </summary>
        [Fact]
        public void Constructor_DefaultsToLinearActivationProvider()
        {
            // Arrange
            var weights = new Matrix(new[] { new Vector(new double[] { 1.0 }) });
            var biases = new Vector(new double[] { 0.0 });

            // Act
            var layer = new NeuralNetworkLayer(weights, biases);

            // Assert
            Assert.IsType<LinearActivationFunctionProvider>(layer.ActivationFunctionProvider);
        }

        /// <summary>
        /// Tests that when an activation provider is supplied to the constructor, the layer uses the supplied provider.
        /// </summary>
        [Fact]
        public void Constructor_ActivationProviderIsSupplied_UsesSuppliedActivationProvider()
        {
            // Arrange
            var weights = new Matrix(new[] { new Vector(new double[] { 1.0 }) });
            var biases = new Vector(new double[] { 0.0 });
            var provider = new BipolarSigmoidActivationFunctionProvider();

            // Act
            var layer = new NeuralNetworkLayer(weights, biases, activationFunctionProvider: provider);

            // Assert
            Assert.Same(provider, layer.ActivationFunctionProvider);
        }

#if DEBUG
        /// <summary>
        /// Verifies that the constructor throws when supplied a negative momentum value.
        /// This behaviour is only enforced in DEBUG builds.
        /// </summary>
        [Fact]
        public void Constructor_Throws_OnNegativeMomentum()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new NeuralNetworkLayer(learningRate: 0.01, momentum: -0.1));
        }

        /// <summary>
        /// Verifies that the constructor throws when supplied a non-positive learning rate (zero or negative).
        /// This behaviour is only enforced in DEBUG builds.
        /// </summary>
        [Fact]
        public void Constructor_Throws_OnNonPositiveLearningRate()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new NeuralNetworkLayer(learningRate: 0.0, momentum: 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NeuralNetworkLayer(learningRate: -1.0, momentum: 0));
        }
#endif

        #endregion
    }
}
