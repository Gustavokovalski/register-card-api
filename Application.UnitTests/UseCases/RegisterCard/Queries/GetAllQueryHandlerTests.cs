using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Application.UseCases.Cards.Queries.GetUserById;
using RegisterCard.Domain.Aggregates.UserAggregate;

namespace RegisterCard.Application.UnitTests.UseCases.RegisterCard.Queries;

[TestFixture]
public class GetAllQueryHandlerTests
{
    private Mock<ICardRepository> _repository;
    private GetAllQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<ICardRepository>();
        _handler = new GetAllQueryHandler(_repository.Object);
    }

    [Test]
    public void Constructor_NullCardRepository_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => new GetAllQueryHandler(null);

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

        _repository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(cards);

        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(new GetAllQuery(), cancellationToken);

        // Assert
        result
            .Should()
            .BeEquivalentTo(cards.Select(card => new GetAllResponse(card.Id, card.Token)));
    }

    [Test]
    public async Task Handle_Should_Return_Empty_List_When_No_Cards_Are_Found()
    {
        // Arrange
        var cards = new List<Card>();
        _repository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(cards);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(new GetAllQuery(), cancellationToken);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task Handle_Should_Call_GetAllAsync_Once()
    {
        // Arrange
        var cards = new List<Card> { new Fixture().Create<Card>() };

        _repository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(cards);

        var query = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _repository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}