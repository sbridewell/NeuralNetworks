// <copyright file="ActivationFunctionProviderControl.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System.Data;
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// User control to select an activation function provider and display its activation and gradient functions.
    /// </summary>
    public partial class ActivationFunctionProviderControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationFunctionProviderControl"/> class.
        /// </summary>
        public ActivationFunctionProviderControl()
        {
            this.InitializeComponent();
            var assembly = typeof(IActivationFunctionProvider).Assembly;
            foreach (var t in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IActivationFunctionProvider).IsAssignableFrom(t)))
            {
                this.comboBox1.Items.Add(new ActivationListItem((IActivationFunctionProvider)Activator.CreateInstance(t) !));
                this.comboBox1.DisplayMember = nameof(ActivationListItem.TypeName);
            }

            this.comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the selected activation function provider.
        /// </summary>
        public IActivationFunctionProvider? SelectedActivationFunctionProvider
        {
            get
            {
                if (this.comboBox1.SelectedItem is ActivationListItem item)
                {
                    return item.provider;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets the title of the control.
        /// </summary>
        /// <param name="title">The title to set.</param>
        public void SetTitle(string title)
        {
            this.label1.Text = title;
        }

        /// <summary>
        /// Disables user input by disabling the combo box, preventing the user from changing the selected provider.
        /// </summary>
        public void DisableUserInput()
        {
            this.comboBox1.Enabled = false;
        }

        /// <summary>
        /// Enables user input by enabling the combo box.
        /// </summary>
        public void EnableUserInput()
        {
            this.comboBox1.Enabled = true;
        }

        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            var activationProvider = ((ActivationListItem)this.comboBox1.SelectedItem!).provider;
            this.activationFunctionControl1.SetActivationProvider(activationProvider);
        }

        /// <summary>
        /// Small wrapper used to expose a TypeName property for ListBox DisplayMember while retaining the provider instance.
        /// </summary>
        private sealed record ActivationListItem(IActivationFunctionProvider provider)
        {
            /// <summary>
            /// Gets the short type name (no namespace) of the provider.
            /// </summary>
            public string TypeName => this.provider.GetType().Name;

            /// <inheritdoc />
            public override string ToString() => this.TypeName;
        }
    }
}
