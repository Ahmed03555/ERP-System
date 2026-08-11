using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.DeleteDepartment
{
    public record DeleteDepartmentCommand(int Id) : IRequest<Result<bool>>;

}
