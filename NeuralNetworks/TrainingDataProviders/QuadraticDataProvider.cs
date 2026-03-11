// <copyright file="QuadraticDataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

using Sde.NeuralNetworks.Quadratics;

namespace Sde.NeuralNetworks.TrainingDataProviders
{
    /// <summary>
    /// Provider of training / test data for solving quadratic equations.
    /// </summary>
    public class QuadraticDataProvider : DataProvider
    {
        /// <inheritdoc/>
        public override string DisplayName => "Solve quadratic equation";

        /// <inheritdoc/>
        public override int NumberOfInputs => 3;

        /// <inheritdoc/>
        public override int NumberOfOutputs => 4;

        /// <inheritdoc/>
        public override void GenerateData()
        {
            var records = new List<double[]>();
            for (var a = InputsLowerBound; a <= InputsUpperBound; a += InputsIncrement)
            {
                for (var b = InputsLowerBound; b <= InputsUpperBound; b += InputsIncrement)
                {
                    for (var c = InputsLowerBound; c <= InputsUpperBound; c += InputsIncrement)
                    {
                        if (a < 0.001 && a > -0.001 && b < 0.001 && b > -0.001)
                        {
                            // c = 0, there is no solution for x.
                            continue;
                        }

                        var equation = new QuadraticEquation { A = a, B = b, C = c };
                        var solutions = equation.SolveForX();
                        records.Add(new double[]
                        {
                            a,
                            b,
                            c,
                            solutions[0].Real,
                            solutions[0].Imaginary,
                            solutions[1].Real,
                            solutions[1].Imaginary,
                        });
                    }
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
