using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CMomentsDlg : Form
    {
        CConsole m_frmConsole = null;

        public CMomentsDlg(CConsole c, string strCaption, int nLeft, int nTop, int nRight, int nBottom)
        {
            InitializeComponent();
            m_frmConsole = c;
            this.Text = strCaption;
            numLeft.Value = nLeft;
            numTop.Value = nTop;
            numRight.Value = nRight;
            numBottom.Value = nBottom;
        }

        public int Left
        {
            get { return Convert.ToInt32(numLeft.Value); }
        }

        public int Top
        {
            get { return Convert.ToInt32(numTop.Value); }
        }

        public int Right
        {
            get { return Convert.ToInt32(numRight.Value); }
        }

        public int Bottom
        {
            get { return Convert.ToInt32(numBottom.Value); }
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
    }
}
