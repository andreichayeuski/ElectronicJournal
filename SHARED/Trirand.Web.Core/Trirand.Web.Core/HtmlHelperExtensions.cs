using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public static class HtmlHelperExtensions
	{
		public static TrirandNamespace Trirand(this IHtmlHelper helper)
		{
			return new TrirandNamespace();
		}
	}
}
