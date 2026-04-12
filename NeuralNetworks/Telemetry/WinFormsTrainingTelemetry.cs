// <copyright file="WinFormsTrainingTelemetry.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Telemetry
{
    using Sde.NeuralNetworks.Networks;

    /// <summary>
    /// Lightweight telemetry adapter for WinForms: posts notifications to a supplied <see cref="SynchronizationContext"/>.
    /// Implementations must be fast on the training thread; this class only enqueues work to the UI context.
    /// </summary>
    public sealed class WinFormsTrainingTelemetry : ITrainingTelemetry
    {
        private readonly SynchronizationContext uiContext;
        private readonly Action? onStarted;
        private readonly Action<TrainingProgressEventArgs>? onProgress;
        private readonly Action<int, LayerState>? onLayerSnapshot;
        private readonly Action? onCompleted;
        private readonly Action<Exception>? onError;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsTrainingTelemetry"/>
        /// class.
        /// </summary>
        /// <param name="uiContext">
        /// Synchronization context used to marshal callbacks to the UI thread.
        /// If <c>null</c> the constructor uses
        /// <see cref="SynchronizationContext.Current"/> or a default
        /// <see cref="SynchronizationContext"/>.
        /// </param>
        /// <param name="onStarted">
        /// Optional callback invoked on the UI thread when training starts.
        /// </param>
        /// <param name="onProgress">
        /// Optional callback invoked on the UI thread for progress updates.
        /// </param>
        /// <param name="onLayerSnapshot">
        /// Optional callback invoked on the UI thread when a layer snapshot is
        /// available.
        /// Parameters are the zero-based layer index and the
        /// <see cref="LayerState"/> snapshot.
        /// </param>
        /// <param name="onCompleted">
        /// Optional callback invoked on the UI thread when training completes
        /// normally.
        /// </param>
        /// <param name="onError">
        /// Optional callback invoked on the UI thread when an exception occurs
        /// during training.
        /// </param>
        public WinFormsTrainingTelemetry(
            SynchronizationContext? uiContext = null,
            Action? onStarted = null,
            Action<TrainingProgressEventArgs>? onProgress = null,
            Action<int, LayerState>? onLayerSnapshot = null,
            Action? onCompleted = null,
            Action<Exception>? onError = null)
        {
            this.uiContext
                = uiContext
                ?? SynchronizationContext.Current
                ?? new SynchronizationContext();
            this.onStarted = onStarted;
            this.onProgress = onProgress;
            this.onLayerSnapshot = onLayerSnapshot;
            this.onCompleted = onCompleted;
            this.onError = onError;
        }

        /// <inheritdoc/>
        public void OnTrainingStarted()
        {
            // Do minimal work on training thread
            try
            {
                this.uiContext.Post(
                    _ => this.onStarted?.Invoke(),
                    null);
            }
            catch (Exception ex)
            {
                this.onError?.Invoke(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public void OnTrainingProgress(in TrainingProgressEventArgs args)
        {
            // Copy the struct/class to avoid issues with in-parameter lifetime
            var copy = new TrainingProgressEventArgs
            {
                CurrentIteration = args.CurrentIteration,
                CurrentSampleIndex = args.CurrentSampleIndex,
                TotalSamples = args.TotalSamples,
                PercentComplete = args.PercentComplete,
            };

            try
            {
                this.uiContext.Post(
                    _ => this.onProgress?.Invoke(copy),
                    null);
            }
            catch (Exception ex)
            {
                this.onError?.Invoke(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public void OnLayerSnapshot(int layerIndex, in LayerState snapshot)
        {
            var copy = snapshot; // record is immutable-like
            try
            {
                this.uiContext.Post(
                    _ => this.onLayerSnapshot?.Invoke(layerIndex, copy),
                    null);
            }
            catch (Exception ex)
            {
                this.onError?.Invoke(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public void OnTrainingCompleted()
        {
            try
            {
                this.uiContext.Post(
                    _ => this.onCompleted?.Invoke(),
                    null);
            }
            catch (Exception ex)
            {
                this.onError?.Invoke(ex);
                throw;
            }
        }
    }
}