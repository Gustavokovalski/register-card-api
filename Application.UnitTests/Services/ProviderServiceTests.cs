using Moq;
using NUnit.Framework;
using RegisterCard.Application.Common.Interfaces;
using RegisterCard.Application.Services;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.UnitTests.Services;
public class ProviderServiceTests
{
    private readonly Mock<ITokenProviderFactory> _mockFactory = new();
    private ProviderService _providerService;

    public ProviderServiceTests()
    {
        _providerService = new ProviderService(_mockFactory.Object);
    }

    [TestCase(TokenProviderType.ProviderA)]
    [TestCase(TokenProviderType.ProviderB)]
    public void GenerateToken_ShouldCallFactoryCreateMethod(TokenProviderType providerType)
    {
        // Arrange
        var mockGenerator = new Mock<ITokenGenerator>();
        _mockFactory.Setup(f => f.Create(It.IsAny<TokenProviderType?>())).Returns(mockGenerator.Object);

        // Act & Assert
        Assert.DoesNotThrow(() => _providerService.GenerateToken(new Domain.Common.CardInfo("4729 3536 71738", "1123", providerType)));
    }
}