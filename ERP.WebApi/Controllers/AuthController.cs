using ERP.Application.Common.Interfaces;
using ERP.Application.Common.Models.Auth.Commands.Login;
using ERP.Application.Common.Models.Auth.Commands.RefreshToken;
using ERP.Application.Common.Models.Auth.Commands.Register;
using ERP.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _IMediator;
        private readonly ICurrentUserService _currentUserService;
        public AuthController(ISender IMediator, ICurrentUserService currentUserService)
        {
            _IMediator = IMediator;
            _currentUserService= currentUserService;
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
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = _currentUserService.UserId,
                Email = _currentUserService.Email,
                IsAuthenticated = _currentUserService.IsAuthenticated
            });
        }
    }
}
