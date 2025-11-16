using Domain.Shared;

namespace Domain.Entities.Users;

public static class UserErrors
{
    public static Error NameEmpty => Error.Validation(
        "User.Name.Empty",
        "O nome não pode ser vazio.");

    public static Error NameTooLong => Error.Validation(
        "User.Name.TooLong",
        "O nome não pode exceder 100 caracteres.");

    public static Error NotFound(Guid userId) => Error.NotFound(
        "User.NotFound",
        $"O usuário com o ID '{userId}' não foi encontrado.");

    public static Error EmailAlreadyExists(string email) => Error.Conflict(
        "User.EmailAlreadyExists",
        $"O email '{email}' já está sendo utilizado por outro usuário.");
}