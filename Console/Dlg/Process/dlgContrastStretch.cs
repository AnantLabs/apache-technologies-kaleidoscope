using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CContrastStretchDlg : Form
    {
        CConsole m_frmConsole = null;

        public CContrastStretchDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbLowerT.Value = 85;
            lblLower.Text = tbLowerT.Value.ToString();
            tbUpperT.Value = 175;
            lblUpper.Text = tbUpperT.Value.ToString();
            tbAlpha.Value = 2;
            lblAlpha.Text = tbAlpha.Value.ToString();
            tbBeta.Value = 2;
            lblBeta.Text = tbBeta.Value.ToString();
            tbGamma.Value = 2;
            lblGamma.Text = tbGamma.Value.ToString();
        }

        private void tbLowerT_Scroll(object sender, EventArgs e)
        {
            lblLower.Text = tbLowerT.Value.ToString();
        }

        private void tbUpperT_Scroll(object sender, EventArgs e)
        {
            lblUpper.Text = tbUpperT.Value.ToString();
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

        public byte LowerT
        {
            get { return Convert.ToByte(tbLowerT.Value); }
        }

        public byte UpperT
        {
            get { return Convert.ToByte(tbUpperT.Value); }
        }

        public byte Alpha
        {
            get { return Convert.ToByte(tbAlpha.Value); }
        }

        public byte Beta
        {
            get { return Convert.ToByte(tbBeta.Value); }
        }

        public byte Gamma
        {
            get { return Convert.ToByte(tbGamma.Value); }
        }

        #endregion
    }
}
