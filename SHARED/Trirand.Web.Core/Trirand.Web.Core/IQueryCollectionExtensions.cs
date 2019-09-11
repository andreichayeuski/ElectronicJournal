using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class IQueryCollectionExtensions
	{
		internal static Dictionary<string, string> ToDictionary(this IQueryCollection list)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string key in list.Keys)
			{
				dictionary.Add(key, list[key]);
			}
			return dictionary;
		}
	}
}
