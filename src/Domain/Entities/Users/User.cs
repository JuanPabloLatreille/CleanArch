using Domain.Shared;
using Domain.ValueObjects;

namespace Domain.Entities.Users;

public sealed class User
{
    private User(UserId id, string name, Email email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    private User()
    {
    }

    public UserId Id { get; private set; }

    public string Name { get; private set; }

    public Email Email { get; private set; }

    public static Result<User> Create(string name, string email)
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

        var user = new User(
            new UserId(Guid.NewGuid()),
            name.Trim(),
            emailResult.Value);

        return Result.Success(user);
    }
}