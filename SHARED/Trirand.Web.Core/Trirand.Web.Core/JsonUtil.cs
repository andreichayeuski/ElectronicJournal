namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class JsonUtil
	{
		internal static string RenderClientSideEvent(string json, string jsName, string eventName)
		{
			string arg = (json.Length > 2) ? "," : "";
			string text = "";
			if (!string.IsNullOrEmpty(eventName))
			{
				text = $"{arg}{jsName}:{eventName}";
				return json.Insert(json.Length - 1, text);
			}
			return json;
		}
	}
}
