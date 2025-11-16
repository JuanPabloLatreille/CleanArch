using Domain.Entities.Users;

namespace Application.Users.GetUserById;

public record GetUserByIdResult(Guid Id, string Name, string Email)
{
    public static GetUserByIdResult FromEntity(User user)
    {
        return new GetUserByIdResult(user.Id.Value, user.Name, user.Email.Address);
    }
}