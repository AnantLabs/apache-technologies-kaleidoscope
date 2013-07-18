using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CChequerDlg : Form
    {
        CConsole m_frmConsole = null;

        public CChequerDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            txtWidth.Text = "256";
            txtHeight.Text = "256";
            txtChequerWidth.Text = "32";
            txtChequerHeight.Text = "32";

            tbSlider1.Value = 0;
            lblOne.Text = tbSlider1.Value.ToString();
            tbSlider2.Value = 255;
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

        private void tbSlider1_Scroll(object sender, EventArgs e)
        {
            lblOne.Text = tbSlider1.Value.ToString();
        }

        private void tbSlider2_Scroll(object sender, EventArgs e)
        {
            lblTwo.Text = tbSlider2.Value.ToString();
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

        public byte LowerT
        {
            get { return Convert.ToByte(tbSlider1.Value); }
        }

        public byte UpperT
        {
            get { return Convert.ToByte(tbSlider2.Value); }
        }

        public int ChequerWidth
        {
            get { return Convert.ToInt32(txtChequerWidth.Text); }
        }

        public int ChequerHeight
        {
            get { return Convert.ToInt32(txtChequerHeight.Text); }
        }

        public int ImageWidth
        {
            get { return Convert.ToInt32(txtWidth.Text); }
        }

        public int ImageHeight
        {
            get { return Convert.ToInt32(txtHeight.Text); }
        }
    }
}
