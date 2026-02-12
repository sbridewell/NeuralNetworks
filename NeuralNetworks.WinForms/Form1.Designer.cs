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
            comboBoxDataProvider = new ComboBox();
            numericUpDownMomentum = new NumericUpDown();
            label6 = new Label();
            progressBar1 = new ProgressBar();
            numericUpDownNeuronsPerHiddenLayer = new NumericUpDown();
            label5 = new Label();
            buttonStop = new Button();
            buttonGo = new Button();
            numericUpDownLearningRate = new NumericUpDown();
            label2 = new Label();
            numericUpDownNumberOfIterations = new NumericUpDown();
            label1 = new Label();
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
            chartErrors = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMomentum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNeuronsPerHiddenLayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLearningRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNumberOfIterations).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)chartErrors).BeginInit();
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
            splitContainer1.Panel1.Controls.Add(label7);
            splitContainer1.Panel1.Controls.Add(comboBoxDataProvider);
            splitContainer1.Panel1.Controls.Add(numericUpDownMomentum);
            splitContainer1.Panel1.Controls.Add(label6);
            splitContainer1.Panel1.Controls.Add(progressBar1);
            splitContainer1.Panel1.Controls.Add(numericUpDownNeuronsPerHiddenLayer);
            splitContainer1.Panel1.Controls.Add(label5);
            splitContainer1.Panel1.Controls.Add(buttonStop);
            splitContainer1.Panel1.Controls.Add(buttonGo);
            splitContainer1.Panel1.Controls.Add(numericUpDownLearningRate);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(numericUpDownNumberOfIterations);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(symmericSplitContainer1);
            splitContainer1.Size = new Size(1165, 583);
            splitContainer1.SplitterDistance = 102;
            splitContainer1.TabIndex = 0;
            // 
            // comboBoxDataProvider
            // 
            comboBoxDataProvider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxDataProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDataProvider.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxDataProvider.FormattingEnabled = true;
            comboBoxDataProvider.Location = new Point(166, 38);
            comboBoxDataProvider.Name = "comboBoxDataProvider";
            comboBoxDataProvider.Size = new Size(997, 33);
            comboBoxDataProvider.TabIndex = 18;
            // 
            // numericUpDownMomentum
            // 
            numericUpDownMomentum.DecimalPlaces = 2;
            numericUpDownMomentum.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownMomentum.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownMomentum.Location = new Point(640, 6);
            numericUpDownMomentum.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownMomentum.Name = "numericUpDownMomentum";
            numericUpDownMomentum.Size = new Size(62, 33);
            numericUpDownMomentum.TabIndex = 17;
            numericUpDownMomentum.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(534, 10);
            label6.Name = "label6";
            label6.Size = new Size(110, 25);
            label6.TabIndex = 16;
            label6.Text = "Momentum";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(3, 76);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1157, 23);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 15;
            // 
            // numericUpDownNeuronsPerHiddenLayer
            // 
            numericUpDownNeuronsPerHiddenLayer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownNeuronsPerHiddenLayer.Location = new Point(885, 6);
            numericUpDownNeuronsPerHiddenLayer.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownNeuronsPerHiddenLayer.Name = "numericUpDownNeuronsPerHiddenLayer";
            numericUpDownNeuronsPerHiddenLayer.Size = new Size(46, 33);
            numericUpDownNeuronsPerHiddenLayer.TabIndex = 14;
            numericUpDownNeuronsPerHiddenLayer.Value = new decimal(new int[] { 6, 0, 0, 0 });
            numericUpDownNeuronsPerHiddenLayer.ValueChanged += NumericUpDownNumberOfHiddenLayer_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(708, 10);
            label5.Name = "label5";
            label5.Size = new Size(175, 25);
            label5.TabIndex = 13;
            label5.Text = "Hidden layer nodes";
            // 
            // buttonStop
            // 
            buttonStop.BackColor = Color.FromArgb(255, 128, 128);
            buttonStop.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonStop.Location = new Point(1072, 5);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(133, 37);
            buttonStop.TabIndex = 12;
            buttonStop.Text = "Stop training";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += ButtonStopClick;
            // 
            // buttonGo
            // 
            buttonGo.BackColor = Color.FromArgb(0, 192, 0);
            buttonGo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonGo.Location = new Point(937, 3);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new Size(129, 39);
            buttonGo.TabIndex = 5;
            buttonGo.Text = "Start training";
            buttonGo.UseVisualStyleBackColor = false;
            buttonGo.Click += ButtonGo_Click;
            // 
            // numericUpDownLearningRate
            // 
            numericUpDownLearningRate.DecimalPlaces = 3;
            numericUpDownLearningRate.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownLearningRate.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownLearningRate.Location = new Point(456, 6);
            numericUpDownLearningRate.Name = "numericUpDownLearningRate";
            numericUpDownLearningRate.Size = new Size(70, 33);
            numericUpDownLearningRate.TabIndex = 2;
            numericUpDownLearningRate.Value = new decimal(new int[] { 1, 0, 0, 196608 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(326, 8);
            label2.Name = "label2";
            label2.Size = new Size(124, 25);
            label2.TabIndex = 2;
            label2.Text = "Learning rate";
            // 
            // numericUpDownNumberOfIterations
            // 
            numericUpDownNumberOfIterations.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDownNumberOfIterations.Location = new Point(200, 6);
            numericUpDownNumberOfIterations.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDownNumberOfIterations.Name = "numericUpDownNumberOfIterations";
            numericUpDownNumberOfIterations.Size = new Size(120, 33);
            numericUpDownNumberOfIterations.TabIndex = 1;
            numericUpDownNumberOfIterations.ThousandsSeparator = true;
            numericUpDownNumberOfIterations.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(7, 10);
            label1.Name = "label1";
            label1.Size = new Size(187, 25);
            label1.TabIndex = 0;
            label1.Text = "Number of iterations";
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
            symmericSplitContainer1.Size = new Size(1165, 477);
            symmericSplitContainer1.SplitterDistance = 582;
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
            tabControlFunctions.Size = new Size(578, 473);
            tabControlFunctions.TabIndex = 17;
            // 
            // tabPageHiddenLayer
            // 
            tabPageHiddenLayer.Controls.Add(activationFunctionProviderControlHidden1);
            tabPageHiddenLayer.Location = new Point(4, 37);
            tabPageHiddenLayer.Name = "tabPageHiddenLayer";
            tabPageHiddenLayer.Padding = new Padding(3);
            tabPageHiddenLayer.Size = new Size(570, 432);
            tabPageHiddenLayer.TabIndex = 0;
            tabPageHiddenLayer.Text = "Hidden layer 1";
            tabPageHiddenLayer.UseVisualStyleBackColor = true;
            // 
            // activationFunctionProviderControlHidden1
            // 
            activationFunctionProviderControlHidden1.Dock = DockStyle.Fill;
            activationFunctionProviderControlHidden1.Location = new Point(3, 3);
            activationFunctionProviderControlHidden1.Name = "activationFunctionProviderControlHidden1";
            activationFunctionProviderControlHidden1.Size = new Size(564, 426);
            activationFunctionProviderControlHidden1.TabIndex = 1;
            // 
            // tabPageOutputLayer
            // 
            tabPageOutputLayer.Controls.Add(activationFunctionProviderControlOutput);
            tabPageOutputLayer.Location = new Point(4, 37);
            tabPageOutputLayer.Name = "tabPageOutputLayer";
            tabPageOutputLayer.Padding = new Padding(3);
            tabPageOutputLayer.Size = new Size(570, 432);
            tabPageOutputLayer.TabIndex = 1;
            tabPageOutputLayer.Text = "Output layer";
            tabPageOutputLayer.UseVisualStyleBackColor = true;
            // 
            // activationFunctionProviderControlOutput
            // 
            activationFunctionProviderControlOutput.Dock = DockStyle.Fill;
            activationFunctionProviderControlOutput.Location = new Point(3, 3);
            activationFunctionProviderControlOutput.Name = "activationFunctionProviderControlOutput";
            activationFunctionProviderControlOutput.Size = new Size(564, 426);
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
            tabControlVisualisation.Size = new Size(575, 473);
            tabControlVisualisation.TabIndex = 0;
            // 
            // tabPageNodes
            // 
            tabPageNodes.Controls.Add(networkVisualiser1);
            tabPageNodes.Location = new Point(4, 37);
            tabPageNodes.Name = "tabPageNodes";
            tabPageNodes.Padding = new Padding(3);
            tabPageNodes.Size = new Size(567, 432);
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
            networkVisualiser1.Size = new Size(561, 426);
            networkVisualiser1.TabIndex = 0;
            // 
            // tabPageErrors
            // 
            tabPageErrors.Controls.Add(chartErrors);
            tabPageErrors.Location = new Point(4, 37);
            tabPageErrors.Name = "tabPageErrors";
            tabPageErrors.Size = new Size(567, 432);
            tabPageErrors.TabIndex = 2;
            tabPageErrors.Text = "Errors";
            tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // chartErrors
            // 
            chartArea1.Name = "ChartArea1";
            chartErrors.ChartAreas.Add(chartArea1);
            chartErrors.Dock = DockStyle.Fill;
            chartErrors.Location = new Point(0, 0);
            chartErrors.Name = "chartErrors";
            chartErrors.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            chartErrors.Series.Add(series1);
            chartErrors.Size = new Size(567, 432);
            chartErrors.SuppressExceptions = true;
            chartErrors.TabIndex = 18;
            chartErrors.Text = "chart1";
            // 
            // tabPageTestResults
            // 
            tabPageTestResults.Controls.Add(testResultsGrid1);
            tabPageTestResults.Location = new Point(4, 37);
            tabPageTestResults.Name = "tabPageTestResults";
            tabPageTestResults.Padding = new Padding(3);
            tabPageTestResults.Size = new Size(567, 432);
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
            testResultsGrid1.Size = new Size(561, 426);
            testResultsGrid1.TabIndex = 0;
            // 
            // tabPageJson
            // 
            tabPageJson.Controls.Add(textBoxJson);
            tabPageJson.Location = new Point(4, 37);
            tabPageJson.Name = "tabPageJson";
            tabPageJson.Size = new Size(567, 432);
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
            textBoxJson.Size = new Size(567, 432);
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
            tabPagePredict.Size = new Size(567, 432);
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
            statusStrip1.Location = new Point(0, 622);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1165, 22);
            statusStrip1.TabIndex = 3;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, enableDisableUIFeaturesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1165, 33);
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
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(7, 40);
            label7.Name = "label7";
            label7.Size = new Size(153, 25);
            label7.TabIndex = 19;
            label7.Text = "Problem to solve";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1165, 644);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Neural Network Playground";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownMomentum).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNeuronsPerHiddenLayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLearningRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNumberOfIterations).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)chartErrors).EndInit();
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
        private NumericUpDown numericUpDownLearningRate;
        private Label label2;
        private NumericUpDown numericUpDownNumberOfIterations;
        private Label label1;
        private Button buttonGo;
        private TabControl tabControlVisualisation;
        private TabPage tabPageNodes;
        private TabPage tabPageTestResults;
        private Sde.NeuralNetworks.WinForms.NetworkVisualiser networkVisualiser1;
        private Button buttonStop;
        private Label label5;
        private NumericUpDown numericUpDownNeuronsPerHiddenLayer;
        private TabControl tabControlFunctions;
        private TabPage tabPageHiddenLayer;
        private TabPage tabPageOutputLayer;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartErrors;
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
        private Label label6;
        private NumericUpDown numericUpDownMomentum;
        private ComboBox comboBoxDataProvider;
        private Label label7;
    }
}
