// <copyright file="NetworkState.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    /// <summary>
    /// DTO representing the persisted state of a multi-layer network, including
    /// per-layer parameters.
    /// </summary>
    /// <param name="layers">The states of the layers in the network.</param>
    /// <param name="learningRate">
    /// The learning rate used when training the network. Controls the step size
    /// applied to parameter updates during optimisation.
    /// Typical values are small positive numbers (for example 0.01).
    /// Implementations should document whether this value is applied globally or
    /// mirrored to per-layer settings and should validate that the value is
    /// finite and non-negative.
    /// </param>
    /// <param name="momentum">
    /// The momentum coefficient used when applying gradient descent with momentum.
    /// Momentum smooths and accelerates updates by incorporating previous update
    /// velocity. Values are typically in the range 0.0 (no momentum) to less than
    /// 1.0.
    /// Implementations should validate that the value is finite and in a sensible
    /// range.
    /// </param>
    /// <param name="numberOfIterations">
    /// The number of times to iterate through the training dataset when training
    /// the network.
    /// </param>
    public sealed record NetworkState(
        IReadOnlyList<LayerState> layers,
        double learningRate,
        double momentum,
        int numberOfIterations);
}
