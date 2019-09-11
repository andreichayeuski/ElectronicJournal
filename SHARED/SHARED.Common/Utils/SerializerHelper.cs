using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SHARED.Common.Utils
{
    public class SerializerHelper
    {
        private static JsonSerializerSettings jsonSerializerSettingsToUTC = new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc };

        /// <summary>
        ///     Сериализует сообщение в АСИВ в xml
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string SerializeToXML(object message)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(message.GetType());
                var ms = new MemoryStream();

                var xmlnsEmpty = new XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");

                var sb = new StringBuilder();
                var w = new StringWriter(sb, CultureInfo.InvariantCulture);
                xmlSerializer.Serialize(w, message, xmlnsEmpty);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }


        public static string SerializeToXML(object message, Encoding encoding, bool needNamespace = false, bool needIntend=false)
        {
            var xmlSerializer = new XmlSerializer(message.GetType());

            var xmlnsEmpty = new XmlSerializerNamespaces();
            xmlnsEmpty.Add("", "");
            
            try
            {
                var builder = new StringBuilder();
                using (var writer = new StringWriterWithEncoding(builder, encoding))
                using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = needIntend }))
                {
                    xmlSerializer.Serialize(xmlWriter, message, xmlnsEmpty);
                }

                return builder.ToString();

            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }


        /// <summary>
        ///     Сериализует сообщение в АСИВ в xml
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string SerializeToXML(object message, String encodingName)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(message.GetType());
                var ms = new MemoryStream();

                var xmlnsEmpty = new XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");

                xmlSerializer.Serialize(ms, message, xmlnsEmpty);

                var xmlDocument = new XmlDocument();
                ms.Position = 0;
                xmlDocument.Load(ms);


                if (xmlDocument.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    var xmlDeclaration = (XmlDeclaration)xmlDocument.FirstChild;
                    xmlDeclaration.Encoding = encodingName;
                    xmlDeclaration.Standalone = "yes";
                }

                return xmlDocument.OuterXml;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }


        public static string SerializeToXMLWithoutStandalone(object message, String encodingName)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(message.GetType());
                var ms = new MemoryStream();

                var xmlnsEmpty = new XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");

                xmlSerializer.Serialize(ms, message, xmlnsEmpty);

                var xmlDocument = new XmlDocument();
                ms.Position = 0;
                xmlDocument.Load(ms);


                if (xmlDocument.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    var xmlDeclaration = (XmlDeclaration)xmlDocument.FirstChild;
                    xmlDeclaration.Encoding = encodingName;
                }

                return xmlDocument.OuterXml;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        /// <summary>
        ///     Преобразует объект в массив byte[].
        /// </summary>
        public static byte[] ObjectToByteArray(object _Object)
        {
            try
            {
                var memoryStream = new MemoryStream();
                var binaryFormatter = new BinaryFormatter();

                binaryFormatter.Serialize(memoryStream, _Object);
                
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static T ByteArrayToObject<T>(byte[] bytes)
        {
            try
            {
                var memoryStream = new MemoryStream();
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Position = 0;
                var binaryFormatter = new BinaryFormatter();

                var result = (T)binaryFormatter.Deserialize(memoryStream);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Десериализует строку XML в объект заданного типа.
        /// </summary>
        public static T DeserializeXmlToObject<T>(string xml)
        {
            if (xml == null)
                return default(T);

            if (xml == string.Empty)
                return (T)Activator.CreateInstance(typeof(T));

            var reader = new StringReader(xml);
            var sr = new XmlSerializer(typeof(T));
            
            return (T)sr.Deserialize(reader);
        }


        public static T DeserializeXmlToObject<T>(byte[] data)
        {
            if (data.Length == 0)
                return (T)Activator.CreateInstance(typeof(T));

            using (var memStream=new MemoryStream(data))
            using (XmlReader xmlReader = XmlReader.Create(memStream))
            {
                var sr = new XmlSerializer(typeof(T));
                return (T)sr.Deserialize(xmlReader);
            }
            
        }

        public static string SerializeJSon<T>(T t)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            DataContractJsonSerializerSettings s = new DataContractJsonSerializerSettings();
            ds.WriteObject(stream, t);
            string jsonString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return jsonString;
        }
        
        public static T DeserializeJson<T>(string json) where T : class
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                return JsonSerializer.Create().Deserialize(reader, typeof(T)) as T;
        }

        /// <summary>
        /// Сериализует объект в набор байт используя Json-сериализацию (Newtonsoft.Json)
        /// </summary>
        /// <param name="_Object">объект</param>
        /// <returns></returns>
        public static byte[] ObjectToJsonBytes(object _Object)
        {
            var json = JsonConvert.SerializeObject(_Object, jsonSerializerSettingsToUTC);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Десериализует набор байт, представляющий из себя json-строку (UTF-8) в объект (Newtonsoft.Json)
        /// </summary>
        /// <param name="bytes">объект</param>
        /// <param name="type">тип</param>
        /// <returns></returns>
        public static object JsonByteToObject(byte[] bytes, Type type)
        {
            return Convert.ChangeType(JsonConvert.DeserializeObject(Encoding.UTF8.GetString(bytes), type, jsonSerializerSettingsToUTC), type);
        }

        /// <summary>
        /// Десериализует набор байт, представляющий из себя json-строку (UTF-8) в объект (Newtonsoft.Json)
        /// </summary>
        /// <param name="bytes">объект</param>
        /// <returns></returns>
        public static T JsonByteToObject<T>(byte[] bytes)
        {
           return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), jsonSerializerSettingsToUTC);
        }

        /// <summary>
        /// тоже самое только с передаваемыми настойками
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns></returns>
        public static T JsonByteToObject<T>(byte[] bytes, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), jsonSerializerSettings);
        }
    }


    internal class StringWriterWithEncoding : StringWriter
    {
        private Encoding _encoding;

        public StringWriterWithEncoding()
            : base() { }

        public StringWriterWithEncoding(IFormatProvider formatProvider)
            : base(formatProvider) { }

        public StringWriterWithEncoding(StringBuilder sb)
            : base(sb) { }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider)
            : base(sb, formatProvider) { }


        public StringWriterWithEncoding(Encoding encoding)
            : base()
        {
            _encoding = encoding;
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            _encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            _encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
            : base(sb, formatProvider)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return _encoding ?? base.Encoding;
            }
        }
    }
}
