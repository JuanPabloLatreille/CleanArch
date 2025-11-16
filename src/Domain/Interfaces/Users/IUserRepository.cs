using Domain.Entities.Users;

namespace Domain.Interfaces.Users;

public interface IUserRepository
{
    void Add(User user);

    void Update(User user);

    void Remove(User user);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}