using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterCard.Domain.Aggregates.UserAggregate;

namespace RegisterCard.Infrastructure.Configuration;
public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
