using MediatR;
using Microsoft.AspNetCore.Mvc;
using products.Domain.Auth.Commands;
using products.Domain.Auth.Contracts;

namespace products.Domain.Api.Controllers;
[ApiController]
[Route("login")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;
    private readonly IMediator _mediator;

    public AuthController(IAuthRepository authRepository, IMediator mediator)
    {
        _authRepository = authRepository;
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Login(AuthCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
