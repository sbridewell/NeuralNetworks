namespace Sde.NeuralNetworks.WinForms.Forms
{
    partial class ActivationFunctionProviderForm
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            activationFunctionProviderControlHidden1 = new ActivationFunctionProviderControl();
            tabPage2 = new TabPage();
            activationFunctionProviderControlOutput = new ActivationFunctionProviderControl();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(activationFunctionProviderControlHidden1);
            tabPage1.Location = new Point(4, 37);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 409);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Hidden layer 1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // activationFunctionProviderControlHidden1
            // 
            activationFunctionProviderControlHidden1.Dock = DockStyle.Fill;
            activationFunctionProviderControlHidden1.Location = new Point(3, 3);
            activationFunctionProviderControlHidden1.Name = "activationFunctionProviderControlHidden1";
            activationFunctionProviderControlHidden1.Size = new Size(786, 403);
            activationFunctionProviderControlHidden1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(activationFunctionProviderControlOutput);
            tabPage2.Location = new Point(4, 37);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 409);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Output layer";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // activationFunctionProviderControlOutput
            // 
            activationFunctionProviderControlOutput.Dock = DockStyle.Fill;
            activationFunctionProviderControlOutput.Location = new Point(3, 3);
            activationFunctionProviderControlOutput.Name = "activationFunctionProviderControlOutput";
            activationFunctionProviderControlOutput.Size = new Size(786, 403);
            activationFunctionProviderControlOutput.TabIndex = 0;
            // 
            // ActivationFunctionProviderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "ActivationFunctionProviderForm";
            Text = "ActivationFunctionProviderForm";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private ActivationFunctionProviderControl activationFunctionProviderControlHidden1;
        private TabPage tabPage2;
        private ActivationFunctionProviderControl activationFunctionProviderControlOutput;
    }
}