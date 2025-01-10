using MediatR;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Domain.Aggregates.UserAggregate;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;
public class RegisterCardHandler : IRequestHandler<RegisterCardCommand, RegisterCardResponse>
{
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly ICardRepository _repository;

    public RegisterCardHandler(ITokenGeneratorService tokenGeneratorService, ICardRepository repository)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _repository = repository;
    }

    public async Task<RegisterCardResponse> Handle(RegisterCardCommand request, CancellationToken cancellationToken)
    {
        var token = _tokenGeneratorService.GenerateToken(request.Provider!, request.CardNumber!, request.Cvv!);
        //var res = await _tokenGeneratorService.Send(new CreateTokenCommand
        //{
        //    CardNumber = request.CardNumber!,
        //    Cvv = request.Cvv!,
        //    Provider = request.Provider!
        //});

        var card = new Card { CustomerId = request.CustomerId, TokenDate = DateTime.Now };
        card.SetToken(token);
        await _repository.AddAsync(card, cancellationToken);

        var tst = await _repository.GetAllAsync(cancellationToken);

        return new RegisterCardResponse(card.Token!.ToString());
    }
}
