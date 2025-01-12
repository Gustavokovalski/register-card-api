using Microsoft.Extensions.Logging;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Domain.Common;

namespace RegisterCard.Application.Services;

public class ProviderService : ITokenProviderService
{
    private readonly ITokenProviderFactory _factory;
    private ILogger<ProviderService> _logger;

    public ProviderService(
        ITokenProviderFactory factory,
        ILogger<ProviderService> logger)
    {
        _factory = factory.ThrowIfNull();
        _logger = logger.ThrowIfNull();
    }

    public Guid GenerateToken(CardInfo cardInfo)
    {
        var generator = _factory.Create(cardInfo.ProviderType);
        _logger.LogInformation("Token generator created: {GeneratorType}", generator.GetType().FullName);

        var token = generator.GenerateToken(cardInfo.CardNumber, cardInfo.Cvv);
        _logger.LogInformation("Token generated successfully by {GeneratorType}", generator.GetType().FullName);
        return token;
    }
}