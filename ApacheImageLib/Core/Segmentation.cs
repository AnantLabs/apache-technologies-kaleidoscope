using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Apache.ImageLib
{
    public class CSegmentation : CImageInfo
    {
        #region DATA

        class KMEANS_SEGMENTS
        {
            public int nDimension;
            public int nGroupIndex;
            public int nLabelIndex;
            public double dConfidence;
            public double[] arrCluster;
        };

        class KMEANS_SEGMENTS_GROUPS
        {
            public int nDimension;
            public int nGroupIndex;
            public int nLabelIndex;
            public int nSize;
            public double dStdDev;
            public double[] arrGrpCluster;
        };

        #endregion

        #region CTOR

        public CSegmentation(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        #region KMEAN

        public Bitmap KMeans(int nSegments, int nVectorType)
        {
            KMEANS_SEGMENTS[,] arrClusters = new KMEANS_SEGMENTS[m_nHeight, m_nWidth];

            // Call function to compute features
            int nClusterSize = computeClusters(ref arrClusters, nVectorType);

            List<int> listGroupArray = new List<int>();
            for (int nGroup = 0; nGroup < nSegments; nGroup++)
            {
                listGroupArray.Add(m_nWidth * nGroup / nSegments);
            }

            int nGroupSize = nSegments;
            KMEANS_SEGMENTS_GROUPS[] arrClusterGroup = new KMEANS_SEGMENTS_GROUPS[nGroupSize];
            for (int nGroup = 0; nGroup < nGroupSize; nGroup++)
            {
                arrClusterGroup[nGroup] = new KMEANS_SEGMENTS_GROUPS();
                arrClusterGroup[nGroup].nDimension = arrClusters[0, 0].nDimension;
                arrClusterGroup[nGroup].nGroupIndex = nGroup;
                arrClusterGroup[nGroup].arrGrpCluster = new double[arrClusterGroup[nGroup].nDimension];
                Array.Copy(arrClusters[listGroupArray[nGroup] / m_nHeight, listGroupArray[nGroup] % m_nHeight].arrCluster,
                    arrClusterGroup[nGroup].arrGrpCluster, arrClusters[0, 0].nDimension);
            }

            // Call the function to assign arrClusterGroup
            assignGroup(ref arrClusters, nClusterSize, ref arrClusterGroup, nGroupSize, m_nWidth, m_nHeight);

            // Call the function to accumulate the group
            double dPrevMeanSquare = getGroupMSE(ref arrClusters, nClusterSize, ref arrClusterGroup, nGroupSize, m_nWidth, m_nHeight);
            double dCurrMeanSquare, dMeanSquareError;
            int nFlag = 0;
            do
            {
                // Call the function to group the vectors
                getNewGroupCenters(ref arrClusters, nClusterSize, ref arrClusterGroup, nGroupSize, m_nWidth, m_nHeight);

                // Call the function to get the current Mean Square
                dCurrMeanSquare = getGroupMSE(ref arrClusters, nClusterSize, ref arrClusterGroup, nGroupSize, m_nWidth, m_nHeight);
                dMeanSquareError = ((dCurrMeanSquare - dCurrMeanSquare) * 100) / dCurrMeanSquare;

                if (Math.Abs(dMeanSquareError) < 0.5f) nFlag = 1;
                if (nFlag == 0)
                {
                    assignGroup(ref arrClusters, nClusterSize, ref arrClusterGroup, nGroupSize, m_nWidth, m_nHeight);
                }
            } while (nFlag == 0);	//loop for MeanSquareError Greater than 2%

            // Call the function to convert the Image
            return convertImage(ref arrClusters);
        }

        private int computeClusters(ref KMEANS_SEGMENTS[,] arrClusters, int nVectorType)
        {
            int nRow = 0, nCol = 0;
            try
            {
                int nDimension = 0;
                int nClustersSize = 0;
                switch (nVectorType)
                {
                    case 0:
                        nDimension = 9;
                        for (nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                arrClusters[nRow, nCol] = new KMEANS_SEGMENTS();
                                arrClusters[nRow, nCol].arrCluster = new double[nDimension];
                                if (nCol == 0)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol, nRow - 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 1, nRow - 1) & 0xF0;
                                    }
                                    arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow) & 0xF0;
                                    arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow) & 0xF0;
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol, nRow + 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol + 1, nRow + 1) & 0xF0;
                                    }
                                }
                                else if (nCol == m_nWidth - 1)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 1, nRow - 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol, nRow - 1) & 0xF0;
                                    }
                                    arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol - 1, nRow) & 0xF0;
                                    arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow) & 0xF0;
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol - 1, nRow + 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol, nRow + 1) & 0xF0;
                                    }
                                }
                                else
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 1, nRow - 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol, nRow - 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 1, nRow - 1) & 0xF0;
                                    }
                                    arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol - 1, nRow) & 0xF0;
                                    arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow) & 0xF0;
                                    arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow) & 0xF0;
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol - 1, nRow + 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol, nRow + 1) & 0xF0;
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol + 1, nRow + 1) & 0xF0;
                                    }
                                }

                                arrClusters[nRow, nCol].nDimension = nDimension;
                                arrClusters[nRow, nCol].nGroupIndex = 0;
                                nClustersSize++;
                            }
                        }
                        break;
                    case 1:
                        nDimension = 2;	//Mean-nRange Feature
                        int[] vMatrix = new int[9];

                        for (nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                arrClusters[nRow, nCol] = new KMEANS_SEGMENTS();
                                arrClusters[nRow, nCol].arrCluster = new double[nDimension];
                                if (nRow == 0)
                                {
                                    vMatrix[0] = 0; vMatrix[1] = 0; vMatrix[2] = 0;
                                }
                                else if (nRow == m_nHeight - 1)
                                {
                                    vMatrix[6] = 0; vMatrix[7] = 0; vMatrix[8] = 0;
                                }
                                else
                                {
                                    if (nCol == 0)
                                    {
                                        vMatrix[0] = 0;
                                        vMatrix[6] = 0;
                                    }
                                    else
                                    {
                                        vMatrix[0] = GetAvgPixel(nCol - 1, nRow - 1);
                                        vMatrix[6] = GetAvgPixel(nCol - 1, nRow + 1);
                                    }

                                    vMatrix[7] = GetAvgPixel(nCol, nRow + 1);
                                    vMatrix[1] = GetAvgPixel(nCol, nRow - 1);

                                    if (nCol == m_nWidth - 1)
                                    {
                                        vMatrix[2] = 0;
                                        vMatrix[8] = 0;
                                    }
                                    else
                                    {
                                        vMatrix[2] = GetAvgPixel(nCol + 1, nRow - 1);
                                        vMatrix[8] = GetAvgPixel(nCol + 1, nRow + 1);
                                    }
                                }

                                if (nCol == 0) vMatrix[3] = 0;
                                else vMatrix[3] = GetAvgPixel(nCol - 1, nRow);

                                vMatrix[4] = GetAvgPixel(nCol, nRow);

                                if (nCol == m_nWidth - 1) vMatrix[5] = 0;
                                else vMatrix[5] = GetAvgPixel(nCol + 1, nRow);

                                Array.Sort(vMatrix);
                                arrClusters[nRow, nCol].arrCluster[0] = (double)vMatrix[5];
                                arrClusters[nRow, nCol].arrCluster[1] = (double)(vMatrix[8] - vMatrix[0]);
                                arrClusters[nRow, nCol].nDimension = nDimension;
                                arrClusters[nRow, nCol].nGroupIndex = 0;
                                nClustersSize++;
                            }
                        }
                        break;
                    case 2:
                        nDimension = 3;
                        for (nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                arrClusters[nRow, nCol] = new KMEANS_SEGMENTS();
                                arrClusters[nRow, nCol].arrCluster = new double[nDimension];

                                if (nCol == 0)
                                    arrClusters[nRow, nCol].arrCluster[0] = 0;
                                else
                                    arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 1, nRow);

                                arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol, nRow);

                                if (nCol == m_nWidth - 1)
                                    arrClusters[nRow, nCol].arrCluster[2] = 0;
                                else
                                    arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 1, nRow);

                                arrClusters[nRow, nCol].nDimension = nDimension;
                                arrClusters[nRow, nCol].nGroupIndex = 0;
                                nClustersSize++;
                            }
                        }
                        break;
                    case 3:
                        nDimension = 4;
                        for (nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                arrClusters[nRow, nCol] = new KMEANS_SEGMENTS();
                                arrClusters[nRow, nCol].arrCluster = new double[nDimension];

                                arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow) & 0xF0;

                                if (nCol == m_nWidth - 1)
                                    arrClusters[nRow, nCol].arrCluster[1] = 0;
                                else
                                    arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow) & 0xF0;

                                if (nRow < m_nHeight - 1 && nCol < m_nWidth - 1)
                                    arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 1, nRow + 1) & 0xF0;
                                else
                                    arrClusters[nRow, nCol].arrCluster[2] = 0;

                                if (nRow == m_nHeight - 1)
                                    arrClusters[nRow, nCol].arrCluster[3] = 0;
                                else
                                    arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow + 1) & 0xF0;

                                arrClusters[nRow, nCol].nDimension = nDimension;
                                arrClusters[nRow, nCol].nGroupIndex = 0;
                                nClustersSize++;
                            }
                        }
                        break;
                    case 4:
                        nDimension = 21;
                        for (nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                arrClusters[nRow, nCol] = new KMEANS_SEGMENTS();
                                arrClusters[nRow, nCol].arrCluster = new double[nDimension];

                                if (nCol == 0)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 3, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol + 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 3, nRow);
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[19] = GetAvgPixel(nCol + 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[20] = GetAvgPixel(nCol + 3, nRow + 1);
                                    }
                                }
                                else if (nCol == 1)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 3, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol + 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 3, nRow);

                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[19] = GetAvgPixel(nCol + 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[20] = GetAvgPixel(nCol + 3, nRow + 1);
                                    }
                                }
                                else if (nCol == 2)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol - 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 3, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol - 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol + 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 3, nRow);
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol - 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[19] = GetAvgPixel(nCol + 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[20] = GetAvgPixel(nCol + 3, nRow + 1);
                                    }
                                }
                                else if (nCol == 3)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 3, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol - 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 3, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol - 3, nRow);
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol - 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol + 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 3, nRow);
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol - 3, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol - 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[19] = GetAvgPixel(nCol + 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[20] = GetAvgPixel(nCol + 3, nRow + 1);
                                    }
                                }
                                else if (nCol == m_nWidth - 3)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 3, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol - 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 2, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol - 3, nRow);
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol - 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol + 2, nRow);

                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol - 3, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol - 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[19] = GetAvgPixel(nCol + 2, nRow + 1);
                                    }
                                }
                                else if (nCol == m_nWidth - 2)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 3, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol - 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol - 3, nRow);
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol - 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);

                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol - 3, nRow);
                                        arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol - 2, nRow);
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow);
                                    }
                                }
                                else if (nCol == m_nWidth - 1)
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 3, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol - 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol - 3, nRow);
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol - 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);

                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol - 3, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol - 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                    }
                                }
                                else
                                {
                                    if (nRow != 0)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol - 3, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol - 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol - 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol + 1, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 2, nRow - 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 3, nRow - 1);
                                    }
                                    arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol - 3, nRow);
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol - 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol - 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol + 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 3, nRow);
                                    if (nRow != m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol - 3, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol - 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[16] = GetAvgPixel(nCol - 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[17] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[18] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[19] = GetAvgPixel(nCol + 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[20] = GetAvgPixel(nCol + 3, nRow + 1);
                                    }
                                }
                                arrClusters[nRow, nCol].nDimension = nDimension;
                                arrClusters[nRow, nCol].nGroupIndex = 0;
                                nClustersSize++;
                            }
                        }
                        break;
                    case 5:
                        nDimension = 16; //4 nX 4 matrix 0,0 is the center point
                        for (nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                arrClusters[nRow, nCol] = new KMEANS_SEGMENTS();
                                arrClusters[nRow, nCol].arrCluster = new double[nDimension];

                                if (nCol == m_nWidth - 1)
                                {
                                    if (nRow == m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                    }
                                    else if (nRow == m_nHeight - 2)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                    }
                                    else if (nRow == m_nHeight - 3)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                    }
                                    else if (nRow < m_nHeight - 4)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol, nRow + 3);
                                    }
                                }
                                else if (nCol == m_nWidth - 2)
                                {
                                    if (nRow == m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                    }
                                    if (nRow == m_nHeight - 2)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);
                                    }
                                    else if (nRow == m_nHeight - 3)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);

                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol + 1, nRow + 2);
                                    }
                                    else if (nRow < m_nHeight - 4)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol, nRow + 3);
                                        arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 1, nRow + 3);
                                    }
                                }
                                else if (nCol == m_nWidth - 3)
                                {
                                    if (nRow == m_nHeight - 1)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 2, nRow);
                                    }
                                    if (nRow == m_nHeight - 2)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 2, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 2, nRow + 1);
                                    }
                                    else if (nRow == m_nHeight - 3)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 2, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 2, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol + 1, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol + 2, nRow + 2);
                                    }
                                    else if (nRow < m_nHeight - 4)
                                    {
                                        arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                        arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 1, nRow);
                                        arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 1, nRow + 1);
                                        arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol + 1, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol + 2, nRow + 2);
                                        arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol, nRow + 3);
                                        arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 1, nRow + 3);
                                        arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol + 2, nRow + 3);
                                    }
                                }
                                else if (nRow < m_nHeight - 3)
                                {
                                    arrClusters[nRow, nCol].arrCluster[0] = GetAvgPixel(nCol, nRow);
                                    arrClusters[nRow, nCol].arrCluster[1] = GetAvgPixel(nCol + 1, nRow);
                                    arrClusters[nRow, nCol].arrCluster[2] = GetAvgPixel(nCol + 2, nRow);
                                    arrClusters[nRow, nCol].arrCluster[3] = GetAvgPixel(nCol + 3, nRow);
                                    arrClusters[nRow, nCol].arrCluster[4] = GetAvgPixel(nCol, nRow + 1);
                                    arrClusters[nRow, nCol].arrCluster[5] = GetAvgPixel(nCol + 1, nRow + 1);
                                    arrClusters[nRow, nCol].arrCluster[6] = GetAvgPixel(nCol + 2, nRow + 1);
                                    arrClusters[nRow, nCol].arrCluster[7] = GetAvgPixel(nCol + 3, nRow + 1);
                                    arrClusters[nRow, nCol].arrCluster[8] = GetAvgPixel(nCol, nRow + 2);
                                    arrClusters[nRow, nCol].arrCluster[9] = GetAvgPixel(nCol + 1, nRow + 2);
                                    arrClusters[nRow, nCol].arrCluster[10] = GetAvgPixel(nCol + 2, nRow + 2);
                                    arrClusters[nRow, nCol].arrCluster[11] = GetAvgPixel(nCol + 3, nRow + 2);
                                    arrClusters[nRow, nCol].arrCluster[12] = GetAvgPixel(nCol, nRow + 3);
                                    arrClusters[nRow, nCol].arrCluster[13] = GetAvgPixel(nCol + 1, nRow + 3);
                                    arrClusters[nRow, nCol].arrCluster[14] = GetAvgPixel(nCol + 2, nRow + 3);
                                    arrClusters[nRow, nCol].arrCluster[15] = GetAvgPixel(nCol + 3, nRow + 3);
                                }
                                arrClusters[nRow, nCol].nDimension = nDimension;
                                arrClusters[nRow, nCol].nGroupIndex = 0;
                                nClustersSize++;
                            }
                        }
                        break;
                }//END SWITCH

                return nClustersSize;
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + ". Row: " + nRow + " Col: " + nCol);
                throw newEx;
            }
        }

        private void assignGroup(ref KMEANS_SEGMENTS[,] arrClusters, int nClusterSize,
            ref KMEANS_SEGMENTS_GROUPS[] arrClusterGroup, int nGroupSize, int nWidth, int nHeight)
        {
            try
            {
                double dDist;		//variable for distances calculation
                int minIndex;		//Index of nearest group center for group assigning 
                int minFlag;		//flag for testing if current distance is being 
                //less than prevoius or not
                double dTemp, fSum;	//temporary variable for calculations

                //iteration occures for total no of vectors in the arrClusters
                //for assigning group to each List
                for (int nRow = 0; nRow < nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        dDist = 0.0f; fSum = 0.0f;
                        //iteration for total no of dimensions of List
                        //for calculation of the distance between two vectors
                        for (int nCnt = 0; nCnt < arrClusters[nRow, nCol].nDimension; nCnt++)
                        {
                            dTemp = (double)(arrClusterGroup[0].arrGrpCluster[nCnt] - arrClusters[nRow, nCol].arrCluster[nCnt]);
                            fSum += dTemp * dTemp;
                        }
                        dDist = fSum; minIndex = 0;
                        //iteration for total no of group centers in the arrClusterGroup
                        //for checking distances between one List and nGroupSize of groupcenters
                        for (int nGroup = 1; nGroup < nGroupSize; nGroup++)
                        {
                            fSum = 0.0f; minFlag = 1;
                            //iteration for total no of dimensions of a List
                            //for calculation of the distance between two vectors
                            for (int nCnt = 0; nCnt < arrClusters[nRow, nCol].nDimension; nCnt++)
                            {
                                dTemp = (double)(arrClusterGroup[nGroup].arrGrpCluster[nCnt] - arrClusters[nRow, nCol].arrCluster[nCnt]);
                                fSum += dTemp * dTemp;
                                if (fSum > dDist)
                                {		//if current distance is going above previous distance 
                                    minFlag = 0;		//then exit from the loop
                                    break;
                                }
                            }							//else continue the loop until all the dimensions are finished
                            //iteration for total no of dimensions ends here
                            if (minFlag == 1)
                            {			//if current distance is less than previous distance 
                                dDist = fSum;			//then take that group center as nearest center
                                minIndex = nGroup;
                            }
                        }//iteration for total no of arrClusterGroup centers ends here
                        arrClusters[nRow, nCol].nGroupIndex = minIndex;//assign the nearest group center Index to that List
                    }//iteration for total no of vectors in the arrClusters ends here
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double getGroupMSE(ref KMEANS_SEGMENTS[,] arrClusters, int nClusterSize,
                ref KMEANS_SEGMENTS_GROUPS[] arrClusterGroup, int nGroupSize, int nWidth, int nHeight)
        {
            try
            {
                double dCurrMeanSquare;	//Variable for calculation of Mean Square
                int nGroup;				//Temporary variables for iterations
                double dTemp;			//Temporary variable for Distance calculation

                dCurrMeanSquare = 0;
                for (int nRow = 0; nRow < nHeight; nRow++)
                {
                    //iteration for calculation of mean square for all vectors
                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        //set nGroup to the List's group center Index
                        nGroup = arrClusters[nRow, nCol].nGroupIndex;
                        //iteration for the no of dimentions
                        for (int nCnt = 0; nCnt < arrClusters[0, 0].nDimension; nCnt++)
                        {
                            dTemp = (double)(arrClusters[nRow, nCol].arrCluster[nCnt] -
                                arrClusterGroup[nGroup].arrGrpCluster[nCnt]);
                            dCurrMeanSquare += dTemp * dTemp;
                        }
                    }
                }

                dCurrMeanSquare = dCurrMeanSquare / nClusterSize;// calculate mean square;
                return dCurrMeanSquare;	//return mean square
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getNewGroupCenters(ref KMEANS_SEGMENTS[,] arrClusters, int nClusterSize,
                ref KMEANS_SEGMENTS_GROUPS[] arrClusterGroup, int nGroupSize, int nWidth, int nHeight)
        {
            try
            {
                double[,] vCentroid = new double[nGroupSize, arrClusters[0, 0].nDimension];
                //for(int nGroup = 0; nGroup < nGroupSize; nGroup++) {
                //    vCentroid[nGroup] = (double*) malloc(sizeof(double)*arrClusters[0,0].nDimension);
                //    memset(vCentroid[nGroup], 0x00, sizeof(double)*arrClusters[0,0].nDimension);
                //}

                double[] vStdDev = new double[nGroupSize];
                int[] vClustorCount = new int[nGroupSize];
                int[] vMaxIndexR = new int[nGroupSize];
                int[] vMaxIndexC = new int[nGroupSize];
                double[] vMaxDist = new double[nGroupSize];

                double dDist, dTemp;
                for (int nRow = 0; nRow < nHeight; nRow = nRow + 2)
                {
                    for (int nCol = 0; nCol < nWidth; nCol = nCol + 2)
                    {
                        dDist = 0.0f;
                        int nGroup = arrClusters[nRow, nCol].nGroupIndex;
                        //iteration for total no of dimension 
                        for (int nCnt = 0; nCnt < arrClusters[nRow, nCol].nDimension; nCnt++)
                        {
                            vCentroid[nGroup, nCnt] += (double)arrClusters[nRow, nCol].arrCluster[nCnt];
                            dTemp = (double)arrClusters[nRow, nCol].arrCluster[nCnt] - arrClusterGroup[nGroup].arrGrpCluster[nCnt];
                            vStdDev[nGroup] += dTemp * dTemp;
                            //if the List is the first List in this group then ...
                            if (vClustorCount[nGroup] == 0)
                            {
                                vMaxDist[nGroup] += vStdDev[nGroup];//Temp * Temp;
                                vMaxIndexR[nGroup] = nRow;//keep the List no in the farthest List Index array
                                vMaxIndexC[nGroup] = nCol;
                            }
                            else
                            { //if the List is not the first List in this group then...
                                dDist += vStdDev[nGroup]; //Temp * Temp;

                                if (dDist > vMaxDist[nGroup])
                                {//if current List dist is less than previous List
                                    //then keep this new List no in the Index array
                                    vMaxDist[nGroup] = dDist;
                                    vMaxIndexR[nGroup] = nRow;
                                    vMaxIndexC[nGroup] = nCol;
                                }
                            }
                        }//iteration for all dimensios ends
                        vClustorCount[nGroup]++;
                    }
                }

                for (int nGroup = 0; nGroup < nGroupSize; nGroup++)
                {
                    if (vClustorCount[nGroup] != 0)
                    {
                        vStdDev[nGroup] = vStdDev[nGroup] / vClustorCount[nGroup];	//calculate standard Deviation
                        vStdDev[nGroup] = Math.Pow(vStdDev[nGroup], 0.5f);
                    }
                    else
                        vStdDev[nGroup] = 0;

                    for (int nCnt = 0; nCnt < arrClusters[0, 0].nDimension; nCnt++)
                    {											//iteration for calculation of new group center
                        //from centroid for each dimension respectively
                        if (vClustorCount[nGroup] != 0)
                            vCentroid[nGroup, nCnt] = vCentroid[nGroup, nCnt] / vClustorCount[nGroup];
                        else
                            vCentroid[nGroup, nCnt] = 0;
                        arrClusterGroup[nGroup].arrGrpCluster[nCnt] = vCentroid[nGroup, nCnt];
                        arrClusterGroup[nGroup].nSize = vClustorCount[nGroup];//keep size of the group in the arrClusterGroup List structure
                    }							//iteration for dimensions ends
                    arrClusterGroup[nGroup].dStdDev = vStdDev[nGroup];//assign stdDev in the structure
                }

                //for(int nGroup = 0; nGroup < nGroupSize; nGroup++) {
                //    free(vCentroid[nGroup]);
                //}
                //free(vCentroid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int eliminateSmallGroups(ref List<KMEANS_SEGMENTS_GROUPS> arrClusterGroup, ref int nGroupSize, int nThldGroupSize)
        {
            try
            {
                int nFlag = 0;
                for (int nGroup = 0; nGroup < nGroupSize; nGroup++)
                {
                    if (arrClusterGroup[nGroup].nSize < nThldGroupSize)
                    {
                        for (int nNewGroup = nGroup; nNewGroup < nGroupSize - 1; nNewGroup++)
                        {
                            arrClusterGroup[nNewGroup].nDimension = arrClusterGroup[nNewGroup + 1].nDimension;
                            arrClusterGroup[nNewGroup].nSize = arrClusterGroup[nNewGroup + 1].nSize;
                            arrClusterGroup[nNewGroup].dStdDev = arrClusterGroup[nNewGroup + 1].dStdDev;
                            //memcpy(&arrClusterGroup[nNewGroup].arrGrpCluster[0], 
                            //    &arrClusterGroup[nNewGroup+1].arrGrpCluster[0], 
                            //    sizeof(double) * arrClusterGroup[0].nDimension);
                            nFlag = 1;
                        }
                        nGroupSize--;
                    }
                }
                return nFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Bitmap convertImage(ref KMEANS_SEGMENTS[,] arrClusters)
        {
            try
            {
                byte byMaxGroup = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (byMaxGroup < arrClusters[nRow, nCol].nGroupIndex)
                            byMaxGroup = (byte)arrClusters[nRow, nCol].nGroupIndex;
                    }
                }

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                int nI = 0;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        nI = arrClusters[nRow, nCol].nGroupIndex * CDefinitions.IMAX / byMaxGroup;
                        nI = (nI > CDefinitions.IMAX) ? CDefinitions.IMAX : ((nI < CDefinitions.IMIN) ? CDefinitions.IMIN : nI);
                        dest.SetPixel(nCol, nRow, Color.FromArgb(nI, nI, nI));
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

        #region PIXEL_CLASSSIFICATION

        public Bitmap PixelClassification()
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                byte[,] arrPixel = new byte[m_nHeight, m_nWidth];
                for (int nRow = 1; nRow < m_nHeight - 1; nRow++)
                {
                    for (int nCol = 1, nSum = 0; nCol < m_nWidth - 1; nCol++)
                    {
                        List<int> vPixel = Get3x3Pixel(nCol, nRow);
                        for (int nCnt = 0; nCnt < vPixel.Count(); nCnt++)
                        {
                            nSum += vPixel[nCnt];
                        }
                        nSum /= vPixel.Count();
                        arrPixel[nRow, nCol] = (byte)((nSum > CDefinitions.IMAX) ? CDefinitions.IMAX : (nSum < 0 ? CDefinitions.IMIN : nSum));
                    }
                }

                byte[] lkpTable = new byte[CDefinitions.SCALE];
                for (int i = 0; i < CDefinitions.SCALE; i++)
                {
                    if (i >= 0 && i < 50)
                    {
                        lkpTable[i] = CDefinitions.IMAX;
                    }
                    else if (i >= 50 && i < 100)
                    {
                        lkpTable[i] = 192;
                    }
                    else if (i >= 100 && i < 150)
                    {
                        lkpTable[i] = 128;
                    }
                    else if (i >= 150 && i < 200)
                    {
                        lkpTable[i] = 64;
                    }
                    else
                    {
                        lkpTable[i] = 10;
                    }
                }

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nI = arrPixel[nRow, nCol];
                        byte clr = (byte)(CDefinitions.IMAX - lkpTable[nI]);
                        dest.SetPixel(nCol, nRow, Color.FromArgb(clr, clr, clr));
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
