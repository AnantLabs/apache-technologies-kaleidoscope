using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CUniformPseudoColor : Form
    {
        CConsole m_frmConsole = null;

        public CUniformPseudoColor(CConsole c, string strTitle)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbIntensity.Value = 2;
            tbSaturation.Value = 100;
            lblC.Text = tbIntensity.Value.ToString();
            lblB.Text = tbSaturation.Value.ToString();
        }

        private void tbContrast_Scroll(object sender, EventArgs e)
        {
            lblC.Text = tbIntensity.Value.ToString();
        }

        private void tbBrightness_Scroll(object sender, EventArgs e)
        {
            lblB.Text = tbSaturation.Value.ToString();
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

        public int Intensity
        {
            get { return tbIntensity.Value; }
        }

        public int Saturation
        {
            get { return tbSaturation.Value; }
        }

        private int m_nFlag;
        public int Flag
        {
            get { return m_nFlag; }
        }
    }
}
