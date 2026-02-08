// <copyright file="SingleNeuronNetwork.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.SingleNeuron
{
    using System.Text;
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// A neural network consisting of a single neuron (perceptron), which
    /// allows injection of different activation functions.
    /// </summary>
    public class SingleNeuronNetwork
    {
        private readonly IActivationFunctionProvider activationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleNeuronNetwork"/> class.
        /// </summary>
        /// <param name="activationProvider">
        /// Provides an activation function and its derivative.
        /// </param>
        public SingleNeuronNetwork(IActivationFunctionProvider activationProvider)
        {
            // Seed the randomiser so we get the same random numbers each time.
            var random = new Random(1);
            this.Bias = (random.NextSingle() * 2) - 1;
            this.activationProvider = activationProvider;
        }

        /// <summary>
        /// Gets the weights associated with each of the inputs to the neuron.
        /// </summary>
        public double[] Weights { get; private set; } = Array.Empty<double>();

        /// <summary>
        /// Gets the neuron's bias, which is necessary for the network to be able to solve
        /// the [0, 0, 0] case.
        /// </summary>
        /// <remarks>
        /// Without a bias, the sum of weighted inputs will always be zero
        /// when all 3 inputs are zero, meaning the input to the activation
        /// function will also be zero, and so the return value from the
        /// activation function will always be the same, (e.g. 0.5 for a
        /// sigmoid activation function), and no combination of weights will
        /// give the correct answer.
        /// </remarks>
        public double Bias { get; private set; }

        /// <summary>
        /// Gets the weights of the inputs from each iteration of the training
        /// loop, to allow visualisation of the training process.
        /// </summary>
        public List<double[]> WeightsOverTime { get; private set; } = new ();

        /// <summary>
        /// Gets the bias from each iteration of the training loop,
        /// to alloww visualisation of the training process.
        /// </summary>
        public List<double> BiasOverTime { get; private set; } = new ();

        /// <summary>
        /// Adjust the weights of the neural network to minimise the error for the training set.
        /// </summary>
        /// <param name="trainingData">Data to train the neetwork on.</param>
        /// <param name="numberOfIterations">Number of iterations to train for.</param>
        public void Train(TrainingDatum[] trainingData, int numberOfIterations)
        {
            var numberOfInputs = trainingData[0].Inputs.Length;

            // Initialise the weeights here rather than in the constructor
            // to allow a variable number of inputs.
            this.Weights = new double[numberOfInputs];

            // Seed the randomiser so we get the same random numbers each time.
            var random = new Random(1);
            for (var i = 0; i < this.Weights.Length; i++)
            {
                // Random values between -1 and 1.
                this.Weights[i] = (random.NextSingle() * 2) - 1;
            }

            this.CaptureWeightsAndBias();

            for (var iter = 0; iter < numberOfIterations; iter++)
            {
                foreach (var trainingDatum in trainingData)
                {
                    // Predict the output based on the training set example inputs.
                    var predictedOutput = this.Think(trainingDatum.Inputs);

                    // Calculate the error as the difference between the desired output
                    // and the predicted output.
                    // Error cost function: 0.5 * (desiredOutput - predictedOutput)^2, summed across every neuron
                    var errorInOutput = trainingDatum.DesiredOutput - predictedOutput;

                    // Iterate through the weights and adjust each one
                    this.AdjustInputWeightsAndBias(trainingDatum, errorInOutput, predictedOutput);

                    this.CaptureWeightsAndBias();
                }
            }
        }

        /// <summary>
        /// Call this to see what output the network gives for a set of inputs.
        /// </summary>
        /// <param name="neuronInputs">The inputs for which to get an output.</param>
        /// <returns>The predicted output.</returns>
        public double Think(int[] neuronInputs)
        {
            var sumOfWeightedInputs = this.SumOfWeightedInputs(neuronInputs);
            var neuronOutput = this.activationProvider.CalculateActivation(
                sumOfWeightedInputs + this.Bias);
            return neuronOutput;
        }

        /// <summary>
        /// Gets a string representation of the current state of the network.
        /// </summary>
        /// <returns>
        /// The weights of each input and the bias, as a formatted string.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Weights: [");
            foreach (var weight in this.Weights)
            {
                sb.Append($"{weight}, ");
            }

            sb.Append($"Bias: {this.Bias}]");
            return sb.ToString();
        }

        private void AdjustInputWeightsAndBias(
            TrainingDatum trainingDatum,
            double errorInOutput,
            double predictedOutput)
        {
            var derivative = this.activationProvider.CalculateGradient(predictedOutput);
            for (var i = 0; i < this.Weights.Length; i++)
            {
                // Calculate how much to adjust the weight by
                var neuronInput = trainingDatum.Inputs[i];
                var adjustment = neuronInput
                    * errorInOutput
                    * derivative;

                // Adjust the weight
                this.Weights[i] += adjustment;
            }

            // Adjust the bias too
            this.Bias += errorInOutput * derivative;
        }

        private double SumOfWeightedInputs(int[] neuronInputs)
        {
            var sumOfWeightedInputs = 0.0;
            for (var i = 0; i < neuronInputs.Length; i++)
            {
                sumOfWeightedInputs += neuronInputs[i] * this.Weights[i];
            }

            return sumOfWeightedInputs;
        }

        /// <summary>
        /// Capture the current weights and bias for tracking over time.
        /// </summary>
        private void CaptureWeightsAndBias()
        {
            var weightsCopy = new double[this.Weights.Length];
            Array.Copy(this.Weights, weightsCopy, this.Weights.Length);
            this.WeightsOverTime.Add(weightsCopy);
            this.BiasOverTime.Add(this.Bias);
        }
    }
}
