using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CPerimeterFilterDlg : Form
    {
        CConsole m_frmConsole = null;

        public CPerimeterFilterDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            radPass.Checked = true;
            XScale = 1;
            YScale = 1;
            PerimeterLow = 100;
            PerimeterHigh = 200;
        }

        private void radPass_CheckedChanged(object sender, EventArgs e)
        {
            m_nBandType = 0;
        }

        private void radStop_CheckedChanged(object sender, EventArgs e)
        {
            m_nBandType = 1;
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

        int m_nBandType = 0;
        public int BandType
        {
            get { return m_nBandType; }
        }

        public double XScale
        {
            set { txtXScale.Text = value.ToString(); }
            get { return Convert.ToDouble(txtXScale.Text); }
        }

        public double YScale
        {
            set { txtYScale.Text = value.ToString(); }
            get { return Convert.ToDouble(txtYScale.Text); }
        }

        public int PerimeterLow
        {
            set { txtPerimeterLow.Text = value.ToString(); }
            get { return Convert.ToByte(txtPerimeterLow.Text); }
        }

        public int PerimeterHigh
        {
            set { txtPerimeterHigh.Text = value.ToString(); }
            get { return Convert.ToByte(txtPerimeterHigh.Text); }
        }

        #endregion
    }
}
