using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CMosaicDlg : Form
    {
        CConsole m_frmConsole = null;

        public CMosaicDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;
            numUDXScale.Value = 3;
            numUDYScale.Value = 3;
        }

        public byte XScale
        {
            get { return Convert.ToByte(numUDXScale.Value); }
        }

        public byte YScale
        {
            get { return Convert.ToByte(numUDXScale.Value); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            numUDXScale.Value = 0;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
