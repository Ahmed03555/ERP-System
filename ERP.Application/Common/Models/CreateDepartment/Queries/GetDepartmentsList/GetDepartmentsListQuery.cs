using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentsList
{
    public record GetDepartmentsListQuery : IRequest<Result<List<DepartmentDto>>>;

}
