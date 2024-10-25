using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse360.Api.Controllers;

[ApiController]
[Route("user-permissions")]
[Produces("application/json")]
public class UserPermissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserPermissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserPermissions()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
    
    [HttpPost("{permissionId:guid}/assign-to-user")]
    public async Task<IActionResult> AssignPermissionToUser()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
    
    [HttpPost("{permissionId:guid}/revoke-from-user")]
    public async Task<IActionResult> RevokePermissionFromUser()
    {
        var result = "await _mediator.Send(command)";
        return Ok(result);
    }
}