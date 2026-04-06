// <copyright file="TrainingOptions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using Sde.NeuralNetworks.Telemetry;

    /// <summary>
    /// Options that control training behaviour and observability for a multi
    /// layer neural network.
    /// </summary>
    /// <param name="observability">
    /// Controls runtime observability and telemetry behaviour.
    /// Use <see cref="ObservabilityMode"/>
    /// to select the desired level of telemetry (for example <c>None</c> for
    /// maximum throughput, or <c>Full</c> to enable detailed snapshots).
    /// Implementations SHOULD avoid performing expensive work on the training
    /// thread when observability is enabled; telemetry providers are expected to
    /// offload processing to background workers.
    /// </param>
    /// <param name="reportEveryNSamples">
    /// When greater than 0, a hint to implementations to emit progress updates
    /// after approximately this many samples have been processed.
    /// Typical default is <c>100</c>.
    /// Implementations MAY coalesce or throttle updates for performance reasons;
    /// this value is advisory rather than prescriptive.
    /// </param>
    /// <param name="reportEveryMilliseconds">
    /// When > 0, a time-based hint (in milliseconds) directing implementations to
    /// emit progress updates at most once in the specified interval.
    /// If both <paramref name="reportEveryNSamples"/> and
    /// <paramref name="reportEveryMilliseconds"/> are non-zero, implementations
    /// SHOULD ensure updates are emitted when either threshold is reached
    /// (subject to throttling).
    /// </param>
    public sealed record TrainingOptions(
        ObservabilityMode observability = ObservabilityMode.None,
        int reportEveryNSamples = 100,
        int reportEveryMilliseconds = 0);
}
