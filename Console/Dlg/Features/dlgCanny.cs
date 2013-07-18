using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CCannyDlg : Form
    {
        CConsole m_frmConsole = null;

        public CCannyDlg(CConsole c, string strCaption)
        {
            InitializeComponent();
            m_frmConsole = c;

            this.Text = strCaption;

            tbLowerT.Value = 3;
            lblLower.Text = String.Format("{0:0.0}", tbLowerT.Value / 10.0);
            tbUpperT.Value = 5;
            lblUpper.Text = String.Format("{0:0.0}", tbUpperT.Value / 10.0);
            tbSigma.Value = 3;
            lblSigma.Text = tbSigma.Value.ToString();
        }

        private void tbLowerT_Scroll(object sender, EventArgs e)
        {
            lblLower.Text = String.Format("{0:0.0}", tbLowerT.Value / 10.0);
        }

        private void tbUpperT_Scroll(object sender, EventArgs e)
        {
            lblUpper.Text = String.Format("{0:0.0}", tbUpperT.Value / 10.0);
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

        public byte Sigma
        {
            get { return Convert.ToByte(tbSigma.Value); }
        }

        #endregion
    }
}
