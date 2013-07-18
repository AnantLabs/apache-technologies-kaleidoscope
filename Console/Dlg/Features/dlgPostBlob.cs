using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CBlobPostDlg : Form
    {
        CConsole m_frmConsole = null;

        public CBlobPostDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;
        }

        public double Density
        {
            get { return Convert.ToDouble(txtDensity.Text); }
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
