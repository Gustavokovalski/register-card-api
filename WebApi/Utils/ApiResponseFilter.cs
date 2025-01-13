using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RegisterCard.Application.Common.Bases;
using RegisterCard.Application.Common.Models;
using System.Diagnostics.CodeAnalysis;

namespace RegisterCard.WebApi.Utils;

[ExcludeFromCodeCoverage]
public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            HandleException(context);
        }
        else
        {
            HandleSuccess(context);
        }
    }

    private void HandleSuccess(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var response = objectResult.Value;
            if (response is BaseResponse<object> baseResponse)
            {
                if (baseResponse.Data is BaseResponse<object> nestedResponse)
                {
                    objectResult.Value = baseResponse;
                }
                else
                {
                    baseResponse.Success = true;
                    baseResponse.Message = baseResponse.Message ?? "Request succeeded";
                    baseResponse.Errors = baseResponse.Errors ?? Enumerable.Empty<BaseError>();
                    objectResult.Value = baseResponse;
                }
            }
            else
            {
                objectResult.Value = new BaseResponse<object>
                {
                    Success = true,
                    Message = "Request succeeded",
                    Data = response
                };
            }
        }
    }

    private void HandleException(ActionExecutedContext context)
    {
        var response = new BaseResponse<object>
        {
            Success = false,
            Message = "An unexpected error occurred",
            Errors = new List<BaseError>
            {
                new BaseError { PropertyMessage = "ServerError", Description = context.Exception!.Message }
            }
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}