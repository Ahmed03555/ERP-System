using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetPermissionsList
{
    public record PermissionDto(int Id, string Name, string Module);
}
