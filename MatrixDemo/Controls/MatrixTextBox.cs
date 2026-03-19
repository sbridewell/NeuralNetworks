// <copyright file="MatrixTextBox.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.MatrixDemo.Controls
{
    using System.ComponentModel;
    using Sde.NeuralNetworks.LinearAlgebra;
    using Sde.NeuralNetworks.Parsers;

    /// <summary>
    /// A text box for entering matrix values.
    /// </summary>
    public partial class MatrixTextBox : TextBox
    {
        private Matrix value = new Matrix(new Vector[] { });

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixTextBox"/> class.
        /// </summary>
        public MatrixTextBox()
        {
            this.InitializeComponent();
            this.Text = this.Value.ToString();
            this.Validating += this.MatrixTextBox_Validating;
        }

        /// <summary>
        /// Gets or sets the matrix value represented by the text in the text box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Matrix Value
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

        private void MatrixTextBox_Validating(object? sender, CancelEventArgs e)
        {
            var parser = new StringToMatrixParser();
            var success = parser.TryParse(this.Text, out Matrix matrix);
            if (success)
            {
                this.Value = matrix;
            }
            else
            {
                e.Cancel = true;
                var msg = "Please enter a valid matrix value, which must consist of vector lines separated by line breaks.";
                MessageBox.Show(msg, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
