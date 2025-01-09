using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using System.Reflection;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
namespace RegisterCard.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationUseCases(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}

//TODO - separar
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .Select(r => new BaseError() { PropertyMessage = r.PropertyName, ErrorMessage = r.ErrorMessage })
                .ToList();

            if (failures.Any())
            {
                throw new ValidationExceptionCustom(failures);
            }
        }

        return await next();
    }
}

//TODO - separar
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RegisterCard API Handling: { name } {@request }", typeof(TRequest).Name, JsonSerializer.Serialize(request));
        var response = await next();
        _logger.LogInformation("RegisterCard API Response Handling: { name } {@response }", typeof(TResponse).Name, JsonSerializer.Serialize(response));

        return response;
    }
}

//TODO - separar
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch() ?? throw new ArgumentNullException(nameof(Stopwatch));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds > 10)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("RegisterCard API Long Running: {name} ({elapsedMilliseconds} milliseconds) {@request}",
                requestName,
                elapsedMilliseconds,
                JsonSerializer.Serialize(request));
        }
        return response;
    }
}

//TODO - jogar para uma classe
public class BaseError
{
    public string? PropertyMessage { get; set; }
    public string? ErrorMessage { get; set; }
}

//TODO - jogar para uma classe
public class ValidationExceptionCustom : Exception
{
    public IEnumerable<BaseError> Errors { get; }

    public ValidationExceptionCustom()
        : base("One or more validation failures have occured.")
    {
        Errors = new List<BaseError>();
    }

    public ValidationExceptionCustom(IEnumerable<BaseError> errors)
        : this()
    {
        Errors = errors;
    }
}