using RegisterCard.Application.Common.Repositories;
using RegisterCard.Domain.Aggregates.UserAggregate;
using RegisterCard.Infrastructure.Context;

namespace RegisterCard.Infrastructure.Repositories;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(CardDbContext context) : base(context)
    {
    }
}
