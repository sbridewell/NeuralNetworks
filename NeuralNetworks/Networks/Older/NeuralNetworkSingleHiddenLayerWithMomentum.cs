// <copyright file="NeuralNetworkSingleHiddenLayerWithMomentum.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Networks.Older
{
    using Sde.NeuralNetworks.ActivationFunctionProviders;

#pragma warning disable SA1101 // Prefix local calls with this
    /// <summary>
    /// A simple feedforward neural network for solving quadratic equations from first principles.
    /// Has a single hidden layer.
    /// Uses back propogation to train the weights and biases of the network, based on the errors in the
    /// outputs from the output layer.
    /// Uses a user-selectable activation function during training.
    /// Implements momentum when updating weights and biases during training.
    /// Takes coefficients a, b and c as inputs, and output s the real and imaginary parts of both solutions.
    /// </summary>
    public class NeuralNetworkSingleHiddenLayerWithMomentum : INeuralNetworkSingleHiddenLayerWithMomentum
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

        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetworkSingleHiddenLayerWithMomentum"/> class.
        /// </summary>
        public NeuralNetworkSingleHiddenLayerWithMomentum()
        {
            random = new Random(42);
            Momentum = 0.0;
        }

        /// <inheritdoc/>
        public IActivationFunctionProvider HiddenActivationFunctionProvider { get; set; } = new LinearActivationFunctionProvider();

        /// <inheritdoc/>
        public IActivationFunctionProvider OutputActivationFunctionProvider { get; set; } = new LinearActivationFunctionProvider();

        /// <inheritdoc/>
        public string HiddenActivationFunctionProviderName => HiddenActivationFunctionProvider.GetType().Name;

        /// <inheritdoc/>
        public string OutputActivationFunctionProviderName => OutputActivationFunctionProvider.GetType().Name;

        /// <inheritdoc/>
        public int InputSize
        {
            get
            {
                return inputSize;
            }

            set
            {
                inputSize = value;
                InitialiseWeights();
            }
        }

        /// <inheritdoc/>
        public int HiddenSize
        {
            get
            {
                return hiddenSize;
            }

            set
            {
                hiddenSize = value;
                InitialiseWeights();
            }
        }

        /// <inheritdoc/>
        public int OutputSize
        {
            get
            {
                return outputSize;
            }

            set
            {
                outputSize = value;
                InitialiseWeights();
            }
        }

        /// <inheritdoc/>
        public double LearningRate { get; set; }

        /// <inheritdoc/>
        public double Momentum { get; set; }

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
            CurrentIteration = NumberOfIterations;
        }

        /// <inheritdoc/>
        public double[] Predict(double[] inputs)
        {
            var normalisedInputs = NormaliseInputs(inputs);
            var outputsFromHiddenLayer = ApplyHiddenWeightsAndBiases(normalisedInputs);
            var outputsFromOutputLayer = ApplyOutputWeightsAndBiases(outputsFromHiddenLayer);
            return outputsFromOutputLayer;
        }

        /// <inheritdoc/>
        public async Task Train(double[][] inputs, double[][] expectedOutputs)
        {
            InitialiseWeights();
            await Task.Run(() =>
            {
                for (CurrentIteration = 0; CurrentIteration < NumberOfIterations; CurrentIteration++)
                {
                    for (var sample = 0; sample < inputs.Length; sample++)
                    {
                        var input = NormaliseInputs(inputs[sample]);
                        var expectedOutput = expectedOutputs[sample];

                        // Forward propogation - does the same as the Predict method
                        var hidden = ApplyHiddenWeightsAndBiases(input);
                        var output = ApplyOutputWeightsAndBiases(hidden);

                        // Calculate errors
                        var outputLayerErrors = CalculateOutputLayerErrors(expectedOutput, output);
                        var hiddenLayerErrors = CalculateHiddenLayerErrors(outputLayerErrors);
                        OutputLayerMeanSquaredError = outputLayerErrors.Select(e => e * e).Average();
                        HiddenLayerMeanSquaredError = hiddenLayerErrors.Select(e => e * e).Average();

                        // Update weights and biases taking into account the calculated errors (backward propogation)
                        UpdateOutputLayerWeightsAndBiases(outputLayerErrors, hidden, LearningRate);
                        UpdateHiddenLayerWeightsAndBiases(hiddenLayerErrors, input, LearningRate);
                    }
                }

                NumberOfTrainingRecords = inputs.Length;
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
            var hidden = new double[HiddenSize];
            for (var h = 0; h < HiddenSize; h++)
            {
                var sumOfWeightedInputs = 0.0;
                for (var i = 0; i < InputSize; i++)
                {
                    sumOfWeightedInputs += inputs[i] * InputToHiddenWeights[i][h];
                }

                sumOfWeightedInputs += HiddenBiases[h];
                hidden[h] = HiddenActivationFunctionProvider.CalculateActivation(sumOfWeightedInputs);
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
            var output = new double[OutputSize];
            for (var o = 0; o < OutputSize; o++)
            {
                var sumOfWeightedInputs = 0.0;
                for (var h = 0; h < HiddenSize; h++)
                {
                    sumOfWeightedInputs += hidden[h] * HiddenToOutputWeights[h][o];
                }

                sumOfWeightedInputs += OutputBiases[o];
                output[o] = OutputActivationFunctionProvider.CalculateActivation(sumOfWeightedInputs);
            }

            return output;
        }

        /// <summary>
        /// Calculates the errors in the outputs from the output layer, based on the differences
        /// between the actual outputs and the expected outputs as supplied in the training data.
        /// </summary>
        /// <param name="expected">The expected outputs.</param>
        /// <param name="output">The actual outputs.</param>
        /// <returns>The errors in the outputs from the output layer.</returns>
        private double[] CalculateOutputLayerErrors(double[] expected, double[] output)
        {
            var outputErrors = new double[OutputSize];
            for (var o = 0; o < OutputSize; o++)
            {
                outputErrors[o] = expected[o] - output[o];
            }

            return outputErrors;
        }

        /// <summary>
        /// Calculates the errors in the outputs from the hidden layer, based on the errors
        /// in the outputs from the output layer and the weights connecting the hidden layer
        /// to the output layer.
        /// </summary>
        /// <param name="outputErrors">The errors in the outputs from the output layer.</param>
        /// <returns>The errors in the outputs from the hidden layer.</returns>
        private double[] CalculateHiddenLayerErrors(double[] outputErrors)
        {
            var hiddenErrors = new double[HiddenSize];
            for (var h = 0; h < HiddenSize; h++)
            {
                double error = 0;
                for (var o = 0; o < OutputSize; o++)
                {
                    error += outputErrors[o] * HiddenToOutputWeights[h][o];
                }

                hiddenErrors[h] = error;
            }

            return hiddenErrors;
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
            for (var o = 0; o < OutputSize; o++)
            {
                for (var i = 0; i < HiddenSize; i++)
                {
                    var delta = learningRate * outputErrors[o] * hidden[i];
                    var momentumTerm = Momentum * hoPrevWeightsDelta[i][o];
                    HiddenToOutputWeights[i][o] += delta + momentumTerm;
                    hoPrevWeightsDelta[i][o] = delta;
                }

                var biasDelta = learningRate * outputErrors[o];
                var biasMomentum = Momentum * oPrevBiasesDelta[o];
                OutputBiases[o] += biasDelta + biasMomentum;
                oPrevBiasesDelta[o] = biasDelta;
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
            for (var h = 0; h < HiddenSize; h++)
            {
                for (var i = 0; i < InputSize; i++)
                {
                    var delta = learningRate * hiddenErrors[h] * inputs[i];
                    var momentumTerm = Momentum * ihPrevWeightsDelta[i][h];
                    InputToHiddenWeights[i][h] += delta + momentumTerm;
                    ihPrevWeightsDelta[i][h] = delta;
                }

                var biasDelta = learningRate * hiddenErrors[h];
                var biasMomentum = Momentum * hPrevBiasesDelta[h];
                HiddenBiases[h] += biasDelta + biasMomentum;
                hPrevBiasesDelta[h] = biasDelta;
            }
        }

        /// <summary>
        /// Initialises the weights and biases with random values in the range -1..1.
        /// Also initialises previous-delta arrays used by momentum.
        /// </summary>
        private void InitialiseWeights()
        {
            InputToHiddenWeights = new double[InputSize][];
            for (var i = 0; i < InputSize; i++)
            {
                InputToHiddenWeights[i] = new double[HiddenSize];
                for (var h = 0; h < HiddenSize; h++)
                {
                    InputToHiddenWeights[i][h] = (random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1..1]
                }
            }

            HiddenBiases = new double[HiddenSize];
            for (var h = 0; h < HiddenSize; h++)
            {
                HiddenBiases[h] = (random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1..1]
            }

            HiddenToOutputWeights = new double[HiddenSize][];
            for (var h = 0; h < HiddenSize; h++)
            {
                HiddenToOutputWeights[h] = new double[OutputSize];
                for (var o = 0; o < OutputSize; o++)
                {
                    HiddenToOutputWeights[h][o] = (random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1, 1]
                }
            }

            OutputBiases = new double[OutputSize];
            for (var o = 0; o < OutputSize; o++)
            {
                OutputBiases[o] = (random.NextDouble() - 0.5) * 2.0 * 0.1; // [-1, 1]
            }

            // Initialise momentum arrays to zero with the same shapes as the weight matrices.
            ihPrevWeightsDelta = new double[InputSize][];
            for (var i = 0; i < InputSize; i++)
            {
                ihPrevWeightsDelta[i] = new double[HiddenSize];
            }

            hPrevBiasesDelta = new double[HiddenSize];
            hoPrevWeightsDelta = new double[HiddenSize][];
            for (var h = 0; h < HiddenSize; h++)
            {
                hoPrevWeightsDelta[h] = new double[OutputSize];
            }

            oPrevBiasesDelta = new double[OutputSize];
        }
    }
#pragma warning restore SA1101 // Prefix local calls with this
}
