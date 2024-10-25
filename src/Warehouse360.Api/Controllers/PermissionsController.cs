using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse360.Application.IdentityManagement.Commands;

namespace Warehouse360.Api.Controllers;

[ApiController]
[Route("permissions")]
[Produces("application/json")]
public class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPermisions()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("{permissionId:guid}/assign-to-role")]
    public async Task<IActionResult> AssignPermissionToRole([FromBody] AssignPermissionToRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("{permissionId:guid}/revoke-from-role")]
    public async Task<IActionResult> RevokePermissionFromRole()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }

    [HttpPut("{permissionId:guid}")]
    public async Task<IActionResult> UpdatePermission()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
    
    [HttpDelete("{permissionId:guid}")]
    public async Task<IActionResult> DeletePermission()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
}