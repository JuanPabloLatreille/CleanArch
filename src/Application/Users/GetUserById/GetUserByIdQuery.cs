using Domain.Entities.Users;
using Domain.Interfaces.Users;
using Domain.Shared;
using MediatR;

namespace Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<GetUserByIdResult>>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResult>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetUserByIdResult>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetUserByIdResult>(UserErrors.NotFound(request.Id));
        }

        return Result.Success(GetUserByIdResult.FromEntity(user));
    }
}