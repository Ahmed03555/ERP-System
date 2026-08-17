using ERP.Application.Common.Models.Suppliers.Commands.CreateSupplier;
using ERP.Application.Common.Models.Suppliers.Commands.DeleteSupplier;
using ERP.Application.Common.Models.Suppliers.Commands.UpdateSupplier;
using ERP.Application.Common.Models.Suppliers.GetSupplierById;
using ERP.Application.Common.Models.Suppliers.GetSupplierByList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly ISender _mediator;

        public SuppliersController(ISender mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSupplierByIdQuery(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetSuppliersListQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSupplierCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route ID and body ID do not match.");

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSupplierCommand(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
