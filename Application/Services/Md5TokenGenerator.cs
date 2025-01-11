using System.Security.Cryptography;
using System.Text;
using RegisterCard.Application.Common.Interfaces;

namespace RegisterCard.Application.Services;
public class Md5TokenGenerator : ITokenGenerator
{
    public Guid GenerateToken(string cardNumber, string cvv)
    {
        using var md5 = MD5.Create();
        var input = $"{cardNumber}{cvv}";
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return new Guid(hash.Take(16).ToArray());
    }
}