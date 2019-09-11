using System.Collections;
using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class DictionaryExtensions
	{
		internal static void AddIfNotEmpty(this Dictionary<string, object> d, string key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				d.Add(key, value);
			}
		}

		internal static void AddIfNotEmpty(this Dictionary<string, object> d, string key, string value, Hashtable savedValues)
		{
			if (!string.IsNullOrEmpty(value))
			{
				d.Add(key, value);
				savedValues.Add(key, value);
			}
		}

		internal static void AddIfCondition(this Dictionary<string, object> d, bool condition, string key, object value)
		{
			if (condition)
			{
				d.Add(key, value);
			}
		}
	}
}
