namespace Sde.NeuralNetworks.WinForms.Forms
{
    partial class MultiLayerNetworkVisualisationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            multiLayerNetworkVisualiser1 = new Sde.NeuralNetworks.WinForms.Controls.MultiLayerNetworkVisualiser();
            SuspendLayout();
            // 
            // multiLayerNetworkVisualiser1
            // 
            multiLayerNetworkVisualiser1.Dock = DockStyle.Fill;
            multiLayerNetworkVisualiser1.Location = new Point(0, 0);
            multiLayerNetworkVisualiser1.Name = "multiLayerNetworkVisualiser1";
            multiLayerNetworkVisualiser1.Size = new Size(800, 450);
            multiLayerNetworkVisualiser1.TabIndex = 0;
            // 
            // MultiLayerNetworkVisualisationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(multiLayerNetworkVisualiser1);
            Name = "MultiLayerNetworkVisualisationForm";
            Text = "MultiLayerNetworkVisualisationForm";
            ResumeLayout(false);
        }

        #endregion

        private Controls.MultiLayerNetworkVisualiser multiLayerNetworkVisualiser1;
    }
}