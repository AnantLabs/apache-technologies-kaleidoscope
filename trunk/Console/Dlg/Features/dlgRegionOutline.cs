using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CRegionOutlineDlg : Form
    {
        CConsole m_frmConsole = null;

        public CRegionOutlineDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbObjectNum.Value = 1;
            txtObjectNum.Text = tbObjectNum.Value.ToString();
        }

        private void tbObjectNum_Scroll(object sender, EventArgs e)
        {
            txtObjectNum.Text = String.Format("{0:X}", tbObjectNum.Value.ToString());
        }

        private void txtObjectNum_TextChanged(object sender, EventArgs e)
        {
            tbObjectNum.Value = Convert.ToInt32(txtObjectNum.Text);
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

        public byte ObjectNum
        {
            get { return Convert.ToByte(tbObjectNum.Value); }
        }

        #endregion
    }
}
