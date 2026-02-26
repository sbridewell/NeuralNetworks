// <copyright file="ActivationFunctionProviderForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Forms
{
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// Form which allows the user to select activation function providers.
    /// </summary>
    public partial class ActivationFunctionProviderForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationFunctionProviderForm"/> class.
        /// </summary>
        public ActivationFunctionProviderForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the activation function provider for the (first) hidden layer.
        /// </summary>
        public IActivationFunctionProvider HiddenLayerProvider => this.activationFunctionProviderControlHidden1.SelectedActivationFunctionProvider!;

        /// <summary>
        /// Gets the activation function provider for the output layer.
        /// </summary>
        public IActivationFunctionProvider OutputLayerProvider => this.activationFunctionProviderControlOutput.SelectedActivationFunctionProvider!;

        /// <summary>
        /// Disables user input.
        /// </summary>
        public void DisableUserInput()
        {
            this.activationFunctionProviderControlHidden1.DisableUserInput();
            this.activationFunctionProviderControlOutput.DisableUserInput();
        }

        /// <summary>
        /// Enables user input.
        /// </summary>
        public void EnableUserInput()
        {
            this.activationFunctionProviderControlHidden1.EnableUserInput();
            this.activationFunctionProviderControlOutput.EnableUserInput();
        }
    }
}
