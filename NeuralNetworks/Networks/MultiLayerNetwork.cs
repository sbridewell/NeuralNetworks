// <copyright file="MultiLayerNetwork.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Sde.NeuralNetworks.FeatureScaling;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Persistence;
    using Sde.NeuralNetworks.Telemetry;

    /// <summary>
    /// A concrete implementation of <see cref="IMultiLayerNetwork"/> that composes
    /// a sequence of <see cref="INeuralNetworkLayer"/> instances and implements
    /// forward / back propagation.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// The class is designed to be fast on the hot path; any observability or
    /// telemetry work is strictly opt-in via
    /// <see cref="Sde.NeuralNetworks.Networks.TrainingOptions"/> and an optional
    /// <see cref="ITrainingTelemetry"/> instance.
    /// </item>
    /// <item>
    /// This implementation delegates persistence (import) to an
    /// <see cref="INetworkPersistence"/> when one is supplied.
    /// Export produces a <see cref="NetworkState"/> DTO.
    /// </item>
    /// </list>
    /// </remarks>
    public sealed class MultiLayerNetwork : IMultiLayerNetwork
    {
        private readonly IReadOnlyList<INeuralNetworkLayer> layers;
        private readonly TrainingOptions options;
        private readonly ITrainingTelemetry? telemetry;
        private readonly INetworkPersistence? persistence;
        private readonly IFeatureScaler? inputScaler;
        private readonly IFeatureScaler? outputScaler;

        // mutable state
        private readonly double[] hiddenLayerLastMse; // one per hidden layer
        private double outputLayerLastMse;
        private int currentIteration;
        private int numberOfTrainingRecords;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLayerNetwork"/>
        /// class.
        /// </summary>
        /// <param name="layers">
        /// Ordered layers (first = input-facing layer, last = output layer).
        /// </param>
        /// <param name="options">
        /// Training and observability options. When null the defaults are used.
        /// </param>
        /// <param name="telemetry">
        /// Optional telemetry provider for observability (may be null).
        /// </param>
        /// <param name="persistence">
        /// Optional persistence provider.
        /// If null, ImportState is delegated to consumer.
        /// </param>
        /// <param name="inputScaler">
        /// Used to scale the inputs into the network.
        /// </param>
        /// <param name="outputScaler">
        /// Used to scale the expected outputs from the network.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="layers"/> is null.
        /// </exception>
        public MultiLayerNetwork(
            IReadOnlyList<INeuralNetworkLayer> layers,
            TrainingOptions? options = null,
            ITrainingTelemetry? telemetry = null,
            INetworkPersistence? persistence = null,
            IFeatureScaler? inputScaler = null,
            IFeatureScaler? outputScaler = null)
        {
            ArgumentNullException.ThrowIfNull(layers);
            if (layers.Count == 0)
            {
                throw new ArgumentException(
                    "At least one layer must be supplied.",
                    nameof(layers));
            }

            this.layers = layers;
            this.options = options ?? new TrainingOptions();
            this.telemetry = telemetry;
            this.persistence = persistence;
            this.inputScaler = inputScaler;
            this.outputScaler = outputScaler;

            // One hidden-layer MSE entry per hidden layer (exclude output-facing
            // layer).
            // If only the output layer is present then hiddenCount==0
            var hiddenCount = Math.Max(0, this.layers.Count - 1);

            // We consider the first layer as input-facing, subsequent up to
            // (Count-2) as hidden, last is output.
            // hiddenCount should be layers.Count - 2 when input layer is explicit
            // and output exists, but to be conservative treat all layers except
            // the last as "hidden-facing outputs".
            // For the UI a concrete definition will be documented.
            this.hiddenLayerLastMse = new double[hiddenCount];
            this.outputLayerLastMse = double.NaN;
            this.currentIteration = 0;
            this.numberOfTrainingRecords = 0;
            this.TimeSpentTraining = TimeSpan.Zero;
            this.EstimatedTrainingTimeLeft = TimeSpan.Zero;
        }

        #region events

        /// <inheritdoc/>
        public event EventHandler<TrainingProgressEventArgs>
            TrainingProgressChanged = (sender, e) => { };

        /// <inheritdoc/>
        public event EventHandler TrainingStarted = (sender, e) => { };

        /// <inheritdoc/>
        public event EventHandler TrainingCompleted = (sender, e) => { };

        /// <inheritdoc/>
        public event EventHandler TrainingStopped = (sender, e) => { };

        #endregion events

        #region properties

        /// <inheritdoc/>
        public IReadOnlyList<INeuralNetworkLayer> Layers => this.layers;

        /// <inheritdoc/>
        public int NumberOfInputs
            => this.layers.Count > 0 ? this.layers[0].Weights.ColumnCount : 0;

        /// <inheritdoc/>
        public int NumberOfOutputs
            => this.layers.Count > 0 ? this.layers[^1].Weights.RowCount : 0;

        /// <inheritdoc/>
        public double LearningRate { get; set; }

        /// <inheritdoc/>
        public double Momentum { get; set; }

        /// <inheritdoc/>
        public int NumberOfIterations { get; set; }

        /// <inheritdoc/>
        public int CurrentIteration => this.currentIteration;

        /// <inheritdoc/>
        public int NumberOfTrainingRecords => this.numberOfTrainingRecords;

        /// <inheritdoc/>
        public TimeSpan TimeSpentTraining { get; set; }

        /// <inheritdoc/>
        public TimeSpan EstimatedTrainingTimeLeft { get; set; }

        /// <inheritdoc/>
        public IReadOnlyList<double> HiddenLayerMeanSquaredErrors
            => Array.AsReadOnly(this.hiddenLayerLastMse);

        /// <inheritdoc/>
        public double OutputLayerMeanSquaredError => this.outputLayerLastMse;

        #endregion properties

        #region methods

        /// <inheritdoc/>
        public Vector Predict(Vector input)
        {
            var scaledInput = this.inputScaler != null
                ? this.inputScaler.Scale(input)
                : input;

            if (scaledInput.Dimension != this.NumberOfInputs)
            {
                throw new ArgumentException(
                    $"Input vector must be of dimension {this.NumberOfInputs}",
                    nameof(input));
            }

            var current = scaledInput;
            foreach (var layer in this.layers)
            {
                current = layer.FeedForward(current);
            }

            // If an output scaler is present, return values in original units
            if (this.outputScaler != null)
            {
                try
                {
                    return this.outputScaler.ScaleBack(current);
                }
                catch (InvalidOperationException)
                {
                    // Scaler not fitted for ScaleBack, fall back to returning network-domain outputs
                    // TODO: is it correct to swallow this exception?
                }
            }

            return current;
        }

        /// <inheritdoc/>
        public async Task TrainAsync(
            IEnumerable<(Vector inputs, Vector expected)> samples,
            int numberOfiterations = 1,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(samples);
            if (numberOfiterations < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfiterations));
            }

            // fast-path flags
            bool telemetryEnabled
                = this.telemetry != null
                && this.options.observability != ObservabilityMode.None;

            // Materialise samples into an array for indexing; implementations may choose to stream
            var sampleArray = samples as (Vector inputs, Vector expected)[] ?? samples.ToArray();
            var totalSamples = sampleArray.Length;
            this.numberOfTrainingRecords = totalSamples;
            var stopwatch = Stopwatch.StartNew();

            // Raise an event to indicate that training has started.
            this.currentIteration = 0;
            this.TrainingStarted?.Invoke(this, EventArgs.Empty);
            this.telemetry?.OnTrainingStarted();

            try
            {
                await Task.Run(
                    () =>
                {
                    // TODO: move this block into its own method?
                    var lastReportSampleCount = 0;
                    var lastReportTime = Stopwatch.GetTimestamp();
                    var ticksPerMs = Stopwatch.Frequency / 1000.0;

                    for (var iter = 0; iter < numberOfiterations; iter++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        this.currentIteration = iter + 1;

                        for (var s = 0; s < totalSamples; s++)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            // forward pass - collect per-layer inputs & outputs
                            var layerInputs = new Vector[this.layers.Count];
                            var layerOutputs = new Vector[this.layers.Count];
                            Vector current = sampleArray[s].inputs;
                            for (int li = 0; li < this.layers.Count; li++)
                            {
                                layerInputs[li] = current;
                                current = this.layers[li].FeedForward(current);
                                layerOutputs[li] = current;
                            }

                            // compute output MSE for this sample
                            var rawExpected = sampleArray[s].expected;
                            var expected = this.outputScaler != null
                                ? this.outputScaler.Scale(rawExpected)
                                : rawExpected;
                            var output = layerOutputs[^1];
                            double sumSq = 0.0;
                            var dim = Math.Min(expected.Dimension, output.Dimension);
                            for (int i = 0; i < dim; i++)
                            {
                                var e = expected[i] - output[i];
                                sumSq += e * e;
                            }

                            this.outputLayerLastMse = sumSq / Math.Max(1, dim);

                            // backpropagation by delegating to layers' CalculateGradients
                            // Start with last layer: its inputs are layerInputs[^1]
                            var lastLayerInput = layerInputs[^1];
                            Vector propagatedError = this.layers[^1].CalculateGradients(lastLayerInput, expected);

                            // For preceding layers synthesize expected = output + propagatedError and call CalculateGradients
                            for (int li = this.layers.Count - 2; li >= 0; li--)
                            {
                                // synthesize expected_for_layer = outputs_of_layer + propagatedError
                                var layerOut = layerOutputs[li];
                                var expectedForLayer = new Vector(
                                    layerOut.Elements.Select((v, idx) => v +
                                        (idx < propagatedError.Dimension
                                        ? propagatedError[idx]
                                        : 0.0)).ToArray());

                                // capture per-layer MSE from propagated error (sample-level)
                                if (li < this.hiddenLayerLastMse.Length)
                                {
                                    var errSum = 0.0;
                                    for (int k = 0; k < propagatedError.Dimension; k++)
                                    {
                                        errSum += propagatedError[k] * propagatedError[k];
                                    }

                                    this.hiddenLayerLastMse[li] = errSum / Math.Max(1, propagatedError.Dimension);
                                }

                                propagatedError = this.layers[li].CalculateGradients(layerInputs[li], expectedForLayer);
                            }

                            // Progress reporting (throttled by options)
                            var samplesSinceLastReport = s + 1 - lastReportSampleCount;
                            var elapsedMs
                                = (Stopwatch.GetTimestamp() - lastReportTime)
                                / ticksPerMs;
                            var shouldReportByCount
                                = this.options.reportEveryNSamples > 0
                                && samplesSinceLastReport >= this.options.reportEveryNSamples;
                            var shouldReportByTime
                                = this.options.reportEveryMilliseconds > 0
                                && elapsedMs >= this.options.reportEveryMilliseconds;

                            if (shouldReportByCount
                                || shouldReportByTime
                                || s == totalSamples - 1)
                            {
                                var percent
                                    = (int)Math.Round(100.0
                                    * (((iter * (double)totalSamples) + s + 1)
                                    / (numberOfiterations * (double)totalSamples)));
                                var args = new TrainingProgressEventArgs
                                {
                                    CurrentIteration = this.currentIteration,
                                    CurrentSampleIndex = s + 1,
                                    TotalSamples = totalSamples,
                                    PercentComplete = percent,
                                };

                                // fire events / telemetry (both are non-blocking by contract)
                                this.TrainingProgressChanged?.Invoke(this, args);
                                if (telemetryEnabled)
                                {
                                    this.telemetry?.OnTrainingProgress(in args);
                                }

                                lastReportSampleCount = s + 1;
                                lastReportTime = Stopwatch.GetTimestamp();
                            }
                        } // per-sample
                    } // per-iteration

                    // update timers & state
                    stopwatch.Stop();
                    this.TimeSpentTraining += stopwatch.Elapsed;
                    this.EstimatedTrainingTimeLeft = TimeSpan.Zero;
                    this.numberOfTrainingRecords = totalSamples;
                }, cancellationToken).ConfigureAwait(false);

                // finished normally
                this.TrainingCompleted?.Invoke(this, EventArgs.Empty);
                this.telemetry?.OnTrainingCompleted();
            }
            catch (OperationCanceledException)
            {
                // cancelled
                this.TrainingStopped?.Invoke(this, EventArgs.Empty);

                // Do not forward exception; let the caller observe cancellation
                // via the returned Task
                throw;
            }
            catch (Exception)
            {
                // Unexpected error - raise an event to indicate that training has
                // stopped, then rethrow.
                this.TrainingStopped?.Invoke(this, EventArgs.Empty);
                throw;
            }
            finally
            {
                // Ensure telemetry/cleanup if needed (telemetry completion is
                // called on normal completion above)
            }
        }

        /// <summary>
        /// Exports the network's serialisable state as a
        /// <see cref="NetworkState"/> DTO.
        /// </summary>
        /// <returns>
        /// A <see cref="NetworkState"/> describing the network's layers and
        /// global settings.
        /// </returns>
        public NetworkState ExportState()
        {
            var layerStates = this.layers.Select(
                l => new LayerState(l.GetType().Name, l.Weights, l.Biases))
                .ToArray();
            return new NetworkState(
                Array.AsReadOnly(layerStates),
                this.LearningRate,
                this.Momentum,
                this.NumberOfIterations);
        }

        /// <summary>
        /// Imports the supplied <see cref="NetworkState"/> into this network by
        /// delegating to the configured <see cref="INetworkPersistence"/> provider.
        /// </summary>
        /// <param name="state">State to import.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown when no <see cref="INetworkPersistence"/> provider is available.
        /// </exception>
        public void ImportState(NetworkState state)
        {
            if (this.persistence == null)
            {
                var msg = "No INetworkPersistence provider supplied.";
                throw new NotSupportedException(msg);
            }

            this.persistence.ImportState(this, state);
        }

        #endregion methods
    }
}