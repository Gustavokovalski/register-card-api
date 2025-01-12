using System.Text.Json.Serialization;
using MediatR;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;

public class RegisterCardCommand : IRequest<RegisterCardResponse>
{
    [JsonIgnore]
    public int CustomerId { get; set; }

    [JsonRequired]
    public string? CardNumber { get; set; }

    [JsonRequired]
    public string? Cvv { get; set; }
    public TokenProviderType ProviderType { get; set; }
}