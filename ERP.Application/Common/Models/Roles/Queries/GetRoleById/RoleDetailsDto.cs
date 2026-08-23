using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetRoleById
{
    public record RoleDetailsDto(int Id, string Name, List<string> Permissions);
}
