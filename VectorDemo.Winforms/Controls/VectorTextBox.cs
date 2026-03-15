// <copyright file="VectorTextBox.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms.Controls
{
    using System;
    using System.ComponentModel;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Parsers;

    /// <summary>
    /// A text box for entering vector values.
    /// </summary>
    public partial class VectorTextBox : TextBox
    {
        private Vector value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorTextBox"/> class.
        /// </summary>
        public VectorTextBox()
        {
            this.InitializeComponent();
            this.Text = this.Value.ToString();
            this.Validating += this.VectorTextBox_Validating;
        }

        /// <summary>
        /// Gets or sets the vector value represented by the text in the text box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Vector Value
        {
            get => this.value;
            set
            {
                this.value = value;
                this.Text = value.ToString();
            }
        }

        /// <summary>
        /// Prevents all the text in the box from being selected when the box receives focus.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.BeginInvoke((Action)(() => this.SelectionLength = 0));
        }

        private void VectorTextBox_Validating(object? sender, CancelEventArgs e)
        {
            var parser = new StringToVectorParser();
            var success = parser.TryParse(this.Text, out Vector vector);
            if (success)
            {
                this.Value = vector;
            }
            else
            {
                e.Cancel = true;
                var msg = "Please enter a valid vector value, which must consist of numeric values separated by commas or spaces.";
                MessageBox.Show(msg, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
