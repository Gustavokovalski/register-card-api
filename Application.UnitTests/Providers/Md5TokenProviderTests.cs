using FluentAssertions;
using NUnit.Framework;
using RegisterCard.Application.Providers;

namespace RegisterCard.Application.UnitTests.Providers;

[TestFixture]
public class Md5TokenProviderTests
{
    private Md5TokenProvider _tokenProvider = new();

    [Test]
    [TestCase("4152314618768876")]
    [TestCase("4551933473272")]
    public void GenerateToken_Should_Return_Valid_Guid_When_CardNumber_And_Cvv_Are_Valid(string cardNumber)
    {
        // Arrange
        var cvv = "123";

        // Act
        var token = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token.Should().NotBeEmpty();
    }

    [Test]
    public void GenerateToken_Should_Generate_Same_Token_For_Same_CardNumber_And_Cvv()
    {
        // Arrange
        var cardNumber = "4152314618768876";
        var cvv = "331";

        // Act
        var token1 = _tokenProvider.GenerateToken(cardNumber, cvv);
        var token2 = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token1.Should().Be(token2);
    }

    [Test]
    public void GenerateToken_Should_Generate_Different_Token_For_Different_CardNumber()
    {
        // Arrange
        var cardNumber1 = "4152314618768876";
        var cvv = "579";
        var cardNumber2 = "4551933473272";

        // Act
        var token1 = _tokenProvider.GenerateToken(cardNumber1, cvv);
        var token2 = _tokenProvider.GenerateToken(cardNumber2, cvv);

        // Assert
        token1.Should().NotBe(token2);
    }

    [Test]
    public void GenerateToken_Should_Generate_Different_Token_For_Different_Cvv()
    {
        // Arrange
        var cardNumber = "4152314618768876";
        var cvv1 = "1977";
        var cvv2 = "579";

        // Act
        var token1 = _tokenProvider.GenerateToken(cardNumber, cvv1);
        var token2 = _tokenProvider.GenerateToken(cardNumber, cvv2);

        // Assert
        token1.Should().NotBe(token2);
    }

    [Test]
    public void GenerateToken_Should_Return_Same_Token_For_Identical_Input_After_Resets()
    {
        // Arrange
        var cardNumber = "4741978136963546";
        var cvv = "1123";

        // Act
        var token1 = _tokenProvider.GenerateToken(cardNumber, cvv);
        var token2 = _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        token1.Should().Be(token2);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void GenerateToken_Should_Throw_Exception_When_CardNumber_Is_Null(string cardNumber)
    {
        // Arrange
        string cvv = "123";

        // Act
        Action act = () => _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Card number is required");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void GenerateToken_Should_Throw_Exception_When_Cvv_Is_Null(string cvv)
    {
        // Arrange
        string cardNumber = "1234567812345678";

        // Act
        Action act = () => _tokenProvider.GenerateToken(cardNumber, cvv);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("CVV is required");
    }
}
