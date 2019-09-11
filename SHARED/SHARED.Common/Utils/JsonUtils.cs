using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SHARED.Common.Utils
{
    public static class JsonSettings
    {
        public static JsonSerializerSettings Default { get; set; }

        static JsonSettings()
        {
            Default =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
        }
    }

    public static class JsonUtils
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static byte[] ToJsonBytes(object obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static T FromJson<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str, JsonSettings.Default);
        }

        public static T FromJsonBytes<T>(byte[] bytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), JsonSettings.Default);
        }

    }
}