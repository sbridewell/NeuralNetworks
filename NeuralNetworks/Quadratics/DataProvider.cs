// <copyright file="DataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Abstract base class for <see cref="IDataProvider"/> implementations.
    /// </summary>
    public abstract class DataProvider : IDataProvider
    {
        /// <inheritdoc/>
        public double PercentageOfTestData { get; set; } = 0.1;

        /// <inheritdoc/>
        public double InputsLowerBound { get; set; } = 0;

        /// <inheritdoc/>
        public double InputsUpperBound { get; set; } = 100;

        /// <inheritdoc/>
        public double InputsIncrement { get; set; } = 1;

        /// <inheritdoc/>
        public double[][] TrainingData { get; protected set; } = Array.Empty<double[]>();

        /// <inheritdoc/>
        public double[][] TestData { get; protected set; } = Array.Empty<double[]>();

        /// <inheritdoc/>
        public virtual int NumberOfInputs { get; }

        /// <inheritdoc/>
        public virtual int NumberOfOutputs { get; }

        /// <inheritdoc/>
        public abstract void GenerateData();

        /// <summary>
        /// Splits a single array of inputs and outputs into separate arrays.
        /// </summary>
        /// <param name="inputsAndOutputs">The array to split.</param>
        /// <returns>The inputs and outputs.</returns>
        [SuppressMessage(
            "Blocker Code Smell",
            "S2368:Public methods should not have multidimensional array parameters",
            Justification = "As per design")]
        public (double[][] inputs, double[][] outputs) SplitIntoInputsAndOutputs(double[][] inputsAndOutputs)
        {
            var inputs = new double[inputsAndOutputs.Length][];
            var outputs = new double[inputsAndOutputs.Length][];
            for (int i = 0; i < inputsAndOutputs.Length; i++)
            {
                inputs[i] = inputsAndOutputs[i].Take(this.NumberOfInputs).ToArray();
                outputs[i] = inputsAndOutputs[i].Skip(this.NumberOfInputs).Take(this.NumberOfOutputs).ToArray();
            }

            return (inputs, outputs);
        }
    }
}
