using Domain.Entities.Users;

namespace Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}