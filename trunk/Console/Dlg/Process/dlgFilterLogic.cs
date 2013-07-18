using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CFilterLogicDlg : Form
    {
        CConsole m_frmConsole = null;

        public CFilterLogicDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            radOne.Checked = true;
        }

        private void radOne_CheckedChanged(object sender, EventArgs e)
        {
            m_byType = 0;
        }

        private void radTwo_CheckedChanged(object sender, EventArgs e)
        {
            m_byType = 1;
        }

        private void radThree_CheckedChanged(object sender, EventArgs e)
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
