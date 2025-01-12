using System.Text.Json.Serialization;
using MediatR;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;

public class RegisterCardCommand : IRequest<RegisterCardResponse>
{
    [JsonIgnore]
    public int CustomerId { get; set; }

    [JsonRequired]
    public string? CardNumber { get; init; }

    [JsonRequired]
    public string? Cvv { get; init; }
    public TokenProviderType? ProviderType { get; init; }
}