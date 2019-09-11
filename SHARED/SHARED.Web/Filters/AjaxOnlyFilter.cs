using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SHARED.Web.Extensions;

namespace SHARED.Web.Core.Filters
{
    public class AjaxOnlyFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.Filters.Any(t => t is AllowNonAjaxFilter))
            {
                if (!context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new RedirectToActionResult("AjaxOnly", "Home", new { area="" }, false);
                }
            }
            // все ок
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // ничего
        }
    }
}
