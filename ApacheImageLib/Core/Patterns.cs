////////////////////////// SGPattern.cpp /////////////////////////////
//																	//
// SGPattern : implementation of the CPatterns class				//
// AUTHOR	 : Sandeep Gupta										//
// DATED	 : 10 Aug 2011											//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Apache.ImageLib
{
    public class CPatterns : CImageInfo
    {
        #region CTOR

        public CPatterns(Bitmap srcImage)
            : base(srcImage)
        { }

        public CPatterns(int nWidth, int nHeight)
            : base(nWidth, nHeight)
        { }

        #endregion

        #region PATTERNS

        public Bitmap Bar(byte nLow, byte nHigh, byte nBars)
        {
            try
            {
                int nNewWidth = m_nWidth / nBars;
                byte nStepIntensity = (byte)((nHigh - nLow) / (nBars - 1));
                byte byIntensity = nLow;

                List<Color> lineRaster = new List<Color>();
                // HERE THE BUFFER IS FILLED WITH THE REQUIRED VALUES
                for (int nCol = 0; nCol < m_nWidth; nCol += nNewWidth)
                {
                    int nLimit = nCol + nNewWidth;
                    if (nLimit >= m_nWidth) nLimit = m_nWidth - 1;
                    for (int nCnt = nCol; nCnt <= nLimit; nCnt++)
                    {
                        RGB clr = new RGB(byIntensity, byIntensity, byIntensity);
                        lineRaster.Add(clr.Color);
                    }

                    // STEP INCREASE IN THE INTENSITY
                    byIntensity += nStepIntensity;

                    // TAKE CARE OF THE INTENSITY OFFSET
                    if (byIntensity > nHigh) byIntensity = nHigh;
                }
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        SrcImage.SetPixel(nCol, nRow, lineRaster[nCol]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }
            return SrcImage;
        }

        public Bitmap Chequer(byte nLow, byte nHigh, int nBoxWidth, int nBoxHeight)
        {
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                int y = (nRow / nBoxHeight) & 0x01;
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    int x = (nCol / nBoxWidth) & 0x01;
                    int z = (x + y) & 0x01;
                    if (0x01 == z)
                    {
                        RGB clr = new RGB(nHigh, nHigh, nHigh);
                        SrcImage.SetPixel(nCol, nRow, clr.Color);
                    }
                    else
                    {
                        RGB clr = new RGB(nLow, nLow, nLow);
                        SrcImage.SetPixel(nCol, nRow, clr.Color);
                    }
                }
            }

            return SrcImage;
        }

        public Bitmap Constant(int nBPP, byte byIntensity)
        {
            if (1 == nBPP)
                byIntensity = byIntensity > 0 ? CDefinitions.IMAX : CDefinitions.IMIN;
            else if (4 == nBPP)
                byIntensity = (byte)(byIntensity * CDefinitions.IMAX / 15);

            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    RGB clr = new RGB(byIntensity, byIntensity, byIntensity);
                    SrcImage.SetPixel(nCol, nRow, clr.Color);
                }
            }

            return SrcImage; ;
        }

        public Bitmap Disk(int nRadius, int xCentre, int yCentre, byte byIntensity)
        {
            int nSqRadius = nRadius * nRadius;
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                int nDiff = nRow - yCentre;
                int nSqRow = nDiff * nDiff;
                int nLimit = nSqRadius - nSqRow;
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    nDiff = nCol - xCentre;
                    int nSqCol = nDiff * nDiff;

                    if (nSqCol <= nLimit)
                    {
                        RGB clr = new RGB(byIntensity, byIntensity, byIntensity);
                        SrcImage.SetPixel(nCol, nRow, clr.Color);
                    }
                    else
                    {
                        RGB clr = new RGB(CDefinitions.IMIN, CDefinitions.IMIN, CDefinitions.IMIN);
                        SrcImage.SetPixel(nCol, nRow, clr.Color);
                    }
                }
            }

            return SrcImage; ;
        }

        public Bitmap Polygon(byte byIntensity, List<Point> ptCoords)
        {
            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    RGB clr = new RGB(0, 0, 0);
                    SrcImage.SetPixel(nCol, nRow, clr.Color);
                }
            }
            for (int nCnt = 0; nCnt < ptCoords.Count(); nCnt++)
            {
                if (nCnt == (ptCoords.Count() - 1))
                    drawLineImg(ptCoords[nCnt], ptCoords[0], byIntensity);
                else
                    drawLineImg(ptCoords[nCnt], ptCoords[nCnt + 1], byIntensity);
            }

            return SrcImage;
        }

        private void drawLineImg(Point ptStart, Point ptEnd, byte byIntensity)
        {
            Point ptOne = new Point();
            Point ptTwo = new Point();
            Point ptInc = new Point();
            int nIncr1, nIncr2, nDist;

            int newWidth = System.Math.Abs(ptEnd.X - ptStart.X);
            int newHeight = System.Math.Abs(ptEnd.Y - ptStart.Y);

            RGB clr = new RGB(byIntensity, byIntensity, byIntensity);

            if (newWidth >= newHeight)
            {
                if (ptStart.X > ptEnd.X)
                {
                    ptOne = ptEnd;
                    ptTwo.X = ptStart.X;
                    if (newHeight == 0) ptInc.Y = 0;
                    else
                    {
                        if (ptEnd.Y > ptStart.Y) ptInc.Y = -1;
                        else ptInc.Y = 1;
                    }
                }
                else
                {
                    ptOne = ptStart;
                    ptTwo.X = ptEnd.X;
                    if (newHeight == 0) ptInc.Y = 0;
                    else
                    {
                        if (ptEnd.Y > ptStart.Y) ptInc.Y = 1;
                        else ptInc.Y = -1;
                    }
                }
                nIncr1 = 2 * newHeight;
                nDist = nIncr1 - newWidth;
                nIncr2 = 2 * (newHeight - newWidth);
                SrcImage.SetPixel(ptOne.Y, ptOne.X, clr.Color);
                while (ptOne.X < ptTwo.X)
                {
                    ptOne.X++;
                    if (nDist < 0) nDist += nIncr1;
                    else
                    {
                        ptOne.Y += ptInc.Y;
                        nDist += nIncr2;
                    }
                    SrcImage.SetPixel(ptOne.Y, ptOne.X, clr.Color);
                }
            }
            else
            {
                if (ptStart.Y > ptEnd.Y)
                {
                    ptOne = ptEnd;
                    ptTwo.Y = ptStart.Y;
                    if (newWidth == 0)
                        ptInc.X = 0;
                    else
                    {
                        if (ptEnd.X > ptStart.X) ptInc.X = -1;
                        else ptInc.X = 1;
                    }
                }
                else
                {
                    ptOne = ptStart;
                    ptTwo.Y = ptEnd.Y;
                    if (newWidth == 0) ptInc.X = 0;
                    else
                    {
                        if (ptEnd.X > ptStart.X) ptInc.X = 1;
                        else ptInc.X = -1;
                    }
                }
                nIncr1 = 2 * newWidth;
                nDist = nIncr1 - newHeight;
                nIncr2 = 2 * (newWidth - newHeight);
                SrcImage.SetPixel(ptOne.Y, ptOne.X, clr.Color);
                while (ptOne.Y < ptTwo.Y)
                {
                    ptOne.Y++;
                    if (nDist < 0) nDist += nIncr1;
                    else
                    {
                        ptOne.X += ptInc.X;
                        nDist += nIncr2;
                    }
                    SrcImage.SetPixel(ptOne.Y, ptOne.X, clr.Color);
                }
            }
        }

        public Bitmap Sine(int nPeakAmp, int nOffset, double dHorzFreq, double dVerFreq)
        {
            double dHorPeriod, dVerPeriod;

            for (int nRow = 0; nRow < m_nHeight; nRow++)
            {
                if (dVerFreq != 0.0)
                    dVerPeriod = 2 * CDefinitions.PI * ((nRow / (float)m_nHeight)) / dVerFreq;
                else
                    dVerPeriod = 0.0;
                for (int nCol = 0; nCol < m_nWidth; nCol++)
                {
                    // Calculating the period
                    if (dHorzFreq != 0)
                        dHorPeriod = 2 * CDefinitions.PI * ((nCol / (float)m_nWidth)) / dHorzFreq;
                    else
                        dHorPeriod = 0.0;

                    double dSineVal = System.Math.Sin(dHorPeriod + dVerPeriod);

                    double dPixel = (nPeakAmp * dSineVal) + nOffset;
                    if (dPixel > 255) dPixel = CDefinitions.IMAX;
                    else if (dPixel < 0) dPixel = CDefinitions.IMIN;
                    RGB clr = new RGB((byte)dPixel, (byte)dPixel, (byte)dPixel);
                    SrcImage.SetPixel(nCol, nRow, clr.Color);
                }
            }

            return SrcImage; ;
        }

        #endregion

        #region FILTERS

        public Bitmap AddNoise(int nNoise, int nAmp, double dErrorProb)
        {
            Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
            try
            {
                Random rand = new Random();

                switch (nNoise)
                {
                    case 0:
                        for (int nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (int nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                double dRandomNum = 0.0;
                                for (byte seed = 0; seed < 12; seed++)
                                {
                                    double dRand = rand.Next(0, CDefinitions.RAND_MAX);
                                    dRandomNum += ((dRand / CDefinitions.RAND_MAX) - 0.5);
                                }
                                dRandomNum *= nAmp;
                                int nInt = (int)(CDefinitions.CLR2AVR(SrcImage.GetPixel(nCol, nRow)) + dRandomNum);
                                if (nInt < CDefinitions.IMIN) nInt = CDefinitions.IMIN;
                                if (nInt > CDefinitions.IMAX) nInt = CDefinitions.IMAX;
                                dest.SetPixel(nCol, nRow, Color.FromArgb((byte)nInt, (byte)nInt, (byte)nInt));
                            }
                        }
                        break;
                    case 1:
                        dErrorProb = 1.0 - dErrorProb;
                        int nThreshold = (int)(CDefinitions.RAND_MAX * dErrorProb);

                        for (int nRow = 0; nRow < m_nHeight; nRow++)
                        {
                            for (int nCol = 0; nCol < m_nWidth; nCol++)
                            {
                                int nRand = rand.Next(0, CDefinitions.RAND_MAX);
                                if (nRand > nThreshold)
                                {
                                    dest.SetPixel(nCol, nRow, Color.FromArgb((byte)nAmp, (byte)nAmp, (byte)nAmp));
                                }
                                else
                                {
                                    Color clr = SrcImage.GetPixel(nCol, nRow);
                                    dest.SetPixel(nCol, nRow, clr);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }
            return dest;
        }

        public Bitmap Plot3D(double dBeta)
        {
            Bitmap dest = new Bitmap(m_nWidth, m_nHeight);
            try
            {
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        dest.SetPixel(nCol, nRow, Color.Black);
                    }
                }

                // Deciding the step size
                int nStep = 0;
                if ((m_nWidth + m_nHeight - 1) / 2 <= CDefinitions.IMAX)
                    nStep = 1;
                else
                    nStep = 2;

                int nVal = CDefinitions.IMAX - (m_nWidth - 1) / nStep;

                // Converting the Angle into Radians
                dBeta = (dBeta * CDefinitions.PI) / 180.0;
                double dShift = CDefinitions.PI / 4.0;
                double dRTAngle = CDefinitions.PI / 2.0;

                double dCosBeta = System.Math.Cos(dBeta);
                double dSinBeta = System.Math.Sin(dBeta);
                double dSinFhift = System.Math.Sin(dBeta + dShift);

                double dSX = 1.414213562 * dSinFhift;
                double dSY = dCosBeta + (dCosBeta * dSinBeta / dSX);

                int nIMax = -1000;
                //Find the maximum intensity present in the image
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = 0; nCol < m_nWidth; nCol++)
                    {
                        byte clr = CDefinitions.CLR2GREY(SrcImage.GetPixel(nCol, nRow));
                        nIMax = (nIMax > clr) ? nIMax : clr;
                    }
                }

                double dXScale = (m_nWidth / (nIMax + 1));
                double dYScale = 1.0 / dSY;

                int[] nXScale_SinSxRay = new int[CDefinitions.SCALE];
                for (int nCnt = 0; nCnt < CDefinitions.SCALE; nCnt++)
                {
                    nXScale_SinSxRay[nCnt] = (int)(nCnt * dXScale * dSinBeta / dSX);
                }

                if (m_nWidth >= m_nHeight)
                    nIMax = m_nWidth;
                else
                    nIMax = m_nHeight;

                List<double> vCosRay = new List<double>();
                List<double> vSinRay = new List<double>();
                for (int nCnt = 0; nCnt < nIMax; nCnt++)
                {
                    vCosRay.Add(nCnt * dCosBeta);
                    vSinRay.Add(nCnt * dSinBeta);
                }

                int x1, x2, y1, nDeltaR, nDeltaG, nDeltaB;
                byte byTmpVal;
                for (int nRow = 0; nRow < m_nHeight; nRow++)
                {
                    for (int nCol = m_nWidth - 1; nCol >= 0; nCol--)
                    {
                        // Rotation About the Y-Axis
                        Color clr = SrcImage.GetPixel(nCol, nRow);
                        nDeltaR = nXScale_SinSxRay[clr.R];
                        nDeltaG = nXScale_SinSxRay[clr.G];
                        nDeltaB = nXScale_SinSxRay[clr.B];

                        x1 = (int)((vCosRay[nCol] + 0.5f) / dSX);
                        x2 = x1 + nDeltaR;

                        int nRowDest = 0;
                        if ((dRTAngle - dBeta) > 0.001f)
                        {
                            // Rotation About the X-Axis 
                            y1 = (int)((vCosRay[nRow] + vSinRay[x1]) * dYScale);
                            nRowDest = y1;
                        }
                        else
                            nRowDest = nCol;

                        byTmpVal = (byte)((System.Math.Abs(nCol - nRow) / nStep) + nVal);

                        for (int nCnt = x1; nCnt < x2; nCnt++)
                        {
                            dest.SetPixel(nCnt, nRowDest, Color.FromArgb(byTmpVal, byTmpVal, byTmpVal));
                        }

                        dest.SetPixel(x2, nRowDest, SrcImage.GetPixel(nCol, nRow));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }
            return dest;
        }

        public Bitmap Pixellate()
        {
            Bitmap dest = null;
            try
            {
                Pixellate filter = new Pixellate();
                dest = applyFilter(filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }

            return dest;
        }

        public Bitmap OilPainting(int nValue)
        {
            Bitmap dest = null;
            try
            {
                OilPainting filter = new OilPainting(nValue);
                dest = applyFilter(filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME);
            }

            return dest;
        }

        #endregion

        ///******************************************************************************************
        //Function		 : RandonStereo
        //Description		 : 
        //ReturnValue		 : 0 on success
        //*******************************************************************************************/
        //public Bitmap RandomStereogram(CString InFileName, ref CImageInfo imgInfo, 
        //							     float dpi)
        //{	
        //	int	m_nWidth, m_nHeight, lBMPSize;
        //	int		*SameLineRaster, *Zlut;
        //	int		Max, Min;
        //	int		Center, Left, Right;
        //	float	*ZVal, *Ztlut, Factor;
        //	float	DPI, E;
        //
        //	DPI = dpi;
        //	E = Round(2.5 * DPI);
        //
        //	if ((SameLineRaster = new int[m_nWidth]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		return(ERR_MEMERROR);
        //	}
        //	if ((Zlut = new int[SCALE]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] SameLineRaster;
        //		return(ERR_MEMERROR);
        //	}
        //	if ((ZVal = new float[SCALE]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] SameLineRaster;
        //		delete[] Zlut;
        //		return(ERR_MEMERROR);
        //	}
        //	if ((Ztlut = new float[SCALE]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] SameLineRaster;
        //		delete[] Zlut;
        //		delete[] ZVal;
        //		return(ERR_MEMERROR);
        //	}
        //	
        //
        //	// Finding the Maximum and Minimum Values
        //	Min = 256; Max = -1;
        //	for ( int nCnt = 0; nCnt < lBMPSize; nCnt++)
        //	{
        //		if (m_InRaster[nCnt] > Max)
        //			Max = m_InRaster[nCnt];
        //		if (m_InRaster[nCnt] < Min)
        //			Min = m_InRaster[nCnt];
        //	}
        //
        //	if ((Max - Min) != 0)
        //		Factor = 1.0 / (float) (Max - Min);
        //	else
        //		Factor = 999999999.999999;
        //
        //	for ( int nCnt = Min; nCnt <= Max; nCnt++)
        //	{
        //		ZVal[nCnt] = (float) (nCnt - Min) * Factor;
        //		Zlut[nCnt] = (Round((3.0 - ZVal[nCnt]) * E / (6.0 - ZVal[nCnt])));
        //		Ztlut[nCnt] = 2.0 * (6.0 - ZVal[nCnt]) / E;
        //	}
        //
        //	ibase = obase = 0;
        //	for (nRow = 0; nRow < m_nHeight; nRow++)
        //	{
        //		//////////////////////////////////////////////////////////////////
        //		// Each Pixel linked with itself
        //		for (nCol = 0; nCol < m_nWidth; nCol++)
        //			SameLineRaster[nCol] = nCol;	
        //
        //		for (nCol = 0; nCol < m_nWidth; nCol++)
        //		{
        //			Center = Zlut[*(m_InRaster + ibase + nCol)];
        //			Left = nCol - (Center + (Center & nRow & 1)) / 2;
        //			Right = Left + Center;
        //			if (0 <= Left && Right < m_nWidth)
        //			{	
        //				int		Visible; // 1st do hidden-surface removal
        //				int		t = 1;   // check pts (nCol-t, l) & (nCol+t, l) 
        //				float	zt;
        //	
        //				do
        //				{
        //					zt =  ZVal[*(m_InRaster + ibase + nCol)] + 
        //						  Ztlut[*(m_InRaster + ibase + nCol)]*t;
        //					Visible = ZVal[*(m_InRaster + ibase + nCol - t)] < zt 
        //							  && ZVal[*(m_InRaster + ibase + nCol + t)] < zt;
        //					t++;
        //				} while (Visible && (zt < 1));
        //	
        //				if (Visible)
        //				{	
        //					for ( int nCnt = SameLineRaster[Left]; nCnt != Left && nCnt != Right; nCnt = SameLineRaster[Left])
        //					{
        //						if (nCnt < Right)
        //							Left = nCnt;
        //						else
        //						{
        //							Left = Right;
        //							Right = nCnt;
        //						}
        //					}
        //					SameLineRaster[Left] = Right;					
        //				}
        //			}
        //		}
        //		ibase += m_nWidth; 
        //		for (nCol = m_nWidth-1; nCol >= 0; nCol--)
        //		{
        //			if (SameLineRaster[nCol] == nCol)
        //				imgInfo.SetRaster()[nCnt] =  + obase + nCol) = (rand() & 1) * 255;
        //			else
        //				imgInfo.SetRaster()[nCnt] =  + obase + nCol) = imgInfo.SetRaster()[nCnt] =  + obase + SameLineRaster[nCol]);
        //		}
        //		obase += m_nWidth;
        //	}
        //
        //
        //
        //	delete[] SameLineRaster;
        //	delete[] Zlut;
        //	delete[] Ztlut;
        //	delete[] ZVal;
        //
        //	return SrcImage;;
        //}
        //
        ///******************************************************************************************
        //Function		 : AutoStereogram
        //Description		 : 
        //ReturnValue		 : 0 on success
        //*******************************************************************************************/
        //public Bitmap AutoStereogram(CString InFileName1, CString InFileName2, 
        //							   ref CImageInfo imgInfo, float DPI)
        //{
        //	UCHR	*InRaster1, *InRaster2, *LineRaster1, *LineRaster2, *OutLineRaster;
        //	float	*ZLineRaster;
        //	int	lWidth1, lHeight1, lBits1, lBMPSize1;
        //	int	lWidth2, lHeight2, lBits2, lBMPSize2;
        //	int		ibase1, ibase2, Min, Max;
        //
        //	if (lWidth2 < (int) (1.25 * DPI))
        //	{
        //		AfxMessageBox("Depth per inches and Width of the random stereogram doesnot matched.", MB_OK|MB_ICONWARNING);
        //		return(PARAM_ERROR);
        //	}
        //
        //	if ((LineRaster1 = new UCHR[lWidth1]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] m_InRaster;
        //		delete[] InRaster1;
        //		delete[] InRaster2;
        //		return(ERR_MEMERROR);
        //	}
        //
        //	if ((LineRaster2 = new UCHR[lWidth1]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] m_InRaster;
        //		delete[] InRaster1;
        //		delete[] InRaster2;
        //		delete[] LineRaster1;
        //		return(ERR_MEMERROR);
        //	}
        //
        //	if ((ZLineRaster = new float[lWidth1]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] m_InRaster;
        //		delete[] InRaster1;
        //		delete[] InRaster2;
        //		delete[] LineRaster1;
        //		delete[] LineRaster2;
        //		return(ERR_MEMERROR);
        //	}
        //
        //	if ((OutLineRaster = new UCHR[lWidth1]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] m_InRaster;
        //		delete[] InRaster1;
        //		delete[] InRaster2;
        //		delete[] LineRaster1;
        //		delete[] LineRaster2;
        //		delete[] ZLineRaster;
        //		return(ERR_MEMERROR);
        //	}
        //	
        //	if ((m_OutRaster = new UCHR[lBMPSize1 + 12]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		delete[] m_InRaster;
        //		delete[] InRaster1;
        //		delete[] InRaster2;
        //		delete[] LineRaster1;
        //		delete[] LineRaster2;
        //		delete[] ZLineRaster;
        //		delete[] m_OutRaster;
        //		return(ERR_MEMERROR);
        //	}
        //	memset(m_OutRaster, 0x00, (lBMPSize1 + 12));
        //	memcpy(m_OutRaster, &lWidth1   , sizeof(int));
        //	memcpy(m_OutRaster+4, &lHeight1, sizeof(int));
        //	memcpy(m_OutRaster+8, &lBits1  , sizeof(int));
        //
        //	m_OutRaster += 12;
        //
        //	// Finding the Maximum and Minimum Values
        //	Min = 256; Max = -1;
        //	for ( int nCnt = 0; nCnt < lBMPSize1; nCnt++)
        //	{
        //		if (m_InRaster[nCnt] > Max)
        //			Max = m_InRaster[nCnt];
        //		if (m_InRaster[nCnt] < Min)
        //			Min = m_InRaster[nCnt];
        //	}
        //
        //	ibase1 = ibase2 = obase = 0;
        //	for (nRow = 0; nRow < lHeight1; nRow++)
        //	{
        //		memset(LineRaster1, 0x00, lWidth1);		
        //		memcpy((LineRaster2 + (lWidth1 - lWidth2)), (InRaster2 + ibase2), lWidth2);
        //		memcpy(LineRaster1, (InRaster1 + ibase1), lWidth1);
        //		
        //		// Call the function to convert the unsigned char Raster to float
        //		ConvertRasterToDouble(LineRaster1, ZLineRaster, 
        //										  lWidth1, Min, Max);
        //
        //		// Call the function
        //		DrawAutoStereogram(ZLineRaster, LineRaster2, OutLineRaster, 
        //									   lWidth1, nRow&1, DPI);
        //
        //		// Write the Raster to m_OutRaster
        // 		memcpy(m_OutRaster + obase, OutLineRaster, lWidth1);
        //
        //		ibase1 += lWidth1; ibase2 += lWidth2, obase += lWidth1;
        //	}
        //	delete[] LineRaster1;
        //	delete[] LineRaster2;
        //	delete[] ZLineRaster;
        //	delete[] OutLineRaster;
        //
        //	return SrcImage;;
        //}
        //
        //public Bitmap ConvertRasterToDouble(UCHR *LineRaster1, float *ZLineRaster, 
        //									   int lWidth1, int Min, int Max)
        //{
        //	float	Factor;
        //
        //	if ((Max - Min) != 0)
        //		Factor = 1.0 / (float) (Max - Min);
        //	else
        //		Factor = 999999999.999999;
        //	
        //	for ( int nCnt = 0; nCnt < lWidth1; nCnt++)
        //	{
        //		ZLineRaster[nCnt] = (float) ((LineRaster1[nCnt] - Min) * Factor);
        //	}
        //}
        //
        //int DrawAutoStereogram(float *ZLineRaster, UCHR *LineRaster2, 
        //									UCHR *OutLineRaster, int lWidth1, 
        //									int RowFlag, float DPI)
        //{
        //	int		nCnt, pixst, *SameLineRaster;
        //	int		Center, Left, Right;
        //	float	E;
        //
        //	E  = Round(2.5 * DPI);
        //
        //	if ((SameLineRaster = new int[lWidth1]) == NULL)
        //	{
        //		AfxMessageBox("Out of memory", MB_OK|MB_ICONSTOP);
        //		return(ERR_MEMERROR);
        //	}	
        //
        //	// Each Pixel linked with itself
        //	for ( int nCnt = 0; nCnt < lWidth1; nCnt++)
        //	{
        //		SameLineRaster[nCnt] = nCnt;
        //	}
        //
        //	for ( int nCnt = 0; nCnt < lWidth1; nCnt++)
        //	{
        //		Center = Round((1.0 - mu * ZLineRaster[nCnt]) * E / (2.0 - mu * ZLineRaster[nCnt]));
        //
        //		Left = nCnt - (Center + (Center&RowFlag)) / 2;
        //		Right = Left + Center;
        //		if (0 <= Left && Right < lWidth1)
        //		{	
        //			int		Visible;// 1st do hidden-surface removal
        //			int		t = 1;  // check pts (x-t, l) & (x+t, l)
        //			float	Zt;		// ZLineRaster-cood of ray at these 2 pts
        //
        //			do
        //			{
        //				Zt		= ZLineRaster[nCnt] + 2 * (2 - mu * ZLineRaster[nCnt]) * t / (mu * E);
        //				Visible = ZLineRaster[nCnt - t] < Zt && ZLineRaster[nCnt + t] < Zt;
        //				t++;
        //			} while (Visible && Zt < 1);
        //
        //			if (Visible)
        //			{	
        //				int k;
        //				for(k = SameLineRaster[Left]; k != Left && k != Right; k = SameLineRaster[Left])
        //				{
        //					if (k < Right)
        //						Left = k;
        //					else
        //					{
        //						Left = Right;
        //						Right = k;
        //					}
        //				}
        //				SameLineRaster[Left] = Right;
        //			}
        //		}
        //	}
        //
        //	pixst = lWidth1 - Round(E / 2.0);
        //	for ( int nCnt = lWidth1 - 1; nCnt >= 0; nCnt--)
        //	{
        //		if (SameLineRaster[nCnt] == nCnt)
        //			OutLineRaster[nCnt] = LineRaster2[pixst + nCnt%Round(E / 2.0)];
        //		else
        //			OutLineRaster[nCnt] = OutLineRaster[SameLineRaster[nCnt]];
        //	}
        //
        //	delete[] SameLineRaster;
        //	return SrcImage;;
        //}
    }
}
