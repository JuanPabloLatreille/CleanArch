using Domain.Entities.Users;
using Domain.Interfaces.Passwords;
using Domain.Interfaces.UnitOfWork;
using Domain.Interfaces.Users;
using Domain.Shared;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password) : IRequest<Result<Guid>>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser is not null)
        {
            return Result.Failure<Guid>(UserErrors.EmailAlreadyExists(request.Email));
        }

        var passwordValidation = Password.ValidatePlainText(request.Password);
        if (passwordValidation.IsFailure)
        {
            return Result.Failure<Guid>(passwordValidation.Error);
        }

        string passwordHash = _passwordHasher.Hash(request.Password);

        var userResult = User.Create(request.Name, request.Email, passwordHash);

        if (userResult.IsFailure)
        {
            return Result.Failure<Guid>(userResult.Error);
        }

        _userRepository.Add(userResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(userResult.Value.Id.Value);
    }
}