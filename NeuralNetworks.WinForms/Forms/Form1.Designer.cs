namespace Sde.NeuralNetworks.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainer1 = new SplitContainer();
            progressBar1 = new ProgressBar();
            buttonStop = new Button();
            buttonGo = new Button();
            symmericSplitContainer1 = new SymmericSplitContainer();
            tabControlFunctions = new TabControl();
            tabPageHiddenLayer = new TabPage();
            activationFunctionProviderControlHidden1 = new ActivationFunctionProviderControl();
            tabPageOutputLayer = new TabPage();
            activationFunctionProviderControlOutput = new ActivationFunctionProviderControl();
            tabControlVisualisation = new TabControl();
            tabPageNodes = new TabPage();
            networkVisualiser1 = new NetworkVisualiser();
            tabPageErrors = new TabPage();
            errorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tabPageTestResults = new TabPage();
            testResultsGrid1 = new TestResultsGrid();
            tabPageJson = new TabPage();
            textBoxJson = new TextBox();
            tabPagePredict = new TabPage();
            buttonPredict = new Button();
            dataGridViewOutputs = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewInputs = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            label4 = new Label();
            label3 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openTrainedModelToolStripMenuItem = new ToolStripMenuItem();
            saveCurrentModelToolStripMenuItem = new ToolStripMenuItem();
            enableDisableUIFeaturesToolStripMenuItem = new ToolStripMenuItem();
            networkVisualisationToolStripMenuItem = new ToolStripMenuItem();
            errorsChartToolStripMenuItem = new ToolStripMenuItem();
            statusBarToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)symmericSplitContainer1).BeginInit();
            symmericSplitContainer1.Panel1.SuspendLayout();
            symmericSplitContainer1.Panel2.SuspendLayout();
            symmericSplitContainer1.SuspendLayout();
            tabControlFunctions.SuspendLayout();
            tabPageHiddenLayer.SuspendLayout();
            tabPageOutputLayer.SuspendLayout();
            tabControlVisualisation.SuspendLayout();
            tabPageNodes.SuspendLayout();
            tabPageErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorChart).BeginInit();
            tabPageTestResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)testResultsGrid1).BeginInit();
            tabPageJson.SuspendLayout();
            tabPagePredict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOutputs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInputs).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 36);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(progressBar1);
            splitContainer1.Panel1.Controls.Add(buttonStop);
            splitContainer1.Panel1.Controls.Add(buttonGo);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(symmericSplitContainer1);
            splitContainer1.Size = new Size(1241, 583);
            splitContainer1.SplitterDistance = 102;
            splitContainer1.TabIndex = 0;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(3, 76);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1233, 23);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 15;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonStop.Location = new Point(138, 4);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(133, 37);
            buttonStop.TabIndex = 12;
            buttonStop.Text = "Stop training";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += ButtonStopClick;
            // 
            // buttonGo
            // 
            buttonGo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonGo.Location = new Point(3, 3);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new Size(129, 39);
            buttonGo.TabIndex = 5;
            buttonGo.Text = "Start training";
            buttonGo.UseVisualStyleBackColor = false;
            buttonGo.Click += ButtonGo_Click;
            // 
            // symmericSplitContainer1
            // 
            symmericSplitContainer1.BorderStyle = BorderStyle.Fixed3D;
            symmericSplitContainer1.Dock = DockStyle.Fill;
            symmericSplitContainer1.Location = new Point(0, 0);
            symmericSplitContainer1.Name = "symmericSplitContainer1";
            // 
            // symmericSplitContainer1.Panel1
            // 
            symmericSplitContainer1.Panel1.Controls.Add(tabControlFunctions);
            // 
            // symmericSplitContainer1.Panel2
            // 
            symmericSplitContainer1.Panel2.Controls.Add(tabControlVisualisation);
            symmericSplitContainer1.Size = new Size(1241, 477);
            symmericSplitContainer1.SplitterDistance = 620;
            symmericSplitContainer1.TabIndex = 19;
            // 
            // tabControlFunctions
            // 
            tabControlFunctions.Appearance = TabAppearance.Buttons;
            tabControlFunctions.Controls.Add(tabPageHiddenLayer);
            tabControlFunctions.Controls.Add(tabPageOutputLayer);
            tabControlFunctions.Dock = DockStyle.Fill;
            tabControlFunctions.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControlFunctions.Location = new Point(0, 0);
            tabControlFunctions.Name = "tabControlFunctions";
            tabControlFunctions.SelectedIndex = 0;
            tabControlFunctions.Size = new Size(616, 473);
            tabControlFunctions.TabIndex = 17;
            // 
            // tabPageHiddenLayer
            // 
            tabPageHiddenLayer.Controls.Add(activationFunctionProviderControlHidden1);
            tabPageHiddenLayer.Location = new Point(4, 37);
            tabPageHiddenLayer.Name = "tabPageHiddenLayer";
            tabPageHiddenLayer.Padding = new Padding(3);
            tabPageHiddenLayer.Size = new Size(608, 432);
            tabPageHiddenLayer.TabIndex = 0;
            tabPageHiddenLayer.Text = "Hidden layer 1";
            tabPageHiddenLayer.UseVisualStyleBackColor = true;
            // 
            // activationFunctionProviderControlHidden1
            // 
            activationFunctionProviderControlHidden1.Dock = DockStyle.Fill;
            activationFunctionProviderControlHidden1.Location = new Point(3, 3);
            activationFunctionProviderControlHidden1.Name = "activationFunctionProviderControlHidden1";
            activationFunctionProviderControlHidden1.Size = new Size(602, 426);
            activationFunctionProviderControlHidden1.TabIndex = 1;
            // 
            // tabPageOutputLayer
            // 
            tabPageOutputLayer.Controls.Add(activationFunctionProviderControlOutput);
            tabPageOutputLayer.Location = new Point(4, 37);
            tabPageOutputLayer.Name = "tabPageOutputLayer";
            tabPageOutputLayer.Padding = new Padding(3);
            tabPageOutputLayer.Size = new Size(608, 432);
            tabPageOutputLayer.TabIndex = 1;
            tabPageOutputLayer.Text = "Output layer";
            tabPageOutputLayer.UseVisualStyleBackColor = true;
            // 
            // activationFunctionProviderControlOutput
            // 
            activationFunctionProviderControlOutput.Dock = DockStyle.Fill;
            activationFunctionProviderControlOutput.Location = new Point(3, 3);
            activationFunctionProviderControlOutput.Name = "activationFunctionProviderControlOutput";
            activationFunctionProviderControlOutput.Size = new Size(602, 426);
            activationFunctionProviderControlOutput.TabIndex = 0;
            // 
            // tabControlVisualisation
            // 
            tabControlVisualisation.Appearance = TabAppearance.Buttons;
            tabControlVisualisation.Controls.Add(tabPageNodes);
            tabControlVisualisation.Controls.Add(tabPageErrors);
            tabControlVisualisation.Controls.Add(tabPageTestResults);
            tabControlVisualisation.Controls.Add(tabPageJson);
            tabControlVisualisation.Controls.Add(tabPagePredict);
            tabControlVisualisation.Dock = DockStyle.Fill;
            tabControlVisualisation.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControlVisualisation.HotTrack = true;
            tabControlVisualisation.Location = new Point(0, 0);
            tabControlVisualisation.Name = "tabControlVisualisation";
            tabControlVisualisation.SelectedIndex = 0;
            tabControlVisualisation.Size = new Size(613, 473);
            tabControlVisualisation.TabIndex = 0;
            // 
            // tabPageNodes
            // 
            tabPageNodes.Controls.Add(networkVisualiser1);
            tabPageNodes.Location = new Point(4, 37);
            tabPageNodes.Name = "tabPageNodes";
            tabPageNodes.Padding = new Padding(3);
            tabPageNodes.Size = new Size(605, 432);
            tabPageNodes.TabIndex = 0;
            tabPageNodes.Text = "Nodes";
            tabPageNodes.UseVisualStyleBackColor = true;
            // 
            // networkVisualiser1
            // 
            networkVisualiser1.Dock = DockStyle.Fill;
            networkVisualiser1.Location = new Point(3, 3);
            networkVisualiser1.Margin = new Padding(5);
            networkVisualiser1.Name = "networkVisualiser1";
            networkVisualiser1.Size = new Size(599, 426);
            networkVisualiser1.TabIndex = 0;
            // 
            // tabPageErrors
            // 
            tabPageErrors.Controls.Add(errorChart);
            tabPageErrors.Location = new Point(4, 37);
            tabPageErrors.Name = "tabPageErrors";
            tabPageErrors.Size = new Size(605, 432);
            tabPageErrors.TabIndex = 2;
            tabPageErrors.Text = "Errors";
            tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // chartErrors
            // 
            chartArea1.Name = "ChartArea1";
            errorChart.ChartAreas.Add(chartArea1);
            errorChart.Dock = DockStyle.Fill;
            errorChart.Location = new Point(0, 0);
            errorChart.Name = "chartErrors";
            errorChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            errorChart.Series.Add(series1);
            errorChart.Size = new Size(605, 432);
            errorChart.SuppressExceptions = true;
            errorChart.TabIndex = 18;
            errorChart.Text = "chart1";
            // 
            // tabPageTestResults
            // 
            tabPageTestResults.Controls.Add(testResultsGrid1);
            tabPageTestResults.Location = new Point(4, 37);
            tabPageTestResults.Name = "tabPageTestResults";
            tabPageTestResults.Padding = new Padding(3);
            tabPageTestResults.Size = new Size(605, 432);
            tabPageTestResults.TabIndex = 1;
            tabPageTestResults.Text = "Test results";
            tabPageTestResults.UseVisualStyleBackColor = true;
            // 
            // testResultsGrid1
            // 
            testResultsGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            testResultsGrid1.Dock = DockStyle.Fill;
            testResultsGrid1.Location = new Point(3, 3);
            testResultsGrid1.Name = "testResultsGrid1";
            testResultsGrid1.Size = new Size(599, 426);
            testResultsGrid1.TabIndex = 0;
            // 
            // tabPageJson
            // 
            tabPageJson.Controls.Add(textBoxJson);
            tabPageJson.Location = new Point(4, 37);
            tabPageJson.Name = "tabPageJson";
            tabPageJson.Size = new Size(605, 432);
            tabPageJson.TabIndex = 3;
            tabPageJson.Text = "JSON";
            tabPageJson.UseVisualStyleBackColor = true;
            // 
            // textBoxJson
            // 
            textBoxJson.Dock = DockStyle.Fill;
            textBoxJson.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxJson.Location = new Point(0, 0);
            textBoxJson.Multiline = true;
            textBoxJson.Name = "textBoxJson";
            textBoxJson.ReadOnly = true;
            textBoxJson.ScrollBars = ScrollBars.Vertical;
            textBoxJson.Size = new Size(605, 432);
            textBoxJson.TabIndex = 0;
            // 
            // tabPagePredict
            // 
            tabPagePredict.Controls.Add(buttonPredict);
            tabPagePredict.Controls.Add(dataGridViewOutputs);
            tabPagePredict.Controls.Add(dataGridViewInputs);
            tabPagePredict.Controls.Add(label4);
            tabPagePredict.Controls.Add(label3);
            tabPagePredict.Location = new Point(4, 37);
            tabPagePredict.Name = "tabPagePredict";
            tabPagePredict.Padding = new Padding(3);
            tabPagePredict.Size = new Size(605, 432);
            tabPagePredict.TabIndex = 4;
            tabPagePredict.Text = "Predict";
            tabPagePredict.UseVisualStyleBackColor = true;
            // 
            // buttonPredict
            // 
            buttonPredict.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonPredict.Location = new Point(3, 65);
            buttonPredict.Name = "buttonPredict";
            buttonPredict.Size = new Size(593, 35);
            buttonPredict.TabIndex = 4;
            buttonPredict.Text = "Predict";
            buttonPredict.UseVisualStyleBackColor = true;
            buttonPredict.Click += ButtonPredict_Click;
            // 
            // dataGridViewOutputs
            // 
            dataGridViewOutputs.AllowUserToAddRows = false;
            dataGridViewOutputs.AllowUserToDeleteRows = false;
            dataGridViewOutputs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewOutputs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dataGridViewOutputs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewOutputs.ColumnHeadersVisible = false;
            dataGridViewOutputs.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1 });
            dataGridViewOutputs.Location = new Point(3, 136);
            dataGridViewOutputs.Name = "dataGridViewOutputs";
            dataGridViewOutputs.ReadOnly = true;
            dataGridViewOutputs.RowHeadersVisible = false;
            dataGridViewOutputs.Size = new Size(593, 33);
            dataGridViewOutputs.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Column1";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 5;
            // 
            // dataGridViewInputs
            // 
            dataGridViewInputs.AllowUserToAddRows = false;
            dataGridViewInputs.AllowUserToDeleteRows = false;
            dataGridViewInputs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewInputs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dataGridViewInputs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInputs.ColumnHeadersVisible = false;
            dataGridViewInputs.Columns.AddRange(new DataGridViewColumn[] { Column1 });
            dataGridViewInputs.Location = new Point(3, 35);
            dataGridViewInputs.Name = "dataGridViewInputs";
            dataGridViewInputs.RowHeadersVisible = false;
            dataGridViewInputs.Size = new Size(593, 33);
            dataGridViewInputs.TabIndex = 2;
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 5;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Location = new Point(3, 104);
            label4.Name = "label4";
            label4.Size = new Size(593, 29);
            label4.TabIndex = 1;
            label4.Text = "Outputs";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(3, 3);
            label3.Name = "label3";
            label3.Size = new Size(593, 29);
            label3.TabIndex = 0;
            label3.Text = "Inputs";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 614);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1241, 30);
            statusStrip1.TabIndex = 3;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(1226, 25);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Text = "Ready";
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, enableDisableUIFeaturesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1241, 33);
            menuStrip1.TabIndex = 18;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openTrainedModelToolStripMenuItem, saveCurrentModelToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(53, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // openTrainedModelToolStripMenuItem
            // 
            openTrainedModelToolStripMenuItem.Name = "openTrainedModelToolStripMenuItem";
            openTrainedModelToolStripMenuItem.Size = new Size(253, 30);
            openTrainedModelToolStripMenuItem.Text = "Open trained model";
            openTrainedModelToolStripMenuItem.Click += OpenTrainedModelToolStripMenuItem_Click;
            // 
            // saveCurrentModelToolStripMenuItem
            // 
            saveCurrentModelToolStripMenuItem.Name = "saveCurrentModelToolStripMenuItem";
            saveCurrentModelToolStripMenuItem.Size = new Size(253, 30);
            saveCurrentModelToolStripMenuItem.Text = "Save current model";
            saveCurrentModelToolStripMenuItem.Click += SaveCurrentModelToolStripMenuItem_Click;
            // 
            // enableDisableUIFeaturesToolStripMenuItem
            // 
            enableDisableUIFeaturesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { networkVisualisationToolStripMenuItem, errorsChartToolStripMenuItem, statusBarToolStripMenuItem });
            enableDisableUIFeaturesToolStripMenuItem.Name = "enableDisableUIFeaturesToolStripMenuItem";
            enableDisableUIFeaturesToolStripMenuItem.Size = new Size(254, 29);
            enableDisableUIFeaturesToolStripMenuItem.Text = "Enable / disable UI features";
            // 
            // networkVisualisationToolStripMenuItem
            // 
            networkVisualisationToolStripMenuItem.Checked = true;
            networkVisualisationToolStripMenuItem.CheckOnClick = true;
            networkVisualisationToolStripMenuItem.CheckState = CheckState.Checked;
            networkVisualisationToolStripMenuItem.Name = "networkVisualisationToolStripMenuItem";
            networkVisualisationToolStripMenuItem.Size = new Size(264, 30);
            networkVisualisationToolStripMenuItem.Text = "Network visualisation";
            networkVisualisationToolStripMenuItem.Click += NetworkVisualisationToolStripMenuItem_Click;
            // 
            // errorsChartToolStripMenuItem
            // 
            errorsChartToolStripMenuItem.Checked = true;
            errorsChartToolStripMenuItem.CheckOnClick = true;
            errorsChartToolStripMenuItem.CheckState = CheckState.Checked;
            errorsChartToolStripMenuItem.Name = "errorsChartToolStripMenuItem";
            errorsChartToolStripMenuItem.Size = new Size(264, 30);
            errorsChartToolStripMenuItem.Text = "Errors chart";
            errorsChartToolStripMenuItem.Click += ErrorsChartToolStripMenuItem_Click;
            // 
            // statusBarToolStripMenuItem
            // 
            statusBarToolStripMenuItem.Checked = true;
            statusBarToolStripMenuItem.CheckOnClick = true;
            statusBarToolStripMenuItem.CheckState = CheckState.Checked;
            statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            statusBarToolStripMenuItem.Size = new Size(264, 30);
            statusBarToolStripMenuItem.Text = "Status bar";
            statusBarToolStripMenuItem.Click += StatusBarToolStripMenuItem_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.AddToRecent = false;
            saveFileDialog1.Filter = "JSON files|*.json";
            saveFileDialog1.Title = "Save network to file";
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddToRecent = false;
            openFileDialog1.Filter = "JSON files|*.json";
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Title = "Browse for trained network file";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1241, 644);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Neural Network Playground";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            symmericSplitContainer1.Panel1.ResumeLayout(false);
            symmericSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)symmericSplitContainer1).EndInit();
            symmericSplitContainer1.ResumeLayout(false);
            tabControlFunctions.ResumeLayout(false);
            tabPageHiddenLayer.ResumeLayout(false);
            tabPageOutputLayer.ResumeLayout(false);
            tabControlVisualisation.ResumeLayout(false);
            tabPageNodes.ResumeLayout(false);
            tabPageErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorChart).EndInit();
            tabPageTestResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)testResultsGrid1).EndInit();
            tabPageJson.ResumeLayout(false);
            tabPageJson.PerformLayout();
            tabPagePredict.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewOutputs).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInputs).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button buttonGo;
        private TabControl tabControlVisualisation;
        private TabPage tabPageNodes;
        private TabPage tabPageTestResults;
        private Sde.NeuralNetworks.WinForms.NetworkVisualiser networkVisualiser1;
        private Button buttonStop;
        private TabControl tabControlFunctions;
        private TabPage tabPageHiddenLayer;
        private TabPage tabPageOutputLayer;
        private System.Windows.Forms.DataVisualization.Charting.Chart errorChart;
        private ActivationFunctionProviderControl activationFunctionProviderControlHidden1;
        private ActivationFunctionProviderControl activationFunctionProviderControlOutput;
        private SymmericSplitContainer symmericSplitContainer1;
        private TabPage tabPageErrors;
        private TabPage tabPageJson;
        private TextBox textBoxJson;
        private TestResultsGrid testResultsGrid1;
        private ProgressBar progressBar1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openTrainedModelToolStripMenuItem;
        private ToolStripMenuItem saveCurrentModelToolStripMenuItem;
        private ToolStripMenuItem enableDisableUIFeaturesToolStripMenuItem;
        private ToolStripMenuItem networkVisualisationToolStripMenuItem;
        private ToolStripMenuItem errorsChartToolStripMenuItem;
        private ToolStripMenuItem statusBarToolStripMenuItem;
        private TabPage tabPagePredict;
        private Label label4;
        private Label label3;
        private DataGridView dataGridViewInputs;
        private DataGridViewTextBoxColumn Column1;
        private DataGridView dataGridViewOutputs;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private Button buttonPredict;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
    }
}
