using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Apache.ImageLib
{
    public class CMorphology : CImageInfo
    {
        #region CTOR

        public CMorphology(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        #region BINARY_OPS

        public Bitmap BinaryExpand()
        {
            try
            {
                if (false == isBinaryImage()) return null;

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                int nSumNineWhites = 9 * CDefinitions.IMAX;

                for (int nRow = 1; nRow < m_nHeight - 1; nRow++)
                {
                    for (int nCol = 1; nCol < m_nWidth - 1; nCol++)
                    {
                        if (Get3x3PixelSum(nCol, nRow) != nSumNineWhites)
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
                        else
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap BinaryShrink()
        {
            try
            {
                if (false == isBinaryImage()) return null;

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                // BULK OF THE IMAGE PROCESSED HERE
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        int nSumI = Get3x3PixelSum(nCol, nRow);
                        if (nSumI > CDefinitions.IMAX)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                        else
                        {
                            int I = GetAvgPixel(nCol, nRow);
                            dest.SetPixel(nCol, nRow, Color.FromArgb(I, I, I));
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

        public Bitmap ConvexHull(int nIteration, int nInt)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                bool bFinish;

                int nIter = 0;
                do
                {
                    int nSum = 0;
                    bFinish = true;
                    nIter++;
                    int a1, a2, a3, a4, a5, a6, a7, a8;

                    // BULK OF THE IMAGE PROCESSED HERE 
                    for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                    {
                        for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                        {
                            a1 = GetAvgPixel(nCol + 1, nRow) == nInt ? 1 : 0;
                            a2 = GetAvgPixel(nCol + 1, nRow - 1) == nInt ? 1 : 0;
                            a3 = GetAvgPixel(nCol, nRow - 1) == nInt ? 1 : 0;
                            a4 = GetAvgPixel(nCol - 1, nRow - 1) == nInt ? 1 : 0;
                            a5 = GetAvgPixel(nCol - 1, nRow) == nInt ? 1 : 0;
                            a6 = GetAvgPixel(nCol - 1, nRow + 1) == nInt ? 1 : 0;
                            a7 = GetAvgPixel(nCol, nRow + 1) == nInt ? 1 : 0;
                            a8 = GetAvgPixel(nCol + 1, nRow + 1) == nInt ? 1 : 0;

                            nSum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8; //+
                            //      (a1 && (!a2 && a3)) + (a3 && (!a4 && a5)) +
                            //      (a5 && (!a6 && a7)) + (a7 && (!a8 && a1));

                            int nPixel = GetAvgPixel(nCol, nRow);
                            if (nPixel != nInt && nSum > 3)
                            {
                                dest.SetPixel(nCol, nRow, Color.FromArgb(nInt, nInt, nInt));
                                bFinish = false;
                            }
                            else
                                dest.SetPixel(nCol, nRow, Color.FromArgb(nPixel, nPixel, nPixel));
                        }
                    }

                    //// EDGE ROWS PROCESSED HERE 
                    //int nOffset = m_nWidth * (m_nHeight - 1);
                    //for (int nCol = 0; nCol < m_nWidth; nCol++)
                    //{
                    //    if (nCol != 0)
                    //    {
                    //        a4 = GetAvgPixel(nCol - 1] == nInt;
                    //        a5 = GetAvgPixel(nCol - 1] == nInt;
                    //        a6 = GetAvgPixel(m_nWidth + nCol - 1] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a4 = GetAvgPixel(nCol] == nInt;
                    //        a5 = GetAvgPixel(nCol] == nInt;
                    //        a6 = GetAvgPixel(m_nWidth + nCol] == nInt;
                    //    }

                    //    a3 = GetAvgPixel(nCol] == nInt;
                    //    a7 = GetAvgPixel(m_nWidth + nCol] == nInt;
                    //    if (nCol != (m_nWidth - 1))
                    //    {
                    //        a1 = GetAvgPixel(nCol + 1] == nInt;
                    //        a2 = GetAvgPixel(nCol + 1] == nInt;
                    //        a8 = GetAvgPixel(m_nWidth + nCol + 1] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a1 = GetAvgPixel(nCol] == nInt;
                    //        a2 = GetAvgPixel(nCol] == nInt;
                    //        a8 = GetAvgPixel(m_nWidth + nCol] == nInt;
                    //    }

                    //    nSum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 +
                    //         (a1 && (!a2 && a3)) + (a3 && (!a4 && a5)) +
                    //         (a5 && (!a6 && a7)) + (a7 && (!a8 && a1));

                    //    if (GetAvgPixel(nCol] != nInt && (nSum > 3))
                    //    {
                    //        imgInfo.SetRaster()[nCol] = RGB(nInt, nInt, nInt);
                    //        bFinish = false;
                    //    }
                    //    else
                    //        imgInfo.SetRaster()[nCol] = imgInfo.GetRaster()[nCol];

                    //    if (nCol != 0)
                    //    {
                    //        a4 = GetAvgPixel(nOffset - m_nWidth + nCol - 1] == nInt;
                    //        a5 = GetAvgPixel(nOffset + nCol - 1] == nInt;
                    //        a6 = GetAvgPixel(nOffset + nCol - 1] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a4 = GetAvgPixel(nOffset - m_nWidth + nCol] == nInt;
                    //        a5 = GetAvgPixel(nOffset + nCol] == nInt;
                    //        a6 = GetAvgPixel(nOffset + nCol] == nInt;
                    //    }
                    //    a3 = GetAvgPixel(nOffset - m_nWidth + nCol] == nInt;
                    //    a7 = GetAvgPixel(nOffset + nCol] == nInt;
                    //    if (nCol != (m_nWidth - 1))
                    //    {
                    //        a1 = GetAvgPixel(nOffset + nCol + 1] == nInt;
                    //        a2 = GetAvgPixel(nOffset - m_nWidth + nCol + 1] == nInt;
                    //        a8 = GetAvgPixel(nOffset + nCol + 1] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a1 = GetAvgPixel(nOffset + nCol] == nInt;
                    //        a2 = GetAvgPixel(nOffset - m_nWidth + nCol] == nInt;
                    //        a8 = GetAvgPixel(nOffset + nCol] == nInt;
                    //    }

                    //    nSum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 +
                    //          (a1 && (!a2 && a3)) + (a3 && (!a4 && a5)) +
                    //          (a5 && (!a6 && a7)) + (a7 && (!a8 && a1));

                    //    if (GetAvgPixel(nOffset + nCol] != nInt && (nSum > 3))
                    //    {
                    //        imgInfo.SetRaster()[nOffset + nCol] = RGB(nInt, nInt, nInt);
                    //        bFinish = false;
                    //    }
                    //    else
                    //        imgInfo.SetRaster()[nOffset + nCol] = imgInfo.GetRaster()[nOffset + nCol];
                    //}

                    //// EDGE COLUMNS PROCESSED HERE
                    //nBase = nBase = 0;
                    //for (int nRow = 0; nRow < m_nHeight; nRow++)
                    //{
                    //    if (nRow != 0)
                    //    {
                    //        a2 = GetAvgPixel(m_nWidth + nBase + 1] == nInt;
                    //        a3 = GetAvgPixel(m_nWidth + nBase] == nInt;
                    //        a4 = GetAvgPixel(m_nWidth + nBase] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a2 = GetAvgPixel(nBase + 1] == nInt;
                    //        a3 = GetAvgPixel(nBase] == nInt;
                    //        a4 = GetAvgPixel(nBase] == nInt;
                    //    }
                    //    a1 = GetAvgPixel(nBase + 1] == nInt;
                    //    a5 = GetAvgPixel(nBase] == nInt;

                    //    if (nRow != (m_nHeight - 1))
                    //    {
                    //        a6 = GetAvgPixel(nBase + m_nWidth] == nInt;
                    //        a7 = GetAvgPixel(nBase + m_nWidth] == nInt;
                    //        a8 = GetAvgPixel(nBase + m_nWidth + 1] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a6 = GetAvgPixel(nBase] == nInt;
                    //        a7 = GetAvgPixel(nBase] == nInt;
                    //        a8 = GetAvgPixel(nBase + 1] == nInt;
                    //    }

                    //    nSum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 +
                    //         (a1 && (!a2 && a3)) + (a3 && (!a4 && a5)) +
                    //         (a5 && (!a6 && a7)) + (a7 && (!a8 && a1));

                    //    if (GetAvgPixel(nBase] != nInt && nSum > 3)
                    //    {
                    //        imgInfo.SetRaster()[nBase] = RGB(nInt, nInt, nInt);
                    //        bFinish = false;
                    //    }
                    //    else
                    //        imgInfo.SetRaster()[nBase] = imgInfo.GetRaster()[nBase];

                    //    if (nRow != 0)
                    //    {
                    //        a2 = GetAvgPixel(nBase - 1] == nInt;
                    //        a3 = GetAvgPixel(nBase - 1] == nInt;
                    //        a4 = GetAvgPixel(nBase - 2] == nInt;
                    //    }
                    //    else
                    //    {
                    //        a2 = GetAvgPixel(nBase + m_nWidth - 1] == nInt;
                    //        a3 = GetAvgPixel(nBase + m_nWidth - 1] == nInt;
                    //        a4 = GetAvgPixel(nBase - 2] == nInt;
                    //    }
                    //    a1 = GetAvgPixel(nBase + m_nWidth - 1] == nInt;
                    //    a5 = GetAvgPixel(nBase - 2]) == nInt;

                    //    if (nRow != (m_nHeight - 1))
                    //    {
                    //        a6 = GetAvgPixel(nBase + 2 * m_nWidth - 2]) == nInt;
                    //        a7 = GetAvgPixel(nBase + 2 * m_nWidth - 1]) == nInt;
                    //        a8 = GetAvgPixel(nBase + 2 * m_nWidth - 1]) == nInt;
                    //    }
                    //    else
                    //    {
                    //        a6 = GetAvgPixel(nBase + m_nWidth - 2]) == nInt;
                    //        a7 = GetAvgPixel(nBase + m_nWidth - 1]) == nInt;
                    //        a8 = GetAvgPixel(nBase + m_nWidth - 1]) == nInt;
                    //    }

                    //    nSum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 +
                    //         (a1 && (!a2 && a3)) + (a3 && (!a4 && a5)) +
                    //         (a5 && (!a6 && a7)) + (a7 && (!a8 && a1));

                    //    if (GetAvgPixel(m_nWidth - 1]) != nInt && nSum > 3)
                    //    {
                    //        imgInfo.SetRaster()[nBase + m_nWidth - 1] = RGB(nInt, nInt, nInt);
                    //        bFinish = false;
                    //    }
                    //    else
                    //        imgInfo.SetRaster()[nBase + m_nWidth - 1] = imgInfo.GetRaster()[nBase + m_nWidth - 1];
                    //    nBase += m_nWidth;
                    //}
                } while ((!bFinish) && (nIter < nIteration)); //nIteration = nIter;

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Dilate()
        {
            try
            {
                int[] arrStructSet = new int[15];
                arrStructSet[0] = 1; arrStructSet[1] = -1; arrStructSet[2] = 0;
                arrStructSet[3] = 2; arrStructSet[4] = 0; arrStructSet[5] = -1;
                arrStructSet[6] = 3; arrStructSet[7] = 0; arrStructSet[8] = 0;
                arrStructSet[9] = 4; arrStructSet[10] = 0; arrStructSet[11] = 1;
                arrStructSet[12] = 5; arrStructSet[13] = 1; arrStructSet[14] = 0;

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nMaxInt = -1;
                        for (int nCnt = 0; nCnt < arrStructSet.Length / 2; nCnt++)
                        {
                            int cntRow = nRow + arrStructSet[nCnt + 1];
                            if (cntRow < 0) cntRow = 0;
                            else if (cntRow > (m_nHeight - 1)) cntRow = (m_nHeight - 1);

                            int cntCol = nCol + arrStructSet[nCnt];
                            if (cntCol < 0) cntCol = 0;
                            else if (cntCol > (m_nWidth - 1)) cntCol = (m_nWidth - 1);

                            int nSample = 0;
                            if (nMaxInt < (nSample = GetAvgPixel(cntCol, cntRow)))
                                nMaxInt = nSample;
                        }//END vStructSet size

                        dest.SetPixel(nCol, nRow, Color.FromArgb(nMaxInt, nMaxInt, nMaxInt));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Erode()
        {
            try
            {
                int[] arrStructSet = new int[10];
                arrStructSet[0] = 1; arrStructSet[1] = -1; arrStructSet[2] = -1;
                arrStructSet[3] = 0; arrStructSet[4] = 0; arrStructSet[5] = 0;
                arrStructSet[6] = 1; arrStructSet[7] = 0; arrStructSet[8] = 0;
                arrStructSet[9] = 1;

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nMinInt = CDefinitions.IMAX;
                        for (int nCnt = 0; nCnt < arrStructSet.Length / 2; nCnt++)
                        {
                            int cntRow = nRow + arrStructSet[nCnt + 1];
                            if (cntRow < 0) cntRow = 0;
                            else if (cntRow > m_nHeight - 1) cntRow = (m_nHeight - 1);

                            int cntCol = nCol + arrStructSet[nCnt];
                            if (cntCol < 0) cntCol = 0;
                            else if (cntCol > m_nWidth - 1) cntCol = (m_nWidth - 1);

                            int sample = 0;
                            if (nMinInt > (sample = GetAvgPixel(cntCol, cntRow)))
                                nMinInt = sample;
                        }
                        dest.SetPixel(nCol, nRow, Color.FromArgb(nMinInt, nMinInt, nMinInt));
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

        #region FILTERS

        public Bitmap FormFilter(Point ptLow, Point ptHigh, int nBandType)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nI = 0;
                int[] lkpTable = new int[CDefinitions.SCALE];
                for (nI = 0; nI < CDefinitions.SCALE; nI++)
                {
                    lkpTable[nI] = nI;
                }

                Point pt1 = new Point(), pt2 = new Point();
                int diffCol, diffRow;
                switch (nBandType)
                {
                    case 0:
                        for (nI = 1; nI < CDefinitions.SCALE - 1; nI++)
                        {
                            getRectBound(nI, ref pt1, ref pt2);

                            if (pt1.X + pt1.Y + pt2.X + pt2.Y != 0)
                            {
                                diffCol = pt2.X - pt1.X + 1;
                                diffRow = pt2.Y - pt1.Y + 1;
                                if ((diffCol < ptLow.X) || (diffCol > ptHigh.X) ||
                                    (diffRow < ptLow.Y) || (diffRow > ptHigh.Y))
                                    lkpTable[nI] = 0;
                            }
                        }
                        break;
                    case 1:
                        for (byte nInt = 1; nInt < CDefinitions.SCALE - 1; nInt++)
                        {
                            getRectBound(nInt, ref pt1, ref pt2);

                            if (pt1.X + pt1.Y + pt2.X + pt2.Y != 0)
                            {
                                diffCol = pt2.X - pt1.X + 1;
                                diffRow = pt2.Y - pt1.Y + 1;
                                if ((diffCol < ptLow.X) || (diffCol > ptHigh.X) ||
                                    (diffRow < ptLow.Y) || (diffRow > ptHigh.Y))
                                { }
                                else
                                    lkpTable[nI] = 0;
                            }
                        }
                        break;
                    default:
                        break;
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int pixel = lkpTable[GetAvgPixel(nCol, nRow)];
                        dest.SetPixel(nCol, nRow, Color.FromArgb(pixel, pixel, pixel));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getRectBound(int nInt, ref Point ptMin, ref Point ptMax)
        {
            try
            {
                ptMin.X = m_nWidth; ptMin.Y = m_nHeight; ptMax.X = 0; ptMax.Y = 0;

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (GetAvgPixel(nCol, nRow) == nInt)
                        {
                            if (nCol < ptMin.X) ptMin.X = nCol;
                            if (nCol > ptMax.X) ptMax.X = nCol;
                            if (nRow < ptMin.Y) ptMin.Y = nRow;
                            if (nRow > ptMax.Y) ptMax.Y = nRow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap LocalDistance(CDefinitions.REGION_TYPE eRegionType)
        {
            try
            {
                byte[,] tmpRaster = new byte[m_nHeight, m_nWidth];
                for (int nRow = 1; nRow < m_nHeight - 1; nRow++)
                {
                    for (int nCol = 1; nCol < m_nWidth - 1; nCol++)
                    {
                        if (CDefinitions.REGION_TYPE.eWhite == eRegionType) // WHITE
                            tmpRaster[nRow, nCol] = (byte)(CDefinitions.IMAX - GetAvgPixel(nCol, nRow));
                        else
                            tmpRaster[nRow, nCol] = GetAvgPixel(nCol, nRow);
                    }
                }

                // Forward Scan
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        byte nVal = (byte)(CDefinitions.MIN5(tmpRaster[nRow - 1, nCol - 1], tmpRaster[nRow - 1, nCol],
                            tmpRaster[nRow - 1, nCol + 1], tmpRaster[nRow, nCol - 1], tmpRaster[nRow, nCol] - 1) + 1);
                        if (nVal > CDefinitions.IMAX) nVal = CDefinitions.IMAX;
                        tmpRaster[nRow, nCol] = nVal;
                    }
                }

                // Reverse Scan
                for (int nRow = 1; nRow < (m_nHeight - 1); nRow++)
                {
                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        byte nVal = (byte)(CDefinitions.MIN5(tmpRaster[nRow, nCol] - 1, tmpRaster[nRow, nCol + 1],
                                        tmpRaster[nRow + 1, nCol - 1], tmpRaster[nRow + 1, nCol], tmpRaster[nRow + 1, nCol + 1]) + 1);
                        if (nVal > CDefinitions.IMAX) nVal = CDefinitions.IMAX;
                        tmpRaster[nRow, nCol] = nVal;
                    }
                }

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(tmpRaster[nRow, nCol], tmpRaster[nRow, nCol], tmpRaster[nRow, nCol]));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap PerimeterFilter(Point ptPerimeter, double dXScale, double dYScale, int nBand)
        {
            try
            {
                int nI = 0;
                int[] lkpTable = new int[CDefinitions.SCALE];
                for (nI = 0; nI < CDefinitions.SCALE; nI++)
                {
                    lkpTable[nI] = nI;
                }

                List<Point> ptCoord = new List<Point>();

                CRegions obj = new CRegions(SrcImage);
                for (int nInt = 1; nInt < CDefinitions.SCALE - 1; nInt++)
                {
                    if (true == obj.GetOutlinedObjects(SrcImage, nInt, ref ptCoord))
                    {
                        CBlobAnalysis.OBJECT2DGEOMETRY obj2D = new CBlobAnalysis.OBJECT2DGEOMETRY();
                        if (true == obj.GetObject2DFeatures(ref ptCoord, dXScale, dYScale, ref obj2D))
                        {
                            switch (nBand)
                            {
                                case 0:
                                    if ((obj2D.dPerimeter < ptPerimeter.X) || (obj2D.dPerimeter > ptPerimeter.Y))
                                        lkpTable[nInt] = 0;
                                    break;
                                case 1:
                                    if ((obj2D.dPerimeter > ptPerimeter.X) || (obj2D.dPerimeter > ptPerimeter.Y))
                                        lkpTable[nInt] = 0;
                                    break;
                            }// END SWITCH
                        }
                    }
                }//END FOR

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int pixel = lkpTable[GetAvgPixel(nCol, nRow)];
                        dest.SetPixel(nCol, nRow, Color.FromArgb(pixel, pixel, pixel));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap ShapeFilter(double dShapeLow, double dShapeHigh, double dXScale, double dYScale, int nBandType)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                int nI = 0;
                int[] lkpTable = new int[CDefinitions.SCALE];
                for (nI = 0; nI < CDefinitions.SCALE; nI++)
                {
                    lkpTable[nI] = nI;
                }

                List<Point> ptCoord = new List<Point>();

                CRegions obj = new CRegions(SrcImage);
                for (int nInt = 1; nInt < CDefinitions.SCALE - 1; nInt++)
                {
                    if (true == obj.GetOutlinedObjects(SrcImage, nInt, ref ptCoord))
                    {
                        CBlobAnalysis.OBJECT2DGEOMETRY obj2D = new CBlobAnalysis.OBJECT2DGEOMETRY();
                        if (true == obj.GetObject2DFeatures(ref ptCoord, dXScale, dYScale, ref obj2D))
                        {
                            switch (nBandType)
                            {
                                case 0:
                                    if ((obj2D.dShapeFactor < dShapeLow) || (obj2D.dShapeFactor > dShapeHigh))
                                        lkpTable[nInt] = 0;
                                    break;
                                case 1:
                                    if ((obj2D.dShapeFactor > dShapeLow) || (obj2D.dShapeFactor < dShapeHigh))
                                        lkpTable[nInt] = 0;
                                    break;
                            }//END SWITCH
                        }
                    }
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int pixel = lkpTable[GetAvgPixel(nCol, nRow)];
                        dest.SetPixel(nCol, nRow, Color.FromArgb(pixel, pixel, pixel));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap SizeFilter(int m_nLow, int m_nHigh, int nBandType)
        {
            try
            {
                int[] lkpHistogram = new int[CDefinitions.SCALE];
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        lkpHistogram[GetAvgPixel(nCol, nRow)]++;
                    }
                }

                int nI = 0;
                int[] lkpTable = new int[CDefinitions.SCALE];
                for (nI = 0; nI < CDefinitions.SCALE; nI++)
                {
                    lkpTable[nI] = nI;
                }

                for (nI = 1; nI < CDefinitions.SCALE - 1; nI++)
                {
                    switch (nBandType)
                    {
                        case 0:
                            if ((lkpHistogram[nI] < m_nLow) || (lkpHistogram[nI] > m_nHigh))
                                lkpTable[nI] = 0;
                            break;
                        case 1:
                            if ((lkpHistogram[nI] > m_nLow) && (lkpHistogram[nI] < m_nHigh))
                                lkpTable[nI] = 0;
                            break;
                    }
                }

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int pixel = lkpTable[GetAvgPixel(nCol, nRow)];
                        dest.SetPixel(nCol, nRow, Color.FromArgb(pixel, pixel, pixel));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap ThinFilter(CDefinitions.REGION_TYPE eRegionType)
        {
            try
            {
                Bitmap dest = null;
                byte[,] vTempRaster = new byte[m_nHeight, m_nWidth];
                if (false == isBinaryImage())
                {
                    CFeatures obj = new CFeatures(SrcImage);
                    obj.GlobalThreshold(128, 0, ref vTempRaster);
                }
                else
                {
                    for (int nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        for (int nCol = 0; nCol < m_nWidth; nCol++)
                        {
                            vTempRaster[nRow, nCol] = GetAvgPixel(nCol, nRow);
                        }
                    }
                }

                CBlobAnalysis obj2 = new CBlobAnalysis(SrcImage);
                dest = obj2.GetThinImage(ref vTempRaster, eRegionType);
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
