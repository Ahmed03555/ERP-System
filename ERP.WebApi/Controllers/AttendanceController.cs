using ERP.Application.Common.Models.Attendances.Commands.CheckIn;
using ERP.Application.Common.Models.Attendances.Commands.CheckOut;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly ISender _sender;
        public AttendanceController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut(CheckOutCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpPost("check-in")]
        public async Task<IActionResult> Checkin(CheckInCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
