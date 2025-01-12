using RegisterCard.Domain.Enums;

namespace RegisterCard.Domain.Common;
public class CardInfo
{
    public string CardNumber { get; private set; }
    public string Cvv { get; private set; }
    public TokenProviderType? ProviderType { get; private set; }

    public CardInfo(string cardNumber, string cvv, TokenProviderType? providerType)
    {
        CardNumber = cardNumber.Replace(" ", "").Replace("-", "");
        Cvv = cvv;
        ProviderType = providerType;
    }
}