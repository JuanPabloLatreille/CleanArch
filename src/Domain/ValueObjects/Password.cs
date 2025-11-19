using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class Password
{
    private Password(string hash)
    {
        Hash = hash;
    }

    public string Hash { get; private set; }

    public static Result<string> ValidatePlainText(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            return Result.Failure<string>(
                Error.Validation("Password.Empty", "Senha não pode ser vazia"));

        if (plainTextPassword.Length < 8)
            return Result.Failure<string>(
                Error.Validation("Password.TooShort", "Senha deve ter no mínimo 8 caracteres"));

        if (!plainTextPassword.Any(char.IsUpper))
            return Result.Failure<string>(
                Error.Validation("Password.NoUpperCase", "Senha deve conter letra maiúscula"));

        if (!plainTextPassword.Any(char.IsLower))
            return Result.Failure<string>(
                Error.Validation("Password.NoLowerCase", "Senha deve conter letra minúscula"));

        if (!plainTextPassword.Any(char.IsDigit))
            return Result.Failure<string>(
                Error.Validation("Password.NoDigit", "Senha deve conter número"));

        return Result.Success(plainTextPassword);
    }

    public static Password FromHash(string hash)
    {
        return new Password(hash);
    }
}