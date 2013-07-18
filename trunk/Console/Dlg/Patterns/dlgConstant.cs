using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CConstantDlg : Form
    {
        CConsole m_frmConsole = null;

        public CConstantDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            txtWidth.Text = "256";
            txtHeight.Text = "256";

            rad8Bits.Checked = true;

            tbIntensity.Value = 128;
            lblIntensity.Text = tbIntensity.Value.ToString();
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
                e.KeyChar != '.' && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '_')
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

        private void tbIntensity_Scroll(object sender, EventArgs e)
        {
            lblIntensity.Text = tbIntensity.Value.ToString();
        }

        private void rad1bit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rad4Bits_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rad8Bits_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rad24Bits_CheckedChanged(object sender, EventArgs e)
        {

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

        #region PROPERTIES

        public byte Intensity
        {
            get { return Convert.ToByte(tbIntensity.Value); }
        }

        public byte Bits
        {
            get { return 8; }
        }

        public int ImageWidth
        {
            get { return Convert.ToInt32(txtWidth.Text); }
        }

        public int ImageHeight
        {
            get { return Convert.ToInt32(txtHeight.Text); }
        }

        #endregion
    }
}
