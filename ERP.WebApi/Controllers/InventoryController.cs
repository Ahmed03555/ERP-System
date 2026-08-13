using ERP.Application.Common.Models.Inventory.Commands.AdjustStock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly ISender _sender;
        public InventoryController(ISender sender)
        {
            _sender =sender;
        }
        [HttpPost("adjust-stock")]
        public async Task<IActionResult> AdjustStock(AdjustStockCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
