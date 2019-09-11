using SHARED.Web.ActionResults;
using System;

namespace SHARED.Web.Ajax
{
    public class AjaxResult : JsonCamelCaseResult
    {
        private AjaxResult(object data) : base(data)
        {
        }

        public static AjaxResult Success(string info)
        {
            var json = new
            {
                Success = true,
                Info = info
            };

            var result = new AjaxResult(json);

            return result;
        }

        public static AjaxResult Error(string error)
        {
            var json = new
            {
                Success = false,
                Info = error
            };

            var result = new AjaxResult(json);
            
            return result;
        }

        public static AjaxResult Redirect(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentException();
            }

            var json = new
            {
                Success = true,
                Redirect = url,
            };

            var result = new AjaxResult(json);

            return result;
        }
    }
}
