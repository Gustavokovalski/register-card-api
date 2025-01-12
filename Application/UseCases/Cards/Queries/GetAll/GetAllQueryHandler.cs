using MediatR;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Repositories;

namespace RegisterCard.Application.UseCases.Cards.Queries.GetUserById;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<GetAllResponse>>
{
    private readonly ICardRepository _repository;

    public GetAllQueryHandler(ICardRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }

    public async Task<IEnumerable<GetAllResponse>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.GetAllAsync(cancellationToken);
        return cards.Select(card => new GetAllResponse(card.Id, card.Token));
    }
}