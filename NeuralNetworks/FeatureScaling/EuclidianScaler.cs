// <copyright file="EuclidianScaler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.FeatureScaling
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Scales a vector so that its Euclidian magnitude becomes 1.
    /// The elements of the scaled array will be within the range -1..1,
    /// but most likely none of the values will be as large as 1 or -1.
    /// </summary>
    public class EuclidianScaler : IFeatureScaler
    {
        /// <inheritdoc/>
        public Vector Scale(Vector unscaledVector)
        {
            var scaledElements = new List<double>();
            var euclidianMagnitude = unscaledVector.GetEuclidianMagnitude();
            foreach (var element in unscaledVector.Elements)
            {
                scaledElements.Add(element / euclidianMagnitude);
            }

            return new Vector(scaledElements.ToArray());
        }
    }
}
