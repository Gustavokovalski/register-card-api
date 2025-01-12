using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Common.Repositories;
using RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;
using RegisterCard.Domain.Common;

namespace RegisterCard.Application.UnitTests.UseCases.RegisterCard.Commands;

public class RegisterCardHandlerTests
{
    private Mock<ITokenProviderService> _tokenProviderService;
    private Mock<ICardRepository> _repository;
    private RegisterCardHandler _handler;
    private Mock<ILogger<RegisterCardHandler>> _logger = new();

    [SetUp]
    public void SetUp()
    {
        _tokenProviderService = new Mock<ITokenProviderService>();
        _repository = new Mock<ICardRepository>();
        _handler = new RegisterCardHandler(
            _tokenProviderService.Object,
            _repository.Object,
            _logger.Object);
    }

    [Test]
    public void Constructor_NullTokenProviderService_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => new RegisterCardHandler(null, _repository.Object, _logger.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Constructor_NullCardRepository_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => new RegisterCardHandler(_tokenProviderService.Object, null, _logger.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public async Task Handle_ValidRequest_ReturnsGuid()
    {
        // Arrange
        var guid = new Guid();
        var command = new RegisterCardCommand { CustomerId = 1, CardNumber = "4927072305317100", Cvv = "441" };

        _tokenProviderService
            .Setup(x => x.GenerateToken(It.IsAny<CardInfo>()))
            .Returns(guid);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(guid.ToString());
    }
}