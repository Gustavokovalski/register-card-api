namespace RegisterCard.Domain.Aggregates.UserAggregate;

public class Card : Entity
{
    public int CustomerId { get; init; }
    public Guid Token { get; private set; }
    public DateTime TokenDate { get; init; }

    public void SetToken(Guid token)
    {
        Token = token;
    }
}