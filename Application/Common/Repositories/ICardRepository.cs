using RegisterCard.Domain.Aggregates.UserAggregate;

namespace RegisterCard.Application.Common.Repositories;

public interface ICardRepository : IRepository<Card>
{
    Task<IEnumerable<Card>> FindAsync(int customerId, CancellationToken cancellationToken = default);
}