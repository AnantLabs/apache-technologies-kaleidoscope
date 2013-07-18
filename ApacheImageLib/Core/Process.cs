using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Apache.ImageLib
{
    public class CProcess : CImageInfo
    {
        #region CTOR

        public CProcess(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        #region ZOOM

        private float m_fZoom = 1;
        public float ZoomFator
        {
            set { m_fZoom = value; }
            get { return m_fZoom; }
        }

        public void ZoomImage(float fZoom)
        {
            try
            {
                Bitmap bmp = SrcImage;
                if (bmp == null) return;
                int nWidth = Convert.ToInt32(bmp.Width * fZoom);
                int nHeight = Convert.ToInt32(bmp.Height * fZoom);
                ResizeNearestNeighbor filter = new ResizeNearestNeighbor(nWidth, nHeight);
                SrcImage = applyFilter(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region TRANSFORMATION

        public Bitmap TransformImage(PixelFormat format)
        {
            Bitmap dest = null;
            try
            {
                dest = AForge.Imaging.Image.Clone(SrcImage, format);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dest;
        }

        #endregion

        #region FLIP

        public Bitmap HFlip()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(m_nWidth - nCol - 1, nRow);
                        dest.SetPixel(nCol, nRow, clr);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap VFlip()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, m_nHeight - nRow - 1);
                        dest.SetPixel(nCol, nRow, clr);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RESIZE

        public Bitmap DoubleImage()
        {
            try
            {
                Bitmap dest = new Bitmap(2 * m_nWidth, 2 * m_nHeight);
                List<Color> lineRaster = new List<Color>();
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    lineRaster.Clear();
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color rgb = SrcImage.GetPixel(nCol, nRow);
                        lineRaster.Add(rgb);
                        lineRaster.Add(rgb);
                    }
                    for (int nCnt = 0; nCnt < 2; nCnt++)
                        for (int nCol = 0; nCol < lineRaster.Count; nCol++)
                        {
                            dest.SetPixel(nCol, 2 * nRow + nCnt, lineRaster[nCol]);
                        }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Color getHalvePixel(Color rgb1, Color rgb2, Color rgb3, Color rgb4)
        {
            byte r = (byte)((rgb1.R + rgb2.R + rgb3.R + rgb4.R) / 4);
            byte g = (byte)((rgb1.G + rgb2.G + rgb3.G + rgb4.G) / 4);
            byte b = (byte)((rgb1.B + rgb2.B + rgb3.B + rgb4.B) / 4);

            return Color.FromArgb(r, g, b);
        }

        public Bitmap HalveImage()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth / 2, m_nHeight / 2);
                for (int nRow = 0; nRow < m_nHeight / 2; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth / 2; nCol++)
                    {
                        Color rgb = getHalvePixel(
                            SrcImage.GetPixel(2 * nCol, 2 * nRow),
                            SrcImage.GetPixel(2 * nCol + 1, 2 * nRow),
                            SrcImage.GetPixel(2 * nCol, 2 * nRow + 1),
                            SrcImage.GetPixel(2 * nCol + 1, 2 * nRow + 1));
                        dest.SetPixel(nCol, nRow, rgb);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap ResizeImage(byte xScale, byte yScale)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth * xScale, m_nHeight * yScale);
                System.Drawing.Point ptNewImgCoord = new System.Drawing.Point();
                ptNewImgCoord.Y = 0;

                // Initialize the output Y value and vertial differential
                System.Drawing.Point ptDiff = new System.Drawing.Point();
                ptDiff.Y = 0;

                // Calculate the differential amount
                System.Drawing.Point ptZoom = new System.Drawing.Point((int)(1.0 / xScale * 1000), (int)(1.0 / yScale * 1000));

                // Loop over rows in the original image
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    // Adjust the vertical accumulated differential, initialize the
                    // output X pixel and horizontal accumulated differential
                    ptDiff.Y -= 1000;
                    ptNewImgCoord.X = 0;
                    ptDiff.X = 0;
                    // Loop over pixels in the original image
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        // Adjust the horizontal accumulated differential
                        ptDiff.X -= 1000;
                        while (ptDiff.X < 0)
                        {
                            // Store values from original image scanline into the scaled
                            // buffer until accumulated differential crosses threshold
                            if (ptNewImgCoord.X < (m_nWidth * xScale) && ptNewImgCoord.Y < (m_nHeight * yScale))
                                dest.SetPixel(ptNewImgCoord.X, ptNewImgCoord.Y, SrcImage.GetPixel(nCol, nRow));
                            ptNewImgCoord.X++;
                            ptDiff.X += ptZoom.X;
                        }
                    }
                    int nYLine = ptNewImgCoord.Y;
                    while (ptDiff.Y < 0)
                    {
                        // The 'outer loop' -- output resized scan lines until the
                        // vertical threshold is crossed
                        ptDiff.Y += ptZoom.Y;
                        for (int nCol = 0; nCol < ptNewImgCoord.X; nCol++)
                        {
                            if (ptNewImgCoord.X < (m_nWidth * xScale) && ptNewImgCoord.Y < (m_nHeight * yScale))
                                dest.SetPixel(nCol, ptNewImgCoord.Y, dest.GetPixel(nCol, nYLine));
                        }
                        ptNewImgCoord.Y++;
                        if (ptNewImgCoord.Y >= m_nHeight * yScale)
                            ptNewImgCoord.Y = m_nHeight * yScale - 1;
                    }
                }//END ROWs
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ROTATION

        public Bitmap RotateImage(int nAngle)
        {
            try
            {
                RotateNearestNeighbor filter = new RotateNearestNeighbor(nAngle, false);
                return applyFilter(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region POSTPROCESS

        public Bitmap RandomPseudoColor()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                //srand((unsigned)time(NULL));

                //RGBQUAD Palette[SCALE];
                //memset(Palette, 0x00, sizeof(RGBQUAD)*SCALE);
                //COLORREF rgb;
                //int nCnt2 = 0;
                //for(int nCnt1 = 0; nCnt1 < SCALE; nCnt1 ++) {
                //    rgb = RGB(IMAX*rand()/32767, IMAX*rand()/32767, IMAX*rand()/32767);

                //    if (nCnt1 > 0) {
                //        for(nCnt2 = 0; nCnt2 < nCnt1; nCnt2++)
                //            if (RGB(Palette[nCnt2].rgbRed, Palette[nCnt2].rgbGreen, 
                //                Palette[nCnt2].rgbBlue) == rgb) break;
                //    }
                //    if (nCnt1 == nCnt2) {
                //        Palette[nCnt1].rgbRed = GetRValue(rgb);
                //        Palette[nCnt1].rgbGreen = GetGValue(rgb);
                //        Palette[nCnt1].rgbBlue = GetBValue(rgb);
                //        Palette[nCnt1].rgbReserved = 0;
                //    }
                //}

                //imgInfo.SetPalette(Palette);

                //for(int nCnt = 0; nCnt < imgInfo.GetImgInfo().GetWidth()*
                //    imgInfo.GetImgInfo().GetHeight(); nCnt++) {
                //    imgInfo.SetRaster()[nCnt] = imgInfo.GetRaster()[nCnt];
                //}

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap UniformPseudoColor(float fIntensity, float fSaturation)
        {
            Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

            //UINT lkpR[SCALE]; UINT lkpG[SCALE]; UINT lkpB[SCALE];
            //imgInfo.GetHistogramLkp(imgInfo, lkpR, lkpG, lkpB);

            //UINT nIPresent = 0;
            //for(UINT nCnt = 0; nCnt < SCALE; nCnt++)
            //{
            //    if (lkpR[nCnt] > 0)
            //        nIPresent++;
            //}

            //float fIThld = (2.0f * (float) PI) / nIPresent;

            //float fRed[SCALE], fGreen[SCALE], fBlue[SCALE];
            //float minR, minG, minB, maxR, maxG, maxB;
            //float Y, U, V, UbyV, UbyV2;
            //maxR = 0;  minR = IMAX;
            //maxG = 0;  minG = IMAX;
            //maxB = 0;  minB = IMAX;

            //// YUV to RGB
            //for ( UINT nCnt = 0; nCnt < nIPresent; nCnt++)
            //{
            //    if (nCnt < (nIPresent / 4)) {
            //        Y = fIThld * nCnt;
            //        U = 1;
            //        V = 1;
            //    }
            //    else if (nCnt < (nIPresent / 2))  {
            //        Y = fIThld * ((nIPresent / 2) - nCnt);
            //        U = 1;
            //        V = -1;
            //    }
            //    else if (nCnt < (3 * nIPresent / 4)) {
            //        Y = fIThld * (nCnt - (nIPresent /2));
            //        U = -1;
            //        V = -1;
            //    }
            //    else {
            //        Y = fIThld * (nIPresent - nCnt);
            //        U = -1;
            //        V = 1;
            //    }

            //    if (Y != PI / 2) {
            //        UbyV = tan(Y);
            //        UbyV2 = pow(UbyV, 2);
            //        V = V * pow(fSaturation / (UbyV2 + 1), 0.5f);
            //        U = U * V * UbyV;
            //    }
            //    else {
            //        V = 0;
            //        U = pow(fSaturation, 0.5f);
            //    }

            //    fRed[nCnt]   = fIntensity + U;
            //    fGreen[nCnt] = fIntensity - (0.5084f * U) - (0.186f * V);
            //    fBlue[nCnt]  = fIntensity + V;

            //    if (maxR < fRed[nCnt])
            //        maxR = fRed[nCnt];
            //    if (minR > fRed[nCnt])
            //        minR = fRed[nCnt];

            //    if (maxG < fGreen[nCnt])
            //        maxG = fGreen[nCnt];
            //    if (minG > fGreen[nCnt])
            //        minG = fGreen[nCnt];

            //    if (maxB < fBlue[nCnt])
            //        maxB = fBlue[nCnt];
            //    if (minB > fBlue[nCnt]) 
            //        minB = fBlue[nCnt];
            //}

            //RGBQUAD Palette[SCALE];
            //nIPresent = 0;
            //for ( UINT nCnt = 0; nCnt < SCALE; nCnt++)
            //{
            //    if (lkpR[nCnt] > 0){
            //        Palette[nCnt].rgbRed = (BYTE) (((fRed[nIPresent]-minR)/(maxR-minR))*IMAX);
            //        Palette[nCnt].rgbGreen = (BYTE) (((fGreen[nIPresent]-minG)/(maxG-minG))*IMAX);
            //        Palette[nCnt].rgbBlue = (BYTE) (((fBlue[nIPresent]-minB)/(maxB-minB))*IMAX);
            //        Palette[nCnt].rgbReserved = 0;               
            //        nIPresent++;
            //    }
            //}

            //imgInfo.SetPalette(Palette);

            //for(UINT nCnt = 0; nCnt < nImgSize; nCnt++) {
            //    imgInfo.SetRaster()[nCnt] = imgInfo.GetRaster()[nCnt];
            //}

            return dest;
        }

        #endregion

        #region HISTOGRAM

        public Bitmap HistogramCumulative()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] lkpR = new int[CDefinitions.SCALE];
                int[] lkpG = new int[CDefinitions.SCALE];
                int[] lkpB = new int[CDefinitions.SCALE];

                GetHistogramLkp(ref lkpR, ref lkpG, ref lkpB);

                // Number of pixels for previous gray level is added to next gray level
                // i.e for last Gray level, Number of pixels will be Image Width * Image Height.
                for (int nCnt = 1; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    lkpR[nCnt] += lkpR[nCnt - 1];
                    lkpG[nCnt] += lkpG[nCnt - 1];
                    lkpB[nCnt] += lkpB[nCnt - 1];
                }

                int nImgSize = m_nWidth * m_nHeight;
                // Modify the lookup table (CDefinitions.IMAX - 0) * Lookup Table / nImgSize
                int r; int g; int b;
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    r = (int)(255.0f * lkpR[nCnt] / nImgSize);
                    if (r < 0) r = 0;
                    if (r > CDefinitions.IMAX) r = CDefinitions.IMAX;
                    lkpR[nCnt] = r;

                    g = (int)(255.0f * lkpG[nCnt] / nImgSize);
                    if (g < 0) g = 0;
                    if (g > CDefinitions.IMAX) g = CDefinitions.IMAX;
                    lkpG[nCnt] = g;

                    b = (int)(255.0f * lkpB[nCnt] / nImgSize);
                    if (b < 0) b = 0;
                    if (b > CDefinitions.IMAX) b = CDefinitions.IMAX;
                    lkpB[nCnt] = b;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(
                            (byte)lkpR[clr.R],
                            (byte)lkpG[clr.G],
                            (byte)lkpB[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap HistogramEqualization()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nImgSize = m_nWidth * m_nHeight;

                int[] lkpR = new int[CDefinitions.SCALE];
                int[] lkpG = new int[CDefinitions.SCALE];
                int[] lkpB = new int[CDefinitions.SCALE];
                GetHistogramLkp(ref lkpR, ref lkpG, ref lkpB);

                // Calculating the maximum and minimum intensity in the image
                byte minR = 0; byte minG = 0; byte minB = 0;
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    if (lkpR[nCnt] > 0 && 0 == minR)
                        minR = (byte)nCnt;
                    if (lkpG[nCnt] > 0 && 0 == minG)
                        minG = (byte)nCnt;
                    if (lkpB[nCnt] > 0 && 0 == minB)
                        minB = (byte)nCnt;

                    if (minR > 0 && minG > 0 && minB > 0)
                        break;
                }
                byte maxR = 0; byte maxG = 0; byte maxB = 0;
                for (int nCnt = CDefinitions.IMAX; nCnt > 0; nCnt--)
                {
                    if (lkpR[nCnt] > 0 && 0 == maxR)
                        maxR = (byte)nCnt;
                    if (lkpG[nCnt] > 0 && 0 == maxG)
                        maxG = (byte)nCnt;
                    if (lkpB[nCnt] > 0 && 0 == maxB)
                        maxB = (byte)nCnt;

                    if (maxR > 0 && maxG > 0 && maxB > 0)
                        break;
                }

                int r, g, b;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        r = (int)(255.0f * (clr.R - minR) / (maxR - minR));
                        if (r < 0) r = 0;
                        if (r > CDefinitions.IMAX) r = CDefinitions.IMAX;

                        g = (int)(255.0f * (clr.G - minG) / (maxG - minG));
                        if (g < 0) g = 0;
                        if (g > CDefinitions.IMAX) g = CDefinitions.IMAX;

                        b = (int)(255.0f * (clr.G - minB) / (maxB - minB));
                        if (b < 0) b = 0;
                        if (b > CDefinitions.IMAX) b = CDefinitions.IMAX;

                        dest.SetPixel(nCol, nRow, Color.FromArgb((byte)r, (byte)g, (byte)b));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap HistogramExpansion(int nCentreI, float fSlope)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] lkpTable = new int[CDefinitions.SCALE];

                // Calculate Transform
                float fA, fB, fC;
                fA = (1.0f - fSlope) / (CDefinitions.IMAX * CDefinitions.IMAX - 3.0f * nCentreI * CDefinitions.IMAX + 3.0f * nCentreI * nCentreI);
                fB = -3.0f * fA * nCentreI;
                fC = fSlope + 3.0f * fA * nCentreI * nCentreI;

                float fSlopeTaper = 0.2f;
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    float fTmp = fA * nCnt * nCnt * nCnt + fB * nCnt * nCnt + fC * nCnt;
                    float y1 = fSlopeTaper * nCnt;
                    float y2 = fSlopeTaper * nCnt + CDefinitions.IMAX - fSlopeTaper * CDefinitions.IMAX;
                    if (fTmp < y1) fTmp = y1;
                    else if (fTmp > y2) fTmp = y2;
                    lkpTable[nCnt] = (byte)(fTmp + 0.5f);
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(
                            (byte)lkpTable[clr.R],
                            (byte)lkpTable[clr.G],
                            (byte)lkpTable[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap HistogramRamp(int nSlopeType)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] lkpR = new int[CDefinitions.SCALE];
                int[] lkpG = new int[CDefinitions.SCALE];
                int[] lkpB = new int[CDefinitions.SCALE];

                int nImgSize = m_nWidth * m_nHeight;

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        lkpR[clr.R]++;
                        lkpG[clr.G]++;
                        lkpB[clr.B]++;
                    }
                }

                int[] nDistR = new int[CDefinitions.SCALE];
                int[] nDistG = new int[CDefinitions.SCALE];
                int[] nDistB = new int[CDefinitions.SCALE];
                // Construct cumumlative distribution
                getHistogramDistribution(ref lkpR, ref nDistR, CDefinitions.SCALE);
                getHistogramDistribution(ref lkpG, ref nDistG, CDefinitions.SCALE);
                getHistogramDistribution(ref lkpB, ref nDistB, CDefinitions.SCALE);

                int[] lkpTransformR = new int[CDefinitions.SCALE];
                int[] lkpTransformG = new int[CDefinitions.SCALE];
                int[] lkpTransformB = new int[CDefinitions.SCALE];
                getHistogramTransformRamp(ref nDistR, ref lkpTransformR, nSlopeType);
                getHistogramTransformRamp(ref nDistG, ref lkpTransformG, nSlopeType);
                getHistogramTransformRamp(ref nDistB, ref lkpTransformB, nSlopeType);

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(
                            (byte)lkpTransformR[clr.R],
                            (byte)lkpTransformG[clr.G],
                            (byte)lkpTransformB[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void getHistogramDistribution(ref int[] nHist, ref int[] nDist, int nBins)
        {
            try
            {
                int nBinLow, nBinHigh;
                // find lowest and highest occupied bins
                for (nBinLow = 0; nBinLow < nBins; nBinLow++)
                    if (nHist[nBinLow] != 0)
                        break;
                for (nBinHigh = nBins - 1; nBinHigh >= 0; nBinHigh--)
                    if (nHist[nBinHigh] != 0)
                        break;

                // compute cumulative distribution
                for (int nCnt = 0; nCnt <= nBinLow; nCnt++)
                    nDist[nCnt] = 0;

                float fTotal = 0.0f; // floating point cumulative dist
                for (int nCnt = nBinLow + 1; nCnt <= nBinHigh; nCnt++)
                {
                    fTotal += ((nHist[nCnt - 1] + nHist[nCnt]) / 2.0f);
                    nDist[nCnt] = (int)(fTotal + 0.5f);
                }

                for (int nCnt = nBinHigh + 1; nCnt < nBins; nCnt++)
                    nDist[nCnt] = nDist[nCnt - 1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compute transform table for histogram equalization or histogram ramp
        private void getHistogramTransformRamp(ref int[] nDist, ref int[] lkpTransform, int nSlopeType)
        {
            try
            {
                // Total summation under cumulative distribution omits 1/2 of lowest
                // and highest occupied bins
                int nTotal = nDist[CDefinitions.SCALE - 1];

                int nSlope = 0;
                // Calculate Slope
                switch (nSlopeType)
                {
                    case -1:
                        nSlope = (int)(-2.0f * nTotal / (float)(CDefinitions.IMAX * CDefinitions.IMAX));
                        break;
                    case 0:
                        nSlope = 0;
                        break;
                    case 1:
                        nSlope = (int)(2.0f * nTotal / (float)(CDefinitions.IMAX * CDefinitions.IMAX));
                        break;
                }

                float fIntercept;   // histogram 0-intercept
                float fVal, fZ;

                switch (nSlopeType)
                {
                    // negative ramp
                    case -1:
                        fIntercept = nTotal / (float)CDefinitions.IMAX - nSlope / 2.0f * CDefinitions.IMAX;
                        fVal = fIntercept / nSlope;
                        for (int nCnt = 0; nCnt <= CDefinitions.IMAX; nCnt++)
                        {
                            fZ = fVal * fVal + 2.0f / nSlope * nDist[nCnt];
                            if (fZ < 0.0f) fZ = 0.0f; // due to quantization error
                            lkpTransform[nCnt] = (byte)(-fVal + 0.5f - System.Math.Sqrt(fZ));
                        }
                        break;
                    // histogram equalization
                    case 0:
                        fVal = ((float)CDefinitions.IMAX) / nTotal;
                        for (int nCnt = 0; nCnt <= CDefinitions.IMAX; nCnt++)
                        {
                            lkpTransform[nCnt] = (byte)(fVal * nDist[nCnt] + 0.5f);
                        }
                        break;
                    // positive ramp
                    case 1:
                        fIntercept = nTotal / (float)CDefinitions.IMAX - nSlope / 2.0f * CDefinitions.IMAX;
                        fVal = fIntercept / nSlope;
                        for (int nCnt = 0; nCnt <= CDefinitions.IMAX; nCnt++)
                        {
                            fZ = fVal * fVal + 2.0f / nSlope * nDist[nCnt];
                            if (fZ < 0.0f) fZ = 0.0f; // due to quantization error
                            lkpTransform[nCnt] = (byte)(-fVal + 0.5f + System.Math.Sqrt(fZ));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SCALING

        private void getLookupStop(ref byte[] lkp, byte byLower, byte byUpper, byte byIntensity)
        {
            for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
            {
                if ((nCnt < byLower) || (nCnt > byUpper))
                    lkp[nCnt] = byIntensity;
                else
                    lkp[nCnt] = (byte)nCnt;
            }
        }

        private void getLookupPass(ref byte[] lkp, byte byLower, byte byUpper, byte byIntensity)
        {
            for (int nCnt = 0; nCnt <= CDefinitions.SCALE - 1; nCnt++)
            {
                if ((nCnt < byLower) || (nCnt > byUpper))
                    lkp[nCnt] = (byte)nCnt;
                else
                    lkp[nCnt] = byIntensity;
            }
        }

        public Bitmap Band(byte byLower, byte byUpper, byte nR, byte nG, byte nB, byte nType)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                byte[] lkpR = new byte[CDefinitions.SCALE];
                byte[] lkpG = new byte[CDefinitions.SCALE];
                byte[] lkpB = new byte[CDefinitions.SCALE];

                switch (nType)
                {
                    case 0:
                        getLookupPass(ref lkpR, byLower, byUpper, nR);
                        getLookupPass(ref lkpG, byLower, byUpper, nG);
                        getLookupPass(ref lkpB, byLower, byUpper, nB);
                        break;
                    case 1:
                        getLookupStop(ref lkpR, byLower, byUpper, nR);
                        getLookupStop(ref lkpG, byLower, byUpper, nG);
                        getLookupStop(ref lkpB, byLower, byUpper, nB);
                        break;
                    default:
                        break;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(lkpR[clr.R],
                            lkpG[clr.G],
                            lkpB[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Invert(int nRLevel, int nGLevel, int nBLevel)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nR, nG, nB;

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        nR = nRLevel - clr.R; if (nR < 0) nR = 0;
                        nG = nGLevel - clr.G; if (nG < 0) nG = 0;
                        nB = nBLevel - clr.G; if (nR < 0) nB = 0;

                        dest.SetPixel(nCol, nRow, Color.FromArgb((byte)nR, (byte)nG, (byte)nB));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Linear(int nAdd, int nMultiply, int nFlag)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                byte[] lkpLinear = new byte[CDefinitions.SCALE];

                int nVal = 0;
                switch (nFlag)
                {
                    case 0:
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            nVal = (nCnt + nAdd);
                            if (nVal < 0) nVal = 0;
                            else if (nVal > CDefinitions.IMAX) nVal = CDefinitions.IMAX;
                            lkpLinear[nCnt] = (byte)nVal;
                        }
                        break;
                    case 1:
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            nVal = (nCnt * nMultiply);
                            if (nVal < 0) nVal = 0;
                            else if (nVal > CDefinitions.IMAX) nVal = CDefinitions.IMAX;
                            lkpLinear[nCnt] = (byte)nVal;
                        }
                        break;
                    case 2:
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            nVal = (nCnt * nMultiply) + nAdd;
                            if (nVal < 0) nVal = 0;
                            else if (nVal > CDefinitions.IMAX) nVal = CDefinitions.IMAX;
                            lkpLinear[nCnt] = (byte)nVal;
                        }
                        break;
                }//END SWITCH

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(lkpLinear[clr.R],
                            lkpLinear[clr.G],
                            lkpLinear[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Logarithm(int nIndex)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                byte[] lkpLog = new byte[CDefinitions.SCALE];
                double dLog = CDefinitions.IMAX / System.Math.Log(1.0 + nIndex);

                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    float fVal = nIndex * ((float)nCnt / CDefinitions.IMAX);
                    int nResult = (int)(dLog * System.Math.Log(1.0 + fVal));
                    if (nResult < 0) nResult = 0;
                    else if (nResult > CDefinitions.IMAX) nResult = CDefinitions.IMAX;
                    lkpLog[nCnt] = (byte)nResult;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(lkpLog[clr.R],
                            lkpLog[clr.G],
                            lkpLog[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap MaskLogic(int nMask, byte nLogic)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                byte[] lkpMask = new byte[CDefinitions.SCALE];

                switch (nLogic)
                {
                    case 0:
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            lkpMask[nCnt] = (byte)(nCnt & nMask);
                        }
                        break;
                    case 1:
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            lkpMask[nCnt] = (byte)(nCnt | nMask);
                        }
                        break;
                    case 2:
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            lkpMask[nCnt] = (byte)(nCnt ^ nMask);
                        }
                        break;
                    default:
                        break;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(lkpMask[clr.R],
                            lkpMask[clr.G],
                            lkpMask[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Mosaic(int nXSize, int nYSize)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                // Get the size of Matrices and the chunk size
                int nMatric_Size = nXSize * nYSize;
                int nChuck_Size = m_nWidth * nYSize;

                for (int nRow = 0; nRow < m_nHeight; nRow += nYSize)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol += nXSize)
                    {
                        int nSumR = 0, nSumG = 0, nSumB = 0;
                        for (int nBoxRow = 0; nBoxRow < nYSize; nBoxRow++)
                        {
                            for (int ixx = 0; ixx < nXSize; ixx++)
                            {
                                if ((nCol + ixx < m_nWidth) && (nRow + nBoxRow < m_nHeight))
                                {
                                    Color clr = SrcImage.GetPixel(nCol + ixx, nRow + nBoxRow);
                                    nSumR += clr.R; nSumG += clr.G; nSumB += clr.B;
                                }
                            }
                        }
                        int nMeanR = nSumR / nMatric_Size;
                        int nMeanG = nSumG / nMatric_Size;
                        int nMeanB = nSumB / nMatric_Size;
                        for (int nBoxRow = 0; nBoxRow < nYSize; nBoxRow++)
                        {
                            for (int nBoxCol = 0; nBoxCol < nXSize; nBoxCol++)
                            {
                                if ((nCol + nBoxCol < m_nWidth) && (nRow + nBoxRow < m_nHeight))
                                {
                                    dest.SetPixel(nCol + nBoxCol, nRow + nBoxRow,
                                        Color.FromArgb((byte)nMeanR, (byte)nMeanG, (byte)nMeanB));
                                }
                            }
                        }
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Power(double dPower)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                byte[] lkpPower = new byte[CDefinitions.SCALE];

                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    int nVal = (int)(CDefinitions.IMAX * System.Math.Pow((double)nCnt / CDefinitions.IMAX, dPower));

                    if (nVal < 0) nVal = 0;
                    else if (nVal > CDefinitions.IMAX) nVal = CDefinitions.IMAX;
                    lkpPower[nCnt] = (byte)nVal;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(lkpPower[clr.R],
                            lkpPower[clr.G],
                            lkpPower[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap StretchContrast(int nAlpha, int nBeta, int nGamma, int byLower, int byUpper)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] lkpTable = new int[CDefinitions.SCALE];
                int[] lkpContrast = new int[CDefinitions.SCALE];

                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    lkpTable[nCnt] = nCnt;
                }

                if (byLower > byUpper) Swap(ref byUpper, ref byLower);

                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    int u = lkpTable[nCnt];
                    if (u >= 0 && u < byLower)
                    {
                        lkpContrast[nCnt] = nAlpha * u;
                        if (lkpContrast[nCnt] > CDefinitions.IMAX) lkpContrast[nCnt] = CDefinitions.IMAX;
                    }
                    else if (u >= byLower && u < byUpper)
                    {
                        lkpContrast[nCnt] = (nBeta * (u - byLower) + (nAlpha * byLower));
                        if (lkpContrast[nCnt] > CDefinitions.IMAX) lkpContrast[nCnt] = CDefinitions.IMAX;
                    }
                    else if (u >= byUpper && u <= CDefinitions.IMAX)
                    {
                        lkpContrast[nCnt] = (nGamma * (u - byUpper) + (nBeta * (byUpper - byLower) + nAlpha * byLower));
                        if (lkpContrast[nCnt] > CDefinitions.IMAX) lkpContrast[nCnt] = CDefinitions.IMAX;
                    }
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB((byte)lkpContrast[clr.R], (byte)lkpContrast[clr.G], (byte)lkpContrast[clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Stretch3Sigma()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                byte[,] lkpTable = new byte[3, CDefinitions.SCALE];
                int[] nMean = new int[3];
                double[] dSigma = new double[3];

                getMeanAndSigma(ref nMean, ref dSigma);

                // Generarte LookUp Tabel. Wi formulauth u'=  K * ((u - nMean) / sd) + 128
                int nInt;
                for (int nColor = 0; nColor < 3; nColor++)
                    if (dSigma[nColor] < 0.1f)
                    {
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            nInt = (int)(nCnt - nMean[nColor] + 128.0f);

                            nInt = ((nInt < 0) ? 0 : nInt);
                            nInt = ((nInt > CDefinitions.IMAX) ? CDefinitions.IMAX : nInt);

                            lkpTable[nColor, nCnt] = (byte)nInt;
                        }
                    }
                    else
                    {
                        for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                        {
                            //page 184 gozalez
                            nInt = (int)(42.666f * ((nCnt - nMean[nColor]) / dSigma[nColor]) + 128.0f);
                            nInt = ((nInt < 0) ? 0 : nInt);
                            nInt = ((nInt > CDefinitions.IMAX) ? CDefinitions.IMAX : nInt);

                            lkpTable[nColor, nCnt] = (byte)nInt;
                        }
                    }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        RGB rgb = new RGB(lkpTable[0, clr.R], lkpTable[1, clr.G], lkpTable[2, clr.B]);
                        dest.SetPixel(nCol, nRow, rgb.Color);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getMeanAndSigma(ref int[] nMean, ref double[] dSigma)
        {
            try
            {
                // Calculate nMean and Standard Deviation.
                int nSumMeanR = 0; double dSumSqrR = 0.0;
                int nSumMeanG = 0; double dSumSqrG = 0.0;
                int nSumMeanB = 0; double dSumSqrB = 0.0;

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        nSumMeanR += clr.R;
                        dSumSqrR += (clr.R * clr.R);
                        nSumMeanG += clr.G;
                        dSumSqrG += (clr.G * clr.G);
                        nSumMeanB += clr.B;
                        dSumSqrB += (clr.B * clr.B);
                    }
                }

                int nImgSize = m_nHeight * m_nWidth;
                nMean[0] = nSumMeanR / nImgSize;
                dSigma[0] = System.Math.Sqrt(dSumSqrR / nImgSize - nMean[0] * nMean[0]);
                nMean[1] = nSumMeanG / nImgSize;
                dSigma[1] = System.Math.Sqrt(dSumSqrG / nImgSize - nMean[1] * nMean[1]);
                nMean[2] = nSumMeanB / nImgSize;
                dSigma[2] = System.Math.Sqrt(dSumSqrB / nImgSize - nMean[2] * nMean[2]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FILTERING

        public Bitmap Convolve(List<int> listMask, byte byMaskSize, int nMultiplier, int nDivisor)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = byMaskSize / 2; nRow < m_nHeight - byMaskSize / 2; nRow++)
                {
                    for (int nCol = byMaskSize / 2; nCol < m_nWidth - byMaskSize / 2; nCol++)
                    {
                        List<Color> listPixel = GetSrcPixel(nCol, nRow, byMaskSize);
                        Color clr = GetFilteredPixel(listPixel, listMask, nMultiplier, nDivisor);
                        dest.SetPixel(nCol, nRow, clr);
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Highpass(int nMask)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                List<int> listFilter = new List<int>();
                switch (nMask)
                {
                    case 0:
                        listFilter.Add(0); listFilter.Add(-1); listFilter.Add(0);
                        listFilter.Add(-1); listFilter.Add(5); listFilter.Add(-1);
                        listFilter.Add(0); listFilter.Add(-1); listFilter.Add(0);
                        break;
                    case 1:
                        listFilter.Add(-1); listFilter.Add(-1); listFilter.Add(-1);
                        listFilter.Add(-1); listFilter.Add(9); listFilter.Add(-1);
                        listFilter.Add(-1); listFilter.Add(-1); listFilter.Add(-1);
                        break;
                    case 2:
                        listFilter.Add(1); listFilter.Add(-2); listFilter.Add(2);
                        listFilter.Add(-2); listFilter.Add(5); listFilter.Add(-2);
                        listFilter.Add(2); listFilter.Add(-2); listFilter.Add(1);
                        break;
                    default:
                        break;
                }

                int nBase = m_nWidth;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        List<Color> listPixel = GetSrcPixel(nCol, nRow, 3);
                        Color clr = GetFilteredPixel(listPixel, listFilter, 1, 1);
                        dest.SetPixel(nCol, nRow, clr);
                    }
                    nBase += m_nWidth;
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Lowpass(int nMask)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                List<int> listFilter = new List<int>();
                int nDiv = 0;
                switch (nMask)
                {
                    case 0:
                        listFilter.Add(1); listFilter.Add(1); listFilter.Add(1);
                        listFilter.Add(1); listFilter.Add(1); listFilter.Add(1);
                        listFilter.Add(1); listFilter.Add(1); listFilter.Add(1);
                        nDiv = 9;
                        break;
                    case 1:
                        listFilter.Add(1); listFilter.Add(1); listFilter.Add(1);
                        listFilter.Add(1); listFilter.Add(2); listFilter.Add(1);
                        listFilter.Add(1); listFilter.Add(1); listFilter.Add(1);
                        nDiv = 10;
                        break;
                    case 2:
                        listFilter.Add(1); listFilter.Add(2); listFilter.Add(1);
                        listFilter.Add(2); listFilter.Add(4); listFilter.Add(2);
                        listFilter.Add(1); listFilter.Add(2); listFilter.Add(1);
                        nDiv = 16;
                        break;
                    default:
                        break;
                }

                int nBase = m_nWidth;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        List<Color> listPixel = GetSrcPixel(nCol, nRow, 3);
                        Color clr = GetFilteredPixel(listPixel, listFilter, 1, nDiv);
                        dest.SetPixel(nCol, nRow, clr);
                    }
                    nBase += m_nWidth;
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Mean(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (nCol == 0 || nCol == (m_nWidth - 1))
                        {
                            dest.SetPixel(nCol, nRow, SrcImage.GetPixel(nCol, nRow));
                            continue;
                        }
                        int nR = 0, nG = 0, nB = 0;
                        List<Color> listPixel = GetSrcPixel(nCol, nRow, 3);
                        nR += listPixel[0].R; nG += listPixel[0].G; nB += listPixel[0].B;
                        nR += listPixel[1].R; nG += listPixel[1].G; nB += listPixel[1].B;
                        nR += listPixel[2].R; nG += listPixel[2].G; nB += listPixel[2].B;
                        nR += listPixel[3].R; nG += listPixel[3].G; nB += listPixel[3].B;
                        nR += listPixel[4].R; nG += listPixel[4].G; nB += listPixel[4].B;
                        nR += listPixel[5].R; nG += listPixel[5].G; nB += listPixel[5].B;
                        nR += listPixel[6].R; nG += listPixel[6].G; nB += listPixel[6].B;
                        nR += listPixel[7].R; nG += listPixel[7].G; nB += listPixel[7].B;
                        nR += listPixel[8].R; nG += listPixel[8].G; nB += listPixel[8].B;
                        nR /= 9; nG /= 9; nB /= 9;

                        RGB rgb = new RGB((byte)System.Math.Abs(nR - listPixel[4].R),
                            (byte)System.Math.Abs(nG - listPixel[4].G),
                            (byte)System.Math.Abs(nB - listPixel[4].B));
                        if (CDefinitions.CLR2GREY(rgb.Color) >= nThreshold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb((byte)nR, (byte)nG, (byte)nB)); ;
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, listPixel[4]);
                        }
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Median(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (nCol == 0 || nCol == (m_nWidth - 1))
                        {
                            dest.SetPixel(nCol, nRow, SrcImage.GetPixel(nCol, nRow));
                            continue;
                        }
                        List<Color> listPixel = GetSrcPixel(nCol, nRow, 3);
                        Color clrCenter = listPixel[4];
                        listPixel.Sort(SortColor());
                        RGB rgb = new RGB(
                            (byte)System.Math.Abs(clrCenter.R - listPixel[4].R),
                            (byte)System.Math.Abs(clrCenter.G - listPixel[4].G),
                            (byte)System.Math.Abs(clrCenter.B - listPixel[4].B));

                        if (CDefinitions.CLR2GREY(rgb.Color) >= nThreshold)
                            dest.SetPixel(nCol, nRow, listPixel[4]);
                        else
                            dest.SetPixel(nCol, nRow, SrcImage.GetPixel(nCol, nRow));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Mode(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (nCol == 0 || nCol == (m_nWidth - 1))
                        {
                            dest.SetPixel(nCol, nRow, SrcImage.GetPixel(nCol, nRow));
                            continue;
                        }
                        List<Color> listPixel = GetSrcPixel(nCol, nRow, 3);
                        Color clrMax = GetMaxFreqCount(listPixel);
                        RGB rgb = new RGB(
                            (byte)System.Math.Abs(clrMax.R - listPixel[4].R),
                            (byte)System.Math.Abs(clrMax.G - listPixel[4].G),
                            (byte)System.Math.Abs(clrMax.B - listPixel[4].B));
                        if (CDefinitions.CLR2GREY(rgb.Color) >= nThreshold)
                            dest.SetPixel(nCol, nRow, listPixel[4]);
                        else
                            dest.SetPixel(nCol, nRow, SrcImage.GetPixel(nCol, nRow));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap StatDifference(int nSize, int nMean_d, int nStd_dev, int nAmpl_fac, int nAlpha)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                //int nSumR, nSumSqrR, nR, nDiffR, nSigmaR, nMeanR;
                //int nSumG, nSumSqrG, nG, nDiffG, nSigmaG, nMeanG;
                //int nSumB, nSumSqrB, nB, nDiffB, nSigmaB, nMeanB;

                //int nBSize = 2 * nSize + 1;
                //int nMatSize = nBSize * nBSize;

                //int nImgSizeMinuslWidth = m_nWidth * m_nHeight - m_nWidth;
                //int nSizeXWidth = nSize * m_nWidth;

                //Color rgb;
                //CRect rect;
                //int nBase = 0;
                //for (int nRow = 0; nRow < m_nHeight; nRow++)
                //{
                //    rect.X = nBase - nSizeXWidth; rect.bottom = nBase + nSizeXWidth;
                //    if (rect.top < 0) rect.top = 0;
                //    if (rect.bottom > nImgSizeMinuslWidth) rect.bottom = nImgSizeMinuslWidth;

                //    for (int nCol = 0; nCol < m_nWidth; nCol++)
                //    {
                //        rect.left = nCol - nSize;
                //        rect.right = nCol + nSize;
                //        if (rect.left < 0) rect.left = 0;
                //        if (rect.right > (m_nWidth - 1)) rect.right = (m_nWidth - 1);

                //        nSumR = nSumSqrR = 0; nSumG = nSumSqrG = 0; nSumB = nSumSqrB = 0;
                //        for (int iyy = rect.top; iyy < rect.bottom; iyy += m_nWidth)
                //        {
                //            for (int ixx = rect.left; ixx <= rect.right; ixx++)
                //            {
                //                Color clrSrc = SrcImage.GetPixel(ixx, iyy);
                //                nSumR += (nR = clrSrc.R);
                //                nSumSqrR += (nR * nR);
                //                nSumG += (nG = clrSrc.G);
                //                nSumSqrG += (nG * nG);
                //                nSumB += (nB = clrSrc.B);
                //                nSumSqrB += (nB * nB);
                //            }
                //        }
                //        nMeanR = nSumR / nMatSize; nMeanG = nSumG / nMatSize; nMeanB = nSumB / nMatSize;
                //        nSigmaR = (int)(Math.Sqrt((double)nSumSqrR / nMatSize - nMeanR * nMeanR));
                //        nSigmaG = (int)(Math.Sqrt((double)nSumSqrG / nMatSize - nMeanG * nMeanG));
                //        nSigmaB = (int)(Math.Sqrt((double)nSumSqrB / nMatSize - nMeanB * nMeanB));

                //        Color clr = SrcImage.GetPixel(nCol, nRow);
                //        nDiffR = clr.R - nMeanR;
                //        nDiffR *= (nAmpl_fac * nStd_dev);
                //        nDiffR /= (nAmpl_fac * nSigmaR + nStd_dev);

                //        nDiffG = clr.G - nMeanG;
                //        nDiffG *= (nAmpl_fac * nStd_dev);
                //        nDiffG /= (nAmpl_fac * nSigmaG + nStd_dev);

                //        nDiffB = clr.B - nMeanB;
                //        nDiffB *= (nAmpl_fac * nStd_dev);
                //        nDiffB /= (nAmpl_fac * nSigmaB + nStd_dev);

                //        nR = (nAlpha * nMean_d + (100 - nAlpha) * nMeanR) / 100;
                //        nR += nDiffR;
                //        nR = (nR < 0) ? 0 : nR; nR = (nR > CDefinitions.IMAX) ? CDefinitions.IMAX : nR;

                //        nG = (nAlpha * nMean_d + (100 - nAlpha) * nMeanG) / 100;
                //        nG += nDiffG;
                //        nG = (nG < 0) ? 0 : nG; nG = (nG > CDefinitions.IMAX) ? CDefinitions.IMAX : nG;

                //        nB = (nAlpha * nMean_d + (100 - nAlpha) * nMeanB) / 100;
                //        nB += nDiffB;
                //        nR = (nB < 0) ? 0 : nB; nB = (nB > CDefinitions.IMAX) ? CDefinitions.IMAX : nB;

                //        RGB rgbDest = new RGB((byte)nR, (byte)nG, (byte)nB);
                //        dest.SetPixel(nCol, nRow, rgbDest.Color);
                //    }
                //    nBase += m_nWidth;
                //}
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap SubcarrierKill(double dFreq)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] mTab = new int[CDefinitions.SCALE];
                byte[] dTab = new byte[1024];

                double dAngle = 2.0 * CDefinitions.PI * 4.43 / dFreq;
                double dMult = -2.0 * System.Math.Cos(dAngle);

                // Accumulating the cos(Angle) * nCnt
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    mTab[nCnt] = (int)(dMult * nCnt);
                }

                int nI = 0;
                for (int nCnt = 0; nCnt < 1024; nCnt++)
                {
                    nI = (int)(nCnt / (2.0 + dMult));
                    nI = (nI > CDefinitions.IMAX) ? CDefinitions.IMAX : nI;
                    dTab[nCnt] = (byte)nI;
                }

                List<Color> listCurrLine = new List<Color>();
                List<Color> listNewLine = new List<Color>();

                int nR = 0, nG = 0, nB = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        listCurrLine.Add(SrcImage.GetPixel(nCol, nRow));
                    }
                    listNewLine.Add(listCurrLine[0]);

                    for (int nCol = 1; nCol < m_nWidth - 1; nCol++)
                    {
                        nI = CDefinitions.CLR2GREY(listCurrLine[nCol]);
                        AddPixels(
                            listCurrLine[nCol - 1],
                            Color.FromArgb((byte)mTab[nI], (byte)mTab[nI], (byte)mTab[nI]),
                            listCurrLine[nCol + 1],
                            ref nR, ref nG, ref nB);
                        nR = (nR > 1023) ? 1023 : nR;
                        nR = (nG > 1023) ? 1023 : nG;
                        nB = (nB > 1023) ? 1023 : nB;
                        RGB rgbNew = new RGB(dTab[nR], dTab[nG], dTab[nB]);
                        listNewLine.Add(rgbNew.Color);
                    }
                    listNewLine.Add(listCurrLine[m_nWidth - 1]);
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dest.SetPixel(nCol, nRow, listNewLine[nCol]);
                    }
                    listCurrLine.Clear();
                    listNewLine.Clear();
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap VectorQuantization(int nQuantization)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nShift = 8 - nQuantization;
                int nLevel = nQuantization == 1 ? 0 : nQuantization;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int I = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol, nRow)) >> nShift;
                        I = (int)((I * CDefinitions.IMAX) / System.Math.Pow(2.0, nLevel));
                        dest.SetPixel(nCol, nRow, Color.FromArgb(I, I, I));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
