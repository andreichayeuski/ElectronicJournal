using System;
using System.IO;
using System.Xml;

namespace SHARED.Common.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Преобразует XmlDocument в строку
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static String ToTextString(this XmlDocument xmlDoc)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}
