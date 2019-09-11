using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SHARED.Common.Extensions
{
    public static class SerializerExtensions
    {
        private static readonly JsonSerializerSettings JsonSerializerSettingsToUtc = new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc };

        public static byte[] ToJsonBytes(this object _Object)
        {
            var json = JsonConvert.SerializeObject(_Object, JsonSerializerSettingsToUtc);
            return Encoding.UTF8.GetBytes(json);
        }

        public static string ToJsonString(this object _Object)
        {
            return JsonConvert.SerializeObject(_Object, JsonSerializerSettingsToUtc);
        }

        public static T FromJsonString<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
        }

        public static byte[] ToJsonBytesWithLocalTime(this object _Object)
        {
            var json = JsonConvert.SerializeObject(_Object, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            return Encoding.UTF8.GetBytes(json);
        }

        public static T JsonByteToObject<T>(this byte[] bytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), JsonSerializerSettingsToUtc);
        }

        public static T JsonByteToObjectWithLocalTime<T>(this byte[] bytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
        }
    }
}
