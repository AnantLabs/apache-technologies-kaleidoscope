using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Apache.ImageLib
{
    public class CFeatures : CRegions
    {
        #region CTOR

        public CFeatures(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        #region THRESHOLD

        public Bitmap GlobalThreshold(int nThreshold, int nPercent, ref byte[,] vTempRaster)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                if (nPercent != 0)
                {
                    int[] lkpR = new int[CDefinitions.SCALE];
                    int[] lkpG = new int[CDefinitions.SCALE];
                    int[] lkpB = new int[CDefinitions.SCALE];

                    GetHistogramLkp(ref lkpR, ref lkpG, ref lkpB);

                    nPercent *= m_nWidth * m_nHeight;
                    nPercent /= 100;

                    int nSum = 0; nThreshold = CDefinitions.IMAX;
                    while (nSum < nPercent && nThreshold >= 0)
                    {
                        nSum += lkpR[nThreshold--];
                    }
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        if (CDefinitions.CLR2GREY(clr) >= nThreshold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                            vTempRaster[nRow, nCol] = CDefinitions.IMAX;
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
                            vTempRaster[nRow, nCol] = CDefinitions.IMIN;
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

        public Bitmap AdaptiveThreshold(int nMinRange)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nRange, nThreshold = 0;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        List<int> listPixel = Get3x3Pixel(nCol, nRow);
                        listPixel.Sort();

                        nRange = listPixel[8] - listPixel[0];
                        if (nRange > nMinRange)
                            nThreshold = (listPixel[8] + listPixel[0]) / 2;
                        else
                            nThreshold = listPixel[8] - (nMinRange / 2);

                        byte byPixel = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol, nRow));
                        if (byPixel >= nThreshold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        #endregion

        #region EDGEDETECTION

        public Bitmap Canny()
        {
            try
            {
                Bitmap dest = null;
                CannyEdgeDetector filter = new CannyEdgeDetector();
                dest = applyFilter(filter);
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Bitmap EuclideanDistanceColor(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                double v11, v12, v13, v21, v22, v23;
                double dGrad, dD;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < (m_nWidth - 5); nCol++)
                    {
                        // Get the values from the image
                        v11 = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol, nRow));
                        v12 = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol + 1, nRow));
                        v13 = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol + 2, nRow));

                        v21 = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol + 3, nRow));
                        v22 = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol + 4, nRow));
                        v23 = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol + 5, nRow));

                        dGrad = System.Math.Abs(System.Math.Pow((v11 - v21), 2.0) +
                            System.Math.Pow((v12 - v22), 2.0) +
                            System.Math.Pow((v13 - v23), 2.0));
                        dD = System.Math.Sqrt(dGrad);
                        if (dD >= nThreshold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        public Bitmap Krisch(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nTmpX, nTmpY;
                int T_Hold = nThreshold * nThreshold;
                int[] filterX = { -3, -3, 5, -3, 0, 5, -3, -3, 5 };
                int[] filterY = { -3, 5, 5, -3, 0, 5, -3, -3, -3 };
                List<int> listPixel = new List<int>();
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get3x3Pixel(nCol, nRow);
                        nTmpX = GetFilteredPixel(listPixel, filterX);
                        nTmpY = GetFilteredPixel(listPixel, filterY);

                        if (nTmpX * nTmpX + nTmpY * nTmpY >= T_Hold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        public Bitmap LOG4(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[,] vGaussian = new int[m_nHeight, m_nWidth];
                List<int> listPixel;
                int[] filterG = { 1, 1, 1, 1, 2, 1, 1, 1, 1 };
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get3x3Pixel(nCol, nRow);
                        vGaussian[nRow, nCol] = GetFilteredPixel(listPixel, filterG) / 10;
                    }
                }

                int nTmp = 0;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        nTmp = (//vGaussian[nRow - 1, nCol-1]*0+
                                vGaussian[nRow - 1, nCol] * 1 +
                            //vGaussian[nRow - 1, nCol+1]*0+ 
                                vGaussian[nRow, nCol - 1] * 1 +
                                vGaussian[nRow, nCol] * -4 +
                                vGaussian[nRow, nCol + 1] * 1 +
                            //vGaussian[nRow+1, nCol-1]*0+
                                vGaussian[nRow + 1, nCol] * 1
                            //vGaussian[nRow+1,nCol+1]*0 
                              );

                        if (nTmp > nThreshold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        public Bitmap LOG8(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[,] vGaussian = new int[m_nHeight, m_nWidth];
                List<int> listPixel;
                int[] filterG = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };

                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get3x3Pixel(nCol, nRow);
                        vGaussian[nRow, nCol] = GetFilteredPixel(listPixel, filterG) / 16;
                    }
                }

                int nTmp = 0;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        nTmp = (vGaussian[nRow - 1, nCol - 1] * 1 +
                                vGaussian[nRow - 1, nCol] * 1 +
                                vGaussian[nRow - 1, nCol + 1] * 1 +
                                vGaussian[nRow, nCol - 1] * (1) +
                                vGaussian[nRow, nCol] * (-8) +
                                vGaussian[nRow, nCol + 1] * (1) +
                                vGaussian[nRow + 1, nCol - 1] * 1 +
                                vGaussian[nRow + 1, nCol] * 1 +
                                vGaussian[nRow + 1, nCol + 1] * 1
                              );

                        if (nTmp > nThreshold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        public Bitmap Prewitt(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nTmpX, nTmpY;
                int T_Hold = nThreshold * nThreshold;
                int[] filterX = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };
                int[] filterY = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };

                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        List<int> listPixel = Get3x3Pixel(nCol, nRow);
                        nTmpX = GetFilteredPixel(listPixel, filterX);
                        nTmpY = GetFilteredPixel(listPixel, filterY);

                        if (nTmpX * nTmpX + nTmpY * nTmpY >= T_Hold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        public Bitmap Robert(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] filterX = { 0, 1, -1, 0 };
                int[] filterY = { 1, 0, 0, -1 };
                int T_Hold = nThreshold * nThreshold;
                List<int> listPixel;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get2x2Pixel(nCol, nRow);
                        int nTmpX = GetFilteredPixel(listPixel, filterX);
                        int nTmpY = GetFilteredPixel(listPixel, filterY);

                        if (nTmpX * nTmpX + nTmpY * nTmpY >= T_Hold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
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

        public Bitmap Robinson3L(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int T_Hold = nThreshold * nThreshold;
                int[] filterX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
                int[] filterY = { 0, 1, 1, -1, 0, 1, -1, -1, 0 };
                List<int> listPixel;
                int nBase = m_nWidth;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get3x3Pixel(nCol, nRow);
                        int nTmpX = GetFilteredPixel(listPixel, filterX);
                        int nTmpY = GetFilteredPixel(listPixel, filterY);

                        if (nTmpX * nTmpX + nTmpY * nTmpY >= T_Hold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
                        }
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

        public Bitmap Robinson5L(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int T_Hold = nThreshold;
                int[] filterX = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
                int[] filterY = { 0, 1, 2, -1, 0, 1, -2, -1, 0 };
                List<int> listPixel;
                int nBase = m_nWidth;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get3x3Pixel(nCol, nRow);
                        int nTmpX = GetFilteredPixel(listPixel, filterX);
                        int nTmpY = GetFilteredPixel(listPixel, filterY);

                        if (nTmpX * nTmpX + nTmpY * nTmpY >= T_Hold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
                        }
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

        public Bitmap Sobel(int nThreshold)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int[] filterX = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
                int[] filterY = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };

                int T_Hold = nThreshold * nThreshold;
                List<int> listPixel;
                int nBase = m_nWidth;
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        listPixel = Get3x3Pixel(nCol, nRow);
                        int nTmpX = GetFilteredPixel(listPixel, filterX);
                        int nTmpY = GetFilteredPixel(listPixel, filterY);

                        if (nTmpX * nTmpX + nTmpY * nTmpY >= T_Hold)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
                        }
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

        #endregion

        #region REGION_LABEL

        //Function		 : RegionCount
        //Description		 : this function labels connected objects by region 
        //                    extraction method. the input image has to a binary 
        //                    image consisting of intensities 0 and CDefinitions.IMAX only. a 
        //                    Negate flag may be specified which when set to 1 
        //                    causes the input image to be negated before region 
        //                    extraction. this enables the labelling of black 
        //                    objects as well. the algorithm works by labelling a
        //                    new non-zero pixel and then at the end pruning the 
        //                    clashing labels. the function returns the total no.
        //                    of regions found in the variable 'nObjects'. the 
        //                    labelling is carried out only if the no. of labels
        //                    found is less than CDefinitions.IMAX.
        //ReturnValue		 : 0 on success
        public int CountRegions(CDefinitions.REGION_TYPE eRegionType)
        {
            try
            {
                bool bLoop = true;
                for (int nRow = 0; nRow < m_nHeight && bLoop; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow)) != CDefinitions.IMIN &&
                            CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow)) != CDefinitions.IMAX)
                        {
                            MessageBox.Show("This is not binary image.");
                            bLoop = false;
                            break;
                        }
                    }
                }
                int nObjects = -1;
                getLabeledImg(eRegionType, false, ref nObjects);
                return nObjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Function		 : RegionLabel
        //Description		 : this function labels connected objects by region 
        //                   extraction method. the input image has to a binary 
        //                   image consisting of intensities 0 and IMAX only. a 
        //                   Negate flag may be specified which when set to 1 
        //                   causes the input image to be negated before region 
        //                   extraction. this enables the labelling of black 
        //                   objects as well. the algorithm works by labelling a
        //                   new non-zero pixel and then at the end pruning the 
        //                   clashing labels. the function returns the total no.
        //                   of regions found in the variable 'No_Of_Objects'. the 
        //                   labelling is carried out only if the no. of labels
        //                   found is less than IMAX.
        //ReturnValue		 : 0 on success
        public Bitmap LabelRegions(CDefinitions.REGION_TYPE eRegionType, bool bPseudoscolor)
        {
            bool bLoop = true;
            for (int nRow = 0; nRow < m_nHeight && bLoop; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    if (CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow)) != CDefinitions.IMIN &&
                        CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow)) != CDefinitions.IMAX)
                    {
                        MessageBox.Show("This is not binary image.");
                        bLoop = false;
                        break;
                    }
                }
            }

            int nObjects = -1;
            Bitmap dest = getLabeledImg(eRegionType, true, ref nObjects);
            if (bPseudoscolor)
            {
                dest = getStretchedImg(dest);
            }
            return dest;
        }

        #endregion

        #region SHAPE_ANALYSIS

        // Calculates the different moments in an image.
        public bool GeometricMoments(ref double[] vfMoments, ref double[] vfInvMoments, ref double[] vfBetaMoments)
        {
            try
            {
                double dSample = 0.0;
                double M00 = 0.0; double M01 = 0.0; double M10 = 0.0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dSample = GetGreyPixel(nCol, nRow);
                        M10 += dSample * nCol;
                        M01 += dSample * nRow;
                        M00 += dSample;
                    }
                }

                double dXBar, dYBar;
                if (M00 > CDefinitions.SMALLNO)
                {
                    dXBar = M10 / M00; dYBar = M01 / M00;
                }
                else
                {
                    MessageBox.Show("Error due to specified data is out of range");
                    return false;
                }

                double[] dYPower = new double[m_nHeight * 4];
                //dYPower = Enumerable.Repeat(0.0, m_nHeight * 4).ToArray();
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    dYPower[nRow] = 1.0;
                }

                int nBase = m_nHeight;
                for (int cntRow = 1; cntRow < 4; cntRow++)
                {
                    for (int nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        dYPower[nBase + nRow] = dYPower[nBase - m_nHeight + nRow] * (nRow - dYBar);
                    }
                    nBase += m_nHeight;
                }

                double[] dXPower = new double[m_nWidth * 4];
                //dYPower = Enumerable.Repeat(0.0, m_nWidth * 4).ToArray();
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    dXPower[nCol] = 1.0;
                }

                nBase = m_nWidth;
                for (int cntCol = 1; cntCol < 4; cntCol++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dXPower[nBase + nCol] = dXPower[nBase - m_nWidth + nCol] * (nCol - dXBar);
                    }
                    nBase += m_nWidth;
                }

                double dValue = 0.0;
                nBase = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dSample = GetGreyPixel(nCol, nRow);
                        for (int cntRow = 0; cntRow < 4; cntRow++)
                        {
                            dValue = dSample * dYPower[(m_nHeight * cntRow) + nRow];
                            for (int cntCol = 0; cntCol < 4; cntCol++)
                            {
                                vfMoments[(4 * cntRow) + cntCol] += (dValue * dXPower[(m_nWidth * cntCol) + nCol]);
                            }
                        }
                    }//nCol
                    nBase += m_nWidth;
                }//nRow

                double Temp1, Temp2;
                if (vfMoments[0] > CDefinitions.SMALLNO)
                {
                    dValue = vfMoments[0];
                    for (int cntRow = 0; cntRow < 4; cntRow++)
                    {
                        for (int cntCol = 0; cntCol < 4; cntCol++)
                        {
                            Temp1 = ((cntRow + cntCol) / 2.0) + 1.0;
                            Temp1 = Math.Pow(dValue, Temp1);
                            vfMoments[4 * cntRow + cntCol] /= Temp1;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error due to specified data is out of range");
                    return false;
                }

                vfInvMoments[0] = vfMoments[8] + vfMoments[2];

                vfInvMoments[1] = (vfMoments[8] - vfMoments[2]) * (vfMoments[8] - vfMoments[2]);
                vfInvMoments[1] += (4 * vfMoments[5] * vfMoments[5]);

                vfInvMoments[2] = (vfMoments[12] - 3 * vfMoments[6]) * (vfMoments[12] - 3 * vfMoments[6]);
                vfInvMoments[2] += (3 * vfMoments[9] - vfMoments[3]) * (3 * vfMoments[9] - vfMoments[3]);

                vfInvMoments[3] = (vfMoments[12] + vfMoments[6]) * (vfMoments[12] + vfMoments[6]);
                vfInvMoments[3] += (vfMoments[9] + vfMoments[3]) * (vfMoments[9] + vfMoments[3]);

                vfInvMoments[4] = (vfMoments[12] - 3.0 * vfMoments[6]) * (vfMoments[12] + vfMoments[6]);

                Temp1 = (vfMoments[12] + vfMoments[6]) * (vfMoments[12] + vfMoments[6]);
                Temp1 -= (3.0 * (vfMoments[9] + vfMoments[3]) * (vfMoments[9] + vfMoments[3]));
                vfInvMoments[4] *= Temp1;

                dValue = (3.0 * vfMoments[9] - vfMoments[3]) * (vfMoments[9] + vfMoments[3]);
                Temp1 = (3.0 * (vfMoments[12] + vfMoments[6])) * (vfMoments[12] + vfMoments[6]);
                Temp1 -= ((vfMoments[9] + vfMoments[3]) * (vfMoments[9] + vfMoments[3]));
                dValue *= Temp1;
                vfInvMoments[4] += dValue;

                vfInvMoments[5] = vfMoments[8] - vfMoments[2];
                Temp1 = (vfMoments[12] + vfMoments[6]) * (vfMoments[12] + vfMoments[6]);
                Temp1 -= ((vfMoments[9] + vfMoments[3]) * (vfMoments[9] + vfMoments[3]));
                vfInvMoments[5] *= Temp1;
                vfInvMoments[5] += (4.0 * vfMoments[5] * (vfMoments[12] + vfMoments[6]) * (vfMoments[9] + vfMoments[3]));

                vfInvMoments[6] = (3.0 * vfMoments[9] - vfMoments[3]) * (Temp1 = (vfMoments[12] + vfMoments[6]));
                dValue = Temp1 * Temp1;
                dValue -= (3.0 * (vfMoments[9] + vfMoments[3]) * (vfMoments[9] + vfMoments[3]));
                vfInvMoments[6] *= dValue;

                dValue = (vfMoments[12] - 3.0 * vfMoments[6]) * (vfMoments[9] + vfMoments[3]);
                Temp1 = 3.0 * (vfMoments[12] + vfMoments[6]) * (vfMoments[12] + vfMoments[6]);
                Temp1 -= ((vfMoments[9] + vfMoments[3]) * (vfMoments[9] + vfMoments[3]));
                dValue *= Temp1;
                vfInvMoments[6] -= dValue;

                vfBetaMoments[0] = Math.Sqrt(vfInvMoments[1]) / vfInvMoments[0];

                Temp1 = vfInvMoments[2] * vfMoments[0];
                Temp2 = vfInvMoments[1] * vfInvMoments[0];

                if (Temp2 == 0) Temp2 = 1;
                vfBetaMoments[1] = Temp1 / Temp2;

                if (vfInvMoments[2] == 0) vfInvMoments[2] = 1;
                vfBetaMoments[2] = vfInvMoments[3] / vfInvMoments[2];

                if (vfInvMoments[3] == 0) vfInvMoments[3] = 1;
                vfBetaMoments[3] = Math.Sqrt(Math.Abs(vfInvMoments[4]) / vfInvMoments[3]);

                Temp1 = vfInvMoments[3] * vfInvMoments[0];
                if (Temp1 == 0) Temp1 = 1;

                vfBetaMoments[4] = vfInvMoments[5] / Temp1;
                if (vfInvMoments[4] == 0) vfInvMoments[4] = 1;

                vfBetaMoments[5] = vfInvMoments[6] / vfInvMoments[4];

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap LocalRange(int nLeft, int nTop, int nRight, int nBottom)
        {
            int nRow = 0, nCol = 0;
            try
            {
                int xSize = nRight - nLeft + 1;
                int ySize = nBottom - nTop + 1;
                int N;
                int MASKMAX = N = xSize * ySize;

                if ((nLeft < -MASKMAX) || (nLeft > MASKMAX) ||
                    (nRight < -MASKMAX) || (nRight > MASKMAX) ||
                    (nTop < -MASKMAX) || (nTop > MASKMAX) ||
                    (nBottom < -MASKMAX) || (nBottom > MASKMAX) ||
                    (nLeft > nRight) || (nTop > nBottom) ||
                    ((nRight - nLeft + 1) > 2 * MASKMAX + 1) ||
                    ((nBottom - nTop + 1) > 2 * MASKMAX + 1))
                {
                    MessageBox.Show("Error due to specified data is out of range");
                    return null;
                }

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nSample = 0;
                for (nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nMinInt = CDefinitions.IMAX; int nMaxInt = -1;
                        for (int cntRow = nTop; cntRow <= nBottom; cntRow++)
                        {
                            int cntY = nRow + cntRow;
                            if (cntY < 0) cntY = 0;
                            else if (cntY > m_nHeight - 1) cntY = m_nHeight - 1;

                            for (int cntCol = nLeft; cntCol <= nRight; cntCol++)
                            {
                                int cntX = nCol + cntCol;
                                if (cntX < 0) cntX = 0;
                                else if (cntX > m_nWidth - 1) cntX = m_nWidth - 1;

                                nSample = GetAvgPixel(cntX, cntY);
                                if (nSample > nMaxInt) nMaxInt = nSample;
                                if (nSample < nMinInt) nMinInt = nSample;
                            }
                        }
                        dest.SetPixel(nCol, nRow, Color.FromArgb(nMaxInt - nMinInt, nMaxInt - nMinInt, nMaxInt - nMinInt));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + String.Format(". Row = {0} x Col = {1}", nRow, nCol));
                throw newEx;
            }
        }

        public Bitmap LocalSigma(int nLeft, int nTop, int nRight, int nBottom)
        {
            int nRow = 0, nCol = 0;
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int xSize = nRight - nLeft + 1;
                int ySize = nBottom - nTop + 1;
                int MASKMAX;
                int N = MASKMAX = xSize * ySize;

                if ((nLeft < -MASKMAX) || (nLeft > MASKMAX) ||
                    (nRight < -MASKMAX) || (nRight > MASKMAX) ||
                    (nTop < -MASKMAX) || (nTop > MASKMAX) ||
                    (nBottom < -MASKMAX) || (nBottom > MASKMAX) ||
                    (nLeft > nRight) || (nTop > nBottom) ||
                    ((nRight - nLeft + 1) > 2 * MASKMAX + 1) ||
                    ((nBottom - nTop + 1) > 2 * MASKMAX + 1))
                {
                    MessageBox.Show("Error due to specified data is out of range");
                    return null;
                }

                for (nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nSum = 0, SumVar = 0;
                        for (int cntRow = nTop; cntRow <= nBottom; cntRow++)
                        {
                            int cntY = nRow + cntRow;
                            if (cntY < 0) cntY = 0;
                            else if (cntY > m_nHeight - 1) cntY = m_nHeight - 1;
                            for (int cntCol = nLeft; cntCol <= nRight; cntCol++)
                            {
                                int cntX = nCol + cntCol;
                                if (cntX < 0) cntX = 0;
                                else if (cntX > m_nWidth - 1) cntX = m_nWidth - 1;

                                int nSample = GetAvgPixel(cntX, cntY);
                                nSum += nSample;
                                SumVar += (nSample * nSample);
                            }
                        }
                        int nMean = nSum / N;
                        int nResult = (int)Math.Sqrt((double)((SumVar / N) - (nMean * nMean)));
                        if (nResult < 0) nResult = 0;
                        if (nResult > CDefinitions.IMAX) nResult = CDefinitions.IMAX;
                        dest.SetPixel(nCol, nRow, Color.FromArgb(nResult, nResult, nResult));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + String.Format(". Row = {0} x Col = {1}", nRow, nCol));
                throw newEx;
            }
        }

        public Bitmap FuzzySegment(int hSize, int nZeroCrossingT, int nLowT, int nHighT, double g)
        {
            try
            {
                int[] vHistogram = new int[CDefinitions.SCALE + hSize];
                double[] vSmoothHist = new double[CDefinitions.SCALE + hSize];

                // TO COMPUTE THE PIXEL INTENSITY HISTOGRAM OF THE SMOOTHENED IMAGE 
                for (int nCnt = 0; nCnt < CDefinitions.SCALE + hSize; nCnt++)
                {
                    vHistogram[nCnt] = 0; vSmoothHist[nCnt] = 0.0;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nValue = GetAvgPixel(nCol, nRow);
                        vHistogram[nValue]++;
                    }
                }

                // TO SMOOTHEN THE HISTOGRAM BY LOCAL AVERAGING WITH A WEIGHTING FUNCTION
                double[] dBeta = new double[15];
                int hSizeb2 = hSize / 2;
                for (int nCnt = 0; nCnt < hSizeb2; nCnt++)
                {
                    dBeta[nCnt + hSizeb2] = hSizeb2 - nCnt;
                    dBeta[hSizeb2 - nCnt] = hSizeb2 - nCnt;
                }

                int nIndex;
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    double dSum = 0.0;
                    for (int nIdx = 0; nIdx < hSize; nIdx++)
                    {
                        nIndex = nCnt - nIdx + hSizeb2;
                        if (nIndex < 0) nIndex = 0;
                        if (nIndex > CDefinitions.IMAX) nIndex = CDefinitions.IMAX;
                        dSum += (dBeta[nIdx] * vHistogram[nIndex]);
                    }
                    vSmoothHist[nCnt + hSizeb2] = dSum;
                }

                // TO COMPUTE THE DERIVATIVE OF THE HISTOGRAM
                double[] vMapFn = new double[CDefinitions.SCALE + hSize];
                vMapFn[0] = 0.0;
                for (int nCnt = 1; nCnt < CDefinitions.SCALE + hSize; nCnt++)
                {
                    vMapFn[nCnt] = vSmoothHist[nCnt] - vSmoothHist[nCnt - 1];
                }

                // TO LOCATE THE ZERO CROSSINGS IN THE DERIVATIVE
                int[] vPeak = new int[CDefinitions.SCALE + hSize];
                int nCountPeak = 0;
                for (int nCnt = 1; nCnt < CDefinitions.SCALE + hSize; nCnt++)
                {
                    if ((vMapFn[nCnt - 1] > 0.0) &&
                        (vMapFn[nCnt] < 0.0) &&
                        ((vMapFn[nCnt - 1] - vMapFn[nCnt]) > nZeroCrossingT))
                    {
                        vPeak[nCountPeak] = nCnt - hSizeb2 - 1;
                        nCountPeak++;
                    }
                }

                // TO LOCATE THE ALTERNATE VALLEYS OF THE SMOOTHENED HISTOGRAM
                int[] vValley = new int[CDefinitions.SCALE + hSize];
                double dMin;
                int nCountValley = 1, nMidPoint, nRange;
                vValley[0] = 0;
                for (int nCnt = 1; nCnt < nCountPeak; nCnt++)
                {
                    nMidPoint = (vPeak[nCnt - 1] + vPeak[nCnt]) / 2; dMin = Double.MaxValue;
                    nRange = (vPeak[nCnt] - vPeak[nCnt - 1]) / 2;
                    for (int nIdx = 0; nIdx < nRange; nIdx++)
                    {
                        if (vSmoothHist[nIdx + nMidPoint] < dMin)
                        {
                            dMin = vSmoothHist[nIdx + nMidPoint];
                            vValley[nCnt] = nIdx + nMidPoint;
                        }
                        if (vSmoothHist[nMidPoint - nIdx] < dMin)
                        {
                            dMin = vSmoothHist[nMidPoint - nIdx];
                            vValley[nCnt] = nMidPoint - nIdx;
                        }
                    }
                    nCountValley++;
                }

                double dResult = 0.0;
                for (int i = 0; i < CDefinitions.SCALE; i++)
                {
                    if (true == getFuzzyMembership(ref vValley, nCountValley, 0, CDefinitions.IMAX, i, ref dResult))
                        vMapFn[i] = dResult;
                    else
                        return null;
                }

                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    vMapFn[nCnt] = getFuzzyCint(vMapFn[nCnt], g);
                }

                /*nLowT = 1-nLowT;
                nHighT = 1-nHighT;*/
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nSample; int nResult = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        nSample = GetAvgPixel(nCol, nRow);
                        if (nSample > vValley[nCountValley - 1]) nResult = nCountValley;
                        else
                        {
                            nResult = 1;
                            while (vValley[nResult] < nSample) nResult++;
                        }

                        nResult--;
                        dResult = vMapFn[nSample];

                        int I = nResult + 1;
                        if (I < nLowT || I > nHighT)
                        {
                            I = CDefinitions.IMIN;
                        }
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

        public bool getFuzzyMembership(ref int[] vCrossOver, int nPoints, int nMinInt, int nMaxInt, int nX, ref double dResult)
        {
            if ((nX < nMinInt) || (nX > nMaxInt)) return false;

            int nCnt = 0;
            if (nX > vCrossOver[nPoints - 1]) nCnt = nPoints;
            else
            {
                nCnt = 1;
                while (vCrossOver[nCnt] < nX) nCnt++;
            }

            nCnt--;

            double pi = Math.PI; double pib4 = pi / 4.0;

            double dTemp;
            if (nCnt == 0)
            {
                dTemp = Math.Sin((pib4 * (nX - nMinInt) / (vCrossOver[1] - nMinInt)));
                dResult = (dTemp * dTemp);
                return true;
            }

            if (nCnt == nPoints - 1)
            {
                if ((nCnt & 0x01) == 1)
                {
                    dTemp = Math.Sin((pib4 * (nMaxInt + nX - 2 * vCrossOver[nCnt]) / (nMaxInt - vCrossOver[nCnt])));
                    dResult = (dTemp * dTemp);
                    return true;
                }
                else
                {
                    dTemp = Math.Sin((pib4 * (nMaxInt + nX - 2 * vCrossOver[nCnt]) / (nMaxInt - vCrossOver[nCnt])));
                    dResult = (1.0 - (dTemp * dTemp));
                    return true;
                }
            }
            else
            {
                if ((nCnt & 0x01) == 1)
                {
                    dTemp = Math.Sin((pi * (nX - vCrossOver[nCnt]) / (vCrossOver[nCnt + 1] - vCrossOver[nCnt])));
                    dResult = (0.5f * (1.0 + (dTemp * dTemp)));
                    return true;
                }
                else
                {
                    dTemp = Math.Sin((pi * (nX - vCrossOver[nCnt]) / (vCrossOver[nCnt + 1] - vCrossOver[nCnt])));
                    dResult = (0.5f * (1.0 - (dTemp * dTemp)));
                    return true;
                }
            }
        }

        double getFuzzyCint(double dLambda, double g)
        {
            if ((dLambda < 0.0) || (dLambda > 1.0)) return (-100.0);

            if (dLambda <= 0.5f) return (getFuzzyCon(dLambda, g));
            else return (getFuzzyDil(dLambda, 1.0 / g));
        }

        double getFuzzyCon(double dLambda, double g)
        {
            if (g < 1.0) return (-100.0);

            if (dLambda < 0.0 || dLambda > 1.0) return (-100.0);
            else return (Math.Pow(dLambda, g));
        }

        double getFuzzyDil(double dLambda, double g)
        {
            if (g > 1.0) return (-100.0);

            if (dLambda < 0.0 || dLambda > 1.0) return (-100.0);
            else return (Math.Pow(dLambda, g));
        }

        #endregion
    }
}
