using ERP.Application.Common.Models.Roles.Commands.AssignPermissionToRole;
using ERP.Application.Common.Models.Roles.Commands.AssignRoleToUser;
using ERP.Application.Common.Models.Roles.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RolesController : ControllerBase
    {
        private readonly ISender _mediator;

        public RolesController(ISender mediator)
            => _mediator = mediator;

        [HttpPost("assign-to-user")]
        public async Task<IActionResult> AssignToUser(AssignRoleToUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }

        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermission(AssignPermissionToRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }


    }
}
