using ERP.Application.Common.Models.Payroll.Commands.GeneratePayroll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize]
    public class PayrollController : ControllerBase
    {
        private readonly ISender _sender;

        public PayrollController(ISender sender)
        {
            _sender=sender;
        }
        [HttpPost("{generate}")]
        public async Task<IActionResult> Generator(GeneratePayrollCommand command) 
        {
            var result = await _sender.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.Error);
        }
    }
}
