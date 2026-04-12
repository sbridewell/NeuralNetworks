// <copyright file="MinMaxScaler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.FeatureScaling
{
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// Min-max normalisation is defined as (x - min(x)) / (max(x) - min(x)).
    /// </summary>
    public class MinMaxScaler : IFeatureScaler
    {
        /// <inheritdoc/>
        public Vector Scale(Vector unscaledVector)
        {
            var scaledElements = new List<double>();
            var min = unscaledVector.Elements.Min();
            var max = unscaledVector.Elements.Max();
            foreach (var element in unscaledVector.Elements)
            {
                scaledElements.Add((element - min) / (max - min));
            }

            return new Vector(scaledElements.ToArray());
        }

        /// <inheritdoc/>
        public Vector ScaleBack(Vector scaledVector)
        {
            throw new NotImplementedException();
        }
    }
}
