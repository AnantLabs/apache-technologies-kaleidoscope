using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CLinearDlg : Form
    {
        CConsole m_frmConsole = null;

        public CLinearDlg(CConsole c, string strTitle)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbContrast.Value = 2;
            tbBrightness.Value = 100;
            lblC.Text = tbContrast.Value.ToString();
            lblB.Text = tbBrightness.Value.ToString();
        }

        private void tbContrast_Scroll(object sender, EventArgs e)
        {
            lblC.Text = tbContrast.Value.ToString();
        }

        private void tbBrightness_Scroll(object sender, EventArgs e)
        {
            lblB.Text = tbBrightness.Value.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (Brightness == 0 && Contrast >= 1)
                m_nFlag = 1;
            else if (Contrast == 1 && Brightness > 0)
                m_nFlag = 0;
            else if (Contrast >= 1 && Brightness > 0)
                m_nFlag = 2;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public int Contrast
        {
            get { return tbContrast.Value; }
        }

        public int Brightness
        {
            get { return tbBrightness.Value; }
        }

        private int m_nFlag;
        public int Flag
        {
            get { return m_nFlag; }
        }
    }
}
