// <copyright file="IRootFinder.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.TrainingDataProviders
{
    /// <summary>
    /// Interface for classes that can find the root of a function, i.e. the value of x for which f(x) = 0.
    /// </summary>
    public interface IRootFinder
    {
        /// <summary>
        /// Attempts to find a root of the given function.
        /// </summary>
        /// <param name="functionToFindRootOf">
        /// The function to find the root of.
        /// </param>
        /// <param name="derivativeOfFunction">
        /// The derivative function of <see paramref="functionToFindRootOf"/>.
        /// </param>
        /// <param name="initialGuess">
        /// An arbitrary initial guess for the root.
        /// The closer this is to the actual root, the faster the method will converge.
        /// </param>
        /// <param name="tolerance">
        /// The method will return once the absolute value of the function at the current
        /// guess is less than this value.
        /// </param>
        /// <param name="epsilon">
        /// If the derivative function returns less than this value, the method will
        /// throw an exception to avoid division by zero.
        /// </param>
        /// <param name="maxIterations">
        /// The maximum number of iterations to perform before giving up and throwing an
        /// exception.
        /// </param>
        /// <returns>
        /// A root of the function, or an approximation of a root, if the method converges.
        /// </returns>
        double FindRoot(
            Func<double, double> functionToFindRootOf,
            Func<double, double> derivativeOfFunction,
            double initialGuess,
            double tolerance = 1e-7,
            double epsilon = 1e-10,
            int maxIterations = 1000);
    }
}
