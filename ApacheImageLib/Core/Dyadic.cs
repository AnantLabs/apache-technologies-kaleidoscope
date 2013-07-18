using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Apache.ImageLib
{
    public class CDyadic : CImageInfo
    {
        #region COMPARE

        public struct CCompareResult
        {
            public double dSigPower;
            public double dNoisePower;
            public double dNoiseMean;
            public double dSbyN;
        };

        #endregion

        #region VARIABLES

        private int m_nWidth1 = 0;
        private int m_nHeight1 = 0;
        private int m_nWidth2 = 0;
        private int m_nHeight2 = 0;
        private Bitmap m_srcImage1 = null;
        private Bitmap m_srcImage2 = null;

        #endregion

        #region CTOR

        public CDyadic(Bitmap srcImage1, Bitmap srcImage2)
            : base(srcImage1)
        {
            try
            {
                m_nWidth1 = srcImage1.Width;
                m_nHeight1 = srcImage1.Height;
                m_nWidth2 = srcImage2.Width;
                m_nHeight2 = srcImage2.Height;

                m_srcImage1 = srcImage1;
                m_srcImage2 = srcImage2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region PRIVATE_FUNCTIONS

        void getNewCoordinates(CRect rectIn, ref CRect rectImg1, ref CRect rectImg2, ref CRect rectOut)
        {
            try
            {
                rectImg1.Left = -rectIn.Left; rectImg2.Left = -rectIn.Right;
                rectImg1.Right = m_nWidth1 - rectIn.Left - 1;
                rectImg2.Right = m_nWidth2 - rectIn.Right - 1;
                rectImg1.Top = -rectIn.Top; rectImg2.Top = -rectIn.Bottom;
                rectImg1.Bottom = m_nHeight1 - rectIn.Top - 1;
                rectImg2.Bottom = m_nHeight2 - rectIn.Bottom - 1;
                // Left
                if (rectIn.Right > rectIn.Left) rectOut.Left = rectImg2.Left;
                else rectOut.Left = rectImg1.Left;
                // Top
                if (rectIn.Bottom > rectIn.Top) rectOut.Top = rectImg2.Top;
                else rectOut.Top = rectImg1.Top;
                // Right
                if (rectImg2.Right > rectImg1.Right) rectOut.Right = rectImg2.Right;
                else rectOut.Right = rectImg1.Right;
                // Bottom
                if (rectImg2.Bottom > rectImg1.Bottom) rectOut.Bottom = rectImg2.Bottom;
                else rectOut.Bottom = rectImg1.Bottom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Color addPixel(CPixel p1, CPixel p2)
        {
            int R = p1.byR + p2.byR; R = (R > 255) ? 255 : R;
            int G = p1.byG + p2.byG; G = (G > 255) ? 255 : G;
            int B = p1.byB + p2.byB; B = (B > 255) ? 255 : B;
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        private Color addPixel(CPixel p1, CPixel p2, int nPercent1, int nPercent2)
        {
            float R = p1.byR * (nPercent1 / 100.0f) + p2.byR * (nPercent2 / 100.0f); R = (R > 255) ? 255 : R;
            float G = p1.byG * (nPercent1 / 100.0f) + p2.byG * (nPercent2 / 100.0f); G = (G > 255) ? 255 : G;
            float B = p1.byB * (nPercent1 / 100.0f) + p2.byB * (nPercent2 / 100.0f); B = (B > 255) ? 255 : B;
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        private Color subtractPixel(CPixel p1, CPixel p2)
        {
            int R = p1.byR - p2.byR; R = (R < 0) ? 0 : R;
            int G = p1.byG - p2.byG; G = (G < 0) ? 0 : G;
            int B = p1.byB - p2.byB; B = (B < 0) ? 0 : B;
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        private Color multiplyPixel(CPixel p1, CPixel p2, int nScale)
        {
            int R = p1.byR * p2.byR * nScale; R = (R > 255) ? 255 : R;
            int G = p1.byG * p2.byG * nScale; G = (G > 255) ? 255 : G;
            int B = p1.byB * p2.byB * nScale; B = (B > 255) ? 255 : B;
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        private Color dividePixel(CPixel p1, CPixel p2, int nScale)
        {
            float R = (p1.byR * nScale) / (1.0f + p2.byR); R = (R < 0) ? 0 : (R > 255) ? 255 : R;
            float G = (p1.byG * nScale) / (1.0f + p2.byG); G = (G < 0) ? 0 : (G > 255) ? 255 : G;
            float B = (p1.byB * nScale) / (1.0f + p2.byB); B = (B < 0) ? 0 : (B > 255) ? 255 : B;
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        private Color logicPixel(CPixel p1, CPixel p2, int nMask)
        {
            try
            {
                int R = 0, G = 0, B = 0;
                switch (nMask)
                {
                    case 0:
                        R = (p1.byR & p2.byR); R = (R < 0) ? 0 : (R > 255) ? 255 : R;
                        G = (p1.byG & p2.byG); G = (G < 0) ? 0 : (G > 255) ? 255 : G;
                        B = (p1.byB & p2.byB); B = (B < 0) ? 0 : (B > 255) ? 255 : B;
                        break;
                    case 1:
                        R = (p1.byR | p2.byR); R = (R < 0) ? 0 : (R > 255) ? 255 : R;
                        G = (p1.byG | p2.byG); G = (G < 0) ? 0 : (G > 255) ? 255 : G;
                        B = (p1.byB | p2.byB); B = (B < 0) ? 0 : (B > 255) ? 255 : B;
                        break;
                    case 2:
                        R = (p1.byR ^ p2.byR); R = (R < 0) ? 0 : (R > 255) ? 255 : R;
                        G = (p1.byG ^ p2.byG); G = (G < 0) ? 0 : (G > 255) ? 255 : G;
                        B = (p1.byB ^ p2.byB); B = (B < 0) ? 0 : (B > 255) ? 255 : B;
                        break;
                    default:
                        break;
                }

                return Color.FromArgb((int)R, (int)G, (int)B);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PUBLIC_FUNCTIONS

        public Bitmap Add(CRect rectIn, int nPercent1)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();
                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                int nOffset1 = 0, nOffset2 = 0;
                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                Bitmap dest = new Bitmap(nWidth, nHeight);
                Color[] R1 = new Color[nWidth]; Color[] R2 = new Color[nWidth];
                int nPercent2 = 100 - nPercent1;
                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {
                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }
                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        CPixel p1 = new CPixel(R1[nCol]);
                        CPixel p2 = new CPixel(R2[nCol]);

                        dest.SetPixel(nCol, nRow, addPixel(p1, p2, nPercent1, nPercent2));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Subtract(CRect rectIn)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();
                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                int nOffset1 = 0, nOffset2 = 0;
                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                Bitmap dest = new Bitmap(nWidth, nHeight);

                Color[] R1 = new Color[nWidth]; Color[] R2 = new Color[nWidth];
                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {
                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }
                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        CPixel p1 = new CPixel(R1[nCol]);
                        CPixel p2 = new CPixel(R2[nCol]);
                        dest.SetPixel(nCol, nRow, subtractPixel(p1, p2));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Multiply(CRect rectIn, int nScale)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();
                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                int nOffset1 = 0, nOffset2 = 0;
                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                Bitmap dest = new Bitmap(nWidth, nHeight);


                Color[] R1 = new Color[nWidth]; Color[] R2 = new Color[nWidth];
                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {
                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }

                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        CPixel p1 = new CPixel(R1[nCol]);
                        CPixel p2 = new CPixel(R2[nCol]);

                        dest.SetPixel(nCol, nRow, multiplyPixel(p1, p2, nScale));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Divide(CRect rectIn, int nScale)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();
                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                int nOffset1 = 0, nOffset2 = 0;
                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                Bitmap dest = new Bitmap(nWidth, nHeight);

                Color[] R1 = new Color[nWidth]; Color[] R2 = new Color[nWidth];
                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {

                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }

                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        CPixel p1 = new CPixel(R1[nCol]);
                        CPixel p2 = new CPixel(R2[nCol]);

                        dest.SetPixel(nCol, nRow, dividePixel(p1, p2, nScale));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CCompareResult Compare(CRect rectIn)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();
                int nOffset1 = 0, nOffset2 = 0;

                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                int nMeanDiff = 0, nMsvDiff = 0, nMsvSamp = 0;
                Color[] R1 = new Color[nWidth];
                Color[] R2 = new Color[nWidth];

                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {
                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            if (nCol < m_nWidth) R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }

                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            if (nCol < m_nWidth) R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        int s1 = CDefinitions.CLR2GREY(R1[nCol]);
                        int s2 = CDefinitions.CLR2GREY(R2[nCol]);
                        int nDiff = s1 - s2;

                        nMsvSamp += (s1 * s2);
                        nMeanDiff += nDiff;
                        nMsvDiff += (nDiff * nDiff);
                    }//END COL
                }//END ROW

                CCompareResult res = new CCompareResult();
                res.dSigPower = (double)nMsvSamp / (m_nWidth * m_nHeight);
                res.dNoisePower = (double)nMsvDiff / (m_nWidth * m_nHeight);
                res.dNoiseMean = (double)nMeanDiff / (m_nWidth * m_nHeight);

                if (nMsvDiff != 0)
                    res.dSbyN = 10.0 * Math.Log10(res.dSigPower / (res.dNoisePower + 1));
                else
                    res.dSbyN = 1000.0f;

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap Superpose(CRect rectIn)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();
                int nOffset1 = 0, nOffset2 = 0;

                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                Bitmap dest = new Bitmap(nWidth, nHeight);

                Color[] R1 = new Color[nWidth]; Color[] R2 = new Color[nWidth];

                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {

                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }

                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        CPixel p1 = new CPixel(R1[nCol]);
                        CPixel p2 = new CPixel(R2[nCol]);

                        dest.SetPixel(nCol, nRow, addPixel(p1, p2));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bitmap ImageLogic(CRect rectIn, int nMask)
        {
            try
            {
                CRect rectImg1 = new CRect(), rectImg2 = new CRect(), rectOut = new CRect();

                getNewCoordinates(rectIn, ref rectImg1, ref rectImg2, ref rectOut);

                int nWidth = rectOut.Right - rectOut.Left + 1;
                int nHeight = rectOut.Bottom - rectOut.Top + 1;

                int nOffset1 = 0, nOffset2 = 0;
                if (rectIn.Right - rectIn.Left > 0)
                {
                    nOffset1 = rectIn.Right - rectIn.Left;
                    nOffset2 = 0;
                }
                else
                {
                    nOffset1 = 0;
                    nOffset2 = rectIn.Left - rectIn.Right;
                }

                Bitmap dest = new Bitmap(nWidth, nHeight);

                Color[] R1 = new Color[nWidth]; Color[] R2 = new Color[nWidth];
                for (int nRow = rectOut.Top; nRow <= rectOut.Bottom; nRow++)
                {
                    if (nRow >= rectImg1.Top && nRow <= rectImg1.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth1; nCol++)
                        {
                            R1[nOffset1 + nCol] = m_srcImage1.GetPixel(nCol, nRow);
                        }
                    }

                    if (nRow >= rectImg2.Top && nRow <= rectImg2.Bottom)
                    {
                        for (int nCol = 0; nCol < m_nWidth2; nCol++)
                        {
                            R2[nOffset2 + nCol] = m_srcImage2.GetPixel(nCol, nRow);
                        }
                    }

                    for (int nCol = 0; nCol < nWidth; nCol++)
                    {
                        CPixel p1 = new CPixel(R1[nCol]);
                        CPixel p2 = new CPixel(R2[nCol]);
                        dest.SetPixel(nCol, nRow, logicPixel(p1, p2, nMask));
                    }
                }

                return dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Bitmap BackgroundCorrection(
        //                          int nChipFactor, BOOL bBinary)
        //{
        //    Bitmap dest = new Bitmap(m_nWidth1, m_nHeight1, 
        //        imgInfo1.GetImgInfo().GetBPP())) 
        //        

        //    //float fNormPixel = 0.0f;
        //    //float fNormPixelMax = 0.0f;

        //    //int nBase = 0;
        //    //for (int nRow = 0; nRow < m_nHeight1; nRow++) {
        //    //	for (nCol = 0; nCol < m_nWidth1; nCol++) {
        //    //		float P1 = imgInfo1.GetAvgRaster()[nRow*nBase + nCol];
        //    //		float P2 = imgInfo2.GetAvgRaster()[nRow*nBase + nCol];
        //    //		P2 = (P2 + 1.f > IMAX) ? IMAX : P2 + 1.f;
        //    //		if (P2 < 2.0f) continue;
        //    //		if (nChipFactor) {
        //    //			fNormPixel = (P1 / P2 > (float) nChipFactor) ? (float) nChipFactor : P1 / P2;
        //    //			if (bBinary)
        //    //				setpixel (nRow, nCol, imgOut, (int) (IMAX * (int) (fNormPixel + 0.5)));
        //    //			else
        //    //				setpixel (nRow, nCol, imgOut, (int) fNormPixel);
        //    //		}
        //    //		else {                    /*just get the maximum normalized pixel */
        //    //			fNormPixel = P1 / P2;
        //    //			if (fNormPixel > fNormPixelMax)
        //    //				fNormPixelMax = fNormPixel;
        //    //		}
        //    //	}
        //    //	nBase += m_nWidth1;
        //    //}
        //    ///*
        //    //* if there was no clipping factor specified,
        //    //* we just calculated the maximum normalized pixel value
        //    //* now scale the image according to fNormPixelMax
        //    //*/
        //    //if (!nChipFactor) {
        //    //	for (int nRow = 0; nRow < m_nHeight1; nRow++) {
        //    //		for (nCol = 0; nCol < m_nWidth1; nCol++) {
        //    //			if (nRow > 100 && nCol > 100)
        //    //				j = 0;
        //    //			P1 = imgInfo1.GetAvgRaster()[nRow*nBase + nCol];
        //    //			P2 = imgInfo2.GetAvgRaster()[nRow*nBase + nCol];
        //    //			P2 = (P2 + (float) 1 > (float) IMAX) ? (float) IMAX : P2 + (float) 1;
        //    //			fNormPixel = P1 / (P2 * fNormPixelMax);
        //    //			if (bBinary)
        //    //				setpixel (nRow, nCol, imgOut, (int) (IMAX * (int) (fNormPixel + 0.5)));
        //    //			else
        //    //				setpixel (nRow, nCol, imgOut, (fNormPixel > 1.0 ? IMAX : (int) (IMAX * fNormPixel)));
        //    //		}
        //    //	}
        //    //}

        //    return NOT_ERROR;
        //}

        #endregion
    }
}
