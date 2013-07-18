using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CResizeDlg : Form
    {
        CConsole m_frmConsole = null;

        public CResizeDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;
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
