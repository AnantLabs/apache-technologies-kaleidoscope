using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Apache.ImageLib;

namespace Kaleidoscope
{
    public partial class CFormFilterDlg : Form
    {
        CConsole m_frmConsole = null;

        public CFormFilterDlg(CConsole c, string strCaption)
        {
            InitializeComponent();
            m_frmConsole = c;

            radPass.Checked = true;
            XLow = tbXLow.Value = 50;
            XHigh = tbXHigh.Value = 100;
            YLow = tbYLow.Value = 65;
            YHigh = tbYHigh.Value = 75;
        }

        #region PROPERTIES

        int m_nBandType = 0;
        public int BandType
        {
            get { return m_nBandType; }
        }

        public int XLow
        {
            set { txtXLow.Text = value.ToString(); }
            get { return Convert.ToInt32(txtXLow.Text); }
        }

        public int XHigh
        {
            set { txtXHigh.Text = value.ToString(); }
            get { return Convert.ToInt32(txtXHigh.Text); }
        }

        public int YLow
        {
            set { txtYLow.Text = value.ToString(); }
            get { return Convert.ToInt32(txtYLow.Text); }
        }

        public int YHigh
        {
            set { txtYHigh.Text = value.ToString(); }
            get { return Convert.ToInt32(txtYHigh.Text); }
        }

        #endregion

        private void txtAlphaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }
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
    }
}
