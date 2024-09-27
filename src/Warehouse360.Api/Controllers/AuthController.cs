using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;

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
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] CreateUserCommand command)
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
    
    [HttpPost("role")]
    public async Task<IActionResult> Role([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("permission")]
    public async Task<IActionResult> Permission([FromBody] CreatePermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("AssignPermissionToRole")]
    public async Task<IActionResult> AssignPermissionToRole([FromBody] AssignPermissionToRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}