using ERP.Application.Common.Models.Products.Commands.CreateProduct;
using ERP.Application.Common.Models.Products.Commands.DeleteProduct;
using ERP.Application.Common.Models.Products.Commands.UpdateProduct;
using ERP.Application.Common.Models.Products.Queries.GetProductById;
using ERP.Application.Common.Models.Products.Queries.GetProductsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetProductsListQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateProductCommand command)
        {
            if(command.Id != id)
                return BadRequest("Route ID and body ID do not match.");
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
