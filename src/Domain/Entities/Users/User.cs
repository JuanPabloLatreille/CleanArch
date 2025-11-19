using Domain.Shared;
using Domain.ValueObjects;

namespace Domain.Entities.Users;

public sealed class User
{
    private User(UserId id, string name, Email email, Password password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    private User()
    {
    }

    public UserId Id { get; private set; }

    public string Name { get; private set; }

    public Email Email { get; private set; }

    public Password Password { get; private set; }

    public static Result<User> Create(string name, string email, string passwordHash)
    {
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<User>(UserErrors.NameEmpty);
        }

        if (name.Length > 100)
        {
            return Result.Failure<User>(UserErrors.NameTooLong);
        }

        var password = Password.FromHash(passwordHash);

        var user = new User(
            new UserId(Guid.NewGuid()),
            name.Trim(),
            emailResult.Value,
            password);

        return Result.Success(user);
    }
}