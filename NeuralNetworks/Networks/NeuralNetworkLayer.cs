// <copyright file="NeuralNetworkLayer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using System;
    using System.Text;
    using Sde.NeuralNetworks.ActivationFunctionProviders;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// A single layer in a neural network.
    /// </summary>
    public class NeuralNetworkLayer : INeuralNetworkLayer
    {
        private readonly IActivationFunctionProvider activationFunctionProvider;
        private readonly ILayerMaths layerMaths;

        #region constructors

        // TODO: do we need both NeuralNetworkLayer constructors? The one with weights/biases is useful for deterministic unit tests, but is it worth the extra complexity in the public API?
        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetworkLayer"/> class.
        /// </summary>
        /// <param name="learningRate">
        /// The rate at which the model learns during training.
        /// The larger this is, the more the model's parameters are updated in
        /// response to the computed gradients.
        /// Must be positive and non-zero.
        /// </param>
        /// <param name="momentum">
        /// Momentum coefficient.
        /// The larger this is, the more the previous weight update influences the
        /// current update, which can help accelerate training and smooth out updates.
        /// Must be non-negative.
        /// </param>
        /// <param name="activationFunctionProvider">
        /// Provides the activation function and its gradient for this layer.
        /// If null, a default linear activation function provider will be used
        /// (i.e. f(x) = x).
        /// </param>
        /// <param name="layerMaths">
        /// Implements the pure linear / activation / gradient maths required by
        /// the layer.
        /// </param>
        public NeuralNetworkLayer(
            double learningRate = 0.01,
            double momentum = 0,
            IActivationFunctionProvider? activationFunctionProvider = null,
            ILayerMaths? layerMaths = null)
        {
#if DEBUG
            if (momentum < 0)
            {
                var msg = $"Momentum must be non-negative. Supplied value: {momentum}";
                throw new ArgumentOutOfRangeException(nameof(momentum), msg);
            }

            if (learningRate <= 0)
            {
                var msg = $"Learning rate must be positive and non-zero. Supplied "
                    + $"value: {learningRate}";
                throw new ArgumentOutOfRangeException(nameof(learningRate), msg);
            }
#endif

            this.LearningRate = learningRate;
            this.Momentum = momentum;
            if (activationFunctionProvider is null)
            {
                // Default to linear activation if none provided
                this.activationFunctionProvider = new LinearActivationFunctionProvider();
            }
            else
            {
                this.activationFunctionProvider = activationFunctionProvider;
            }

            this.layerMaths = layerMaths ?? new LayerMaths();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetworkLayer"/>
        /// class with the specified weights and biases.
        /// Useful for deterministic unit tests.
        /// </summary>
        /// <param name="weights">
        /// Initial weight matrix (rows = outputs, columns = inputs).
        /// </param>
        /// <param name="biases">Initial bias vector (length = outputs).</param>
        /// <param name="learningRate">
        /// The rate at which the model learns during training.
        /// The larger this is, the more the model's parameters are updated in
        /// response to the computed gradients.
        /// Must be positive and non-zero.
        /// </param>
        /// <param name="momentum">
        /// Momentum coefficient.
        /// The larger this is, the more the previous weight update influences the
        /// current update, which can help accelerate training and smooth out
        /// updates.
        /// Must be non-negative.
        /// </param>
        /// <param name="activationFunctionProvider">
        /// Provides the activation function and its gradient for this layer.
        /// If null, a default linear activation function provider will be used (i.e. f(x) = x).
        /// </param>
        public NeuralNetworkLayer(
            Matrix weights,
            Vector biases,
            double learningRate = 0.01,
            double momentum = 0,
            IActivationFunctionProvider? activationFunctionProvider = null)
            : this(learningRate, momentum, activationFunctionProvider)
        {
            this.Weights = weights;
            this.Biases = biases;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the activation function provider used by this layer.
        /// </summary>
        public IActivationFunctionProvider ActivationFunctionProvider => this.activationFunctionProvider;

        /// <inheritdoc/>
        public double Momentum { get; }

        /// <inheritdoc/>
        public double LearningRate { get; }

        /// <inheritdoc/>
        public Matrix Weights { get; private set; } = new Matrix(Array.Empty<Vector>());

        /// <inheritdoc/>
        public Vector Biases { get; private set; } = new Vector(Array.Empty<double>());

        /// <inheritdoc/>
        public Matrix PreviousWeightDeltas { get; private set; } = new Matrix(Array.Empty<Vector>());

        /// <inheritdoc/>
        public Vector PreviousBiasDeltas { get; private set; } = new Vector(Array.Empty<double>());

        #endregion

        /// <inheritdoc/>
        public Vector FeedForward(Vector inputs)
        {
#if DEBUG
            if (this.Weights is null || this.Weights.RowCount == 0 || this.Weights.ColumnCount == 0)
            {
                throw new InvalidOperationException("Layer weights are not initialised.");
            }

            if (this.Weights.ColumnCount != inputs.Dimension)
            {
                throw new ArgumentException(
                    $"Input dimension ({inputs.Dimension}) does not match weights' number of columns ({this.Weights.ColumnCount}).",
                    nameof(inputs));
            }

            if (this.Biases.Dimension != this.Weights.RowCount)
            {
                throw new InvalidOperationException(
                    "Bias vector length must equal the number of destination neurons (weights row count).");
            }
#endif

            // For a fully connected layer each input connects to every neuron; the
            // weight matrix therefore has one row per destination neuron (outputs)
            // and one column per source value (inputs).
            // Multiplying the matrix by the input vector yields the pre-activation
            // vector (one linear combination per neuron).
            var weightedInputs = this.Weights.Multiply(inputs);

            // Add the bias and apply the activation to obtain the layer outputs.
            var outputs = new double[weightedInputs.Dimension];
            for (var rowIndex = 0; rowIndex < weightedInputs.Dimension; rowIndex++)
            {
                var preActivation = weightedInputs[rowIndex] + this.Biases[rowIndex];
                outputs[rowIndex] = this.ActivationFunctionProvider.CalculateActivation(preActivation);
            }

            return new Vector(outputs);
        }

        /// <inheritdoc/>
        public Vector CalculateGradients(Vector inputs, Vector expectedOutputs)
        {
#if DEBUG
            if (this.Weights.ColumnCount != inputs.Dimension)
            {
                throw new ArgumentException(
                    $"Input dimension ({inputs.Dimension}) does not match weights' number of columns ({this.Weights.ColumnCount}).",
                    nameof(inputs));
            }

            if (expectedOutputs.Dimension != this.Weights.RowCount)
            {
                throw new ArgumentException(
                    $"Expected output length ({expectedOutputs.Dimension}) does not match layer output size ({this.Weights.RowCount}).",
                    nameof(expectedOutputs));
            }
#endif

            // TODO: NaN validation could be refactord into its own method
            var preActivations = this.layerMaths.ComputePreActivations(
                this.Weights,
                this.Biases,
                inputs);

#if VERBOSE
            // Start of diagnostics
            // small helper to compute stats for a Vector (min/max, any Inf, any NaN)
            static (double min, double max, bool anyInf, bool anyNaN) Stats(Vector v)
            {
                var elems = v.Elements;
                if (elems.Length == 0) return (double.NaN, double.NaN, false, false);
                double min = elems[0], max = elems[0];
                bool anyInf = false, anyNaN = false;
                for (int i = 0; i < elems.Length; i++)
                {
                    var x = elems[i];
                    if (double.IsNaN(x)) anyNaN = true;
                    if (double.IsInfinity(x)) anyInf = true;
                    if (!double.IsNaN(x) && !double.IsInfinity(x))
                    {
                        if (x < min) min = x;
                        if (x > max) max = x;
                    }
                }

                return (min, max, anyInf, anyNaN);
            }

            var paStats = Stats(preActivations);
            System.Diagnostics.Debug.WriteLine($"CalcGradients: preActivations min={paStats.min} max={paStats.max} anyInf={paStats.anyInf} anyNaN={paStats.anyNaN}");
            if (paStats.anyInf || paStats.anyNaN) throw new InvalidOperationException($"Invalid preActivations: anyInf={paStats.anyInf} anyNaN={paStats.anyNaN}");
#endif
            var outputs = this.layerMaths.ApplyActivation(preActivations, this.ActivationFunctionProvider);
#if VERBOSE
            var outStats = Stats(outputs);
            System.Diagnostics.Debug.WriteLine($"CalcGradients: outputs min={outStats.min} max={outStats.max} anyInf={outStats.anyInf} anyNaN={outStats.anyNaN}");
            if (outStats.anyInf || outStats.anyNaN) throw new InvalidOperationException($"Invalid outputs: anyInf={outStats.anyInf} anyNaN={outStats.anyNaN}");

            var residual = expectedOutputs.Subtract(outputs);
            var resStats = Stats(residual);
            System.Diagnostics.Debug.WriteLine($"CalcGradients: residual (expected-output) min={resStats.min} max={resStats.max} anyInf={resStats.anyInf} anyNaN={resStats.anyNaN}");
            if (resStats.anyInf || resStats.anyNaN) throw new InvalidOperationException($"Invalid residuals: anyInf={resStats.anyInf} anyNaN={resStats.anyNaN}");

            // activation gradient (vector) and final deltas
            var activationGrad = this.ActivationFunctionProvider.CalculateGradient(preActivations);
            var gradStats = Stats(activationGrad);
            System.Diagnostics.Debug.WriteLine($"CalcGradients: activationGradient min={gradStats.min} max={gradStats.max} anyInf={gradStats.anyInf} anyNaN={gradStats.anyNaN}");
            if (gradStats.anyInf || gradStats.anyNaN) throw new InvalidOperationException($"Invalid activation-gradient: anyInf={gradStats.anyInf} anyNaN={gradStats.anyNaN}");
#endif
            var deltas = this.layerMaths.ComputeDeltas(preActivations, outputs, expectedOutputs, this.ActivationFunctionProvider);
#if VERBOSE
            var deltaStats = Stats(deltas);
            System.Diagnostics.Debug.WriteLine($"CalcGradients: deltas min={deltaStats.min} max={deltaStats.max} anyInf={deltaStats.anyInf} anyNaN={deltaStats.anyNaN}");
            if (deltaStats.anyInf || deltaStats.anyNaN) throw new InvalidOperationException($"Invalid deltas: anyInf={deltaStats.anyInf} anyNaN={deltaStats.anyNaN}");

            // End of diagnostics
#endif
#if DEBUG
            ThrowIfContainsNaNOrInfinity(preActivations, nameof(preActivations));
            ThrowIfContainsNaNOrInfinity(outputs, nameof(outputs));
            ThrowIfContainsNaNOrInfinity(deltas, nameof(deltas));

            // Gradient clipping - prevent exploding deltas and infinite gradients
            const double maxAbsDelta = 1e6;
            var deltaElems = deltas.Elements;
            for (var di = 0; di < deltaElems.Length; di++)
            {
                var v = deltaElems[di];
#if DEBUG
                if (!double.IsFinite(v))
                {
                    var msg = $"Invalid delta value before gradient build at index {di}: {v}";
                    throw new InvalidOperationException(msg);
                }
#endif

                if (Math.Abs(v) > maxAbsDelta)
                {
                    deltaElems[di] = Math.Sin(v) * maxAbsDelta;
                }
            }

            // replace deltas with clipped copy
            deltas = new Vector(deltaElems);
#endif

            var weightGradients = this.layerMaths.BuildWeightGradients(inputs, deltas);

            // TODO: pointless assignment to biasGradients
            var biasGradients = deltas;
#if DEBUG
            ThrowIfContainsNaNOrInfinity(biasGradients, nameof(biasGradients));
            ThrowIfContainsNaNOrInfinity(weightGradients, nameof(weightGradients));
#endif

            // Apply gradients (mutates weights/biases and updates Previous*Deltas)
            this.ApplyGradientsWithMomentum(weightGradients, biasGradients);

            // Error signal for previous layer: W^T * deltas
            var inputErrorSignal = this.Weights.Transpose().Multiply(deltas);
#if DEBUG
            ThrowIfContainsNaNOrInfinity(inputErrorSignal, nameof(inputErrorSignal));
#endif
            return inputErrorSignal;
        }

        /// <inheritdoc/>
        public void ApplyGradientsWithMomentum(Matrix weightGradients, Vector biasGradients)
        {
#if DEBUG
            if (weightGradients is null)
            {
                throw new ArgumentNullException(nameof(weightGradients));
            }
#endif

            // Ensure previous-delta shapes (create zeros if not supplied)
            var prevW = this.PreviousWeightDeltas;
            if (prevW.RowCount != this.Weights.RowCount || prevW.ColumnCount != this.Weights.ColumnCount)
            {
                prevW = CreateZeroMatrix(this.Weights.RowCount, this.Weights.ColumnCount);
            }

            var prevB = this.PreviousBiasDeltas;
            if (prevB.Dimension != this.Biases.Dimension)
            {
                prevB = new Vector(new double[this.Biases.Dimension]);
            }

            // velocity = momentum * prev + learningRate * gradient
            var scaledGradW = weightGradients.Multiply(this.LearningRate);
            var velocityW = prevW.Multiply(this.Momentum) + scaledGradW;

            var scaledGradB = biasGradients.Multiply(this.LearningRate);
            var velocityB = prevB.Multiply(this.Momentum) + scaledGradB;
#if DEBUG
            ThrowIfContainsNaNOrInfinity(scaledGradB, nameof(scaledGradB));
            ThrowIfContainsNaNOrInfinity(scaledGradW, nameof(scaledGradW));
            ThrowIfContainsNaNOrInfinity(velocityB, nameof(velocityB));
            ThrowIfContainsNaNOrInfinity(velocityW, nameof(velocityW));
#endif

            // apply update
            this.Weights.SubtractInPlace(velocityW);
            this.Biases.SubtractInPlace(velocityB);

            // persist velocities for the next update
            this.PreviousWeightDeltas = velocityW;
            this.PreviousBiasDeltas = velocityB;
        }

        private static Matrix CreateZeroMatrix(int rows, int columns)
        {
            var zeroRows = new Vector[rows];
            for (var r = 0; r < rows; r++)
            {
                zeroRows[r] = new Vector(new double[columns]);
            }

            return new Matrix(zeroRows);
        }

#if DEBUG

        private static void ThrowIfContainsNaNOrInfinity(Vector v, string name)
        {
            if (ContainsNaN(v))
            {
                var msg = $"NaN or infinity detected in {name}. Values: "
                    + Environment.NewLine
                    + v.ToString();
                System.Diagnostics.Debug.WriteLine(msg);
                throw new InvalidOperationException(msg);
            }
        }

        private static void ThrowIfContainsNaNOrInfinity(Matrix m, string name)
        {
            for (var i = 0; i < m.RowCount; i++)
            {
                var v = m.RowVectors[i];
                if (ContainsNaN(v))
                {
                    var msg = $"NaN or infinity detected in {name}. Values:"
                        + Environment.NewLine
                        + m.ToString();
                    System.Diagnostics.Debug.WriteLine(msg);
                    throw new InvalidOperationException(msg);
                }
            }
        }

        private static bool ContainsNaN(Vector v)
        {
            var elements = v.Elements;
            for (var i = 0; i < elements.Length; i++)
            {
                if (double.IsNaN(elements[i]) || double.IsInfinity(elements[i]))
                {
                    return true;
                }
            }

            return false;
        }
#endif
    }
}
