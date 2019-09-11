using System.Collections;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class HashbableExtensions
	{
		internal static void AddIfNotEmpty(this Hashtable hash, string key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				hash.Add(key, value);
			}
		}

		internal static void AddIfNotEmpty(this Hashtable hash, string key, string value, Hashtable savedValues)
		{
			if (!string.IsNullOrEmpty(value))
			{
				hash.Add(key, value);
				savedValues.Add(key, value);
			}
		}

		internal static void AddIfCondition(this Hashtable hash, bool condition, string key, object value)
		{
			if (condition)
			{
				hash.Add(key, value);
			}
		}
	}
}
