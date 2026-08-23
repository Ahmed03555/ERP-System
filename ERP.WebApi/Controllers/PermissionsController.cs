using ERP.Application.Common.Models.Roles.Commands.CreatePermission;
using ERP.Application.Common.Models.Roles.Queries.GetPermissionsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers;

[ApiController]
[Route("api/permissions")]
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

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetPermissionsListQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}