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
    public partial class CGeometricResultDlg : Form
    {
        private double[] m_arrMoments = new double[16];
        private double[] m_arrInvMoments = new double[7];
        private double[] m_arrBetaMoments = new double[6];

        public CGeometricResultDlg(ref double[] vfMoments, ref double[] vfInvMoments, ref double[] vfBetaMoments)
        {
            try
            {
                InitializeComponent();
                m_arrMoments = vfMoments;
                m_arrInvMoments = vfInvMoments;
                m_arrBetaMoments = vfBetaMoments;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        // LISTVIEW - Sandeep
        private void CGeometricResultDlg_Load(object sender, EventArgs e)
        {
            try
            {
                listResult.View = View.Details;
                listResult.Columns.Add("Param", 150);
                listResult.Columns.Add("Value", 400);

                // Row 1
                ListViewItem row1 = new ListViewItem("Moments");
                string str = String.Empty;
                for (int nCnt = 0; nCnt < m_arrMoments.Length; nCnt++)
                {
                    str += String.Format("{0:0.000000}", m_arrMoments[nCnt]);
                    if (nCnt < m_arrMoments.Length - 1) str += ", ";
                }

                row1.SubItems.Add(str);
                listResult.Items.Add(row1);

                // Row 2
                ListViewItem row2 = new ListViewItem("Invariant Moments");
                str = String.Empty;
                for (int nCnt = 0; nCnt < m_arrInvMoments.Length; nCnt++)
                {
                    str += String.Format("{0:0.000000}", m_arrInvMoments[nCnt]);
                    if (nCnt < m_arrInvMoments.Length - 1) str += ", ";
                }

                row2.SubItems.Add(str);
                listResult.Items.Add(row2);

                // Row 2
                ListViewItem row3 = new ListViewItem("Beta Moments");
                str = String.Empty;
                for (int nCnt = 0; nCnt < m_arrBetaMoments.Length; nCnt++)
                {
                    str += String.Format("{0:0.000000}", m_arrBetaMoments[nCnt]);
                    if (nCnt < m_arrBetaMoments.Length - 1) str += ", ";
                }

                row3.SubItems.Add(str);
                listResult.Items.Add(row3);
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
