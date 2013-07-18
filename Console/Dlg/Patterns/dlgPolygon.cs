using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CPolygonDlg : Form
    {
        #region VARIABLES

        CConsole m_frmConsole = null;
        DataTable m_dtPoints = null;

        #endregion

        #region CTOR

        public CPolygonDlg(CConsole c)
        {
            InitializeComponent();
            m_frmConsole = c;

            txtWidth.Text = "256";
            txtHeight.Text = "256";

            tbIntensity.Value = 255;
            lblIntensity.Text = tbIntensity.Value.ToString();

            m_dtPoints = new DataTable();
            m_dtPoints.Columns.Add("X");
            m_dtPoints.Columns.Add("Y");

            m_dtPoints.Rows.Add(220, 206);
            m_dtPoints.Rows.Add(36, 206);
            m_dtPoints.Rows.Add(127, 36);

            dgPoints.DataSource = m_dtPoints;
        }

        private void tbIntensity_Scroll(object sender, EventArgs e)
        {
            lblIntensity.Text = tbIntensity.Value.ToString();
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

        #endregion

        #region CHECK_EVENTS

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAlphaNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '_')
            {
                e.Handled = true;
            }
        }

        private void txtCurrency_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        #endregion

        #region PROPERTIES

        public byte Intensity
        {
            get { return Convert.ToByte(tbIntensity.Value); }
        }

        public int ImageWidth
        {
            get { return Convert.ToInt32(txtWidth.Text); }
        }

        public int ImageHeight
        {
            get { return Convert.ToInt32(txtHeight.Text); }
        }

        private List<Point> m_listPts = new List<Point>();
        public List<Point> Points
        {
            get
            {
                foreach (DataRow dr in m_dtPoints.Rows)
                {
                    m_listPts.Add(new Point(
                        Convert.ToInt32(dr["X"].ToString()), 
                        Convert.ToInt32(dr["Y"].ToString())));
                }

                return m_listPts;
            }
        }

        #endregion
    }
}
