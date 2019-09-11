using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SHARED.Common.Utils;
using SHARED.Models;
using SHARED.Web.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EJ.Web.Filters
{
    public class EJActionFilterAttribute : ActionFilterAttribute
    {
        public bool IsCanceled { get; set; }
        public string ErrorMessage { get; set; }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == 500 || ErrorMessage != null)
            {
               /* var routeAction = Enum.TryParse(typeof(RouteActionEnum), context.HttpContext.Request.Form["routeAction"].FirstOrDefault(), true, out object action) ? (RouteActionEnum)action : default(RouteActionEnum);

                if (routeAction == RouteActionEnum.Edit || routeAction == RouteActionEnum.EditLosses ||
                    routeAction == RouteActionEnum.AddAdditionalEventInfo)
                {
                    context.HttpContext.Items.Add("XSS_MiddlewareError_Used", true);
                    context.HttpContext.Response.Clear();
                    context.HttpContext.Response.StatusCode = 200;*/
                    var jsonResponse = new JsonResponse
                    {
                        Type = JsonResponseType.Error,
                        Errors = Arr.Single(new KeyValuePair<string, string[]>("",
                            new[] { ErrorMessage ?? "Возникла ошибка при обработке запроса" })),
                        CallbackFunction = "editActiveForm",
                        CalbackParameters = new object[] { 1 }.ToList(),
                        Message = "Исправьте ошибки и попробуйте снова"
                    };

                    new JsonCamelCaseResult(jsonResponse).ExecuteResult(context);

                    if (context.Controller == null)
                    {
                        // значит вызов был до самого выполнения, тем самым сразу выходим
                        IsCanceled = true;
                    }
                //}
            }
        }
    }
}
