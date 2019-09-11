using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using EJ.Web.Filters;

namespace EJ.Web.Middlewares
{
    public class EJActionExceptionCatcherMiddleware
    {
        private readonly RequestDelegate _next;

        public EJActionExceptionCatcherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var ejActionFilter = new EJActionFilterAttribute();
            ejActionFilter.ErrorMessage = context.Items.TryGetValue("XSS_MiddlewareError", out var tmp) ? (string)tmp : null;

            ejActionFilter.OnResultExecuted(new ResultExecutedContext(
                new ActionContext(context, new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>(), new EmptyResult(), null));
            if (ejActionFilter.IsCanceled)
            {
                return;
            }
            else
            {
                if (ejActionFilter.ErrorMessage != null)
                {
                    return;
                }
                await _next(context);
            }
        }
    }
}
