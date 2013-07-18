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
    public partial class CRegionOutlineResultDlg : Form
    {
        CBlobAnalysis.OBJECT2DGEOMETRY m_2DObj = null;

        public CRegionOutlineResultDlg(CBlobAnalysis.OBJECT2DGEOMETRY obj)
        {
            try
            {
                InitializeComponent();
                m_2DObj = obj;
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
                double dXCentroid = m_2DObj.dXCentre; if (dXCentroid < 0) dXCentroid = 0.0f;
                double dYCentroid = m_2DObj.dYCentre; if (dYCentroid < 0) dYCentroid = 0.0f;
                listResult.Items.Add("Centroid: " + String.Format("{0:0.00}, {1:0.00}", dXCentroid, dYCentroid));

                listResult.Items.Add("X-Bound: " + String.Format("{0:0.00}, {1:0.00}", m_2DObj.dXMin, m_2DObj.dXMax));
                listResult.Items.Add("Y-Bound: " + String.Format("{0:0.00}, {1:0.00}", m_2DObj.dYMin, m_2DObj.dYMax));

                listResult.Items.Add("Radius: " + String.Format("{0:0.000}", m_2DObj.dRadius));
                listResult.Items.Add("Perimeter: " + String.Format("{0:0.000}", m_2DObj.dPerimeter));
                listResult.Items.Add("Area: " + String.Format("{0:0.000}", m_2DObj.dArea)); 
                listResult.Items.Add("Shape Factor: " + String.Format("{0:0.000}", m_2DObj.dShapeFactor));

                double dSpherocity = (Math.PI * Math.Pow(m_2DObj.dRadius, 2.0)) / (m_2DObj.dArea + 0.1);
                listResult.Items.Add("Spherocity: " + String.Format("{0:0.000}", dSpherocity));
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
