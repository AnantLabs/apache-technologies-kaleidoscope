using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Kaleidoscope
{
    class RegistryAccess
    {
        #region VARIABLES

        static private RegistryKey m_localMachine = Registry.LocalMachine;
        static private string strAppKey = "SOFTWARE\\ApacheTechnologies." + Application.ProductName;

        #endregion

        #region ACCESS_FUNCTIONS

        static public int ReadRegistryInt(string keyName)
        {
            int nValue = -1;
            try
            {
                RegistryKey key = m_localMachine.OpenSubKey(strAppKey);
                if (key != null && key.GetValue(keyName.ToLower()) != null)
                {
                    nValue = Convert.ToInt32(key.GetValue(keyName.ToLower()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return nValue;
        }

        static public string ReadRegistryString(string keyName)
        {
            string strValue = String.Empty;
            try
            {
                RegistryKey key = m_localMachine.OpenSubKey(strAppKey);
                if (key != null && key.GetValue(keyName.ToLower()) != null)
                {
                    strValue = key.GetValue(keyName.ToLower()).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return strValue;
        }

        static public bool WriteRegistry(string keyName, object keyValue)
        {
            try
            {
                RegistryKey key = m_localMachine.CreateSubKey(strAppKey);
                key.SetValue(keyName.ToLower(), keyValue);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.cAPP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
