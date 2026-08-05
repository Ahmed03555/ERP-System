using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Auth___User
{
    public class Roles : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<UserRoles> UserRoles { get; set; } = [];
        public ICollection<RolePermissions> RolePermissions { get; set; } = [];
    }
}
