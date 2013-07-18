using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CRadialHistogramDlg : Form
    {
        CConsole m_frmConsole = null;

        public CRadialHistogramDlg(CConsole c, int nWidth, int nHeight)
        {
            InitializeComponent();
            m_frmConsole = c;

            tbColumnNum.Minimum = 0;
            tbColumnNum.Maximum = nWidth;
            tbColumnNum.TickFrequency = (nWidth - 0) / 20;
            tbColumnNum.Value = 0;
            lblColumnNum.Text = tbColumnNum.Value.ToString();

            tbRowNum.Minimum = 0;
            tbRowNum.Maximum = nWidth;
            tbRowNum.TickFrequency = (nHeight - 0) / 20;
            tbRowNum.Value = 0;
            lblRowNum.Text = tbRowNum.Value.ToString();

            tbRingNum.Value = 10;
            lblRingNum.Text = tbRingNum.Value.ToString();

            tbRingWidth.Value = 3;
            lblRingWidth.Text = tbRingWidth.Value.ToString();
        }

        #region EVENTS

        private void tbColNum_Scroll(object sender, EventArgs e)
        {
            lblColumnNum.Text = tbColumnNum.Value.ToString();
        }

        private void tbRowNum_Scroll(object sender, EventArgs e)
        {
            lblRowNum.Text = tbRowNum.Value.ToString();
        }

        private void tbRingNum_Scroll(object sender, EventArgs e)
        {
            lblRingNum.Text = tbRingNum.Value.ToString();
        }

        private void tbRingWidth_Scroll(object sender, EventArgs e)
        {
            lblRingWidth.Text = tbRingWidth.Value.ToString();
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

        #region PROPERTIES

        public int RingNum
        {
            get { return Convert.ToInt32(tbRingNum.Value); }
        }

        public int Ringwidth
        {
            get { return Convert.ToInt32(tbRingWidth.Value); }
        }

        public byte ColNum
        {
            get { return Convert.ToByte(tbColumnNum.Value); }
        }

        public byte RowNum
        {
            get { return Convert.ToByte(tbRowNum.Value); }
        }

        #endregion
    }
}
