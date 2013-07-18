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
    public class CBlobAnalysis : CRegions
    {
        #region CONSTANT

        private const int cFALSE = 0;
        private const int cTRUE = 1;

        #endregion

        #region CELM

        public class CElm
        {
            private Point m_ptBoundaryT;
            public Point BoundaryT
            {
                get { return m_ptBoundaryT; }
                set { m_ptBoundaryT = value; }
            }

            private CElm m_objNext;
            public CElm Next
            {
                get { return m_objNext; }
                set { m_objNext = value; }
            }

            public CElm()
            {
                m_ptBoundaryT = new Point();
            }

            //~CElm() 
            //{
            //    m_ptBoundaryT = null;
            //}
        };

        #endregion

        #region ELM_LINKEDLIST

        public class CElmLinkedList
        {
            public CElm pHead = null;
            public CElm pTail = null;

            ~CElmLinkedList()
            {
                pHead = null;
                pTail = null;
            }

            public void AddElmType(Point ptBT)
            {
                try
                {
                    CElm pElm = new CElm();

                    pElm.BoundaryT = ptBT; // SG
                    pElm.Next = null;

                    if (pHead == null)
                    {
                        pHead = pElm;
                        pTail = pElm;
                    }
                    else
                    {
                        pTail.Next = pElm;
                        pTail = pTail.Next;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        };

        #endregion

        #region BLOB_DATA

        public class CBlob
        {
            public int nTag;
            public int nChainLen;
            public CRect rectDir;
            public CElmLinkedList chain;

            public void Draw(int nId, ref byte[,] vRaster)
            {
#if _DEBUG_
                string str = "Count = " + nId.ToString() + "\n" +
                    "Tag = " + nTag.ToString() + "\n" +
                    "Chain Length = " + nChainLen.ToString() + "\n";
                MessageBox.Show(str);
#endif
                while (chain.pHead != null)
                {
                    vRaster[chain.pHead.BoundaryT.Y, chain.pHead.BoundaryT.X] = CDefinitions.EDGE;
                    chain.pHead = chain.pHead.Next;
                }
            }
        };

        #endregion

        #region BLOB_NODE

        public class CBlobNode
        {
            private CBlob m_objBlob = null;
            public CBlob Blob
            {
                get { return m_objBlob; }
                set { m_objBlob = value; }
            }

            public CBlobNode m_objBlobNodeNext;
            public CBlobNode Next
            {
                get { return m_objBlobNodeNext; }
                set { m_objBlobNodeNext = value; }
            }
        };

        #endregion

        #region BLOB_LINKEDLIST

        class CBlobLinkedList
        {
            #region VARIABLES

            public CBlobNode m_objFront = null;
            public CBlobNode m_objRear = null;
            private static int m_nBLobs = 0;

            #endregion

            #region CTOR

            ~CBlobLinkedList()
            {
                m_objFront = null;
                m_objRear = null;
            }

            public void AddBlobType(CBlob aBlob)
            {
                try
                {
                    CBlobNode objBlobNode = new CBlobNode();
                    objBlobNode.Blob = aBlob;
                    objBlobNode.Next = null;

                    if (m_objFront == null)
                    {
                        m_objFront = m_objRear = objBlobNode;
                    }
                    else
                    {
                        m_objRear.Next = objBlobNode;
                        m_objRear = m_objRear.Next;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Draw(ref byte[,] vRaster)
            {
                //	Store List to Raster. The Bufer will then no longer have small disconnected parts.
                while (m_objFront != null)
                {
                    m_nBLobs++;
                    m_objFront.Blob.Draw(m_nBLobs, ref vRaster);
                    m_objFront = m_objFront.Next;
                }
            }

            #endregion
        };

        #endregion

        #region OBJECTS_GEOMETRY

        public class OBJECT2DGEOMETRY
        {
            public int nCount;
            public double dXScale, dYScale, dXCent, dYCent, dXMax, dXMin, dYMax, dYMin;
            public double dPerimeter, dArea, dShapeFactor;
            public double dXCentre, dYCentre, dRadius, dmsError;
        };

        public class OBJECT3DGEOMETRY
        {
            private int m_nSNo = -1;
            public int SNo
            {
                get { return m_nSNo; }
                set { m_nSNo = value; }
            }

            private double m_dWidth = -1;
            public double Width
            {
                get { return Math.Round(m_dWidth, 2); }
                set { m_dWidth = value; }
            }

            private double m_dLength;
            public double Length
            {
                get { return Math.Round(m_dLength, 2); }
                set { m_dLength = value; }
            }

            private double m_dDiameter;
            public double Diameter
            {
                get { return Math.Round(m_dDiameter, 2); }
                set { m_dDiameter = value; }
            }

            private double m_dPerimeter;
            public double Perimeter
            {
                get { return Math.Round(m_dPerimeter, 2); }
                set { m_dPerimeter = value; }
            }

            private double m_dArea;
            public double Area
            {
                get { return Math.Round(m_dArea, 2); }
                set { m_dArea = value; }
            }

            private double m_dVolume;
            public double Volume
            {
                get { return Math.Round(m_dVolume, 2); }
                set { m_dVolume = value; }
            }

            private double m_dSurfaceArea;
            public double SurfaceArea
            {
                get { return Math.Round(m_dSurfaceArea, 2); }
                set { m_dSurfaceArea = value; }
            }

            private double m_dShapeFactor;
            public double ShapeFactor
            {
                get { return Math.Round(m_dShapeFactor, 2); }
                set { m_dShapeFactor = value; }
            }

            private double m_dSpherocity;
            public double Spherocity
            {
                get { return Math.Round(m_dSpherocity, 2); }
                set { m_dSpherocity = value; }
            }

            private double m_dWeight;
            public double Weight
            {
                get { return Math.Round(m_dWeight, 2); }
                set { m_dWeight = value; }
            }
        };

        #endregion

        #region VARIABLES

        int m_nChainLen = 0;
        CRect m_rectDir = new CRect();

        #endregion

        #region CTOR

        public CBlobAnalysis(Bitmap srcImage)
            : base(srcImage)
        { }

        #endregion

        #region THIN_IMAGE

        private byte[,] getThinImage(byte[,] vInRaster, CDefinitions.REGION_TYPE eRegion)
        {
            try
            {
                byte[,] vOutRaster = new byte[m_nHeight, m_nWidth];
                byte[,] vTempRaster = new byte[m_nHeight, m_nWidth];

                if (CDefinitions.REGION_TYPE.eBlack == eRegion)
                {
                    for (int nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        for (int nCol = 0; nCol < m_nWidth; nCol++)
                        {
                            vTempRaster[nRow, nCol] = (byte)(CDefinitions.IMAX - vInRaster[nRow, nCol]);
                        }
                    }
                }
                else
                {
                    Array.Copy(vInRaster, vTempRaster, vTempRaster.Length);
                }

                // Below computation is valid only if Height is multiple of 4 
                int nBlockWidth = m_nHeight / 4;
                int nNumBlocks = m_nHeight / nBlockWidth;
                if ((m_nHeight % nBlockWidth) != 0) nNumBlocks += 1;

                // flags keeps track of block that have been thined completely
                // This speeds up the m_nHeight*m_nWidth processing time
                int[] vFlags = new int[nNumBlocks];

                // When number of flags = nNumBlocks image is processed completely
                int nNumFlagsSet = 0;
                while (nNumFlagsSet != nNumBlocks)
                {
                    Array.Copy(vTempRaster, vOutRaster, vTempRaster.Length);
                    for (int nBlockCount = 0; nBlockCount < nNumBlocks; nBlockCount++)
                    {
                        if (vFlags[nBlockCount] == cTRUE) continue;

                        if (processImage(vTempRaster, vOutRaster, nBlockCount, nBlockWidth, 1) == cFALSE)
                        {
                            nNumFlagsSet++;
                            vFlags[nBlockCount] = cTRUE;
                            continue;
                        }
                    }
                    if (nNumFlagsSet == nNumBlocks) break;

                    Array.Copy(vOutRaster, vTempRaster, vOutRaster.Length);
                    for (int nBlockCount = 0; nBlockCount < nNumBlocks; nBlockCount++)
                    {
                        if (vFlags[nBlockCount] == cTRUE) continue;

                        if (processImage(vTempRaster, vOutRaster, nBlockCount, nBlockWidth, 2) == cFALSE)
                        {
                            nNumFlagsSet++;
                            vFlags[nBlockCount] = cTRUE;
                            continue;
                        }
                    }
                }// END WHILE

                Array.Copy(vTempRaster, vOutRaster, vOutRaster.Length);
                vTempRaster = null;
                return vOutRaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int processImage(byte[,] vInRaster, byte[,] vOutRaster, int nBlockNum, int nBlockWidth, int nPass)
        {
            try
            {
                int nDeleted = cFALSE;
                Point ptY = new Point(nBlockNum * nBlockWidth, (nBlockNum + 1) * nBlockWidth);

                for (int nRow = ptY.X; nRow < ptY.Y; nRow++)
                {
                    // For 1st Block Only
                    if (nRow == 0)
                    {
                        //nRow += 1;
                        continue;
                    }
                    else if (nRow >= m_nHeight - 1) break;

                    for (int nCol = 1; nCol < (m_nWidth - 1); nCol++)
                    {
                        if (cFALSE != vInRaster[nRow, nCol])
                        {
                            if (deletePixelFromBlock(ref vInRaster, nCol, nRow, nPass) == true)
                            {
                                nDeleted = cTRUE;
                                vOutRaster[nRow, nCol] = CDefinitions.NOEDGE;
                            }
                        }
                    }
                }

                return nDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool deletePixelFromBlock(ref byte[,] vInRaster, int nCol, int nRow, int nPass)
        {
            try
            {
                bool bSuccess = false;
                byte[] buffer = new byte[9];

                // Top Left Neighbor
                buffer[0] = vInRaster[nRow - 1, nCol - 1]; buffer[1] = vInRaster[nRow - 1, nCol]; buffer[2] = vInRaster[nRow - 1, nCol + 1];
                buffer[4] = vInRaster[nRow, nCol - 1]; buffer[5] = vInRaster[nRow, nCol]; buffer[6] = vInRaster[nRow - 1, nCol + 1];
                buffer[7] = vInRaster[nRow + 1, nCol - 1]; buffer[3] = vInRaster[nRow + 1, nCol]; buffer[8] = vInRaster[nRow + 1, nCol + 1];//buffer[0];

                // Ensure 1/0 Binary Image
                for (int i = 0; i < 9; i++)
                {
                    if (0 < buffer[i]) buffer[i] = 1;
                }

                int N1 = buffer[0] | buffer[1] + buffer[2] | buffer[3] + buffer[4] | buffer[5] + buffer[6] | buffer[7];
                int N2 = buffer[1] | buffer[2] + buffer[3] | buffer[4] + buffer[5] | buffer[6] + buffer[7] | buffer[0];
                int N = System.Math.Min(N1, N2);

                int C = (1 ^ buffer[1]) & (buffer[2] | buffer[3]) +
                        (1 ^ buffer[3]) & (buffer[4] | buffer[5]) +
                        (1 ^ buffer[5]) & (buffer[6] | buffer[7]) +
                        (1 ^ buffer[7]) & (buffer[0] | buffer[1]);

                if (C == 1 && (N == 2 || N == 3))
                {
                    switch (nPass)
                    {
                        case 1:
                            if (((buffer[1] | buffer[2] | (1 ^ buffer[4])) & buffer[3]) == 0) bSuccess = true;
                            break;
                        case 2:
                            if (((buffer[1] | buffer[2] | (1 ^ buffer[4])) & buffer[3]) == 0) bSuccess = true;
                            break;
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

        #region CONNECT_BLOBS

        private void getBinaryRaster(ref byte[,] vRaster)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (vRaster[nRow, nCol] >= CDefinitions.POSSIBLE_EDGE)
                            vRaster[nRow, nCol] = CDefinitions.IMAX;
                        else
                            vRaster[nRow, nCol] = CDefinitions.IMIN;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getThickBoundaryImg(ref byte[,] vRaster)
        {
            for (int nRow = 1; nRow < m_nHeight - 1; nRow++)
            {
                for (int nCol = 1; nCol < m_nWidth - 1; nCol++)
                {
                    int nNeighbour = 0;
                    if (vRaster[nRow, nCol] != CDefinitions.NOEDGE)
                    {
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                if (nNeighbour < 3)
                                {
                                    if (vRaster[nRow + i, nCol + j] == CDefinitions.NOEDGE)
                                        vRaster[nRow + i, nCol + j] = CDefinitions.EDGE;
                                    nNeighbour++;
                                }
                                else break;
                            }
                            if (nNeighbour >= 3) break;
                        }
                    }
                }
            }
        }

        // pElm points to the head of a list of Boundary pixels. This list is different from the List of Rocks
        private void getConnectedEdges(ref byte[,] vRaster, int nMinLenUnConnected, int nMinLenConnected)
        {
            try
            {
                CBlobLinkedList llBlob = new CBlobLinkedList();

                // Storing blobs in the List
                getListType(ref vRaster, ref llBlob, nMinLenUnConnected, nMinLenConnected);
                Array.Clear(vRaster, 0, m_nWidth * m_nHeight);
                llBlob.Draw(ref vRaster);

                //Join the disconnected parts
                joinUnConnectedEdges(ref vRaster, llBlob);

                //nCol,nRow starts from 2 so that the endpoints on the edges are not considered
                for (int nRow = 2; nRow < m_nHeight - 2; nRow++)
                {
                    for (int nCol = 2; nCol < m_nWidth - 2; nCol++)
                    {
                        if (getEndPoints(ref vRaster, nCol, nRow) == 1 &&
                            vRaster[nRow, nCol] == CDefinitions.EDGE)
                        {
                            for (int nRadius = 1; nRadius <= 100; nRadius++)
                            {
                                for (int rRow = -nRadius; rRow < nRadius + 1; rRow++)
                                {
                                    for (int rCol = -nRadius; rCol < nRadius + 1; rCol++)
                                    {
                                        // If the same point
                                        if (rCol != 0 && rRow != 0)
                                            // If the boundaries are not crossed
                                            if ((nCol + rCol) >= 0 && (nRow + rRow) >= 0)
                                                // Other endpoint detected
                                                if (getEndPoints(ref vRaster, nCol + rCol, nRow + rRow) == 1 &&
                                                    vRaster[nRow + rRow, nCol + rCol] == CDefinitions.EDGE)
                                                {
                                                    drawDisconnectedEdges(ref vRaster, new Rectangle(nCol, nRow, rCol, rRow));
                                                    goto NextEndPoint;
                                                }
                                    }
                                }
                            }
                        NextEndPoint: ;
                        }//All endpoints are joined to the nearest endpoints
                    }
                }

                byte[,] vTmpBuffer = new byte[m_nHeight, m_nWidth];
                for (int nRow = 2; nRow < m_nHeight - 2; nRow++)
                {
                    for (int nCol = 2; nCol < m_nWidth - 2; nCol++)
                    {
                        if (getEndPoints(ref vRaster, nCol, nRow) == 1 &&
                            vRaster[nRow, nCol] == CDefinitions.EDGE)
                        {
                            Array.Copy(vRaster, vTmpBuffer, m_nWidth * m_nHeight);
                            eatEdges(ref vTmpBuffer, nCol, nRow);
                            for (int nRadius = 1; nRadius < 200; nRadius++)
                            {
                                for (int rRow = -nRadius; rRow < nRadius + 1; rRow++)
                                {
                                    for (int rCol = -nRadius; rCol < nRadius + 1; rCol++)
                                    {
                                        if (rCol != 0 && rRow != 0)
                                            if ((nCol + rCol) >= 0 && (nRow + rRow) >= 0)
                                                if (vTmpBuffer[nRow + rRow, nCol + rCol] == CDefinitions.EDGE)
                                                {
                                                    drawDisconnectedEdges(ref vRaster, new Rectangle(nCol, nRow, rCol, rRow));
                                                    goto _NEXT_POINT_;
                                                }
                                    }
                                }
                            }
                        _NEXT_POINT_: ;
                        } //All endpoints are joined to the nearest curve
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Description: pElm points to the head of a list of Boundary pixels. This list is different from the List of Rocks.
        //InputParameters  : ConnectRaster, 100000, 0
        private void afterConnectEdges(ref byte[,] vRaster, int nMinLenUnConnected, int nMinLenConnected)
        {
            try
            {
                CBlobLinkedList llBlob = new CBlobLinkedList();
                //	Store Raster in List
                getListType(ref vRaster, ref llBlob, nMinLenUnConnected, nMinLenConnected);

                //	Store List to Raster. The Bufer will then no longer have small disconnected parts.
                while (llBlob.m_objFront != null)
                {
                    CElm pElm = llBlob.m_objFront.Blob.chain.pHead;
                    while (pElm != null)
                    {
                        vRaster[pElm.BoundaryT.Y, pElm.BoundaryT.X] = CDefinitions.EDGE;
                        CElm ptemp = pElm;
                        pElm = pElm.Next;
                        ptemp = null;
                    }
                    llBlob.m_objFront = llBlob.m_objFront.Next;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // This Routine will eat up  the Disconnected Components from the Raster and store them in the list 
        private void getListType(ref byte[,] vRaster, ref CBlobLinkedList llBlob, int nMinLenUnConnected, int nMinLenConnected)
        {
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        if (getEndPoints(ref vRaster, nCol, nRow) == 1 && vRaster[nRow, nCol] == CDefinitions.EDGE)
                        {

                            //	Initialising Boundary Length 
                            //	This must be declared as a global variable
                            m_nChainLen = 0;

                            //	Initialising rectangle parameters for disconnected components
                            m_rectDir.SetRect(m_nWidth - 1, 0, 0, m_nHeight - 1); // SG

                            CElmLinkedList llElm = new CElmLinkedList();
                            traverseEnds(ref vRaster, ref llElm, nCol, nRow);

                            CBlob blob = new CBlob();
                            blob.nChainLen = m_nChainLen;
                            blob.chain = llElm;
                            blob.nTag = CDefinitions.UNCONNECTED;
                            blob.rectDir = m_rectDir;

                            if (blob.nChainLen >= nMinLenUnConnected) llBlob.AddBlobType(blob);
                            blob = null;
                        }
                    }
                }

                //	This Routine will put the remaining connected pieces of the buffer to the list
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        //	Initialising Boundary Length 
                        m_nChainLen = 0;

                        if (vRaster[nRow, nCol] == CDefinitions.EDGE)
                        {
                            //	Initialising rectangle parameters
                            m_rectDir.SetRect(m_nWidth - 1, 0, 0, m_nHeight - 1); // SG

                            CElmLinkedList llElm = new CElmLinkedList();
                            traverseConnected(ref vRaster, ref llElm, nCol, nRow);

                            CBlob blob = new CBlob();
                            blob.nChainLen = m_nChainLen;
                            blob.chain = llElm;
                            blob.nTag = CDefinitions.CONNECTED;
                            blob.rectDir = m_rectDir;

                            if (blob.nChainLen >= nMinLenConnected) llBlob.AddBlobType(blob);
                            blob = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Will return 1 if x,y is an end point else it will return 0
        // nu, nr, nd, nl are the buffer positions of the NEAR up, right, down and left
        // neighbours respectively. n1, n2, n3, n4 are the intensity levels of the 
        // NEAR up, right, down and left neighbours respectively. 
        // f1, f2, f2, f4 are the intensity levels of the FAR up&right, down&right, 
        // down&left and up&left neighbours respectiely.
        // cntNear is the no of Near Neighbours.
        // cntFar is the no of Far Neighbours.
        private int getEndPoints(ref byte[,] vRaster, int aCol, int aRow)
        {
            try
            {
                int nVal = 0;
                // Get the pixel intensity for the given (nCol, nRow)
                if ((nVal = vRaster[aRow, aCol]) == 0)
                    return (0);

                int nTotal = getNeighbourCount(ref vRaster, aCol, aRow);

                if (nTotal == 0) return (1);
                if (nTotal > 3) return (0);
                if (nTotal == 1) return (1);

                // Call function to get the pixel intensity
                int n1, n2, n3, n4, f1, f2, f3, f4;

                if (aRow < m_nHeight - 1) n1 = vRaster[aRow + 1, aCol];
                else n1 = 0;

                if (aCol < m_nWidth - 1) n2 = vRaster[aRow, aCol + 1];
                else n2 = 0;

                if (aRow > 0) n3 = vRaster[aRow - 1, aCol];
                else n3 = 0;

                if (aCol > 0) n4 = vRaster[aRow, aCol - 1];
                else n4 = 0;

                int cntNear = 0;
                if (n1 == CDefinitions.EDGE) cntNear++;
                if (n2 == CDefinitions.EDGE) cntNear++;
                if (n3 == CDefinitions.EDGE) cntNear++;
                if (n4 == CDefinitions.EDGE) cntNear++;

                if (cntNear >= 2) return (0);

                if (aCol < m_nWidth - 1 && aRow < m_nHeight - 1) f1 = vRaster[aRow + 1, aCol + 1];
                else f1 = 0;

                if (aCol < m_nWidth - 1 && aRow > 0) f2 = vRaster[aRow - 1, aCol + 1];
                else f2 = 0;

                if (aCol > 0 && aRow > 0) f3 = vRaster[aRow - 1, aCol - 1];
                else f3 = 0;

                if (aCol > 0 && aRow < m_nHeight - 1) f4 = vRaster[aRow + 1, aCol - 1];
                else f4 = 0;

                int cntFar = 0;

                if (f1 == CDefinitions.EDGE) cntFar++;
                if (f2 == CDefinitions.EDGE) cntFar++;
                if (f3 == CDefinitions.EDGE) cntFar++;
                if (f4 == CDefinitions.EDGE) cntFar++;

                if (cntNear == 1 && cntFar == 2)
                {
                    if (n1 == CDefinitions.EDGE) if (f1 == CDefinitions.EDGE && f4 == CDefinitions.EDGE) return (1);
                    if (n2 == CDefinitions.EDGE) if (f1 == CDefinitions.EDGE && f2 == CDefinitions.EDGE) return (1);
                    if (n3 == CDefinitions.EDGE) if (f3 == CDefinitions.EDGE && f2 == CDefinitions.EDGE) return (1);
                    if (n4 == CDefinitions.EDGE) if (f3 == CDefinitions.EDGE && f4 == CDefinitions.EDGE) return (1);
                }

                if (cntFar >= 2) return (0);

                if (n1 == CDefinitions.EDGE) if (f4 == CDefinitions.EDGE || f1 == CDefinitions.EDGE) return (1);
                if (n2 == CDefinitions.EDGE) if (f1 == CDefinitions.EDGE || f2 == CDefinitions.EDGE) return (1);
                if (n3 == CDefinitions.EDGE) if (f3 == CDefinitions.EDGE || f2 == CDefinitions.EDGE) return (1);
                if (n4 == CDefinitions.EDGE) if (f4 == CDefinitions.EDGE || f3 == CDefinitions.EDGE) return (1);

                return (-1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Returns the no of neighbours of (x,y) of Raster Raster.
        private int getNeighbourCount(ref byte[,] vRaster, int aCol, int aRow)
        {
            try
            {
                int startCol = aCol - 1;
                int startRow = aRow - 1;
                int endCol = aCol + 1;
                int endRow = aRow + 1;

                if (startCol < 0) startCol = 0;
                if (startRow < 0) startRow = 0;
                if (endCol >= m_nWidth) endCol = m_nWidth - 1;
                if (endRow >= m_nHeight) endRow = m_nHeight - 1;

                int nNum = 0;
                for (int j = startRow; j <= endRow; j++)
                {
                    for (int i = startCol; i <= endCol; i++)
                    {
                        int nVal = vRaster[j, i];
                        if (aCol == i && aRow == j) continue;
                        if (nVal == CDefinitions.EDGE) nNum++;
                    }
                }

                return (nNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Traverses an end point
        private void traverseEnds(ref byte[,] vRaster, ref CElmLinkedList llElm, int aCol, int aRow)
        {
            try
            {
                if (getEndPoints(ref vRaster, aCol, aRow) == 0) return;

                if (aCol < 0 || aCol > m_nWidth - 1) return;
                if (aRow < 0 || aRow > m_nHeight - 1) return;

                int nPos = aCol + aRow * m_nWidth;
                int nNum = getNeighbourCount(ref vRaster, aCol, aRow);

                vRaster[aRow, aCol] = 0;
                m_nChainLen++;

                Point ptBT = new Point();
                ptBT.X = aCol; ptBT.Y = aRow;

                llElm.AddElmType(ptBT);

                if (aRow > m_rectDir.Top) m_rectDir.Top = aRow;
                if (aRow < m_rectDir.Bottom) m_rectDir.Bottom = aRow;
                if (aCol < m_rectDir.Left) m_rectDir.Left = aCol;
                if (aCol > m_rectDir.Right) m_rectDir.Right = aCol; //SG

                if (aRow < m_nHeight - 1) traverseEnds(ref vRaster, ref llElm, aCol, aRow + 1);
                if (aCol < m_nWidth - 1) traverseEnds(ref vRaster, ref llElm, aCol + 1, aRow);
                if (aRow > 0) traverseEnds(ref vRaster, ref llElm, aCol, aRow - 1);
                if (aCol > 0) traverseEnds(ref vRaster, ref llElm, aCol - 1, aRow);
                if (aCol < m_nWidth - 1 && aRow < m_nHeight - 1) traverseEnds(ref vRaster, ref llElm, aCol + 1, aRow + 1);
                if (aCol < m_nWidth - 1 && aRow > 0) traverseEnds(ref vRaster, ref llElm, aCol + 1, aRow - 1);
                if (aCol > 0 && aRow > 0) traverseEnds(ref vRaster, ref llElm, aCol - 1, aRow - 1);
                if (aCol > 0 && aRow < m_nHeight - 1) traverseEnds(ref vRaster, ref llElm, aCol - 1, aRow + 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void traverseConnected(ref byte[,] vRaster, ref CElmLinkedList llElm, int aCol, int aRow)
        {
            try
            {
                if (vRaster[aRow, aCol] == 0) return;

                if (aCol < 0 || aCol > m_nWidth - 1) return;
                if (aRow < 0 || aRow > m_nHeight - 1) return;

                int nNum = getNeighbourCount(ref vRaster, aCol, aRow);

                vRaster[aRow, aCol] = 0;
                m_nChainLen++;

                if (aRow > m_rectDir.Top) m_rectDir.Y = aRow;
                if (aRow < m_rectDir.Bottom) m_rectDir.Bottom = aRow;
                if (aCol < m_rectDir.Left) m_rectDir.X = aCol;
                if (aCol > m_rectDir.Right) m_rectDir.Right = aCol; //SG

                Point ptBT = new Point();
                ptBT.X = aCol; ptBT.Y = aRow;
                llElm.AddElmType(ptBT);

                if (aRow < m_nHeight - 1) traverseConnected(ref vRaster, ref llElm, aCol, aRow + 1);
                if (aCol < m_nWidth - 1) traverseConnected(ref vRaster, ref llElm, aCol + 1, aRow);
                if (aRow > 0) traverseConnected(ref vRaster, ref llElm, aCol, aRow - 1);
                if (aCol > 0) traverseConnected(ref vRaster, ref llElm, aCol - 1, aRow);
                if (aCol < m_nWidth - 1 && aRow < m_nHeight - 1) traverseConnected(ref vRaster, ref llElm, aCol + 1, aRow + 1);
                if (aCol < m_nWidth - 1 && aRow > 0) traverseConnected(ref vRaster, ref llElm, aCol + 1, aRow - 1);
                if (aCol > 0 && aRow > 0) traverseConnected(ref vRaster, ref llElm, aCol - 1, aRow - 1);
                if (aCol > 0 && aRow < m_nHeight - 1) traverseConnected(ref vRaster, ref llElm, aCol - 1, aRow + 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Description : This function will fill the buffer from x,y to xto,yto provided it does
        // not ntersect an existing curve.
        //*******************************************************************************************/
        private void drawDisconnectedEdges(ref byte[,] vRaster, Rectangle rect)
        {
            try
            {
                double dCol, fRow, fColTo, fRowTo, Tx, Ty, nSlope;
                int V2, nDiffCol, nDiffRow;
                List<Point> vLine = new List<Point>();

                //	If nFlag is 1, we draw the line
                int nFlag = 1;
                if (rect.Left == rect.Right)	//	SLOPE IS INFINITY
                {
                    if (rect.Top <= rect.Bottom)
                    {
                        for (int i = rect.Top; i <= rect.Bottom; i++)
                        {
                            if (nFlag == 0) continue;
                            if (vRaster[i, rect.Left] == CDefinitions.EDGE && !(i == rect.Top) && !(i == rect.Bottom))
                            {
                                nFlag = 0; continue;
                            }
                            vLine.Add(new Point(rect.Left, i));
                        }
                    }
                    else
                    {
                        for (int i = rect.Bottom; i <= rect.Top; i++)
                        {
                            if (nFlag == 0) continue;
                            if (vRaster[i, rect.Left] == CDefinitions.EDGE && !(i == rect.Top) && !(i == rect.Bottom))
                            {
                                nFlag = 0; continue;
                            }
                            vLine.Add(new Point(rect.Left, i));
                        }
                    }
                }
                else //	SLOPE IS NOT INFINITE
                {
                    fColTo = rect.Right; fRowTo = rect.Bottom;

                    //	Draw aCol,aRow to colTo,rect.Bottom
                    nDiffCol = rect.Left - rect.Right;
                    nDiffRow = rect.Top - rect.Bottom;
                    if (nDiffCol < 0) nDiffCol = -nDiffCol;
                    if (nDiffRow < 0) nDiffRow = -nDiffRow;

                    dCol = rect.Left; fRow = rect.Top;

                    nSlope = (fRow - fRowTo) / (dCol - fColTo);

                    if (nDiffCol >= nDiffRow)
                    {
                        if (rect.Left < rect.Right)		//		rect.Left==colTo 
                        {
                            for (int i = rect.Left; i <= rect.Right; i++)
                            {
                                Tx = (double)(i);
                                Ty = fRow + nSlope * (Tx - dCol);
                                V2 = (int)(Ty);
                                if (nFlag == 0) continue;
                                if (vRaster[i, V2] == CDefinitions.EDGE &&
                                    !(i == rect.Left && V2 == rect.Top) &&
                                    !(i == rect.Right && V2 == rect.Bottom))
                                {
                                    nFlag = 0;
                                    continue;
                                }
                                vLine.Add(new Point(i, V2));
                            }
                        }
                        else
                        {
                            for (int i = rect.Right; i <= rect.Left; i++)
                            {
                                Tx = (double)(i);
                                Ty = fRow + nSlope * (Tx - dCol);

                                V2 = (int)(Ty);
                                if (nFlag == 0) continue;
                                if (vRaster[i, V2] == CDefinitions.EDGE &&
                                    !(i == rect.Left && V2 == rect.Top) &&
                                    !(i == rect.Right && V2 == rect.Bottom))
                                {
                                    nFlag = 0;
                                    continue;
                                }
                                /* Raster[getPos(Raster,i,V2)] = CDefinitions.EDGE;*/
                                vLine.Add(new Point(i, V2));
                            }
                        }
                    }
                    else
                    {
                        if (rect.Top <= rect.Bottom)
                        {
                            for (int i = rect.Top; i <= rect.Bottom; i++)
                            {
                                Ty = i;
                                Tx = dCol + (Ty - fRow) / nSlope;
                                V2 = (int)(Tx);
                                if (nFlag == 0) continue;
                                if (vRaster[V2, i] == CDefinitions.EDGE &&
                                    !(V2 == rect.Left && i == rect.Top) &&
                                    !(V2 == rect.Right && i == rect.Bottom))
                                {
                                    nFlag = 0;
                                    continue;
                                }
                                vLine.Add(new Point(V2, i));
                            }
                        }
                        else
                        {
                            for (int i = rect.Bottom; i <= rect.Top; i++)
                            {
                                Ty = i;
                                Tx = dCol + (Ty - fRow) / nSlope;
                                V2 = (int)(Tx);
                                if (nFlag == 0) continue;
                                if (vRaster[i, V2] == CDefinitions.EDGE &&
                                    !(V2 == rect.Left && i == rect.Top) &&
                                    !(V2 == rect.Right && i == rect.Bottom))
                                {
                                    nFlag = 0; continue;
                                }
                                vLine.Add(new Point(V2, i));
                            }
                        }
                    }
                }//	If slope is infinity over			

                if (nFlag == 1)
                {
                    for (int i = 0; i < vLine.Count; i++)
                    {
                        vRaster[vLine[i].Y, vLine[i].X] = CDefinitions.EDGE;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void joinUnConnectedEdges(ref byte[,] vRaster, CBlobLinkedList llBlob)
        {
            try
            {
                CElm pElm = null; CElmLinkedList llElm = null;
                List<Point> vEnds = new List<Point>();
                double dDist, dxx, dyy;

                CBlobNode objBlobNode = llBlob.m_objFront;
                while (objBlobNode != null)
                {
                    //	Connect the ends if possible
                    if (objBlobNode.Blob.nTag == CDefinitions.UNCONNECTED)
                    {
                        llElm = objBlobNode.Blob.chain;
                        pElm = llElm.pHead;

                        while (pElm != null)
                        {
                            if (getEndPoints(ref vRaster, pElm.BoundaryT.X, pElm.BoundaryT.Y) == 1)
                            {
                                vEnds.Add(new Point(pElm.BoundaryT.X, pElm.BoundaryT.Y));
                            }
                            pElm = pElm.Next;
                        }
                    }//	End if

                    //	If there are exactly 2 endpoints for the rock pointed at by objBlobNode, then
                    //	drawDisconnectedEdges is activated for the 2 endpoints
                    if (vEnds.Count == 2)
                    {
                        dxx = (vEnds[0].X - vEnds[1].X);
                        dyy = (vEnds[0].Y - vEnds[1].Y);
                        dDist = System.Math.Pow(dxx * dxx + dyy * dyy, 0.5f);
                        if (dDist < objBlobNode.Blob.nChainLen * (2.0f / 3.0f) && dDist < 500)
                        {
                            drawDisconnectedEdges(ref vRaster, new Rectangle(vEnds[0].X, vEnds[0].Y, vEnds[1].X - vEnds[0].X, vEnds[1].Y - vEnds[0].Y));
                        }
                    }
                    objBlobNode = objBlobNode.Next;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eatEdges(ref byte[,] vRaster, int aCol, int aRow)
        {
            try
            {
                int nNewCol = 0, nNewRow = 0;

                vRaster[aRow, aCol] = CDefinitions.NOEDGE;
                if (setNeighbourToNil(ref vRaster, aCol, aRow, ref nNewCol, ref nNewRow))
                {
                    eatEdges(ref vRaster, nNewCol, nNewRow);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Description:- This function finds the neighbour of the current point i,j
        private bool setNeighbourToNil(ref byte[,] vRaster, int aCol, int aRow, ref int aNewCol, ref int aNewRow)
        {
            try
            {
                int nCount = 0;
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        if (vRaster[aRow + y, aCol + x] == CDefinitions.EDGE)
                        {
                            aNewCol = aCol + x;
                            aNewRow = aRow + y;
                            nCount++;
                        }
                    }
                }

                if (nCount == 1) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PUBLIC_FUNCTION

        public Bitmap PreBlobAnalysis(double dTLow, double dTHigh, int nSigma, ref int nBlobs)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);

                CCanny obj = new CCanny(SrcImage);
                SrcImage = obj.Canny(dTLow, dTHigh, nSigma);
                if (null == SrcImage) return null;

                // Call the function getThinImage, ConnectEdges, AfterPreProcess are 
                // called twice because that's the loop through which the image 
                // passes through during editing.
                byte[,] vInRaster = new byte[m_nHeight, m_nWidth];
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        vInRaster[nRow, nCol] = CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow));
                    }
                }

                byte[,] vOutRaster = getThinImage(vInRaster, CDefinitions.REGION_TYPE.eWhite);
                //createBitmap(vOutRaster, ref dest);

                // Only if the image is binary then it has to be updated to the 
                // InRaster and further functions accomplished - otherwise the 
                // ConnectEdges function would fail and the system would crash.
                getBinaryRaster(ref vOutRaster);
                getConnectedEdges(ref vOutRaster, 0, 0);

                afterConnectEdges(ref vOutRaster, 1000000, 0);

                getThickBoundaryImg(ref vOutRaster);

                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dest.SetPixel(nCol, nRow, Color.FromArgb(vOutRaster[nRow, nCol], vOutRaster[nRow, nCol], vOutRaster[nRow, nCol]));
                    }
                }

                getLabeledImg(CDefinitions.REGION_TYPE.eWhite, false, ref nBlobs);

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // function	: PostBlobAnalysis
        // return		: 3D Record, nObject
        // description: This function does the Complete grain analysis of the given image.
        // Parameters : Labeled Image
        //              Equivalent unit.
        // Returns    : On sucess returns 0.
        public Bitmap PostBlobAnalysis(double dDensity, double dXScale, double dYScale, ref List<OBJECT3DGEOMETRY> lRecords)
        {
            try
            {
                // This function will give an OutRaster for 8 Bits Image.
                int nBlobs = -1;
                Bitmap dest = getLabeledImg(CDefinitions.REGION_TYPE.eWhite, true, ref nBlobs);
                if (nBlobs <= 0)
                {
                    MessageBox.Show("No object is detected.\nMaybe Pre-Blob analysis is not performed on the image");
                    return null;
                }

                // Detect the object 3D dimensions
                for (int nObj = 1; nObj <= nBlobs; nObj++)
                {
                    List<Point> lCoords = new List<Point>();
                    if (false == GetOutlinedObjects(dest, nObj, ref lCoords)) continue;
                    OBJECT2DGEOMETRY obj2D = new OBJECT2DGEOMETRY();
                    // Call the function for metrics calculation
                    if (false == GetObject2DFeatures(ref lCoords, 1.0, 1.0, ref obj2D)) continue;

                    int nMaxInt = Int32.MinValue, nMinInt = Int32.MaxValue;
                    double dRadius1 = 0.0, dRadius2 = 0.0;
                    for (int nCnt = 0; nCnt < obj2D.nCount; nCnt++)
                    {
                        dRadius1 = (obj2D.dXCentre - lCoords[nCnt].X) * (obj2D.dXCentre - lCoords[nCnt].X);
                        dRadius1 += (obj2D.dYCentre - lCoords[nCnt].Y) * (obj2D.dYCentre - lCoords[nCnt].Y);
                        if (dRadius1 > nMaxInt) nMaxInt = (int)dRadius1;
                        if (dRadius1 < nMinInt) nMinInt = (int)dRadius1;
                    }

                    dRadius1 = Math.Sqrt(Math.Abs(nMaxInt));
                    dRadius2 = Math.Sqrt(Math.Abs(nMinInt));

                    double dRadiusRatio = 2.0f;
                    if (dRadius1 > 1.0e-06 && dRadius2 > 1.0e-06)
                        dRadiusRatio = (dRadius2 / dRadius1);

                    double dLength, dWidth;
                    if (obj2D.dXMax - obj2D.dXMin > obj2D.dYMax - obj2D.dYMin)
                    {
                        dLength = obj2D.dXMax - obj2D.dXMin;
                        dWidth = obj2D.dYMax - obj2D.dYMin;
                    }
                    else
                    {
                        dLength = obj2D.dYMax - obj2D.dYMin;
                        dWidth = obj2D.dXMax - obj2D.dXMin;
                    }

                    OBJECT3DGEOMETRY obj3D = new OBJECT3DGEOMETRY();
                    obj3D.SNo = nObj;
                    obj3D.Width = dXScale * dWidth;
                    obj3D.Length = dYScale * dLength;
                    obj3D.Perimeter = dXScale * obj2D.dPerimeter;
                    obj3D.Area = dXScale * dYScale * obj2D.dArea;
                    obj3D.Volume = dXScale * dXScale * dXScale * dWidth * obj2D.dArea * Math.Pow(dRadiusRatio, 1.5);
                    obj3D.Diameter = Math.Pow((42 / 22.0f) * obj3D.Volume, (1 / 3.0));
                    obj3D.SurfaceArea = (22.0f / 7.0f) * obj3D.Diameter * obj3D.Diameter;
                    obj3D.ShapeFactor = obj2D.dShapeFactor;
                    obj3D.Spherocity = dRadiusRatio;
                    obj3D.Weight = dDensity * obj3D.Volume;

                    //Only those fragments which have all non-zero 3D parameters are considered
                    if (obj3D.Volume > 0 && obj3D.Diameter > 0 && obj3D.Weight > 0)
                    {
                        lRecords.Add(obj3D);
                    }
                }// END BLOBS

                // The previous nRecordNum is maintained as it is, in PostBlobAnalysis 
                // function, hence nNumObjects contains the no. of all the 
                // analysed fragments in all the views.
                // this function stretches the number of regions present in an image
                dest = getStretchedImg(dest);
                if (null == dest)
                {
                    MessageBox.Show("Please do proper thresholding.\nToo many regions are detected");
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap OutlineObjectImage(int nObjNum, ref List<System.Drawing.Point> lCoords)
        {
            try
            {
                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                if (true == GetOutlinedObjects(m_bmpSrc, nObjNum, ref lCoords))
                {
                    for (int nRow = 0; nRow < m_nHeight; nRow++)
                    {
                        for (int nCol = 0; nCol < m_nWidth; nCol++)
                        {
                            dest.SetPixel(nCol, nRow, Color.FromArgb(CDefinitions.IMAX, CDefinitions.IMAX, CDefinitions.IMAX));
                        }
                    }

                    for (int nCnt = 0; nCnt < lCoords.Count(); nCnt++)
                    {
                        dest.SetPixel(lCoords[nCnt].X, lCoords[nCnt].Y, Color.FromArgb(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN));
                    }
                }
                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap GetThinImage(ref byte[,] vInRaster, CDefinitions.REGION_TYPE eRegion)
        {
            try
            {
                byte[,] arrOut = getThinImage(vInRaster, eRegion);

                Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        int nI = arrOut[nRow, nCol];
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
    }
}
