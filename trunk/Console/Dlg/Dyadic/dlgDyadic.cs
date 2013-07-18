using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Apache.ImageLib;

namespace Kaleidoscope
{
    public partial class CDyadicDlg : Form
    {
        #region VARIABLE

        CConsole m_frmConsole = null;

        #endregion

        #region CTOR

        public CDyadicDlg(CConsole c, Bitmap src, string strImageName, string strCaption)
        {
            InitializeComponent();
            m_frmConsole = c;

            pbImage1.BackgroundImage = src;
            udImg1X.Minimum = 0; udImg1X.Maximum = src.Width;
            udImg1Y.Minimum = 0; udImg1Y.Maximum = src.Height;
            ParamSliderValue = tbParamSlider.Value = 50;

            lblImage1.Text = Path.GetFileName(strImageName);

            switch (strCaption)
            {
                case "Subtract":
                    panelParam.Visible = false;
                    break;
                case "Multiply":
                    lblParamTitle.Text = "Scale";
                    break;
                case "Divide":
                    lblParamTitle.Text = "Scale";
                    break;
                case "Compare":
                    panelParam.Visible = false;
                    break;
                case "Superimpose":
                    panelParam.Visible = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region PROPERTIES

        private CRect m_rectCoord = new CRect();
        public CRect ImgCoord
        {
            get
            {
                m_rectCoord.SetRect(Convert.ToInt32(udImg1X.Value), Convert.ToInt32(udImg1Y.Value),
                Convert.ToInt32(udImg2X.Value), Convert.ToInt32(udImg2Y.Value));
                return m_rectCoord;
            }
        }

        public int ParamSliderValue
        {
            set { lblParamValue.Text = tbParamSlider.Value.ToString(); }
            get { return Convert.ToInt32(tbParamSlider.Value); }
        }

        private Bitmap m_srcImage2 = null;
        public Bitmap SrcImage2
        {
            get { return m_srcImage2; }
        }

        #endregion

        #region EVENTS

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string strLastPath = RegistryAccess.ReadRegistryString("LastOpenPath");
            dlg.InitialDirectory = strLastPath.Length == 0 ? Application.StartupPath : strLastPath;
            dlg.Filter = "All files (*.*)|*.*|JPEG file (*.jpg)|*.jpg|Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif";
            dlg.FilterIndex = 1;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            m_srcImage2 = (Bitmap)Image.FromFile(dlg.FileName);
            pbImage2.BackgroundImage = m_srcImage2;
            udImg2X.Minimum = 0; udImg2X.Maximum = m_srcImage2.Width;
            udImg2Y.Minimum = 0; udImg2Y.Maximum = m_srcImage2.Height;
            lblImage2.Text = Path.GetFileName(dlg.FileName);
        }

        private void tbParamSlider_Scroll(object sender, EventArgs e)
        {
            ParamSliderValue = tbParamSlider.Value;
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
    }
}
