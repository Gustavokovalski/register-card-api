using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Providers;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.Common.Factories;
public class TokenProviderFactory : ITokenProviderFactory
{
    private readonly IConfiguration _configuration;
    protected string _DEFAULT_PROVIDER;
    private ILogger<TokenProviderFactory> _logger;

    public TokenProviderFactory(
        IConfiguration configuration,
        ILogger<TokenProviderFactory> logger)
    {
        _configuration = configuration.ThrowIfNull();
        _DEFAULT_PROVIDER = _configuration["DefaultTokenProvider"]!;
        _logger = logger.ThrowIfNull();
    }

    public ITokenGenerator Create(TokenProviderType? providerType)
    {
        if (providerType is null)
        {
            _logger.LogInformation("No provider type specified. Using default provider: {DefaultProviderType}", _DEFAULT_PROVIDER);
            providerType = (TokenProviderType)Enum.Parse(typeof(TokenProviderType), _DEFAULT_PROVIDER);
        }

        _logger.LogInformation("ProviderType: {providerType} selected", providerType);

        try
        {
            return providerType switch
            {
                TokenProviderType.ProviderA => new Md5TokenProvider(),
                TokenProviderType.ProviderB => new RotatedTokenProvider(),
                _ => throw new ArgumentException($"Invalid provider type:{providerType}")
            };
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Failed to create token generator for provider type: {ProviderType}", providerType);
            throw;
        }

    }
}