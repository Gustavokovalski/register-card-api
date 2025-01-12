using RegisterCard.Application.Common.Exception;
using RegisterCard.Application.Common.Models;
using System.Text.Json;

namespace RegisterCard.WebApi.Extensions.Middlewares;

public class ValidationHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationHandler> _logger;

    public ValidationHandler(RequestDelegate next, ILogger<ValidationHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);
            await _next.Invoke(context);
            _logger.LogInformation("Request processed successfully: {Method} {Path}", context.Request.Method, context.Request.Path);
        }
        catch (ValidationExceptionCustom ex)
        {
            _logger.LogError(ex, "Validation error occurred for request: {Method} {Path}", context.Request.Method, context.Request.Path);
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object> { Message = "Validation Errors", Errors = ex.Errors });
        }
    }
}