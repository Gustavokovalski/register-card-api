using MediatR;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Domain.Aggregates.UserAggregate;
using RegisterCard.Domain.Common;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;
public class RegisterCardHandler : IRequestHandler<RegisterCardCommand, RegisterCardResponse>
{
    private readonly ITokenProviderService _tokenProviderService;
    private readonly ICardRepository _repository;

    public RegisterCardHandler(
        ITokenProviderService tokenProviderService,
        ICardRepository repository)
    {
        _tokenProviderService = tokenProviderService.ThrowIfNull();
        _repository = repository.ThrowIfNull();
    }

    public async Task<RegisterCardResponse> Handle(RegisterCardCommand request, CancellationToken cancellationToken)
    {
        var token = _tokenProviderService.GenerateToken(new CardInfo(request.CardNumber!, request.Cvv!, request.ProviderType));

        var card = new Card { CustomerId = request.CustomerId, TokenDate = DateTime.Now };
        card.SetToken(token);

        await _repository.AddAsync(card, cancellationToken);
        return new RegisterCardResponse(card.Token!.ToString());
    }
}
