// <copyright file="MultiLayerNetworkVisualisationForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Forms
{
    using System.Windows.Forms;
    using Sde.NeuralNetworks.ActivationFunctionProviders;
    using Sde.NeuralNetworks.FeatureScaling;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Networks;
    using Sde.NeuralNetworks.TrainingDataProviders;

    /// <summary>
    /// Simple form for displaying a visual representation of a neural network.
    /// </summary>
    public partial class MultiLayerNetworkVisualisationForm : Form
    {
        private readonly Random rnd;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MultiLayerNetworkVisualisationForm"/> class.
        /// </summary>
        public MultiLayerNetworkVisualisationForm()
        {
            this.InitializeComponent();

            ////var provider = new AddTwoNumbersTrainingDataProvider()
            var provider = new QuadraticEquationTrainingDataProvider()
            {
                InputsLowerBound = -10,
                InputsUpperBound = 20,
                InputsIncrement = 1,
            };
            provider.GenerateData();

            // Derive architecture from the provider (inputs/outputs).
            // Choose a small hidden layer size for this short-term tactical example.
            int inputSize = provider.NumberOfInputs;
            int outputSize = provider.NumberOfOutputs;
            int hiddenSize = Math.Max(1, inputSize * 2); // simple heuristic

            this.rnd = new Random();

            // Build hidden and output layers with random weights/biases that reflect the network dimensions.
            // TODO: MultiLayerNetwork constructor which works out input size and output size from training data aand accepts an array of hidden layer sizes, to ensure network is self-consisten
            var hiddenLayer1 = this.CreateHiddenLayer1(hiddenSize, inputSize);
            var outputLayer = this.CreateOutputLayer(outputSize, hiddenSize);

            // TODO: change ITrainingDataProvider to return IEnumerable<(double[] inputs, double[] expected)> directly to avoid this conversion step.
            // Convert provider.TrainingData (double[][]) to IEnumerable<(Vector inputs, Vector expected)>.
            var trainingRows = provider.TrainingData ?? Array.Empty<double[]>();
            var samples = trainingRows
                .Select(row =>
                {
                    var inputs = row.Take(provider.NumberOfInputs).ToArray();
                    var expecttedOutputs = row.Skip(provider.NumberOfInputs).ToArray();
                    return (inputs: new Vector(inputs), expected: new Vector(expecttedOutputs));
                })
                .ToArray();

            var layers = new NeuralNetworkLayer[] { hiddenLayer1, outputLayer };
            var network = this.CreateNetwork(layers, samples);

            // Don't start training the network until the form has been shown, to
            // ensure the UI is responsive and the visualiser is visible during
            // training.
            this.Shown += async (s, e) =>
            {
                try
                {
                    await network.TrainAsync(
                        samples,
                        numberOfiterations: Math.Max(
                            1,
                            network.NumberOfIterations));
                    MessageBox.Show("Training complete");
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("Training cancelled");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        this,
                        $"Training failed: {ex.Message}",
                        "Training error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };
        }

        private NeuralNetworkLayer CreateHiddenLayer1(int hiddenSize, int inputSize)
        {
            var hiddenWeights = this.CreateRandomMatrix(hiddenSize, inputSize);
            var hiddenBiases = this.CreateRandomVector(hiddenSize);
            var hiddenLayer = new NeuralNetworkLayer(
                hiddenWeights,
                hiddenBiases,
                activationFunctionProvider: new SigmoidActivationFunctionProvider(),
                learningRate: 1e-4,
                momentum: 0.00);
            return hiddenLayer;
        }

        private NeuralNetworkLayer CreateOutputLayer(int outputSize, int hiddenSize)
        {
            var outputWeights = this.CreateRandomMatrix(outputSize, hiddenSize);
            var outputBiases = this.CreateRandomVector(outputSize);
            var outputLayer = new NeuralNetworkLayer(
                outputWeights,
                outputBiases,
                learningRate: 1e-4,
                momentum: 0.00);
            return outputLayer;
        }

        private MultiLayerNetwork CreateNetwork(
            NeuralNetworkLayer[] layers,
            IEnumerable<(Vector inputs, Vector expected)> samples)
        {
            var inputScaler = ZScoreScaler.Fit(samples.Select(s => s.inputs));
            var outputScaler = ZScoreScaler.Fit(samples.Select(s => s.expected));
            var network = new MultiLayerNetwork(
                layers,
                inputScaler: inputScaler,
                outputScaler: outputScaler);
            this.multiLayerNetworkVisualiser1.Network = network;
            return network;
        }

        /// <summary>
        /// Builds a vector of random biases of neurons in a single layer of a
        /// multi-layer network.
        /// </summary>
        /// <param name="length">The numberf of neurons in the layer.</param>
        /// <returns>A randomly initialised layer.</returns>
        private Vector CreateRandomVector(int length)
        {
            var values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = ((this.rnd.NextDouble() * 2.0) - 1.0) * 0.5;
            }

            return new Vector(values);
        }

        /// <summary>
        /// Builds a matrix of random weights of connections between two layers
        /// in a multi-layer network.
        /// The rows in the matrix are the outputs.
        /// </summary>
        /// <param name="rows">
        /// The number of neurons in the destination layer.
        /// </param>
        /// <param name="cols">
        /// The number of neurons in the source layer.
        /// </param>
        /// <returns>A matrix of randomly initialised values.</returns>
        private Matrix CreateRandomMatrix(int rows, int cols)
        {
            var rowsVec = new Vector[rows];
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var values = new double[cols];
                for (int columnIndex = 0; columnIndex < cols; columnIndex++)
                {
                    // small initial weights
                    values[columnIndex]
                        = ((this.rnd.NextDouble() * 2.0) - 1.0) * 0.5;
                }

                rowsVec[rowIndex] = new Vector(values);
            }

            return new Matrix(rowsVec);
        }
    }
}
