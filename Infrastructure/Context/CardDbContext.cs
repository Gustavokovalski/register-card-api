using Microsoft.EntityFrameworkCore;
using RegisterCard.Domain.Aggregates.UserAggregate;
using RegisterCard.Infrastructure.Configuration;

namespace RegisterCard.Infrastructure.Context;
public class CardDbContext : DbContext
{
    public CardDbContext(DbContextOptions<CardDbContext> options) : base(options) { }
    public DbSet<Card> Cards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("CardDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardConfiguration());
    }
}

