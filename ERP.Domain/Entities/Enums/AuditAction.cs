using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Enums
{
    public enum AuditAction
    {
        Create = 1,
        Update,
        Delete,
        Login,
        Logout
    }
}
