using System;
using System.Text;
using System.Xml;

namespace SHARED.Common.Utils
{
    public static class XmlHelper
    {
        public static string BeautifyXml(XmlDocument xmlDocument)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(stringBuilder, xmlWriterSettings))
            {
                xmlDocument.Save(writer);
            }
            return stringBuilder.ToString();
        }

        public static string BeautifyXml(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return BeautifyXml(xmlDocument);
        }
    }
}
