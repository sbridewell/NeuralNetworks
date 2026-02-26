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
        private bool chartErrorSeriesInitialised;
        private int trainingDataLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            this.Network = new NeuralNetwork();
            this.Network.NumberOfIterations = 100;
            this.Network.Momentum = 0.9;
            this.Network.InputSize = 1;
            this.Network.HiddenSize = 1;
            this.Network.OutputSize = 1;
            this.networkVisualiser1.Network = this.Network;
            this.networkVisualiser1.Invalidate();
            this.statusStripTimer.Tick += this.StatusStripTimer_Tick;
            this.visualiserTimer.Tick += (s, e) => this.networkVisualiser1.Invalidate();
            this.errorsTimer.Tick += this.ErrorsTimer_Tick;
            this.progressBarTimer.Tick += this.ProgressBarTimer_Tick;

            this.testResultsGrid1.Network = this.Network;

            this.TrainingDataPropertiesForm = new TrainingDataPropertiesForm(this.trainingDataViewModel);
            var initialProvider = this.trainingDataViewModel.DataProvider;
            if (initialProvider != null)
            {
                initialProvider.InputsLowerBound = -50;
                initialProvider.InputsUpperBound = 50;
                initialProvider.InputsIncrement = 0.5;
                initialProvider.PercentageOfTestData = 1;
            }

            this.NetworkPropertiesForm = new NetworkPropertiesForm
            {
                ViewModel = new NetworkPropertiesViewModel
                {
                    LearningRate = (decimal)this.Network.LearningRate,
                    Momentum = (decimal)this.Network.Momentum,
                    NumberOfIterations = this.Network.NumberOfIterations,
                    NodesInHiddenLayer = this.Network.HiddenSize,
                },
            };

            // Keep the runtime view-model and network in sync: when the properties form's
            // view-model changes, propagate the HiddenSize to the active network and UI.
            this.NetworkPropertiesForm.ViewModel.PropertyChanged += this.NetworkPropertiesForm_ViewModel_PropertyChanged;

            this.ActivationFunctionProviderForm = new ActivationFunctionProviderForm();

            this.ActivationFunctionProviderForm.Show();
            this.TrainingDataPropertiesForm.Show();
            this.NetworkPropertiesForm.Show();
        }

        private INeuralNetwork Network { get; }

        private TrainingDataPropertiesForm TrainingDataPropertiesForm { get; }

        private NetworkPropertiesForm NetworkPropertiesForm { get; }

        private ActivationFunctionProviderForm ActivationFunctionProviderForm { get; }

        #region button click event handlers

        /// <summary>
        /// Starts the training process.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private async void ButtonGo_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBoxJson.Text = string.Empty;
                this.DisableUserInput();

                this.testResultsGrid1.Rows.Clear();
                this.testResultsGrid1.Columns.Clear();

                this.ResetErrorChart();
                this.ClearDataGridViews();

                this.chartErrorSeriesInitialised = false;
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
                this.textBoxJson.Text = JsonSerializer.Serialize(this.Network, new JsonSerializerOptions { WriteIndented = true });

                var testData = dataProvider.TestData;
                var (testInputs, expected) = dataProvider.SplitIntoInputsAndOutputs(testData);
                this.testResultsGrid1.Populate(testInputs, expected);

                this.networkVisualiser1.Invalidate();

                this.EnableUserInput();
                this.ShowInStatusBar("Ready");
                this.InitialiseDataGridViews();
            }
            catch (Exception ex)
            {
                var errorForm = new ErrorForm { Exception = ex };
                _ = await errorForm.ShowDialogAsync();
                this.EnableUserInput();
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

        /// <summary>
        /// Used to predict outputs from the network for user-entered inputs.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void ButtonPredict_Click(object sender, EventArgs e)
        {
            var inputs = this.dataGridViewInputs.Rows[0].Cells.Cast<DataGridViewCell>().Select(c => double.TryParse(c.Value?.ToString(), out var v) ? v : 0).ToArray();
            var outputs = this.Network!.Predict(inputs);
            for (var i = 0; i < outputs.Length; i++)
            {
                this.dataGridViewOutputs.Rows[0].Cells[i].Value = $"{outputs[i]:F4}";
            }
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
            if (this.networkVisualiser1.Network != null)
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
            var net = this.networkVisualiser1?.Network;
            if (net == null || this.errorChart == null)
            {
                return;
            }

            // Initialise series the first time we have a valid network instance.
            if (!this.chartErrorSeriesInitialised)
            {
                this.errorChart.Series.Clear();

                if (this.errorChart.ChartAreas.Count == 0)
                {
                    return;
                }

                var area = this.errorChart.ChartAreas[0];
                area.AxisX.Minimum = 0;
                area.AxisX.Title = "Epoch";
                area.AxisY.Title = "Error";

                // Clear any existing legends and add two dedicated legends:
                // - one for the hidden-layer mean squared error
                // - one for the output-layer mean squared error
                this.errorChart.Legends.Clear();

                var hiddenLegend = new Legend("HiddenLegend")
                {
                    // leave Docking unset so Position takes full control
                    IsDockedInsideChartArea = false,
                };

                var outputLegend = new Legend("OutputLegend")
                {
                    // leave Docking unset so Position takes full control
                    IsDockedInsideChartArea = false,
                };

                // Place the two legends side-by-side above the chart area using absolute positions
                // Positions are percentages: (x, y, width, height).
                // Slight vertical offset (y) keeps them above the plotting area.
                hiddenLegend.Font = new Font(this.errorChart.Font.FontFamily, 14);
                hiddenLegend.Position = new ElementPosition(5f, 0f, 45f, 8f);  // left legend
                outputLegend.Font = new Font(this.errorChart.Font.FontFamily, 14);
                outputLegend.Position = new ElementPosition(50f, 0f, 45f, 8f); // right legend

                this.errorChart.Legends.Add(hiddenLegend);
                this.errorChart.Legends.Add(outputLegend);

                // Series that plot the aggregated mean-squared-errors.
                var seriesHiddenErrors = new Series("Hidden errors")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Legend = hiddenLegend.Name,
                    LegendText = "Hidden layer MSE",
                };
                this.errorChart.Series.Add(seriesHiddenErrors);

                var seriesOutputErrors = new Series("Output errors")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Legend = outputLegend.Name,
                    LegendText = "Output layer MSE",
                };
                this.errorChart.Series.Add(seriesOutputErrors);

                this.chartErrorSeriesInitialised = true;
            }

            // Append current errors if available.
            try
            {
                var epochX = net.CurrentIteration;
                var seriesName1 = "Output errors";
                if (this.errorChart.Series.IndexOf(seriesName1) >= 0)
                {
                    this.errorChart.Series[seriesName1].Points.AddXY(epochX, net.OutputLayerMeanSquaredError);
                }

                var seriesName2 = "Hidden errors";
                if (this.errorChart.Series.IndexOf(seriesName2) >= 0)
                {
                    this.errorChart.Series[seriesName2].Points.AddXY(epochX, net.HiddenLayerMeanSquaredError);
                }

                // Keep X axis range sensible while training.
                if (this.errorChart.ChartAreas.Count > 0)
                {
                    var area = this.errorChart.ChartAreas[0];
                    if (net.NumberOfIterations > 0)
                    {
                        // Keep the axis progressive during training.
                        area.AxisX.Maximum = net.CurrentIteration;
                    }

                    this.errorChart.Invalidate();
                }
            }
            catch
            {
                // Defensive: ignore transient read errors while the network is being updated on another thread.
            }
        }

        /// <summary>
        /// Displays some progress information on the status bar.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void StatusStripTimer_Tick(object? sender, EventArgs e)
        {
            if (this.networkVisualiser1.Network != null)
            {
                var net = this.networkVisualiser1.Network;
                if (net.NumberOfIterations == 0)
                {
                    return;
                }

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
                        this.networkVisualiser1.Invalidate();
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

            // TODO: disable prediction control once refactored
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

            // TODO: enable prediction control once refactored
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

            if (this.networkVisualisationToolStripMenuItem.Checked)
            {
                this.visualiserTimer.Start();
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

        #region clear / initialise data grid views

        private void ClearDataGridViews()
        {
            this.dataGridViewInputs.Rows.Clear();
            this.dataGridViewInputs.Columns.Clear();
            this.dataGridViewOutputs.Rows.Clear();
            this.dataGridViewOutputs.Columns.Clear();
        }

        private void InitialiseDataGridViews()
        {
            var inputRow = new DataGridViewRow();
            for (var i = 0; i < this.Network.InputSize; i++)
            {
                this.dataGridViewInputs.Columns.Add(string.Empty, string.Empty);
                inputRow.Cells.Add(new DataGridViewTextBoxCell());
            }

            var outputRow = new DataGridViewRow();
            for (var i = 0; i < this.Network.OutputSize; i++)
            {
                this.dataGridViewOutputs.Columns.Add(string.Empty, string.Empty);
                outputRow.Cells.Add(new DataGridViewTextBoxCell());
            }

            this.dataGridViewInputs.Rows.Add(inputRow);
            this.dataGridViewOutputs.Rows.Add(outputRow);
        }

        #endregion

        private void ResetErrorChart()
        {
            if (this.errorChart != null)
            {
                this.errorChart.Series.Clear();
                if (this.errorChart.ChartAreas.Count > 0)
                {
                    var area = this.errorChart.ChartAreas[0];
                    area.AxisX.Minimum = 0;
                    area.AxisX.Maximum = this.Network.NumberOfIterations;
                    area.AxisX.Title = "Epoch";
                    area.AxisY.Title = "Error";
                    area.AxisX.TitleFont = new Font(this.errorChart.Font.FontFamily, 14);
                    area.AxisY.TitleFont = new Font(this.errorChart.Font.FontFamily, 14);
                    area.AxisY.IsStartedFromZero = false;
                }
            }
        }

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
            if (e?.PropertyName != nameof(NetworkPropertiesViewModel.NodesInHiddenLayer))
            {
                return;
            }

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

                // Refresh visualiser to reflect the new architecture.
                this.networkVisualiser1.Invalidate();
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
    }
}
