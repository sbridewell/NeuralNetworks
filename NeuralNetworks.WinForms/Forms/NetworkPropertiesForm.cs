// <copyright file="NetworkPropertiesForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sde.NeuralNetworks.WinForms.ViewModels;

    /// <summary>
    /// Form for editing the properties of a neural network.
    /// </summary>
    public partial class NetworkPropertiesForm : Form
    {
        private BindingSource formBindingSource;
        private NetworkPropertiesViewModel viewModel = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkPropertiesForm"/> class
        /// with a supplied view-model.
        /// </summary>
        /// <param name="viewModel">The view-model to bind to.</param>
        public NetworkPropertiesForm(NetworkPropertiesViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            this.InitializeComponent();
            this.viewModel = viewModel;
            this.formBindingSource = new BindingSource { DataSource = this.viewModel };
            this.InitializeBindings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkPropertiesForm"/> class.
        /// </summary>
        public NetworkPropertiesForm()
        {
            this.InitializeComponent();
            this.viewModel = new NetworkPropertiesViewModel();
            this.formBindingSource = new BindingSource { DataSource = this.viewModel };
            this.InitializeBindings();
        }

        /// <summary>
        /// Gets or sets the view-model used by the form.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NetworkPropertiesViewModel ViewModel
        {
            get => this.viewModel;
            set
            {
                this.viewModel = value ?? throw new ArgumentNullException(nameof(value));

                if (this.formBindingSource == null)
                {
                    this.formBindingSource = new BindingSource { DataSource = this.viewModel };
                }
                else
                {
                    this.formBindingSource.DataSource = this.viewModel;
                }
            }
        }

        /// <summary>
        /// Disables the controls which allow user input.
        /// </summary>
        public void DisableUserInput()
        {
            this.numericUpDownNumberOfIterations.Enabled = false;
            this.numericUpDownLearningRate.Enabled = false;
            this.numericUpDownMomentum.Enabled = false;
            this.numericUpDownNodesInHiddenLayer.Enabled = false;
        }

        /// <summary>
        /// Enables the controls which allow user input.
        /// </summary>
        public void EnableUserInput()
        {
            this.numericUpDownNumberOfIterations.Enabled = true;
            this.numericUpDownLearningRate.Enabled = true;
            this.numericUpDownMomentum.Enabled = true;
            this.numericUpDownNodesInHiddenLayer.Enabled = true;
        }

        private void InitializeBindings()
        {
            this.BindNumericUpDown(this.numericUpDownNumberOfIterations, nameof(this.viewModel.NumberOfIterations));
            this.BindNumericUpDown(this.numericUpDownLearningRate, nameof(this.viewModel.LearningRate));
            this.BindNumericUpDown(this.numericUpDownMomentum, nameof(this.viewModel.Momentum));
            this.BindNumericUpDown(this.numericUpDownNodesInHiddenLayer, nameof(this.viewModel.NodesInHiddenLayer));

            this.formBindingSource?.ResetBindings(false);
        }

        private void BindNumericUpDown(NumericUpDown control, string viewModelPropertyName)
        {
            control.DataBindings.Add(
                nameof(control.Value),
                this.formBindingSource,
                viewModelPropertyName,
                true,
                DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
