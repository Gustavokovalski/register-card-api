using MediatR;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Repositories;

namespace RegisterCard.Application.UseCases.Cards.Queries.GetUserById;

public class GetAllByCustomerIdQueryHandler : IRequestHandler<GetAllByCustomerIdQuery, IEnumerable<GetAllByCustomerIdResponse>>
{
    private readonly ICardRepository _repository;

    public GetAllByCustomerIdQueryHandler(ICardRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }

    public async Task<IEnumerable<GetAllByCustomerIdResponse>> Handle(GetAllByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.FindAsync(request.CustomerId, cancellationToken);
        return cards.Select(card => new GetAllByCustomerIdResponse(card.Id, card.Token));
    }
}