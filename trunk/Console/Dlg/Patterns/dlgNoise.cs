using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CNoiseDlg : Form
    {
        CConsole m_frmConsole = null;

        public CNoiseDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            radSparkle.Checked = true;
            txtAmplitude.Text = "200";
            txtErrorProbability.Text = "0.01";
        }

        private void txtAlphaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void radRandom_CheckedChanged(object sender, EventArgs e)
        {
            m_nNoiseType = 0;
            txtErrorProbability.Enabled = false;
        }

        private void radSparkle_CheckedChanged(object sender, EventArgs e)
        {
            m_nNoiseType = 1;
            txtErrorProbability.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            m_nAmp = Convert.ToInt32(txtAmplitude.Text);
            if (m_nAmp < 0 || m_nAmp > 255)
            {
                MessageBox.Show("Please enter the Amplitude between 0 and 255",
                    Program.cAPP_NAME);
                return;
            }
            m_dErrorProb = Convert.ToDouble(txtErrorProbability.Text);
            if (m_nNoiseType == 1)
            {
                if (m_dErrorProb <= 0 || m_dErrorProb >= 1.0)
                {
                    MessageBox.Show("Please enter the Error Probability between 0 and 1",
                        Program.cAPP_NAME);
                    return;
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        int m_nNoiseType = 0;
        public int NoiseType
        {
            get { return m_nNoiseType; }
        }

        int m_nAmp = 0;
        public int Amplitude
        {
            get { return m_nAmp; }
        }

        double m_dErrorProb = 0.01;
        public double ErrorProb
        {
            get { return m_dErrorProb; }
        }
    }
}
