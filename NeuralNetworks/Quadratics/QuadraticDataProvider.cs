// <copyright file="QuadraticDataProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Quadratics
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    /// <summary>
    /// Provider of training / test data for solving quadratic equations.
    /// </summary>
    public class QuadraticDataProvider
    {
        private readonly double[][] inputsAndOutputs;
        private readonly int numberOfInputs = 3;
        private readonly int numberOfOutputs = 4;
        private readonly int numberOfTrainingRecords;
        private readonly int numberOfTestRecords;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticDataProvider"/> class.
        /// </summary>
        /// <param name="lowerBound">Thhe lowest value to return in the provided data.</param>
        /// <param name="upperBound">The highest value to return in the provided data.</param>
        /// <param name="increment">Gets the interval between values in the provided data.</param>
        /// <param name="percentageOfTestData">
        /// The percentage of the data to reserve for testing the network.
        /// </param>
        public QuadraticDataProvider(double lowerBound, double upperBound, double increment, double percentageOfTestData)
        {
            var records = new List<double[]>();
            for (double a = lowerBound; a <= upperBound; a += increment)
            {
                for (double b = lowerBound; b <= upperBound; b += increment)
                {
                    for (double c = lowerBound; c <= upperBound; c += increment)
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

            this.inputsAndOutputs = records.Shuffle().ToArray();
            this.numberOfTestRecords = (int)(this.inputsAndOutputs.Length * percentageOfTestData);
            this.numberOfTrainingRecords = this.inputsAndOutputs.Length - this.numberOfTestRecords;
        }

        /// <summary>
        /// Gets data for training the network.
        /// </summary>
        /// <returns>Training data.</returns>
        public double[][] GetTrainingData()
        {
            return this.inputsAndOutputs;
        }

        /// <summary>
        /// Gets data for testing the network.
        /// </summary>
        /// <returns>Test data.</returns>
        public double[][] GetTestData()
        {
            return this.inputsAndOutputs
                .Skip(this.numberOfTrainingRecords)
                .Take(this.numberOfTestRecords)
                .ToArray();
        }

        /// <summary>
        /// Splits a single array of inputs and outputs into separate arrays.
        /// </summary>
        /// <param name="inputsAndOutputs">The array ti split.</param>
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
                inputs[i] = inputsAndOutputs[i].Take(this.numberOfInputs).ToArray();
                outputs[i] = inputsAndOutputs[i].Skip(this.numberOfInputs).Take(this.numberOfOutputs).ToArray();
            }

            return (inputs, outputs);
        }
    }
}
