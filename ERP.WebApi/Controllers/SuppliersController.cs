using ERP.Application.Common.Models.Suppliers.Commands.CreateSupplier;
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
    }
}
