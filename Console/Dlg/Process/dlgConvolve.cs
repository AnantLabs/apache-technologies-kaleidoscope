using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CConvolveDlg : Form
    {
        CConsole m_frmConsole = null;
        DataTable m_dtPoints = null;

        public CConvolveDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            txtDivisor.Text = "9";
            txtMultiplier.Text = "1";

            m_dtPoints = new DataTable();
            m_dtPoints.Columns.Add("1");
            m_dtPoints.Columns.Add("2");
            m_dtPoints.Columns.Add("3");

            m_dtPoints.Rows.Add(0, 1, 3);
            m_dtPoints.Rows.Add(4, 5, 6);
            m_dtPoints.Rows.Add(7, 8, 9);

            dgMask.ColumnHeadersVisible = false;
            dgMask.RowHeadersVisible = false;

            dgMask.DataSource = m_dtPoints;
        }

        private void txtMaskSize_TextChanged(object sender, EventArgs e)
        {
            int nMaskSize = Convert.ToByte(txtMaskSize.Text);
            if (nMaskSize < 2 || 0 == nMaskSize%2)
            {
                MessageBox.Show("Mask size should be between 2 and 9 and it should be odd.",
                    Program.cAPP_NAME);
                return;
            }

            dgMask.DataSource = null;
            m_dtPoints.Clear();
            m_dtPoints.Columns.Clear();

            for (int i = 0; i < nMaskSize; i++)
            {
                m_dtPoints.Columns.Add((i+1).ToString());
            }
            for (int i = 0; i < nMaskSize; i++)
            {
                m_dtPoints.Rows.Add();
            }

            dgMask.DataSource = m_dtPoints;
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public int Multiplier
        {
            get { return Convert.ToInt32(txtMultiplier.Text); }
        }

        public int Divisor
        {
            get { return Convert.ToInt32(txtDivisor.Text); }
        }

        public byte MaskSize
        {
            get { return Convert.ToByte(txtMaskSize.Text); }
        }

        private List<int> m_listMask = new List<int>();
        public List<int> Mask
        {
            get{ return m_listMask; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtPoints.Rows)
            {
                string strRow = String.Empty;
                for (int nCol = 0; nCol < dr.ItemArray.Length; nCol++)
                {
                    if (String.Empty == dr.ItemArray[nCol].ToString())
                    {
                        MessageBox.Show("Please enter all the Mask Value(s).",
                            Program.cAPP_NAME);
                        return;
                    }
                    else
                    {
                        m_listMask.Add(Convert.ToInt32( dr.ItemArray[nCol].ToString()));
                    }
                }
            }
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
