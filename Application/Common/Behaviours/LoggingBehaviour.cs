using MediatR;
using Microsoft.Extensions.Logging;
using RegisterCard.Application.Common.Exceptions;

namespace RegisterCard.Application.Common.Behaviours;
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger.ThrowIfNull();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RegisterCard API Handling: { name }", typeof(TRequest).Name);
        var response = await next();
        _logger.LogInformation("RegisterCard API Response Handling: { name }", typeof(TResponse).Name);

        return response;
    }
}