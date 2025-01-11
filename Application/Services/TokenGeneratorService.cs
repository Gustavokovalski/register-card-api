using RegisterCard.Application.Common.Factories;
using RegisterCard.Application.Common.Interfaces;

namespace RegisterCard.Application.Services;
public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly TokenGeneratorFactory _factory;

    public TokenGeneratorService(TokenGeneratorFactory factory)
    {
        _factory = factory;
    }

    public Guid GenerateToken(string provider, string cardNumber, string cvv)
    {
        var generator = _factory.Create(provider);
        return generator.GenerateToken(cardNumber, cvv);
    }
}