// <copyright file="TrainingProgressEventArgs.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    /// <summary>
    /// Arguments for the event that is raised when the training progress of a
    /// neural network changes.
    /// </summary>
    public class TrainingProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the current iteration of the training process.
        /// Each iteration corresponds to one pass through the entire training
        /// dataset.
        /// </summary>
        public int CurrentIteration { get; init; }

        /// <summary>
        /// Gets the index into the training dataset of the current sample being
        /// processed.
        /// </summary>
        public int CurrentSampleIndex { get; init; }

        /// <summary>
        /// Gets the total number of samples in the training dataset.
        /// </summary>
        public int TotalSamples { get; init; }

        /// <summary>
        /// Gets the percentage of the training process that has been completed.
        /// This is an integer between 0 and 100, inclusive.
        /// </summary>
        public int PercentComplete { get; init; }
    }
}
