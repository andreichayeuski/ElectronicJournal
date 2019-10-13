using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SHARED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SHARED.Common.Extensions;
using SHARED.Common.Utils;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SHARED.Web.Other;

namespace SHARED.Web.Extensions
{
    public static class HtmlHelpers
    {
        #region Grid
        public static IHtmlContent BasicJqGrid<T>(this IHtmlHelper<T> helper, JqGridSettingsModel model)
        {
            return helper.Partial("~/Views/Shared/_BasicJqGrid.cshtml", model);
        }
        #endregion

        #region Select2 Autcomplete
        public static IHtmlContent Autocomplete<T>(this IHtmlHelper<T> helper, string name, string action, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocomplete(helper, name, urlHelper.Action(action), resultsPerPage, new { });
        }

        public static IHtmlContent Autocomplete<T>(this IHtmlHelper<T> helper, string name, string action, object routedValues, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocomplete(helper, name, urlHelper.Action(action, routedValues), resultsPerPage, new { });
        }

        public static IHtmlContent Autocomplete<T>(this IHtmlHelper<T> helper, string name, string action, object routedValues, object htmlAttributes, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocomplete(helper, name, urlHelper.Action(action, routedValues), resultsPerPage, htmlAttributes);
        }

        public static IHtmlContent Autocomplete<T>(this IHtmlHelper<T> helper, string name, string action, object routedValues, object htmlAttributes, SelectListItem[] defaultItems, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocomplete(helper, name, urlHelper.Action(action, routedValues), resultsPerPage, htmlAttributes, defaultItems);
        }

        public static IHtmlContent AutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action), resultsPerPage, new { });
        }

        public static IHtmlContent AutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, string controller, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action, controller), resultsPerPage, new { });
        }

        public static IHtmlContent AutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, string controller, object routedValues, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action, controller, routedValues), resultsPerPage, new {});
        }

        public static IHtmlContent AutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, object htmlAttributes, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action), resultsPerPage, htmlAttributes);
        }

        public static IHtmlContent AutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, object htmlAttributes, SelectListItem[] defaultItems, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action), resultsPerPage, htmlAttributes, defaultItems);
        }

        public static IHtmlContent AutocompleteFor<T, TResult,TKey, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, string controller, IDictionary<TKey, TValue> htmlAttributes, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action, controller), resultsPerPage, htmlAttributes);
        }
        

        public static IHtmlContent AutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string action, string controller, object routedValues, object htmlAttributes, SelectListItem[] defaultItems= null, int resultsPerPage = 15)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            return CreateAutocompleteFor(helper, expr, urlHelper.Action(action, controller, routedValues), resultsPerPage, htmlAttributes, defaultItems);
        }

        private static IHtmlContent CreateAutocompleteFor<T, TResult>(this IHtmlHelper<T> helper, Expression<Func<T, TResult>> expr, string url, int resultsPerPage, object htmlAttributes, SelectListItem[] defaultItems = null)
        {
            var expresionProvider = helper.ViewContext.HttpContext.RequestServices
                .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

            var name = expresionProvider.GetExpressionText(expr);
            return CreateAutocomplete(helper, name, url, resultsPerPage, htmlAttributes, defaultItems);
        }

        private static IHtmlContent CreateAutocomplete<T>(this IHtmlHelper<T> helper, string name, string url, int resultsPerPage, object htmlAttributes, SelectListItem[] defaultItems = null)
        {
            var values = htmlAttributes.ToDictionary();
            foreach (var valuesKey in values.Keys.ToArray())
            {
                if (valuesKey.StartsWith("data_"))
                {
                    var value = values[valuesKey];
                    values.Remove(valuesKey);
                    var newKey = valuesKey.Replace("_", "-");
                    values.Add(newKey, value);
                }
            }
            values.Add("data-select2-autocomplete", true);
            values.Add("data-url", url);
            values.Add("data-select2-resultsPerPage", resultsPerPage);

            return helper.DropDownList(name, defaultItems ?? Arr.Empty<SelectListItem>(), htmlAttributes: values);
        }

        #endregion

        #region AjaxLink

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName)
        {
            return AjaxLinkInner(helper, linkText, actionName, null, new AjaxLink());
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, null, new AjaxLink(), htmlAttributes: htmlAttributes);
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, string controller, object routedValues, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, controller, new AjaxLink(), routedValues, htmlAttributes);
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, string controller, AjaxLink ajaxLinkProperties, object routedValues, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, controller, ajaxLinkProperties, routedValues, htmlAttributes);
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, object routedValues, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, null, new AjaxLink(), routedValues, htmlAttributes);
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, AjaxLink ajaxLinkProperties)
        {
            return AjaxLinkInner(helper, linkText, actionName, null, ajaxLinkProperties);
        }
        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, AjaxLink ajaxLinkProperties, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, null, ajaxLinkProperties, htmlAttributes: htmlAttributes);
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, AjaxLink ajaxLinkProperties, object routedValues, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, null, ajaxLinkProperties, routedValues, htmlAttributes: htmlAttributes);
        }

        public static IHtmlContent AjaxLink(this IHtmlHelper helper, string linkText, string actionName, string actionController, AjaxLink ajaxLinkProperties, object htmlAttributes)
        {
            return AjaxLinkInner(helper, linkText, actionName, actionController, ajaxLinkProperties, htmlAttributes: htmlAttributes);
        }

        private static IHtmlContent AjaxLinkInner(this IHtmlHelper helper, string linkText, string actionName,
            string actionController, AjaxLink ajaxlinkProperties,
            object routedValues = null,
            object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext);
            var url = urlHelper.Action(actionName, actionController, routedValues ?? new RouteValueDictionary());

            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes ?? new object());
            customAttributes.Add("data-ajax-url", url);
            if (ajaxlinkProperties != null)
            {
                customAttributes.Add("data-dialog-title", ajaxlinkProperties.DialogTitle);
                customAttributes.Add("data-dialog-width", ajaxlinkProperties.DialogWidth + "px");
            }

            var tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttributes(customAttributes);
            tagBuilder.InnerHtml.AppendHtmlLine(linkText);

            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return new HtmlString(writer.ToString());
            }
        }
        #endregion

        #region UserMessage
        public static IHtmlContent UserMessage(this IHtmlHelper helper, string linkText, UserMessage messageProperties, object htmlButtonAttributes = null)
        {
            var tagBuilder = new TagBuilder("a");
            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlButtonAttributes ?? new object());
            customAttributes.Add("data-message-dismiss", "data-message-dismiss");
            customAttributes.Add("data-message-target", "#" + messageProperties.MessageIdentifier);
            tagBuilder.MergeAttributes(customAttributes);
            tagBuilder.InnerHtml.AppendHtmlLine(linkText);
            helper.ViewBag.Button = tagBuilder.ToString();

            return helper.Partial("_Message", messageProperties);
        }

        public static IHtmlContent DismissButton(this IHtmlHelper helper, string text, object htmlButtonAttributes = null)
        {
            var tagBuilder = new TagBuilder("a");
            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlButtonAttributes ?? new object());
            customAttributes.Add("data-message-dismiss", "data-message-dismiss");
            tagBuilder.MergeAttributes(customAttributes);
            tagBuilder.AddCssClass("btn btn-primary");
            tagBuilder.InnerHtml.AppendHtmlLine(text);

            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return new HtmlString(writer.ToString());
            }
        }

        public static IHtmlContent ApprovalButton(this IHtmlHelper helper, string text, object htmlButtonAttributes = null)
        {
            var tagBuilder = new TagBuilder("a");
            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlButtonAttributes ?? new object());
            tagBuilder.MergeAttributes(customAttributes);
            tagBuilder.AddCssClass("btn btn-primary");
            tagBuilder.InnerHtml.AppendHtmlLine(text);

            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return new HtmlString(writer.ToString());
            }
        }

        #endregion

        #region RenderAction

        public static IHtmlContent RenderAction(this IHtmlHelper helper, string action, object parameters = null)
        {
            var controller = (string)helper.ViewContext.RouteData.Values["controller"];
            return RenderAction(helper, action, controller, parameters);
        }

        public static IHtmlContent RenderAction(this IHtmlHelper helper, string action, string controller, object parameters = null)
        {
            var area = (string)helper.ViewContext.RouteData.Values["area"];
            return RenderAction(helper, action, controller, area, parameters);
        }

        public static IHtmlContent RenderAction(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(controller));
            if (controller == null)
                throw new ArgumentNullException(nameof(action));

            var task = RenderActionAsync(helper, action, controller, area, parameters);
            return task.Result;
        }

        private static async Task<IHtmlContent> RenderActionAsync(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            // fetching required services for invocation
            var currentHttpContext = helper.ViewContext.HttpContext;
            var httpContextFactory = GetServiceOrFail<IHttpContextFactory>(currentHttpContext);
            var actionInvokerFactory = GetServiceOrFail<IActionInvokerFactory>(currentHttpContext);
            var actionSelector = GetServiceOrFail<IActionDescriptorCollectionProvider>(currentHttpContext);

            // creating new action invocation context
            var routeData = new RouteData();
            var routeParams = new RouteValueDictionary(parameters ?? new { });
            var routeValues = new RouteValueDictionary(new { area, controller, action });
            var newHttpContext = httpContextFactory.Create(currentHttpContext.Features);

            newHttpContext.Response.Body = new MemoryStream();

            foreach (var router in helper.ViewContext.RouteData.Routers)
                routeData.PushState(router, null, null);

            routeData.PushState(null, routeValues, null);
            routeData.PushState(null, routeParams, null);

            var actionDescriptor = actionSelector.ActionDescriptors.Items.First(i => i.RouteValues["Controller"] == controller && i.RouteValues["Action"] == action);
            var actionContext = new ActionContext(newHttpContext, routeData, actionDescriptor);

            // invoke action and retreive the response body
            var invoker = actionInvokerFactory.CreateInvoker(actionContext);
            string content = null;

            await invoker.InvokeAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    content = task.Exception.Message;
                }
                else if (task.IsCompleted)
                {
                    newHttpContext.Response.Body.Position = 0;
                    using (var reader = new StreamReader(newHttpContext.Response.Body))
                        content = reader.ReadToEnd();
                }
            });

            return new HtmlString(content);
        }

        private static TService GetServiceOrFail<TService>(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var service = httpContext.RequestServices.GetService(typeof(TService));

            if (service == null)
                throw new InvalidOperationException($"Could not locate service: {nameof(TService)}");

            return (TService)service;
        }

        #endregion
    }
}
