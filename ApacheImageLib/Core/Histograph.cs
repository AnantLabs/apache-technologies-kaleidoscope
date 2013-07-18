using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Apache.ImageLib
{
    public class CHistograph : CImageInfo
    {
        #region CTOR

        public CHistograph(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        public void CumulativeHistogram(ref int[] vHistogram)
        {
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    vHistogram[GetAvgPixel(nCol, nRow)]++;
                }
            }

            for (int nCnt = 1; nCnt < CDefinitions.SCALE; nCnt++)
            {
                vHistogram[nCnt] += vHistogram[nCnt - 1];
            }
        }

        public void SimpleHistogram(ref int[] vHistogram)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        vHistogram[GetAvgPixel(nCol, nRow)]++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RowScanHistogram(ref int[] vHistogram, int nRowNum)
        {
            try
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    vHistogram[nCol] = GetAvgPixel(nCol, nRowNum);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ColoumScanHistogram(ref int[] vHistogram, int nColNum)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    vHistogram[nRow] = GetAvgPixel(nColNum, nRow);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LateralXHistogram(ref int[] vHistogram)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        vHistogram[nCol] += GetAvgPixel(nCol, nRow);
                    }
                }

                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    vHistogram[nCol] /= m_nHeight;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LateralYHistogram(ref int[] vHistogram)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        vHistogram[nRow] += GetAvgPixel(nCol, nRow);
                    }
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    vHistogram[nRow] /= m_nWidth;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RadialHistogram(ref int[] vHistogram, Point pt, int nRingNum, int nRingWidth)
        {
            try
            {
                Point pt1 = new Point(pt.X - nRingNum * nRingWidth, pt.Y - nRingNum * nRingWidth);
                Point pt2 = new Point(pt.X + nRingNum * nRingWidth, pt.Y + nRingNum * nRingWidth);

                if (pt1.X < 0 || pt1.Y < 0 || pt2.X >= m_nWidth || pt2.Y >= m_nHeight || nRingWidth <= 0)
                {
                    return false;
                }

                int[] vCount = new int[nRingNum];

                for (int nRow = pt1.Y; nRow <= pt2.Y; nRow++)
                {
                    int YComp = nRow - pt.Y; YComp = YComp * YComp;
                    for (int nCol = pt1.X; nCol <= pt2.X; nCol++)
                    {
                        int XComp = nCol - pt.X;
                        int nRadius = (int)Math.Sqrt((XComp * XComp) + YComp);
                        int nRingNo = nRadius / nRingWidth;
                        if (nRingNo < nRingNum)
                        {
                            vHistogram[nRingNo] += GetAvgPixel(nCol, nRow);
                            vCount[nRingNo]++;
                        }
                    }
                }

                for (int nCnt = 0; nCnt < nRingNum; nCnt++)
                {
                    if (vCount[nCnt] != 0) vHistogram[nCnt] /= vCount[nCnt];
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
