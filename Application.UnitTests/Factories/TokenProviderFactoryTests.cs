using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RegisterCard.Application.Common.Factories;
using RegisterCard.Application.Providers;
using RegisterCard.Domain.Enums;

namespace RegisterCard.Application.UnitTests.Factories;

[TestFixture]
internal class TokenProviderFactoryTests
{
    private Mock<IConfiguration> _configurationMock;
    private Mock<ILogger<TokenProviderFactory>> _loggerMock;
    private TokenProviderFactory _factory;

    [SetUp]
    public void SetUp()
    {
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<TokenProviderFactory>>();
    }

    [Test]
    public void Constructor_ShouldThrowArgumentNullException_WhenConfigurationIsNull()
    {
        // Act
        Action act = () => new TokenProviderFactory(null, _loggerMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*configuration*");
    }

    [Test]
    public void Constructor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
        // Act
        Action act = () => new TokenProviderFactory(_configurationMock.Object, null);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*logger*");
    }

    [Test]
    public void Create_ShouldReturnMd5TokenProvider_WhenProviderTypeIsProviderA()
    {
        // Arrange
        _configurationMock.Setup(c => c["DefaultTokenProvider"]).Returns("ProviderA");
        _factory = new TokenProviderFactory(_configurationMock.Object, _loggerMock.Object);

        // Act
        var result = _factory.Create(TokenProviderType.ProviderA);

        // Assert
        result.Should().BeOfType<Md5TokenProvider>();
    }

    [Test]
    public void Create_ShouldReturnRotatedTokenProvider_WhenProviderTypeIsProviderB()
    {
        // Arrange
        _configurationMock.Setup(c => c["DefaultTokenProvider"]).Returns("ProviderB");
        _factory = new TokenProviderFactory(_configurationMock.Object, _loggerMock.Object);

        // Act
        var result = _factory.Create(TokenProviderType.ProviderB);

        // Assert
        result.Should().BeOfType<RotatedTokenProvider>();
    }

    [Test]
    public void Create_ShouldReturnDefaultProviderB_WhenProviderTypeIsNull()
    {
        // Arrange
        _configurationMock.Setup(c => c["DefaultTokenProvider"]).Returns("ProviderB");
        _factory = new TokenProviderFactory(_configurationMock.Object, _loggerMock.Object);

        // Act
        var result = _factory.Create(null);

        // Assert
        result.Should().BeOfType<RotatedTokenProvider>();
    }

    [Test]
    public void Create_ShouldThrowArgumentException_WhenProviderTypeIsInvalid()
    {
        // Arrange
        _configurationMock.Setup(c => c["DefaultTokenProvider"]).Returns("InvalidProvider");
        _factory = new TokenProviderFactory(_configurationMock.Object, _loggerMock.Object);

        // Act
        Action act = () => _factory.Create((TokenProviderType)999);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid provider type:*");
    }

    [Test]
    public void Create_ShouldThrowArgumentException_WhenDefaultProviderIsInvalid()
    {
        // Arrange
        _configurationMock.Setup(c => c["DefaultTokenProvider"]).Returns("InvalidProvider");
        _factory = new TokenProviderFactory(_configurationMock.Object, _loggerMock.Object);

        // Act
        Action act = () => _factory.Create(null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Requested value 'InvalidProvider' was not found.");
    }

}
