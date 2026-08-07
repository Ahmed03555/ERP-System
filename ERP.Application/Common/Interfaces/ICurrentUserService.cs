using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Interfaces
{
  

    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        IReadOnlyList<string> Roles { get; }
    }
}

