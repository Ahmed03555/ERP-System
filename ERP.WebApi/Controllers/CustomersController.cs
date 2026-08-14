using ERP.Application.Common.Models.Customers.Commands.CreateCustomer;
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
    }
}
