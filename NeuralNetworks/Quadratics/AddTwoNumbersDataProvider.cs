// <copyright file="AddTwoNumbersDataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    /// <summary>
    /// Provider of training / test data for solving the problem of adding two numbers together.
    /// </summary>
    public class AddTwoNumbersDataProvider : DataProvider
    {
        /// <inheritdoc/>
        public override int NumberOfInputs => 2;

        /// <inheritdoc/>
        public override int NumberOfOutputs => 1;

        /// <inheritdoc/>
        public override void GenerateData()
        {
            var records = new List<double[]>();
            for (var input1 = this.InputsLowerBound; input1 <= this.InputsUpperBound; input1 += this.InputsIncrement)
            {
                for (var input2 = this.InputsLowerBound; input2 <= this.InputsUpperBound; input2 += this.InputsIncrement)
                {
                    records.Add(new double[] { input1, input2, input1 + input2 });
                }
            }

            var inputsAndOutputs = records.Shuffle().ToArray();
            var numberOfTestRecords = (int)(inputsAndOutputs.Length * this.PercentageOfTestData / 100);
            var numberOfTrainingRecords = inputsAndOutputs.Length - numberOfTestRecords;
            this.TrainingData = inputsAndOutputs.Take(numberOfTrainingRecords).ToArray();
            this.TestData = inputsAndOutputs.Skip(numberOfTrainingRecords).Take(numberOfTestRecords).ToArray();
        }
    }
}
