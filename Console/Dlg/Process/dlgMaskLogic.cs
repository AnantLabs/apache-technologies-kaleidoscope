using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CMaskLogicDlg : Form
    {
        CConsole m_frmConsole = null;

        public CMaskLogicDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            radAnd.Checked = true;
            tbSliderMask.Value = 60;

            lblMaskValue.Text = tbSliderMask.Value.ToString();
        }

        private void tbSliderMask_Scroll(object sender, EventArgs e)
        {
            lblMaskValue.Text = String.Format("{0:X}", tbSliderMask.Value.ToString());
        }

        private void radAnd_CheckedChanged(object sender, EventArgs e)
        {
            m_byType = 0;
        }

        private void radOr_CheckedChanged(object sender, EventArgs e)
        {
            m_byType = 1;
        }

        private void radXor_CheckedChanged(object sender, EventArgs e)
        {
            m_byType = 2;
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

        public byte MaskValue
        {
            get { return Convert.ToByte(tbSliderMask.Value); }
        }

        byte m_byType = 0;
        public byte MaskType
        {
            get 
            {
                return m_byType; 
            }
        }

        #endregion
    }
}
