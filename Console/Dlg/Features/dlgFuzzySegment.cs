using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CFuzzySegmentDlg : Form
    {
        CConsole m_frmConsole = null;

        public CFuzzySegmentDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            Histogram = tbHistogram.Value = 7;
            LowT = tbLowT.Value = 2;
            HighT = tbHighT.Value = 8;
            ZeroCrossing =  tbZeroCrossing.Value = 1200;
        }

        private void tbHistogram_Scroll(object sender, EventArgs e)
        {
            Histogram = tbHistogram.Value;
        }

        private void tbLowT_Scroll(object sender, EventArgs e)
        {
            LowT = tbLowT.Value;
        }

        private void tbHighT_Scroll(object sender, EventArgs e)
        {
            HighT = tbHighT.Value;
        }

        private void tbZeroCrossing_Scroll(object sender, EventArgs e)
        {
            ZeroCrossing = tbZeroCrossing.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region PROPERTIES

        public int Histogram
        {
            get { return Convert.ToInt32(tbHistogram.Value); }
            set { lblHistogram.Text = String.Format("{0}", value); }
        }

        public int LowT
        {
            get { return Convert.ToInt32(tbLowT.Value); }
            set { lblLowT.Text = String.Format("{0:0.0}", value / 10.0); }
        }

        public int HighT
        {
            get { return Convert.ToByte(tbHighT.Value); }
            set { lblHighT.Text = String.Format("{0:0.0}", value / 10.0); }
        }

        public int ZeroCrossing
        {
            get { return Convert.ToByte(tbZeroCrossing.Value); }
            set { lblZeroCrossing.Text = String.Format("{0}", value); }
        }

        #endregion
    }
}
