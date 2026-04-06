// <copyright file="ITrainingTelemetry.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Telemetry
{
    using Sde.NeuralNetworks.Networks;

    /// <summary>
    /// Provider for training telemetry used by <see cref="IMultiLayerNetwork"/>.
    /// Implementations receive lightweight notifications from the training loop
    /// and should be non-blocking and low-allocation to avoid impacting training
    /// throughput.
    /// </summary>
    /// <remarks>
    /// Methods on this interface are invoked on the thread performing training
    /// and therefore MUST return quickly.
    /// A typical implementation will enqueue received data for background
    /// processing and marshaling to a UI thread. Implementations SHOULD handle
    /// their own exceptions and MUST NOT allow exceptions to propagate back to
    /// the training code.
    /// </remarks>
    public interface ITrainingTelemetry
    {
        /// <summary>
        /// Notifies the telemetry provider that training has started.
        /// </summary>
        /// <remarks>
        /// Called once when a training operation begins. Implementations should
        /// use this callback to initialise any internal state or to start
        /// background workers.
        /// </remarks>
        void OnTrainingStarted();

        /// <summary>
        /// Reports progress updates for the current training operation.
        /// </summary>
        /// <param name="args">
        /// Progress details including iteration, sample indices and optional loss
        /// values.
        /// Passed by reference to minimise allocations; implementations MUST
        /// treat the data as read-only.
        /// </param>
        /// <remarks>
        /// This method may be invoked frequently. Implementations SHOULD throttle
        /// or coalesce updates if expensive work would otherwise occur for each
        /// call.
        /// </remarks>
        void OnTrainingProgress(in TrainingProgressEventArgs args);

        /// <summary>
        /// Delivers a snapshot of a layer's serialisable state for inspection or
        /// persistence.
        /// </summary>
        /// <param name="layerIndex">
        /// The zero-based index of the layer in forward order (0 = first hidden
        /// layer after the input-facing layer).
        /// </param>
        /// <param name="snapshot">
        /// A read-only <see cref="LayerState"/> containing the serialisable
        /// parameters of the layer.
        /// Passed by reference to reduce copying.
        /// </param>
        /// <remarks>
        /// Implementations SHOULD treat snapshots as immutable and process them
        /// off the training thread. Snapshots may be supplied only when
        /// observability is enabled.
        /// </remarks>
        void OnLayerSnapshot(int layerIndex, in LayerState snapshot);

        /// <summary>
        /// Notifies the telemetry provider that training has completed normally.
        /// </summary>
        /// <remarks>
        /// Called when training finishes without cancellation or error.
        /// Implementations can use this to flush queued data and stop background
        /// workers.
        /// </remarks>
        void OnTrainingCompleted();
    }
}
