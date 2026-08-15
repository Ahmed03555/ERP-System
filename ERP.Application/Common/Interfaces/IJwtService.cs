using ERP.Domain.Entities.Auth___User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Users user, IList<string> roles,IList<string> permissions);
        string GenerateRefreshToken();
        int RefreshTokenExpirationDays { get; }
    }
}
