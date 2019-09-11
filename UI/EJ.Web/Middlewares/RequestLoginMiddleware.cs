using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EJ.Web.Middlewares
{
    public class RequestLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var name = context.User.Identity.Name;

            //name = "cbt\\ortest6";

            if (name.Contains("\\"))
            {
                var nameParts = name.Split("\\");
                var convertedName = $"{nameParts[1]}@{nameParts[0]}";
                var claim = new Claim(ClaimTypes.WindowsAccountName, convertedName);
                context.User.AddIdentity(new ClaimsIdentity(new[] {claim}));
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
