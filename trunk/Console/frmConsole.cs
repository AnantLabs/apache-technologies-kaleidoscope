using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Apache.ImageLib;
using Apache.ImageLib.TwainLib;

namespace Kaleidoscope
{
    public partial class CConsole : Form, IMessageFilter
    {
        #region VARIABLES

        private bool m_bMsgfilter;
        private CTwain m_objTwain = null;
        private string m_strImageName = null;
        private static List<Form> m_listForms = new List<Form>();

        #endregion

        #region CTOR

        public CConsole()
        {
            InitializeComponent();
        }

        #endregion

        #region PROPERTIES

        public Stream GetResource(string strImageName)
        {
            return this.GetType().Assembly.GetManifestResourceStream(strImageName);
        }

        private Bitmap m_bmpSrc;
        public Bitmap SrcImage
        {
            get { return m_bmpSrc; }
            set { m_bmpSrc = value; }
        }

        private void displayImg(Bitmap src)
        {
            try
            {
                if (null != src)
                {
                    CImageViewFrm view = new CImageViewFrm(this, src);
                    view.MdiParent = this; //this refers to MainForm (parent)
                    m_listForms.Add(view);
                    view.Show();
                }
                else
                {
                    MessageBox.Show("Please check the image format", Program.cAPP_NAME);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void showErrorMsg(Exception ex)
        {
            MessageBox.Show(ex.Message);
            this.Cursor = Cursors.Hand;
        }

        #endregion

        #region TWAIN_CONTROLS

        private void mbFileSelectScanner_Click(object sender, EventArgs e)
        {
            try
            {
                m_objTwain.Select();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void mbFileAcquire_Click(object sender, EventArgs e)
        {
            try
            {
                if (!m_bMsgfilter)
                {
                    this.Enabled = false;
                    m_bMsgfilter = true;
                    Application.AddMessageFilter(this);
                }
                m_objTwain.Acquire();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            try
            {
                TwainCommand cmd = m_objTwain.PassMessage(ref m);
                if (cmd == TwainCommand.Not)
                    return false;

                switch (cmd)
                {
                    case TwainCommand.CloseRequest:
                        {
                            stopAcquiring();
                            m_objTwain.CloseSrc();
                            break;
                        }
                    case TwainCommand.CloseOk:
                        {
                            stopAcquiring();
                            m_objTwain.CloseSrc();
                            break;
                        }
                    case TwainCommand.DeviceEvent:
                        {
                            break;
                        }
                    case TwainCommand.TransferReady:
                        {
                            ArrayList arrPics = m_objTwain.TransferPictures();
                            stopAcquiring();
                            m_objTwain.CloseSrc();
                            for (int i = 0; i < arrPics.Count; i++)
                            {
                                //CGdiPlus.SaveImage(m_frmDisplay.DirPath + "\\" + DateTime.Now.ToString("yyyyMMMdd_HH-mm-ss") + ".jpg",
                                //    (IntPtr)arrPics[i], false);
                            }
                            //m_frmDisplay.UpdateImageList();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }

        private void stopAcquiring()
        {
            if (m_bMsgfilter)
            {
                Application.RemoveMessageFilter(this);
                m_bMsgfilter = false;
                this.Enabled = true;
                this.Activate();
            }
        }

        #endregion

        #region FILE_MENU

        private void msFileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                string strLastPath = RegistryAccess.ReadRegistryString("LastOpenPath");
                dlg.InitialDirectory = strLastPath.Length == 0 ? Application.StartupPath : strLastPath;
                dlg.Filter = "All files (*.*)|*.*|JPEG file (*.jpg)|*.jpg|Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                m_strImageName = dlg.FileName;
                RegistryAccess.WriteRegistry("LastOpenPath", dlg.FileName);
                CImageViewFrm view = new CImageViewFrm(this, dlg.FileName);
                view.Show();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFileSave_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFileSaveAs_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFileProperties_Click(object sender, EventArgs e)
        {
            try
            {
                CImageInfo obj = new CImageInfo(SrcImage);
                int nMinGrey = 0, nMaxGrey = 0;
                double dMean = 0.0, dMeanSquare = 0.0, dStdDeviation = 0.0, dVariance = 0.0, dEntropy = 0.0;
                if (true == obj.ImageStatistics(ref nMinGrey, ref nMaxGrey, ref dMean, ref dMeanSquare,
                        ref dVariance, ref dStdDeviation, ref dEntropy))
                {
                    CPropertiesDlg dlg = new CPropertiesDlg(SrcImage.Width, SrcImage.Height, nMinGrey, nMaxGrey,
                        dMean, dMeanSquare, dVariance, dStdDeviation, dEntropy);
                    dlg.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region PATTERN_MENU

        private void msPatternBar_Click(object sender, EventArgs e)
        {
            try
            {
                CBarDlg dlg = new CBarDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(dlg.ImageWidth, dlg.ImageHeight);
                    Bitmap src = obj.Bar(dlg.LowerT, dlg.UpperT, dlg.Bars);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternChequer_Click(object sender, EventArgs e)
        {
            try
            {
                CChequerDlg dlg = new CChequerDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(dlg.ImageWidth, dlg.ImageHeight);
                    Bitmap src = obj.Chequer(dlg.LowerT, dlg.UpperT, dlg.ChequerWidth, dlg.ChequerHeight);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternConstant_Click(object sender, EventArgs e)
        {
            try
            {
                CConstantDlg dlg = new CConstantDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(dlg.ImageWidth, dlg.ImageHeight);
                    Bitmap src = obj.Constant(dlg.Bits, dlg.Intensity);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternDisk_Click(object sender, EventArgs e)
        {
            try
            {
                CDiskDlg dlg = new CDiskDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(dlg.ImageWidth, dlg.ImageHeight);
                    Bitmap src = obj.Disk(dlg.DiskRadius, dlg.DiskXCenter, dlg.DiskYCenter, dlg.Intensity);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternPolygon_Click(object sender, EventArgs e)
        {
            try
            {
                CPolygonDlg dlg = new CPolygonDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(dlg.ImageWidth, dlg.ImageHeight);

                    Bitmap src = obj.Polygon(dlg.Intensity, dlg.Points);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternSinosoidal_Click(object sender, EventArgs e)
        {
            try
            {
                CSineDlg dlg = new CSineDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(dlg.ImageWidth, dlg.ImageHeight);
                    Bitmap src = obj.Sine(dlg.PeakAmp, dlg.Offset, dlg.HorzFreq, dlg.VertFreq);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternNoise_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CNoiseDlg dlg = new CNoiseDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(SrcImage);
                    Bitmap src = obj.AddNoise(dlg.NoiseType, dlg.Amplitude, dlg.ErrorProb);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
                dlg.Close();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternPlot3D_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CAngleDlg dlg = new CAngleDlg(this, "Plot 3D");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CPatterns obj = new CPatterns(SrcImage);
                    Bitmap src = obj.Plot3D(dlg.Angle);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
                dlg.Close();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternOilPainting_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CPatterns obj = new CPatterns(SrcImage);
                Bitmap src = obj.OilPainting(15);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msPatternPixellate_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CPatterns obj = new CPatterns(SrcImage);
                Bitmap src = obj.Pixellate();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region PROCESS_MENU

        #region ZOOM

        private void tsZoomPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                obj.ZoomFator *= 2;
                if (obj.ZoomFator > 8.0f)
                {
                    obj.ZoomFator = 8.0f;
                    return;
                }
                obj.ZoomImage(obj.ZoomFator);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void tsZoomMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                obj.ZoomFator /= 2;
                if (obj.ZoomFator < (1.0f / 8.0f))
                {
                    obj.ZoomFator = 1.0f / 8.0f;
                    return;
                }
                obj.ZoomImage(obj.ZoomFator);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region CONVERSION

        private void msProcessConvert1Bit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.TransformImage(PixelFormat.Format1bppIndexed);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessConvert4Bit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.TransformImage(PixelFormat.Format4bppIndexed);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessConvert8Bit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.TransformImage(PixelFormat.Format8bppIndexed);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessConvert24Bit_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.TransformImage(PixelFormat.Format24bppRgb);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region PREPROCESS

        #region FLIP

        private void msProcessFlipHorizontal_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.HFlip();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessFlipVertical_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.VFlip();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region RESIZE

        private void msProcessDouble_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.DoubleImage();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessHalve_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.HalveImage();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessResize_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CResizeDlg dlg = new CResizeDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.ResizeImage(dlg.XScale, dlg.YScale);
                    displayImg(src);
                }
                dlg.Close();
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region ROTATION

        private void msProcessRotate90_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.RotateImage(90);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessRotate180_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.RotateImage(180);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessRotate270_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.RotateImage(270);
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessRotateArbitary_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CAngleDlg dlg = new CAngleDlg(this, "Resize");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.RotateImage(dlg.Angle);
                    displayImg(src);
                }
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
                this.Cursor = Cursors.Hand;
            }
        }

        #endregion

        #endregion

        #region POSTPROCESS

        private void msProcessAnnotateMark_Click(object sender, EventArgs e)
        {

        }

        private void msProcessAnnotateSave_Click(object sender, EventArgs e)
        {

        }

        private void msProcessPseudocolorRandom_Click(object sender, EventArgs e)
        {
            if (null == SrcImage) return;
            this.Cursor = Cursors.WaitCursor;
            CProcess obj = new CProcess(SrcImage);
            Bitmap src = obj.RandomPseudoColor();
            displayImg(src);
            this.Cursor = Cursors.Hand;
        }

        private void msProcessPseudocolorUniform_Click(object sender, EventArgs e)
        {
            if (null == SrcImage) return;
            CUniformPseudoColor dlg = new CUniformPseudoColor(this, "Uniform Pseudocolor");
            if (DialogResult.Cancel == dlg.ShowDialog()) return;
            this.Cursor = Cursors.WaitCursor;
            CProcess obj = new CProcess(SrcImage);
            Bitmap src = obj.UniformPseudoColor(dlg.Intensity / 100.0f, dlg.Saturation / 100.0f);
            displayImg(src);
            this.Cursor = Cursors.Hand;
        }

        #endregion

        #region HISTOGRAM

        private void msProcessCummulative_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.HistogramCumulative();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessEqualization_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.HistogramEqualization();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessExpansion_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistogramExpansionDlg dlg = new CHistogramExpansionDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.HistogramExpansion(dlg.CenterIntensity, dlg.Slope / 10.0f);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessRamp_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistogramRampDlg dlg = new CHistogramRampDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.HistogramRamp(dlg.SlopeType);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessCummulativeHistograph_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistograph obj = new CHistograph(SrcImage);
                int[] vHistogram = new int[CDefinitions.SCALE];
                obj.CumulativeHistogram(ref vHistogram);
                CView dlg = new CView(ref vHistogram);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessSimple_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistograph obj = new CHistograph(SrcImage);
                int[] vHistogram = new int[CDefinitions.SCALE];
                obj.SimpleHistogram(ref vHistogram);
                CView dlg = new CView(ref vHistogram);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessRowScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistographDlg dlg = new CHistographDlg(this, "Row Scan", "Row Num", m_bmpSrc.Height - 1, 0, 0);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CHistograph obj = new CHistograph(SrcImage);
                    int[] vHistogram = new int[m_bmpSrc.Width];
                    obj.RowScanHistogram(ref vHistogram, dlg.Value);
                    CView res = new CView(ref vHistogram);
                    res.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessColumnScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistographDlg dlg = new CHistographDlg(this, "Column Scan", "Col Num", m_bmpSrc.Width - 1, 0, 0);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CHistograph obj = new CHistograph(SrcImage);
                    int[] vHistogram = new int[m_bmpSrc.Height];
                    obj.ColoumScanHistogram(ref vHistogram, dlg.Value);
                    CView res = new CView(ref vHistogram);
                    res.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessLateralX_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistograph obj = new CHistograph(SrcImage);
                int[] vHistogram = new int[m_bmpSrc.Width];
                obj.LateralXHistogram(ref vHistogram);
                CView dlg = new CView(ref vHistogram);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessLateralY_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CHistograph obj = new CHistograph(SrcImage);
                int[] vHistogram = new int[m_bmpSrc.Height];
                obj.LateralYHistogram(ref vHistogram);
                CView dlg = new CView(ref vHistogram);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessRadial_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CRadialHistogramDlg dlg = new CRadialHistogramDlg(this, m_bmpSrc.Width, m_bmpSrc.Height);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CHistograph obj = new CHistograph(SrcImage);
                    int[] vHistogram = new int[dlg.RingNum];
                    if (true == obj.RadialHistogram(ref vHistogram, new Point(dlg.ColNum, dlg.RowNum), dlg.RingNum, dlg.Ringwidth))
                    {
                        CView res = new CView(ref vHistogram);
                        res.ShowDialog();
                    }
                    else 
                    {
                        MessageBox.Show("Graph is not supported for given set of parameter(s).");
                    }
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region SCALING

        private void msProcessBand_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CBandDlg dlg = new CBandDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Band(dlg.LowerT, dlg.UpperT,
                        dlg.R, dlg.G, dlg.B, dlg.Type);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessInvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CIntensityDlg dlg = new CIntensityDlg(this, "Invert");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Invert(dlg.R, dlg.G, dlg.B);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessLinear_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CLinearDlg dlg = new CLinearDlg(this, "Linear");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Linear(dlg.Brightness, dlg.Contrast, dlg.Flag);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Logrithmic", "Cofficient", 1000, 0, 25);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Logarithm(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessMask_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMaskLogicDlg dlg = new CMaskLogicDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.MaskLogic(dlg.MaskValue, dlg.MaskType);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessMosaic_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMosaicDlg dlg = new CMosaicDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Mosaic(dlg.XScale, dlg.YScale);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
                this.Cursor = Cursors.Hand;
            }
        }

        private void msProcessPower_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Power", "Exponent", 100, 0, 5);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Power(dlg.Value / 10.0);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessStretchContrast_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CContrastStretchDlg dlg = new CContrastStretchDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.StretchContrast(dlg.Alpha, dlg.Beta, dlg.Gamma, dlg.LowerT, dlg.UpperT);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessStretch3Sigma_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                this.Cursor = Cursors.WaitCursor;
                CProcess obj = new CProcess(SrcImage);
                Bitmap src = obj.Stretch3Sigma();
                displayImg(src);
                this.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region FILTERING

        private void msProcessConvolve_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CConvolveDlg dlg = new CConvolveDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Convolve(dlg.Mask, dlg.MaskSize, dlg.Multiplier, dlg.Divisor);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessHighPass_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CFilterLogicDlg dlg = new CFilterLogicDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Highpass(dlg.MaskType);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessLowPass_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CFilterLogicDlg dlg = new CFilterLogicDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Lowpass(dlg.MaskType);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessMean_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Mean", "Difference", 25, 0, 5);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Mean(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessMedian_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Median", "Difference", 25, 0, 5);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Median(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Mode", "Difference", 25, 0, 5);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.Mode(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessStatistical_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessSubcarrierKill_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Subcarrier Kill", "Frequency", 500, 0, 125);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.SubcarrierKill(dlg.Value / 10.0);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msProcessVectorQuantization_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Vector Quantization", "Quality Factor", 8, 1, 4);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CProcess obj = new CProcess(SrcImage);
                    Bitmap src = obj.VectorQuantization(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
                this.Cursor = Cursors.Hand;
            }
        }

        #endregion

        #endregion

        #region FEATURES_MENU

        #region THRESHOLD

        private void msFeatureGlobal_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CGlobalThresholdDlg dlg = new CGlobalThresholdDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    byte[,] vTempRaster = new byte[SrcImage.Height, SrcImage.Width];
                    Bitmap src = obj.GlobalThreshold(dlg.Threshold, dlg.Percent, ref vTempRaster);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
                this.Cursor = Cursors.Hand;
            }
        }

        private void msFeatureAdaptive_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Adaptive", "Mininum Range", 255, 0, 51);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.AdaptiveThreshold(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region EDGE

        private void msFeatureCanny_Click(object sender, EventArgs e)
        {
            try
            {
                //if (null == SrcImage) return;
                //this.Cursor = Cursors.WaitCursor;
                //CFeatures obj = new CFeatures(SrcImage);
                //Bitmap src = obj.Canny();
                //displayImg(src);
                //this.Cursor = Cursors.Hand;

                if (null == SrcImage) return;
                CCannyDlg dlg = new CCannyDlg(this, "Pre-blob Analysis");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CCanny obj = new CCanny(SrcImage);
                    Bitmap src = obj.Canny(dlg.LowerT / 10.0, dlg.UpperT / 10.0, dlg.Sigma);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeaturesEuclidean_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 128);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.EuclideanDistanceColor(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureKrisch_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 128);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.Krisch(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeature4Neighbors_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 10);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.LOG4(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeature8Neighbors_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 10);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.LOG8(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeaturePrewitt_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 128);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.Prewitt(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureRobert_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 64);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.Robert(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureRobinson3Level_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 128);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.Robinson3L(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureRobinson5Level_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 128);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.Robinson5L(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureSobel_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CSingleSliderDlg dlg = new CSingleSliderDlg(this, "Edge Detection", "Intensity", 255, 0, 128);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap src = obj.Sobel(dlg.Value);
                    displayImg(src);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region SEGMENTATION

        private void msFeaturesKMeans_Click(object sender, EventArgs e)
        {
            try
            {
                CKMeansDlg dlg = new CKMeansDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CSegmentation obj = new CSegmentation(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.KMeans(dlg.Segments, dlg.VectorType);
                    displayImg(dest);
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeaturesPixelClassification_Click(object sender, EventArgs e)
        {
            CSegmentation obj = new CSegmentation(SrcImage);
            this.Cursor = Cursors.WaitCursor;
            Bitmap dest = obj.PixelClassification();
            displayImg(dest);
            this.Cursor = Cursors.Hand;
        }

        #endregion

        #region BLOB_ANALYSIS

        private void msFeatureBlobPre_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CCannyDlg dlg = new CCannyDlg(this, "Blob Analysis");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CBlobAnalysis obj = new CBlobAnalysis(SrcImage);
                    int nBlobs = -1;
                    Bitmap src = obj.PreBlobAnalysis(dlg.LowerT / 10.0, dlg.UpperT / 10.0, dlg.Sigma, ref nBlobs);
                    displayImg(src);
                    MessageBox.Show("The number of object(s) detected are " + nBlobs.ToString());
                    this.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureBlobPost_Click(object sender, EventArgs e)
        {
            try
            {
                CBlobPostDlg dlg = new CBlobPostDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CBlobAnalysis obj = new CBlobAnalysis(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    double dXScale = 1.0, dYScale = 1.0;
                    List<CBlobAnalysis.OBJECT3DGEOMETRY> lRecords = new List<CBlobAnalysis.OBJECT3DGEOMETRY>();
                    Bitmap dest = obj.PostBlobAnalysis(dlg.Density, dXScale, dYScale, ref lRecords);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                    CBlobPostResultDlg result = new CBlobPostResultDlg(ref lRecords);
                    result.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region REGION_ANALYSIS

        private void msFeatureRegionCount_Click(object sender, EventArgs e)
        {
            try
            {
                CRegionTypeDlg dlg = new CRegionTypeDlg(this, "Region Count");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CFeatures obj = new CFeatures(SrcImage);
                    int nObjects = obj.CountRegions(dlg.RegionType);
                    MessageBox.Show("The number of object(s) detected are " + nObjects.ToString());

                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureLabel_Click(object sender, EventArgs e)
        {
            try
            {
                CRegionTypeDlg dlg = new CRegionTypeDlg(this, "Region Label");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CFeatures obj = new CFeatures(SrcImage);
                    displayImg(obj.LabelRegions(dlg.RegionType, dlg.Pseudocolor));
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureRegionOutline_Click(object sender, EventArgs e)
        {
            try
            {
                CRegionOutlineDlg dlg = new CRegionOutlineDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CBlobAnalysis obj = new CBlobAnalysis(SrcImage);
                    List<Point> lCoords = new List<Point>();
                    Bitmap dest = obj.OutlineObjectImage(dlg.ObjectNum, ref lCoords);
                    if (null != dest)
                    {
                        displayImg(dest);
                        CBlobAnalysis.OBJECT2DGEOMETRY obj2D = new CBlobAnalysis.OBJECT2DGEOMETRY();
                        // Call the function for metrics calculation
                        if (true == obj.GetObject2DFeatures(ref lCoords, 1.0, 1.0, ref obj2D))
                        {
                            CRegionOutlineResultDlg res = new CRegionOutlineResultDlg(obj2D);
                            res.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region MOMENTS

        private void msFeatureGeometricMoments_Click(object sender, EventArgs e)
        {
            try
            {
                double[] m_arrMoments = new double[16];
                double[] m_arrInvMoments = new double[7];
                double[] m_arrBetaMoments = new double[6];
                CFeatures obj = new CFeatures(SrcImage);
                if (true == obj.GeometricMoments(ref m_arrMoments, ref m_arrInvMoments, ref m_arrBetaMoments))
                {
                    CGeometricResultDlg dlg = new CGeometricResultDlg(ref m_arrMoments, ref m_arrInvMoments, ref m_arrBetaMoments);
                    dlg.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureLocalRange_Click(object sender, EventArgs e)
        {
            try
            {
                CMomentsDlg dlg = new CMomentsDlg(this, "Local Range", -2, -3, 4, 5);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap dest = obj.LocalRange(dlg.Left, dlg.Top, dlg.Right, dlg.Bottom);
                    if (null != dest)
                    {
                        displayImg(dest);
                    }
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureLocalSigma_Click(object sender, EventArgs e)
        {
            try
            {
                CMomentsDlg dlg = new CMomentsDlg(this, "Local Sigma", -2, -3, 4, 5);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap dest = obj.LocalSigma(dlg.Left, dlg.Top, dlg.Right, dlg.Bottom);
                    if (null != dest)
                    {
                        displayImg(dest);
                    }
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msFeatureFuzzySegment_Click(object sender, EventArgs e)
        {
            try
            {
                CFuzzySegmentDlg dlg = new CFuzzySegmentDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CFeatures obj = new CFeatures(SrcImage);
                    Bitmap dest = obj.FuzzySegment(dlg.Histogram, dlg.ZeroCrossing, dlg.LowT, dlg.HighT, 0.5);
                    if (null != dest)
                    {
                        displayImg(dest);
                    }
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #endregion

        #region MORPHOLOGY

        #region MOMENTS

        private void msMorphBinaryExpand_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMorphology obj = new CMorphology(SrcImage);
                this.Cursor = Cursors.WaitCursor;
                Bitmap dest = obj.BinaryExpand();
                this.Cursor = Cursors.Hand;
                displayImg(dest);
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphBinaryShrink_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMorphology obj = new CMorphology(SrcImage);
                this.Cursor = Cursors.WaitCursor;
                Bitmap dest = obj.BinaryShrink();
                this.Cursor = Cursors.Hand;
                displayImg(dest);
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphConvexHull_Click(object sender, EventArgs e)
        {
            try
            {
                //if (null == SrcImage) return;
                //CMorphology obj = new CMorphology(SrcImage);
                //this.Cursor = Cursors.WaitCursor;
                //Bitmap dest = obj.ConvexHull(1, 128);
                //this.Cursor = Cursors.Hand;
                //displayImg(dest);
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphDilate_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMorphology obj = new CMorphology(SrcImage);
                this.Cursor = Cursors.WaitCursor;
                Bitmap dest = obj.Dilate();
                this.Cursor = Cursors.Hand;
                displayImg(dest);
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphErode_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMorphology obj = new CMorphology(SrcImage);
                this.Cursor = Cursors.WaitCursor;
                Bitmap dest = obj.Erode();
                this.Cursor = Cursors.Hand;
                displayImg(dest);
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphFormFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CFormFilterDlg dlg = new CFormFilterDlg(this, "Form Filter");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CMorphology obj = new CMorphology(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.FormFilter(new Point(dlg.XLow, dlg.YLow), new Point(dlg.XHigh, dlg.YHigh), dlg.BandType);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphLocalDistance_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CRegionSelectionDlg dlg = new CRegionSelectionDlg(this, "Local Distance");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CMorphology obj = new CMorphology(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.LocalDistance(dlg.RegionType);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphPerimeterFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CPerimeterFilterDlg dlg = new CPerimeterFilterDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CMorphology obj = new CMorphology(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.PerimeterFilter(new Point(dlg.PerimeterLow, dlg.PerimeterHigh),
                        dlg.XScale, dlg.YScale, dlg.BandType);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphShapeFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CShapeFilterDlg dlg = new CShapeFilterDlg(this);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CMorphology obj = new CMorphology(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.ShapeFilter(dlg.ShapeLow, dlg.ShapeHigh, dlg.XScale, dlg.YScale, dlg.BandType);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphSizeFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CMorphology obj = new CMorphology(SrcImage);
                CSizeFilterDlg dlg = new CSizeFilterDlg(this, "Size Filter", m_bmpSrc.Width * m_bmpSrc.Height);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.SizeFilter(dlg.LowT, dlg.HighT, dlg.BandType);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msMorphThinFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CRegionSelectionDlg dlg = new CRegionSelectionDlg(this, "Thin Filter");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CMorphology obj = new CMorphology(SrcImage);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.ThinFilter(dlg.RegionType);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #endregion

        #region DYADIC

        private void msDyadicAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CDyadicDlg dlg = new CDyadicDlg(this, SrcImage, m_strImageName, "Add");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.Add(dlg.ImgCoord, dlg.ParamSliderValue);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicSubstract_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CDyadicDlg dlg = new CDyadicDlg(this, SrcImage, m_strImageName, "Subtract");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.Subtract(dlg.ImgCoord);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CDyadicDlg dlg = new CDyadicDlg(this, SrcImage, m_strImageName, "Multiply");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.Multiply(dlg.ImgCoord, dlg.ParamSliderValue / 10);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicDivide_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CDyadicDlg dlg = new CDyadicDlg(this, SrcImage, m_strImageName, "Divide");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.Divide(dlg.ImgCoord, dlg.ParamSliderValue / 10);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicCompare_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CDyadicDlg dlg = new CDyadicDlg(this, SrcImage, m_strImageName, "Compare");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    CDyadic.CCompareResult result = obj.Compare(dlg.ImgCoord);
                    this.Cursor = Cursors.Hand;
                    CCompareResultDlg res = new CCompareResultDlg(result);
                    res.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicSuperImpose_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CDyadicDlg dlg = new CDyadicDlg(this, SrcImage, m_strImageName, "Superimpose");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.Superpose(dlg.ImgCoord);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicLogic_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CLogicDlg dlg = new CLogicDlg(this, SrcImage, m_strImageName, "Image Logic");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CDyadic obj = new CDyadic(SrcImage, dlg.SrcImage2);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest = obj.ImageLogic(dlg.ImgCoord, dlg.Logic);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        private void msDyadicDepthMaps_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == SrcImage) return;
                CStereoImagingDlg dlg = new CStereoImagingDlg(this, SrcImage, m_strImageName, "Stereoimaging");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    CStereoimaging obj = new CStereoimaging(SrcImage, dlg.SrcImage2, 
                        dlg.OcclusionPenality, dlg.MatchReward, dlg.MaximumDisparity, dlg.ReliableThreshold,
                        dlg.Alpha, dlg.MaximumAttractionThreshold);
                    this.Cursor = Cursors.WaitCursor;
                    Bitmap dest1, dest2;
                    dest1 = new Bitmap(SrcImage.Width, SrcImage.Height);
                    dest2 = new Bitmap(SrcImage.Width, SrcImage.Height);
                    obj.DepthImage(ref dest1, ref dest2);
                    this.Cursor = Cursors.Hand;
                    displayImg(dest1);
                    displayImg(dest2);
                }
            }
            catch (Exception ex)
            {
                showErrorMsg(ex);
            }
        }

        #endregion

        #region WINDOWS

        private void msWindowsCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
            Point location = new Point(0, 0);
            foreach (Form f in m_listForms)
             {
                f.Width =  400;
                f.Height = 300;
                f.SetDesktopLocation(location.X, location.Y);
                location.X += 10;
                location.Y += 10;
             }
        }

        #endregion

        #region HELP_MENU

        private void msHelpAboutUs_Click(object sender, EventArgs e)
        {
            CAboutUs frm = new CAboutUs();
            frm.ShowDialog();
        }

        #endregion
    }
}
