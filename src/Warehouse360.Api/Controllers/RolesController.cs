using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse360.Api.Controllers;

[ApiController]
[Route("roles")]
[Produces("application/json")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
    
    [HttpPost("{roleId:guid}/assign")]
    public async Task<IActionResult> AssignRole()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }

    [HttpPut("{roleId:guid}")]
    public async Task<IActionResult> UpdateRole()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
    
    [HttpDelete("{roleId:guid}")]
    public async Task<IActionResult> DeleteRole()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
}