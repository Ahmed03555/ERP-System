using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.UpdateDepartment
{
    public record UpdateDepartmentCommand(int Id, string Name, int? ManagerId) : IRequest<Result<bool>>;
}
