// <copyright file="AddTwoNumbersTrainingDataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.TrainingDataProviders
{
    /// <summary>
    /// Provider of training / test data for solving the problem of adding two numbers together.
    /// </summary>
    public class AddTwoNumbersTrainingDataProvider : TrainingDataProvider
    {
        /// <inheritdoc/>
        public override string DisplayName => "Add two numbers";

        /// <inheritdoc/>
        public override int NumberOfInputs => 2;

        /// <inheritdoc/>
        public override int NumberOfOutputs => 1;

        /// <inheritdoc/>
        public override void GenerateData()
        {
            var records = new List<double[]>();
            for (var input1 = InputsLowerBound; input1 <= InputsUpperBound; input1 += InputsIncrement)
            {
                for (var input2 = InputsLowerBound; input2 <= InputsUpperBound; input2 += InputsIncrement)
                {
                    records.Add(new double[] { input1, input2, input1 + input2 });
                }
            }

            var inputsAndOutputs = records.Shuffle().ToArray();
            var numberOfTestRecords = (int)(inputsAndOutputs.Length * PercentageOfTestData / 100);
            var numberOfTrainingRecords = inputsAndOutputs.Length - numberOfTestRecords;
            TrainingData = inputsAndOutputs.Take(numberOfTrainingRecords).ToArray();
            TestData = inputsAndOutputs.Skip(numberOfTrainingRecords).Take(numberOfTestRecords).ToArray();
        }
    }
}
