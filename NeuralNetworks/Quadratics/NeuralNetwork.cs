// <copyright file="NeuralNetwork.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// A simple feedforward neural network for solving quadratic equations from first principles.
    /// Has a single hidden layer.
    /// Uses back propogation to train the weights and biases of the network, based on the errors in the
    /// outputs from the output layer.
    /// Uses a user-selectable activation function during training.
    /// Implements momentum when updating weights and biases during training.
    /// Takes coefficients a, b and c as inputs, and output s the real and imaginary parts of both solutions.
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly Random random;
        private int inputSize;
        private int hiddenSize;
        private int outputSize;

        // Momentum state (previous parameter deltas).
        private double[][] ihPrevWeightsDelta = Array.Empty<double[]>(); // input -> hidden
        private double[] hPrevBiasesDelta = Array.Empty<double>(); // hidden biases
        private double[][] hoPrevWeightsDelta = Array.Empty<double[]>(); // hidden -> output
        private double[] oPrevBiasesDelta = Array.Empty<double>(); // output biases
        // Stored pre-activation sums required for correct backpropagation.
        private double[] hiddenPreActivations = Array.Empty<double>();
        private double[] outputPreActivations = Array.Empty<double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetwork"/> class.
        /// </summary>
        public NeuralNetwork()
        {
            this.random = new Random(42);
            this.Momentum = 0.0;
        }

        /// <inheritdoc/>
        public IActivationFunctionProvider HiddenActivationFunctionProvider { get; set; } = new LinearActivationProvider();

        /// <inheritdoc/>
        public IActivationFunctionProvider OutputActivationFunctionProvider { get; set; } = new LinearActivationProvider();

        /// <inheritdoc/>
        public string HiddenActivationFunctionProviderName => this.HiddenActivationFunctionProvider.GetType().Name;

        /// <inheritdoc/>
        public string OutputActivationFunctionProviderName => this.OutputActivationFunctionProvider.GetType().Name;

        /// <inheritdoc/>
        public int InputSize
        {
            get
            {
                return this.inputSize;
            }

            set
            {
                this.inputSize = value;
                this.InitialiseWeights();
            }
        }

        /// <inheritdoc/>
        public int HiddenSize
        {
            get
            {
                return this.hiddenSize;
            }

            set
            {
                this.hiddenSize = value;
                this.InitialiseWeights();
            }
        }

        /// <inheritdoc/>
        public int OutputSize
        {
            get
            {
                return this.outputSize;
            }

            set
            {
                this.outputSize = value;
                this.InitialiseWeights();
            }
        }

        /// <inheritdoc/>
        public double LearningRate { get; set; }

        /// <inheritdoc/>
        public double Momentum { get; set; } // TODO: add momentum control to form

        /// <inheritdoc/>
        public double[][] InputToHiddenWeights { get; private set; } = Array.Empty<double[]>();

        /// <inheritdoc/>
        public double[] HiddenBiases { get; private set; } = Array.Empty<double>();

        /// <inheritdoc/>
        public double[][] HiddenToOutputWeights { get; private set; } = Array.Empty<double[]>();

        /// <inheritdoc/>
        public double[] OutputBiases { get; private set; } = Array.Empty<double>();

        /// <inheritdoc/>
        public int NumberOfTrainingRecords { get; set; }

        /// <inheritdoc/>
        public int NumberOfIterations { get; set; }

        /// <inheritdoc/>
        public int CurrentIteration { get; private set; }

        /// <inheritdoc/>
        public TimeSpan TimeSpentTraining { get; set; }

        /// <inheritdoc/>
        public TimeSpan EstimatedTrainingTimeLeft { get; set; }

        /// <inheritdoc/>
        public double HiddenLayerMeanSquaredError { get; private set; }

        /// <inheritdoc/>
        public double OutputLayerMeanSquaredError { get; private set; }

        /// <inheritdoc/>
        public void Stop()
        {
            this.CurrentIteration = this.NumberOfIterations;
        }

        /// <inheritdoc/>
        public double[] Predict(double[] inputs)
        {
            var normalisedInputs = NormaliseInputs(inputs);
            var outputsFromHiddenLayer = this.ApplyHiddenWeightsAndBiases(normalisedInputs);
            var outputsFromOutputLayer = this.ApplyOutputWeightsAndBiases(outputsFromHiddenLayer);
            return outputsFromOutputLayer;
        }

        /// <inheritdoc/>
        public async Task Train(double[][] inputs, double[][] expectedOutputs)
        {
            this.InitialiseWeights();
            await Task.Run(() =>
            {
                for (this.CurrentIteration = 0; this.CurrentIteration < this.NumberOfIterations; this.CurrentIteration++)
                {
                    for (var sample = 0; sample < inputs.Length; sample++)
                    {
                        var input = NormaliseInputs(inputs[sample]);
                        var expectedOutput = expectedOutputs[sample];

                        // Forward propogation - does the same as the Predict method
                        var hidden = this.ApplyHiddenWeightsAndBiases(input);
                        var output = this.ApplyOutputWeightsAndBiases(hidden);

                        // Calculate raw errors for reporting
                        var rawOutputErrors = new double[this.OutputSize];
                        for (var o = 0; o < this.OutputSize; o++)
                        {
                            rawOutputErrors[o] = expectedOutput[o] - output[o];
                        }

                        // Calculate deltas (local gradients) to use for weight updates
                        var outputLayerDeltas = this.CalculateOutputLayerDeltas(expectedOutput, output);

                        // Calculate raw hidden errors for reporting (before applying activation derivative)
                        var rawHiddenErrors = new double[this.HiddenSize];
                        for (var h = 0; h < this.HiddenSize; h++)
                        {
                            var err = 0.0;
                            for (var o = 0; o < this.OutputSize; o++)
                            {
                                err += rawOutputErrors[o] * this.HiddenToOutputWeights[h][o];
                            }

                            rawHiddenErrors[h] = err;
                        }

                        var hiddenLayerDeltas = this.CalculateHiddenLayerDeltas(outputLayerDeltas);

                        this.OutputLayerMeanSquaredError = rawOutputErrors.Select(e => e * e).Average();
                        this.HiddenLayerMeanSquaredError = rawHiddenErrors.Select(e => e * e).Average();

                        // Update weights and biases taking into account the calculated errors (backward propogation)
                        this.UpdateOutputLayerWeightsAndBiases(outputLayerDeltas, hidden, this.LearningRate);
                        this.UpdateHiddenLayerWeightsAndBiases(hiddenLayerDeltas, input, this.LearningRate);
                    }
                }

                this.NumberOfTrainingRecords = inputs.Length;
            });
        }

        /// <summary>
        /// Scales the values in the supplied array to values in the range -1 to 1,
        /// whilst maintaining their relative magnitudes.
        /// </summary>
        /// <param name="input">The array to normalise.</param>
        /// <returns>Array of normalised values.</returns>
        private static double[] NormaliseInputs(double[] input)
        {
            var maxAbs = Math.Max(1.0, input.Max(i => Math.Abs(i)));
            var normalised = new double[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                normalised[i] = input[i] / maxAbs;
            }

            return normalised;
        }

        /// <summary>
        /// Calculates the values passed to the hidden layer for the given inputs,
        /// the input to hidden weights for the network, and the biases of the
        /// hidden layer.
        /// The values passed into each neuron in the hidden layer are calculated as
        /// the sum of the products of each input value and its corresponding weight,
        /// plus the neuron's bias, and then passed into the activation function.
        /// </summary>
        /// <param name="inputs">The values input into the network.</param>
        /// <returns>The values passeed to the hidden layer.</returns>
        private double[] ApplyHiddenWeightsAndBiases(double[] inputs)
        {
            var hidden = new double[this.HiddenSize];
            this.hiddenPreActivations = new double[this.HiddenSize];
            for (var h = 0; h < this.HiddenSize; h++)
            {
                var sumOfWeightedInputs = 0.0;
                for (var i = 0; i < this.InputSize; i++)
                {
                    sumOfWeightedInputs += inputs[i] * this.InputToHiddenWeights[i][h];
                }

                sumOfWeightedInputs += this.HiddenBiases[h];
                // store pre-activation for gradient calculation during backprop
                this.hiddenPreActivations[h] = sumOfWeightedInputs;
                hidden[h] = this.HiddenActivationFunctionProvider.CalculateActivation(sumOfWeightedInputs);
            }

            return hidden;
        }

        /// <summary>
        /// Calculates the values passed to the output layer for the given hidden layer outputs,
        /// the hidden to output weights for the network, and the biases of the output layer.
        /// The values passed out of each neuron in the output layer are calculated as
        /// the sum of the products of each value received from the hidden layer and its
        /// corresponding weight, plus the neuron's bias, and then passed into the activation
        /// function.
        /// </summary>
        /// <param name="hidden">The values passed from the hidden layer to the output layer.</param>
        /// <returns>The values passed to the output layer.</returns>
        private double[] ApplyOutputWeightsAndBiases(double[] hidden)
        {
            var output = new double[this.OutputSize];
            this.outputPreActivations = new double[this.OutputSize];
            for (var o = 0; o < this.OutputSize; o++)
            {
                var sumOfWeightedInputs = 0.0;
                for (var h = 0; h < this.HiddenSize; h++)
                {
                    sumOfWeightedInputs += hidden[h] * this.HiddenToOutputWeights[h][o];
                }

                sumOfWeightedInputs += this.OutputBiases[o];
                // store pre-activation for gradient calculation during backprop
                this.outputPreActivations[o] = sumOfWeightedInputs;
                output[o] = this.OutputActivationFunctionProvider.CalculateActivation(sumOfWeightedInputs);
            }

            return output;
        }

        /// <summary>
        /// Calculates the output-layer deltas (local gradients) for backpropagation.
        /// Returns delta_o = (expected_o - output_o) * f'_output(preActivation_o).
        /// </summary>
        /// <param name="expected">The expected outputs.</param>
        /// <param name="output">The actual outputs.</param>
        /// <returns>The output-layer deltas.</returns>
        private double[] CalculateOutputLayerDeltas(double[] expected, double[] output)
        {
            var outputDeltas = new double[this.OutputSize];
            for (var o = 0; o < this.OutputSize; o++)
            {
                var error = expected[o] - output[o]; // raw error
                var grad = this.OutputActivationFunctionProvider.CalculateGradient(this.outputPreActivations.Length > o ? this.outputPreActivations[o] : output[o]);
                outputDeltas[o] = error * grad;
            }

            return outputDeltas;
        }

        /// <summary>
        /// Calculates the hidden-layer deltas (local gradients) from output-layer deltas.
        /// Returns delta_h = f'_hidden(preActivation_h) * sum_o (delta_o * w_h_o).
        /// </summary>
        /// <param name="outputDeltas">The output-layer deltas.</param>
        /// <returns>The hidden-layer deltas.</returns>
        private double[] CalculateHiddenLayerDeltas(double[] outputDeltas)
        {
            var hiddenDeltas = new double[this.HiddenSize];
            for (var h = 0; h < this.HiddenSize; h++)
            {
                var sum = 0.0;
                for (var o = 0; o < this.OutputSize; o++)
                {
                    sum += outputDeltas[o] * this.HiddenToOutputWeights[h][o];
                }

                var grad = this.HiddenActivationFunctionProvider.CalculateGradient(this.hiddenPreActivations.Length > h ? this.hiddenPreActivations[h] : 0.0);
                hiddenDeltas[h] = grad * sum;
            }

            return hiddenDeltas;
        }

        /// <summary>
        /// Updates the weights and biases of the output layer based on the errors in the outputs
        /// from the output layer and the values passed to the hidden layer.
        /// Implements optional momentum: parameter update = delta + momentum * previousDelta.
        /// </summary>
        /// <param name="outputErrors">Errors in the outputs from the output layer.</param>
        /// <param name="hidden">Values passed into the hidden layer, as adjusted by the input to hidden weights.</param>
        /// <param name="learningRate">Controls how much the weights and biases can change per iteration.</param>
        private void UpdateOutputLayerWeightsAndBiases(double[] outputErrors, double[] hidden, double learningRate)
        {
            for (var o = 0; o < this.OutputSize; o++)
            {
                for (var i = 0; i < this.HiddenSize; i++)
                {
                    var delta = learningRate * outputErrors[o] * hidden[i];
                    var momentumTerm = this.Momentum * this.hoPrevWeightsDelta[i][o];
                    this.HiddenToOutputWeights[i][o] += delta + momentumTerm;
                    this.hoPrevWeightsDelta[i][o] = delta;
                }

                var biasDelta = learningRate * outputErrors[o];
                var biasMomentum = this.Momentum * this.oPrevBiasesDelta[o];
                this.OutputBiases[o] += biasDelta + biasMomentum;
                this.oPrevBiasesDelta[o] = biasDelta;
            }
        }

        /// <summary>
        /// Updates the weights and biases of the hidden layer based on the errors in the outputs
        /// from the hidden layer and the values passed to the hidden layer.
        /// Implements optional momentum: parameter update = delta + momentum * previousDelta.
        /// </summary>
        /// <param name="hiddenErrors">The errors in the outputs from the hidden layer.</param>
        /// <param name="inputs">The values passed to the hidden layer.</param>
        /// <param name="learningRate">Controls how much the weights and biases can change per iteration.</param>
        private void UpdateHiddenLayerWeightsAndBiases(double[] hiddenErrors, double[] inputs, double learningRate)
        {
            for (var h = 0; h < this.HiddenSize; h++)
            {
                for (var i = 0; i < this.InputSize; i++)
                {
                    var delta = learningRate * hiddenErrors[h] * inputs[i];
                    var momentumTerm = this.Momentum * this.ihPrevWeightsDelta[i][h];
                    this.InputToHiddenWeights[i][h] += delta + momentumTerm;
                    this.ihPrevWeightsDelta[i][h] = delta;
                }

                var biasDelta = learningRate * hiddenErrors[h];
                var biasMomentum = this.Momentum * this.hPrevBiasesDelta[h];
                this.HiddenBiases[h] += biasDelta + biasMomentum;
                this.hPrevBiasesDelta[h] = biasDelta;
            }
        }

        /// <summary>
        /// Initialises the weights and biases with random values in the range -1..1.
        /// Also initialises previous-delta arrays used by momentum.
        /// </summary>
        private void InitialiseWeights()
        {
            this.InputToHiddenWeights = new double[this.InputSize][];
            for (var i = 0; i < this.InputSize; i++)
            {
                this.InputToHiddenWeights[i] = new double[this.HiddenSize];
                for (var h = 0; h < this.HiddenSize; h++)
                {
                    this.InputToHiddenWeights[i][h] = (this.random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1..1]
                }
            }

            this.HiddenBiases = new double[this.HiddenSize];
            for (var h = 0; h < this.HiddenSize; h++)
            {
                this.HiddenBiases[h] = (this.random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1..1]
            }

            this.HiddenToOutputWeights = new double[this.HiddenSize][];
            for (var h = 0; h < this.HiddenSize; h++)
            {
                this.HiddenToOutputWeights[h] = new double[this.OutputSize];
                for (var o = 0; o < this.OutputSize; o++)
                {
                    this.HiddenToOutputWeights[h][o] = (this.random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1, 1]
                }
            }

            this.OutputBiases = new double[this.OutputSize];
            for (var o = 0; o < this.OutputSize; o++)
            {
                this.OutputBiases[o] = (this.random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1, 1]
            }

            // Initialise momentum arrays to zero with the same shapes as the weight matrices.
            this.ihPrevWeightsDelta = new double[this.InputSize][];
            for (var i = 0; i < this.InputSize; i++)
            {
                this.ihPrevWeightsDelta[i] = new double[this.HiddenSize];
            }

            this.hPrevBiasesDelta = new double[this.HiddenSize];
            this.hoPrevWeightsDelta = new double[this.HiddenSize][];
            for (var h = 0; h < this.HiddenSize; h++)
            {
                this.hoPrevWeightsDelta[h] = new double[this.OutputSize];
            }

            this.oPrevBiasesDelta = new double[this.OutputSize];
        }
    }
}
