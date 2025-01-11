using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Infrastructure.Context;
using RegisterCard.Infrastructure.Repositories;

namespace RegisterCard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationExternalDependencies(
        this IServiceCollection services)
    {
        //services.AddMEOpenTelemetry(configuration);
        services.AddDbContext<CardDbContext>(
            options => options.UseInMemoryDatabase("CardDatabase"));

        services.AddScoped<ICardRepository, CardRepository>();
        return services;
    }
}