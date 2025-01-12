using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;

namespace RegisterCard.Application.UnitTests.UseCases.RegisterCard.Commands;

public class RegisterCardCommandValidatorTests
{
    private RegisterCardCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new RegisterCardCommandValidator();
    }

    [Test]
    [TestCase(0)]
    public void Should_Have_Error_When_CustomerId_Is_EqualToZero(int customerId)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = customerId, CardNumber = "4298903509494686", Cvv = "853" };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.CustomerId);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("'Customer Id' must not be empty."));
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Customer Id is required."));
    }

    [Test]
    [TestCase(-1)]
    public void Should_Have_Error_When_CustomerId_Is_LessThanZero(int customerId)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = customerId, CardNumber = "4298903509494686", Cvv = "853" };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.CustomerId);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Customer Id is required."));
    }

    [Test]
    [TestCase(null)]
    public void Should_Have_Error_CardNumber_Is_Null(string cardNumber)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = 1, CardNumber = cardNumber, Cvv = "853" };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.CardNumber);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Card Number is required."));
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("'Card Number' must not be empty."));
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    public void Should_Have_Error_CardNumber_Is_Empty(string cardNumber)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = 1, CardNumber = cardNumber, Cvv = "853" };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.CardNumber);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Card Number is required."));
    }

    [Test]
    [TestCase("1")]
    [TestCase("-429890350949466")]
    [TestCase("4865974547464")]
    public void Should_Have_Error_CardNumber_Format_Is_Invalid(string cardNumber)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = 1, CardNumber = cardNumber, Cvv = "853" };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.CardNumber);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Invalid card number."));
    }

    [Test]
    [TestCase(null)]
    public void Should_Have_Error_Cvv_Is_Null_Or_Empty(string cvv)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = 1, CardNumber = "4298903509494686", Cvv = cvv };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.Cvv);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Cvv is required."));
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("'Cvv' must not be empty."));
    }

    [Test]
    [TestCase("")]
    [TestCase("1")]
    [TestCase("22")]
    [TestCase("-7485")]
    [TestCase("12345")]
    public void Should_Have_Error_Cvv_Is_Invalid(string cvv)
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = 1, CardNumber = "4298903509494686", Cvv = cvv };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.Cvv);
        result.Errors.Should().Contain(e => e.ErrorMessage.Equals("Cvv must contain 3 or 4 digits."));
    }

    [Test]
    public void Should_Have_Error_Command_Is_Invalid()
    {
        // Arrange
        var command = new RegisterCardCommand { CustomerId = 0, CardNumber = string.Empty, Cvv = string.Empty };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(request => request.CustomerId);
        result.ShouldHaveValidationErrorFor(request => request.CardNumber);
        result.ShouldHaveValidationErrorFor(request => request.Cvv);
    }
}