using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Auth___User
{
    public class UserRoles
    {
       public int UserId { get; set; }
        public Users User { get; set; } = default!;
        public int RoleId { get; set; }
        public Roles Role { get; set; } = default!;
    }
}
