using Application.Authentication.Interfaces;
using Domain.Interfaces.Passwords;
using Domain.Interfaces.Users;
using Domain.Shared;
using MediatR;

namespace Application.Authentication.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResult>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Credenciais inválidas");

        if (!_passwordHasher.Verify(request.Password, user.Password.Hash))
            throw new UnauthorizedAccessException("Credenciais inválidas");

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        var role = user.Email.Address.Contains("@insidesistemas", StringComparison.OrdinalIgnoreCase) 
            ? "Admin" 
            : "User";

        return new LoginResult(token, user.Email.Address, user.Name, role);
    }
}