using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using ERP.Application.Common.Models.DeleteDepartment;
using ERP.Application.Common.Models.Employee.Commands.DeleteEmployee;
using ERP.Application.Common.Models.Employee.Commands.UpdateEmployee;
using ERP.Application.Common.Models.Employee.CreateEmployee;
using ERP.Application.Common.Models.Employee.Queries;
using ERP.Application.Common.Models.Employee.Queries.GetEmployeeById;
using ERP.Application.Common.Models.Employee.Queries.GetEmployeesListQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _mediator;

        public EmployeesController(ISender mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result.Errors);
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetEmployeesListQuery());
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeCommand command) 
        {
            if (id != command.Id)
                return BadRequest("Route ID and body ID do not match.");
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
