using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SHARED.Web.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SHARED.Web.Extensions
{
    public static class ControllerExtension
    {
        public static IHtmlHelper GetHtmlHelper(this Controller controller)
        {
            var htmlHelper = (IHtmlHelper)controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(IHtmlHelper));
            var viewContextAware = new ViewContextAware(htmlHelper);
            var actionContext = new ActionContext(controller.HttpContext, controller.RouteData, new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            var viewContext = new ViewContext(actionContext, new FakeView(), controller.ViewData, controller.TempData, TextWriter.Null, new HtmlHelperOptions());
            (viewContextAware as IViewContextAware).Contextualize(viewContext);
            return viewContextAware.Html;
        }

        /// <summary>
        /// Render a partial view to string. https://www.surinderbhomra.com/Blog/Post/2018/09/02/ASPNET-Core-Render-Partial-View-To-String-Outside-Controller-Context
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="controller"></param>
        /// <param name="viewNamePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<string> RenderViewToStringAsync<TModel>(this Controller controller, string viewNamePath, TModel model)
        {
            if (string.IsNullOrEmpty(viewNamePath))
                viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

            controller.ViewData.Model = model;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                    ViewEngineResult viewResult = null;

                    if (viewNamePath.EndsWith(".cshtml"))
                        viewResult = viewEngine.GetView(viewNamePath, viewNamePath, false);
                    else
                        viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

                    if (!viewResult.Success)
                        return $"A view with the name '{viewNamePath}' could not be found";

                    ViewContext viewContext = new ViewContext(
                        controller.ControllerContext,
                        viewResult.View,
                        controller.ViewData,
                        controller.TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);

                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception exc)
                {
                    return $"Failed - {exc.Message}";
                }
            }
        }

        public class FakeView : IView
        {
            public string Path => throw new NotImplementedException();

            public void Render(ViewContext viewContext, TextWriter writer)
            {
                throw new InvalidOperationException();
            }

            public Task RenderAsync(ViewContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
