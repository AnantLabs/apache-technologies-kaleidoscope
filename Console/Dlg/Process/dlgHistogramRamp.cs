using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CHistogramRampDlg : Form
    {
        CConsole m_frmConsole = null;

        public CHistogramRampDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;
            radDark.Checked = true;
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

        int m_nSlopeType = 0;
        public int SlopeType
        {
            get 
            {
                return m_nSlopeType;
            }
        }

        private void radDark_CheckedChanged(object sender, EventArgs e)
        {
            m_nSlopeType = -1;
        }

        private void radMedium_CheckedChanged(object sender, EventArgs e)
        {
            m_nSlopeType = 0;
        }

        private void radLight_CheckedChanged(object sender, EventArgs e)
        {
            m_nSlopeType = 1;
        }
    }
}
