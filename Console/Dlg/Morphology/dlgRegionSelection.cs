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
    public partial class CRegionSelectionDlg : Form
    {
        CConsole m_frmConsole = null;

        public CRegionSelectionDlg(CConsole c, string strCaption)
        {
            InitializeComponent();
            m_frmConsole = c;
            this.Text = strCaption;
            radBlack.Checked = true;
        }

        private void txtAlphaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void radBlack_CheckedChanged(object sender, EventArgs e)
        {
            m_nRegionType = 0;
        }

        private void radWhite_CheckedChanged(object sender, EventArgs e)
        {
            m_nRegionType = 1;
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

        int m_nRegionType = 0;
        public CDefinitions.REGION_TYPE RegionType
        {
            get { return (CDefinitions.REGION_TYPE)m_nRegionType; }
        }
    }
}
