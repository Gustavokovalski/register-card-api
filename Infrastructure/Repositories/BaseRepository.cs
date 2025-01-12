using Microsoft.EntityFrameworkCore;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Infrastructure.Context;

namespace RegisterCard.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly CardDbContext _context;
    public BaseRepository(CardDbContext context)
    {
        _context = context.ThrowIfNull();
    }
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }
}