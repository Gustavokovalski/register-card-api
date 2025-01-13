using Microsoft.EntityFrameworkCore;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Domain.Aggregates.UserAggregate;
using RegisterCard.Infrastructure.Context;

namespace RegisterCard.Infrastructure.Repositories;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(CardDbContext context) : base(context)
    {

    }

    public async Task<IEnumerable<Card>> FindAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Card>()
            .Where(card => card.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }
}
