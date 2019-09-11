using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class JsonTools
	{
		public class MinifiedNumArrayConverter : JsonConverter
		{
			private Type dblArrayType = typeof(double[]);

			private Type decArrayType = typeof(decimal[]);

			private Type decType = typeof(decimal);

			private Type decNullType = typeof(decimal?);

			public override bool CanRead => false;

			private void dumpNumArray<T>(JsonWriter writer, T[] array)
			{
				for (int i = 0; i < array.Length; i++)
				{
					T val = array[i];
					string text = val.ToString();
					if (text.EndsWith(".0"))
					{
						writer.WriteRawValue(text.Substring(0, text.Length - 2));
					}
					else if (text.Contains("."))
					{
						writer.WriteRawValue(text.TrimEnd('0'));
					}
					else
					{
						writer.WriteRawValue(text);
					}
				}
			}

			private void dumpNum<T>(JsonWriter writer, T value)
			{
				string text = value.ToString();
				if (text.EndsWith(".0"))
				{
					writer.WriteRawValue(text.Substring(0, text.Length - 2));
				}
				else if (text.Contains("."))
				{
					writer.WriteRawValue(text.TrimEnd('0'));
				}
				else
				{
					writer.WriteRawValue(text);
				}
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				Type type = value.GetType();
				if (type == dblArrayType)
				{
					writer.WriteStartArray();
					dumpNumArray(writer, (double[])value);
					writer.WriteEndArray();
				}
				else if (type == decArrayType)
				{
					writer.WriteStartArray();
					dumpNumArray(writer, (decimal[])value);
					writer.WriteEndArray();
				}
				else
				{
					if (!(type == decType) && !(type == decNullType))
					{
						throw new NotImplementedException();
					}
					dumpNum(writer, (decimal)value);
				}
			}

			public override bool CanConvert(Type objectType)
			{
				if (objectType == dblArrayType || objectType == decArrayType || objectType == decType || objectType == decNullType)
				{
					return true;
				}
				return false;
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}

			//public MinifiedNumArrayConverter()
			//	: this()
			//{
			//}
		}

		public static string ObjectToJson(object obj)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(obj.GetType());
			MemoryStream memoryStream = new MemoryStream();
			dataContractJsonSerializer.WriteObject((Stream)memoryStream, obj);
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			return Encoding.UTF8.GetString(array);
		}

		public static object JsonToObject(string jsonString, object obj)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(obj.GetType());
			MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
			obj = dataContractJsonSerializer.ReadObject((Stream)stream);
			return obj;
		}

		public static T JsonToObject<T>(string jsonString)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
			{
				return (T)dataContractJsonSerializer.ReadObject((Stream)stream);
			}
		}

		public static string ObjectToJson2(object value)
		{
			return ObjectToJson2(value, clearLastZero: false);
		}

		public static string ObjectToJson2(object value, bool clearLastZero)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Expected O, but got Unknown
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Expected O, but got Unknown
			Type type = value.GetType();
			JsonSerializer val = new JsonSerializer();
			val.ObjectCreationHandling=ObjectCreationHandling.Replace;
			val.MissingMemberHandling=MissingMemberHandling.Ignore;
			val.ReferenceLoopHandling=ReferenceLoopHandling.Ignore;
			val.ContractResolver=new CamelCasePropertyNamesContractResolver();
			JsonConverterCollection converters = val.Converters;
			StringEnumConverter val2 = new StringEnumConverter();
			val2.CamelCaseText=true;
			((Collection<JsonConverter>)converters).Add(val2);
			((Collection<JsonConverter>)val.Converters).Add(new StringEnumConverter());
			IsoDateTimeConverter val3 = new IsoDateTimeConverter();
			val3.DateTimeFormat="yyyy-MM-dd";
			((Collection<JsonConverter>)val.Converters).Add(val3);
			val.Formatting=Formatting.None;
		    val.NullValueHandling = NullValueHandling.Ignore;
			if (clearLastZero)
			{
				((Collection<JsonConverter>)val.Converters).Add(new MinifiedNumArrayConverter());
			}
			StringWriter stringWriter = new StringWriter();
			JsonTextWriter val4 = new JsonTextWriter((TextWriter)stringWriter);
			val4.Formatting=Formatting.None;
			val4.QuoteChar='"';
			val4.QuoteName=false;
			val.Serialize(val4, value);
			string text = stringWriter.ToString();
			val4.Close();
			stringWriter.Close();
			return text.Replace("coreCharts", "echarts", StringComparison.CurrentCultureIgnoreCase);
		}

		public static T JsonToObject2<T>(string jsonText)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			JsonSerializer val = new JsonSerializer();
			val.NullValueHandling=NullValueHandling.Ignore;
			val.ObjectCreationHandling=ObjectCreationHandling.Replace;
			val.MissingMemberHandling=MissingMemberHandling.Ignore;
			val.ReferenceLoopHandling=ReferenceLoopHandling.Ignore;
			StringReader stringReader = new StringReader(jsonText);
			JsonTextReader val2 = new JsonTextReader((TextReader)stringReader);
			T result = default(T);
			try
			{
				result = (T)val.Deserialize(val2, typeof(T));
				return result;
			}
			catch
			{
			}
			finally
			{
				val2.Close();
			}
			return result;
		}

		public static string ListToJson(IEnumerable array)
		{
			string text = "[";
			foreach (object item in array)
			{
				text = text + ObjectToJson(item) + ",";
			}
			int length = text.LastIndexOf(',');
			string str = text.Substring(0, length);
			return str + "]";
		}

		public string DataTableToJson(string jsonName, DataTable dt, string strTotal = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[{\"TotalCount\":\"" + strTotal + "\",\"Head\":[");
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				stringBuilder.Append("{\"ColumnHead\":\"" + dt + dt.Columns[i].ColumnName + "\"}");
				if (i < dt.Columns.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("],");
			stringBuilder.Append("\"" + jsonName + "\":[");
			if (dt.Rows.Count > 0)
			{
				for (int j = 0; j < dt.Rows.Count; j++)
				{
					stringBuilder.Append("{");
					for (int k = 0; k < dt.Columns.Count; k++)
					{
						stringBuilder.Append("\"" + dt.Columns[k].ColumnName.ToString() + "\":\"" + dt.Rows[j][k].ToString() + "\"");
						if (k < dt.Columns.Count - 1)
						{
							stringBuilder.Append(",");
						}
					}
					stringBuilder.Append("}");
					if (j < dt.Rows.Count - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			stringBuilder.Append("]}]");
			return stringBuilder.ToString();
		}
	}
}
