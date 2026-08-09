using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var Userepository = _unitOfWork.GetRepository<Users>();
            var user = await Userepository.Query().FirstOrDefaultAsync(x => x.Email == request.Email,cancellationToken);

            if (user is  null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) 
                return Result<LoginResponse>.Failure("Invalid email or password.");

            if(!user.IsActive)
                return Result<LoginResponse>.Failure("User account is inactive.");

            var accessToken = _jwtService.GenerateAccessToken(user,new List<string>());
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshTokens
            {
                UserId = user.Id,
                Token = refreshToken,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(_jwtService.RefreshTokenExpirationDays)
            };

            var refreshTokenRepository = _unitOfWork.GetRepository<RefreshTokens>();
            await refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<LoginResponse>.Success(new LoginResponse(accessToken, refreshToken));


        }
    }
}
