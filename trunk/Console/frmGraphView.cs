using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Kaleidoscope
{
    public partial class CView : Form
    {
        #region VARIABLES

        private int[] m_vHistogram = null;
        private const int GRAPH_SIZE = 512;

        #endregion

        public CView(ref int[] vHistogram)
        {
            try
            {
                InitializeComponent();
                grpCharacteristics.ForeColor = Color.White;

                m_vHistogram = vHistogram;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }
        }

        private void CView_Load(object sender, EventArgs e)
        {
            try
            {
                ListView lv = new ListView();
                grpCharacteristics.Controls.Add(lv);

                lv.Dock = DockStyle.Fill;
                lv.View = View.Details;
                lv.GridLines = true;
                lv.FullRowSelect = true;
                lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                lv.Columns.Add("Property", 200);
                lv.Columns.Add("Value", 150);

                //lv.BorderStyle = Border3DStyle.Raised;
                string[] arr = new string[2];
                ListViewItem row;

                //arr[0] = "Sample Length";
                //arr[1] = m_objWaveInfo.Length.ToString();
                //row = new ListViewItem(arr);
                //lv.Items.Add(row);

                //arr[0] = "Sampling Frequency (Hz)";
                //arr[1] = m_objWaveInfo.SamplingFrequency.ToString();
                //row = new ListViewItem(arr);
                //lv.Items.Add(row);

                //arr[0] = "Mininum Amplitude";
                //arr[1] = String.Format("{0:0.000}", m_objWaveInfo.MinAmpl);
                //row = new ListViewItem(arr);
                //lv.Items.Add(row);

                //arr[0] = "Maximum Amplitude";
                //arr[1] = String.Format("{0:0.000}", m_objWaveInfo.MaxAmpl);
                //row = new ListViewItem(arr);
                //lv.Items.Add(row);

                //arr[0] = "X Uint";
                //arr[1] = m_objWaveInfo.XUnit;
                //row = new ListViewItem(arr);
                //lv.Items.Add(row);

                //arr[0] = "Y Uint";
                //arr[1] = m_objWaveInfo.YUnit;
                //row = new ListViewItem(arr);
                //lv.Items.Add(row);

                // clear chart series
                chartSample.Series.Clear();
                var series = chartSample.Series.Add("Graph");
                series.ChartType = SeriesChartType.FastLine;
                for (int cnt = 0; cnt < m_vHistogram.Length; cnt++)
                {
                    series.Points.AddXY(cnt + 1, m_vHistogram[cnt]);
                }

                var chartArea = chartSample.ChartAreas[series.ChartArea];
                chartArea.CursorX.AutoScroll = true;

                chartArea.AxisX.ScaleView.Zoomable = true;
                chartArea.AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
                chartArea.AxisX.ScaleView.Zoom(0, GRAPH_SIZE);

                chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
                chartArea.AxisX.ScaleView.SmallScrollSize = GRAPH_SIZE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
