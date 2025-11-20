using FluentValidation;

namespace Application.Authentication.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório.")
            .EmailAddress()
            .WithMessage("Email inválido.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatório.");
    }
}