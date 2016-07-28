using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace MedicineBoxRecorder.Helper
{
    public class ConfigHelper
    {
        /// <summary>
        /// 返回app.config文件中的appSettings配置节中的value项
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string strKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(AppConfig());
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + strKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return "";
            }
        }


        public static bool UpdateAppConfig(string strKey, string strValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppConfig());
                XmlNode node = doc.SelectSingleNode(@"//appSettings");
                XmlElement ele = (XmlElement)node.SelectSingleNode(@"//add[@key='" + strKey + "']");
                ele.SetAttribute("value", strValue);
                doc.Save(AppConfig());
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private static string AppConfig()
        {
            string strDirectoryPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), Process.GetCurrentProcess().MainModule.FileName.Replace("vshost.", "") + ".config");

            return strDirectoryPath;
        }


    }
}
