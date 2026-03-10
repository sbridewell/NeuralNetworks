// <copyright file="IHaveADisplayName.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks
{
    /// <summary>
    /// Interface for classes which have a display name which can be shown to the user.
    /// </summary>
    public interface IHaveADisplayName
    {
        /// <summary>
        /// Gets a human-readable display name for the class, suitable for displaying in the UI.
        /// </summary>
        string DisplayName { get; }
    }
}
