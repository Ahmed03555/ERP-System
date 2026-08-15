using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Auth___User
{
    public class RolePermissions : BaseEntity
    {
        public int RoleId { get; set; }
        public Roles Role { get; set; } = default!;
        public int PermissionId { get; set; }
        public Permissions Permission { get; set; } = default!;
    }
}
