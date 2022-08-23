using MediatR;
using Microsoft.AspNetCore.Mvc;
using products.Domain.Auth.Commands;

namespace products.Domain.Api.Controllers;
[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public UserController(IUserRepository userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
