using System.Security.Cryptography;
using System.Text;
using RegisterCard.Application.Common.Interfaces;

namespace RegisterCard.Application.Providers;
public class Md5TokenProvider : ITokenGenerator
{
    public Guid GenerateToken(string cardNumber, string cvv)
    {
        if (string.IsNullOrEmpty(cardNumber))
            throw new ArgumentException("Card number is required");

        if (string.IsNullOrEmpty(cvv))
            throw new ArgumentException("CVV is required");

        using var md5 = MD5.Create();
        var input = $"{cardNumber}{cvv}";
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return new Guid(hash.Take(16).ToArray());
    }
}