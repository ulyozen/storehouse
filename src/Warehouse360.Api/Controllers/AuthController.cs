using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new AuthResultDto
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken
        });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new AuthResultDto
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken
        });
    }
}