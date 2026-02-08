// <copyright file="MsdnProgram.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

// https://visualstudiomagazine.com/articles/2015/04/01/back-propagation-using-c.aspx
namespace Sde.NeuralNetworks.MsdnMagazine
{
    using System;

    /// <summary>
    /// Class containing the main entry point for the MSDN back-propagation demo.
    /// </summary>
    public static class MsdnProgram
    {
        /// <summary>
        /// Main entry point for the back-propagation demo.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("\nBegin neural network back-propagation demo");

            int numInput = 4; // number features
            int numHidden = 5;
            int numOutput = 3; // number of classes for Y
            int numRows = 1000;
            int seed = 1; // gives nice demo

            Console.WriteLine("\nGenerating " + numRows +
              " artificial data items with " + numInput + " features");
            double[][] allData = MakeAllData(numInput, numHidden, numOutput, numRows, seed);
            Console.WriteLine("Done");

            ////ShowMatrix(allData, allData.Length, 2, true);

            Console.WriteLine("\nCreating train (80%) and test (20%) matrices");
            double[][] trainData;
            double[][] testData;
            SplitTrainTest(allData, 0.80, seed, out trainData, out testData);
            Console.WriteLine("Done\n");

            Console.WriteLine("Training data:");
            ShowMatrix(trainData, 4, 2, true);
            Console.WriteLine("Test data:");
            ShowMatrix(testData, 4, 2, true);

            Console.WriteLine("Creating a " + numInput + "-" + numHidden +
              "-" + numOutput + " neural network");
            var nn = new MsdnNeuralNetwork(numInput, numHidden, numOutput);

            int maxEpochs = 1000;
            double learnRate = 0.05;
            double momentum = 0.01;
            Console.WriteLine("\nSetting maxEpochs = " + maxEpochs);
            Console.WriteLine("Setting learnRate = " + learnRate.ToString("F2"));
            Console.WriteLine("Setting momentum  = " + momentum.ToString("F2"));

            Console.WriteLine("\nStarting training");
            double[] weights = nn.Train(trainData, maxEpochs, learnRate, momentum);
            Console.WriteLine("Done");
            Console.WriteLine("\nFinal neural network model weights and biases:\n");
            ShowVector(weights, 2, 10, true);

            ////double[] y = nn.ComputeOutputs(new double[] { 1.0, 2.0, 3.0, 4.0 });
            ////ShowVector(y, 3, 3, true);

            double trainAcc = nn.Accuracy(trainData);
            Console.WriteLine("\nFinal accuracy on training data = " +
              trainAcc.ToString("F4"));

            double testAcc = nn.Accuracy(testData);
            Console.WriteLine("Final accuracy on test data     = " +
              testAcc.ToString("F4"));

            Console.WriteLine("\nEnd back-propagation demo\n");
            Console.ReadLine();
        } // Main

        /// <summary>
        /// Displays a matric (jagged array) on the console.
        /// </summary>
        /// <param name="matrix">The matrix to display.</param>
        /// <param name="numRows">Maximum number of rows to display.</param>
        /// <param name="decimals">Number of decimal places to display.</param>
        /// <param name="indices">True to display indices as well as element values.</param>
        private static void ShowMatrix(double[][] matrix, int numRows, int decimals, bool indices)
        {
            int len = matrix.Length.ToString().Length;
            for (int i = 0; i < numRows; ++i)
            {
                if (indices)
                {
                    Console.Write("[" + i.ToString().PadLeft(len) + "]  ");
                }

                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    double v = matrix[i][j];
                    if (v >= 0.0)
                    {
                        Console.Write(" "); // '+'
                    }

                    Console.Write(v.ToString("F" + decimals) + "  ");
                }

                Console.WriteLine(string.Empty);
            }

            if (numRows < matrix.Length)
            {
                Console.WriteLine(". . .");
                int lastRow = matrix.Length - 1;
                if (indices)
                {
                    Console.Write("[" + lastRow.ToString().PadLeft(len) + "]  ");
                }

                for (int j = 0; j < matrix[lastRow].Length; ++j)
                {
                    double v = matrix[lastRow][j];
                    if (v >= 0.0)
                    {
                        Console.Write(" "); // '+'
                    }

                    Console.Write(v.ToString("F" + decimals) + "  ");
                }
            }

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Displays a vector (1 dimensional arrray) on the console.
        /// </summary>
        /// <param name="vector">The vector to display.</param>
        /// <param name="decimals">Number of decimal places to display.</param>
        /// <param name="lineLen">Start a new line after this many characters.</param>
        /// <param name="newLine">True to write a new line at the end of the output.</param>
        private static void ShowVector(double[] vector, int decimals, int lineLen, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
            {
                if (i > 0 && i % lineLen == 0)
                {
                    Console.WriteLine(string.Empty);
                }

                if (vector[i] >= 0)
                {
                    Console.Write(" ");
                }

                Console.Write(vector[i].ToString("F" + decimals) + " ");
            }

            if (newLine)
            {
                Console.WriteLine(string.Empty);
            }
        }

        /// <summary>
        /// Creates training / test data.
        /// </summary>
        /// <param name="numInput">Number of inputs to the network.</param>
        /// <param name="numHidden">Number of neurons in the network's hidden layer.</param>
        /// <param name="numOutput">Number of outputs from the network.</param>
        /// <param name="numRows">Number of rows of data to create.</param>
        /// <param name="seed">Seed for the random number generator.</param>
        /// <returns>The created data.</returns>
        private static double[][] MakeAllData(int numInput, int numHidden, int numOutput, int numRows, int seed)
        {
            Random rnd = new Random(seed);
            int numWeights = (numInput * numHidden) + numHidden +
              (numHidden * numOutput) + numOutput;
            double[] weights = new double[numWeights]; // actually weights & biases
            for (int i = 0; i < numWeights; ++i)
            {
                weights[i] = (20.0 * rnd.NextDouble()) - 10.0; // [-10.0 to 10.0]
            }

            Console.WriteLine("Generating weights and biases:");
            ShowVector(weights, 2, 10, true);

            double[][] result = new double[numRows][]; // allocate return-result
            for (int i = 0; i < numRows; ++i)
            {
                result[i] = new double[numInput + numOutput]; // 1-of-N in last column
            }

            var gnn =
              new MsdnNeuralNetwork(numInput, numHidden, numOutput); // generating NN
            gnn.SetWeights(weights);

            for (int r = 0; r < numRows; ++r) // for each row
            {
                // generate random inputs
                double[] inputs = new double[numInput];
                for (int i = 0; i < numInput; ++i)
                {
                    inputs[i] = (20.0 * rnd.NextDouble()) - 10.0; // [-10.0 to -10.0]
                }

                // compute outputs
                double[] outputs = gnn.ComputeOutputs(inputs);

                // translate outputs to 1-of-N
                double[] oneOfN = new double[numOutput]; // all 0.0

                int maxIndex = 0;
                double maxValue = outputs[0];
                for (int i = 0; i < numOutput; ++i)
                {
                    if (outputs[i] > maxValue)
                    {
                        maxIndex = i;
                        maxValue = outputs[i];
                    }
                }

                oneOfN[maxIndex] = 1.0;

                // place inputs and 1-of-N output values into curr row
                int c = 0; // column into result[][]
                for (int i = 0; i < numInput; ++i) // inputs
                {
                    result[r][c++] = inputs[i];
                }

                for (int i = 0; i < numOutput; ++i) // outputs
                {
                    result[r][c++] = oneOfN[i];
                }
            } // each row

            return result;
        } // MakeAllData

        /// <summary>
        /// Splits the data into training and test data.
        /// </summary>
        /// <param name="allData">The data to split.</param>
        /// <param name="trainPct">Percentage of the data to use as training data.</param>
        /// <param name="seed">Seed for the random number generator.</param>
        /// <param name="trainData">The training data is returned in this parameter.</param>
        /// <param name="testData">The test data is returned in this parameter.</param>
        private static void SplitTrainTest(double[][] allData, double trainPct, int seed, out double[][] trainData, out double[][] testData)
        {
            Random rnd = new Random(seed);
            int totRows = allData.Length;
            int numTrainRows = (int)(totRows * trainPct); // usually 0.80
            int numTestRows = totRows - numTrainRows;
            trainData = new double[numTrainRows][];
            testData = new double[numTestRows][];

            double[][] copy = new double[allData.Length][]; // ref copy of data
            for (int i = 0; i < copy.Length; ++i)
            {
                copy[i] = allData[i];
            }

            for (int i = 0; i < copy.Length; ++i) // scramble order
            {
                int r = rnd.Next(i, copy.Length);
                double[] tmp = copy[r];
                copy[r] = copy[i];
                copy[i] = tmp;
            }

            for (int i = 0; i < numTrainRows; ++i)
            {
                trainData[i] = copy[i];
            }

            for (int i = 0; i < numTestRows; ++i)
            {
                testData[i] = copy[i + numTrainRows];
            }
        } // SplitTrainTest
    } // Program
} // ns
