// <copyright file="LayerState.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// DTO representing a single layer's serialisable state.
    /// </summary>
    /// <param name="layerType"></param>
    /// <param name="weights">The weights of the inputs into the layer.</param>
    /// <param name="biases">The biases of the neurons in the layer.</param>
    public sealed record LayerState(
        string layerType,
        Matrix weights,
        Vector biases);
}
