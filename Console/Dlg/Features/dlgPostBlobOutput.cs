using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Apache.ImageLib;

namespace Kaleidoscope
{
    public partial class CBlobPostResultDlg : Form
    {
        List<CBlobAnalysis.OBJECT3DGEOMETRY> m_lRecords = null;

        public CBlobPostResultDlg(ref List<CBlobAnalysis.OBJECT3DGEOMETRY> lRecords)
        {
            InitializeComponent();
            m_lRecords = lRecords;
        }

        private void CBlobPostResultDlg_Load(object sender, EventArgs e)
        {
            try
            {
                dgvBlobPostResult.AutoSize = true;
                dgvBlobPostResult.DefaultCellStyle.ForeColor = Color.Black;
                dgvBlobPostResult.DataSource = m_lRecords;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private void insertData(int aRow, int aCol, double aData)
        {
            //CString str;
            //str.Format("%.3f", aData);
            //m_listRes.SetItemText(aRow, aCol, str);
        }
    }
}
