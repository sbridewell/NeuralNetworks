// <copyright file="ObservabilityMode.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Telemetry
{
    /// <summary>
    /// Constrols runtime observability / telemetry behaviour during training.
    /// </summary>
    public enum ObservabilityMode
    {
        /// <summary>
        /// Disables all non-essential telemetry and minimises allocations to
        /// maximise performance.
        /// </summary>
        None,

        /// <summary>
        /// Reports coarse progress only (iteration, percentage complete) at
        /// configured intervals.
        /// </summary>
        Minimal,

        /// <summary>
        /// Collects layer snapshots and diagnostic state for UI inspection.
        /// </summary>
        Full,
    }
}
