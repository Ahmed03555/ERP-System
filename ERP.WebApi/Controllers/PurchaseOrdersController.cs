using ERP.Application.Common.Models;
using ERP.Application.Common.Models.Products.Queries.GetProductsList;
using ERP.Application.Common.Models.PurchaseOrders.Commands.CreatePurchaseOrder;
using ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrderById;
using ERP.Application.Common.Models.PurchaseOrders.Commands.ReceivePurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly ISender _sender;
        public PurchaseOrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseOrderCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }


        [HttpPost("{id}/receive")]
        public async Task<IActionResult> Recieve(int id , ReceivePurchaseOrderCommand command)
        {
            if (id != command.PurchaseOrderId)
                return BadRequest("Route ID and body ID do not match.");

            var result = await _sender.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetPurchaseOrderByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _sender.Send(new GetProductsListQuery());
            return result.IsSuccess ? Ok(result) : NotFound(result.Error);
        }
    }
}
