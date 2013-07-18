using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    public partial class CHistographDlg : Form
    {
        CConsole m_frmConsole = null;

        public CHistographDlg(CConsole c, string strTitle, string strSliderName, 
            int nSliderMax, int nSliderMin, int nSliderValue)
        {
            InitializeComponent();
            m_frmConsole = c;

            this.Text = strTitle;
            lblSliderName.Text = strSliderName;
            tbSlider.Maximum = nSliderMax;
            tbSlider.Minimum = nSliderMin;
            tbSlider.TickFrequency = (nSliderMax-nSliderMin)/20;
            tbSlider.Value = nSliderValue;
            txtSliderValue.Text = tbSlider.Value.ToString();
        }

        private void tbSlider_Scroll(object sender, EventArgs e)
        {
            txtSliderValue.Text = tbSlider.Value.ToString();
        }

        private void txtSliderValue_TextChanged(object sender, EventArgs e)
        {
            int nValue = Convert.ToInt32(txtSliderValue.Text);
            if (nValue >= tbSlider.Minimum && nValue <= tbSlider.Maximum)
            {
                tbSlider.Value = nValue;
            }
            else
            {
                MessageBox.Show(String.Format("Please enter the value between {0} and {1}.", tbSlider.Minimum, tbSlider.Maximum));
                txtSliderValue.Text = tbSlider.Minimum.ToString();
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

        public int Value
        {
            get { return tbSlider.Value; }
        }
    }
}
