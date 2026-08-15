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
            var userRepository = _unitOfWork.GetRepository<Users>();

            var user = await userRepository
                .Query()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Result<LoginResponse>.Failure("Invalid email or password.");

            if (!user.IsActive)
                return Result<LoginResponse>.Failure("This account is deactivated.");
   
            var roles = await _unitOfWork
                .GetRepository<UserRoles>()
                .Query()
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToListAsync(cancellationToken);

            var roleIds = await _unitOfWork
                .GetRepository<UserRoles>()
                .Query()
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId)
                .ToListAsync(cancellationToken);

            var permissions = await _unitOfWork
                .GetRepository<RolePermissions>()
                .Query()
                .Where(rp => roleIds.Contains(rp.RoleId))
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToListAsync(cancellationToken);

            var accessToken = _jwtService.GenerateAccessToken(user, roles, permissions);
            var refreshTokenValue = _jwtService.GenerateRefreshToken();

            var refreshToken = new RefreshTokens
            {
                UserId = user.Id,
                Token = refreshTokenValue,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(_jwtService.RefreshTokenExpirationDays)
            };

            var refreshTokenRepository = _unitOfWork.GetRepository<RefreshTokens>();
            await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<LoginResponse>.Success(new LoginResponse(accessToken, refreshTokenValue));
        }
    }
    }
