using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Application.Features.Users.Queries.GetAllUsers;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Core.Domain.Entities.User>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }
}

