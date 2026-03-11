// <copyright file="PredictionControl.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Controls
{
    using System.ComponentModel;
    using Sde.NeuralNetworks.Networks;

    /// <summary>
    /// A control which allows the user to input values for a test case and
    /// see the network's predicted output for those values.
    /// </summary>
    public partial class PredictionControl : UserControl
    {
        private INeuralNetwork? network;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictionControl"/> class.
        /// </summary>
        public PredictionControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the neural network used to make predictions.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INeuralNetwork? Network
        {
            get
            {
                return this.network;
            }

            set
            {
                if (value != null)
                {
                    this.network = value;
                    this.AddChildControls();
                }
            }
        }

        /// <summary>
        /// Disables user input.
        /// </summary>
        public void DisableUserInput()
        {
            foreach (var textBox in this.tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                textBox.Enabled = false;
            }

            var predictButton = this.tableLayoutPanel1.Controls.OfType<Button>().FirstOrDefault(b => b.Name == "predictButton");
            if (predictButton != null)
            {
                predictButton.Enabled = false;
            }
        }

        /// <summary>
        /// Enables user input.
        /// </summary>
        public void EnableUserInput()
        {
            foreach (var textBox in this.tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                textBox.Enabled = true;
            }

            var predictButton = this.tableLayoutPanel1.Controls.OfType<Button>().FirstOrDefault(b => b.Name == "predictButton");
            if (predictButton != null)
            {
                predictButton.Enabled = true;
            }
        }

        private void PredictButton_Click(object? sender, EventArgs e)
        {
            if (this.network == null)
            {
                return;
            }

            var inputs = new double[this.network.InputSize];
            var inputTextBoxes = this.tableLayoutPanel1.Controls.OfType<TextBox>().Where(tb => tb.Name.StartsWith("inputTextBox")).ToArray();
            for (var i = 0; i < inputs.Length; i++)
            {
                if (double.TryParse(inputTextBoxes[i].Text, out var value))
                {
                    inputs[i] = value;
                }
                else
                {
                    MessageBox.Show($"Invalid input for Input {i}: '{inputTextBoxes[i].Text}' is not a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var outputs = this.network.Predict(inputs);
            var outputTextBoxes = this.tableLayoutPanel1.Controls.OfType<TextBox>().Where(tb => tb.Name.StartsWith("outputTextBox")).ToArray();
            for (var o = 0; o < outputs.Length; o++)
            {
                outputTextBoxes[o].Text = outputs[o].ToString("G4");
            }
        }

        private void AddChildControls()
        {
            if (this.network == null)
            {
                return;
            }

            // Clear previous rows/controls and ensure two columns (label / control).
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.RowCount = 0;

            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // labels
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // textboxes expand

            // Add input rows (label + textbox)
            for (var i = 0; i < this.network.InputSize; i++)
            {
                var label = new Label { Text = $"Input {i}:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Top };
                var textBox = new TextBox { Name = $"inputTextBox{i}", Anchor = AnchorStyles.Left | AnchorStyles.Right };

                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                this.tableLayoutPanel1.Controls.Add(label, 0, this.tableLayoutPanel1.RowCount);
                this.tableLayoutPanel1.Controls.Add(textBox, 1, this.tableLayoutPanel1.RowCount);
                this.tableLayoutPanel1.RowCount++;
            }

            // Add a button that spans both columns
            var button = new Button
            {
                Text = "Predict",
                Name = "predictButton",
                Font = this.Font,
            };
            button.Click += this.PredictButton_Click;
            var buttonHeight = button.PreferredSize.Height + 6;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, buttonHeight));
            this.tableLayoutPanel1.Controls.Add(button, 0, this.tableLayoutPanel1.RowCount);
            this.tableLayoutPanel1.SetColumnSpan(button, 2);
            button.Dock = DockStyle.Fill; // set this after setting the row height to ensure it fills the row correctly
            this.tableLayoutPanel1.RowCount++;

            // Add output rows (label + read-only textbox)
            for (var o = 0; o < this.network.OutputSize; o++)
            {
                var label = new Label { Text = $"Output {o}:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Top };
                var textBox = new TextBox { Name = $"outputTextBox{o}", ReadOnly = true, Anchor = AnchorStyles.Left | AnchorStyles.Right };

                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                this.tableLayoutPanel1.Controls.Add(label, 0, this.tableLayoutPanel1.RowCount);
                this.tableLayoutPanel1.Controls.Add(textBox, 1, this.tableLayoutPanel1.RowCount);
                this.tableLayoutPanel1.RowCount++;
            }
        }
    }
}
