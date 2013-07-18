using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CIntensityDlg : Form
    {
        CConsole m_frmConsole = null;

        public CIntensityDlg(CConsole c, string strTitle)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbR.Value = 255;
            tbG.Value = 255;
            tbB.Value = 255;
            lblR.Text = tbR.Value.ToString();
            lblG.Text = tbG.Value.ToString();
            lblB.Text = tbB.Value.ToString();
        }

        private void tbR_Scroll(object sender, EventArgs e)
        {
            lblR.Text = tbR.Value.ToString();
        }

        private void tbG_Scroll(object sender, EventArgs e)
        {
            lblG.Text = tbG.Value.ToString();
        }

        private void tbB_Scroll(object sender, EventArgs e)
        {
            lblB.Text = tbB.Value.ToString();
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

        public int R
        {
            get { return tbR.Value; }
        }

        public int G
        {
            get { return tbG.Value; }
        }

        public int B
        {
            get { return tbB.Value; }
        }
    }
}
