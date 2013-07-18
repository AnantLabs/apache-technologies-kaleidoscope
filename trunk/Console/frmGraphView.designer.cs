namespace Kaleidoscope
{
    partial class CView
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CView));
            this.scView = new System.Windows.Forms.SplitContainer();
            this.chartSample = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grpCharacteristics = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.scView)).BeginInit();
            this.scView.Panel1.SuspendLayout();
            this.scView.Panel2.SuspendLayout();
            this.scView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSample)).BeginInit();
            this.SuspendLayout();
            // 
            // scView
            // 
            this.scView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scView.Location = new System.Drawing.Point(0, 0);
            this.scView.Name = "scView";
            this.scView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scView.Panel1
            // 
            this.scView.Panel1.Controls.Add(this.chartSample);
            // 
            // scView.Panel2
            // 
            this.scView.Panel2.Controls.Add(this.grpCharacteristics);
            this.scView.Size = new System.Drawing.Size(808, 411);
            this.scView.SplitterDistance = 269;
            this.scView.TabIndex = 0;
            // 
            // chartSample
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSample.ChartAreas.Add(chartArea1);
            this.chartSample.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartSample.Legends.Add(legend1);
            this.chartSample.Location = new System.Drawing.Point(0, 0);
            this.chartSample.Name = "chartSample";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartSample.Series.Add(series1);
            this.chartSample.Size = new System.Drawing.Size(808, 269);
            this.chartSample.TabIndex = 0;
            this.chartSample.Text = "Samples";
            // 
            // grpCharacteristics
            // 
            this.grpCharacteristics.BackColor = System.Drawing.Color.Transparent;
            this.grpCharacteristics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCharacteristics.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCharacteristics.Location = new System.Drawing.Point(0, 0);
            this.grpCharacteristics.Name = "grpCharacteristics";
            this.grpCharacteristics.Size = new System.Drawing.Size(808, 138);
            this.grpCharacteristics.TabIndex = 0;
            this.grpCharacteristics.TabStop = false;
            this.grpCharacteristics.Text = "Characteristics";
            // 
            // CView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(808, 411);
            this.Controls.Add(this.scView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Graph";
            this.Load += new System.EventHandler(this.CView_Load);
            this.scView.Panel1.ResumeLayout(false);
            this.scView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scView)).EndInit();
            this.scView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scView;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSample;
        private System.Windows.Forms.GroupBox grpCharacteristics;
    }
}