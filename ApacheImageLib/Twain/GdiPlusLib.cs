using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Apache.ImageLib.TwainLib
{
    public class CGdiPlus
    {
        #region STATIC_FUNCTIONS

        public static bool SaveImage(string strFilename, IntPtr hDIB, bool bShowDlg)
        {
            bool bSuccess = true;
            try
            {
                IntPtr ptrBMP = CGdiPlus.GlobalLock(hDIB);
                IntPtr ptrPixel = CGdiPlus.GetPixelInfo(ptrBMP);

                bSuccess = CGdiPlus.SaveDIB(strFilename, ptrBMP, ptrPixel, bShowDlg);

                if (hDIB != IntPtr.Zero)
                {
                    CGdiPlus.GlobalFree(hDIB);
                    hDIB = IntPtr.Zero;
                }
            }
            catch 
            {
                MessageBox.Show("Error encountered in saving captured image.");
                bSuccess = false;
            }
            return bSuccess;
        }

        private static IntPtr GetPixelInfo(IntPtr ptrBmp)
        {
            BITMAPINFOHEADER bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(ptrBmp, bmi);

            Rectangle bmprect = new Rectangle(0, 0, 0, 0);
            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)ptrBmp;
            return (IntPtr)p;
        }

        private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        private static bool GetCodecClsid(string filename, out Guid clsid)
        {
            clsid = Guid.Empty;
            string ext = Path.GetExtension(filename);
            if (ext == null)
                return false;
            ext = "*" + ext.ToUpper();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.IndexOf(ext) >= 0)
                {
                    clsid = codec.Clsid;
                    return true;
                }
            }
            return false;
        }

        private static bool SaveDIB(string strImageName, IntPtr ptrBMP, IntPtr ptrPixel, bool bShowDialog)
        {
            try
            {
                if (bShowDialog)
                {
                    SaveFileDialog sd = new SaveFileDialog();

                    sd.FileName = strImageName;
                    sd.Title = "Save bitmap as...";
                    sd.Filter = "Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|JPEG file (*.jpg)|*.jpg|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*";
                    sd.FilterIndex = 1;
                    if (sd.ShowDialog() != DialogResult.OK)
                        return false;
                    strImageName = sd.FileName;
                }
                Guid clsid;
                if (!GetCodecClsid(strImageName, out clsid))
                {
                    MessageBox.Show("Unknown picture format for extension " + Path.GetExtension(Path.GetExtension(strImageName)),
                                    Apache.ImageLib.Program.cAPP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                IntPtr img = IntPtr.Zero;
                int st = GdipCreateBitmapFromGdiDib(ptrBMP, ptrPixel, ref img);
                if ((st != 0) || (img == IntPtr.Zero))
                    return false;

                st = GdipSaveImageToFile(img, strImageName, ref clsid, IntPtr.Zero);
                GdipDisposeImage(img);
                return st == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #endregion

        #region DLL_FUNCTIONS

        [DllImport("gdiplus.dll", ExactSpelling = true)]
        internal static extern int GdipCreateBitmapFromGdiDib(IntPtr bminfo, IntPtr pixdat, ref IntPtr image);

        [DllImport("gdiplus.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal static extern int GdipSaveImageToFile(IntPtr image, string filename, [In] ref Guid clsid, IntPtr encparams);

        [DllImport("gdiplus.dll", ExactSpelling = true)]
        internal static extern int GdipDisposeImage(IntPtr image);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        #endregion
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }
}
