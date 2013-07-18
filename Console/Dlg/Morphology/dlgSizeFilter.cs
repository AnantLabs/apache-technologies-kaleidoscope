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
    public partial class CSizeFilterDlg : Form
    {
        CConsole m_frmConsole = null;

        public CSizeFilterDlg(CConsole c, string strCaption, int nImgSz)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbLowerT.Minimum = tbHigherT.Minimum = 0;
            tbLowerT.Maximum = tbHigherT.Maximum = nImgSz;
            tbLowerT.TickFrequency = tbHigherT.TickFrequency = nImgSz / 10;

            radPass.Checked = true;
            LowT = tbLowerT.Value = nImgSz / 4;
            HighT = tbHigherT.Value = 3 * nImgSz / 4;
        }

        #region PROPERTIES

        int m_nBandType = 0;
        public int BandType
        {
            get { return m_nBandType; }
        }

        public int LowT
        {
            set { txtLowT.Text = value.ToString(); }
            get { return Convert.ToInt32(txtLowT.Text); }
        }

        public int HighT
        {
            set { txtHighT.Text = value.ToString(); }
            get { return Convert.ToInt32(txtHighT.Text); }
        }

        #endregion

        #region EVENTS

        private void txtAlphaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void radRandom_CheckedChanged(object sender, EventArgs e)
        {
            m_nBandType = 0;
        }

        private void radSparkle_CheckedChanged(object sender, EventArgs e)
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
    }
}
