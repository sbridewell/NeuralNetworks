// <copyright file="TrainingDataViewModel.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// View model for the training data properties form.
    /// </summary>
    public class TrainingDataViewModel : INotifyPropertyChanged
    {
        private IDataProvider? dataProvider;

        /// <summary>
        /// The event which is raised when the value of a property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the data provider to be edited in the form.
        /// </summary>
        public IDataProvider? DataProvider
        {
            get => this.dataProvider;
            set
            {
                this.dataProvider = value;
                this.OnChanged();
            }
        }

        private void OnChanged([CallerMemberName] string? name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
