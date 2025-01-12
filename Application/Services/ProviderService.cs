using RegisterCard.Application.Common.Factories;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Domain.Common;

namespace RegisterCard.Application.Services;

public class ProviderService : ITokenProviderService
{
    private readonly ITokenProviderFactory _factory;

    public ProviderService(ITokenProviderFactory factory)
    {
        _factory = factory;
    }

    public Guid GenerateToken(CardInfo cardInfo)
    {
        var generator = _factory.Create(cardInfo.ProviderType);
        return generator.GenerateToken(cardInfo.CardNumber, cardInfo.Cvv);
    }
}