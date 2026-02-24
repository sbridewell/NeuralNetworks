// <copyright file="NetworkPropertiesViewModel.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// View model for the network properties form.
    /// </summary>
    public class NetworkPropertiesViewModel : INotifyPropertyChanged
    {
        private int numberOfIterations = 1000;
        private decimal learningRate = 0.001m;
        private decimal momentum = 0.001m;
        private int nodesInHiddenLayer = 5;

        /// <summary>
        /// The event which is raised when the value of a property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the number of iterations used to train the network.
        /// </summary>
        public int NumberOfIterations
        {
            get => this.numberOfIterations;
            set
            {
                this.numberOfIterations = value;
                this.OnChanged();
            }
        }

        /// <summary>
        /// Gets or sets the learning rate used by the network.
        /// </summary>
        public decimal LearningRate
        {
            get => this.learningRate;
            set
            {
                this.learningRate = value;
                this.OnChanged();
            }
        }

        /// <summary>
        /// Gets or sets the momentum used by the network.
        /// </summary>
        public decimal Momentum
        {
            get => this.momentum;
            set
            {
                this.momentum = value;
                this.OnChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number of nodes in each hidden layer.
        /// </summary>
        public int NodesInHiddenLayer
        {
            get => this.nodesInHiddenLayer;
            set
            {
                this.nodesInHiddenLayer = value;
                this.OnChanged();
            }
        }

        private void OnChanged([CallerMemberName] string? name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
