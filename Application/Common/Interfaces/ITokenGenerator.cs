namespace RegisterCard.Application.Common.Interfaces;
public interface ITokenGenerator
{
    Guid GenerateToken(string cardNumber, string cvv);
}