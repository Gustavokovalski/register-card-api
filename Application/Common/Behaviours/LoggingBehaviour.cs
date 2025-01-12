using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace RegisterCard.Application.Common.Behaviours;
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RegisterCard API Handling: { name } {@request }", typeof(TRequest).Name, JsonSerializer.Serialize(request));
        var response = await next();
        _logger.LogInformation("RegisterCard API Response Handling: { name } {@response }", typeof(TResponse).Name, JsonSerializer.Serialize(response));

        return response;
    }
}