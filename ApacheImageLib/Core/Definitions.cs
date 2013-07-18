using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Apache.ImageLib
{
    public class CDefinitions
    {
        public enum REGION_TYPE
        {
            eBlack,
            eWhite
        };

        // Definitions
        public const int SCALE = 256;
        public const int GREYLEVELS = 256;
        public const byte IMIN = 0;
        public const byte IMAX = 255;
        public const float PI = 3.1415962f;
        public const int RAND_MAX = 0x7fff;
        public const double BOOSTBLURFACTOR = 90.0;
        public const byte EDGE = 255;
        public const byte NOEDGE = 0;
        public const byte POSSIBLE_EDGE = 128;
        public const byte CONNECTED = 1;
        public const byte UNCONNECTED = 1;
        public const int MAX_OBJECTS = 4096;

        public static byte CLR2GREY(Color clr)
        {
            return (byte)(clr.R * 0.3f + clr.G * 0.59f + clr.B * 0.11f);
        }

        public static byte CLR2AVR(Color clr)
        {
            return (byte)((clr.R + clr.G + clr.B) / 3.0f);
        }

        public static int MAX(int a, int b)
        {
            return (a > b) ? a : b;
        }

        public static int MIN(int a, int b)
        {
            return (a < b) ? a : b;
        }

        public static int MIN3(int a, int b, int c)
        {
            return MIN(MIN(a, b), c);
        }

        public static int MAX3(int a, int b, int c)
        {
            return MAX(MAX(a, b), c);
        }

        public static int MIN5(int a, int b, int c, int d, int e)
        {
            return MIN(MIN(MIN(a, b), MIN(c, d)), e);
        }

        public static int ABSOLUTE_1(int x)
        {
            return ((x) > 0 ? (x) : -(x));
        }

        //#define NEGATE(clr)				RGB(IMAX-GetRValue(clr),IMAX-GetGValue(clr),IMAX-GetBValue(clr))
        public static int OUTLINE_MAX = 8192;
        public static double SMALLNO = 0.0000001;
        //#define PixelsToBytes(n)		((n+7)/8)
        //#define BIG_SCALE				50000L
        //#define DIST(r1, g1, b1, r2, g2, b2)\
        //    (UINT)(3L * (UINT) ((r1) - (r2)) * (UINT)((r1) - (r2)) + \
        //        4L * (UINT) ((g1) - (g2)) * (UINT)((g1) - (g2)) + \
        //        2L * (UINT) ((b1) - (b2)) * (UINT)((b1) - (b2)))
    }

    public class CRect
    {
        private int m_nTop = 0;
        public int Top
        {
            get { return m_nTop; }
            set { m_nTop = value; }
        }

        private int m_nBottom = 0;
        public int Bottom
        {
            get { return m_nBottom; }
            set { m_nBottom = value; }
        }

        private int m_nLeft = 0;
        public int Left
        {
            get { return m_nLeft; }
            set { m_nLeft = value; }
        }

        private int m_nRight = 0;
        public int Right
        {
            get { return m_nRight; }
            set { m_nRight = value; }
        }

        private int m_nX = 0;
        public int X
        {
            get { return m_nX; }
            set { m_nX = value; }
        }

        private int m_nY = 0;
        public int Y
        {
            get { return m_nY; }
            set { m_nY = value; }
        }

        public void SetRect(int x1, int y1, int x2, int y2)
        {
            m_nLeft = x1;
            m_nTop = y1;
            m_nRight = x2;
            m_nBottom = y2;
        }
    }

    class CSortColors : IComparer<Color>
    {
        int IComparer<Color>.Compare(Color c1, Color c2)
        {
            byte I1 = CDefinitions.CLR2GREY(c1);
            byte I2 = CDefinitions.CLR2GREY(c2);

            if (I1 > I2)
            {
                return 1;
            }
            else if (I1 < I2)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class CPixel
    {
        public int byR, byG, byB;

        public CPixel()
        {
            byR = 0;
            byG = 0;
            byB = 0;
        }

        public CPixel(int R, int G, int B)
        {
            byR = R;
            byG = G;
            byB = B;
        }

        public CPixel(Color rgb)
        {
            byR = rgb.R;
            byG = rgb.G;
            byB = rgb.B;
        }

        public CPixel(CPixel p)
        {
            byR = p.byR; byG = p.byG; byB = p.byB;
        }

        public void Print()
        {
            Console.WriteLine("P(r={0}, g={1}, b={2})\n", byR, byG, byB);
        }
    };
}
