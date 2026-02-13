// <copyright file="IDataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Interface for providers of training / test data for a neural network.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Gets or sets the percentage of the data to reserve for testing the network.
        /// 0 for all training data.
        /// 1 for all test data (which would be silly, we can't do much with an
        /// untrained network).
        /// </summary>
        double PercentageOfTestData { get; set; }

        /// <summary>
        /// Gets or sets the lower bound for the range of values to use in the provided data.
        /// </summary>
        double InputsLowerBound { get; set; }

        /// <summary>
        /// Gets or sets the upper bound for the range of values to use in the inputs of
        /// the provided data.
        /// </summary>
        double InputsUpperBound { get; set; }

        /// <summary>
        /// Gets or sets the increment between values in the inputs of the provided data.
        /// </summary>
        double InputsIncrement { get; set; }

        /// <summary>
        /// Gets data for training the network.
        /// </summary>
        double[][] TrainingData { get; }

        /// <summary>
        /// Gets data for testing the network.
        /// </summary>
        double[][] TestData { get; }

        /// <summary>
        /// Gets the number of inputs into the neural network.
        /// Each element of the test data and training data must have the same number
        /// of elements as NumberOfInputs + NumberOfOutputs.
        /// </summary>
        int NumberOfInputs { get; }

        /// <summary>
        /// Gets the number of outputs from the neural network.
        /// Each element of the test data and training data must have the same number
        /// of elements as NumberOfInputs + NumberOfOutputs.
        /// </summary>
        int NumberOfOutputs { get; }

        /// <summary>
        /// Generates test and training data.
        /// </summary>
        void GenerateData();

        /// <summary>
        /// Splits a single array of inputs and outputs into separate arrays.
        /// </summary>
        /// <param name="inputsAndOutputs">The array to split.</param>
        /// <returns>The inputs and outputs.</returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design")]
        (double[][] inputs, double[][] outputs) SplitIntoInputsAndOutputs(double[][] inputsAndOutputs);
    }
}
