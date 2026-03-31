// <copyright file="INeuralNetworkLayer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Interface for a single layer in a neural network.
    /// </summary>
    public interface INeuralNetworkLayer
    {
        #region properties

        /// <summary>
        /// Gets the learning rate used by the model during training.
        /// The larger this is, the more the model's parameters are updated in response to the
        /// computed gradients.
        /// </summary>
        double LearningRate { get; }

        /// <summary>
        /// Gets the momentum coefficient (typically in the range 0..1).
        /// The larger this is, the more the previous weight update influences the current
        /// update, which can help accelerate training and smooth out updates.
        /// </summary>
        double Momentum { get; }

        /// <summary>
        /// Gets the weight matrix for the layer.
        /// Each row represents a destination neuron and each column a source value.
        /// </summary>
        Matrix Weights { get; }

        /// <summary>
        /// Gets the bias vector applied to each neuron in this layer.
        /// </summary>
        Vector Biases { get; }

        /// <summary>
        /// Gets the previous velocity (delta) matrix used for momentum updates of the weights.
        /// This is updated each time <see cref="ApplyGradientsWithMomentum(Matrix,Vector)"/> is called.
        /// </summary>
        Matrix PreviousWeightDeltas { get; }

        /// <summary>
        /// Gets the previous velocity (delta) vector used for momentum updates of the biases.
        /// This is updated each time <see cref="ApplyGradientsWithMomentum(Matrix,Vector)"/> is called.
        /// </summary>
        Vector PreviousBiasDeltas { get; }

        #endregion

        #region methods

        /// <summary>
        /// Calculates the values to be passed to the next layer in the network,
        /// from the values passed from the previous layer in the network.
        /// </summary>
        /// <param name="inputs">
        /// The values passed from the previous layer in the network.
        /// </param>
        /// <returns>
        /// The values to be passed to the next layer in the network.
        /// </returns>
        Vector FeedForward(Vector inputs);

        /// <summary>
        /// Compares this layer's outputs for the supplied inputs with the expected outputs,
        /// computes the gradients for the weights and biases, applies those gradients to this
        /// layer (updating parameters and momentum state), and returns the error signal to be
        /// propagated to the previous layer.
        /// </summary>
        /// <param name="inputs">Input vector supplied to this layer.</param>
        /// <param name="expectedOutputs">Expected output vector for the same inputs.</param>
        /// <returns>
        /// The input error signal (to propagate back to the previous layer).
        /// The layer's weights, biases and momentum state are updated as part of this call.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when dimensions are incompatible.</exception>
        Vector CalculateGradients(Vector inputs, Vector expectedOutputs);

        /// <summary>
        /// Applies gradients using stochastic gradient descent with momentum.
        /// The update rule is:
        /// <c>velocity := momentum * prevVelocity + learningRate * gradient</c>
        /// <c>param := param - velocity</c>.
        /// </summary>
        /// <param name="weightGradients">
        /// Gradient matrix for the weights. Must have the same dimensions as <see cref="Weights"/>.
        /// </param>
        /// <param name="biasGradients">
        /// Gradient vector for the biases. Must have the same length as <see cref="Biases"/>.
        /// </param>
        /// <remarks>
        /// This method mutates the current layer: it updates <see cref="Weights"/> and <see cref="Biases"/>
        /// in-place and stores the new velocity state in <see cref="PreviousWeightDeltas"/> and
        /// <see cref="PreviousBiasDeltas"/> for use in subsequent updates.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="weightGradients"/> or <paramref name="biasGradients"/> is null.
        /// </exception>
        void ApplyGradientsWithMomentum(Matrix weightGradients, Vector biasGradients);

        #endregion
    }
}
