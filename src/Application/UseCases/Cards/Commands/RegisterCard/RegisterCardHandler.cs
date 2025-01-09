using MediatR;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Application.UseCases.Token.Commands.CreateToken;
using RegisterCard.Domain.Aggregates.UserAggregate;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;
public class RegisterCardHandler : IRequestHandler<RegisterCardCommand, RegisterCardResponse>
{
    private readonly ICardRepository _repository;
    private readonly IMediator _mediator;

    public RegisterCardHandler(IMediator mediator, ICardRepository repository)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<RegisterCardResponse> Handle(RegisterCardCommand request, CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(new CreateTokenCommand
        {
            CardNumber = request.CardNumber!,
            Cvv = request.Cvv!,
            Provider = request.Provider!
        });

        var card = new Card { CustomerId = request.CustomerId, TokenDate = DateTime.Now };
        card.SetToken(res.Token);
        await _repository.AddAsync(card, cancellationToken);

        var tst = await _repository.GetAllAsync(cancellationToken);

        return new RegisterCardResponse(card.Token!.ToString());
    }
}
