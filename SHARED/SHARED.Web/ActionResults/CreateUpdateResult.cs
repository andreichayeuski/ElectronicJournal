using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SHARED.Models;
using SHARED.Web.ActionResults;

namespace EJ.Web.ActionResults
{
    public class CreateUpdateResult<T> : IActionResult
    {
        private readonly Func<T, Task<OperationResult>> _handler;
        private readonly JsonResponse _jsonParameters;
        private readonly IEnumerable<string> _excludedValidationParameters;
        private readonly Action<JsonResponse> _setResponceParameters;
        private readonly T _viewModel;

        public CreateUpdateResult(T viewModel, Func<T, Task<OperationResult>> handler,
            IEnumerable<string> excludedValidationParameters = null,
            Action<JsonResponse> setResponceParameters = null)
        {
            _handler = handler;
            _viewModel = viewModel;
            _jsonParameters = new JsonResponse
            {
                    CallbackFunction = "$('.ui-jqgrid:visible [role=\"presentation\"]').trigger('reloadGrid', [{ current: true }]);"
            };
            _excludedValidationParameters = excludedValidationParameters;
            _setResponceParameters = setResponceParameters;
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var modelState = context.ModelState;

            modelState.Remove("Id"); //TODO:bug in mvc

            if (_excludedValidationParameters != null && _excludedValidationParameters.Any())
            {
                foreach (var evp in _excludedValidationParameters)
                {
                    if (modelState[evp].Errors.Count > 0)
                        modelState[evp].Errors.RemoveAt(0);
                }
            }

            if (modelState.IsValid)
            {
                var t = await _handler.Invoke(_viewModel);

                var jsonResponce = (JsonResponse) t;

                if (t.Succeeded)
                {
                    jsonResponce.NeedClose = _jsonParameters.NeedClose;
                    if (!String.IsNullOrEmpty(_jsonParameters.Message))
                        jsonResponce.Message = _jsonParameters.Message;

                    jsonResponce.CallbackFunction = _jsonParameters.CallbackFunction;
                    jsonResponce.CalbackParameters = _jsonParameters.CalbackParameters;
                    _setResponceParameters?.Invoke(jsonResponce);
                }
                else
                {
                    jsonResponce.CallbackFunction = "EJ.Form.removeLoading";
                }

                await new JsonCamelCaseResult(jsonResponce).ExecuteResultAsync(context);
            }
            else
            {
                var errors = modelState.ToDictionary(kvp => kvp.Key,
                        kvp => kvp.Value.Errors
                            .Select(e => e.ErrorMessage).ToArray())
                    .Where(m => m.Value.Any());

                await new JsonCamelCaseResult(new JsonResponse
                {
                    Type = "error",
                    Errors = errors.ToArray(),
                    CallbackFunction = "EJ.Form.removeLoading"
                }).ExecuteResultAsync(context);
            }
        }
    }
}
