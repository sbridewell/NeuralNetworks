// <copyright file="Form1.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    using System;
    using System.Diagnostics;
    using System.Text.Json;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using Sde.NeuralNetworks.Quadratics;

    /// <summary>
    /// Main form in the application.
    /// </summary>
    public partial class Form1 : Form
    {
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
            this.Network.Momentum = 0.9;
            this.Network.InputSize = 1;
            this.Network.HiddenSize = (int)this.numericUpDownNeuronsPerHiddenLayer.Value;
            this.Network.OutputSize = 1;
            this.networkVisualiser1.Network = this.Network;
            this.networkVisualiser1.Invalidate();
            this.statusStripTimer.Tick += this.StatusStripTimer_Tick;
            this.visualiserTimer.Tick += (s, e) => this.networkVisualiser1.Invalidate();
            this.errorsTimer.Tick += this.ErrorsTimer_Tick;
            this.progressBarTimer.Tick += this.ProgressBarTimer_Tick;

            this.testResultsGrid1.Network = this.Network;

            var assembly = typeof(IDataProvider).Assembly;
            var dataProviderTypes = assembly.GetTypes().Where(t => typeof(IDataProvider).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToArray();
            foreach (var type in dataProviderTypes)
            {
                this.comboBoxDataProvider.Items.Add(new DataProviderListItem((IDataProvider)Activator.CreateInstance(type) !));
                this.comboBoxDataProvider.DisplayMember = nameof(DataProviderListItem.TypeName);
            }

            this.comboBoxDataProvider.SelectedIndex = 0;
        }

        private INeuralNetwork Network { get; }

        #region button click event handlers

        /// <summary>
        /// Starts the training process.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private async void ButtonGo_Click(object sender, EventArgs e)
        {
            this.textBoxJson.Text = string.Empty;

            this.numericUpDownNumberOfIterations.Enabled = false;
            this.numericUpDownLearningRate.Enabled = false;
            this.numericUpDownNeuronsPerHiddenLayer.Enabled = false;
            this.buttonGo.Enabled = false;
            this.buttonStop.Enabled = true;

            this.testResultsGrid1.Rows.Clear();
            this.testResultsGrid1.Columns.Clear();

            // Reset chart state before training starts.
            if (this.chartErrors != null)
            {
                this.chartErrors.Series.Clear();
                if (this.chartErrors.ChartAreas.Count > 0)
                {
                    var area = this.chartErrors.ChartAreas[0];
                    area.AxisX.Minimum = 0;
                    area.AxisX.Maximum = (double)this.numericUpDownNumberOfIterations.Value;
                    area.AxisX.Title = "Epoch";
                    area.AxisY.Title = "Error";
                    area.AxisX.TitleFont = new Font(this.chartErrors.Font.FontFamily, 14);
                    area.AxisY.TitleFont = new Font(this.chartErrors.Font.FontFamily, 14);
                    area.AxisY.IsStartedFromZero = false;
                }
            }

            this.chartErrorSeriesInitialised = false;
            if (this.errorsChartToolStripMenuItem.Checked)
            {
                this.errorsTimer.Start();
            }

            if (this.networkVisualisationToolStripMenuItem.Checked)
            {
                this.visualiserTimer.Start();
            }

            this.progressBarTimer.Start();
            this.trainingStopwatch.Reset();
            this.trainingStopwatch.Start();

            this.Network.NumberOfIterations = (int)this.numericUpDownNumberOfIterations.Value;
            this.Network.LearningRate = (double)this.numericUpDownLearningRate.Value;
            this.Network.Momentum = (double)this.numericUpDownMomentum.Value;
            this.Network.HiddenSize = (int)this.numericUpDownNeuronsPerHiddenLayer.Value;
            this.Network.HiddenActivationFunctionProvider = this.activationFunctionProviderControlHidden1.SelectedActivationFunctionProvider!;
            this.Network.OutputActivationFunctionProvider = this.activationFunctionProviderControlOutput.SelectedActivationFunctionProvider!;

            this.toolStripStatusLabel1.Text = "Creating training data...";
            this.Invalidate();

            var dataProvider = ((DataProviderListItem)this.comboBoxDataProvider.SelectedItem!).provider;
            dataProvider.InputsLowerBound = (double)this.numericUpDownInputLowerBound.Value;
            dataProvider.InputsUpperBound = (double)this.numericUpDownInputUpperBound.Value;
            dataProvider.InputsIncrement = (double)this.numericUpDownInputsIncrement.Value;
            dataProvider.PercentageOfTestData = (double)this.numericUpDownPercentageTestData.Value;
            dataProvider.GenerateData();
            var trainingData = dataProvider.TrainingData;
            this.trainingDataLength = trainingData.Length;
            var (inputs, targets) = dataProvider.SplitIntoInputsAndOutputs(trainingData);
            this.Network.InputSize = inputs[0].Length;
            this.Network.OutputSize = targets[0].Length;

            if (this.statusBarToolStripMenuItem.Checked)
            {
                this.statusStripTimer.Start();
            }

            /////////////////////////////////////////////////////////////////////////////////////
            // Train the network! ///////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////
            await this.Network!.Train(inputs, targets);

            // Training finished. Stop polling.
            this.trainingStopwatch.Stop();
            this.errorsTimer.Stop();
            this.progressBarTimer.Stop();
            this.visualiserTimer.Stop();
            this.statusStripTimer.Stop();
            this.toolStripStatusLabel1.Text = "Training complete. Testing network.";
            this.Invalidate();
            this.textBoxJson.Text = JsonSerializer.Serialize(this.Network, new JsonSerializerOptions { WriteIndented = true });

            var testData = dataProvider.TestData;
            var (testInputs, expected) = dataProvider.SplitIntoInputsAndOutputs(testData);
            this.testResultsGrid1.Populate(testInputs, expected);

            this.networkVisualiser1.Invalidate();
            this.numericUpDownNumberOfIterations.Enabled = true;
            this.numericUpDownLearningRate.Enabled = true;
            this.numericUpDownNeuronsPerHiddenLayer.Enabled = true;
            this.buttonGo.Enabled = true;
            this.buttonStop.Enabled = false;
            this.toolStripStatusLabel1.Text = "Ready";
            this.Invalidate();

            this.dataGridViewInputs.Rows.Clear();
            this.dataGridViewInputs.Columns.Clear();
            this.dataGridViewOutputs.Rows.Clear();
            this.dataGridViewOutputs.Columns.Clear();
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

        /// <summary>
        /// Can be used to stop the training process early.
        /// </summary>
        /// <param name="sender">Object which raised the event.</param>
        /// <param name="e">Event arcuments.</param>
        private void ButtonStopClick(object sender, EventArgs e)
        {
            this.Network!.Stop();
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
            if (net == null || this.chartErrors == null)
            {
                return;
            }

            // Initialise series the first time we have a valid network instance.
            if (!this.chartErrorSeriesInitialised)
            {
                this.chartErrors.Series.Clear();

                if (this.chartErrors.ChartAreas.Count == 0)
                {
                    return;
                }

                var area = this.chartErrors.ChartAreas[0];
                area.AxisX.Minimum = 0;
                area.AxisX.Title = "Epoch";
                area.AxisY.Title = "Error";

                // Clear any existing legends and add two dedicated legends:
                // - one for the hidden-layer mean squared error
                // - one for the output-layer mean squared error
                this.chartErrors.Legends.Clear();

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
                hiddenLegend.Font = new Font(this.chartErrors.Font.FontFamily, 14);
                hiddenLegend.Position = new ElementPosition(5f, 0f, 45f, 8f);  // left legend
                outputLegend.Font = new Font(this.chartErrors.Font.FontFamily, 14);
                outputLegend.Position = new ElementPosition(50f, 0f, 45f, 8f); // right legend

                this.chartErrors.Legends.Add(hiddenLegend);
                this.chartErrors.Legends.Add(outputLegend);

                // Series that plot the aggregated mean-squared-errors.
                var seriesHiddenErrors = new Series("Hidden errors")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Legend = hiddenLegend.Name,
                    LegendText = "Hidden layer MSE",
                };
                this.chartErrors.Series.Add(seriesHiddenErrors);

                var seriesOutputErrors = new Series("Output errors")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Legend = outputLegend.Name,
                    LegendText = "Output layer MSE",
                };
                this.chartErrors.Series.Add(seriesOutputErrors);

                this.chartErrorSeriesInitialised = true;
            }

            // Append current errors if available.
            try
            {
                var epochX = net.CurrentIteration;
                var seriesName1 = "Output errors";
                if (this.chartErrors.Series.IndexOf(seriesName1) >= 0)
                {
                    this.chartErrors.Series[seriesName1].Points.AddXY(epochX, net.OutputLayerMeanSquaredError);
                }

                var seriesName2 = "Hidden errors";
                if (this.chartErrors.Series.IndexOf(seriesName2) >= 0)
                {
                    this.chartErrors.Series[seriesName2].Points.AddXY(epochX, net.HiddenLayerMeanSquaredError);
                }

                // Keep X axis range sensible while training.
                if (this.chartErrors.ChartAreas.Count > 0)
                {
                    var area = this.chartErrors.ChartAreas[0];
                    if (net.NumberOfIterations > 0)
                    {
                        // Keep the axis progressive during training.
                        area.AxisX.Maximum = net.CurrentIteration;
                    }

                    this.chartErrors.Invalidate();
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
                this.toolStripStatusLabel2.Text
                    = $"Training with {this.trainingDataLength} items"
                    + $". Epoch: {net.CurrentIteration}/{net.NumberOfIterations}"
                    + $". Time spent training: {net.TimeSpentTraining.ToString(@"hh\:mm\:ss")}"
                    + $". Estimated training time remaining: {net.EstimatedTrainingTimeLeft.ToString(@"hh\:mm\:ss")}"
                    + $". ";
                this.toolStripStatusLabel3.Text
                    = $"Hidden MSE: {net.HiddenLayerMeanSquaredError:F4}"
                    + $". ";
                this.toolStripStatusLabel4.Text
                    = $"Output MSE: {net.OutputLayerMeanSquaredError:F4}"
                    + $". ";
            }
        }

        #endregion

        private void NumericUpDownNumberOfHiddenLayer_ValueChanged(object sender, EventArgs e)
        {
            this.Network.HiddenSize = (int)this.numericUpDownNeuronsPerHiddenLayer.Value;
            this.networkVisualiser1.Invalidate();
        }

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

        #region menu item click event handlers

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

        private void ComboBoxDataProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            var provider = ((DataProviderListItem)this.comboBoxDataProvider.SelectedItem!).provider;
            this.Network.InputSize = provider.NumberOfInputs;
            this.Network.OutputSize = provider.NumberOfOutputs;
            this.networkVisualiser1.Invalidate();
        }

        /// <summary>
        /// Small wrapper used to expose a TypeName property for ListBox DisplayMember while retaining the provider instance.
        /// </summary>
        private sealed record DataProviderListItem(IDataProvider provider)
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
