using Microsoft.Extensions.Configuration;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Providers;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.Common.Factories;
public class TokenProviderFactory : ITokenProviderFactory
{
    private readonly IConfiguration _configuration;
    protected string _DEFAULT_PROVIDER;

    public TokenProviderFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _DEFAULT_PROVIDER = _configuration["DefaultTokenProvider"]!;
    }

    public ITokenGenerator Create(TokenProviderType? providerType)
    {
        if (providerType is null)
        {
            providerType = (TokenProviderType)Enum.Parse(typeof(TokenProviderType), _DEFAULT_PROVIDER);
        }

        return providerType switch
        {
            TokenProviderType.ProviderA => new Md5TokenProvider(),
            TokenProviderType.ProviderB => new RotatedTokenProvider(),
            _ => throw new ArgumentException("Invalid provider")
        };
    }
}