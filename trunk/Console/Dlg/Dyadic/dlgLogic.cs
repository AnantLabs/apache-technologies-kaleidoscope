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
    public partial class CLogicDlg : Form
    {
        CConsole m_frmConsole = null;

        public CLogicDlg(CConsole c, Bitmap src, string strImageName, string strCaption)
        {
            InitializeComponent();
            m_frmConsole = c;

            pbImage1.BackgroundImage = src;
            udImg1X.Minimum = 0; udImg1X.Maximum = src.Width;
            udImg1Y.Minimum = 0; udImg1Y.Maximum = src.Height;

            lblImage1.Text = Path.GetFileName(strImageName);

            radAnd.Checked = true;
        }

        #region PROPERTIES

        private Bitmap m_srcImage2 = null;
        public Bitmap SrcImage2
        {
            get { return m_srcImage2; }
        }

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

        private int m_nLogic = 0;
        public int Logic
        {
            get { return m_nLogic; }
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

        private void radAnd_CheckedChanged(object sender, EventArgs e)
        {
            m_nLogic = 0;
        }

        private void radOr_CheckedChanged(object sender, EventArgs e)
        {
            m_nLogic = 1;
        }

        private void radXor_CheckedChanged(object sender, EventArgs e)
        {
            m_nLogic = 2;
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
