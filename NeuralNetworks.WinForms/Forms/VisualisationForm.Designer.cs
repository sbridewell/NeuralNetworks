namespace Sde.NeuralNetworks.WinForms.Forms
{
    partial class VisualisationForm
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
            tabPageNodes = new TabPage();
            networkVisualiser1 = new NetworkVisualiser();
            tabPageErrors = new TabPage();
            trainingErrorsChart1 = new Sde.NeuralNetworks.WinForms.Controls.TrainingErrorsChart();
            tabPageTestResults = new TabPage();
            testResultsGrid1 = new TestResultsGrid();
            tabPageJson = new TabPage();
            textBoxJson = new TextBox();
            tabPagePredict = new TabPage();
            predictionControl1 = new Sde.NeuralNetworks.WinForms.Controls.PredictionControl();
            tabControl1.SuspendLayout();
            tabPageNodes.SuspendLayout();
            tabPageErrors.SuspendLayout();
            tabPageTestResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)testResultsGrid1).BeginInit();
            tabPageJson.SuspendLayout();
            tabPagePredict.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Controls.Add(tabPageNodes);
            tabControl1.Controls.Add(tabPageErrors);
            tabControl1.Controls.Add(tabPageTestResults);
            tabControl1.Controls.Add(tabPageJson);
            tabControl1.Controls.Add(tabPagePredict);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(807, 401);
            tabControl1.TabIndex = 0;
            // 
            // tabPageNodes
            // 
            tabPageNodes.Controls.Add(networkVisualiser1);
            tabPageNodes.Location = new Point(4, 37);
            tabPageNodes.Name = "tabPageNodes";
            tabPageNodes.Padding = new Padding(3);
            tabPageNodes.Size = new Size(799, 360);
            tabPageNodes.TabIndex = 0;
            tabPageNodes.Text = "Nodes";
            tabPageNodes.UseVisualStyleBackColor = true;
            // 
            // networkVisualiser1
            // 
            networkVisualiser1.Dock = DockStyle.Fill;
            networkVisualiser1.Location = new Point(3, 3);
            networkVisualiser1.Name = "networkVisualiser1";
            networkVisualiser1.Size = new Size(793, 354);
            networkVisualiser1.TabIndex = 0;
            // 
            // tabPageErrors
            // 
            tabPageErrors.Controls.Add(trainingErrorsChart1);
            tabPageErrors.Location = new Point(4, 37);
            tabPageErrors.Name = "tabPageErrors";
            tabPageErrors.Padding = new Padding(3);
            tabPageErrors.Size = new Size(799, 360);
            tabPageErrors.TabIndex = 1;
            tabPageErrors.Text = "Errors";
            tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // trainingErrorsChart1
            // 
            trainingErrorsChart1.Dock = DockStyle.Fill;
            trainingErrorsChart1.Location = new Point(3, 3);
            trainingErrorsChart1.Name = "trainingErrorsChart1";
            trainingErrorsChart1.Size = new Size(793, 354);
            trainingErrorsChart1.TabIndex = 0;
            // 
            // tabPageTestResults
            // 
            tabPageTestResults.Controls.Add(testResultsGrid1);
            tabPageTestResults.Location = new Point(4, 37);
            tabPageTestResults.Name = "tabPageTestResults";
            tabPageTestResults.Size = new Size(799, 360);
            tabPageTestResults.TabIndex = 2;
            tabPageTestResults.Text = "Test results";
            tabPageTestResults.UseVisualStyleBackColor = true;
            // 
            // testResultsGrid1
            // 
            testResultsGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            testResultsGrid1.Dock = DockStyle.Fill;
            testResultsGrid1.Location = new Point(0, 0);
            testResultsGrid1.Name = "testResultsGrid1";
            testResultsGrid1.Size = new Size(799, 360);
            testResultsGrid1.TabIndex = 0;
            // 
            // tabPageJson
            // 
            tabPageJson.Controls.Add(textBoxJson);
            tabPageJson.Location = new Point(4, 37);
            tabPageJson.Name = "tabPageJson";
            tabPageJson.Size = new Size(799, 360);
            tabPageJson.TabIndex = 3;
            tabPageJson.Text = "JSON";
            tabPageJson.UseVisualStyleBackColor = true;
            // 
            // textBoxJson
            // 
            textBoxJson.Dock = DockStyle.Fill;
            textBoxJson.Location = new Point(0, 0);
            textBoxJson.Multiline = true;
            textBoxJson.Name = "textBoxJson";
            textBoxJson.ReadOnly = true;
            textBoxJson.ScrollBars = ScrollBars.Vertical;
            textBoxJson.Size = new Size(799, 360);
            textBoxJson.TabIndex = 0;
            // 
            // tabPagePredict
            // 
            tabPagePredict.Controls.Add(predictionControl1);
            tabPagePredict.Location = new Point(4, 37);
            tabPagePredict.Name = "tabPagePredict";
            tabPagePredict.Size = new Size(799, 360);
            tabPagePredict.TabIndex = 4;
            tabPagePredict.Text = "Predict";
            tabPagePredict.UseVisualStyleBackColor = true;
            // 
            // predictionControl1
            // 
            predictionControl1.Dock = DockStyle.Fill;
            predictionControl1.Location = new Point(0, 0);
            predictionControl1.Name = "predictionControl1";
            predictionControl1.Size = new Size(799, 360);
            predictionControl1.TabIndex = 0;
            // 
            // VisualisationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 401);
            ControlBox = false;
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "VisualisationForm";
            Text = "Visualisation";
            tabControl1.ResumeLayout(false);
            tabPageNodes.ResumeLayout(false);
            tabPageErrors.ResumeLayout(false);
            tabPageTestResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)testResultsGrid1).EndInit();
            tabPageJson.ResumeLayout(false);
            tabPageJson.PerformLayout();
            tabPagePredict.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageNodes;
        private TabPage tabPageErrors;
        private TabPage tabPageTestResults;
        private TabPage tabPageJson;
        private TabPage tabPagePredict;
        private NetworkVisualiser networkVisualiser1;
        private TestResultsGrid testResultsGrid1;
        private TextBox textBoxJson;
        private Controls.TrainingErrorsChart trainingErrorsChart1;
        private Controls.PredictionControl predictionControl1;
    }
}