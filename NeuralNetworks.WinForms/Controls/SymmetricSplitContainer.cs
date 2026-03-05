// <copyright file="SymmetricSplitContainer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    /// <summary>
    /// Subclass of <see cref="SplitContainer"/> which adjusts the splitter distance each
    /// time it is resized, to keep both panels the same size.
    /// </summary>
    public partial class SymmetricSplitContainer : SplitContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricSplitContainer"/> class.
        /// </summary>
        public SymmetricSplitContainer()
        {
            this.InitializeComponent();
            this.Resize += (sender, eventArgs) =>
            {
                if (this.Orientation == Orientation.Vertical)
                {
                    this.SplitterDistance = this.Width / 2;
                }
                else
                {
                    this.SplitterDistance = this.Height / 2;
                }
            };
        }
    }
}