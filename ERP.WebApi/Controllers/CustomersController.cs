using ERP.Application.Common.Models.Customers.Commands.CreateCustomer;
using ERP.Application.Common.Models.Customers.Commands.DeleteCustomer;
using ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomerById;
using ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomersList;
using ERP.Application.Common.Models.Customers.Commands.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ISender _sender;
        public CustomersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetCustomerByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _sender.Send(new GetCustomersListQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route ID and body ID do not match.");

            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new DeleteCustomerCommand(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
