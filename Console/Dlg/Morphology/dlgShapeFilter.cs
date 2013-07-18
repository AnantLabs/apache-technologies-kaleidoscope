using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CShapeFilterDlg : Form
    {
        CConsole m_frmConsole = null;

        public CShapeFilterDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            XScale = YScale = 1.0;
            ShapeLow = 0.05;
            ShapeHigh = 0.07;
            radPass.Checked = true;
        }

        #region EVENTS

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

        #endregion

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

        public double ShapeLow
        {
            set { txtShapeLow.Text = value.ToString(); }
            get { return Convert.ToDouble(txtShapeLow.Text); }
        }

        public double ShapeHigh
        {
            set { txtShapeHigh.Text = value.ToString(); }
            get { return Convert.ToDouble(txtShapeHigh.Text); }
        }

        #endregion
    }
}
