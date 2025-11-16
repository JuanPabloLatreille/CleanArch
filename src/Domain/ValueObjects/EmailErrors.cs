using Domain.Shared;

namespace Domain.ValueObjects;

public static class EmailErrors
{
    public static Error Empty => Error.Validation(
        "Email.Empty",
        "O email não pode ser vazio.");

    public static Error TooLong => Error.Validation(
        "Email.TooLong",
        "O email não pode exceder 255 caracteres.");

    public static Error InvalidFormat => Error.Validation(
        "Email.InvalidFormat",
        "O email deve conter '@'.");
}