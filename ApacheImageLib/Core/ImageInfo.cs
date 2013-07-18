using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Apache.ImageLib
{
    public class CImageInfo
    {
        #region CTOR

        protected int m_nWidth = -1;
        protected int m_nHeight = -1;

        #endregion

        #region DELEGATES

        public static IComparer<Color> SortColor()
        {
            return (IComparer<Color>)new CSortColors();
        }

        #endregion

        #region CTOR

        public CImageInfo(Bitmap srcImage)
        {
            m_bmpSrc = srcImage;
            m_nWidth = m_bmpSrc.Width;
            m_nHeight = m_bmpSrc.Height;
        }

        public CImageInfo(int nWidth, int nHeight)
        {
            m_nWidth = nWidth;
            m_nHeight = nHeight;
            m_bmpSrc = new Bitmap(m_nWidth, m_nHeight);
        }

        #endregion

        #region PROPERTY

        protected Bitmap m_bmpSrc;
        public Bitmap SrcImage
        {
            get { return m_bmpSrc; }
            set { m_bmpSrc = value; }
        }

        public System.Drawing.Imaging.PixelFormat PixelFormat
        {
            get { return m_bmpSrc.PixelFormat; }
        }

        // Apply Filter
        protected Bitmap applyFilter(IFilter filter)
        {
            return filter.Apply(m_bmpSrc);
        }

        protected void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public byte GetGreyPixel(int nCol, int nRow)
        {
            Color clr = SrcImage.GetPixel(nCol, nRow);
            return (byte)(clr.R * 0.3f + clr.G * 0.59f + clr.B * 0.11f);
        }

        public byte GetAvgPixel(int nCol, int nRow)
        {
            try
            {
                Color clr = SrcImage.GetPixel(nCol, nRow);
                return (byte)((clr.R + clr.G + clr.B) / 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ADJUSTMENTS

        protected void AddPixels(Color clr1, Color clr2, Color clr3, ref int nR, ref int nG, ref int nB)
        {
            try
            {
                nR = clr1.R + clr2.R + clr3.R;
                nG = clr1.G + clr2.G + clr3.G;
                nB = clr1.B + clr2.B + clr3.B;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Color> GetSrcPixel(int nCol, int nRow, int nMaskSize)
        {
            try
            {
                List<Color> listPixel = new List<Color>();
                for (int i = -nMaskSize / 2; i <= nMaskSize / 2; i++)
                    for (int j = -nMaskSize / 2; j <= nMaskSize / 2; j++)
                    {
                        Color clr = m_bmpSrc.GetPixel(nCol + j, nRow + i);
                        listPixel.Add(clr);
                    }

                return listPixel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> Get2x2Pixel(int nCol, int nRow)
        {
            try
            {
                List<int> listPixel = new List<int>();
                for (int i = 0; i <= 1; i++)
                    for (int j = 0; j <= 1; j++)
                    {
                        Color clr = m_bmpSrc.GetPixel(nCol + j, nRow + i);
                        listPixel.Add(CDefinitions.CLR2GREY(clr));
                    }

                return listPixel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> Get3x3Pixel(int nCol, int nRow)
        {
            try
            {
                List<int> listPixel = new List<int>();
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        Color clr = m_bmpSrc.GetPixel(nCol + j, nRow + i);
                        listPixel.Add(CDefinitions.CLR2GREY(clr));
                    }

                return listPixel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Get3x3PixelSum(int nCol, int nRow)
        {
            try
            {
                int nSumPixel = 0;
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        Color clr = m_bmpSrc.GetPixel(nCol + j, nRow + i);
                        nSumPixel += CDefinitions.CLR2AVR(clr);
                    }

                return nSumPixel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected Color GetFilteredPixel(List<Color> listPixel, List<int> listMask, int nMultiplier, int nDivisor)
        {
            try
            {
                int nR = 0, nG = 0, nB = 0;
                for (int nCnt = 0; nCnt < listPixel.Count; nCnt++)
                {
                    nR += listPixel[nCnt].R * listMask[nCnt];
                    nG += listPixel[nCnt].G * listMask[nCnt];
                    nB += listPixel[nCnt].B * listMask[nCnt];
                }
                if (0 != nDivisor)
                {
                    nR *= nMultiplier; nG *= nMultiplier; nB *= nMultiplier;
                    nR /= nDivisor; nG /= nDivisor; nB /= nDivisor;
                }
                nR = (nR > CDefinitions.IMAX) ? CDefinitions.IMAX : nR;
                nG = (nG > CDefinitions.IMAX) ? CDefinitions.IMAX : nG;
                nB = (nB > CDefinitions.IMAX) ? CDefinitions.IMAX : nB;
                nR = (nR < CDefinitions.IMIN) ? CDefinitions.IMIN : nR;
                nG = (nG < CDefinitions.IMIN) ? CDefinitions.IMIN : nG;
                nB = (nB < CDefinitions.IMIN) ? CDefinitions.IMIN : nB;

                return Color.FromArgb((byte)nR, (byte)nG, (byte)nB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected int GetFilteredPixel(List<int> listPixel, int[] arrFilter)
        {
            try
            {
                int nPixel = 0;
                for (int nCnt = 0; nCnt < listPixel.Count; nCnt++)
                {
                    nPixel += listPixel[nCnt] * arrFilter[nCnt];
                }
                return nPixel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected Color GetMaxFreqCount(List<Color> listPixel)
        {
            try
            {
                int[] lkpR = new int[CDefinitions.SCALE];
                int[] lkpG = new int[CDefinitions.SCALE];
                int[] lkpB = new int[CDefinitions.SCALE];

                int nMinR = CDefinitions.IMAX; int nMaxR = 0;
                int nMinG = CDefinitions.IMAX; int nMaxG = 0;
                int nMinB = CDefinitions.IMAX; int nMaxB = 0;
                // Find MIN_MAX in the 3x3 sample.
                for (int nCnt = 0; nCnt < listPixel.Count; nCnt++)
                {
                    nMinR = (listPixel[nCnt].R < nMinR) ? listPixel[nCnt].R : nMinR;
                    nMaxR = (listPixel[nCnt].R > nMaxR) ? listPixel[nCnt].R : nMaxR;
                    nMinG = (listPixel[nCnt].G < nMinG) ? listPixel[nCnt].G : nMinG;
                    nMaxG = (listPixel[nCnt].G > nMaxG) ? listPixel[nCnt].G : nMaxG;
                    nMinB = (listPixel[nCnt].B < nMinB) ? listPixel[nCnt].B : nMinB;
                    nMaxB = (listPixel[nCnt].B > nMaxB) ? listPixel[nCnt].B : nMaxB;

                    lkpR[listPixel[nCnt].R]++;
                    lkpG[listPixel[nCnt].G]++;
                    lkpB[listPixel[nCnt].B]++;
                }

                // Default Central Pixel 
                RGB rgbMax = new RGB(listPixel[4]);

                //Get CDefinitions.IMAX the frequency Count.
                int nCount = 0;
                for (int nCnt = nMinR; nCnt <= nMaxR; nCnt++)
                {
                    if (lkpR[nCnt] > nCount)
                    {
                        nCount = lkpR[nCnt];
                        rgbMax.Red = (byte)nCnt;
                    }
                }
                nCount = 0;
                for (int nCnt = nMinG; nCnt <= nMaxG; nCnt++)
                {
                    if (lkpG[nCnt] > nCount)
                    {
                        nCount = lkpG[nCnt];
                        rgbMax.Green = (byte)nCnt;
                    }
                }
                nCount = 0;
                for (int nCnt = nMinB; nCnt <= nMaxB; nCnt++)
                {
                    if (lkpB[nCnt] > nCount)
                    {
                        nCount = lkpB[nCnt];
                        rgbMax.Blue = (byte)nCnt;
                    }
                }

                return rgbMax.Color;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GetHistogramLkp(ref int[] lkpR, ref int[] lkpG, ref int[] lkpB)
        {
            try
            {
                // Get the number of pixels for each Gray Level
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = m_bmpSrc.GetPixel(nCol, nRow);
                        lkpR[clr.R]++;
                        lkpG[clr.G]++;
                        lkpB[clr.B]++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected int getMax4(int a, int b, int c, int d)
        {
            return CDefinitions.MAX(CDefinitions.MAX(a, b), CDefinitions.MAX(c, d));
        }

        protected int getMin4Nz(int a, int b, int c, int d)
        {
            try
            {
                if (a == 0) a = 0x7fffffff;
                if (b == 0) b = 0x7fffffff;
                if (c == 0) c = 0x7fffffff;
                if (d == 0) d = 0x7fffffff;
                return CDefinitions.MIN(CDefinitions.MIN(a, b), CDefinitions.MIN(c, d));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void createBitmap(byte[,] src, ref Bitmap dest)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(src[nRow, nCol], src[nRow, nCol], src[nRow, nCol]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region STATS

        protected bool isBinaryImage()
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (GetAvgPixel(nCol, nRow) != 0 && GetAvgPixel(nCol, nRow) != CDefinitions.IMAX)
                        {
                            MessageBox.Show("This is not a binary image.");
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CountPixel(int nLowT, int nHighT, ref int nPixelCount, ref double dPhasePercent)
        {
            try
            {
                int nVal = 0;
                nPixelCount = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        nVal = GetAvgPixel(nCol, nRow);

                        if (nVal >= nLowT && nVal <= nHighT) nPixelCount++;
                    }
                }

                dPhasePercent = (100.0 * nPixelCount) / m_nWidth * m_nHeight;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ImageStatistics(ref int nMinGrey, ref int nMaxGrey, ref double dMean, ref double dMeanSquare,
                ref double dVariance, ref double dStdDeviation, ref double dEntropy)
        {
            try
            {
                int[] arrHistogram = new int[CDefinitions.SCALE];

                double dSum = 0.0, dSqrSum = 0.0;
                nMinGrey = CDefinitions.IMAX; nMaxGrey = 0;

                // Calculate minGrayLevel, maxGrayLevel and mean.
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nVal = GetAvgPixel(nCol, nRow);
                        dSum += nVal;
                        dSqrSum += (nVal * nVal);
                        arrHistogram[nVal]++;

                        // Calculate the minimum and maximum Intensity
                        nMinGrey = (nVal < nMinGrey) ? nVal : nMinGrey;
                        nMaxGrey = (nVal > nMaxGrey) ? nVal : nMaxGrey;
                    }
                }

                dMean = dSum / (m_nWidth * m_nHeight);		//5
                dMeanSquare = dSqrSum / (m_nWidth * m_nHeight);	//6

                // Calculate Variance.
                dVariance = dMeanSquare - (dMean * dMean);		//7
                if (dVariance < Double.MinValue) dVariance = 0.0;

                // Calculate Standard Deviation. sd = Math.Sqrt (((U - mean) ^ 2) / m_nWidth*m_nHeight ).
                dStdDeviation = Math.Sqrt(dVariance);	//8

                double[] dProbabilityArray = new double[CDefinitions.SCALE];
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    dProbabilityArray[nCnt] = arrHistogram[nCnt] / (double)(m_nWidth * m_nHeight);
                }

                // Calculate Entropy.
                double dLogResult = 0.0; dEntropy = 0.0;
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    if (dProbabilityArray[nCnt] != 0.0)
                    {
                        dLogResult = Math.Log10(dProbabilityArray[nCnt]) / Math.Log10(2.0);
                        dEntropy -= dProbabilityArray[nCnt] * dLogResult;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
