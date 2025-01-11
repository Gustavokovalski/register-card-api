using FluentValidation;

namespace RegisterCard.Application.UseCases.Token.Commands.CreateToken;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator()
    {
        //RuleFor(p => p.Name).NotEmpty();
        //RuleFor(p => p.Email).NotEmpty().EmailAddress();
    }
}