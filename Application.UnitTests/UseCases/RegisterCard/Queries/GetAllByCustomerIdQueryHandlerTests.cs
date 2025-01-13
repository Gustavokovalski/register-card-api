using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Application.UseCases.Cards.Queries.GetUserById;
using RegisterCard.Domain.Aggregates.UserAggregate;

namespace RegisterCard.Application.UnitTests.UseCases.RegisterCard.Queries;

[TestFixture]
public class GetAllByCustomerIdQueryHandlerTests
{
    private Mock<ICardRepository> _repository;
    private GetAllByCustomerIdQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<ICardRepository>();
        _handler = new GetAllByCustomerIdQueryHandler(_repository.Object);
    }

    [Test]
    public void Constructor_NullCardRepository_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => new GetAllByCustomerIdQueryHandler(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public async Task Handle_Should_Return_List_Of_Cards()
    {
        // Arrange
        var card1 = new Fixture().Create<Card>();
        var card2 = new Fixture().Create<Card>();

        var cards = new List<Card> { card1, card2 };

        _repository.Setup(r => r.FindAsync(card1.CustomerId, It.IsAny<CancellationToken>())).ReturnsAsync(cards);

        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(new GetAllByCustomerIdQuery(card1.CustomerId), cancellationToken);

        // Assert
        result
            .Should()
            .BeEquivalentTo(cards.Select(card => new GetAllByCustomerIdResponse(card.Id, card.Token)));
    }

    [Test]
    public async Task Handle_Should_Return_Empty_List_When_No_Cards_Are_Found()
    {
        // Arrange
        var cards = new List<Card>();
        var customerId = cards.Select(x => x.CustomerId).FirstOrDefault();

        _repository.Setup(r => r.FindAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(cards);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(new GetAllByCustomerIdQuery(customerId), cancellationToken);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task Handle_Should_Call_GetAllAsync_Once()
    {
        // Arrange
        var cards = new List<Card> { new Fixture().Create<Card>() };
        var customerId = cards.Select(x => x.CustomerId).FirstOrDefault();

        _repository.Setup(r => r.FindAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(cards);

        var query = new GetAllByCustomerIdQuery(customerId);
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _repository.Verify(r => r.FindAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
    }
}