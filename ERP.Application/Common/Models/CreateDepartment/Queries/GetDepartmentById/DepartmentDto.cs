using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById
{
    public record DepartmentDto(
    int Id,
    string Name,
    
    string? ManagerName,
    int? ManagerId
        );
    
}
