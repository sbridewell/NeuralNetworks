// <copyright file="Form1.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Text.Json;
    using System.Windows.Forms;
    using Sde.NeuralNetworks.Quadratics;
    using Sde.NeuralNetworks.WinForms.Forms;
    using Sde.NeuralNetworks.WinForms.ViewModels;

    /// <summary>
    /// Main form in the application.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly TrainingDataViewModel trainingDataViewModel = new TrainingDataViewModel();
        private readonly INeuralNetwork network;
        private readonly TrainingDataPropertiesForm trainingDataPropertiesForm;
        private readonly NetworkPropertiesForm networkPropertiesForm;
        private readonly ActivationFunctionProviderForm activationFunctionProviderForm;
        private readonly VisualisationForm visualisationForm;

#pragma warning disable SA1000 // Keywords should be spaced correctly
        private readonly Timer statusStripTimer = new() { Interval = 100 };
        private readonly Timer visualiserTimer = new() { Interval = 100 };
        private readonly Timer errorsTimer = new() { Interval = 100 };
        private readonly Timer progressBarTimer = new() { Interval = 100 };
        private readonly Stopwatch trainingStopwatch = new();
#pragma warning restore SA1000 // Keywords should be spaced correctly
        private int trainingDataLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            this.Invalidate();

            this.network = new NeuralNetwork();

            this.networkPropertiesForm = new NetworkPropertiesForm();
            this.activationFunctionProviderForm = new ActivationFunctionProviderForm();
            this.visualisationForm = new VisualisationForm { Network = this.network };

            this.network.NumberOfIterations = 100;
            this.network.Momentum = 0.9;
            this.network.InputSize = 1;
            this.network.HiddenSize = 1;
            this.network.OutputSize = 1;

            // TODO: consolidate into a single timer
            this.statusStripTimer.Tick += this.StatusStripTimer_Tick;
            this.errorsTimer.Tick += this.ErrorsTimer_Tick;
            this.progressBarTimer.Tick += this.ProgressBarTimer_Tick;

            // React to the training data provider selection being changed so that
            // UI controls that depend on input / output counts remain in sync.
            this.trainingDataViewModel.PropertyChanged += this.TrainingDataViewModel_PropertyChanged;

            this.trainingDataPropertiesForm = new TrainingDataPropertiesForm(this.trainingDataViewModel);
            var initialProvider = this.trainingDataViewModel.DataProvider;
            if (initialProvider != null)
            {
                initialProvider.InputsLowerBound = -50;
                initialProvider.InputsUpperBound = 50;
                initialProvider.InputsIncrement = 0.5;
                initialProvider.PercentageOfTestData = 1;
            }

            this.trainingDataViewModel.PropertyChanged += this.TrainingDataViewModel_PropertyChanged;
            this.ApplyProviderToNetworkAndUI();

            this.networkPropertiesForm.ViewModel = new NetworkPropertiesViewModel
            {
                LearningRate = (decimal)this.network.LearningRate,
                Momentum = (decimal)this.network.Momentum,
                NumberOfIterations = this.network.NumberOfIterations,
                NodesInHiddenLayer = this.network.HiddenSize,
            };

            // Keep the runtime view-model and network in sync: when the properties form's
            // view-model changes, propagate the HiddenSize to the active network and UI.
            this.networkPropertiesForm.ViewModel.PropertyChanged += this.NetworkPropertiesForm_ViewModel_PropertyChanged;

            this.Shown += (s, e) => this.PositionForms();

            this.visualiserTimer.Tick += (s, e) => this.visualisationForm.Invalidate();
            this.visualiserTimer.Start();
        }

        private void PositionForms()
        {
            var screenSize = Screen.FromControl(this).WorkingArea;
            var screenWidth = screenSize.Width;
            var screenHeight = screenSize.Height;

            this.Location = new Point(0, 0);
            this.Width = screenWidth;

            this.trainingDataPropertiesForm.Show();
            this.trainingDataPropertiesForm.Location = new Point(0, this.Location.Y + this.Height);

            this.networkPropertiesForm.Show();
            this.networkPropertiesForm.Location = new Point(
                0,
                this.Location.Y + this.Height + this.trainingDataPropertiesForm.Height);

            this.visualisationForm.Show();
            this.visualisationForm.Location = new Point(
                this.trainingDataPropertiesForm.Location.X + this.trainingDataPropertiesForm.Width,
                this.Location.Y + this.Height);
            this.visualisationForm.Width = screenWidth - this.visualisationForm.Location.X;
            this.visualisationForm.Height = screenHeight - this.visualisationForm.Location.Y;

            this.activationFunctionProviderForm.Show();
            this.activationFunctionProviderForm.Width = Math.Max(this.trainingDataPropertiesForm.Width, this.networkPropertiesForm.Width);
            this.activationFunctionProviderForm.Height
                = screenHeight
                - this.Location.Y
                - this.Height
                - this.trainingDataPropertiesForm.Height
                - this.networkPropertiesForm.Height;
            this.activationFunctionProviderForm.Location = new Point(
                    0,
                    this.Location.Y + this.Height + this.trainingDataPropertiesForm.Height + this.networkPropertiesForm.Height);
        }

        #region button click event handlers

        private void TrainingDataViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName != nameof(TrainingDataViewModel.DataProvider))
            {
                return;
            }

            if (this.InvokeRequired)
            {
                _ = this.BeginInvoke((Action)this.ApplyProviderToNetworkAndUI);
            }
            else
            {
                this.ApplyProviderToNetworkAndUI();
            }
        }

        /// <summary>
        /// Starts the training process.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private async void ButtonGo_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableUserInput();
                this.visualisationForm.ClearTestResults();
                this.visualisationForm.ResetErrorsChart();

                var viewModel = this.networkPropertiesForm.ViewModel;
                this.network.NumberOfIterations = viewModel.NumberOfIterations;
                this.network.LearningRate = (double)viewModel.LearningRate;
                this.network.Momentum = (double)viewModel.Momentum;
                this.network.HiddenSize = viewModel.NodesInHiddenLayer;
                this.network.HiddenActivationFunctionProvider = this.activationFunctionProviderForm.HiddenLayerProvider;
                this.network.OutputActivationFunctionProvider = this.activationFunctionProviderForm.OutputLayerProvider;

                this.ShowInStatusBar("Creating training data...");

                var dataProvider = this.trainingDataViewModel.DataProvider!;
                dataProvider.GenerateData();
                var trainingData = dataProvider.TrainingData;
                this.trainingDataLength = trainingData.Length;
                var (inputs, targets) = dataProvider.SplitIntoInputsAndOutputs(trainingData);
                this.network.InputSize = inputs[0].Length;
                this.network.OutputSize = targets[0].Length;

                this.StartTimers();

                /////////////////////////////////////////////////////////////////////////////////////
                // Train the network! ///////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////
                await this.network!.Train(inputs, targets);

                // Training finished. Stop polling.
                this.StopTimers();
                this.ShowInStatusBar("Training complete. Testing network.");
                this.visualisationForm.DisplayNetworkJson();

                var testData = dataProvider.TestData;
                var (testInputs, expected) = dataProvider.SplitIntoInputsAndOutputs(testData);
                this.visualisationForm.DisplayTestResults(testInputs, expected);

                this.EnableUserInput();
                this.ShowInStatusBar("Ready");
            }
            catch (Exception ex)
            {
                var errorForm = new ErrorForm { Exception = ex };
                _ = await errorForm.ShowDialogAsync();
                this.EnableUserInput();
                throw;
            }
        }

        /// <summary>
        /// Can be used to stop the training process early.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void ButtonStopClick(object sender, EventArgs e)
        {
            this.network!.Stop();
            this.EnableUserInput();
        }

        #endregion

        #region timer event handlers

        /// <summary>
        /// Advances the progress bar to show how close to completion the training is.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void ProgressBarTimer_Tick(object? sender, EventArgs e)
        {
            if (this.visualisationForm.Network != null)
            {
                this.progressBar1.Maximum = this.network.NumberOfIterations;
                this.progressBar1.Value = this.network.CurrentIteration;

                // debug only - just to check the weights and biases are being updated during training
                // FIXME: network weights and biases aren't being updated during training
                this.visualisationForm.DisplayNetworkJson();
            }
        }

        /// <summary>
        /// Polls the network during training and appends the hidden/output errors to the errors chart.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void ErrorsTimer_Tick(object? sender, EventArgs e)
        {
            this.visualisationForm.UpdateErrorsChart();
        }

        /// <summary>
        /// Displays some progress information on the status bar.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void StatusStripTimer_Tick(object? sender, EventArgs e)
        {
            if (this.visualisationForm.Network != null)
            {
                var net = this.visualisationForm.Network;
                if (net.NumberOfIterations == 0)
                {
                    return;
                }

                // TODO: write all this to a text box in Form1
                this.network.TimeSpentTraining = this.trainingStopwatch.Elapsed;
                var progress = (double)net.CurrentIteration / net.NumberOfIterations;
                var estimatedTotalTime = TimeSpan.FromTicks((long)(this.trainingStopwatch.Elapsed.Ticks / progress));
                this.network.EstimatedTrainingTimeLeft = estimatedTotalTime - this.trainingStopwatch.Elapsed;
                var message = $"Training with {this.trainingDataLength} items. "
                    + $"Epoch: {net.CurrentIteration}/{net.NumberOfIterations}. "
                    + $"Time spent training: {net.TimeSpentTraining:hh\\:mm\\:ss}. "
                    + $"Estimated training time remaining: {net.EstimatedTrainingTimeLeft:hh\\:mm\\:ss}. "
                    + $"Hidden MSE: {net.HiddenLayerMeanSquaredError:F4}. "
                    + $"Output MSE: {net.OutputLayerMeanSquaredError:F4}. ";
                this.ShowInStatusBar(message);
            }
        }

        #endregion

        #region menu item click event handlers

        private void SaveCurrentModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var path = this.saveFileDialog1.FileName;
                try
                {
                    var json = JsonSerializer.Serialize(this.network, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, json);
                    MessageBox.Show($"Model saved to {path}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving model: {ex.Message}");
                }
            }
        }

        private void OpenTrainedModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var path = this.openFileDialog1.FileName;
                try
                {
                    var json = File.ReadAllText(path);
                    var loadedNetwork = JsonSerializer.Deserialize<NeuralNetworkSingleHiddenLayer>(json);
                    if (loadedNetwork != null)
                    {
                        this.network.InputSize = loadedNetwork.InputSize;
                        this.network.HiddenSize = loadedNetwork.HiddenSize;
                        this.network.OutputSize = loadedNetwork.OutputSize;
                        this.network.LearningRate = loadedNetwork.LearningRate;
                        this.network.HiddenActivationFunctionProvider = loadedNetwork.HiddenActivationFunctionProvider;
                        this.network.OutputActivationFunctionProvider = loadedNetwork.OutputActivationFunctionProvider;
                        Array.Copy(loadedNetwork.InputToHiddenWeights, this.network.InputToHiddenWeights, loadedNetwork.InputToHiddenWeights.Length);
                        Array.Copy(loadedNetwork.HiddenBiases, this.network.HiddenBiases, loadedNetwork.HiddenBiases.Length);
                        Array.Copy(loadedNetwork.HiddenToOutputWeights, this.network.HiddenToOutputWeights, loadedNetwork.HiddenToOutputWeights.Length);
                        Array.Copy(loadedNetwork.OutputBiases, this.network.OutputBiases, loadedNetwork.OutputBiases.Length);
                        MessageBox.Show($"Model loaded from {path}");
                    }
                    else
                    {
                        MessageBox.Show($"Error loading model: deserialized object is null");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading model: {ex.Message}");
                }
            }
        }

        #endregion

        #region enable / disable user input

        /// <summary>
        /// Disables the controls which allow user input, except for the stop button,
        /// because it only makes sense to allow the user to stop the training process
        /// once it's started.
        /// </summary>
        private void DisableUserInput()
        {
            this.buttonGo.Enabled = false;
            this.buttonStop.Enabled = true;
            this.fileToolStripMenuItem.Enabled = false;
            this.trainingDataPropertiesForm.DisableUserInput();
            this.networkPropertiesForm.DisableUserInput();
            this.activationFunctionProviderForm.DisableUserInput();
            this.visualisationForm.DisableUserInput();
        }

        /// <summary>
        /// Enables the controls which allow user input, except for the stop button,
        /// because it only makes sense to allow the user to stop the training process
        /// once it's started.
        /// </summary>
        private void EnableUserInput()
        {
            this.buttonGo.Enabled = true;
            this.buttonStop.Enabled = false;
            this.fileToolStripMenuItem.Enabled = true;
            this.trainingDataPropertiesForm.EnableUserInput();
            this.networkPropertiesForm.EnableUserInput();
            this.activationFunctionProviderForm.EnableUserInput();
            this.visualisationForm.EnableUserInput();
        }

        #endregion

        #region start / stop timers

        private void StartTimers()
        {
            this.progressBarTimer.Start();
            this.trainingStopwatch.Reset();
            this.trainingStopwatch.Start();
            this.errorsTimer.Start();
            this.statusStripTimer.Start();
        }

        private void StopTimers()
        {
            // TODO: wait a second before stopping timers to allow the final updates to be rendered in the UI?
            this.trainingStopwatch.Stop();
            this.errorsTimer.Stop();
            this.progressBarTimer.Stop();
            this.visualiserTimer.Stop();
            this.statusStripTimer.Stop();
        }

        #endregion

        private void ShowInStatusBar(string text)
        {
            if (this.InvokeRequired)
            {
                // Marshal the update to the UI thread without blocking the caller.
                _ = this.BeginInvoke((Action)(() => this.ShowInStatusBar(text)));
                return;
            }

            this.toolStripStatusLabel1!.Text = text;

            // Refresh only the status strip so the new text is visible immediately.
            this.statusStrip1?.Refresh();
        }

        /// <summary>
        /// Propagates changes from the NetworkPropertiesForm view-model into the running network.
        /// </summary>
        private void NetworkPropertiesForm_ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is not NetworkPropertiesViewModel vm)
            {
                return;
            }

            // Capture the new value.
            var newHiddenSize = vm.NodesInHiddenLayer;

            void ApplyChange()
            {
                // Update the network.
                this.network.HiddenSize = newHiddenSize;
            }

            if (this.InvokeRequired)
            {
                _ = this.BeginInvoke((Action)ApplyChange);
            }
            else
            {
                ApplyChange();
            }
        }

        private void ApplyProviderToNetworkAndUI()
        {
            var provider = this.trainingDataViewModel.DataProvider;
            if (provider == null)
            {
                return;
            }

            this.network.InputSize = provider.NumberOfInputs;
            this.network.OutputSize = provider.NumberOfOutputs;

            // Re-assign the network to the visualisation form to align its
            // text boxes with the new input / output sizes.
            this.visualisationForm.Network = this.network;
        }
    }
}
