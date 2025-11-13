using MediatR;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<User>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IRepository<User> _userRepository;

    public GetAllUsersQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return users.ToList();
    }
}

