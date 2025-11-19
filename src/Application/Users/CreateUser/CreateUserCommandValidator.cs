using Domain.ValueObjects;
using FluentValidation;

namespace Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome não pode ter mais de 200 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .Custom((password, context) =>
            {
                var validationResult = Password.ValidatePlainText(password);
                if (validationResult.IsFailure)
                {
                    context.AddFailure(validationResult.Error.Message);
                }
            });
    }
}