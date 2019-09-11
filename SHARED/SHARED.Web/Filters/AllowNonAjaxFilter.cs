using Microsoft.AspNetCore.Mvc.Filters;

namespace SHARED.Web.Core.Filters
{
    public class AllowNonAjaxFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // все ок
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // ничего
        }
    }
}
