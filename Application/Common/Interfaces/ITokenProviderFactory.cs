using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.Common.Interfaces;
public interface ITokenProviderFactory
{
    ITokenGenerator Create(TokenProviderType? providerType);
}