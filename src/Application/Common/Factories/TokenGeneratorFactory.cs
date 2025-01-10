using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Services;

namespace RegisterCard.Application.Common.Factories;
public class TokenGeneratorFactory
{
    //TODO - melhorar o nome + setar um default caso venha null
    public ITokenGenerator Create(string provider)
    {
        return provider switch
        {
            "ProviderA" => new Md5TokenGenerator(),
            "ProviderB" => new RotatedTokenGenerator(),
            _ => throw new ArgumentException("Invalid provider")
        };
    }
}