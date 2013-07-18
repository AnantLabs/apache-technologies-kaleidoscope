using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Apache.ImageLib
{
    public class CRegions : CImageInfo
    {
        #region ENUMS

        public enum OUTLINE_DIR
        {
            eNONE = -1,
            eEAST = 0,
            eSOUTH,
            eWEST,
            eNORTH,
            eNORTH_EAST,
            eSOUTH_EAST,
            eSOUTH_WEST,
            eNORTH_WEST,
            eSINGLE
        };

        #endregion

        #region CTOR

        public CRegions(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        #region REGION_LABELLING

        protected Bitmap getLabeledImg(CDefinitions.REGION_TYPE eRegionType, bool bCreateOutputImage, ref int nObjects)
        {
            Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    byte I = CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow));
                    if (CDefinitions.REGION_TYPE.eBlack == eRegionType)
                    {
                        I = (byte)(CDefinitions.IMAX - I);
                    }
                    RGB o = new RGB(I, I, I);
                    dest.SetPixel(nCol, nRow, o.Color);
                }
            }

            // Initialisation the number of counts
            int nRegionNum = 1, nTemp = 0;
            int North_East = -1, North = -1, North_West = -1, West = -1;
            int[] ObjectsDectected = new int[CDefinitions.MAX_OBJECTS];
            ObjectsDectected[0] = 0;

            int[] vPreviousLine = new int[m_nWidth];
            int[] vCurrentLine = new int[m_nWidth];
            int[] vLabels = new int[m_nWidth * m_nHeight];

            int nBase = 0;
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    North = vPreviousLine[nCol];
                    //For the first coloum
                    if (nCol == 0)
                    {
                        West = 0;
                        North_West = 0;
                    }
                    else if (nCol > 1)
                    {
                        West = vCurrentLine[nCol - 1];
                        North_West = vPreviousLine[nCol - 1];
                    }
                    // For the last coloum
                    if (nCol == m_nWidth - 1) North_East = 0;
                    else if (nCol < m_nWidth - 1) North_East = vPreviousLine[nCol + 1];

                    // If the Value of InRaster is not zero
                    byte I = CDefinitions.CLR2AVR(dest.GetPixel(nCol, nRow));
                    if (I != 0)
                    {
                        if (North_East == 0 && North == 0 && North_West == 0 && West == 0)
                        {
                            if (nRegionNum >= CDefinitions.MAX_OBJECTS) return null;
                            nRegionNum++;
                            vCurrentLine[nCol] = nRegionNum;
                            ObjectsDectected[nRegionNum] = nRegionNum;
                        }
                        else
                        {
                            vCurrentLine[nCol] = getMax4(North_East, North, North_West, West);
                            nTemp = getMin4Nz(ObjectsDectected[North_East],
                                            ObjectsDectected[North],
                                            ObjectsDectected[North_West],
                                            ObjectsDectected[West]);

                            ObjectsDectected[ObjectsDectected[North_East]] = nTemp;
                            ObjectsDectected[ObjectsDectected[North]] = nTemp;
                            ObjectsDectected[ObjectsDectected[North_West]] = nTemp;
                            ObjectsDectected[ObjectsDectected[West]] = nTemp;
                            ObjectsDectected[North_East] = nTemp;
                            ObjectsDectected[North] = nTemp;
                            ObjectsDectected[North_West] = nTemp;
                            ObjectsDectected[West] = nTemp;
                            ObjectsDectected[0] = 0;
                        }
                    }
                    else
                        vCurrentLine[nCol] = 0;
                }

                Array.Copy(vCurrentLine, 0, vLabels, nBase, m_nWidth);
                Array.Copy(vCurrentLine, vPreviousLine, m_nWidth);
                nBase += m_nWidth;
            }

            for (int nCnt = nRegionNum; nCnt > 1; nCnt--)
            {
                while (ObjectsDectected[ObjectsDectected[nCnt]] < ObjectsDectected[nCnt])
                {
                    ObjectsDectected[nCnt] = ObjectsDectected[ObjectsDectected[nCnt]];
                }
            }

            int[] vLkpHistogram = new int[nRegionNum + 1];
            Array.Clear(vLkpHistogram, 0, nRegionNum + 1);
            for (int nCnt = 0; nCnt <= nRegionNum; nCnt++)
            {
                vLkpHistogram[ObjectsDectected[nCnt]]++;
            }

            // Storing the number of Objects found
            for (int nCnt = 0; nCnt <= nRegionNum; nCnt++) // SG - Changed indexing from 1 to 0
            {
                if (vLkpHistogram[nCnt] != 0) nObjects++;
            }

            if (bCreateOutputImage)
            {
                vLkpHistogram = Enumerable.Repeat(0x00, nRegionNum + 1).ToArray();
                for (int nCnt = 0; nCnt < m_nHeight * m_nWidth; nCnt++)
                {
                    vLkpHistogram[ObjectsDectected[vLabels[nCnt]]]++;
                }

                int[] vTable = new int[nRegionNum + 1];
                for (int nCnt = 0; nCnt <= nRegionNum; nCnt++)
                {
                    vTable[nCnt] = nCnt;
                }

                int nMaxIndex = -1, nMaxInt, nSize = 1;
                for (int nRow = 1; nRow <= nRegionNum; nRow++)
                {
                    nMaxInt = -2;
                    for (int nCol = 1; nCol <= nRegionNum; nCol++)
                    {
                        if (nMaxInt < vLkpHistogram[nCol])
                        {
                            nMaxInt = vLkpHistogram[nCol];
                            nMaxIndex = nCol;
                        }
                    }
                    if (nMaxInt != -2)
                    {
                        //largest object gets label 1
                        vTable[nMaxIndex] = nSize++;
                        vLkpHistogram[nMaxIndex] = -3;
                    }
                }

                for (int nCnt = 0; nCnt <= nRegionNum; nCnt++)
                {
                    if (vTable[nCnt] > CDefinitions.SCALE) vTable[nCnt] = 0;
                }

                for (int nCnt = 0, nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++, nCnt++)
                    {
                        byte nI = (byte)vTable[ObjectsDectected[vLabels[nCnt]]];
                        RGB o = new RGB(nI, nI, nI);
                        dest.SetPixel(nCol, nRow, o.Color);
                    }
                }
            }
            return dest;
        }

        //Name          : int StretchBMP(Bitmap src);
        //References    : Digital image processing by W.K.Pratt, Supercharged bit
        //Functionality : This function stretches an input image.
        //Returns       : 0 on success else error no..
        //Remarks       : None.
        protected Bitmap getStretchedImg(Bitmap src)
        {
            int maxR = -1; Color clr;
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    clr = src.GetPixel(nCol, nRow);
                    if (clr.R > maxR) maxR = clr.R;
                }
            }

            Random random = new Random();
            Bitmap dest = new Bitmap(m_nWidth, m_nHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    int I = CDefinitions.CLR2AVR(src.GetPixel(nCol, nRow));

                    if (I > 0)
                    {
                        clr = Color.FromArgb((I + 50) * (CDefinitions.IMAX * CDefinitions.IMAX * CDefinitions.IMAX) / maxR);
                        //clr = Color.FromArgb(random.Next(1, CDefinitions.IMAX * CDefinitions.IMAX * CDefinitions.IMAX)); 
                    }
                    else
                        clr = Color.Black;

                    dest.SetPixel(nCol, nRow, clr);
                }
            }
            src = null;
            return dest;
        }

        #endregion

        #region OBJECTS_OUTLINE

        public bool GetOutlinedObjects(Bitmap src, int nObjNum, ref List<System.Drawing.Point> lCoords)
        {
            try
            {
                // TO LOCATE THE FIRST PIXEL MATCHING WITH THE GIVEN OBJECT NUMBER
                bool bNoBlank = false;
                int nRow = 0, nCol = 0;
                for (nRow = 0; nRow < m_nHeight && !bNoBlank; nRow++)
                {
                    for (nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        bNoBlank = CDefinitions.CLR2AVR(src.GetPixel(nCol, nRow)) == nObjNum;
                        if (bNoBlank) break;
                    }
                }

                if (false == bNoBlank) return (false);

                System.Drawing.Point ptStart = new System.Drawing.Point(nCol, nRow - 1);

                lCoords.Add(new System.Drawing.Point(ptStart.X, ptStart.Y));
                System.Drawing.Point ptNow = ptStart;

                bool bComplete = false; OUTLINE_DIR eDir = OUTLINE_DIR.eEAST;
                while (!bComplete)
                {
                    switch (eDir = getNearestFriend(ref src, ptNow, nObjNum, eDir))
                    {
                        case OUTLINE_DIR.eEAST:
                            ptNow.X++;
                            break;
                        case OUTLINE_DIR.eSOUTH_EAST:
                            ptNow.X++; ptNow.Y++;
                            break;
                        case OUTLINE_DIR.eSOUTH:
                            ptNow.Y++;
                            break;
                        case OUTLINE_DIR.eSOUTH_WEST:
                            ptNow.Y++; ptNow.X--;
                            break;
                        case OUTLINE_DIR.eWEST:
                            ptNow.X--;
                            break;
                        case OUTLINE_DIR.eNORTH_WEST:
                            ptNow.X--; ptNow.Y--;
                            break;
                        case OUTLINE_DIR.eNORTH:
                            ptNow.Y--;
                            break;
                        case OUTLINE_DIR.eNORTH_EAST:
                            ptNow.Y--; ptNow.X++;
                            break;
                        case OUTLINE_DIR.eSINGLE:
                            bComplete = true;
                            break;
                    }//END SWITCH

                    // Accumulate the peripheral coordinates for the desired label
                    lCoords.Add(ptNow);

                    if (ptNow == ptStart) break;
                } // Completed

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Here we would locate the friend by travesing in the clockwise direction and 
           diagonal opposite would be the last to check. So we would start 
           from direction next to the diagonal Opposite direction*/
        protected OUTLINE_DIR getNearestFriend(ref Bitmap src, System.Drawing.Point pt, int nObjNum, OUTLINE_DIR eDir)
        {
            List<OUTLINE_DIR> sequence = new List<OUTLINE_DIR>();
            switch (eDir)
            {
                case OUTLINE_DIR.eEAST:
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eSOUTH_EAST:
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eSOUTH:
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eSOUTH_WEST:
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eWEST:
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eNORTH_WEST:
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eNORTH:
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
                case OUTLINE_DIR.eNORTH_EAST:
                    sequence.Add(OUTLINE_DIR.eWEST);
                    sequence.Add(OUTLINE_DIR.eNORTH_WEST);
                    sequence.Add(OUTLINE_DIR.eNORTH);
                    sequence.Add(OUTLINE_DIR.eNORTH_EAST);
                    sequence.Add(OUTLINE_DIR.eEAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH_EAST);
                    sequence.Add(OUTLINE_DIR.eSOUTH);
                    sequence.Add(OUTLINE_DIR.eSOUTH_WEST);
                    return searchFriend(ref src, ref sequence, pt.X, pt.Y, nObjNum);
            }

            return OUTLINE_DIR.eNONE;
        }

        protected OUTLINE_DIR searchFriend(ref Bitmap src, ref List<OUTLINE_DIR> sequence, int nX, int nY, int nObjNum)
        {
            for (int nCnt = 0; nCnt < sequence.Count; )
            {
                switch (sequence[nCnt])
                {
                    case OUTLINE_DIR.eEAST:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX + 1, nY)) == nObjNum) return (OUTLINE_DIR.eEAST);
                        break;
                    case OUTLINE_DIR.eSOUTH_EAST:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX + 1, nY + 1)) == nObjNum) return (OUTLINE_DIR.eSOUTH_EAST);
                        break;
                    case OUTLINE_DIR.eSOUTH:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX, nY + 1)) == nObjNum) return (OUTLINE_DIR.eSOUTH);
                        break;
                    case OUTLINE_DIR.eSOUTH_WEST:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX - 1, nY + 1)) == nObjNum) return (OUTLINE_DIR.eSOUTH_WEST);
                        break;
                    case OUTLINE_DIR.eWEST:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX - 1, nY)) == nObjNum) return (OUTLINE_DIR.eWEST);
                        break;
                    case OUTLINE_DIR.eNORTH_WEST:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX - 1, nY - 1)) == nObjNum) return (OUTLINE_DIR.eNORTH_WEST);
                        break;
                    case OUTLINE_DIR.eNORTH:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX, nY - 1)) == nObjNum) return (OUTLINE_DIR.eNORTH);
                        break;
                    case OUTLINE_DIR.eNORTH_EAST:
                        if (CDefinitions.CLR2AVR(src.GetPixel(nX + 1, nY - 1)) == nObjNum) return (OUTLINE_DIR.eNORTH_EAST);
                        break;
                }//END SWITCH
                ++nCnt;
            }//END FOR

            return (OUTLINE_DIR.eSINGLE);
        }

        #endregion

        #region OBJECTS_FEATURE

        public bool GetObject2DFeatures(ref List<System.Drawing.Point> lCoords, double dXScale, double dYScale,
            ref CBlobAnalysis.OBJECT2DGEOMETRY objGeo)
        {
            try
            {
                double dXMax, dXMin, dYMax, dYMin;
                dXMin = dXMax = lCoords[0].X;
                dYMin = dYMax = lCoords[0].Y;

                double dXT, dYT;
                double dXSum = 0.0; double dYSum = 0.0;
                for (int nCnt = 0; nCnt < lCoords.Count; nCnt++)
                {
                    dXT = lCoords[nCnt].X; dYT = lCoords[nCnt].Y;

                    dXSum += dXT; dYSum += dYT;

                    if (dXT > dXMax) dXMax = dXT;
                    if (dXT < dXMin) dXMin = dXT;
                    if (dYT > dYMax) dYMax = dYT;
                    if (dYT < dYMin) dYMin = dYT;
                }

                double dXCentre, dYCentre;

                objGeo.nCount = lCoords.Count;
                objGeo.dXScale = dYScale;
                objGeo.dYScale = dYScale;
                objGeo.dXCentre = dXScale * (dXCentre = dXSum / lCoords.Count);
                objGeo.dYCentre = dYScale * (dYCentre = dYSum / lCoords.Count);

                objGeo.dXMax = dXScale * (dXMax);
                objGeo.dXMin = dXScale * (dXMin);
                objGeo.dYMax = dYScale * (dYMax);
                objGeo.dYMin = dYScale * (dYMin);

                int nNext;
                double dSumArea = 0.0;
                double dPerimeter = 0.0;
                double dHorizontal = dXScale * 0.948f;
                double dVertical = dYScale * 0.948f;
                double dDiagonal = Math.Sqrt(dHorizontal * dHorizontal + dVertical * dVertical);

                for (int nCnt = 0; nCnt < lCoords.Count; nCnt++)
                {
                    dXT = dXScale * (lCoords[nCnt].X - dXCentre);
                    dYT = dYScale * (lCoords[nCnt].Y - dYCentre);

                    if (nCnt != (lCoords.Count - 1)) nNext = nCnt + 1;
                    else nNext = 0;

                    dSumArea += (dXT * dXScale * (lCoords[nNext].X - dYCentre)) -
                        (dYT * dYScale * (lCoords[nNext].Y - dXCentre));

                    if (lCoords[nCnt].X == lCoords[nNext].X)
                        dPerimeter += dVertical;
                    else if (lCoords[nCnt].Y == lCoords[nNext].Y)
                        dPerimeter += dHorizontal;
                    else
                        dPerimeter += dDiagonal;
                }

                bool bSuccess = false;
                objGeo.dArea = 0.5 * Math.Abs(dSumArea);
                if (objGeo.dArea > 0)
                {

                    objGeo.dPerimeter = dPerimeter;
                    objGeo.dShapeFactor = objGeo.dArea / (dPerimeter * dPerimeter);

                    double dRadius = 0.0f;
                    if (getCircleFit(ref lCoords, ref dXCentre, ref dYCentre, ref dRadius, ref objGeo))
                    {
                        objGeo.dXCentre = dXCentre;
                        objGeo.dYCentre = dYCentre;
                        objGeo.dRadius = dRadius;

                        bSuccess = true;
                    }
                }
                return bSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool getCircleFit(ref List<System.Drawing.Point> lCoords, ref double dXCentre, ref double dYCentre,
            ref double dRadius, ref CBlobAnalysis.OBJECT2DGEOMETRY objGeo)
        {
            double[] A = new double[9];
            double[] B = new double[3];
            double Z;

            for (int nCnt = 0; nCnt < 9; nCnt++) A[nCnt] = 0.0f;

            for (int nCnt = 0; nCnt < 3; nCnt++) B[nCnt] = 0.0f;

            for (int nCnt = 0; nCnt < lCoords.Count; nCnt++)
            {
                A[0] += 1.0;
                A[1] += (double)lCoords[nCnt].X;
                A[2] += (double)lCoords[nCnt].Y;
                A[4] += (double)lCoords[nCnt].X * (double)lCoords[nCnt].X;
                A[5] += (double)lCoords[nCnt].X * (double)lCoords[nCnt].Y;
                A[8] += (double)lCoords[nCnt].Y * (double)lCoords[nCnt].Y;
                B[0] += (Z = ((double)lCoords[nCnt].Y * (double)lCoords[nCnt].Y +
                                (double)lCoords[nCnt].X * (double)lCoords[nCnt].X));
                B[1] += (double)lCoords[nCnt].X * Z;
                B[2] += (double)lCoords[nCnt].Y * Z;
            }

            A[3] = A[1]; A[6] = A[2]; A[7] = A[5];

            bool bSuccess = false;
            if (doGaussEliminate(ref A, ref B, 3))
            {
                dXCentre = objGeo.dXScale * (B[1] / 2.0f);
                dYCentre = objGeo.dYScale * (B[2] / 2.0f);
                dRadius = Math.Sqrt(B[0] + dXCentre * dXCentre + dYCentre * dYCentre);

                bSuccess = true;
            }

            return bSuccess;
        }

        private bool doGaussEliminate(ref double[] dA, ref double[] dB, int nCount)
        {
            double[,] dC = new double[11, 11];

            if (nCount > 11) return false;

            for (int nRow = 0; nRow < nCount; nRow++)
            {
                for (int nCol = 0; nCol < nCount; nCol++)
                {
                    dC[nRow, nCol] = dA[(nCount * nRow) + nCol];
                }
            }

            double dFact = 0, dConstant = 0;
            for (int nRow = 0; nRow < (nCount - 1); nRow++)
            {
                dFact = dC[nRow, nRow];
                if (Math.Abs(dFact) < CDefinitions.SMALLNO)
                {
                    return false;
                }
                for (int nCol = nRow; nCol < nCount; nCol++)
                {
                    dC[nRow, nCol] /= dFact;
                }
                dB[nRow] /= dFact;

                for (int nCol = (nRow + 1); nCol < nCount; nCol++)
                {
                    dConstant = dC[nCol, nRow];
                    for (int nCnt = nRow; nCnt < nCount; nCnt++)
                    {
                        dC[nCol, nCnt] -= (dConstant * dC[nRow, nCnt]);
                    }
                    dB[nCol] -= (dConstant * dB[nRow]);
                }
            }

            if (Math.Abs(dC[nCount - 1, nCount - 1]) < CDefinitions.SMALLNO) return false;

            dB[nCount - 1] /= dC[nCount - 1, nCount - 1];

            double dSum = 0.0;
            for (int nRow = (nCount - 2); nRow >= 0; nRow--)
            {
                dSum = 0.0;
                for (int nCol = nRow + 1; nCol < nCount; nCol++)
                {
                    dSum += dC[nRow, nCol] * dB[nCol];
                }
                dB[nRow] -= dSum;
            }

            return true;
        }

        #endregion
    }
}
