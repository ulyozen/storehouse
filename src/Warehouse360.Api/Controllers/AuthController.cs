using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse360.Application.IdentityManagement.Commands;

namespace Warehouse360.Api.Controllers;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}