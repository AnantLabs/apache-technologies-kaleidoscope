using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CSineDlg : Form
    {
        CConsole m_frmConsole = null;

        public CSineDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            txtWidth.Text = "256";
            txtHeight.Text = "256";

            txtHorizontalFreq.Text = "0.1";
            txtVerticalFreq.Text = "0.1";

            tbSlider1.Value = 127;
            lblOne.Text = tbSlider1.Value.ToString();
            tbSlider2.Value = 128;
            lblTwo.Text = tbSlider2.Value.ToString();

            chkHoriFreq.Checked = true;
        }

        #region CHECK_EVENTS

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAlphaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCurrency_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        #endregion

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
            if (chkHoriFreq.Checked)
            {
                if (m_dHorizontalFreq > 1 || m_dHorizontalFreq < 1.0 / (double)(ImageWidth / 2))
                {
                    MessageBox.Show("Please check the horzontal sampling frequencies.", 
                        Program.cAPP_NAME);
                    return;
                }
            }
            else
                m_dHorizontalFreq = 0;

            if (chkVertFreq.Checked)
            {
                if (m_dVerticalFreq > 1 || m_dVerticalFreq < 1.0 / (double)(ImageHeight / 2))
                {
                    MessageBox.Show("Please check the vertical sampling frequencies.",
                        Program.cAPP_NAME);
                    return;
                }
            }
            else
                m_dVerticalFreq = 0;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public byte PeakAmp
        {
            get { return Convert.ToByte(tbSlider1.Value); }
        }

        public byte Offset
        {
            get { return Convert.ToByte(tbSlider2.Value); }
        }

        public int ImageWidth
        {
            get { return Convert.ToInt32(txtWidth.Text); }
        }

        public int ImageHeight
        {
            get { return Convert.ToInt32(txtHeight.Text); }
        }

        private double m_dHorizontalFreq = 0.1;
        public double HorzFreq
        {
            get { return m_dHorizontalFreq;  }
        }

        private double m_dVerticalFreq = 0.1;
        public double VertFreq
        {
            get { return m_dVerticalFreq; }
        }
    }
}
