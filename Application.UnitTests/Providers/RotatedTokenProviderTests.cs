using FluentAssertions;
using NUnit.Framework;
using RegisterCard.Application.Providers;

namespace RegisterCard.Application.UnitTests.Providers;

[TestFixture]
public class RotatedTokenProviderTests
{
    private RotatedTokenProvider _tokenProvider = new();

    [Test]
    public void GenerateToken_Should_Generate_Token_When_Rotation_Is_0()
    {
        // Arrange
        string cardNumber = "1234567812345678";
        string cvv = "000";

        // Act
        var token = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token.Should().NotBeEmpty();
    }

    [Test]
    public void GenerateToken_Should_Rotate_Correctly_When_Rotation_Is_Less_Than_4()
    {
        // Arrange
        string cardNumber = "1234567812341234";
        string cvv = "1";

        // Act
        var token = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token.Should().NotBeEmpty();
        var expectedGuid = new Guid(token.ToString());
        expectedGuid.Should().NotBeEmpty();
    }


    [Test]
    public void GenerateToken_Should_Handle_Cvv()
    {
        // Arrange
        string cardNumber = "9876543212345678";
        string cvv = "9"; //Rotation = 1 (cvv = 9 % 4)

        // Act
        var token = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token.Should().NotBeEmpty();
    }

    [Test]
    [TestCase("4075109055589999", "351")]
    [TestCase("47947339518989", "1267")]
    public void GenerateToken_Should_Handle_Cvv(string cardNumber, string cvv)
    {
        // Act
        var token = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token.Should().NotBeEmpty();
    }
}