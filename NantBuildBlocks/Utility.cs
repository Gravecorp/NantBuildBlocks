using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;

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
                    string fileName = Utility.EncodeTo64UTF8(element.ToString()) + Constants.DEFAULTEXTENSION;
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

        public static bool DeleteBlock(BasicElement block)
        {
            string fileName = Utility.EncodeTo64UTF8(block.ToString()) + Constants.DEFAULTEXTENSION;
            string filePath = Path.Combine(Constants.DEFAULTSAVEPATH, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return (!File.Exists(filePath));
        }

        public static BasicElement LoadBuildFile(string path)
        {
            BasicElement ret = new BasicElement("");
            XmlDocument doc = new XmlDocument();
            using (Stream stream = File.OpenRead(path))
            {
                doc.Load(stream);
            }
            ret = ParseBuildFile(doc);
            return (ret);
        }

        public static BasicElement ParseBuildFile(XmlDocument doc)
        {
            BasicElement root = new BasicElement();
            root.ElementName = doc.DocumentElement.Name;
            if (doc.DocumentElement.HasAttributes)
            {
                foreach (XmlAttribute attribute in doc.DocumentElement.Attributes)
                {
                    BasicProperty prop = new BasicProperty();
                    prop.Name = attribute.Name;
                    prop.Value = attribute.Value;
                    root.Properties.Add(prop);
                }
            }
            if (doc.DocumentElement.HasChildNodes)
            {
                foreach (XmlNode child in doc.DocumentElement.ChildNodes)
                {
                    root.ChildElements.Add(ParseBuildElement(child));
                }
            }
            return (root);
        }

        public static BasicElement ParseBuildElement(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.CDATA)
            {
                CDataElement  cdata = new CDataElement();
                CDataValue prop = new CDataValue();
                prop.Value = node.InnerText;
                cdata.Properties.Add(prop);
                return (cdata);
            }
            if (node.NodeType == XmlNodeType.Comment)
            {
                BasicElement comment = new BasicElement("Comment");
                BasicProperty prop = new BasicProperty();
                prop.Name = "";
                prop.Value = node.InnerText;
                comment.Properties.Add(prop);
                return (comment);
            }
            BasicElement element = new BasicElement();
            element.ElementName = node.Name;
            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    BasicProperty prop = new BasicProperty();
                    prop.Name = attribute.Name;
                    prop.Value = attribute.Value;
                    element.Properties.Add(prop);
                }
            }
            if (node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    element.ChildElements.Add(ParseBuildElement(child));
                }
            }
            return (element);
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
