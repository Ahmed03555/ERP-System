using ERP.Application.Common.Models.SalesOrders.Commands.ConfirmSalesOrder;
using ERP.Application.Common.Models.SalesOrders.Commands.CreateSalesOrder;
using ERP.Application.Common.Models.SalesOrders.Queries;
using ERP.Application.Common.Models.SalesOrders.Queries.GetSalesOrdersList;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSalesOrderByIdQuery(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetSalesOrdersListQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

    }
}
