using MediatR;
using Microsoft.Extensions.Logging;
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
    private ILogger<RegisterCardHandler> _logger;

    public RegisterCardHandler(
        ITokenProviderService tokenProviderService,
        ICardRepository repository,
        ILogger<RegisterCardHandler> logger)
    {
        _tokenProviderService = tokenProviderService.ThrowIfNull();
        _repository = repository.ThrowIfNull();
        _logger = logger.ThrowIfNull();
    }

    public async Task<RegisterCardResponse> Handle(RegisterCardCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Card processing started for CustomerId:{customerId}", request.CustomerId);

        var token = _tokenProviderService.GenerateToken(new CardInfo(request.CardNumber!, request.Cvv!, request.ProviderType));

        _logger.LogInformation("Successfully generated token for CustomerId:{customerId}", request.CustomerId);

        var card = new Card { CustomerId = request.CustomerId, TokenDate = DateTime.Now };
        card.SetToken(token);

        await _repository.AddAsync(card, cancellationToken);
        _logger.LogInformation("Card:{0} for CustomerId:{1} Token was saved successfully", card.Id, request.CustomerId);
        return new RegisterCardResponse(card.Token!.ToString());
    }
}
