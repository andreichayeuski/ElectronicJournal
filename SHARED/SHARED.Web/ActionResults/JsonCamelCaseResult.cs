using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SHARED.Web.ActionResults
{
    public class JsonCamelCaseResult : IActionResult
    {
        public JsonCamelCaseResult(object data)
        {
            Data = data;
        }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            if (!response.HasStarted)
            {
                response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            }

            if (Data == null)
                return;

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if (ContentEncoding != null)
            {
                response.WriteAsync(JsonConvert.SerializeObject(Data, jsonSerializerSettings), ContentEncoding);
            }
            else
            {
                response.WriteAsync(JsonConvert.SerializeObject(Data, jsonSerializerSettings));
            }
        }

            public virtual Task ExecuteResultAsync(ActionContext context)
            {
                ExecuteResult(context);
                return Task.FromResult(true);
            }
   
    }
}
