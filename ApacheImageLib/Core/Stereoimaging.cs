using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apache.ImageLib
{
    public class CStereoimaging
    //: CImageInfo
    {
        #region CONSTANTS

        private const byte DISCONTINUITY = 255;  // symbol for a depth discontinuity 
        private const byte NO_DISCONTINUITY = 0;	// symbol for no depth discontinuity 

        private const UInt16 FIRST_MATCH = 65535;  // symbol for first match 
        private const UInt16 DEFAULT_COST = 600;   // prevents costs from becoming negative
        private const UInt16 NOT_IG_PEN = 1000;    // penalty for depth discontinuity without intensity gradient

        private int m_nWidth = 0;
        private int m_nHeight = 0;

        #endregion

        #region VARIABLES

        private int m_nOcclusionPenalty;
        private int m_nRewards;
        private int m_nDisparity;
        private int m_nReliableThreshold;
        private int m_nModeratelyReliableThreshold;
        private int m_nSlightlyReliableThreshold;   // Threshold for CDefinitions.MAX distance for aligning with gradient to handle 
        // nearly horizontal boundaries 
        private int m_nMaxAttractionThreshold;
        private float m_fAlpha;

        private int[] m_pHistogram = new int[CDefinitions.SCALE];
        public int[] Histogram
        {
            get { return m_pHistogram; }
        }

        //private int m_nWidth, m_nHeight;
        private Bitmap m_srcImage1 = null;
        private Bitmap m_srcImage2 = null;

        private byte GetAvgPixel1(int nCol, int nRow)
        {
            try
            {
                Color clr = m_srcImage1.GetPixel(nCol, nRow);
                return (byte)((clr.R + clr.G + clr.B) / 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte GetAvgPixel2(int nCol, int nRow)
        {
            try
            {
                Color clr = m_srcImage2.GetPixel(nCol, nRow);
                return (byte)((clr.R + clr.G + clr.B) / 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CTOR

        public CStereoimaging(Bitmap srcImage1, Bitmap srcImage2,
                int nOcclusion, int nReward, int nDisparity, int nRT, float fAlpha, int nMAT)
        //: base(srcImage1)
        {
            m_srcImage1 = srcImage1;
            m_srcImage2 = srcImage2;
            m_nOcclusionPenalty = nOcclusion < 0 ? 25 : 2 * nOcclusion;
            m_nRewards = nReward < 0 ? 0 : 2 * nReward;
            m_nDisparity = nDisparity;
            m_nReliableThreshold = nRT < 0 ? 0 : nRT;
            m_fAlpha = fAlpha;
            m_nMaxAttractionThreshold = nMAT < 0 ? 0 : nMAT;
            m_nWidth = m_srcImage1.Width;
            m_nHeight = m_srcImage1.Height;
        }

        #endregion

        #region PRIVATE_FUNCTIONS

        // Function    : getMatchScanlines
        // Description : Used in STAN-CS-TR96-1573 and ICCV'98
        //               Matches each pair of scanlines independently from the other 
        //               pairs, using the Faster Algorithm, which is dynamic programming
        //               with pruning.

        // NOTES		 : On indexing the matrices, pPhi(y, nDeltaA) is the total cost of
        //               the match sequence ending with the match (y+nDeltaA, y).  In 
        //               other words, y is the index into the right nRow and nDeltaA 
        //               is the disparity.  For the sake of efficiency, these matrices 
        //               are transposed from those in the paper. 
        bool getMatchScanlines(ref byte[,] pDisparityMap, ref byte[,] pDDMap)
        {
            int nRow = 0;
            try
            {
                // Allocating the memory
                int[,] pDisparityRaster = new int[m_nWidth + m_nDisparity + 1, m_nDisparity + 1];	    // dissimilarity between two pixels

                int[,] pPhi = new int[m_nWidth + m_nDisparity + 1, m_nDisparity + 1];               // cost of a match sequence
                int[,] pPieY = new int[m_nWidth + m_nDisparity + 1, m_nDisparity + 1];			    // points to the immediately  
                int[,] pPieD = new int[m_nWidth + m_nDisparity + 1, m_nDisparity + 1];			    // preceding match

                int[] pBadNodes = new int[m_nWidth + m_nDisparity + 1];
                int[] pNoIgL = new int[m_nWidth + m_nDisparity + 1];  // dissimilarity between two pixels
                int[] pNoIgR = new int[m_nWidth + m_nDisparity + 1];

                int nPhiNew = 0, nPhiYBest = 0, nPhiDBest = 0;
                for (nRow = 0; nRow < m_nHeight; nRow++)
                {
                    // Call function to get the DISPARITY RASTER
                    fillDissimilarityTable(ref pDisparityRaster, nRow);

                    // Call function to get the NUMBER OF GRADIENT IN LEFT AND RIGHT RASTER
                    computeIntensityGradientsX(nRow, ref pNoIgL, ref pNoIgR);

                    // Initialize pPhi and pBadNodes RASTER with UInt16.MaxValue 
                    for (int nCol = 1; nCol < m_nWidth; nCol++)
                    {
                        pBadNodes[nCol] = UInt16.MaxValue;
                        for (int nDeltaP = 0; nDeltaP <= m_nDisparity; nDeltaP++) pPhi[nCol, nDeltaP] = UInt16.MaxValue;
                    }

                    for (int nDeltaP = 0; nDeltaP <= m_nDisparity; nDeltaP++)
                    {
                        pPhi[0, nDeltaP] = DEFAULT_COST + pDisparityRaster[0, nDeltaP];
                        pPieY[0, nDeltaP] = FIRST_MATCH;
                        pPieD[0, nDeltaP] = FIRST_MATCH;
                        pBadNodes[0] = pPhi[0, nDeltaP];
                    }

                    for (int nColP = 0; nColP < m_nWidth; nColP++)
                    {
                        // Determine nYmin
                        int nColMin = UInt16.MaxValue;
                        for (int nDeltaP = 0; nDeltaP <= m_nDisparity; nDeltaP++)
                        {
                            nColMin = CDefinitions.MIN(nColMin, pPhi[nColP, nDeltaP]);
                        }

                        for (int nDeltaP = 0; nDeltaP <= m_nDisparity; nDeltaP++)
                        {
                            // Expand good y nodes
                            if (pPhi[nColP, nDeltaP] <= nColMin)
                            {
                                int nY = nColP + 1;
                                for (int nDeltaA = nDeltaP + 1; nDeltaA <= m_nDisparity; nDeltaA++)
                                {
                                    nPhiNew = pPhi[nColP, nDeltaP] + pDisparityRaster[nY, nDeltaA]
                                              - m_nRewards + m_nOcclusionPenalty + pNoIgL[nY + nDeltaA];
                                    if (nPhiNew < pPhi[nY, nDeltaA])
                                    {
                                        pPhi[nY, nDeltaA] = nPhiNew;
                                        pPieY[nY, nDeltaA] = nColP;
                                        pPieD[nY, nDeltaA] = nDeltaP;
                                        pBadNodes[nY + nDeltaA] = CDefinitions.MIN(pBadNodes[nY + nDeltaA], nPhiNew);
                                    }// end if(nPhiNew) 
                                }// end for(nDeltaA) 
                            }// end if(pPhi[,] <= nYmin) 

                            // Expand good x nodes
                            if (pPhi[nColP, nDeltaP] <= pBadNodes[nColP + nDeltaP])
                            {
                                for (int nDeltaA = 0; nDeltaA < nDeltaP; nDeltaA++)
                                {
                                    int nY = nColP + nDeltaP - nDeltaA + 1;
                                    nPhiNew = pPhi[nColP, nDeltaP] + pDisparityRaster[nY, nDeltaA] - m_nRewards + m_nOcclusionPenalty + pNoIgR[nColP];
                                    if (nPhiNew < pPhi[nY, nDeltaA])
                                    {
                                        pPhi[nY, nDeltaA] = nPhiNew;
                                        pPieY[nY, nDeltaA] = nColP;
                                        pPieD[nY, nDeltaA] = nDeltaP;
                                        pBadNodes[nY + nDeltaA] = CDefinitions.MIN(pBadNodes[nY + nDeltaA], nPhiNew);
                                    }// end if(nPhiNew)
                                }// end for(nDeltaA)
                            }// end if(pPhi[,] <= pBadNodes[])

                            // Expand all nodes
                            nPhiNew = pPhi[nColP, nDeltaP] + pDisparityRaster[nColP + 1, nDeltaP] - m_nRewards;
                            //nPhiNew = pPhi[nColP,nDeltaP] + *(pDisparityRaster + (nColP+1)*nSlope + nDeltaP) - m_nRewards;

                            if (nPhiNew < pPhi[nColP + 1, nDeltaP])
                            {
                                pPhi[nColP + 1, nDeltaP] = nPhiNew;
                                pPieY[nColP + 1, nDeltaP] = nColP;
                                pPieD[nColP + 1, nDeltaP] = nDeltaP;
                                pBadNodes[nColP + 1 + nDeltaP] = CDefinitions.MIN(pBadNodes[nColP + 1 + nDeltaP], nPhiNew);
                            }// end(if)
                        }// end for(nDeltaP)
                    }// end for(nDeltaP)

                    // find ending match $m_k$
                    int nPhiBest = UInt16.MaxValue;
                    for (int nDeltaA = 0; nDeltaA <= m_nDisparity; nDeltaA++)
                    {
                        int nY = m_nWidth - 1 - nDeltaA;
                        if (pPhi[nY, nDeltaA] <= nPhiBest)
                        {
                            nPhiBest = pPhi[nY, nDeltaA];
                            nPhiYBest = nY;
                            nPhiDBest = nDeltaA;
                        }
                    }

                    // Compute disparity map and depth discontinuities
                    {
                        int nY1 = nPhiYBest;
                        int nDeltaA1 = nPhiDBest;
                        int nX1 = nY1 + nDeltaA1;
                        int nY2 = pPieY[nY1, nDeltaA1];
                        int nDeltaA2 = pPieD[nY1, nDeltaA1];
                        int nX2 = nY2 + nDeltaA2;
                        for (int x = (m_nWidth - 1); x >= nX1; x--)
                        {
                            pDisparityMap[nRow, x] = (byte)nDeltaA1;
                            pDDMap[nRow, x] = NO_DISCONTINUITY;
                        }

                        while (nY2 != FIRST_MATCH)
                        {
                            if (nDeltaA1 == nDeltaA2)
                            {
                                pDisparityMap[nRow, nX2] = (byte)nDeltaA2;
                                pDDMap[nRow, nX2] = NO_DISCONTINUITY;
                            }
                            else if (nDeltaA2 > nDeltaA1)
                            {
                                pDisparityMap[nRow, nX2] = (byte)nDeltaA2;
                                pDDMap[nRow, nX2] = DISCONTINUITY;
                            }
                            else
                            {
                                pDisparityMap[nRow, nX1 - 1] = (byte)nDeltaA2;
                                pDDMap[nRow, nX1 - 1] = DISCONTINUITY;

                                for (int x = nX1 - 2; x >= nX2; x--)
                                {
                                    pDisparityMap[nRow, x] = (byte)nDeltaA2;
                                    pDDMap[nRow, x] = NO_DISCONTINUITY;
                                }
                            }
                            nY1 = nY2;
                            nDeltaA1 = nDeltaA2;
                            nX1 = nY1 + nDeltaA1;

                            nY2 = pPieY[nY1, nDeltaA1];
                            nDeltaA2 = pPieD[nY1, nDeltaA1];
                            nX2 = nY2 + nDeltaA2;
                        }

                        for (int x = (nY1 + nDeltaA1 - 1); x >= 0; x--)
                        {
                            pDisparityMap[nRow, x] = (byte)nDeltaA1;
                            pDDMap[nRow, x] = NO_DISCONTINUITY;
                        }
                    }// END 
                }// endfor -- nRow

                return true;
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + " Row = " + nRow.ToString());
                throw newEx;
            }
        }

        // Function    : fillDissimilarityTable
        // Description : Precomputes the dissimilarity values between each pair of 
        //               pixels. 
        //               The dissimilarity is defined as the minimum of:
        //               CDefinitions.MIN{ |I_1(nX1) - I_2'(z)|, |I_1'(z) - I_2(nX2)| }, 
        //               where the prime denotes the linearly interpolated image.
        //               Actually returns twice the dissimilarity, to keep everything 
        //               integers. 
        //                dInRasterL[x] = 2 * vLeftRaster[x];
        //                hInRasterL[x] = vLeftRaster[x - 1] + vLeftRaster[x];
        //                hInRasterL[x + 1] = vLeftRaster[x] + vLeftRaster[x + 1];
        public bool fillDissimilarityTable(ref int[,] pDisparityRaster, int nRowNum)
        {
            int nCol = 0, nAlpha = 0;
            try
            {
                UInt16[] hInRasterL = new UInt16[m_nWidth + 1];
                UInt16[] hInRasterR = new UInt16[m_nWidth + 1];
                UInt16[] dInRasterL = new UInt16[m_nWidth];
                UInt16[] dInRasterR = new UInt16[m_nWidth];

                UInt16 L0, L1, L2, R0, R1, R2;
                // Get the first pixel value from both images i.e Left and Right Raster
                L1 = GetAvgPixel1(0, nRowNum);
                R1 = GetAvgPixel2(0, nRowNum);

                hInRasterL[0] = (UInt16)(2 * L1); hInRasterL[m_nWidth - 1] = (UInt16)(2 * (GetAvgPixel1(m_nWidth - 1, nRowNum)));
                hInRasterR[0] = (UInt16)(2 * R1); hInRasterR[m_nWidth - 1] = (UInt16)(2 * (GetAvgPixel2(m_nWidth - 1, nRowNum)));
                dInRasterL[0] = (UInt16)(2 * L1); dInRasterR[0] = (UInt16)(2 * R1);

                UInt16 minL, maxL, minR, maxR, minN;
                int cntDisp;
                for (nCol = 1; nCol < m_nWidth; nCol++)
                {
                    L0 = L1;						 // Assign the first pixel value of Left Coloum Raster to L0
                    L1 = GetAvgPixel1(nCol, nRowNum);// L1 gets the next pixel value
                    R0 = R1;						 // Assign the first pixel value of Right Coloum Raster to R0
                    R1 = GetAvgPixel2(nCol, nRowNum);// R1 gets the next pixel value

                    // HINRASTER stores the SUM of the previous and current pixel value
                    hInRasterL[nCol] = (UInt16)(L0 + L1);
                    hInRasterR[nCol] = (UInt16)(R0 + R1);

                    // DINRASTER stores the twices the current pixel value
                    dInRasterL[nCol] = (UInt16)(2 * L1);
                    dInRasterR[nCol] = (UInt16)(2 * R1);
                }

                for (nCol = 0; nCol < m_nWidth; nCol++)
                {
                    for (nAlpha = 0; nAlpha <= m_nDisparity; nAlpha++)
                    {
                        cntDisp = nCol + nAlpha;
                        if (cntDisp < m_nWidth)
                        {
                            L0 = dInRasterL[cntDisp]; L1 = hInRasterL[cntDisp]; L2 = hInRasterL[cntDisp + 1];

                            R0 = dInRasterR[nCol]; R1 = hInRasterR[nCol]; R2 = hInRasterR[nCol + 1];

                            minN = UInt16.MaxValue;
                            maxL = (UInt16)CDefinitions.MAX3(L0, L1, L2);
                            minL = (UInt16)CDefinitions.MIN3(L0, L1, L2);
                            maxR = (UInt16)CDefinitions.MAX3(R0, R1, R2);
                            minR = (UInt16)CDefinitions.MIN3(R0, R1, R2);

                            if (L0 >= minR)
                            {
                                if (L0 <= maxR) minN = 0;
                                else minN = (UInt16)(L0 - maxR);
                            }
                            else
                            {
                                minN = (UInt16)(minR - L0);
                            }

                            if (minN > 0)
                            {
                                if (R0 >= minL)
                                {
                                    if (R0 <= maxL) minN = 0;
                                    else minN = (UInt16)CDefinitions.MIN(minN, R0 - maxL);
                                }
                                else
                                    minN = (UInt16)CDefinitions.MIN(minN, minL - R0);
                            }
                            pDisparityRaster[nCol, nAlpha] = minN;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message +
                    " Col = " + nCol.ToString() +
                    " Alpha = " + nAlpha.ToString());
                throw newEx;
            }
        }

        // Function    : ComputeIntensityGradientsX
        // Description : This function determines which pixels lie beside intensity 
        //               gradients. A pixel in the {left} nRow lies to the {left} of
        //               an intensity gradient if there is some intensity variation 
        //               to its right. Likewise, a pixel in the {right} nRow lies to
        //               the {right} of an intensity gradient if there is some 
        //               intensity variation to its left.
        //                    A pixel which lies beside an intensity gradient is
        //               labelled with 0; all other pixels are labelled with NOT_IG_PEN.
        //               This label is used as a penalty to enforce the constraint 
        //               that depth discontinuities must occur at intensity gradients.
        //               That is, a value of 0 has no effect on the cost function,
        //               whereas a value of NOT_IG_PEN prohibits depth discontinuities.

        // NOTE		 : Because NOT_IG_PEN is so large, this computation prohibits
        //               depth discontinuities that are not accompanied by intensity
        //               gradients, just like the ``if'' statements in the paper. 
        //               This method is chosen for computational speed.
        private void computeIntensityGradientsX(int nRowNum, ref int[] pNoIgL, ref int[] pNoIgR)
        {
            try
            {
                int minIGVar = 5;                // minimum intensity variation within window 
                int Window = 3;                // width of window 
                byte maxL, minL, maxR, minR;
                int nShift;

                // Initially, declare all pixels to be NOT intensity gradients
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    pNoIgL[nCol] = NOT_IG_PEN;
                    pNoIgR[nCol] = NOT_IG_PEN;
                }

                // Find intensity gradients in the left RowNum
                for (int nCol = 0; nCol < (m_nWidth - Window + 1); nCol++)
                {
                    maxL = 0; minL = 255;
                    for (nShift = nCol; nShift < (nCol + Window); nShift++)
                    {
                        if (GetAvgPixel1(nShift, nRowNum) < minL) minL = GetAvgPixel1(nShift, nRowNum);
                        if (GetAvgPixel1(nShift, nRowNum) > maxL) maxL = GetAvgPixel1(nShift, nRowNum);
                    }
                    if (maxL - minL >= minIGVar) pNoIgL[nCol] = 0;
                }

                // Find intensity gradients in the right RowNum 
                for (int nCol = (Window - 1); nCol < m_nWidth; nCol++)
                {
                    maxR = 0; minR = 255;
                    for (nShift = (nCol - Window + 1); nShift <= nCol; nShift++)
                    {
                        if (GetAvgPixel2(nShift, nRowNum) < minR) minR = GetAvgPixel2(nShift, nRowNum);
                        if (GetAvgPixel2(nShift, nRowNum) > maxR) maxR = GetAvgPixel2(nShift, nRowNum);
                    }
                    if (maxR - minR >= minIGVar) pNoIgR[nCol] = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Description : Given an pRaster, surrounded pixels are coerced.  In other 
        //               words, given a pixel P in the pRaster, if P's two immediate
        //               neighbors in the y direction (the one above and the one 
        //               below) have the same value, then P's value is changed to 
        //               agree with theirs.
        private bool coerceSurroundedPixels(ref byte[,] pRaster)
        {
            try
            {
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < m_nWidth; nCol++)
                    {
                        if (pRaster[nRow - 1, nCol] == pRaster[nRow + 1, nCol])
                        {
                            pRaster[nRow, nCol] = pRaster[(nRow - 1), nCol];
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

        // Description : Computes intensity gradients.  For each window of pixels,
        //               declares the pixels within the window to be intensity 
        //               gradients if the  intensity variation w/nRow the window is
        //               greater than a threshold.
        // Interpretation:  pIGx = 1  means that pixel is an ig in the x-direction 
        //                  pIGy = 1     "      "      "       "       y-direction 
        private bool computeIGxy(ref byte[,] pIGx, ref byte[,] pIGy)
        {
            try
            {
                int nTH = 5;                // minimum variation w/nRow window 
                int nWindow = 3;            // window width 
                int nMaxX, nMinN;

                // Compute intensity gradients in x-direction
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < (m_nWidth - nWindow + 1); nCol++)
                    {
                        nMaxX = 0; nMinN = UInt16.MaxValue;
                        for (int k = nCol; k < (nCol + nWindow); k++)
                        {
                            if (GetAvgPixel1(nCol, nRow) < nMinN)
                            {
                                nMinN = GetAvgPixel1(k, nRow);
                            }
                            if (GetAvgPixel1(k, nRow) > nMaxX)
                            {
                                nMaxX = GetAvgPixel1(k, nRow);
                            }
                        }

                        if (nMaxX - nMinN >= nTH)
                            for (int k = nCol; k < nCol + nWindow; k++)
                            {
                                pIGx[nRow, k] = 1;
                            }
                    }
                }

                //Compute intensity gradients in y-direction
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    for (int nRow = 0; nRow < (m_nHeight - nWindow + 1); nRow++)
                    {
                        nMaxX = 0; nMinN = Int32.MaxValue;
                        for (int k = nRow; k < (nRow + nWindow); k++)
                        {
                            if (GetAvgPixel1(nCol, k) < nMinN)
                            {
                                nMinN = GetAvgPixel1(nCol, k);
                            }
                            if (GetAvgPixel1(nCol, k) > nMaxX)
                            {
                                nMaxX = GetAvgPixel1(nCol, k);
                            }
                        }
                        if (nMaxX - nMinN >= nTH)
                            for (int k = nRow; k < (nRow + nWindow); k++)
                            {
                                pIGy[k, nCol] = 1;
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

        // Description : Given a binary map, removes isolated 1's in the x direction.
        // INPUTS      : binary pRaster 
        //               nLen - smallest # of 1's that is allowed to remain. 
        //               For example, if nLen = 3, then 
        //               0000011000011100000 becomes 0000000000011100000. 
        private bool removeIsolatedXPixels(ref byte[,] pRaster, int nLen)
        {
            int nRow = 0, nCol = 0;
            try
            {
                int nShift, nCurrLen;

                for (nRow = 0; nRow < m_nHeight; nRow++)
                {
                    nCurrLen = 0;
                    for (nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (pRaster[nRow, nCol] != 0) nCurrLen++;
                        else if (nCurrLen < nLen)
                        {
                            for (nShift = (nCol - nCurrLen); nShift < nCol; nShift++)
                                pRaster[nRow, nCol] = 0;
                            nCurrLen = 0;
                        }
                        else
                            nCurrLen = 0;
                    }// endfor nCol 

                    if (nCurrLen < nLen)
                        for (nShift = (nCol - nCurrLen); nShift < nCol; nShift++)
                            pRaster[nRow, nShift] = 0;
                }// endfor nRow

                return true;
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "\nRow = " + nRow.ToString() + " Col = " + nCol.ToString());
                throw newEx;
            }
        }

        // compute_igyd
        // Given pIGy (the boolean map of intensity gradients in the 
        // y direction), computes pIGyd (the integer map of distances to ig's. 
        // Interpretation:  pIGyd = 0 means pixel is ig in y-direction
        //                   "   = d means nearest ig is d pixels  below
        //                   "   = -d   "     "     "       "      above 
        //                         (d is positive)
        private bool computeIGyd(ref byte[,] pIGy, ref int[,] pIGyd)
        {
            try
            {
                int nCurrDist = 0;

                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    // Expand downward 
                    nCurrDist = (-1 * UInt16.MaxValue);
                    for (int nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        if (pIGy[nRow, nCol] != 0) nCurrDist = 0;
                        else nCurrDist--;

                        pIGy[nRow, nCol] = (byte)nCurrDist;
                    }
                    // Expand upward
                    nCurrDist = UInt16.MaxValue;
                    for (int nRow = (m_nHeight - 1); nRow >= 0; nRow--)
                    {
                        if (pIGy[nRow, nCol] != 0) nCurrDist = 0;
                        else nCurrDist++;

                        if (nCurrDist < -1 * (pIGy[nRow, nCol]))
                            pIGy[nRow, nCol] = (byte)nCurrDist;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Propagates each moderately reliable region in the y direction until
        // it hits an intensity gradient.  (? First checks whether border of  
        // reliable region is just beyond an intensity gradient.  If so, then  
        // the region has probably extended into an object by accident, 
        // so therefore it backs up to align its border with the intensity  
        // gradient.?)  Propagation occurs as int as neighboring region is  
        // either unreliable or has a disparity at least two greater than the 
        // current region. 
        private bool propagateY(ref byte[,] pDM, ref byte[,] pIGy, ref int[,] pIGyd, ref int[,] pReliabilityMap)
        {
            try
            {
                int k = 0;
                int currDisparity;

                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    int nRow = 0;
                    while (nRow < m_nHeight)
                    {
                        // Find top of new stable region
                        while (nRow < m_nHeight && pReliabilityMap[nRow, nCol] < (int)m_nModeratelyReliableThreshold)
                            nRow++;
                        if (nRow >= m_nHeight)
                            break;
                        currDisparity = (int)pDM[nRow, nCol];

                        //expand region upward 
                        for (k = (nRow - 1); k >= 0 && 0 == pIGy[k, nCol] &&
                              (pReliabilityMap[k, nCol] < m_nSlightlyReliableThreshold || pDM[k, nCol] == currDisparity); k--)
                        {
                            pDM[k, nCol] = (byte)currDisparity;
                            pReliabilityMap[k, nCol] = m_nModeratelyReliableThreshold;
                        }

                        // Find bottom of stable region 
                        for (k = nRow + 1; k < m_nHeight && pDM[k, nCol] == currDisparity; k++) ;
                        nRow = k - 1;

                        if (k >= m_nHeight) break;

                        // expand region downward 
                        for (; k < m_nHeight && 0 == pIGy[k, nCol] && (pReliabilityMap[k, nCol] < (int)m_nSlightlyReliableThreshold
                              || pDM[k, nCol] == currDisparity); k++)
                        {
                            pDM[k, nCol] = (byte)currDisparity;
                            pReliabilityMap[k, nCol] = m_nModeratelyReliableThreshold;
                        }
                        nRow = k;
                    }// endwhile nRow
                }// endfor nCol

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // removeIsolatedPixelsY
        // Same as removeIsolatedXPixels, except in the y direction.
        private bool removeIsolatedYPixels(ref byte[,] pRaster, int nLen)
        {
            try
            {
                int nCurrLen;

                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    nCurrLen = 0;
                    int nRow = 0;
                    for (nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        if (0 != pRaster[nRow, nCol]) nCurrLen++;
                        else if (nCurrLen < nLen)
                        {
                            for (int k = (nRow - nCurrLen); k < nRow; k++)
                                pRaster[k, nCol] = 0;
                            nCurrLen = 0;
                        }
                        else
                            nCurrLen = 0;
                    }// endfor nRow

                    if (nCurrLen < nLen)
                        for (int k = (nRow - nCurrLen); k < nRow; k++)
                            pRaster[k, nCol] = 0;
                }// endfor nCol

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Given pIGx (the boolean map of intensity gradients in the  
        // x direction), computes pIGxd (the integer map of distances to ig's).
        // Interpretation:  pIGxd = 0 means pixel is ig in x-direction 
        //                   "   = d means nearest ig is d pixels to the right
        //                   "   = -d   "     "     "       "     to the left 
        //                         (d is positive) 
        private bool computeIGxd(ref byte[,] pIGx, ref int[,] pIGxd)
        {
            try
            {
                int nCurrDist;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    int nCol = 0;
                    // Expand rightward 
                    nCurrDist = -1 * UInt16.MaxValue;
                    for (nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (0 != pIGx[nRow, nCol]) nCurrDist = 0;
                        else nCurrDist--;

                        pIGxd[nRow, nCol] = nCurrDist;
                    }
                    // Expand leftward 
                    nCurrDist = UInt16.MaxValue;
                    for (nCol = (m_nWidth - 1); nCol >= 0; nCol--)
                    {
                        if (0 != pIGx[nRow, nCol]) nCurrDist = 0;
                        else nCurrDist++;

                        if (nCurrDist < -1 * (pIGxd[nRow, nCol]))
                            pIGxd[nRow, nCol] = nCurrDist;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // computeReliabilitiesX 
        // Same as computeReliabilitiesY, but in the x direction. 
        private bool computeReliabilitiesX(ref byte[,] pDM, ref int[,] pReliabilityMap)
        {
            try
            {
                int currDisparity, currLength;

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    int nCol = 0;
                    currDisparity = -1;
                    currLength = 0;
                    for (nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (pDM[nRow, nCol] == currDisparity)
                            currLength++;
                        else
                        {
                            for (int k = nCol - currLength; k < nCol; k++)
                                pReliabilityMap[nRow, k] = currLength;
                            currDisparity = pDM[nRow, nCol];
                            currLength = 1;
                        }
                    }
                    for (int k = nCol - currLength; k < nCol; k++)
                        pReliabilityMap[nRow, k] = currLength;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Labels each pixel with its "reliability".  Reliability of a pixel P
        // is defined as the number of pixels in P's region, where the region is 
        // the contiguous set of pixels (in the y direction) with the same 
        // disparity as P. 
        private bool computeReliabilitiesY(ref byte[,] pDM, ref int[,] pReliabilityMap)
        {
            try
            {
                int currDisparity, currLength;

                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    int nRow = 0;
                    currDisparity = -1;
                    currLength = 0;
                    for (nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        if (pDM[nRow, nCol] == currDisparity)
                            currLength++;
                        else
                        {
                            for (int k = nRow - currLength; k < nRow; k++)
                                pReliabilityMap[k, nCol] = currLength;

                            currDisparity = pDM[nRow, nCol];
                            currLength = 1;
                        }
                    }

                    for (int k = nRow - currLength; k < nRow; k++)
                        pReliabilityMap[k, nCol] = currLength;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Same as propagateY, except in the x direction.
        private bool propagateX(ref byte[,] pDM, ref byte[,] pIGx, ref int[,] pIGxd, ref int[,] pReliabilityMap)
        {
            try
            {
                int currDisparity = 0;

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    int nCol = 0;
                    while (nCol < m_nWidth)
                    {
                        //Find left of new stable region
                        while (nCol < m_nWidth && (pReliabilityMap[nRow, nCol]) < (int)m_nModeratelyReliableThreshold) nCol++;
                        if (nCol >= m_nWidth) break;
                        currDisparity = pDM[nRow, nCol];

                        // expand region leftward 
                        int k = 0;
                        for (k = (nCol - 1); k >= 0 && 0 != pIGx[nRow, k] && (pReliabilityMap[nRow, k] < (int)m_nSlightlyReliableThreshold
                             || pDM[nRow, k] == currDisparity); k--)
                        {
                            pDM[nRow, k] = (byte)currDisparity;
                            pReliabilityMap[nRow, k] = m_nModeratelyReliableThreshold;
                        }

                        // Find right of stable region
                        for (k = (nCol + 1); k < m_nWidth && (pDM[nRow, k]) == currDisparity; k++) ;
                        nCol = k - 1;
                        if (k >= m_nWidth)
                            break;

                        // expand region rightward 
                        for (; k < m_nWidth && 0 != pIGx[nRow, k] && (pReliabilityMap[nRow, k] < (int)m_nSlightlyReliableThreshold
                                || pDM[nRow, k] == currDisparity); k++)
                        {
                            pDM[nRow, k] = (byte)currDisparity;
                            pReliabilityMap[nRow, k] = m_nModeratelyReliableThreshold;
                        }
                        nCol = k;
                    }// endwhile nCol
                }// endfor nRow
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Performs nMode filtering in the y direction on an pRaster whose  
        // elements range from 0 to m_nDisparity. 
        // INPUTS 
        // pRaster:  an pRaster whose elements range from 0 to m_nDisparity 
        // h:  filter height (must be odd) 
        // NOTES 
        // This is an inefficient implementation, but it works. 
        private bool modeYFilter(ref byte[,] pRaster, int nWindow)
        {
            try
            {
                int nMaxX, nMode = 0;
                int hh = nWindow / 2;  // filter half-height
                int nDisparity, nInertia;

                int[] pHistogram = new int[m_nDisparity + 1];

                if (nWindow % 2 == 0) MessageBox.Show("modeYFilter:  filter height must be odd");

                for (int nCol = 0; nCol < m_nWidth; nCol++)
                    for (int nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        int k = 0;
                        for (k = 0; k <= m_nDisparity; k++)
                            pHistogram[k] = 0;
                        for (k = CDefinitions.MAX(0, nRow - hh); k <= CDefinitions.MIN(m_nHeight - 1, nRow + hh); k++)
                        {
                            nDisparity = pRaster[k, nCol];
                            (pHistogram[nDisparity])++;
                        }
                        nDisparity = pRaster[nRow, nCol];
                        nMode = nDisparity;
                        nMaxX = pHistogram[nDisparity];
                        for (k = 0; k <= m_nDisparity; k++)
                            if (pHistogram[k] > nMaxX)
                            {
                                nMaxX = pHistogram[k];
                                nMode = k;
                            }
                        nInertia = pHistogram[nDisparity];

                        if (nDisparity > 0) nInertia += pHistogram[nDisparity - 1];
                        if (nDisparity < m_nDisparity) nInertia += pHistogram[nDisparity + 1];
                        if (nMaxX > nInertia)
                        {
                            pRaster[nRow, nCol] = (byte)nMode;
                        }
                        else
                        {
                            nMode = nDisparity;
                            nMaxX = pHistogram[nDisparity];

                            if (nDisparity > 0 && pHistogram[nDisparity - 1] > nMaxX)
                            {
                                nMaxX = pHistogram[nDisparity - 1];
                                nMode = nDisparity - 1;
                            }

                            if (nDisparity < m_nDisparity && pHistogram[nDisparity + 1] > nMaxX)
                            {
                                nMode = nDisparity + 1;
                            }

                            pRaster[nRow, nCol] = (byte)nMode;
                        }
                    }

                //delete[] pHistogram;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Same as modefilterY, but in the x direction.
        private bool modeXFilter(ref byte[,] pRaster, int nWindow)
        {
            try
            {
                int nMaxX, nMode;
                int hw = nWindow / 2;     // filter half-width
                int nComputeDisparity;
                int nInertia;

                int[] pHistogram = new int[m_nDisparity + 1];

                if (nWindow % 2 == 0)
                    MessageBox.Show("modeXFilter:  filter width must be odd");

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int k = 0;
                        for (k = 0; k <= m_nDisparity; k++)
                            pHistogram[k] = 0;
                        for (k = CDefinitions.MAX(0, nCol - hw); k <= CDefinitions.MIN(m_nWidth - 1, nCol + hw); k++)
                        {
                            nComputeDisparity = pRaster[nRow, k];
                            (pHistogram[nComputeDisparity])++;
                        }

                        nComputeDisparity = pRaster[nRow, nCol];
                        nMode = nComputeDisparity;
                        nMaxX = pHistogram[nComputeDisparity];
                        for (k = 0; k <= m_nDisparity; k++)
                            if (pHistogram[k] > nMaxX)
                            {
                                nMaxX = pHistogram[k];
                                nMode = k;
                            }
                        nInertia = pHistogram[nComputeDisparity];
                        if (nComputeDisparity > 0) nInertia += pHistogram[nComputeDisparity - 1];
                        if (nComputeDisparity < m_nDisparity) nInertia += pHistogram[nComputeDisparity + 1];
                        if (CDefinitions.ABSOLUTE_1(nMode - nComputeDisparity) <= 1 || nMaxX > nInertia)
                        {
                            pRaster[nRow, nCol] = (byte)nMode;
                        }
                        else
                        {
                            nMode = nComputeDisparity;
                            nMaxX = pHistogram[nComputeDisparity];
                            if (nComputeDisparity > 0 && pHistogram[nComputeDisparity - 1] > nMaxX)
                            {
                                nMaxX = pHistogram[nComputeDisparity - 1];
                                nMode = nComputeDisparity - 1;
                            }
                            if (nComputeDisparity < m_nDisparity && pHistogram[nComputeDisparity + 1] > nMaxX)
                            {
                                nMode = nComputeDisparity + 1;
                            }
                            pRaster[nRow, nCol] = (byte)nMode;
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

        // Given a disparity map, computes the depth discontinuities as those 
        // pixels that border a change in disparity of at least two levels,  
        // and that lie on the background.  (This latter condition is necessary
        // to prevent two neighboring pixels from both being declared  
        // discontinuities.)
        private bool computeDepthDiscontinuities(ref byte[,] pDM, ref byte[,] pDD)
        {
            try
            {
                int nRow, nCol;

                for (nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        if (pDM[nRow, nCol] < (pDM[(nRow + 1), nCol] - 1) || 
                            pDM[nRow, nCol] < (pDM[(nRow - 1), nCol] - 1) || 
                            pDM[nRow, nCol] < (pDM[nRow, nCol + 1] - 1) || 
                            pDM[nRow, nCol] < (pDM[nRow, nCol - 1] - 1))
                            pDD[nRow, nCol] = DISCONTINUITY;
                        else
                            pDD[nRow, nCol] = NO_DISCONTINUITY;
                    }
                }

                for (nRow = 0; nRow < m_nHeight; nRow++)
                {
                    pDD[nRow, 0] = NO_DISCONTINUITY;
                    pDD[nRow, m_nWidth - 1] = NO_DISCONTINUITY;
                }

                for (nCol = 0; nCol < m_nWidth; nCol++)
                {
                    pDD[0, nCol] = NO_DISCONTINUITY;
                    pDD[m_nHeight - 1, nCol] = NO_DISCONTINUITY;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Version used in STAN-CS-TR-96-1573 and ICCV '98 
        // Postprocess the disparity map.  The basic idea is to propagate 
        // reliable regions into unreliable regions, stopping at intensity  
        // gradients.  (The actual rules are more complicated, of course.)   
        // Further processing, like nMode filtering, helps to clean up things. 

        // INPUTS 
        // m_imgInfo1.GetAvgRaster():  original left intensity image 
        // pDM1:  disparity map computed by matching the scanlines independently 

        // OUTPUTS 
        // pDM:  disparity map after postprocessing 
        // dDD2:  depth discontinuities after postprocessing 
        //*************************************************************************/
        private bool postProcessDepthImage(ref byte[,] pDM, ref byte[,] dDD2)
        {
            try
            {
                // CALCULATING THE RELIABLE THRESHOLD
                m_nSlightlyReliableThreshold = (int)(m_nReliableThreshold * (1 - m_fAlpha) + 0.5f);
                m_nModeratelyReliableThreshold = (int)(m_nReliableThreshold * (1 + m_fAlpha) + 0.5f);

                if ((m_nSlightlyReliableThreshold > m_nModeratelyReliableThreshold))
                {
                    MessageBox.Show("Postprocess:  Reliability thresholds do not obey monotonicity.");
                }

                // REMOVE "OBVIOUS ERRORS" IN THE DISPARITY MAP
                // Writing the binary map of the intensity gradients is constructed by 
                // centring the 3*1 vertical windows around every pixels in the image.
                // distance to nearest gradient
                if (false == coerceSurroundedPixels(ref pDM)) return false;

                // Compute intensity gradients
                // intensity gradients      
                byte[,] pIGx = new byte[m_nHeight, m_nWidth];
                byte[,] pIGy = new byte[m_nHeight, m_nWidth];
                if (false == computeIGxy(ref pIGx, ref pIGy)) return false;
                // ROMOVE "ISOLATED" INTENSITY GRADIENT IN THE y DIRECTION
                if (false == removeIsolatedXPixels(ref pIGy, 3)) return false;

                int[,] pIGyd = new int[m_nHeight, m_nWidth];
                int[,] pIGxd = new int[m_nHeight, m_nWidth];
                // PROPAGATE RELIABLE REGIONS IN THE  y DIRECTION 
                if (false == computeIGyd(ref pIGy, ref pIGyd)) return false;

                // reliabilities of pixels
                int[,] pReliabilityMap = new int[m_nHeight, m_nWidth];
                if (false == computeReliabilitiesY(ref pDM, ref pReliabilityMap)) return false;
                if (false == propagateY(ref pDM, ref pIGy, ref pIGyd, ref pReliabilityMap)) return false;

                // REMOVE "ISOLATED" INTENSITY GRADIENTS IN THE X DIRECTION
                if (false == removeIsolatedYPixels(ref pIGx, 3)) return false;

                // PROPAGATE RELIABLE REGIONS IN THE x DIRECTION
                if (false == computeIGxd(ref pIGx, ref pIGxd)) return false;
                if (false == computeReliabilitiesX(ref pDM, ref pReliabilityMap)) return false;
                if (false == propagateX(ref pDM, ref pIGx, ref pIGxd, ref pReliabilityMap)) return false;

                // MODE FILTER THE DIPARITY MAP
                if (false == modeYFilter(ref pDM, 11)) return false;
                if (false == modeXFilter(ref pDM, 11)) return false;

                // FIND THE DEPTH DISCONTINUOTIES FROM THE DISPARITY MAP 
                if (false == computeDepthDiscontinuities(ref pDM, ref dDD2)) return false;

                // Storing the Histogram equalised result
                //byte[,] pOutRaster = new byte[m_nHeight, m_nWidth];
                //delete[] pReliabilityMap;
                //delete[] pIGx;
                //delete[] pIGy;
                //delete[] pIGxd;
                //delete[] pIGyd;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool getEqualizedImage(ref byte[,] pDisparityMap, ref byte[,] pOutRaster)
        {
            try
            {
                int[] arrHistogram = new int[CDefinitions.SCALE];
                //calculating the number of pixels of particular intensity
                int nBase = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        pOutRaster[nRow, nCol] = 0;
                        arrHistogram[pDisparityMap[nRow, nCol]]++;
                    }
                    nBase += m_nWidth;
                }

                for (int i = 1; i < CDefinitions.SCALE; i++)
                {
                    arrHistogram[i] += arrHistogram[i - 1];
                }

                int nTotal = m_nWidth * m_nHeight;
                for (int i = 0; i < CDefinitions.SCALE; i++)
                {
                    arrHistogram[i] = (byte)((255.0f * arrHistogram[i]) / nTotal);
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        pOutRaster[nRow, nCol] = (byte)arrHistogram[pDisparityMap[nRow, nCol]];
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

        #region PUBLIC_FUNCTION

        // This function generates the 2-D image from a stereo image pairs
        public bool DepthImage(ref Bitmap dest1, ref Bitmap dest2)
        {
            try
            {
                byte[,] pDisparityMap = new byte[m_nHeight, m_nWidth];
                byte[,] pDDMap1 = new byte[m_nHeight, m_nWidth];

                //MATCH SCANLINE USING DYNAMIC PROGRAMMING
                bool bSuccess = getMatchScanlines(ref pDisparityMap, ref pDDMap1);
#if TEST
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nI = pDisparityMap[nRow, nCol];
                        dest1.SetPixel(nCol, nRow, Color.FromArgb(nI, nI, nI));
                        nI = pDDMap1[nRow, nCol];
                        dest2.SetPixel(nCol, nRow, Color.FromArgb(nI, nI, nI));
                    }
                }
#endif

                if (bSuccess)
                {
                    byte[,] pDDMap2 = new byte[m_nHeight, m_nWidth];
                    // DOING POSTPROCESSING
                    bSuccess &= postProcessDepthImage(ref pDisparityMap, ref pDDMap2);
                    if (bSuccess)
                    {
                        byte[,] arrOutRaster = new byte[m_nHeight, m_nWidth];
                        bSuccess = getEqualizedImage(ref pDisparityMap, ref arrOutRaster);
                        if (bSuccess)
                        {
                            for (int nRow = 0; nRow < m_nHeight; nRow++)
                            {
                                for (int nCol = 0; nCol < m_nWidth; nCol++)
                                {
                                    int nI = arrOutRaster[nRow, nCol];
                                    dest1.SetPixel(nCol, nRow, Color.FromArgb(nI, nI, nI));
                                    nI = pDDMap2[nRow, nCol];
                                    dest2.SetPixel(nCol, nRow, Color.FromArgb(nI, nI, nI));
                                }
                            }

                            for (int nRow = 0; nRow < m_nHeight; nRow++)
                            {
                                for (int nCol = 0; nCol < m_nWidth; nCol++)
                                {
                                    m_pHistogram[arrOutRaster[nRow, nCol]]++;
                                }
                            }
                        }
                    }
                }

                return bSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
