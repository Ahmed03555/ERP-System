using ERP.Application.Common.Models.CreateDepartment.Commands;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentsList;
using ERP.Application.Common.Models.UpdateDepartment;
using ERP.Application.Common.Models.DeleteDepartment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly ISender _sender;
        public DepartmentsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess
             ? Ok(result)
            : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetDepartmentByIdQuery(id));

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result.Error);
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            var result = await _sender.Send(new GetDepartmentsListQuery());
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentCommand command)
        {
            if(id != command.Id)
                return BadRequest("Route ID and body ID do not match.");
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new DeleteDepartmentCommand(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
