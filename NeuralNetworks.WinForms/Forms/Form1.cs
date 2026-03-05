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
    using System.Windows.Forms.DataVisualization.Charting;
    using Sde.NeuralNetworks.Quadratics;
    using Sde.NeuralNetworks.WinForms.Forms;
    using Sde.NeuralNetworks.WinForms.ViewModels;

    /// <summary>
    /// Main form in the application.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly TrainingDataViewModel trainingDataViewModel = new TrainingDataViewModel();

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

            this.Network = new NeuralNetwork();

            this.NetworkPropertiesForm = new NetworkPropertiesForm();
            this.ActivationFunctionProviderForm = new ActivationFunctionProviderForm();
            this.VisualisationForm = new VisualisationForm { Network = this.Network };

            this.Network.NumberOfIterations = 100;
            this.Network.Momentum = 0.9;
            this.Network.InputSize = 1;
            this.Network.HiddenSize = 1;
            this.Network.OutputSize = 1;

            // TODO: consolidate into a single timer
            this.statusStripTimer.Tick += this.StatusStripTimer_Tick;
            this.errorsTimer.Tick += this.ErrorsTimer_Tick;
            this.progressBarTimer.Tick += this.ProgressBarTimer_Tick;

            // React to the training data provider selection being changed so that
            // UI controls that depend on input / output counts remain in sync.
            this.trainingDataViewModel.PropertyChanged += this.TrainingDataViewModel_PropertyChanged;

            this.TrainingDataPropertiesForm = new TrainingDataPropertiesForm(this.trainingDataViewModel);
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

            this.NetworkPropertiesForm.ViewModel = new NetworkPropertiesViewModel
            {
                LearningRate = (decimal)this.Network.LearningRate,
                Momentum = (decimal)this.Network.Momentum,
                NumberOfIterations = this.Network.NumberOfIterations,
                NodesInHiddenLayer = this.Network.HiddenSize,
            };

            // Keep the runtime view-model and network in sync: when the properties form's
            // view-model changes, propagate the HiddenSize to the active network and UI.
            this.NetworkPropertiesForm.ViewModel.PropertyChanged += this.NetworkPropertiesForm_ViewModel_PropertyChanged;

            this.Shown += (s, e) => this.PositionForms();

            this.visualiserTimer.Tick += (s, e) => this.VisualisationForm.Invalidate();
            this.visualiserTimer.Start();
        }

        // TODO: make these fields rather than properties?
        private INeuralNetwork Network { get; }

        private TrainingDataPropertiesForm TrainingDataPropertiesForm { get; }

        private NetworkPropertiesForm NetworkPropertiesForm { get; }

        private ActivationFunctionProviderForm ActivationFunctionProviderForm { get; }

        private VisualisationForm VisualisationForm { get; }

        private void PositionForms()
        {
            // TODO: PositionForms method?
            var screenSize = Screen.FromControl(this).WorkingArea;
            var screenWidth = screenSize.Width;
            var screenHeight = screenSize.Height;

            this.Location = new Point(0, 0);
            this.Width = screenWidth;

            this.TrainingDataPropertiesForm.Show();
            this.TrainingDataPropertiesForm.Location = new Point(0, this.Location.Y + this.Height);

            this.NetworkPropertiesForm.Show();
            this.NetworkPropertiesForm.Location = new Point(
                0,
                this.Location.Y + this.Height + this.TrainingDataPropertiesForm.Height);

            this.VisualisationForm.Show();
            this.VisualisationForm.Location = new Point(
                this.TrainingDataPropertiesForm.Location.X + this.TrainingDataPropertiesForm.Width,
                this.Location.Y + this.Height);
            this.VisualisationForm.Width = screenWidth - this.VisualisationForm.Location.X;
            this.VisualisationForm.Height = screenHeight - this.VisualisationForm.Location.Y;

            this.ActivationFunctionProviderForm.Show();
            this.ActivationFunctionProviderForm.Width = Math.Max(this.TrainingDataPropertiesForm.Width, this.NetworkPropertiesForm.Width);
            this.ActivationFunctionProviderForm.Height
                = screenHeight
                - this.Location.Y
                - this.Height
                - this.TrainingDataPropertiesForm.Height
                - this.NetworkPropertiesForm.Height;
            this.ActivationFunctionProviderForm.Location = new Point(
                    0,
                    this.Location.Y + this.Height + this.TrainingDataPropertiesForm.Height + this.NetworkPropertiesForm.Height);
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
                this.VisualisationForm.ClearTestResults();
                this.VisualisationForm.ResetErrorsChart();

                var viewModel = this.NetworkPropertiesForm.ViewModel;
                this.Network.NumberOfIterations = viewModel.NumberOfIterations;
                this.Network.LearningRate = (double)viewModel.LearningRate;
                this.Network.Momentum = (double)viewModel.Momentum;
                this.Network.HiddenSize = viewModel.NodesInHiddenLayer;
                this.Network.HiddenActivationFunctionProvider = this.ActivationFunctionProviderForm.HiddenLayerProvider;
                this.Network.OutputActivationFunctionProvider = this.ActivationFunctionProviderForm.OutputLayerProvider;

                this.ShowInStatusBar("Creating training data...");

                var dataProvider = this.trainingDataViewModel.DataProvider!;
                dataProvider.GenerateData();
                var trainingData = dataProvider.TrainingData;
                this.trainingDataLength = trainingData.Length;
                var (inputs, targets) = dataProvider.SplitIntoInputsAndOutputs(trainingData);
                this.Network.InputSize = inputs[0].Length;
                this.Network.OutputSize = targets[0].Length;

                this.StartTimers();

                /////////////////////////////////////////////////////////////////////////////////////
                // Train the network! ///////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////
                await this.Network!.Train(inputs, targets);

                // Training finished. Stop polling.
                this.StopTimers();
                this.ShowInStatusBar("Training complete. Testing network.");
                this.VisualisationForm.DisplayNetworkJson();

                var testData = dataProvider.TestData;
                var (testInputs, expected) = dataProvider.SplitIntoInputsAndOutputs(testData);
                this.VisualisationForm.DisplayTestResults(testInputs, expected);

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
            this.Network!.Stop();
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
            if (this.VisualisationForm.Network != null)
            {
                this.progressBar1.Maximum = this.Network.NumberOfIterations;
                this.progressBar1.Value = this.Network.CurrentIteration;
            }
        }

        /// <summary>
        /// Polls the network during training and appends the hidden/output errors to the errors chart.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void ErrorsTimer_Tick(object? sender, EventArgs e)
        {
            this.VisualisationForm.UpdateErrorsChart();
        }

        /// <summary>
        /// Displays some progress information on the status bar.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void StatusStripTimer_Tick(object? sender, EventArgs e)
        {
            if (this.VisualisationForm.Network != null)
            {
                var net = this.VisualisationForm.Network;
                if (net.NumberOfIterations == 0)
                {
                    return;
                }

                // TODO: write all this to a text box in Form1
                this.Network.TimeSpentTraining = this.trainingStopwatch.Elapsed;
                var progress = (double)net.CurrentIteration / net.NumberOfIterations;
                var estimatedTotalTime = TimeSpan.FromTicks((long)(this.trainingStopwatch.Elapsed.Ticks / progress));
                this.Network.EstimatedTrainingTimeLeft = estimatedTotalTime - this.trainingStopwatch.Elapsed;
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

        private void NetworkVisualisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.networkVisualisationToolStripMenuItem.Checked)
            {
                this.visualiserTimer.Start();
            }
            else
            {
                this.visualiserTimer.Stop();
            }
        }

        private void ErrorsChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.errorsChartToolStripMenuItem.Checked)
            {
                this.errorsTimer.Start();
            }
            else
            {
                this.errorsTimer.Stop();
            }
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.statusBarToolStripMenuItem.Checked)
            {
                this.statusStripTimer.Start();
            }
            else
            {
                this.statusStripTimer.Stop();
            }
        }

        private void SaveCurrentModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var path = this.saveFileDialog1.FileName;
                try
                {
                    var json = JsonSerializer.Serialize(this.Network, new JsonSerializerOptions { WriteIndented = true });
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
                        this.Network.InputSize = loadedNetwork.InputSize;
                        this.Network.HiddenSize = loadedNetwork.HiddenSize;
                        this.Network.OutputSize = loadedNetwork.OutputSize;
                        this.Network.LearningRate = loadedNetwork.LearningRate;
                        this.Network.HiddenActivationFunctionProvider = loadedNetwork.HiddenActivationFunctionProvider;
                        this.Network.OutputActivationFunctionProvider = loadedNetwork.OutputActivationFunctionProvider;
                        Array.Copy(loadedNetwork.InputToHiddenWeights, this.Network.InputToHiddenWeights, loadedNetwork.InputToHiddenWeights.Length);
                        Array.Copy(loadedNetwork.HiddenBiases, this.Network.HiddenBiases, loadedNetwork.HiddenBiases.Length);
                        Array.Copy(loadedNetwork.HiddenToOutputWeights, this.Network.HiddenToOutputWeights, loadedNetwork.HiddenToOutputWeights.Length);
                        Array.Copy(loadedNetwork.OutputBiases, this.Network.OutputBiases, loadedNetwork.OutputBiases.Length);
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
            this.enableDisableUIFeaturesToolStripMenuItem.Enabled = false;
            this.TrainingDataPropertiesForm.DisableUserInput();
            this.NetworkPropertiesForm.DisableUserInput();
            this.ActivationFunctionProviderForm.DisableUserInput();
            this.VisualisationForm.DisableUserInput();
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
            this.enableDisableUIFeaturesToolStripMenuItem.Enabled = true;
            this.TrainingDataPropertiesForm.EnableUserInput();
            this.NetworkPropertiesForm.EnableUserInput();
            this.ActivationFunctionProviderForm.EnableUserInput();
            this.VisualisationForm.EnableUserInput();
        }

        #endregion

        #region start / stop timers

        private void StartTimers()
        {
            this.progressBarTimer.Start();
            this.trainingStopwatch.Reset();
            this.trainingStopwatch.Start();
            if (this.errorsChartToolStripMenuItem.Checked)
            {
                this.errorsTimer.Start();
            }

            if (this.statusBarToolStripMenuItem.Checked)
            {
                this.statusStripTimer.Start();
            }
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
                this.Network.HiddenSize = newHiddenSize;
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

            this.Network.InputSize = provider.NumberOfInputs;
            this.Network.OutputSize = provider.NumberOfOutputs;

            // Re-assign the network to the visualisation form to align its
            // text boxes with the new input / output sizes.
            this.VisualisationForm.Network = this.Network;
        }
    }
}
