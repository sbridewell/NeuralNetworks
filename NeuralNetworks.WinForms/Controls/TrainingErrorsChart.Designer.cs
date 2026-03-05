namespace Sde.NeuralNetworks.WinForms.Controls
{
    partial class TrainingErrorsChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            errorsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)errorsChart).BeginInit();
            SuspendLayout();
            // 
            // errorsChart
            // 
            chartArea2.Name = "ChartArea1";
            errorsChart.ChartAreas.Add(chartArea2);
            errorsChart.Dock = DockStyle.Fill;
            legend2.Name = "Legend1";
            errorsChart.Legends.Add(legend2);
            errorsChart.Location = new Point(0, 0);
            errorsChart.Name = "errorsChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            errorsChart.Series.Add(series2);
            errorsChart.Size = new Size(893, 496);
            errorsChart.TabIndex = 0;
            errorsChart.Text = "chart1";
            // 
            // TrainingErrorsChart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(errorsChart);
            Name = "TrainingErrorsChart";
            Size = new Size(893, 496);
            ((System.ComponentModel.ISupportInitialize)errorsChart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart errorsChart;
    }
}
