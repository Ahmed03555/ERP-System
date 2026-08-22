using ERP.Application.Common.Interfaces;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentsList
{
    public record GetDepartmentsListQuery : IRequest<Result<List<DepartmentDto>>> , ICacheableQuery
    {
        public string CacheKey => "departments:list";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

}
