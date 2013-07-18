using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CGlobalThresholdDlg : Form
    {
        CConsole m_frmConsole = null;

        public CGlobalThresholdDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            radThreshold.Checked = true;
            tbThreshold.Value = 127;
            tbPercentage.Value = 50;
            lblThreshold.Text = tbThreshold.Value.ToString();
            lblPercent.Text = tbPercentage.Value.ToString();
        }

        private void radThreshold_CheckedChanged(object sender, EventArgs e)
        {
            tbThreshold.Enabled = true;
            tbPercentage.Enabled = false;
        }

        private void radPercent_CheckedChanged(object sender, EventArgs e)
        {
            tbThreshold.Enabled = false;
            tbPercentage.Enabled = true;
        }

        private void tbThreashold_Scroll(object sender, EventArgs e)
        {
            lblThreshold.Text = tbThreshold.Value.ToString();
        }

        private void tbPercent_Scroll(object sender, EventArgs e)
        {
            lblPercent.Text = tbPercentage.Value.ToString();
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

        public int Threshold
        {
            get { return radThreshold.Checked ? tbThreshold.Value : 0; }
        }

        public int Percent
        {
            get { return radPercent.Checked ? tbPercentage.Value : 0; }
        }
    }
}
