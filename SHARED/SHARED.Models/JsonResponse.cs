using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SHARED.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            NeedClose = true;
            CallbackFunction = "reloadJqGrid";
        }

        /// <summary>
        /// Любая строка, объявленная в JsonResponseType
        /// </summary>
        public String Type { get; set; }
        
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public String Message { get; set; }

        public IEnumerable<KeyValuePair<string,string[]>> Errors { get; set; } 

        public int Id { get; set; }

        public string CallbackFunction { get; set; }

        public List<object> CalbackParameters { get; set; } 

        public string UpdateDialogUrl { get; set; }

        public bool? NeedClose { get; set; }
        public bool NeedPageReload { get; set; }
        public bool NeedTabReload { get; set; }


        public static explicit operator JsonResponse(OperationResult operationResult)
        {
            if (operationResult == null) return null;
            return new JsonResponse()
            {
                Type = operationResult.Succeeded ? JsonResponseType.Success : JsonResponseType.Error,
                Message = operationResult.Info,
                Errors = operationResult.ValidationMessages.Select(z=>new KeyValuePair<string, string[]>("", new []{z})),
                Id = operationResult.OtherParams!=null? operationResult.OtherParams.Item1: -1, //Хрен пойми для чего это, но я не трогал
            };
        }



        public static explicit operator JsonResponse(ValidationResult validationResult)
        {
            if (validationResult == null) return null;
            return new JsonResponse()
            {
                Type = validationResult == ValidationResult.Success ? JsonResponseType.Success : JsonResponseType.Error,
                Message = validationResult.ToString(),
                //Errors = validationResult.ErrorMessage.Select(z => new KeyValuePair<string, string[]>("", new[] { z })),
                //Id = validationResult.OtherParams != null ? validationResult.OtherParams.Item1 : -1
            };
        }

        public static JsonResponse GetJson(OperationResult operationResult, string succeedCallBack = "reloadJqGrid", string errorCallBack = "FOR.Form.removeLoading",string message=null)
        {
            var jsonResponse = (JsonResponse)operationResult;

            jsonResponse.CallbackFunction = operationResult.Succeeded ? succeedCallBack : errorCallBack;
            if (message != null)
                jsonResponse.Message = message;

            return jsonResponse;
        }
    }

    public class JsonResponseType
    {
        public static readonly string Error = "error";
        public static readonly string Success = "success";
    }
}
