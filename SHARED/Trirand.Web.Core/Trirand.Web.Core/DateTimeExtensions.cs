using System;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class DateTimeExtensions
	{
		public static string ToJsonUTC(this DateTime dateTime)
		{
			return $"Date.UTC({dateTime.Year},{dateTime.Month - 1},{dateTime.Day},{dateTime.Hour},{dateTime.Minute},{dateTime.Second},{dateTime.Millisecond})";
		}
	}
}
