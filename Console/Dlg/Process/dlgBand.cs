using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CBandDlg : Form
    {
        CConsole m_frmConsole = null;

        public CBandDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            radStop.Checked = true;
            tbLowerT.Value = 60;
            tbUpperT.Value = 180;
            tbR.Value = 128;
            tbG.Value = 128;
            tbB.Value = 128;

            lblLower.Text = tbLowerT.Value.ToString();
            lblUpper.Text = tbUpperT.Value.ToString();
        }

        private void tbLowerT_Scroll(object sender, EventArgs e)
        {
            lblLower.Text = tbLowerT.Value.ToString();
        }

        private void tbUpperT_Scroll(object sender, EventArgs e)
        {
            lblUpper.Text = tbUpperT.Value.ToString();
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

        public byte LowerT
        {
            get { return Convert.ToByte(tbLowerT.Value); }
        }

        public byte UpperT
        {
            get { return Convert.ToByte(tbUpperT.Value); }
        }

        public byte R
        {
            get { return Convert.ToByte(tbR.Value); }
        }

        public byte G
        {
            get { return Convert.ToByte(tbG.Value); }
        }

        public byte B
        {
            get { return Convert.ToByte(tbB.Value); }
        }

        public byte Type
        {
            get 
            {
                byte byType = 1;
                if (radPass.Checked) byType = 0;
                return byType; 
            }
        }

        #endregion
    }
}
