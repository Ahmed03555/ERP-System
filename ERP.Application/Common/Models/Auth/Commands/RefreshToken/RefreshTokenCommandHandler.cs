using ERP.Application.Common.Interfaces;
using ERP.Application.Common.Models;
using ERP.Application.Common.Models.Auth.Commands.Login;
using ERP.Application.Common.Models.Auth.Commands.RefreshToken;

using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ERP.Application.Modules.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenRepository = _unitOfWork.GetRepository<RefreshTokens>();

        var storedToken = await refreshTokenRepository
            .Query()
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

  
        if (storedToken is null)
            return Result<LoginResponse>.Failure("Invalid refresh token.");

        var isExpired = DateTime.UtcNow >= storedToken.ExpiresOn;
        var isRevoked = storedToken.RevokedOn is not null;

        if (isExpired || isRevoked)
            return Result<LoginResponse>.Failure("Refresh token is no longer valid.");

       
        storedToken.RevokedOn = DateTime.UtcNow;
        refreshTokenRepository.UpdateAsync(storedToken);


        var roleIds = await _unitOfWork
            .GetRepository<UserRoles>()
            .Query()
            .Where(ur => ur.UserId == storedToken.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roles = await _unitOfWork
            .GetRepository<UserRoles>()
            .Query()
            .Where(ur => ur.UserId == storedToken.UserId)
            .Select(ur => ur.Role.Name)
            .ToListAsync(cancellationToken);

        var permissions = await _unitOfWork
            .GetRepository<RolePermissions>()
            .Query()
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.Permission.Name)
            .Distinct()
            .ToListAsync(cancellationToken);

        var newAccessToken = _jwtService.GenerateAccessToken(storedToken.User, roles, permissions);
        var newRefreshTokenValue = _jwtService.GenerateRefreshToken();

        var newRefreshToken = new RefreshTokens
        {
            UserId = storedToken.UserId,
            Token = newRefreshTokenValue,
            CreatedOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddDays(_jwtService.RefreshTokenExpirationDays)
        };

        await refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LoginResponse>.Success(new LoginResponse(newAccessToken, newRefreshTokenValue));
    }
}