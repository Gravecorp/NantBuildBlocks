using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace NantBuildBlocks
{
    class Utility
    {

        public static void SaveBlocks(BasicElement[] elementArray)
        {
            if (elementArray.Length > 0)
            {
                if (!Directory.Exists(Constants.DEFAULTSAVEPATH))
                {
                    Directory.CreateDirectory(Constants.DEFAULTSAVEPATH);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(BasicElement));
                foreach (BasicElement element in elementArray)
                {
                    string fileName = Utility.EncodeTo64UTF8(element.ToString()) + ".block";
                    string savePath = Path.Combine(Constants.DEFAULTSAVEPATH, fileName);
                    using (Stream stream = File.Open(savePath, FileMode.Create))
                    {
                        serializer.Serialize(stream, element);
                    }
                }
            }
        }

        public static BasicElement[] LoadBlocks()
        {
            if (Directory.Exists(Constants.DEFAULTSAVEPATH))
            {
                string[] files = Directory.GetFiles(Constants.DEFAULTSAVEPATH, Constants.SAVEDBLOCKSSEARCHPATTERN);
                BasicElement[] returnArray = new BasicElement[files.Length];
                XmlSerializer serializer = new XmlSerializer(typeof(BasicElement));
                for (int i = 0; i < files.Length; i++)
                {
                    using (Stream stream = File.OpenRead(files[i]))
                    {
                        returnArray[i] = (BasicElement)serializer.Deserialize(stream);
                    }
                }
                return (returnArray);
            }
            else
            {
                BasicElement[] returnArray = new BasicElement[0];
                return (returnArray);
            }

        }

        public static string EncodeTo64UTF8(string m_enc)
        {
            byte[] toEncodeAsBytes =
            Encoding.UTF8.GetBytes(m_enc);
            string returnValue =
            Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string m_enc)
        {
            byte[] encodedDataAsBytes =
            Convert.FromBase64String(m_enc);
            string returnValue =
            Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
