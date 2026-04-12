// <copyright file="INetworkPersistence.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Persistence
{
    using Sde.NeuralNetworks.Networks;

    /// <summary>
    /// Abstraction for exporting and importing the state of a multi-layer neural
    /// network.
    /// </summary>
    public interface INetworkPersistence
    {
        /// <summary>
        /// Exports the state of the supplied network as a
        /// <see cref="NetworkState"/> DTO.
        /// </summary>
        /// <param name="network">The network to export.</param>
        /// <returns>
        /// A <see cref="NetworkState"/> instance containing the state of the
        /// supplied network.
        /// </returns>
        NetworkState ExportState(IMultiLayerNetwork network);

        /// <summary>
        /// Imports the supplied <see cref="NetworkState"/> into the supplied
        /// network, restoring parameters.
        /// </summary>
        /// <param name="network">The network to import into.</param>
        /// <param name="state">The state to import.</param>
        void ImportState(IMultiLayerNetwork network, NetworkState state);
    }
}
