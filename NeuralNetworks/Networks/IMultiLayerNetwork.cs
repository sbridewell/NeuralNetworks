// <copyright file="IMultiLayerNetwork.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using System;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Interface for a neural network consisting of multiple layers.
    /// <list type="bullet">
    /// <item>One input layer.</item>
    /// <item>Multiple hidden layers.</item>
    /// <item>One output layer.</item>
    /// </list>
    /// </summary>
    public interface IMultiLayerNetwork
    {
        #region events

        /// <summary>
        /// Occurs when the progress of a training operation changes.
        /// </summary>
        /// <remarks>
        /// Raised periodically during a training operation to report progress.
        /// The <see cref="TrainingProgressEventArgs"/> contains details such as
        /// current iteration, sample indices and optional loss values.
        /// Implementations may throttle or suppress progress events to reduce
        /// runtime overhead; callers should not rely on a particular frequency of
        /// updates.
        /// The event is raised on the thread performing training; handlers that
        /// update UI elements must marshal to the UI thread (for example using
        /// System.Windows.Forms.Control.Invoke or a
        /// <see cref="System.Threading.SynchronizationContext"/>).
        /// </remarks>
        event EventHandler<TrainingProgressEventArgs> TrainingProgressChanged;

        /// <summary>
        /// Occurs when the training process begins.
        /// </summary>
        /// <remarks>
        /// Raised once when a training operation starts. The event is raised on
        /// the thread performing training; handlers should be thread-safe and
        /// must marshal to the UI thread before touching UI controls.
        /// </remarks>
        event EventHandler TrainingStarted;

        /// <summary>
        /// Occurs when the training process has completed successfully.
        /// </summary>
        /// <remarks>
        /// Raised when training finishes normally (i.e. it reached the configured
        /// number of iterations).
        /// It is not raised when training is cancelled or when an error causes
        /// termination — in those cases <see cref="TrainingStopped"/> will be
        /// raised.
        /// The event is raised on the thread that performed the training.
        /// </remarks>
        event EventHandler TrainingCompleted;

        /// <summary>
        /// Occurs when the training process has stopped before normal completion.
        /// </summary>
        /// <remarks>
        /// Raised when training is interrupted, cancelled or terminates due to an
        /// error.
        /// Handlers can use this to perform cleanup or to distinguish between a
        /// normal completion (<see cref="TrainingCompleted"/>) and an early stop.
        /// The event is raised on the thread that performed the training.
        /// </remarks>
        event EventHandler TrainingStopped;

        #endregion events

        #region properties

        /// <summary>
        /// Gets the ordered collection of layers that compose this network.
        /// </summary>
        /// <value>
        /// An ordered, read-only list of <see cref="INeuralNetworkLayer"/>
        /// instances.
        /// The first element is the input-facing layer and the last element is
        /// the output layer; intermediate elements (if any) are hidden layers.
        /// </value>
        /// <remarks>
        /// The returned list defines the forward-pass order used by the network.
        /// Implementations MUST ensure the layers are dimensionally compatible
        /// (outputs of layer N must match inputs of layer N+1) and SHOULD
        /// validate compatibility at construction time or when layers are changed.
        ///
        /// Consumers SHOULD treat the collection as read-only. Although the
        /// property exposes an <see cref="IReadOnlyList{T}"/>, the concrete
        /// backing collection may be mutated by the network implementation;
        /// modifying layers while training is in progress is undefined behaviour
        /// and may cause exceptions.
        ///
        /// Access to this property may not be thread-safe; callers that read it
        /// from UI event handlers or other threads should marshal to an
        /// appropriate synchronization context.
        /// </remarks>
        IReadOnlyList<INeuralNetworkLayer> Layers { get; }

        /// <summary>
        /// Gets the number of inputs into the network in a single sample.
        /// </summary>
        int NumberOfInputs { get; }

        /// <summary>
        /// Gets the number of outputs from the network in a single sample.
        /// </summary>
        int NumberOfOutputs { get; }

        /// <summary>
        /// Gets or sets the learning rate for the network.
        /// </summary>
        double LearningRate { get; set; }

        /// <summary>
        /// Gets or sets the momentum factor used to accelerate gradient descent
        /// updates.
        /// </summary>
        /// <remarks>
        /// A higher momentum value can help the optimizer converge faster by
        /// smoothing out oscillations, but values that are too high may cause
        /// instability.
        /// Typical values range from 0.0 (no momentum) to just below 1.0.
        /// </remarks>
        double Momentum { get; set; }

        /// <summary>
        /// Gets or sets the number of times the training process
        /// should iterate through all the samples in the training
        /// data.
        /// </summary>
        /// <remarks>
        /// Raised when training is interrupted, cancelled or terminates due to an
        /// error.
        /// Handlers can use this to perform cleanup or to distinguish between a
        /// normal completion (<see cref="TrainingCompleted"/>) and an early stop.
        /// The event is raised on the thread that performed the training.
        /// </remarks>
        int NumberOfIterations { get; set; }

        /// <summary>
        /// Gets the current iteration of the training process.
        /// </summary>
        /// <remarks>
        /// Value is read-only and represents the number of completed iterations
        /// (or the current iteration index) during an in-progress training
        /// operation.
        /// </remarks>
        int CurrentIteration { get; }

        /// <summary>
        /// Gets the number of records in the training data.
        /// </summary>
        int NumberOfTrainingRecords { get; }

        /// <summary>
        /// Gets or sets the length of time spent training the network.
        /// </summary>
        TimeSpan TimeSpentTraining { get; set; }

        /// <summary>
        /// Gets or sets the estimated remaining time required to complete the
        /// training process.
        /// </summary>
        /// <remarks>
        /// The value is typically updated as training progresses and may be based
        /// on current performance metrics or historical data.
        /// The accuracy of the estimate may vary depending on the training
        /// algorithm and available information.
        /// </remarks>
        TimeSpan EstimatedTrainingTimeLeft { get; set; }

        /// <summary>
        /// Gets the mean squared error (MSE) for the network's hidden-layer
        /// outputs.
        /// </summary>
        /// <value>
        /// The arithmetic mean of the per-hidden-layer MSE values computed for
        /// the most recent training sample or evaluation.
        /// For networks with multiple hidden layers this value is an aggregate
        /// (the mean across all hidden layers).
        /// Implementations may return <c>double.NaN</c> if no error has yet been
        /// computed.
        /// </value>
        /// <remarks>
        /// Callers that require per-layer errors should use
        /// <see cref="HiddenLayerMeanSquaredErrors"/>.
        /// Reading this value from multiple threads is not guaranteed to be
        /// thread-safe.
        /// </remarks>
        [Obsolete("Backwards compatibility with older implementations with a single hiddne layer")]
        double HiddenLayerMeanSquaredError { get; }

        /// <summary>
        /// Gets the mean squared error (MSE) for each hidden layer, in forward
        /// order.
        /// </summary>
        /// <value>
        /// A read-only list where each element is the MSE for the corresponding
        /// hidden layer (index 0 = first hidden layer).
        /// The list is empty if the network has no hidden layers or if no error
        /// has yet been computed.
        /// </value>
        /// <remarks>
        /// This property is additive and non-breaking; implementations may
        /// compute the values on demand.
        /// Consumers should assume the values may be updated during training and
        /// must marshal access if reading from multiple threads.
        /// </remarks>
        IReadOnlyList<double> HiddenLayerMeanSquaredErrors { get; }

        /// <summary>
        /// Gets the mean squared error (MSE) for the network's output-layer
        /// outputs.
        /// </summary>
        /// <value>
        /// The mean of the squared differences between the network's output
        /// values and the expected target values computed for the most recent
        /// training sample, batch or evaluation.
        /// This metric is independent of the number of hidden layers.
        /// </value>
        /// <remarks>
        /// Implementations MUST document the aggregation semantics (for example:
        /// last-sample, per-batch, running average or per-iteration/epoch).
        /// The value is typically updated during training and/or evaluation;
        /// callers should not assume immediate consistency and should treat reads
        /// as not necessarily thread-safe.
        /// Implementations MAY return <c>double.NaN</c> when no error has yet
        /// been computed.
        /// </remarks>
        double OutputLayerMeanSquaredError { get; }

        #endregion properties

        #region methods

        /// <summary>
        /// Produces an output prediction for the supplied input vector.
        /// </summary>
        /// <param name="input">
        /// Input vector for a single sample.
        /// Its dimension must match <see cref="NumberOfInputs"/>.
        /// </param>
        /// <returns>
        /// The network's output vector for the supplied input.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="input"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the input dimension does not match
        /// <see cref="NumberOfInputs"/>.
        /// </exception>
        Vector Predict(Vector input);

        /// <summary>
        /// Obsolete convenience overload that accepts a primitive array.
        /// </summary>
        /// <param name="input">
        /// Input values for a single sample. Must not be null.
        /// </param>
        /// <returns>The network's output values.</returns>
        /// <remarks>
        /// Prefer the <see cref="Predict(Vector)"/> overload which avoids
        /// unnecessary allocations.
        /// This overload is retained for backwards compatibility.
        /// </remarks>
        [Obsolete("Backwards compatibility only? Use the overload that accepts a Vector instead.")]
        double[] Predict(double[] input);

        /// <summary>
        /// Asynchronously trains the network using the provided samples.
        /// </summary>
        /// <param name="samples">
        /// Collection of tuples containing input and expected output vectors for
        /// training.
        /// Implementations MUST validate the collection and its elements; the
        /// sequence itself may be enumerated multiple times by some
        /// implementations.
        /// </param>
        /// <param name="numberOfiterations">
        /// The number of times to iterate over the entire set of samples.
        /// Must be greater than or equal to 1.
        /// Implementations should throw <see cref="ArgumentOutOfRangeException"/>
        /// for invalid values.
        /// </param>
        /// <param name="cancellationToken">
        /// Token used to request cancellation of the training operation.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous training operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="samples"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="numberOfiterations"/> is less than 1.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// The returned task will be cancelled if
        /// <paramref name="cancellationToken"/> is cancelled.
        /// </exception>
        /// <remarks>
        /// Implementations SHOULD:
        /// <list type="bullet">
        /// <item>
        /// Honour the <paramref name="cancellationToken"/> promptly.
        /// </item>
        /// <item>
        /// Raise the <see cref="TrainingStarted"/> event when training begins,
        /// <see cref="TrainingProgressChanged"/> as configured or necessary, and
        /// either <see cref="TrainingCompleted"/> (normal completion) or
        /// <see cref="TrainingStopped"/> (cancellation or error).
        /// </item>
        /// <item>
        /// Avoid allocating diagnostic objects on the hot path unless an explicit
        /// observability/telemetry mode requests them.
        /// </item>
        /// </list>
        /// Handlers of raised events should assume they may be invoked on a
        /// background thread and must marshal to the UI thread before touching UI
        /// elements.
        /// </remarks>
        Task TrainAsync(
            IEnumerable<(Vector inputs, Vector expected)> samples,
            int numberOfiterations = 1,
            CancellationToken cancellationToken = default);

        #endregion methods
    }
}
