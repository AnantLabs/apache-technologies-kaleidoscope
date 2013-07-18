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
    public partial class CStereoImagingDlg : Form
    {
        #region VARIABLE

        CConsole m_frmConsole = null;

        #endregion

        #region CTOR

        public CStereoImagingDlg(CConsole c, Bitmap src, string strImageName, string strCaption)
        {
            InitializeComponent();
            m_frmConsole = c;

            pbImage1.BackgroundImage = src;
            lblImage1.Text = Path.GetFileName(strImageName);
        }

        #endregion

        #region PROPERTIES

        private Bitmap m_srcImage2 = null;
        public Bitmap SrcImage2
        {
            get { return m_srcImage2; }
        }

        public int OcclusionPenality
        {
            set { udOcclusionPenality.Value = value; }
            get { return Convert.ToInt32(udOcclusionPenality.Value); }
        }

        public int MatchReward
        {
            get { return Convert.ToInt32(udMatchReward.Value); }
        }

        public int MaximumDisparity
        {
            get { return Convert.ToInt32(udMaxDisparity.Value); }
        }

        public int ReliableThreshold
        {
            get { return Convert.ToInt32(udReliableThreashold.Value); }
        }

        public float Alpha
        {
            get { return Convert.ToInt32(udAlpha.Value)/100.0f; }
        }

        public int MaximumAttractionThreshold
        {
            get { return Convert.ToInt32(udOcclusionPenality.Value); }
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
            lblImage2.Text = Path.GetFileName(dlg.FileName);
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
