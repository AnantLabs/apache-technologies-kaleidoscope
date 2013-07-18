using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using AForge;
using AForge.Imaging;

namespace Apache.ImageLib
{
    public class CCanny : CImageInfo
    {
        #region CTOR

        public CCanny(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        public Bitmap Canny(double dTLow, double dTHigh, int nSigma)
        {
            Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
            try
            {
                int[,] arrGaussianRaster = new int[m_nHeight, m_nWidth];
                // Smoothing the image using a gaussian kernel
                getSmoothGaussian(ref arrGaussianRaster, nSigma);

                int[,] vDeltaX = new int[m_nHeight, m_nWidth];
                int[,] vDeltaY = new int[m_nHeight, m_nWidth];
                // Compute the first derivative in the X and y directions.
                getDerivativeX_Y(arrGaussianRaster, ref vDeltaX, ref vDeltaY, m_nWidth, m_nHeight);

                int[,] vMagnitude = new int[m_nHeight, m_nWidth];
                // Compute the magnitude of the gradient
                getMagnitudeX_Y(vDeltaX, vDeltaY, ref vMagnitude, m_nWidth, m_nHeight);

                int[,] vNonMaxSup = new int[m_nHeight, m_nWidth];
                // Perform non-maximal suppression.
                getNonMaxSup(vMagnitude, vDeltaX, vDeltaY, ref vNonMaxSup, m_nWidth, m_nHeight);

                // Use hysteresis to mark the OutRaster pixels
                applyHysteresis(ref dest, vMagnitude, vNonMaxSup, dTLow, dTHigh);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }
            return dest;
        }

        /******************************************************************************************
        Function		 : Gaussian_Smooth
        Description		 : 
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void getSmoothGaussian(ref int[,] arrGaussianRaster, int nSigma)
        {
            // Create a 1-dimensional gaussian smoothing kernel.
            List<double> vKernel = new List<double>();
            int nWindowSize = 0;
            makeGaussianKernel(nSigma, ref vKernel, ref nWindowSize);
            int nCenter = nWindowSize / 2;

            double[,] tmpRaster = new double[m_nHeight, m_nWidth];
            double dDot, dSum;
            // Blur in the X - direction.
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    dDot = 0.0; dSum = 0.0;
                    for (int nCnt = (-nCenter); nCnt <= nCenter; nCnt++)
                    {
                        if ((nCol + nCnt) >= 0 && (nCol + nCnt) < m_nWidth)
                        {
                            Color clr = SrcImage.GetPixel(nCol + nCnt, nRow);
                            dDot += CDefinitions.CLR2GREY(clr) * vKernel[nCenter + nCnt];
                            dSum += vKernel[nCenter + nCnt];
                        }
                    }
                    tmpRaster[nRow, nCol] = dDot / dSum;
                }
            }

            // Blur in the y - direction.
            for (int nCol = 0; nCol < m_nWidth; nCol++)
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    dSum = 0.0; dDot = 0.0;
                    for (int nCnt = -nCenter; nCnt <= nCenter; nCnt++)
                    {
                        if ((nRow + nCnt) >= 0 && (nRow + nCnt) < m_nHeight)
                        {
                            dDot += tmpRaster[nRow + nCnt, nCol] * vKernel[nCenter + nCnt];
                            dSum += vKernel[nCenter + nCnt];
                        }
                    }
                    arrGaussianRaster[nRow, nCol] = (int)(dDot * CDefinitions.BOOSTBLURFACTOR / dSum + 0.5);
                }
            }
        }

        /******************************************************************************************
        Function		 : Make_Gaussian_Kernal
        Description		 : Create a one dimensional gaussian kernel.
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void makeGaussianKernel(int nSigma, ref List<double> vKernel, ref int nWindowSize)
        {
            nWindowSize = (int)(1 + 2 * System.Math.Ceiling(2.5 * nSigma));
            int nCenter = nWindowSize / 2;

            double dX, dSumX = 0.0;
            for (int nCnt = 0; nCnt < nWindowSize; nCnt++)
            {
                dX = (double)(System.Math.Pow(2.71828, -0.5 * (nCnt - nCenter) * (nCnt - nCenter) / (nSigma * nSigma)) /
                    (nSigma * System.Math.Sqrt(6.2831853f)));
                vKernel.Add(dX);
                dSumX += dX;
            }

            for (int nCnt = 0; nCnt < nWindowSize; nCnt++)
            {
                vKernel[nCnt] /= dSumX;
            }
        }

        /******************************************************************************************
        Function		 : getDerivativeX_Y
        Description		 : Compute the first derivative of the image in both the 
                           X any y directions. The differential filters that are 
                           used are:
                                                  -1
                           dx =  -1 0 +1 and dy =  0
                                                  +1
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void getDerivativeX_Y(int[,] arrGaussianRaster, ref int[,] vDeltaX, ref int[,] vDeltaY,
            int nWidth, int nHeight)
        {
            // Compute the X-derivative. Adjust the derivative at the borders to avoid losing pixels.
            for (int nRow = 0; nRow < nHeight; nRow++)
            {
                for (int nCol = 1; nCol < nWidth - 1; nCol++)
                {
                    vDeltaX[nRow, nCol] = arrGaussianRaster[nRow, nCol + 1] - arrGaussianRaster[nRow, nCol - 1];
                }
            }

            // Compute the y-derivative. Adjust the derivative at the borders to avoid losing pixels.

            for (int nCol = 0; nCol < nWidth; nCol++)
            {
                for (int nRow = 1; nRow < (nHeight - 1); nRow++)
                {
                    vDeltaY[nRow, nCol] = arrGaussianRaster[nRow + 1, nCol] - arrGaussianRaster[nRow - 1, nCol];
                }
            }
        }

        /******************************************************************************************
        Function		 : getMagnitudeX_Y
        Description		 : Compute the magnitude of the gradient. This is the 
                           square root of the sum of the squared derivative values.
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void getMagnitudeX_Y(int[,] vDeltaX, int[,] vDeltaY, ref int[,] vMagnitude,
            int nWidth, int nHeight)
        {
            int nSq1 = 0, nSq2 = 0;

            for (int nRow = 0; nRow < nHeight; nRow++)
            {
                for (int nCol = 0; nCol < nWidth; nCol++)
                {
                    nSq1 = (int)(vDeltaX[nRow, nCol] * vDeltaX[nRow, nCol]);
                    nSq2 = (int)(vDeltaY[nRow, nCol] * vDeltaY[nRow, nCol]);
                    vMagnitude[nRow, nCol] = (int)(0.5 + System.Math.Sqrt((double)nSq1 + (double)nSq2));
                }
            }
        }

        /******************************************************************************************
        Function		 : getNonMaxSup
        Description		 : This routine applies non-maximal suppression to the 
                           magnitude of the gradient image.
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void getNonMaxSup(int[,] vMagnitude, int[,] vDeltaX,
            int[,] vDeltaY, ref int[,] vNonMaxSup, int nWidth, int nHeight)
        {
            int Z1 = 0, Z2 = 0, M00 = 0, gX = 0, gY = 0;
            double Mag1 = 0.0, Mag2 = 0.0, XPerp = 0.0, YPerp = 0.0;

            // Suppress non-maximum points.
            for (int nRow = 1; nRow < nHeight - 1; nRow++)
            {
                for (int nCol = 1; nCol < nWidth - 1; nCol++)
                {
                    M00 = vMagnitude[nRow, nCol];
                    if (M00 == 0)
                    {
                        vNonMaxSup[nRow, nCol] = CDefinitions.NOEDGE;
                    }
                    else
                    {
                        XPerp = -(gX = vDeltaX[nRow, nCol]) / ((double)M00);
                        YPerp = (gY = vDeltaY[nRow, nCol]) / ((double)M00);
                    }

                    if (gX >= 0)
                    {
                        if (gY >= 0)
                        {
                            if (gX >= gY)
                            {
                                // 111
                                // Left point
                                Z1 = vMagnitude[nRow, nCol - 1];
                                Z2 = vMagnitude[nRow - 1, nCol - 1];

                                Mag1 = (M00 - Z1) * XPerp + (Z2 - Z1) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow, nCol + 1];
                                Z2 = vMagnitude[nRow + 1, nCol + 1];

                                Mag2 = (M00 - Z1) * XPerp + (Z2 - Z1) * YPerp;
                            }
                            else
                            {
                                // 110
                                // Left point
                                Z1 = vMagnitude[nRow - 1, nCol];
                                Z2 = vMagnitude[nRow - 1, nCol - 1];

                                Mag1 = (Z1 - Z2) * XPerp + (Z1 - M00) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow + 1, nCol];
                                Z2 = vMagnitude[nRow + 1, nCol + 1];

                                Mag2 = (Z1 - Z2) * XPerp + (Z1 - M00) * YPerp;
                            }
                        }
                        else
                        {
                            if (gX >= -gY)
                            {
                                // 101
                                // Left point
                                Z1 = vMagnitude[nRow, nCol - 1];
                                Z2 = vMagnitude[nRow + 1, nCol - 1];

                                Mag1 = (M00 - Z1) * XPerp + (Z1 - Z2) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow, nCol + 1];
                                Z2 = vMagnitude[nRow - 1, nCol + 1];

                                Mag2 = (M00 - Z1) * XPerp + (Z1 - Z2) * YPerp;
                            }
                            else
                            {
                                // 100
                                // Left point
                                Z1 = vMagnitude[nRow + 1, nCol];
                                Z2 = vMagnitude[nRow + 1, nCol - 1];

                                Mag1 = (Z1 - Z2) * XPerp + (M00 - Z1) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow - 1, nCol];
                                Z2 = vMagnitude[nRow - 1, nCol + 1];

                                Mag2 = (Z1 - Z2) * XPerp + (M00 - Z1) * YPerp;
                            }
                        }
                    }
                    else
                    {
                        if ((gY = vDeltaY[nRow, nCol]) >= 0)
                        {
                            if (-gX >= gY)
                            {
                                // 011
                                // Left point
                                Z1 = vMagnitude[nRow, nCol + 1];
                                Z2 = vMagnitude[nRow - 1, nCol + 1];

                                Mag1 = (Z1 - M00) * XPerp + (Z2 - Z1) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow, nCol - 1];
                                Z2 = vMagnitude[nRow + 1, nCol - 1];

                                Mag2 = (Z1 - M00) * XPerp + (Z2 - Z1) * YPerp;
                            }
                            else
                            {
                                // 010
                                // Left point
                                Z1 = vMagnitude[nRow - 1, nCol];
                                Z2 = vMagnitude[nRow - 1, nCol + 1];

                                Mag1 = (Z2 - Z1) * XPerp + (Z1 - M00) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow + 1, nCol];
                                Z2 = vMagnitude[nRow + 1, nCol - 1];

                                Mag2 = (Z2 - Z1) * XPerp + (Z1 - M00) * YPerp;
                            }
                        }
                        else
                        {
                            if (-gX > -gY)
                            {
                                // 001
                                // Left point
                                Z1 = vMagnitude[nRow, nCol + 1];
                                Z2 = vMagnitude[nRow + 1, nCol + 1];

                                Mag1 = (Z1 - M00) * XPerp + (Z1 - Z2) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow, nCol - 1];
                                Z2 = vMagnitude[nRow - 1, nCol - 1];

                                Mag2 = (Z1 - M00) * XPerp + (Z1 - Z2) * YPerp;
                            }
                            else
                            {
                                // 000
                                // Left point 
                                Z1 = vMagnitude[nRow + 1, nCol];
                                Z2 = vMagnitude[nRow + 1, nCol + 1];

                                Mag1 = (Z2 - Z1) * XPerp + (M00 - Z1) * YPerp;

                                // Right point
                                Z1 = vMagnitude[nRow - 1, nCol];
                                Z2 = vMagnitude[nRow - 1, nCol - 1];

                                Mag2 = (Z2 - Z1) * XPerp + (M00 - Z1) * YPerp;
                            }
                        }
                    }
                    // Now determine if the current point is a maximum point
                    if ((Mag1 > 0.0) || (Mag2 > 0.0))
                        vNonMaxSup[nRow, nCol] = CDefinitions.NOEDGE;
                    else if (Mag2 == 0.0)
                        vNonMaxSup[nRow, nCol] = CDefinitions.NOEDGE;
                    else
                        vNonMaxSup[nRow, nCol] = CDefinitions.POSSIBLE_EDGE;
                }
            }
        }

        /******************************************************************************************
        Function		 : getApplyHysteresis
        Description		 : This routine finds edges that are above some high 
                           threshhold or are connected to a high pixel by a path of
                           pixels greater than a low threshold.
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void applyHysteresis(ref Bitmap dest, int[,] vMagnitude, int[,] vNonMaxSup,
            double dTLow, double dTHigh)
        {
            // Initialize the OutRaster map to possible edges everywhere the 
            // non-maximal suppression suggested there could be an OutRaster except 
            // for the border. At the border we say there can not be an OutRaster 
            // because it makes the follow_edges algorithm more efficient to 
            // not worry about tracking an OutRaster off the side of the image.
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    if (vNonMaxSup[nRow, nCol] == CDefinitions.POSSIBLE_EDGE)
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.POSSIBLE_EDGE, CDefinitions.POSSIBLE_EDGE, CDefinitions.POSSIBLE_EDGE));
                    }
                    else
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.NOEDGE, CDefinitions.NOEDGE, CDefinitions.NOEDGE));
                    }
                }
            }

            // Compute the lkpHistogram of the magnitude image. Then use the 
            // lkpHistogram to compute hysteresis thresholds.
            int[] lkpHistogram = new int[m_nWidth * m_nHeight];
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    Color clr = dest.GetPixel(nCol, nRow);
                    if (CDefinitions.CLR2AVR(clr) == CDefinitions.POSSIBLE_EDGE)
                        lkpHistogram[vMagnitude[nRow, nCol]]++;
                }
            }

            int nEdgeCount = 0, nMaxMagnitude = 0;
            // Compute the number of pixels that passed the nonmaximal suppression.
            for (int nCnt = 0; nCnt < lkpHistogram.Length; nCnt++)
            {
                if (lkpHistogram[nCnt] != 0) nMaxMagnitude = nCnt;
                nEdgeCount += lkpHistogram[nCnt];
            }

            int nHighCount = (int)(nEdgeCount * dTHigh + 0.5);

            //Compute the high threshold Value as the (100 * thigh) percentage 
            // point in the magnitude of the gradient lkpHistogram of all the 
            // pixels that passes non-maximal suppression. Then calculate the 
            // low threshold as a fraction of the computed high threshold Value.
            // John Canny said in his paper "A Computational Approach to Edge 
            // Detection" that "The ratio of the high to low threshold in the 
            // implementation is in the nRange two or three to one." That means 
            // that in terms of this implementation, we should choose 
            // tlow ~= 0.5 or 0.33333.
            int nCount = 1; nEdgeCount = lkpHistogram[1];
            while ((nCount < (nMaxMagnitude - 1)) && (nEdgeCount < nHighCount))
            {
                nCount++;
                nEdgeCount += lkpHistogram[nCount];
            }

            int nHighThreshold = nCount;
            int nLowThreshold = (int)(nHighThreshold * dTLow + 0.5f);

            // This loop looks for pixels above the HighThreshold to locate 
            // edges and then calls follow_edges to continue the OutRaster.
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    Color clr = dest.GetPixel(nCol, nRow);
                    if ((CDefinitions.CLR2AVR(clr) == CDefinitions.POSSIBLE_EDGE) &&
                        (vMagnitude[nRow, nCol] >= nHighThreshold))
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.EDGE, CDefinitions.EDGE, CDefinitions.EDGE));
                        followEdges(ref dest, nCol, nRow, vMagnitude, nLowThreshold);
                    }
                }
            }

            // Set all the remaining possible edges to non-edges.
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    Color clr = dest.GetPixel(nCol, nRow);
                    if (CDefinitions.CLR2AVR(clr) != CDefinitions.EDGE)
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.NOEDGE, CDefinitions.NOEDGE, CDefinitions.NOEDGE));
                    }
                }
            }
        }

        /******************************************************************************************
        Function		 : getFollowEdges
        Description		 : This procedure edges is a recursive routine that traces
                           edgs along all paths whose magnitude values remain above 
                           some specifyable lower threshhold.
        ReturnValue		 : 0 on success
        *******************************************************************************************/
        private void followEdges(ref Bitmap dest, int nCol, int nRow, int[,] listMagnitude, int nLowT)
        {
            int[] X = { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] y = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int nCnt = 0; nCnt < 8; nCnt++)
            {
                Color clr = dest.GetPixel(nCol + X[nCnt], nRow + y[nCnt]);
                if ((CDefinitions.CLR2AVR(clr) == CDefinitions.POSSIBLE_EDGE) &&
                    (listMagnitude[nRow + y[nCnt], nCol + X[nCnt]] > nLowT))
                {
                    dest.SetPixel(nCol + X[nCnt], nRow + y[nCnt], Color.FromArgb(CDefinitions.EDGE, CDefinitions.EDGE, CDefinitions.EDGE));
                    followEdges(ref dest, nCol + X[nCnt], nRow + y[nCnt], listMagnitude, nLowT);
                }
            }
        }
    }
}
