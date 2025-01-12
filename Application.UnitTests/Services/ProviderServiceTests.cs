using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Services;
using RegisterCard.Domain.Common;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.UnitTests.Services;

[TestFixture]
public class ProviderServiceTests
{
    private Mock<ITokenProviderFactory> _mockFactory;
    private Mock<ITokenGenerator> _mockGenerator;
    private ProviderService _providerService;
    private Mock<ILogger<ProviderService>> _logger = new();

    [SetUp]
    public void SetUp()
    {
        _mockFactory = new Mock<ITokenProviderFactory>();
        _mockGenerator = new Mock<ITokenGenerator>();
        _providerService = new ProviderService(_mockFactory.Object, _logger.Object);
    }

    [Test]
    public void Constructor_NullTokenProviderService_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => new ProviderService(null, _logger.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase(TokenProviderType.ProviderA)]
    [TestCase(TokenProviderType.ProviderB)]
    public void GenerateToken_Should_Call_FactoryCreateMethod(TokenProviderType providerType)
    {
        // Arrange
        var cardInfo = new CardInfo("1234567812345678", "123", providerType);

        _mockFactory
            .Setup(f => f.Create(cardInfo.ProviderType))
            .Returns(_mockGenerator.Object);

        _mockGenerator.
            Setup(g => g.GenerateToken(cardInfo.CardNumber, cardInfo.Cvv))
            .Returns(Guid.NewGuid());

        // Act
        var token = _providerService.GenerateToken(cardInfo);

        // Assert
        token.Should().NotBeEmpty();
        _mockFactory.Verify(f => f.Create(cardInfo.ProviderType), Times.Once);
        _mockGenerator.Verify(g => g.GenerateToken(cardInfo.CardNumber, cardInfo.Cvv), Times.Once);
    }
}