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
    public partial class CCompareResultDlg : Form
    {
        CDyadic.CCompareResult m_objResult;

        public CCompareResultDlg(CDyadic.CCompareResult result)
        {
            try
            {
                InitializeComponent();
                m_objResult = result;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        // LISTVIEW - Sandeep
        private void CBlobPostResultDlg_Load(object sender, EventArgs e)
        {
            try
            {
                listResult.Items.Add("Signal Power: " + String.Format("{0:0.00}", m_objResult.dSigPower));
                listResult.Items.Add("Noise Power: " + String.Format("{0:0.00}", m_objResult.dNoisePower));
                listResult.Items.Add("Noise Mean: " + String.Format("{0:0.00}", m_objResult.dNoiseMean));
                listResult.Items.Add("Signal/Noise: " + String.Format("{0:0.000}", m_objResult.dSbyN));
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
