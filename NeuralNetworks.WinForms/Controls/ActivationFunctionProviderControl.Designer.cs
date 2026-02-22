namespace Sde.NeuralNetworks.WinForms
{
    partial class ActivationFunctionProviderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            comboBox1 = new ComboBox();
            activationFunctionControl1 = new ActivationFunctionControl();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 4);
            label1.Name = "label1";
            label1.Size = new Size(591, 23);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 32);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(591, 33);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += ComboBox1SelectedIndexChanged;
            // 
            // activationFunctionControl1
            // 
            activationFunctionControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            activationFunctionControl1.Location = new Point(6, 71);
            activationFunctionControl1.Name = "activationFunctionControl1";
            activationFunctionControl1.Size = new Size(591, 361);
            activationFunctionControl1.TabIndex = 2;
            // 
            // ActivationFunctionProviderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(activationFunctionControl1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Name = "ActivationFunctionProviderControl";
            Size = new Size(600, 435);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private ActivationFunctionControl activationFunctionControl1;
    }
}
