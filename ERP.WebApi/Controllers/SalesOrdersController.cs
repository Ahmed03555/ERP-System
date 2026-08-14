using ERP.Application.Common.Models.SalesOrders.Commands.ConfirmSalesOrder;
using ERP.Application.Common.Models.SalesOrders.Commands.CreateSalesOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISender _mediator;

        public SalesOrdersController(ISender mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalesOrderCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }
        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> Confirm(int id, ConfirmSalesOrderCommand command)
        {
            if (id != command.SalesOrderId)
                return BadRequest("Route ID and body ID do not match.");

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
