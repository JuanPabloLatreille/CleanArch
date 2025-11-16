using Domain.Enums;

namespace Domain.Shared;

public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(
            "Validation.MultipleErrors",
            FormatErrorMessage(errors),
            ErrorType.Validation)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    private static string FormatErrorMessage(Error[] errors)
    {
        return errors.Length == 1
            ? errors[0].Message
            : $"Ocorreram {errors.Length} erros de validação: {string.Join("; ", errors.Select(e => e.Message))}";
    }
}