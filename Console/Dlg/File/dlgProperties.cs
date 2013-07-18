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
    public partial class CPropertiesDlg : Form
    {
        public CPropertiesDlg(int nWidth, int nHeight, int nMinGrey, int nMaxGrey, 
            double dMean, double dMeanSquare, double dVariance, double dStdDeviation, double dEntropy)
        {
            try
            {
                InitializeComponent();
                listResult.View = View.Details;
                listResult.Columns.Add("Param", 150);
                listResult.Columns.Add("Value", 240);

                // Row 1
                ListViewItem row1 = new ListViewItem("Width");
                row1.SubItems.Add(nWidth.ToString());
                listResult.Items.Add(row1);

                // Row 2
                ListViewItem row2 = new ListViewItem("Height");
                row2.SubItems.Add(nHeight.ToString());
                listResult.Items.Add(row2);

                // Row 3
                ListViewItem row3 = new ListViewItem("Min Intensity");
                row3.SubItems.Add(nMinGrey.ToString());
                listResult.Items.Add(row3);

                // Row 4
                ListViewItem row4 = new ListViewItem("Max Intensity");
                row4.SubItems.Add(nMaxGrey.ToString());
                listResult.Items.Add(row4);

                // Row 5
                ListViewItem row5 = new ListViewItem("Mean");
                row5.SubItems.Add(String.Format("{0:0.00}", dMean));
                listResult.Items.Add(row5);

                // Row 6
                ListViewItem row6 = new ListViewItem("Mean Square");
                row6.SubItems.Add(String.Format("{0:0.00}", dMeanSquare));
                listResult.Items.Add(row6);

                // Row 7
                ListViewItem row7 = new ListViewItem("Variance");
                row7.SubItems.Add(String.Format("{0:0.00}", dVariance));
                listResult.Items.Add(row7);

                // Row 8
                ListViewItem row8 = new ListViewItem("Std Deviation");
                row8.SubItems.Add(String.Format("{0:0.00}", dStdDeviation));
                listResult.Items.Add(row8);

                // Row 9
                ListViewItem row9 = new ListViewItem("Entropy");
                row9.SubItems.Add(String.Format("{0:0.00}", dEntropy));
                listResult.Items.Add(row9);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
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
