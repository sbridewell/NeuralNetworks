namespace Sde.NeuralNetworks.WinForms
{
    partial class ActivationFunctionControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartGradientFunction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            symmericSplitContainer1 = new SymmericSplitContainer();
            chartActivationFunction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)chartGradientFunction).BeginInit();
            ((System.ComponentModel.ISupportInitialize)symmericSplitContainer1).BeginInit();
            symmericSplitContainer1.Panel1.SuspendLayout();
            symmericSplitContainer1.Panel2.SuspendLayout();
            symmericSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartActivationFunction).BeginInit();
            SuspendLayout();
            // 
            // chartGradientFunction
            // 
            chartArea1.Name = "ChartArea1";
            chartGradientFunction.ChartAreas.Add(chartArea1);
            chartGradientFunction.Dock = DockStyle.Fill;
            legend1.Alignment = StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Gradient function";
            chartGradientFunction.Legends.Add(legend1);
            chartGradientFunction.Location = new Point(0, 0);
            chartGradientFunction.Name = "chartGradientFunction";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Gradient function";
            series1.Name = "Gradient function";
            chartGradientFunction.Series.Add(series1);
            chartGradientFunction.Size = new Size(377, 379);
            chartGradientFunction.TabIndex = 1;
            chartGradientFunction.Text = "chart2";
            // 
            // symmericSplitContainer1
            // 
            symmericSplitContainer1.Dock = DockStyle.Fill;
            symmericSplitContainer1.Location = new Point(0, 0);
            symmericSplitContainer1.Name = "symmericSplitContainer1";
            // 
            // symmericSplitContainer1.Panel1
            // 
            symmericSplitContainer1.Panel1.Controls.Add(chartActivationFunction);
            // 
            // symmericSplitContainer1.Panel2
            // 
            symmericSplitContainer1.Panel2.Controls.Add(chartGradientFunction);
            symmericSplitContainer1.Size = new Size(762, 379);
            symmericSplitContainer1.SplitterDistance = 381;
            symmericSplitContainer1.TabIndex = 2;
            // 
            // chartActivationFunction
            // 
            chartArea2.Name = "ChartArea1";
            chartActivationFunction.ChartAreas.Add(chartArea2);
            chartActivationFunction.Dock = DockStyle.Fill;
            legend2.Alignment = StringAlignment.Center;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            chartActivationFunction.Legends.Add(legend2);
            chartActivationFunction.Location = new Point(0, 0);
            chartActivationFunction.Name = "chartActivationFunction";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Activation function";
            chartActivationFunction.Series.Add(series2);
            chartActivationFunction.Size = new Size(381, 379);
            chartActivationFunction.TabIndex = 0;
            chartActivationFunction.Text = "chart1";
            // 
            // ActivationFunctionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(symmericSplitContainer1);
            Name = "ActivationFunctionControl";
            Size = new Size(762, 379);
            ((System.ComponentModel.ISupportInitialize)chartGradientFunction).EndInit();
            symmericSplitContainer1.Panel1.ResumeLayout(false);
            symmericSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)symmericSplitContainer1).EndInit();
            symmericSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartActivationFunction).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGradientFunction;
        private SymmericSplitContainer symmericSplitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartActivationFunction;
    }
}
