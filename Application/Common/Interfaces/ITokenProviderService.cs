using RegisterCard.Domain.Common;

namespace RegisterCard.Application.Common.Interfaces;
public interface ITokenProviderService
{
    Guid GenerateToken(CardInfo cardInfo);
}