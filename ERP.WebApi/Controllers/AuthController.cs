using ERP.Application.Common.Models.Auth.Commands.Login;
using ERP.Application.Common.Models.Auth.Commands.RefreshToken;
using ERP.Application.Common.Models.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _IMediator;

        public AuthController(ISender IMediator)
        {
            _IMediator = IMediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _IMediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _IMediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : Unauthorized(result.Error);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _IMediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : Unauthorized(result.Error);
        }
    }
}
