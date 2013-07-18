using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Apache.ImageLib.TwainLib;

namespace Kaleidoscope
{
    static class Program
    {
        #region STATICS

        public static string cAPP_NAME = Application.ProductName + " " + Application.ProductVersion;

        private static bool CheckForOpenProcesses(string strProcessName, int count)
        {
            Process[] p = Process.GetProcessesByName(strProcessName);
            if (p.Length > count)
            {
                DialogResult dr = MessageBox.Show("There are other " + strProcessName + " instances running.\n" +
                    "Please close it first...",
                    cAPP_NAME,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private static CConsole theConsole = null;
        public static CConsole GetConsole()
        {
            return theConsole;
        }

        #endregion

        #region MAIN_ENTRY

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (CTwain.ScreenBitDepth < 15)
                {
                    MessageBox.Show("The application required high/true color video mode.",
                        cAPP_NAME,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!CheckForOpenProcesses("Kaleidoscope", 1)) return;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                theConsole = new CConsole();
                Application.Run(theConsole);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
