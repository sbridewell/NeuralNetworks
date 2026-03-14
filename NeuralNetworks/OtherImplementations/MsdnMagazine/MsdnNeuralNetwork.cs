// <copyright file="MsdnNeuralNetwork.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

// https://visualstudiomagazine.com/articles/2015/04/01/back-propagation-using-c.aspx
namespace Sde.NeuralNetworks.OtherImplementations.MsdnMagazine
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

#pragma warning disable SA1407 // Arithmetic expressions should declare precedence
#pragma warning disable SA1101 // Prefix local calls with this
    /// <summary>
    /// A neural network with back propagation, as described in MSDN Magazine.
    /// Has an input layer, one hidden layer, and an output layer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MsdnNeuralNetwork
    {
        /// <summary>
        /// Number of inputs into the network.
        /// </summary>
        private readonly int numberOfInputs; // number input nodes

        /// <summary>
        /// Number of neurons in the hidden layer.
        /// </summary>
        private readonly int numberOfHiddenNodes;

        /// <summary>
        /// Number of neurons in the output layer, i.e. number of outputs from the network.
        /// </summary>
        private readonly int numberOfOutputs;

        /// <summary>
        /// Input vlues.
        /// </summary>
        private readonly double[] inputs;

        /// <summary>
        /// Weights of the inputs from the input layer into the hidden layer.
        /// The first index identifies the input.
        /// The second index identifies the neuron in the hidden layer.
        /// </summary>
        private readonly double[][] inputToHiddenWeights; // input-hidden

        /// <summary>
        /// Biases of the neurons in the hidden lauyer.
        /// </summary>
        private readonly double[] hiddenLayerBiases;

        /// <summary>
        /// Values of the outputs from the neurons in the hidden layer.
        /// </summary>
        private readonly double[] hiddenLayerOutputs;

        /// <summary>
        /// Weights of tthe inputs from the hidden layer into the output layer.
        /// The first index identifies the neuron in the hidden layer.
        /// The second index identifies the neuron in the output layer.
        /// </summary>
        private readonly double[][] hiddenToOutputWeights; // hidden-output

        /// <summary>
        /// The biases of the neurons in the output layer.
        /// </summary>
        private readonly double[] outputLayerBiases;

        /// <summary>
        /// The outputs from the neurons in the output layer, i.e. the outputs
        /// from the network.
        /// </summary>
        private readonly double[] outputs;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private readonly Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsdnNeuralNetwork"/> class.
        /// </summary>
        /// <param name="numInput">Number of input neurons.</param>
        /// <param name="numHidden">Number of neurons in the hidden layer.</param>
        /// <param name="numOutput">Number of output neurons.</param>
        public MsdnNeuralNetwork(int numInput, int numHidden, int numOutput)
        {
            numberOfInputs = numInput;
            numberOfHiddenNodes = numHidden;
            numberOfOutputs = numOutput;

            inputs = new double[numInput];

            inputToHiddenWeights = MakeMatrix(numInput, numHidden, 0.0);
            hiddenLayerBiases = new double[numHidden];
            hiddenLayerOutputs = new double[numHidden];

            hiddenToOutputWeights = MakeMatrix(numHidden, numOutput, 0.0);
            outputLayerBiases = new double[numOutput];
            outputs = new double[numOutput];

            rnd = new Random(0);
            InitializeWeights(); // all weights and biases
        } // ctor

        /// <summary>
        /// Sets the weights. and biases.
        /// </summary>
        /// <param name="weights">
        /// The weights and biases as a flattened array.
        /// </param>
        public void SetWeights(double[] weights)
        {
            // copy serialized weights and biases in weights[] array
            // to i-h weights, i-h biases, h-o weights, h-o biases
            var numberOfWeights = numberOfInputs * numberOfHiddenNodes
                + numberOfHiddenNodes * numberOfOutputs
                + numberOfHiddenNodes + numberOfOutputs;
            if (weights.Length != numberOfWeights)
            {
                throw new ArgumentException("Bad weights array in SetWeights", nameof(weights));
            }

            var k = 0; // points into weights param

            for (var input = 0; input < numberOfInputs; ++input)
            {
                for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                {
                    inputToHiddenWeights[input][hidden] = weights[k++];
                }
            }

            for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
            {
                hiddenLayerBiases[hidden] = weights[k++];
            }

            for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
            {
                for (var output = 0; output < numberOfOutputs; ++output)
                {
                    hiddenToOutputWeights[hidden][output] = weights[k++];
                }
            }

            for (var output = 0; output < numberOfOutputs; ++output)
            {
                outputLayerBiases[output] = weights[k++];
            }
        }

        /// <summary>
        /// Gets all the weights and biases as a flattened array in the following order:
        /// <list type="bullet">
        /// <item>input-to-hidden weights</item>
        /// <item>hidden biases</item>
        /// <item>hidden-to-output weights</item>
        /// <item>output biases.</item>
        /// </list>
        /// </summary>
        /// <returns>The weights and biasses as a flattened array.</returns>
        public double[] GetWeights()
        {
            var numberOfWeights = numberOfInputs * numberOfHiddenNodes
                + numberOfHiddenNodes * numberOfOutputs
                + numberOfHiddenNodes + numberOfOutputs;
            var result = new double[numberOfWeights];
            var k = 0;
            for (var i = 0; i < inputToHiddenWeights.Length; ++i)
            {
                for (var j = 0; j < inputToHiddenWeights[0].Length; ++j)
                {
                    result[k++] = inputToHiddenWeights[i][j];
                }
            }

            for (var hidden = 0; hidden < hiddenLayerBiases.Length; ++hidden)
            {
                result[k++] = hiddenLayerBiases[hidden];
            }

            for (var i = 0; i < hiddenToOutputWeights.Length; ++i)
            {
                for (var j = 0; j < hiddenToOutputWeights[0].Length; ++j)
                {
                    result[k++] = hiddenToOutputWeights[i][j];
                }
            }

            for (var output = 0; output < outputLayerBiases.Length; ++output)
            {
                result[k++] = outputLayerBiases[output];
            }

            return result;
        }

        /// <summary>
        /// Calculates and returns the outputs of each of the neurons in the network.
        /// The hyperbolic tangent fuactivation functio is used for the hidden layer,
        /// and the SoftMax activation function is used for the output layer.
        /// The outputs are also stored in the <see cref="outputs"/> field.
        /// </summary>
        /// <param name="xValues">
        /// The inputs for which to calculate the outputs.
        /// </param>
        /// <returns>The calculated outputs.</returns>
        public double[] ComputeOutputs(double[] xValues)
        {
            var hiddenSums = new double[numberOfHiddenNodes]; // hidden nodes sums scratch array
            var outputSums = new double[numberOfOutputs]; // output nodes sums

            for (var input = 0; input < xValues.Length; ++input) // copy x-values to inputs
            {
                inputs[input] = xValues[input];
            }

            // note: no need to copy x-values unless you implement a ToString.
            // more efficient is to simply use the xValues[] directly.
            // compute i-h sum of weights * inputs
            for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
            {
                for (var input = 0; input < numberOfInputs; ++input)
                {
                    hiddenSums[hidden] += inputs[input] * inputToHiddenWeights[input][hidden];
                }
            }

            // add biases to hidden sums
            for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
            {
                hiddenSums[hidden] += hiddenLayerBiases[hidden];
            }

            // apply activation
            for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
            {
                hiddenLayerOutputs[hidden] = HyperTan(hiddenSums[hidden]);
            }

            // compute h-o sum of weights * hOutputs
            for (var output = 0; output < numberOfOutputs; ++output)
            {
                for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                {
                    outputSums[output] += hiddenLayerOutputs[hidden] * hiddenToOutputWeights[hidden][output];
                }
            }

            // add biases to output sums
            for (var output = 0; output < numberOfOutputs; ++output)
            {
                outputSums[output] += outputLayerBiases[output];
            }

            double[] softOut = Softmax(outputSums); // all outputs at once for efficiency
            Array.Copy(softOut, outputs, softOut.Length);
            double[] retResult = new double[numberOfOutputs]; // could define a GetOutputs
            Array.Copy(outputs, retResult, retResult.Length);
            return retResult;
        }

        /// <summary>
        /// Trains the network using back-propagation with momentum.
        /// </summary>
        /// <param name="trainData">Training data.</param>
        /// <param name="maxEpochs">Number of iterations.</param>
        /// <param name="learnRate">
        /// Controls how much each weight and bias can change in each iteration.
        /// Large values increase the speed of training at the risk of
        /// overshooting optimal weight values.
        /// </param>
        /// <param name="momentum">
        /// Helps to prevent training from getting stuck with local, non-optimal
        /// weight values, and prevents oscillation where training never converges
        /// to stable values.
        /// </param>
        /// <returns>
        /// A flattened version of the network's weights and biases after training.
        /// </returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design")]
        public double[] Train(
            double[][] trainData,
            int maxEpochs,
            double learnRate,
            double momentum)
        {
            // train using back-prop
            // back-prop specific arrays
            double[][] hiddenToOutputGradients = MakeMatrix(numberOfHiddenNodes, numberOfOutputs, 0.0); // hidden-to-output weight gradients
            double[] outputBiasGradients = new double[numberOfOutputs];                   // output bias gradients

            double[][] inputToHiddenGradients = MakeMatrix(numberOfInputs, numberOfHiddenNodes, 0.0);  // input-to-hidden weight gradients
            double[] hiddenBiasGradients = new double[numberOfHiddenNodes];                   // hidden bias gradients

            double[] outputSignals = new double[numberOfOutputs];                  // local gradient output signals - gradients w/o associated input terms
            double[] hiddenSignals = new double[numberOfHiddenNodes];                  // local gradient hidden node signals

            // back-prop momentum specific arrays
            double[][] ihPrevWeightsDelta = MakeMatrix(numberOfInputs, numberOfHiddenNodes, 0.0);
            double[] hPrevBiasesDelta = new double[numberOfHiddenNodes];
            double[][] hoPrevWeightsDelta = MakeMatrix(numberOfHiddenNodes, numberOfOutputs, 0.0);
            double[] oPrevBiasesDelta = new double[numberOfOutputs];

            int epoch = 0;
            double[] xValues = new double[numberOfInputs]; // inputs
            double[] targetValues = new double[numberOfOutputs]; // target values
            double derivative = 0.0;
            double errorSignal = 0.0;

            int[] sequence = new int[trainData.Length];
            for (int i = 0; i < sequence.Length; ++i)
            {
                sequence[i] = i;
            }

            int errInterval = maxEpochs / 10; // interval to check error
            while (epoch < maxEpochs)
            {
                ++epoch;

                if (epoch % errInterval == 0 && epoch < maxEpochs)
                {
                    double trainErr = Error(trainData);
                    Console.WriteLine("epoch = " + epoch + "  error = " +
                      trainErr.ToString("F4"));
                    Console.ReadLine();
                }

                Shuffle(sequence); // visit each training data in random order
                for (var ii = 0; ii < trainData.Length; ++ii)
                {
                    var idx = sequence[ii];
                    Array.Copy(trainData[idx], xValues, numberOfInputs);
                    Array.Copy(trainData[idx], numberOfInputs, targetValues, 0, numberOfOutputs);
                    ComputeOutputs(xValues); // copy xValues in, compute outputs

                    // indices: i = inputs, j = hiddens, k = outputs

                    // 1. compute output node signals (assumes softmax)
                    for (var output = 0; output < numberOfOutputs; ++output)
                    {
                        errorSignal = targetValues[output] - outputs[output];  // Wikipedia uses (o-t)
                        derivative = (1 - outputs[output]) * outputs[output]; // for softmax
                        outputSignals[output] = errorSignal * derivative;
                    }

                    // 2. compute hidden-to-output weight gradients using output signals
                    for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                    {
                        for (var output = 0; output < numberOfOutputs; ++output)
                        {
                            hiddenToOutputGradients[hidden][output] = outputSignals[output] * hiddenLayerOutputs[hidden];
                        }
                    }

                    // 2b. compute output bias gradients using output signals
                    for (var output = 0; output < numberOfOutputs; ++output)
                    {
                        outputBiasGradients[output] = outputSignals[output] * 1.0; // dummy assoc. input value
                    }

                    // 3. compute hidden node signals
                    for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                    {
                        derivative = (1 + hiddenLayerOutputs[hidden]) * (1 - hiddenLayerOutputs[hidden]); // for tanh
                        double sum = 0.0; // need sums of output signals times hidden-to-output weights
                        for (var output = 0; output < numberOfOutputs; ++output)
                        {
                            sum += outputSignals[output] * hiddenToOutputWeights[hidden][output]; // represents error signal
                        }

                        hiddenSignals[hidden] = derivative * sum;
                    }

                    // 4. compute input-hidden weight gradients
                    for (var input = 0; input < numberOfInputs; ++input)
                    {
                        for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                        {
                            inputToHiddenGradients[input][hidden] = hiddenSignals[hidden] * inputs[input];
                        }
                    }

                    // 4b. compute hidden node bias gradients
                    for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                    {
                        hiddenBiasGradients[hidden] = hiddenSignals[hidden] * 1.0; // dummy 1.0 input
                    }

                    // == update weights and biases

                    // update input-to-hidden weights
                    for (var input = 0; input < numberOfInputs; ++input)
                    {
                        for (int hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                        {
                            double delta = inputToHiddenGradients[input][hidden] * learnRate;
                            inputToHiddenWeights[input][hidden] += delta; // would be -= if (o-t)
                            inputToHiddenWeights[input][hidden] += ihPrevWeightsDelta[input][hidden] * momentum;
                            ihPrevWeightsDelta[input][hidden] = delta; // save for next time
                        }
                    }

                    // update hidden biases
                    for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                    {
                        var delta = hiddenBiasGradients[hidden] * learnRate;
                        hiddenLayerBiases[hidden] += delta;
                        hiddenLayerBiases[hidden] += hPrevBiasesDelta[hidden] * momentum;
                        hPrevBiasesDelta[hidden] = delta;
                    }

                    // update hidden-to-output weights
                    for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                    {
                        for (var output = 0; output < numberOfOutputs; ++output)
                        {
                            var delta = hiddenToOutputGradients[hidden][output] * learnRate;
                            hiddenToOutputWeights[hidden][output] += delta;
                            hiddenToOutputWeights[hidden][output] += hoPrevWeightsDelta[hidden][output] * momentum;
                            hoPrevWeightsDelta[hidden][output] = delta;
                        }
                    }

                    // update output node biases
                    for (int output = 0; output < numberOfOutputs; ++output)
                    {
                        var delta = outputBiasGradients[output] * learnRate;
                        outputLayerBiases[output] += delta;
                        outputLayerBiases[output] += oPrevBiasesDelta[output] * momentum;
                        oPrevBiasesDelta[output] = delta;
                    }
                } // each training item
            } // while

            var bestWeights = GetWeights();
            return bestWeights;
        } // Train

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            // data row
            for (var input = 0; input < numberOfInputs; input++)
            {
                sb.Append($"Input to hidden weights: {input}: ");
                for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
                {
                    sb.Append($"{inputToHiddenWeights[input][hidden]:F2}".PadLeft(6));
                }

                sb.AppendLine();
            }

            sb.Append("Hidden layer biases: ");
            for (var hidden = 0; hidden < numberOfHiddenNodes; ++hidden)
            {
                sb.Append($"{hiddenLayerBiases[hidden]:F2}.".PadLeft(6));
            }

            sb.AppendLine();

            for (var hidden = 0; hidden < numberOfHiddenNodes; hidden++)
            {
                sb.Append($"Hidden to output weights: {hidden}: ");
                for (var output = 0; output < numberOfOutputs; ++output)
                {
                    sb.Append($"{hiddenToOutputWeights[hidden][output]:F2}".PadLeft(6));
                }

                sb.AppendLine();
            }

            sb.Append($"Output layer biases: ");
            for (var output = 0; output < numberOfOutputs; ++output)
            {
                sb.Append($"{outputLayerBiases[output]:F2}".PadLeft(6));
            }

            sb.AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// Calculates the percentage of correct predictions by the netwoek
        /// when processing the test data.
        /// </summary>
        /// <param name="testData">Test data.</param>
        /// <returns>Percentage accuracy.</returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design.")]
        public double Accuracy(double[][] testData)
        {
            // percentage correct using winner-takes all
            var numCorrect = 0;
            var numWrong = 0;
            var xValues = new double[numberOfInputs]; // inputs
            var tValues = new double[numberOfOutputs]; // targets
            double[] yValues; // computed Y

            for (int i = 0; i < testData.Length; ++i)
            {
                Array.Copy(testData[i], xValues, numberOfInputs); // get x-values
                Array.Copy(testData[i], numberOfInputs, tValues, 0, numberOfOutputs); // get t-values
                yValues = ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues); // which cell in yValues has largest value?
                int tMaxIndex = MaxIndex(tValues);

                if (maxIndex == tMaxIndex)
                {
                    ++numCorrect;
                }
                else
                {
                    ++numWrong;
                }
            }

            return numCorrect * 1.0 / (numCorrect + numWrong);
        }

        /// <summary>
        /// Gets the index of the element in the supplied array with the highest value.
        /// </summary>
        /// <param name="vector">The array to checkk.</param>
        /// <returns>The index of the element with the highest value.</returns>
        private static int MaxIndex(double[] vector) // helper for Accuracy()
        {
            // index of largest value
            var bigIndex = 0;
            var biggestVal = vector[0];
            for (var i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }
            }

            return bigIndex;
        }

        private static double HyperTan(double x)
        {
            if (x < -20.0)
            {
                return -1.0; // approximation is correct to 30 decimals
            }
            else if (x > 20.0)
            {
                return 1.0;
            }
            else
            {
                return Math.Tanh(x);
            }
        }

        private static double[] Softmax(double[] outputSums)
        {
            // does all output nodes at once so scale
            // doesn't have to be re-computed each time
            var sum = 0.0;
            for (var output = 0; output < outputSums.Length; ++output)
            {
                sum += Math.Exp(outputSums[output]);
            }

            double[] result = new double[outputSums.Length];
            for (var output = 0; output < outputSums.Length; ++output)
            {
                result[output] = Math.Exp(outputSums[output]) / sum;
            }

            return result; // now scaled so that xi sum to 1.0
        }

        private static double[][] MakeMatrix(
            int rows,
            int cols,
            double v) // helper for ctor, Train
        {
            var result = new double[rows][];
            for (var r = 0; r < result.Length; ++r)
            {
                result[r] = new double[cols];
            }

            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < cols; ++j)
                {
                    result[i][j] = v;
                }
            }

            return result;
        }

        ////private static double[][] MakeMatrixRandom(int rows,
        ////  int cols, int seed) // helper for ctor, Train
        ////{
        ////  Random rnd = new Random(seed);
        ////  double hi = 0.01;
        ////  double lo = -0.01;
        ////  double[][] result = new double[rows][];
        ////  for (int r = 0; r < result.Length; ++r)
        ////    result[r] = new double[cols];
        ////  for (int i = 0; i < rows; ++i)
        ////    for (int j = 0; j < cols; ++j)
        ////      result[i][j] = (hi - lo) * rnd.NextDouble() + lo;
        ////  return result;
        ////}

        /// <summary>
        /// Initializes all weights and biases to small random values.
        /// </summary>
        private void InitializeWeights() // helper for ctor
        {
            var numWeights = numberOfInputs * numberOfHiddenNodes
                + numberOfHiddenNodes * numberOfOutputs
                + numberOfHiddenNodes + numberOfOutputs;
            var initialWeights = new double[numWeights];
            for (var i = 0; i < initialWeights.Length; ++i)
            {
                initialWeights[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
            }

            SetWeights(initialWeights);
        }

        /// <summary>
        /// Shuffles the supplied array.
        /// </summary>
        /// <param name="sequence">The array to shuffle.</param>
        private void Shuffle(int[] sequence) // instance method
        {
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        } // Shuffle

        /// <summary>
        /// Calculates the network's mean squared error.
        /// </summary>
        /// <param name="trainData">The training data.</param>
        /// <returns>The mean squared error.</returns>
        private double Error(double[][] trainData)
        {
            // average squared error per training item
            var sumSquaredError = 0.0;
            var xValues = new double[numberOfInputs]; // first numInput values in trainData
            var tValues = new double[numberOfOutputs]; // last numOutput values

            // walk thru each training case. looks like (6.9 3.2 5.7 2.3) (0 0 1)
            for (var i = 0; i < trainData.Length; ++i)
            {
                Array.Copy(trainData[i], xValues, numberOfInputs);
                Array.Copy(trainData[i], numberOfInputs, tValues, 0, numberOfOutputs); // get target values
                var yValues = ComputeOutputs(xValues); // outputs using current weights
                for (var output = 0; output < numberOfOutputs; ++output)
                {
                    var err = tValues[output] - yValues[output];
                    sumSquaredError += err * err;
                }
            }

            return sumSquaredError / trainData.Length;
        } // MeanSquaredError
    }
#pragma warning restore SA1101 // Prefix local calls with this
#pragma warning restore SA1407 // Arithmetic expressions should declare precedence
}
