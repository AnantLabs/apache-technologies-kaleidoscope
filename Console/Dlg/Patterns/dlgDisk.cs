using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CDiskDlg : Form
    {
        CConsole m_frmConsole = null;

        public CDiskDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            txtWidth.Text = "256";
            txtHeight.Text = "256";
            txtDiskXCenter.Text = "128";
            txtDiskYCenter.Text = "128";
            txtRadius.Text = "255";

            tbIntensity.Value = 255;
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

        public int ImageWidth
        {
            get { return Convert.ToInt32(txtWidth.Text); }
        }

        public int ImageHeight
        {
            get { return Convert.ToInt32(txtHeight.Text); }
        }

        public int DiskXCenter
        {
            get { return Convert.ToInt32(txtDiskXCenter.Text); }
        }

        public int DiskYCenter
        {
            get { return Convert.ToInt32(txtDiskYCenter.Text); }
        }

        public int DiskRadius
        {
            get { return Convert.ToInt32(txtRadius.Text); }
        }

        #endregion
    }
}
