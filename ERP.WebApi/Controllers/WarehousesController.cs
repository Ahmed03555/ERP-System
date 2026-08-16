using ERP.Application.Common.Models.Warehouses.CreateWarehouse;
using ERP.Application.Common.Models.Warehouses.CreateWarehouse.DeleteWarehouse;
using ERP.Application.Common.Models.Warehouses.Queries.GetWarehouseById;
using ERP.Application.Common.Models.Warehouses.Queries.GetWarehouseById.GetWarehouseByListQuery;
using ERP.Application.Common.Models.Warehouses.UpdateWarehouse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WarehousesController : ControllerBase
    {
        private readonly ISender _sender;

        public WarehousesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWarehouseCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetWarehouseById(id));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sender.Send(new GetWarehouseByListQuery());

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , UpdateWarehouseCommand command)
        {
            if (command.Id != id)
                return BadRequest("Route ID and body ID do not match.");
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new DeleteWarehouseCommand(id));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
