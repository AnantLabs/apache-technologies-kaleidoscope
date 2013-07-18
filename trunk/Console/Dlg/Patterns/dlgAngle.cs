using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CAngleDlg : Form
    {
        CConsole m_frmConsole = null;

        public CAngleDlg(CConsole c, string strTitle)
        {
            m_frmConsole = c;
            InitializeComponent();
            this.Text = strTitle;
        }

        public int Angle
        {
            get { return Convert.ToInt32(numUDAngle.Value); }
        }

        private void numUDAngle_ValueChanged(object sender, EventArgs e)
        {
            //m_frmConsole.RotateImage(Angle);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            numUDAngle.Value = 0;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
