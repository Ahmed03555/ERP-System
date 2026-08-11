using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Queries.GetEmployeeById
{
    public record EmployeeDto(
            int Id,
            string FullName,
            string JobTitle,
            decimal Salary,
            DateOnly HireDate,
            int? DepartmentId,
            string? DepartmentName,
            int? ManagerId,
            string? ManagerName
    );

}
