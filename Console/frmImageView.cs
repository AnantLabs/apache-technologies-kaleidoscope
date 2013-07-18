using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Kaleidoscope
{
    class CImageViewFrm : Form
    {
        #region VARIABLES

        private CConsole m_frmConsole = null;
        private PictureBox m_pbView = null;
        private StatusStrip ssChild;
        private Panel m_panelView = null;

        #endregion

        #region CTOR

        public CImageViewFrm(CConsole c, string strFileName)
        {
            try
            {
                InitializeComponent();

                m_frmConsole = c;
                addControls(strFileName);
                loadImage(strFileName);

                m_bDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public CImageViewFrm(CConsole c, Bitmap src)
        {
            try
            {
                InitializeComponent();

                m_frmConsole = c;
                addControls(TempImgName);
                loadImage(src);

                m_bDirty = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CImageViewFrm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region PROPERTIES

        private bool m_bDirty;
        public bool IsDirty
        {
            get { return m_bDirty; }
        }

        static int m_nTempImgCounter = 0;
        static string TempImgName
        {
            get { return (++m_nTempImgCounter).ToString() + ".bmp"; }
        }

        private void loadImage(string strFileName)
        {
            m_pbView.Image = Image.FromFile(strFileName);
            setScrollBars();
        }

        private void loadImage(Bitmap bmpSrc)
        {
            m_pbView.Image = bmpSrc;
            setScrollBars();
        }

        private void setScrollBars()
        {
            if (m_pbView.SizeMode == PictureBoxSizeMode.AutoSize)
            {
                m_panelView.AutoScrollMinSize = new Size(m_pbView.Image.Width, m_pbView.Image.Height);
            }
            else
                m_panelView.AutoScrollMinSize = new Size(0, 0);
        }

        private void addControls(string strFileName)
        {
            try
            {
                m_panelView = new Panel();
                m_panelView.Dock = DockStyle.Fill;
                this.Controls.Add(m_panelView);

                m_pbView = new PictureBox();
                m_pbView.SizeMode = PictureBoxSizeMode.AutoSize;
                m_pbView.Dock = DockStyle.Fill;

                this.MdiParent = m_frmConsole;
                this.Dock = DockStyle.Fill;
                m_panelView.Controls.Add(m_pbView);

                this.Text = Path.GetFileName(strFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region OVERLOADS

        private void CImageViewFrm_Activated(object sender, EventArgs e)
        {
            m_frmConsole.SrcImage = (System.Drawing.Bitmap)m_pbView.Image;
        }

        private void CImageViewFrm_Deactivate(object sender, EventArgs e)
        {
            m_frmConsole.SrcImage = null;
        }

        #endregion

        #region PUBLIC_FUNCTIONS

        public void SaveImage()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                string strLastPath = RegistryAccess.ReadRegistryString("LastSavedPath");
                dlg.InitialDirectory = strLastPath.Length == 0 ? Application.StartupPath : strLastPath;
                dlg.Filter = "JPEG file (*.jpg)|*.jpg|Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                //DisplayImage.Save(dlg.FileName);
                RegistryAccess.WriteRegistry("LastSavedPath", dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MaybeSaveImage()
        {
            try
            {
                if (m_bDirty)
                {
                    DialogResult res = MessageBox.Show("Do you want to save the image?",
                        Program.cAPP_NAME,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        //DisplayImage.Save(DirPath + "\\" + DateTime.Now.ToString("yyyyMMMdd_HH-mm-ss") + ".bmp");
                    }
                    m_bDirty = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region INITIALIZATION

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CImageViewFrm));
            this.ssChild = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // ssChild
            // 
            this.ssChild.Location = new System.Drawing.Point(0, 248);
            this.ssChild.Name = "ssChild";
            this.ssChild.Size = new System.Drawing.Size(395, 22);
            this.ssChild.TabIndex = 4;
            this.ssChild.Text = "Child Status";
            // 
            // CImageViewFrm
            // 
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(395, 270);
            this.Controls.Add(this.ssChild);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CImageViewFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.CImageViewFrm_Activated);
            this.Deactivate += new System.EventHandler(this.CImageViewFrm_Deactivate);
            this.Load += new System.EventHandler(this.CImageViewFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
