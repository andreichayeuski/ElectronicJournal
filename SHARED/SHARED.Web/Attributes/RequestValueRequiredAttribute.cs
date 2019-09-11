using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHARED.Web.Attributes
{
    /// <summary>
    /// Marks action method as valid for selection only if httprequest form values 
    /// contain value with specified name.
    /// </summary>
    public class RequestValueRequiredAttribute : ActionMethodSelectorAttribute
    {
        private readonly string _name;

        public RequestValueRequiredAttribute(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            _name = name;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (routeContext == null)
            {
                throw new ArgumentNullException("routeContext");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            StringValues value;
            if (routeContext.HttpContext.Request.Query.TryGetValue(_name, out value)
            || (routeContext.HttpContext.Request.Method == "POST" && routeContext.HttpContext.Request.Form.TryGetValue(_name, out value)))
            {
                return value.ToString() != null || value.ToArray().Any();
            }

            return false;

        }
    }
}
