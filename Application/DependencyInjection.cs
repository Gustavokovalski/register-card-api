using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using System.Reflection;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Services;
using RegisterCard.Application.Common.Factories;
using RegisterCard.Application.Providers;
using RegisterCard.Application.Common.Behaviours;
namespace RegisterCard.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationUseCases(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviourHandler<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviourHandler<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviourHandler<,>));

        services.AddScoped<ITokenProviderFactory, TokenProviderFactory>();
        services.AddScoped<ITokenProviderService, ProviderService>();
        services.AddScoped<ITokenGenerator, Md5TokenProvider>();
        services.AddScoped<ITokenGenerator, RotatedTokenProvider>();

        return services;
    }
}