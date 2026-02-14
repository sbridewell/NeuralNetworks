// <copyright file="INeuralNetworkSingleHiddenLayerWithMomentum.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// Interface for a neural network intended to solve quadratic equations,
    /// with one input layer, one hidden layer and one output layer.
    /// </summary>
    public interface INeuralNetworkSingleHiddenLayerWithMomentum
    {
        /// <summary>
        /// Gets or sets the provider of activation and gradient functions for the neurons in the hidden layer.
        /// </summary>
        [JsonIgnore]
        IActivationFunctionProvider HiddenActivationFunctionProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider of activation and gradient functions for the neurons in the output layer.
        /// </summary>
        [JsonIgnore]
        IActivationFunctionProvider OutputActivationFunctionProvider { get; set; }

        /// <summary>
        /// Gets the name of the provider of activation and gradient functions for the neurons in the hidden layer.
        /// </summary>
        string HiddenActivationFunctionProviderName { get; }

        /// <summary>
        /// Gets the name of the provider of activation and gradient functions for the neurons in the output layer.
        /// </summary>
        string OutputActivationFunctionProviderName { get; }

        /// <summary>
        /// Gets or sets the number of inputs into the network.
        /// </summary>
        int InputSize { get; set; }

        /// <summary>
        /// Gets or sets the number of neurons in the hidden layer.
        /// </summary>
        int HiddenSize { get; set; }

        /// <summary>
        /// Gets or sets the number of outputs from the network.
        /// </summary>
        int OutputSize { get; set; }

        /// <summary>
        /// Gets or sets the amount by which the weights and biases can change during a single epoch of training.
        /// </summary>
        double LearningRate { get; set; }

        /// <summary>
        /// Gets or sets the momentum coefficient in range [0,1). 0 disables momentum.
        /// This is the amount by which the weights and biases are changed in the
        /// same direction as they were changed in the previous epoch of training.
        /// </summary>
        double Momentum { get; set; }

        /// <summary>
        /// Gets the weights of the connections from the input layer to the hidden layer.
        /// </summary>
        double[][] InputToHiddenWeights { get; }

        /// <summary>
        /// Gets the biases of the neurons in the hidden layer.
        /// </summary>
        double[] HiddenBiases { get; }

        /// <summary>
        /// Gets the weights of the connections from the hidden layer to the output layer.
        /// </summary>
        double[][] HiddenToOutputWeights { get; }

        /// <summary>
        /// Gets the biases of the neurons in the output layer.
        /// </summary>
        double[] OutputBiases { get; }

        /// <summary>
        /// Gets or sets the number of records the network was trained with.
        /// </summary>
        int NumberOfTrainingRecords { get; set; }

        /// <summary>
        /// Gets or sets the number of epochs to train the network for.
        /// </summary>
        int NumberOfIterations { get; set; }

        /// <summary>
        /// Gets the current epoch number during training.
        /// </summary>
        [JsonIgnore]
        int CurrentIteration { get; }

        /// <summary>
        /// Gets or sets the time spent training the network so far.
        /// </summary>
        TimeSpan TimeSpentTraining { get; set; }

        /// <summary>
        /// Gets or sets the estimated time left to finish training the network.
        /// </summary>
        [JsonIgnore]
        TimeSpan EstimatedTrainingTimeLeft { get; set; }

        /// <summary>
        /// Gets the mean squared error in the outputs from the hidden layer.
        /// </summary>
        double HiddenLayerMeanSquaredError { get; }

        /// <summary>
        /// Gets the mean squared error in the outputs from the output layer.
        /// </summary>
        double OutputLayerMeanSquaredError { get; }

        /// <summary>
        /// Stops the network training.
        /// </summary>
        void Stop();

        /// <summary>
        /// Predicts the outputs for the given inputs.
        /// </summary><param name="inputs">Inputs to the network.</param>
        /// <returns>The outputs for the given inputs.</returns>
        double[] Predict(double[] inputs);

        /// <summary>
        /// Trains the network.
        /// </summary>
        /// <param name="inputs">Array of inputs (a, b, c).</param>
        /// <param name="expectedOutputs">
        /// Array of expected outputs for the inputs in the corresponding input array.
        /// </param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "I might change this method later to make each neuron its own object")]
        Task Train(double[][] inputs, double[][] expectedOutputs);
    }
}
