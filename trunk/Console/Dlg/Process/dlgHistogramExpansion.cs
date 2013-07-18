using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CHistogramExpansionDlg : Form
    {
        CConsole m_frmConsole = null;

        public CHistogramExpansionDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbSlider1.Value = 20;
            tbSlider2.Value = 100;
            lblOne.Text = tbSlider1.Value.ToString();
            lblTwo.Text = tbSlider2.Value.ToString();
        }

        public byte CenterIntensity
        {
            get { return Convert.ToByte(tbSlider1.Value); }
        }

        public int Slope
        {
            get { return Convert.ToInt32(tbSlider2.Value); }
        }

        private void tbSlider1_Scroll(object sender, EventArgs e)
        {
            lblOne.Text = tbSlider1.Value.ToString();
        }

        private void tbSlider2_Scroll(object sender, EventArgs e)
        {
            lblTwo.Text = tbSlider2.Value.ToString();
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
