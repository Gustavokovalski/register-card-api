using FluentValidation;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;

public class RegisterCardCommandValidator : AbstractValidator<RegisterCardCommand>
{
    private const string RequiredMessage = "{PropertyName} is required.";
    private const string InvalidCardFormatMessage = "Invalid card number format.";
    private const string InvalidCvvFormatMessage = "CVV must be exactly 4 digits.";

    public RegisterCardCommandValidator()
    {
        RuleFor(p => p.CustomerId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage(RequiredMessage);

        RuleFor(p => p.CardNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage(RequiredMessage)
            .CreditCard()
            .WithMessage(InvalidCardFormatMessage);

        RuleFor(p => p.Cvv)
            .NotNull()
            .NotEmpty()
            .WithMessage(RequiredMessage)
            .Matches(@"^\d{4}$")
            .WithMessage(InvalidCvvFormatMessage);
    }
}