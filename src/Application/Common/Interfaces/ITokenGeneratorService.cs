namespace RegisterCard.Application.Common.Interfaces;
public interface ITokenGeneratorService
{
    Guid GenerateToken(string provider, string cardNumber, string cvv);
}