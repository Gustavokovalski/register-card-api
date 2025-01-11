using RegisterCard.Application;
using RegisterCard.Application.Common.Models;
using System.Text.Json;

namespace RegisterCard.WebApi.Extensions.Middlewares;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            //TODO - log 
            await _next.Invoke(context);
        }
        catch (ValidationExceptionCustom ex)
        {
            //TODO - log error
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object> { Message = "Validation Errors", Errors = ex.Errors });
        }
    }
}