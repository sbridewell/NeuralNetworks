// <copyright file="TrainingDataPropertiesForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Sde.NeuralNetworks.Quadratics;
    using Sde.NeuralNetworks.WinForms.ViewModels;

    /// <summary>
    /// Form for setting the properties of the training data provider.
    /// </summary>
    public partial class TrainingDataPropertiesForm : Form
    {
        [SuppressMessage(
            "StyleCop.CSharp.SpacingRules",
            "SA1000:Keywords should be spaced correctly",
            Justification = "Feels like I'm fighting the IDE")]
        private TrainingDataViewModel viewModel = new();
        [SuppressMessage(
            "StyleCop.CSharp.SpacingRules",
            "SA1000:Keywords should be spaced correctly",
            Justification = "Feels like I'm fighting the IDE")]
        private List<IDataProvider> dataProviders = new();
        [SuppressMessage(
            "StyleCop.CSharp.SpacingRules",
            "SA1000:Keywords should be spaced correctly",
            Justification = "Feels like I'm fighting the IDE")]
        private List<ClassListItem<IDataProvider>> dataProviderListItems = new();
        private BindingSource formBindingSource;
        private BindingSource providerBindingSource;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDataPropertiesForm"/> class
        /// with a supplied view-model.
        /// </summary>
        /// <param name="viewModel">The view-model to bind to.</param>
        public TrainingDataPropertiesForm(TrainingDataViewModel viewModel)
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
        /// Initializes a new instance of the <see cref="TrainingDataPropertiesForm"/> class.
        /// </summary>
        public TrainingDataPropertiesForm()
        {
            this.InitializeComponent();
            this.viewModel = new TrainingDataViewModel();
            this.formBindingSource = new BindingSource { DataSource = this.viewModel };
            this.InitializeBindings();
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Gets or sets the view-model used by the form.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TrainingDataViewModel ViewModel
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
        /// Gets the available <see cref="IDataProvider"/> implementations.
        /// </summary>
        public List<IDataProvider> DataProviders
        {
            get
            {
                if (this.dataProviders == null || this.dataProviders.Count == 0)
                {
                    var assembly = typeof(IDataProvider).Assembly;
                    this.dataProviders = assembly.GetTypes()
                        .Where(t => typeof(IDataProvider).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                        .Select(t => (IDataProvider)Activator.CreateInstance(t) !)
                        .ToList();
                }

                return this.dataProviders;
            }
        }

        /// <summary>
        /// Gets a list of list items to use as the data source for the data provider combo box.
        /// </summary>
        public List<ClassListItem<IDataProvider>> DataProviderListItems
        {
            get
            {
                if (this.dataProviderListItems == null || this.dataProviderListItems.Count == 0)
                {
                    this.dataProviderListItems = this.DataProviders
                        .Select(p => new ClassListItem<IDataProvider>(p)).ToList();
                }

                return this.dataProviderListItems;
            }
        }

        /// <summary>
        /// Disables the controls which allow user input.
        /// </summary>
        public void DisableUserInput()
        {
            this.numericUpDownLowerBound.Enabled = false;
            this.numericUpDownUpperBound.Enabled = false;
            this.numericUpDownIncrement.Enabled = false;
            this.numericUpDownPercentageOfTrainingData.Enabled = false;
            this.comboBoxDataProvider.Enabled = false;
        }

        /// <summary>
        /// Enables the controls which allow user input.
        /// </summary>
        public void EnableUserInput()
        {
            this.numericUpDownLowerBound.Enabled = true;
            this.numericUpDownUpperBound.Enabled = true;
            this.numericUpDownIncrement.Enabled = true;
            this.numericUpDownPercentageOfTrainingData.Enabled = true;
            this.comboBoxDataProvider.Enabled = true;
        }

        private void InitializeBindings()
        {
            if (this.DataProviders.Count == 0)
            {
                throw new InvalidOperationException("No IDataProvider implementations found.");
            }

            this.ViewModel.DataProvider = this.DataProviders[0];

            // Bind controls to the DataProvider instance via a nested BindingSource.
            // providerBindingSource watches the view model's DataProvider property.
            this.providerBindingSource = new BindingSource
            {
                DataSource = this.formBindingSource,
                DataMember = nameof(TrainingDataViewModel.DataProvider),
            };

            this.BindNumericUpDown(this.numericUpDownLowerBound, nameof(IDataProvider.InputsLowerBound));
            this.BindNumericUpDown(this.numericUpDownUpperBound, nameof(IDataProvider.InputsUpperBound));
            this.BindNumericUpDown(this.numericUpDownIncrement, nameof(IDataProvider.InputsIncrement));
            this.BindNumericUpDown(this.numericUpDownPercentageOfTrainingData, nameof(IDataProvider.PercentageOfTestData));

            this.comboBoxDataProvider.DataSource = this.DataProviderListItems;
            this.comboBoxDataProvider.DisplayMember = nameof(ClassListItem<IDataProvider>.TypeName);
            this.comboBoxDataProvider.ValueMember = nameof(ClassListItem<IDataProvider>.instance);

            this.comboBoxDataProvider.DataBindings.Add(
                "SelectedValue",
                this.formBindingSource,
                nameof(TrainingDataViewModel.DataProvider),
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            // Ensure a selection and force binding propagation to the view-model.
            if (this.comboBoxDataProvider.Items.Count > 0)
            {
                this.comboBoxDataProvider.SelectedIndex = 0;

                // Force the BindingSource to push values into the view-model now.
                this.formBindingSource.ResetBindings(false);
                this.providerBindingSource?.ResetBindings(false);
            }
        }

        private void BindNumericUpDown(NumericUpDown control, string viewModelPropertyName)
        {
            control.DataBindings.Add(
                nameof(control.Value),
                this.providerBindingSource,
                viewModelPropertyName,
                true,
                DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
