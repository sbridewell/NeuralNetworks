// <copyright file="VectorDemoMainForm.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.VectorDemo.Winforms.Forms
{
    /// <summary>
    /// Main form in the application.
    /// </summary>
    public partial class VectorDemoMainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VectorDemoMainForm"/> class.
        /// </summary>
        public VectorDemoMainForm()
        {
            this.InitializeComponent();
        }

        private void ButtonAddition_Click(object sender, EventArgs e)
        {
            new AdditionForm().Show();
        }

        private void ButtonSubtraction_Click(object sender, EventArgs e)
        {
            new SubtractionForm().Show();
        }

        private void ButtonDotProduct_Click(object sender, EventArgs e)
        {
            new DotProductForm().Show();
        }

        private void ButtonScalarMultiplication_Click(object sender, EventArgs e)
        {
            new ScalarMultiplicationForm().Show();
        }

        private void ButtonElementWiseMultiplication_Click(object sender, EventArgs e)
        {
            new ElementWiseMultiplicationForm().Show();
        }

        private void ButtonMagnitude_Click(object sender, EventArgs e)
        {
            new MagnitudeForm().Show();
        }

        private void ButtonCosineSimilarity_Click(object sender, EventArgs e)
        {
            new CosineSimilarityForm().Show();
        }

        private void ButtonScaling_Click(object sender, EventArgs e)
        {
            new FeatureScalingForm().Show();
        }
    }
}
