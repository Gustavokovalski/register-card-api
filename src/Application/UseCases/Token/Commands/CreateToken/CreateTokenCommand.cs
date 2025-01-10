using MediatR;

namespace RegisterCard.Application.UseCases.Token.Commands.CreateToken;

public class CreateTokenCommand : IRequest<CreateTokenResponse>
{
    //TODO - avaliar =null!
    public string CardNumber { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
}