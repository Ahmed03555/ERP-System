using ERP.Application.Common.Models.Roles.Commands.CreatePermission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers;

[ApiController]
[Route("api/v1/permissions")]
//[Authorize]
public class PermissionsController : ControllerBase
{
    private readonly ISender _mediator;

    public PermissionsController(ISender mediator)
        => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreatePermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}