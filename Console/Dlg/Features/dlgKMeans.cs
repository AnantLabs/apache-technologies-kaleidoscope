using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CKMeansDlg : Form
    {
        CConsole m_frmConsole = null;

        public CKMeansDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbSegments.Value = 5;
            tbVectorType.Value = 3;
            lblSegments.Text = tbSegments.Value.ToString();
            lblVectorType.Text = tbVectorType.Value.ToString();
        }

        private void tbContrast_Scroll(object sender, EventArgs e)
        {
            lblSegments.Text = tbSegments.Value.ToString();
        }

        private void tbBrightness_Scroll(object sender, EventArgs e)
        {
            lblVectorType.Text = tbVectorType.Value.ToString();
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

        public int Segments
        {
            get { return tbSegments.Value; }
        }

        public int VectorType
        {
            get { return tbVectorType.Value; }
        }
    }
}
