// <copyright file="QuadraticDataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provider of training / test data for solving quadratic equations.
    /// </summary>
    public class QuadraticDataProvider : DataProvider
    {
        /// <inheritdoc/>
        public override int NumberOfInputs => 3;

        /// <inheritdoc/>
        public override int NumberOfOutputs => 4;

        /// <inheritdoc/>
        public override void GenerateData()
        {
            var records = new List<double[]>();
            for (var a = this.InputsLowerBound; a <= this.InputsUpperBound; a += this.InputsIncrement)
            {
                for (var b = this.InputsLowerBound; b <= this.InputsUpperBound; b += this.InputsIncrement)
                {
                    for (var c = this.InputsLowerBound; c <= this.InputsUpperBound; c += this.InputsIncrement)
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
            var numberOfTestRecords = (int)(inputsAndOutputs.Length * this.PercentageOfTestData / 100);
            var numberOfTrainingRecords = inputsAndOutputs.Length - numberOfTestRecords;
            this.TrainingData = inputsAndOutputs.Take(numberOfTrainingRecords).ToArray();
            this.TestData = inputsAndOutputs.Skip(numberOfTrainingRecords).Take(numberOfTestRecords).ToArray();
        }
    }
}
