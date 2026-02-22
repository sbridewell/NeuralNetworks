// <copyright file="ErrorForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms.Forms
{
    using System.ComponentModel;

    /// <summary>
    /// Form for displaying details of an exception.
    /// </summary>
    public partial class ErrorForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorForm"/> class.
        /// </summary>
        public ErrorForm()
        {
            this.InitializeComponent();
            this.textBoxError.Text = this.Exception?.ToString() ?? "Hmmm, the application hasn't supplied an exception for me to display.";
        }

        /// <summary>
        /// Gets or sets the exception to be displayed in the form.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Exception? Exception { get; set; }
    }
}
