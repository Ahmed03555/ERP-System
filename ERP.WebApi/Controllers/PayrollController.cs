using ERP.Application.Common.Models.Categories.Queries.GetCategoriesList;
using ERP.Application.Common.Models.Payroll.Commands.GeneratePayroll;
using ERP.Application.Common.Models.Payroll.Queries.GetPayrollById;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetPayrollByIdQuery(id));
            return result.IsSuccess ? Ok(result): BadRequest(result.Error);
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _sender.Send(new GetCategoriesListQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
