// <copyright file="QuadraticEquation.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.TrainingDataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// A quadratic equation, e.g. a^2 + b + c = 0.
    /// </summary>
    public class QuadraticEquation
    {
        /// <summary>
        /// Gets or sets the coefficient which is multiplied by x squared.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Gets or sets the cowfficient which is multiplied by x.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Gets or sets the coefficient which is the constant term.
        /// </summary>
        public double C { get; set; }

        /// <summary>
        /// Finds the two values of x for which the equation is true.
        /// </summary>
        /// <returns>The solutions of the equation.</returns>
        [SuppressMessage(
            "StyleCop.CSharp.SpacingRules",
            "SA1010:Opening square brackets should be spaced correctly",
            Justification = "This array is too short to span multiple lines.")]
        public Complex[] SolveForX()
        {
            if (A < 0.001 && A > -0.001)
            {
                if (B < 0.001 && B > -0.001)
                {
                    // c = 0
                }

                // linear equation bx + c = 0 => x - -c // b
                return new Complex[] { new Complex(-C / B, 0), new Complex(-C / B, 0) };
            }

            double discriminant = B * B - 4 * A * C;
            Complex sqrtDiscriminant = Complex.Sqrt(discriminant);
            Complex root1 = (-B + sqrtDiscriminant) / (2 * A);
            Complex root2 = (-B - sqrtDiscriminant) / (2 * A);
            return [root1, root2];
        }

        /////// <summary>
        /////// Evaluates the polynomial for the supplied value of x.
        /////// </summary>
        /////// <param name="x">The value of x.</param>
        /////// <returns>The return value of the polynomial.</returns>
        ////public double Evaluate(Complex x)
        ////{
        ////    return (this.A * x * x) + (this.B * x) + this.C;
        ////}
    }
}
